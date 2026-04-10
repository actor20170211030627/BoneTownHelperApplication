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
using MessageBox = System.Windows.MessageBox;

namespace BoneTownHelperApplication.Pages {
    public partial class TRainer_Edition_QQ_Group_32_Page : Page {
        
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

        public TRainer_Edition_QQ_Group_32_Page() {
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
            MemoryDllUtils.BindToUI<int>(TRainerEditionQQGroup32Helper.Money, delegate(string s) {
                // Console.WriteLine($"钱Money: {s}");
                // 使用 Dispatcher 切换到 UI 线程
                Dispatcher.Invoke(() => {
                    this.TB_Money.Text = s;
                });
            });
            //啤酒Beer
            MemoryDllUtils.BindToUI<int>(TRainerEditionQQGroup32Helper.Beer, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Beer.Text = s;
                });
            });
            //威士忌Whiskey
            MemoryDllUtils.BindToUI<int>(TRainerEditionQQGroup32Helper.Whiskey, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Whiskey.Text = s;
                });
            });
            //小块大麻Nug🍃
            MemoryDllUtils.BindToUI<int>(TRainerEditionQQGroup32Helper.Weed, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Nug.Text = s;
                });
            });
            //迷幻蘑菇🍄Shroom
            MemoryDllUtils.BindToUI<int>(TRainerEditionQQGroup32Helper.Shroom, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Shroom.Text = s;
                });
            });
            //乌羽玉仙人掌的干燥茎块(Peyote Button)🌵
            MemoryDllUtils.BindToUI<int>(TRainerEditionQQGroup32Helper.Peyote, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Peyote.Text = s;
                });
            });
            //蟾蜍Toad🐸
            MemoryDllUtils.BindToUI<int>(TRainerEditionQQGroup32Helper.Frog, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Frog.Text = s;
                });
            });
            //可卡因块(Rock)
            MemoryDllUtils.BindToUI<int>(TRainerEditionQQGroup32Helper.Crack, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Crack.Text = s;
                });
            });

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
                    TRainerEditionQQGroup32Helper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) TRainerEditionQQGroup32Helper.SetBallsSize((float) ((Slider)sender).Value);
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
                    TRainerEditionQQGroup32Helper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) TRainerEditionQQGroup32Helper.SetClothing_Health((int) ((Slider)sender).Value);
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
                    TRainerEditionQQGroup32Helper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) {
                    float value = (float)((Slider)sender).Value;
                    isFreezeHighJump = value > 0;
                    if (isFreezeHighJump) isUnfreezeAll = false;
                    TRainerEditionQQGroup32Helper.SetJumpHigher(value, isFreezeHighJump, false);
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
                    TRainerEditionQQGroup32Helper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) {
                    float value = (float)((Slider)sender).Value;
                    isFreezeShield = value > 0;
                    if (isFreezeShield) isUnfreezeAll = false;
                    TRainerEditionQQGroup32Helper.SetShield(value, isFreezeShield, false);
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
                    TRainerEditionQQGroup32Helper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) {
                    float value = (float)((Slider)sender).Value;
                    isFreezeInvisible = value > 0;
                    if (isFreezeInvisible) isUnfreezeAll = false;
                    TRainerEditionQQGroup32Helper.SetInvisible(value, isFreezeInvisible, false);
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
                    TRainerEditionQQGroup32Helper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) {
                    float value = (float)((Slider)sender).Value;
                    isFreezeDamageTouches = value > 0;
                    if (isFreezeDamageTouches) isUnfreezeAll = false;
                    TRainerEditionQQGroup32Helper.SetDamageTouches(value, isFreezeDamageTouches, false);
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
                    TRainerEditionQQGroup32Helper.PlayClick();
                }
                // 鼠标左键按住 + 鼠标在 Slider 上
                bool isFromUser = Mouse.LeftButton == MouseButtonState.Pressed && ((Slider)sender).IsMouseOver;
                if (isFromUser) {
                    float value = (float)((Slider)sender).Value;
                    isFreezeFastRun = value > 0;
                    if (isFreezeFastRun) isUnfreezeAll = false;
                    TRainerEditionQQGroup32Helper.SetFastRun(value, isFreezeFastRun, false);
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
            double highJumpOld = -1, fastRunOld = -1;
            double highJumpStep = this.Slider_High_Jump.TickFrequency, fastRunStep = this.Slider_Fast_Run.TickFrequency;
            _dispatcherTimer.Tick += delegate(object sender, EventArgs args) {
                _isProcOpen = MemoryDllUtils.OpenProcess(TRainerEditionQQGroup32Helper.ProcessName);
                if (_isProcOpen) {
                    if (!_isTRainerOpen) return;

                    //z轴 ZAxis
                    float zAxis = MemoryDllUtils.ReadFloat(TRainerEditionQQGroup32Helper.ZAxis);
                    this.TB_ZAxis.Text = ((int) zAxis).ToString();

                    //jj性感度
                    this.Slider_Balls_Size.Value = TRainerEditionQQGroup32Helper.GetBallsSize();
                    //攻击力
                    this.Slider_FightBuff.Value = TRainerEditionQQGroup32Helper.GetClothing_Health();
                    //跳高
                    this.Slider_High_Jump.Value = TRainerEditionQQGroup32Helper.GetHighJump();
                    //护盾
                    this.Slider_Shield.Value = TRainerEditionQQGroup32Helper.GetShield();
                    //隐身
                    this.Slider_Invisible.Value = TRainerEditionQQGroup32Helper.GetInvisible();
                    //撞飞
                    this.Slider_Damage_Touches.Value = TRainerEditionQQGroup32Helper.GetDamageTouches();
                    //快跑🏃‍♀️
                    this.Slider_Fast_Run.Value = TRainerEditionQQGroup32Helper.GetFastRun();
                } else {
                    // Console.WriteLine($"openProcessSuccess: {_isProcOpen}");
                    UnfreezeAll();
                }

                this.Border_Running.Visibility = _isProcOpen ? Visibility.Visible : Visibility.Collapsed;
                this.Border_Stopped.Visibility = _isProcOpen ? Visibility.Collapsed : Visibility.Visible;
                
                //设置传送Grid的显示/隐藏
                int mapPosition = TRainerEditionQQGroup32Helper.GetMapPosition();
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
                //Edition重置版, 多1个隐藏传送点
                SetGridElementVisibility(this.Grid_Teleport, 8, 6, mapPosition);
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
                TRainerEditionQQGroup32Helper.MoneyAdd();
            };
            Action actionBeer = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionQQGroup32Helper.BeerAdd();
            };
            Action actionWhiskey = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionQQGroup32Helper.WhiskeyAdd();
            };
            Action actionNug = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionQQGroup32Helper.NugAdd();
            };
            Action actionShroom = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionQQGroup32Helper.ShroomAdd();
            };
            Action actionPeyote = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionQQGroup32Helper.PeyoteAdd();
            };
            Action actionFrog = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionQQGroup32Helper.FrogAdd();
            };
            Action actionCrack = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionQQGroup32Helper.CrackAdd();
            };
            Action actionHeightAdd = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionQQGroup32Helper.ZAxisEdit(true, value);
            };
            Action actionHeightMinus = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionQQGroup32Helper.ZAxisEdit(false, value);
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
                TRainerEditionQQGroup32Helper.GoRightOrLeft(true, value);
                return;
            }
            if (e.KeyCode == Keys.Left) {   //西
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionQQGroup32Helper.GoRightOrLeft(false, value);
                return;
            }
            if (e.KeyCode == Keys.Up) {     //北
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionQQGroup32Helper.GoFrontOrBack(true, value);
                return;
            }
            if (e.KeyCode == Keys.Down) {   //南
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionQQGroup32Helper.GoFrontOrBack(false, value);
                return;
            }
        }
        

        private void Btn_OnClick(object sender, RoutedEventArgs e) {
            
            TRainerEditionQQGroup32Helper.PlayClick();

            if (!(sender is FrameworkElement fe)) return;
            string name = fe.Name;
            
            //播放Ang
            if (name == this.Image_Play_Ang.Name) {
                TRainerEditionQQGroup32Helper.IsPlayAng = !TRainerEditionQQGroup32Helper.IsPlayAng;
                Uri uri = TRainerEditionQQGroup32Helper.GetSwitchUri(TRainerEditionQQGroup32Helper.IsPlayAng);
                this.Image_Play_Ang.Source = new BitmapImage(uri);
                if (TRainerEditionQQGroup32Helper.IsPlayAng) {
                    TRainerEditionQQGroup32Helper.PlayAng();
                }
                return;
            }
            //激活修改器(TRainer activate)
            if (name == this.Image_TRainer_State.Name) {
                _isTRainerOpen = !_isTRainerOpen;
                Uri uri = TRainerEditionQQGroup32Helper.GetSwitchUri(_isTRainerOpen);
                this.Image_TRainer_State.Source = new BitmapImage(uri);
                TRainerEditionQQGroup32Helper.PlayActivate(_isTRainerOpen);
                if (!_isTRainerOpen) {
                    UnfreezeAll();
                }
                return;
            }
            //关于
            if (name == this.Btn_About.Name) {
                MessageBox.Show(TRainerEditionQQGroup32Helper.StrAbout, "说明(explain):");
                return;
            }

            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;
            
            //钱💰
            if (name == this.Btn_Money.Name) {
                TRainerEditionQQGroup32Helper.MoneyAdd();
                return;
            }
            
            //啤酒🍺
            if (name == this.Btn_Beer.Name) {
                TRainerEditionQQGroup32Helper.BeerAdd();
                return;
            }
            //威士忌🤳
            if (name == this.Btn_Whiskey.Name) {
                TRainerEditionQQGroup32Helper.WhiskeyAdd();
                return;
            }
            //小块大麻Nug🍃
            if (name == this.Btn_Nug.Name) {
                TRainerEditionQQGroup32Helper.NugAdd();
                return;
            }
            //迷幻蘑菇🍄Shroom
            if (name == this.Btn_Shroom.Name) {
                TRainerEditionQQGroup32Helper.ShroomAdd();
                return;
            }
            //乌羽玉仙人掌的干燥茎块(Peyote Button)🌵
            if (name == this.Btn_Peyote.Name) {
                TRainerEditionQQGroup32Helper.PeyoteAdd();
                return;
            }
            //蟾蜍Toad🐸
            if (name == this.Btn_Frog.Name) {
                TRainerEditionQQGroup32Helper.FrogAdd();
                return;
            }
            //可卡因块(Rock)
            if (name == this.Btn_Crack.Name) {
                TRainerEditionQQGroup32Helper.CrackAdd();
                return;
            }
            
            //东
            if (name == this.Btn_XAxis_Plus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionQQGroup32Helper.GoRightOrLeft(true, value);
                return;
            }
            //西
            if (name == this.Btn_XAxis_Minus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionQQGroup32Helper.GoRightOrLeft(false, value);
                return;
            }
            
            //北
            if (name == this.Btn_YAxis_Plus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionQQGroup32Helper.GoFrontOrBack(true, value);
                return;
            }
            //南
            if (name == this.Btn_YAxis_Minus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionQQGroup32Helper.GoFrontOrBack(false, value);
                return;
            }
            
            //高度+
            if (name == this.Btn_ZAxis_Plus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionQQGroup32Helper.ZAxisEdit(true, value);
                return;
            }
            //高度-
            if (name == this.Btn_ZAxis_Minus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionQQGroup32Helper.ZAxisEdit(false, value);
                return;
            }
            //无限健康
            if (name == this.Image_Infinite_Health.Name) {
                isFreezeHealth = !isFreezeHealth;
                Uri uri = TRainerEditionQQGroup32Helper.GetSwitchUri(isFreezeHealth);
                this.Image_Infinite_Health.Source = new BitmapImage(uri);
                TRainerEditionQQGroup32Helper.FreezeHealth(isFreezeHealth);
                if (isFreezeHealth) {
                    isUnfreezeAll = false;
                }
                return;
            }
            //冻结快感进度
            if (name == this.Image_Freeze_Climax.Name) {
                isFreezeClimax = !isFreezeClimax;
                Uri uri = TRainerEditionQQGroup32Helper.GetSwitchUri(isFreezeClimax);
                this.Image_Freeze_Climax.Source = new BitmapImage(uri);
                TRainerEditionQQGroup32Helper.FreezeClimax(isFreezeClimax);
                if (isFreezeClimax) {
                    isUnfreezeAll = false;
                }
                return;
            }
            //潜水
            if (name == this.Image_Diving.Name) {
                isDiving = !isDiving;
                Uri uri = TRainerEditionQQGroup32Helper.GetSwitchUri(isDiving);
                this.Image_Diving.Source = new BitmapImage(uri);
                TRainerEditionQQGroup32Helper.FreezeDiving(isDiving);
                if (isDiving) {
                    isUnfreezeAll = false;
                }
                return;
            }
        }


        private void OnTeleportClick(object sender, RoutedEventArgs e) {

            TRainerEditionQQGroup32Helper.PlayClick();

            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;

            if (!(sender is FrameworkElement fe)) return;
            string name = fe.Name;

            //Map1(Missionary Beach 传教士海滩)→Map2(Firm Wood Forest 阔叶林)
            if (name == this.TB_MissionaryBeach2FirmWoodForest.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map1_to_Map2, "Map1(Missionary Beach 传教士海滩)→Map2(Firm Wood Forest 阔叶林) 传送点失败!");
                return;
            }
            //Map1(Missionary Beach 传教士海滩)→Map4(Gabacho Heights 加巴乔高地)
            if (name == this.TB_MissionaryBeach2GabachoHeights.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map1_to_Map4, "Map1(Missionary Beach 传教士海滩)→Map4(Gabacho Heights 加巴乔高地) 传送点失败!");
                return;
            }
            
            //Map2(Firm Wood Forest 阔叶林)→Map1(Missionary Beach 传教士海滩)
            if (name == this.TB_FirmWoodForest2MissionaryBeach.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map2_to_Map1, "Map2(Firm Wood Forest 阔叶林)→Map1(Missionary Beach 传教士海滩) 传送点失败!");
                return;
            }
            //Map2(Firm Wood Forest 阔叶林)→Map3(Homeland Trailer Park 国土安全拖车公园)
            if (name == this.TB_FirmWoodForest2HomelandTrailerPark.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map2_to_Map3, "Map2(Firm Wood Forest 阔叶林)→Map3(Homeland Trailer Park 国土安全拖车公园) 传送点失败!");
                return;
            }
            
            //Map3(Homeland Trailer Park 国土安全拖车公园)→Map2(Firm Wood Forest 阔叶林)
            if (name == this.TB_HomelandTrailerPark2FirmWoodForest.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map3_to_Map2, "Map3(Homeland Trailer Park 国土安全拖车公园)→Map2(Firm Wood Forest 阔叶林) 传送点失败!");
                return;
            }

            //Map4(Gabacho Heights 加巴乔高地)→Map1(Missionary Beach 传教士海滩)
            if (name == this.TB_GabachoHeights2MissionaryBeach.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map4_to_Map1, "Map4(Gabacho Heights 加巴乔高地)→Map1(Missionary Beach 传教士海滩) 传送点失败!");
                return;
            }
            //Map4(Gabacho Heights 加巴乔高地)→Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)
            if (name == this.TB_GabachoHeights2HavajoIndianReservation.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map4_to_Map5, "Map4(Gabacho Heights 加巴乔高地)→Map5(Havajo Indian Reservation 哈瓦那印第安人保留地) 传送点失败!");
                return;
            }
            //Map4(Gabacho Heights 加巴乔高地)→Map6(Nobbing Hill 诺丁山)
            if (name == this.TB_GabachoHeights2NobbingHill.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map4_to_Map6, "Map4(Gabacho Heights 加巴乔高地)→Map6(Nobbing Hill 诺丁山) 传送点失败!");
                return;
            }
            
            
            //Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)→Map4(Gabacho Heights 加巴乔高地)
            if (name == this.TB_HavajoIndianReservation2GabachoHeights.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map5_to_Map4, "Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)→Map4(Gabacho Heights 加巴乔高地) 传送点失败!");
                return;
            }
            
            
            //Map6(Nobbing Hill 诺丁山)→Map8(DownTown 市中心)
            if (name == this.TB_NobbingHill2DownTown.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map6_to_Map8, "Map6(Nobbing Hill 诺丁山)→Map8(DownTown 市中心) 传送点失败!");
                return;
            }
            //Map6(Nobbing Hill 诺丁山)→Map4(Gabacho Heights 加巴乔高地)
            if (name == this.TB_NobbingHill2GabachoHeights.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map6_to_Map4, "Map6(Nobbing Hill 诺丁山)→Map4(Gabacho Heights 加巴乔高地) 传送点失败!");
                return;
            }
            //Map6(Nobbing Hill 诺丁山)→Map7(Mushroom Marsh 蘑菇沼泽)
            if (name == this.TB_NobbingHill2MushroomMarsh.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map6_to_Map7, "Map6(Nobbing Hill 诺丁山)→Map7(Mushroom Marsh 蘑菇沼泽) 传送点失败!");
                return;
            }
            
            
            //Map7(Mushroom Marsh 蘑菇沼泽) 撒旦(Satan)
            if (name == this.TB_MushroomMarsh_Satan.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map7_to_Satan, "瞬移到 Map7(Mushroom Marsh 蘑菇沼泽) 撒旦(Satan) 失败!");
                return;
            }
            //Map7(Mushroom Marsh 蘑菇沼泽) 撒旦的老婆(Satan's wife)
            if (name == this.TB_MushroomMarsh_Satan_wife.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map7_to_SatanWife, "瞬移到 Map7(Mushroom Marsh 蘑菇沼泽) 撒旦的老婆(Satan's wife) 失败!");
                return;
            }
            //Map7(Mushroom Marsh 蘑菇沼泽)→Map6(Nobbing Hill 诺丁山)
            if (name == this.TB_MushroomMarsh2NobbingHill.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map7_to_Map6, "Map7(Mushroom Marsh 蘑菇沼泽)→Map6(Nobbing Hill 诺丁山) 失败!");
                return;
            }
            
            
            //Map8(DownTown 市中心) RonJ大富翁
            if (name == this.TB_Downtown_RonJTowers.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map8_to_RonJEntrance, "瞬移到 Map8(DownTown 市中心) RonJ大富翁 失败!");
                return;
            }
            //Map8(DownTown 市中心) 天使
            if (name == this.TB_Downtown_Angle.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map8_to_Angle, "瞬移到 Map8(DownTown 市中心) 天使 失败!");
                return;
            }
            //Map8(DownTown 市中心)→Map9(Man Island 曼岛) 传送点
            if (name == this.TB_DownTown2ManIsland.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map8_to_Map9, "Map8(DownTown 市中心)→Map9(Man Island 曼岛) 传送点失败!");
                return;
            }
            //Map8(DownTown 市中心)→Map6(Nobbing Hill 诺丁山) 传送点
            if (name == this.TB_DownTown2NobbingHill.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map8_to_Map6, "Map8(DownTown 市中心)→Map6(Nobbing Hill 诺丁山) 传送点失败!");
                return;
            }
            //Map8(DownTown 市中心)→Map3(Homeland Trailer Park 国土安全拖车公园) 传送点
            if (name == this.TB_DownTown2HomelandTrailerPark.Name) {
                if (!isDiving) {
                    MessageBoxResult result = MessageBoxUtils.NewMessageBox(
                            "隐藏点在水下, 请先打开'潜水'功能!\nThe hidden spot is underwater, Pls turn on the 'Diving' function first!"
                        ).SetCaption("提示Tips")
                        .SetButton(MessageBoxButton.OK)
                        .SetButtonOk("去设置\n(Go2Set)")
                        .SetIcon(MessageBoxImage.Warning)
                        .SetDefaultResult(MessageBoxResult.Yes)
                        .Show();
                    return;
                }

                string failureStr = "Map8(DownTown 市中心)→Map3(Homeland Trailer Park 国土安全拖车公园) 传送点失败!";
                bool isSuccess0 = TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.CoordinateDownTown2HomelandTrailerPark, failureStr);
                bool isSuccess1 = TRainerEditionQQGroup32Helper.SetDegreeMouseUpDown(1.470796347f);
                bool isSuccess2 = TRainerEditionQQGroup32Helper.SetDegreeMouseLeftRight(0f);
                bool isSuccess3 = TRainerEditionQQGroup32Helper.SetDegreePersonFront(0f);
                if (isSuccess0 && isSuccess1 && isSuccess2 && isSuccess3) {
                } else Console.WriteLine($"{failureStr}: isSuccess0 = {isSuccess0}, isSuccess1 = {isSuccess1}, isSuccess2 = {isSuccess2}, isSuccess3 = {isSuccess3}");
                return;
            }
            
            
            //Map9(Man Island 曼岛)→高塔入口(Man Needle)
            if (name == this.TB_ManIsland2ManNeedle.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map9_to_ManNeedle, "Map9(Man Island 曼岛)→高塔入口(Man Needle) 传送点失败!");
                return;
            }
            //Map9(Man Island 曼岛)→Map8(DownTown 市中心)
            if (name == this.TB_ManIsland2DownTown.Name) {
                TRainerEditionQQGroup32Helper.Teleport(TRainerEditionQQGroup32Helper.Map9_to_Map8, "Map9(Man Island 曼岛)→Map8(DownTown 市中心) 传送点失败!");
                return;
            }
        }

        private void Light_OnClick(object sender, RoutedEventArgs e) {
            TRainerEditionQQGroup32Helper.PlayClick();
            
            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;

            if (sender is Image image) {
                string nameImage = image.Name;
                if (nameImage == this.Image_Pause_Daylight.Name) {
                    _isPauseDaylight = !_isPauseDaylight;
                    Uri uri = TRainerEditionQQGroup32Helper.GetSwitchUri(_isPauseDaylight);
                    this.Image_Pause_Daylight.Source = new BitmapImage(uri);
                    TRainerEditionQQGroup32Helper.PauseDaylight(_isPauseDaylight, true, true);
                    isUnfreezeAll = false;
                    return;
                }
                if (nameImage == this.Image_Lamp_State.Name) {
                    _isLampOpen = !_isLampOpen;
                    Uri uri = TRainerEditionQQGroup32Helper.GetSwitchUri(_isLampOpen);
                    this.Image_Lamp_State.Source = new BitmapImage(uri);
                    TRainerEditionQQGroup32Helper.LampLightSet(_isLampOpen, true);
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
                TRainerEditionQQGroup32Helper.BrightnessSet(0, true);
                return;
            }
            //白昼(Daytime)
            if (name == this.RB_Brightness_Daytime.Name) {
                TRainerEditionQQGroup32Helper.BrightnessSet(1, true);
                return;
            }
            //傍晚(Evening)
            if (name == this.RB_Brightness_Evening.Name) {
                TRainerEditionQQGroup32Helper.BrightnessSet(2, true);
                return;
            }
            //黄昏(Dusk)
            if (name == this.RB_Brightness_Dusk.Name) {
                TRainerEditionQQGroup32Helper.BrightnessSet(3, true);
                return;
            }
            //午夜(Midnight)
            if (name == this.RB_Brightness_Midnight.Name) {
                TRainerEditionQQGroup32Helper.BrightnessSet(4, true);
                return;
            }
            //拂晓(Dawn)
            if (name == this.RB_Brightness_Dawn.Name) {
                TRainerEditionQQGroup32Helper.BrightnessSet(5, true);
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
            TRainerEditionQQGroup32Helper.FreezeHealth(false);
            TRainerEditionQQGroup32Helper.SetJumpHigher(0, false, false);
            TRainerEditionQQGroup32Helper.SetShield(0, false, false);
            TRainerEditionQQGroup32Helper.SetInvisible(0, false, false);
            TRainerEditionQQGroup32Helper.SetDamageTouches(0, false, false);
            TRainerEditionQQGroup32Helper.SetFastRun(0, false, false);
            TRainerEditionQQGroup32Helper.FreezeClimax(false);
            TRainerEditionQQGroup32Helper.PauseDaylight(false, false, false);
            TRainerEditionQQGroup32Helper.FreezeDiving(false);
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