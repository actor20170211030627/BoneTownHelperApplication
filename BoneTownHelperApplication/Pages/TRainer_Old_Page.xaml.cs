using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using BoneTownHelperApplication.Utils;
using Gma.System.MouseKeyHook;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace BoneTownHelperApplication.Pages {
    
    public partial class TRainer_Old_Page : Page {
        
        //进程是否打开
        private bool _isProcOpen = false;
        //修改器是否激活
        private bool _isTRainerOpen = true;
        //中止日/夜循环
        private bool _isPauseDaylight = false;
        //灯光是否打开
        private bool _isLampOpen = true;
        //是否冻结无限健康
        private bool isFreezeHealth = false;
        //冻结跳高效果
        private bool isFreezeHighJump = false;
        //冻结护盾效果
        private bool isFreezeShield = false;
        //冻结隐身效果
        private bool isFreezeInvisible = false;
        //冻结撞飞效果
        private bool isFreezeDamageTouches = false;
        //冻结快跑效果
        private bool isFreezeFastRun = false;
        //冻结快感进度
        private bool isFreezeClimax = false;
        //潜水
        private bool isDiving = false;
        //是否取消冻结所有
        private bool isUnfreezeAll = true;


        private DispatcherTimer _dispatcherTimer;

        private IKeyboardMouseEvents m_GlobalHook;

        public TRainer_Old_Page() {
            InitializeComponent();
            
            this.Loaded += MyPage_Loaded;  // 订阅Loaded事件
            this.Unloaded += MyPage_Unloaded;  // 订阅Unloaded事件
            
            InitializeTimer();
            InitializeMouseKeyHook();
            
            
            int[] xyzDistanceArray = {1, 2, 5, 10, 15, 20, 30, 40, 50, 60, 70, 80, 90, 100};
            this.ComboBox_XYZDistance.ItemsSource = xyzDistanceArray;
            // this.ComboBox_XYZDistance.SelectedItem = xyzDistanceArray[2];
            // this.ComboBox_XYZDistance.SelectedIndex = 2;
            

            //钱Money
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Money, delegate(string s) {
                // Console.WriteLine($"钱Money: {s}");
                // 使用 Dispatcher 切换到 UI 线程
                Dispatcher.Invoke(() => {
                    this.TB_Money.Text = s;
                });
            });
            //啤酒Beer
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Beer, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Beer.Text = s;
                });
            });
            //威士忌Whiskey
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Whiskey, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Whiskey.Text = s;
                });
            });
            //大麻Weed🍃
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Weed, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Nug.Text = s;
                });
            });
            //迷幻蘑菇🍄Shroom
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Shroom, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Shroom.Text = s;
                });
            });
            //乌羽玉仙人掌的干燥茎块(Peyote Button)🌵
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Peyote, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Peyote.Text = s;
                });
            });
            //蟾蜍Toad🐸
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Frog, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Frog.Text = s;
                });
            });
            //强效可卡因(Crack)
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Crack, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Crack.Text = s;
                });
            });
            //换地图后?loop就停下来了, 框架有问题?
            if (false) {
                //z轴 ZAxis
                MemoryDllUtils.BindToUI<decimal>(TRainerHelper.ZAxis, delegate(string s) {
                    bool success = float.TryParse(s, out float value);
                    Console.WriteLine($"s = {s}, success = {success}, value = {value}");
                    Dispatcher.Invoke(() => {
                        this.TB_ZAxis.Text = ((int) value).ToString();
                    });
                });
            }
            
            
            /*
             * 监听按键: 只能在窗口获取焦点时才管用
             */
            if (false) {
                this.KeyDown += delegate(object sender, KeyEventArgs e) {
                    // 判断是否同时按下了Ctrl键和S键
                    if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control) {
                        // 执行你的保存逻辑
                        MessageBox.Show("保存命令被触发！");
            
                        // 标记事件为已处理，防止继续传递
                        e.Handled = true;
                    }
                };
            }

            long clickTime = 0L;
            //jj性感度
            this.Slider_Balls_Size.ValueChanged += (sender, args) => {
                //Value: 0~10, F0：强制取整数，没有小数位
                this.Label_Balls_Size.Content = $"{((Slider)sender).Value * 10:F0}%";
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                // 毫秒级 long 时间戳（最推荐）
                long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                if (timeStamp - clickTime > 300L) {
                    clickTime = timeStamp;
                    //避免回调频率太快, 播放声音过于密集
                    TRainerHelper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) TRainerHelper.SetBallsSize((float) ((Slider)sender).Value);
            };
            //攻击力
            this.Slider_FightBuff.ValueChanged += (sender, args) => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                // 毫秒级 long 时间戳（最推荐）
                long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                if (timeStamp - clickTime > 300L) {
                    clickTime = timeStamp;
                    //避免回调频率太快, 播放声音过于密集
                    TRainerHelper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) TRainerHelper.SetClothing_Health((int) ((Slider)sender).Value);
            };
            //跳高
            this.Slider_High_Jump.ValueChanged += (sender, args) => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                // 毫秒级 long 时间戳（最推荐）
                long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                if (timeStamp - clickTime > 300L) {
                    clickTime = timeStamp;
                    //避免回调频率太快, 播放声音过于密集
                    TRainerHelper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                /**
                 * 为何要判断???
                 * 因为DispatcherTimer一直在设置, Slider这儿会回调, 而实际值一直在减少, 会导致Slider一直往下滑
                 * 并且游戏中手动使用了物品后, DispatcherTimer中也会触发Slider值的改变, 导致值在Slider的ValueChanged中被冻结
                 */
                if (isFromUser) {
                    float value = (float)((Slider)sender).Value;
                    isFreezeHighJump = value > 0;
                    if (isFreezeHighJump) isUnfreezeAll = false;
                    TRainerHelper.SetJumpHigher(value, isFreezeHighJump, false);
                }
            };
            //护盾
            this.Slider_Shield.ValueChanged += (sender, args) => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                // 毫秒级 long 时间戳（最推荐）
                long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                if (timeStamp - clickTime > 300L) {
                    clickTime = timeStamp;
                    //避免回调频率太快, 播放声音过于密集
                    TRainerHelper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) {
                    float value = (float)((Slider)sender).Value;
                    isFreezeShield = value > 0;
                    if (isFreezeShield) isUnfreezeAll = false;
                    TRainerHelper.SetShield(value, isFreezeShield, false);
                }
            };
            //隐身
            this.Slider_Invisible.ValueChanged += (sender, args) => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                // 毫秒级 long 时间戳（最推荐）
                long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                if (timeStamp - clickTime > 300L) {
                    clickTime = timeStamp;
                    //避免回调频率太快, 播放声音过于密集
                    TRainerHelper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) {
                    float value = (float)((Slider)sender).Value;
                    isFreezeInvisible = value > 0;
                    if (isFreezeInvisible) isUnfreezeAll = false;
                    TRainerHelper.SetInvisible(value, isFreezeInvisible, false);
                }
            };
            //撞飞
            this.Slider_Damage_Touches.ValueChanged += (sender, args) => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                // 毫秒级 long 时间戳（最推荐）
                long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                if (timeStamp - clickTime > 300L) {
                    clickTime = timeStamp;
                    //避免回调频率太快, 播放声音过于密集
                    TRainerHelper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) {
                    float value = (float)((Slider)sender).Value;
                    isFreezeDamageTouches = value > 0;
                    if (isFreezeDamageTouches) isUnfreezeAll = false;
                    TRainerHelper.SetDamageTouches(value, isFreezeDamageTouches, false);
                }
            };
            //快跑🏃‍♀️
            this.Slider_Fast_Run.ValueChanged += (sender, args) => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                // 毫秒级 long 时间戳（最推荐）
                long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                if (timeStamp - clickTime > 300L) {
                    clickTime = timeStamp;
                    //避免回调频率太快, 播放声音过于密集
                    TRainerHelper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) {
                    float value = (float)((Slider)sender).Value;
                    isFreezeFastRun = value > 0;
                    if (isFreezeFastRun) isUnfreezeAll = false;
                    TRainerHelper.SetFastRun(value, isFreezeFastRun, false);
                }
            };
        }
        
        private void MyPage_Loaded(object sender, RoutedEventArgs e) {
            Console.WriteLine("页面加载完成 - 在这里初始化数据");
            // 加载数据、绑定事件等
        }

        private void InitializeTimer() {
            // 创建定时器，间隔为 1 秒
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(300.0);
            _dispatcherTimer.Tick += delegate(object sender, EventArgs args) {
                _isProcOpen = MemoryDllUtils.OpenProcess(TRainerHelper.ProcessName);
                if (_isProcOpen) {
                    if (!_isTRainerOpen) return;

                    //z轴 ZAxis
                    float zAxis = MemoryDllUtils.ReadFloat(TRainerHelper.ZAxis);
                    this.TB_ZAxis.Text = ((int) zAxis).ToString();
                    
                    //jj性感度
                    this.Slider_Balls_Size.Value = TRainerHelper.GetBallsSize();
                    //攻击力
                    this.Slider_FightBuff.Value = TRainerHelper.GetClothing_Health();
                    //跳高
                    this.Slider_High_Jump.Value = TRainerHelper.GetHighJump();
                    //护盾
                    this.Slider_Shield.Value = TRainerHelper.GetShield();
                    //隐身
                    this.Slider_Invisible.Value = TRainerHelper.GetInvisible();
                    //撞飞
                    this.Slider_Damage_Touches.Value = TRainerHelper.GetDamageTouches();
                    //快跑🏃‍♀️
                    this.Slider_Fast_Run.Value = TRainerHelper.GetFastRun();
                } else {
                    // Console.WriteLine($"openProcessSuccess: {_isProcOpen}");
                    UnfreezeAll();
                }

                this.Border_Running.Visibility = _isProcOpen ? Visibility.Visible : Visibility.Collapsed;
                this.Border_Stopped.Visibility = _isProcOpen ? Visibility.Collapsed : Visibility.Visible;
                
                //设置传送Grid的显示/隐藏
                int mapPosition = TRainerHelper.GetMapPosition();
                SetGridElementVisibility(this.Grid_Teleport, 1, 4, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 1, 5, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 2, 3, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 2, 4, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 3, 3, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 4, 3, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 4, 4, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 4, 5, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 5, 3, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 6, 3, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 6, 4, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 6, 5, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 7, 2, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 7, 3, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 7, 4, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 8, 2, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 8, 3, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 8, 4, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 8, 5, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 9, 4, mapPosition);
                SetGridElementVisibility(this.Grid_Teleport, 9, 5, mapPosition);
            };
            _dispatcherTimer.Start();
        }

        /// <summary>
        /// 初始化键盘鼠标hook
        /// </summary>
        private void InitializeMouseKeyHook() {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyUp += GlobalHookKeyUp;
            
            
            //1. Define key combinations
            //                                                               +数字无效???
            // Combination combinationMoney = Combination.FromString("Control+0");
            // Combination combinationBeer = Combination.FromString("Control+1");
            // Key.LeftCtrl;
            // Key.D0;
            Combination combinationMoney = Combination.TriggeredBy(Keys.D0).With(Keys.Control);
            // Combination combinationMoney = Combination.TriggeredBy(Keys.NumPad0).With(Keys.Control);
            Combination combinationBeer = Combination.TriggeredBy(Keys.D1).Control();
            Combination combinationWhiskey = Combination.TriggeredBy(Keys.D2).Control();
            Combination combinationNug = Combination.TriggeredBy(Keys.D3).Control();
            Combination combinationShroom = Combination.TriggeredBy(Keys.D4).Control();
            Combination combinationPeyote = Combination.TriggeredBy(Keys.D5).Control();
            Combination combinationFrog = Combination.TriggeredBy(Keys.D6).Control();
            Combination combinationCrack = Combination.TriggeredBy(Keys.D7).Control();
            Combination combinationHeightAdd = Combination.TriggeredBy(Keys.H).Control();
            Combination combinationHeightMinus = Combination.TriggeredBy(Keys.L).Control();

            //2. Define actions
            Action actionMoney = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerHelper.MoneyAdd();
            };
            Action actionBeer = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerHelper.BeerAdd();
            };
            Action actionWhiskey = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerHelper.WhiskeyAdd();
            };
            Action actionNug = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerHelper.NugAdd();
            };
            Action actionShroom = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerHelper.ShroomAdd();
            };
            Action actionPeyote = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerHelper.PeyoteAdd();
            };
            Action actionFrog = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerHelper.FrogAdd();
            };
            Action actionCrack = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerHelper.CrackAdd();
            };
            Action actionHeightAdd = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerHelper.ZAxisEdit(true, value);
            };
            Action actionHeightMinus = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerHelper.ZAxisEdit(false, value);
            };

            //3. Assign actions to key combinations
            var assignment = new Dictionary<Combination, Action> {
                {combinationMoney, actionMoney},
                {combinationBeer, actionBeer},
                {combinationWhiskey, actionWhiskey},
                {combinationNug, actionNug},
                {combinationShroom, actionShroom},
                {combinationPeyote, actionPeyote},
                {combinationFrog, actionFrog},
                {combinationCrack, actionCrack},
                {combinationHeightAdd, actionHeightAdd},
                {combinationHeightMinus, actionHeightMinus}
            };

            //4. Install listener
            // Hook.GlobalEvents().OnCombination(assignment);
            m_GlobalHook.OnCombination(assignment);
        }

        private void GlobalHookKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
            // KeyCode = Left, KeyData = Left, KeyValue = 37, SuppressKeyPress = False
            // KeyCode = Up, KeyData = Up, KeyValue = 38, SuppressKeyPress = False
            // KeyCode = Right, KeyData = Right, KeyValue = 39, SuppressKeyPress = False
            // KeyCode = Down, KeyData = Down, KeyValue = 40, SuppressKeyPress = False
            // Console.WriteLine($"KeyCode = {e.KeyCode}, KeyData = {e.KeyData}, KeyValue = {e.KeyValue}, SuppressKeyPress = {e.SuppressKeyPress}");

            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;
            
            if (e.KeyCode == Keys.Right) {  //东
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerHelper.GoRightOrLeft(true, value);
                return;
            }
            if (e.KeyCode == Keys.Left) {   //西
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerHelper.GoRightOrLeft(false, value);
                return;
            }
            if (e.KeyCode == Keys.Up) {     //北
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerHelper.GoFrontOrBack(true, value);
                return;
            }
            if (e.KeyCode == Keys.Down) {   //南
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerHelper.GoFrontOrBack(false, value);
                return;
            }
        }
        

        private void Btn_OnClick(object sender, RoutedEventArgs e) {
            
            TRainerHelper.PlayClick();

            if (!(sender is FrameworkElement fe)) return;
            string name = fe.Name;
            
            //播放Ang
            if (name == this.Image_Play_Ang.Name) {
                TRainerHelper.IsPlayAng = !TRainerHelper.IsPlayAng;
                Uri uri = TRainerHelper.GetSwitchUri(TRainerHelper.IsPlayAng);
                this.Image_Play_Ang.Source = new BitmapImage(uri);
                if (TRainerHelper.IsPlayAng) {
                    TRainerHelper.PlayAng();
                }
                return;
            }
            //激活修改器(TRainer activate)
            if (name == this.Image_TRainer_State.Name) {
                _isTRainerOpen = !_isTRainerOpen;
                Uri uri = TRainerHelper.GetSwitchUri(_isTRainerOpen);
                this.Image_TRainer_State.Source = new BitmapImage(uri);
                TRainerHelper.PlayActivate(_isTRainerOpen);
                if (!_isTRainerOpen) {
                    UnfreezeAll();
                }
                return;
            }
            //关于
            if (name == this.Btn_About.Name) {
                MessageBox.Show(TRainerHelper.StrAbout, "说明(explain):");
                return;
            }

            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;
            
            //钱💰
            if (name == this.Btn_Money.Name) {
                TRainerHelper.MoneyAdd();
                return;
            }
            
            //啤酒🍺
            if (name == this.Btn_Beer.Name) {
                TRainerHelper.BeerAdd();
                return;
            }
            //威士忌🤳
            if (name == this.Btn_Whiskey.Name) {
                TRainerHelper.WhiskeyAdd();
                return;
            }
            //大麻Weed🍃
            if (name == this.Btn_Nug.Name) {
                TRainerHelper.NugAdd();
                return;
            }
            //迷幻蘑菇🍄Shroom
            if (name == this.Btn_Shroom.Name) {
                TRainerHelper.ShroomAdd();
                return;
            }
            //乌羽玉仙人掌的干燥茎块(Peyote Button)🌵
            if (name == this.Btn_Peyote.Name) {
                TRainerHelper.PeyoteAdd();
                return;
            }
            //蟾蜍Toad🐸
            if (name == this.Btn_Frog.Name) {
                TRainerHelper.FrogAdd();
                return;
            }
            //强效可卡因(Crack)
            if (name == this.Btn_Crack.Name) {
                TRainerHelper.CrackAdd();
                return;
            }
            
            //东
            if (name == this.Btn_XAxis_Plus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerHelper.GoRightOrLeft(true, value);
                return;
            }
            //西
            if (name == this.Btn_XAxis_Minus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerHelper.GoRightOrLeft(false, value);
                return;
            }
            
            //北
            if (name == this.Btn_YAxis_Plus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerHelper.GoFrontOrBack(true, value);
                return;
            }
            //南
            if (name == this.Btn_YAxis_Minus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerHelper.GoFrontOrBack(false, value);
                return;
            }
            
            //高度+
            if (name == this.Btn_ZAxis_Plus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerHelper.ZAxisEdit(true, value);
                return;
            }
            //高度-
            if (name == this.Btn_ZAxis_Minus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerHelper.ZAxisEdit(false, value);
                return;
            }
            //无限健康
            if (name == this.Image_Infinite_Health.Name) {
                isFreezeHealth = !isFreezeHealth;
                Uri uri = TRainerHelper.GetSwitchUri(isFreezeHealth);
                this.Image_Infinite_Health.Source = new BitmapImage(uri);
                TRainerHelper.FreezeHealth(isFreezeHealth);
                if (isFreezeHealth) isUnfreezeAll = false;
                return;
            }
            //冻结快感进度
            if (name == this.Image_Freeze_Climax.Name) {
                isFreezeClimax = !isFreezeClimax;
                Uri uri = TRainerHelper.GetSwitchUri(isFreezeClimax);
                this.Image_Freeze_Climax.Source = new BitmapImage(uri);
                TRainerHelper.FreezeClimax(isFreezeClimax);
                if (isFreezeClimax) isUnfreezeAll = false;
                return;
            }
            //潜水
            if (name == this.Image_Diving.Name) {
                isDiving = !isDiving;
                Uri uri = TRainerHelper.GetSwitchUri(isDiving);
                this.Image_Diving.Source = new BitmapImage(uri);
                TRainerHelper.FreezeDiving(isDiving);
                if (isDiving) {
                    isUnfreezeAll = false;
                }
                return;
            }
        }


        private void OnTeleportClick(object sender, RoutedEventArgs e) {

            TRainerHelper.PlayClick();

            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;

            if (!(sender is FrameworkElement fe)) return;
            string name = fe.Name;

            //Map1(Missionary Beach 传教士海滩)→Map2(Firm Wood Forest 阔叶林)
            if (name == this.TB_MissionaryBeach2FirmWoodForest.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map1_to_Map2, "Map1(Missionary Beach 传教士海滩)→Map2(Firm Wood Forest 阔叶林) 传送点失败!");
                return;
            }
            //Map1(Missionary Beach 传教士海滩)→Map4(Gabacho Heights 加巴乔高地)
            if (name == this.TB_MissionaryBeach2GabachoHeights.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map1_to_Map4, "Map1(Missionary Beach 传教士海滩)→Map4(Gabacho Heights 加巴乔高地) 传送点失败!");
                return;
            }
            
            //Map2(Firm Wood Forest 阔叶林)→Map1(Missionary Beach 传教士海滩)
            if (name == this.TB_FirmWoodForest2MissionaryBeach.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map2_to_Map1, "Map2(Firm Wood Forest 阔叶林)→Map1(Missionary Beach 传教士海滩) 传送点失败!");
                return;
            }
            //Map2(Firm Wood Forest 阔叶林)→Map3(Homeland Trailer Park 国土安全拖车公园)
            if (name == this.TB_FirmWoodForest2HomelandTrailerPark.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map2_to_Map3, "Map2(Firm Wood Forest 阔叶林)→Map3(Homeland Trailer Park 国土安全拖车公园) 传送点失败!");
                return;
            }
            
            //Map3(Homeland Trailer Park 国土安全拖车公园)→Map2(Firm Wood Forest 阔叶林)
            if (name == this.TB_HomelandTrailerPark2FirmWoodForest.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map3_to_Map2, "Map3(Homeland Trailer Park 国土安全拖车公园)→Map2(Firm Wood Forest 阔叶林) 传送点失败!");
                return;
            }

            //Map4(Gabacho Heights 加巴乔高地)→Map1(Missionary Beach 传教士海滩)
            if (name == this.TB_GabachoHeights2MissionaryBeach.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map4_to_Map1, "Map4(Gabacho Heights 加巴乔高地)→Map1(Missionary Beach 传教士海滩) 传送点失败!");
                return;
            }
            //Map4(Gabacho Heights 加巴乔高地)→Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)
            if (name == this.TB_GabachoHeights2HavajoIndianReservation.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map4_to_Map5, "Map4(Gabacho Heights 加巴乔高地)→Map5(Havajo Indian Reservation 哈瓦那印第安人保留地) 传送点失败!");
                return;
            }
            //Map4(Gabacho Heights 加巴乔高地)→Map6(Nobbing Hill 诺丁山)
            if (name == this.TB_GabachoHeights2NobbingHill.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map4_to_Map6, "Map4(Gabacho Heights 加巴乔高地)→Map6(Nobbing Hill 诺丁山) 传送点失败!");
                return;
            }
            
            
            //Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)→Map4(Gabacho Heights 加巴乔高地)
            if (name == this.TB_HavajoIndianReservation2GabachoHeights.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map5_to_Map4, "Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)→Map4(Gabacho Heights 加巴乔高地) 传送点失败!");
                return;
            }
            
            
            //Map6(Nobbing Hill 诺丁山)→Map8(DownTown 市中心)
            if (name == this.TB_NobbingHill2DownTown.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map6_to_Map8, "Map6(Nobbing Hill 诺丁山)→Map8(DownTown 市中心) 传送点失败!");
                return;
            }
            //Map6(Nobbing Hill 诺丁山)→Map4(Gabacho Heights 加巴乔高地)
            if (name == this.TB_NobbingHill2GabachoHeights.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map6_to_Map4, "Map6(Nobbing Hill 诺丁山)→Map4(Gabacho Heights 加巴乔高地) 传送点失败!");
                return;
            }
            //Map6(Nobbing Hill 诺丁山)→Map7(Mushroom Marsh 蘑菇沼泽)
            if (name == this.TB_NobbingHill2MushroomMarsh.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map6_to_Map7, "Map6(Nobbing Hill 诺丁山)→Map7(Mushroom Marsh 蘑菇沼泽) 传送点失败!");
                return;
            }
            
            
            //Map7(Mushroom Marsh 蘑菇沼泽) 撒旦(Satan)
            if (name == this.TB_MushroomMarsh_Satan.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map7_to_Satan, "瞬移到 Map7(Mushroom Marsh 蘑菇沼泽) 撒旦(Satan) 失败!");
                return;
            }
            //Map7(Mushroom Marsh 蘑菇沼泽) 撒旦的老婆(Satan's wife)
            if (name == this.TB_MushroomMarsh_Satan_wife.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map7_to_SatanWife, "瞬移到 Map7(Mushroom Marsh 蘑菇沼泽) 撒旦的老婆(Satan's wife) 失败!");
                return;
            }
            //Map7(Mushroom Marsh 蘑菇沼泽)→Map6(Nobbing Hill 诺丁山)
            if (name == this.TB_MushroomMarsh2NobbingHill.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map7_to_Map6, "Map7(Mushroom Marsh 蘑菇沼泽)→Map6(Nobbing Hill 诺丁山) 失败!");
                return;
            }
            
            
            //Map8(DownTown 市中心) RonJ大富翁
            if (name == this.TB_Downtown_RonJTowers.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map8_to_RonJEntrance, "瞬移到 Map8(DownTown 市中心) RonJ大富翁 失败!");
                return;
            }
            //Map8(DownTown 市中心) 天使
            if (name == this.TB_Downtown_Angle.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map8_to_Angle, "瞬移到 Map8(DownTown 市中心) 天使 失败!");
                return;
            }
            //Map8(DownTown 市中心)→Map9(Man Island 曼岛) 传送点
            if (name == this.TB_DownTown2ManIsland.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map8_to_Map9, "Map8(DownTown 市中心)→Map9(Man Island 曼岛) 传送点失败!");
                return;
            }
            //Map8(DownTown 市中心)→Map6(Nobbing Hill 诺丁山) 传送点
            if (name == this.TB_DownTown2NobbingHill.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map8_to_Map6, "Map8(DownTown 市中心)→Map6(Nobbing Hill 诺丁山) 传送点失败!");
                return;
            }
            
            
            //Map9(Man Island 曼岛)→高塔入口(Man Needle)
            if (name == this.TB_ManIsland2ManNeedle.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map9_to_ManNeedle, "Map9(Man Island 曼岛)→高塔入口(Man Needle) 传送点失败!");
                return;
            }
            //Map9(Man Island 曼岛)→Map8(DownTown 市中心)
            if (name == this.TB_ManIsland2DownTown.Name) {
                TRainerHelper.Teleport(TRainerHelper.Map9_to_Map8, "Map9(Man Island 曼岛)→Map8(DownTown 市中心) 传送点失败!");
                return;
            }
        }

        private void Light_OnClick(object sender, RoutedEventArgs e) {
            TRainerHelper.PlayClick();
            
            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;

            if (sender is Image image) {
                string nameImage = image.Name;
                if (nameImage == this.Image_Pause_Daylight.Name) {
                    _isPauseDaylight = !_isPauseDaylight;
                    Uri uri = TRainerHelper.GetSwitchUri(_isPauseDaylight);
                    this.Image_Pause_Daylight.Source = new BitmapImage(uri);
                    TRainerHelper.PauseDaylight(_isPauseDaylight, true, true);
                    isUnfreezeAll = false;
                    return;
                }
                if (nameImage == this.Image_Lamp_State.Name) {
                    _isLampOpen = !_isLampOpen;
                    Uri uri = TRainerHelper.GetSwitchUri(_isLampOpen);
                    this.Image_Lamp_State.Source = new BitmapImage(uri);
                    TRainerHelper.LampLightSet(_isLampOpen, true);
                    return;
                }
                return;
            }
            
            if (!(sender is System.Windows.Controls.Control control)) return;
            
            string name = control.Name;

            /**
             * 环境亮度设置
             */
            //黎明(Morning)
            if (name == this.RB_Brightness_Morning.Name) {
                TRainerHelper.BrightnessSet(0, true);
                return;
            }
            //白昼(Daytime)
            if (name == this.RB_Brightness_Daytime.Name) {
                TRainerHelper.BrightnessSet(1, true);
                return;
            }
            //傍晚(Evening)
            if (name == this.RB_Brightness_Evening.Name) {
                TRainerHelper.BrightnessSet(2, true);
                return;
            }
            //黄昏(Dusk)
            if (name == this.RB_Brightness_Dusk.Name) {
                TRainerHelper.BrightnessSet(3, true);
                return;
            }
            //午夜(Midnight)
            if (name == this.RB_Brightness_Midnight.Name) {
                TRainerHelper.BrightnessSet(4, true);
                return;
            }
            //拂晓(Dawn)
            if (name == this.RB_Brightness_Dawn.Name) {
                TRainerHelper.BrightnessSet(5, true);
                return;
            }
        }

        /// <summary>
        /// 设置Grid中行列对应的子元素的显示/隐藏
        /// </summary>
        /// <param name="grid">目标Grid</param>
        /// <param name="rowIndex">行索引（从0开始）</param>
        /// <param name="colIndex">列索引（从0开始）</param>
        /// <param name="mapPosition">第几张地图</param>
        private void SetGridElementVisibility(Grid grid, int rowIndex, int colIndex, int mapPosition) {
            foreach (UIElement element in grid.Children) {
                if (Grid.GetRow(element) == rowIndex && Grid.GetColumn(element) == colIndex) {
                    if (mapPosition < 0) {
                        element.Visibility = Visibility.Hidden;
                    } else {
                        element.Visibility = rowIndex == mapPosition ? Visibility.Visible : Visibility.Hidden;
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// 取消所有冻结
        /// </summary>
        private void UnfreezeAll() {
            if (isUnfreezeAll) return;
            TRainerHelper.FreezeHealth(false);
            TRainerHelper.SetJumpHigher(0, false, false);
            TRainerHelper.SetShield(0, false, false);
            TRainerHelper.SetInvisible(0, false, false);
            TRainerHelper.SetDamageTouches(0, false, false);
            TRainerHelper.SetFastRun(0, false, false);
            TRainerHelper.FreezeClimax(false);
            TRainerHelper.PauseDaylight(false, false, false);
            TRainerHelper.FreezeDiving(false);
            isUnfreezeAll = true;
        }
        
        // 页面卸载时执行 - 这是主要的方法
        private void MyPage_Unloaded(object sender, RoutedEventArgs e) {
            Console.WriteLine("页面卸载 - 在这里清理资源");
            
            m_GlobalHook.KeyUp -= GlobalHookKeyUp;
            //It is recommened to dispose it
            m_GlobalHook.Dispose();
            
            //停止定时器
            _dispatcherTimer.Stop();
            // _dispatcherTimer.Tick -= ;
            MemoryDllUtils.CloseProcess();
        }
    }
}