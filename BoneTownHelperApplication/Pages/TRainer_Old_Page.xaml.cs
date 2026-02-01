using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        //灯光是否打开
        private bool _isLampOpen = true;
        //是否冻结无限健康
        private bool isFreezeHealth = false;
        //冻结跳高效果
        private bool isFreezeHighJump = false;
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
                _isProcOpen = MemoryDllUtils.OpenProcess(TRainerHelper.ProcessName);
                if (_isProcOpen) {
                    if (!_isTRainerOpen) return;

                    //z轴 ZAxis
                    float zAxis = MemoryDllUtils.ReadFloat(TRainerHelper.ZAxis);
                    this.TB_ZAxis.Text = ((int) zAxis).ToString();

                    //if先开游戏🎮并已修改, 再重新打开修改器
                    if (highJumpOld < 0 && fastRunOld < 0) {
                        highJumpOld = TRainerHelper.GetHighJump();
                        fastRunOld = TRainerHelper.GetFastRun();
                        this.Slider_High_Jump.Value = highJumpOld;
                        this.Slider_Fast_Run.Value = fastRunOld;
                        return;
                    }
                    /**
                     * 为什么要在这儿获取值, 而不是在Slider的ValueChanged 事件中获取值呢? 因为Slider在拖动时更新值太快, 但又没有直接办法判断Slider是否正在拖动:
                     * 鼠标🖱️/触摸是否已松开, 键盘⌨️是否结束🔚按下←→
                     * IsMouseCaptureWithin: 鼠标是否在拖动
                     * IsTouchCaptured: 是否触摸调整中
                     * IsKeyboardFocused: 键盘操作「捕获状态」，只能通过「失去焦点」或「延迟」判断, 获取焦点后按←→能调整进度
                     */
                    //跳高效果
                    double highJump = this.Slider_High_Jump.Value;
                    bool isHighJumpChanged = Math.Abs(highJump - highJumpOld) > highJumpStep;
                    if (isHighJumpChanged) {
                        TRainerHelper.PlayClick();
                        highJumpOld = highJump;
                    }
                    if (false) {
                        //防止换地图后冻结失效, 所以不管值有没有变化, 都调用
                        isFreezeHighJump = highJump > 0;
                        if (isFreezeHighJump) {
                            isUnfreezeAll = false;
                        }
                        TRainerHelper.SetHighJump((float) highJump, isFreezeHighJump, isHighJumpChanged);
                    } else {
                        //反正都是死循环, 就不用冻结了
                        TRainerHelper.SetHighJump((float) highJump, false, isHighJumpChanged);
                    }

                    //快跑效果
                    double fastRun = this.Slider_Fast_Run.Value;
                    bool isFastRunChanged = Math.Abs(fastRun - fastRunOld) > fastRunStep;
                    if (isFastRunChanged) {
                        TRainerHelper.PlayClick();
                        fastRunOld = fastRun;
                    }
                    if (false) {
                        //防止换地图后冻结失效, 所以不管值有没有变化, 都调用
                        isFreezeFastRun = fastRun > 0;
                        if (isFreezeFastRun) {
                            isUnfreezeAll = false;
                        }
                        TRainerHelper.SetFastRun((float) fastRun, isFreezeFastRun, isFastRunChanged);
                    } else {
                        //反正都是死循环, 就不用冻结了
                        TRainerHelper.SetFastRun((float) fastRun, false, isFastRunChanged);
                    }
                } else {
                    Console.WriteLine($"openProcessSuccess: {_isProcOpen}");
                    highJumpOld = fastRunOld = 0;
                    //2个Slider归0, 否则重开游戏🎮的时候会判断并设置值
                    this.Slider_High_Jump.Value = 0.0;
                    this.Slider_Fast_Run.Value = 0.0;
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
            //问号❓️❔️
            if (name == this.Image_Question_Mark.Name) {
                MessageBox.Show(TRainerHelper.StrBrightness, "亮度修改说明(Brightness explain):");
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
            //jj性感度加到最大
            if (name == this.Btn_Balls_Max.Name) {
                TRainerHelper.BallsAdd210();
                return;
            }
            //无限健康
            if (name == this.Image_Infinite_Health.Name) {
                isFreezeHealth = !isFreezeHealth;
                Uri uri = TRainerHelper.GetSwitchUri(isFreezeHealth);
                this.Image_Infinite_Health.Source = new BitmapImage(uri);
                TRainerHelper.FreezeHealth(isFreezeHealth);
                if (isFreezeHealth) {
                    isUnfreezeAll = false;
                }
                return;
            }
            //冻结快感进度
            if (name == this.Image_Freeze_Climax.Name) {
                isFreezeClimax = !isFreezeClimax;
                Uri uri = TRainerHelper.GetSwitchUri(isFreezeClimax);
                this.Image_Freeze_Climax.Source = new BitmapImage(uri);
                TRainerHelper.FreezeClimax(isFreezeClimax);
                if (isFreezeClimax) {
                    isUnfreezeAll = false;
                }
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
                TRainerHelper.Teleport(TRainerHelper.CoordinateMissionaryBeach2FirmWoodForest, "Map1(Missionary Beach 传教士海滩)→Map2(Firm Wood Forest 阔叶林) 传送点失败!");
                return;
            }
            //Map1(Missionary Beach 传教士海滩)→Map4(Gabacho Heights 加巴乔高地)
            if (name == this.TB_MissionaryBeach2GabachoHeights.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateMissionaryBeach2GabachoHeights, "Map1(Missionary Beach 传教士海滩)→Map4(Gabacho Heights 加巴乔高地) 传送点失败!");
                return;
            }
            
            //Map2(Firm Wood Forest 阔叶林)→Map1(Missionary Beach 传教士海滩)
            if (name == this.TB_FirmWoodForest2MissionaryBeach.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateFirmWoodForest2MissionaryBeach, "Map2(Firm Wood Forest 阔叶林)→Map1(Missionary Beach 传教士海滩) 传送点失败!");
                return;
            }
            //Map2(Firm Wood Forest 阔叶林)→Map3(Homeland Trailer Park 国土安全拖车公园)
            if (name == this.TB_FirmWoodForest2HomelandTrailerPark.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateFirmWoodForest2HomelandTrailerPark, "Map2(Firm Wood Forest 阔叶林)→Map3(Homeland Trailer Park 国土安全拖车公园) 传送点失败!");
                return;
            }
            
            //Map3(Homeland Trailer Park 国土安全拖车公园)→Map2(Firm Wood Forest 阔叶林)
            if (name == this.TB_HomelandTrailerPark2FirmWoodForest.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateHomelandTrailerPark2FirmWoodForest, "Map3(Homeland Trailer Park 国土安全拖车公园)→Map2(Firm Wood Forest 阔叶林) 传送点失败!");
                return;
            }

            //Map4(Gabacho Heights 加巴乔高地)→Map1(Missionary Beach 传教士海滩)
            if (name == this.TB_GabachoHeights2MissionaryBeach.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateGabachoHeights2MissionaryBeach, "Map4(Gabacho Heights 加巴乔高地)→Map1(Missionary Beach 传教士海滩) 传送点失败!");
                return;
            }
            //Map4(Gabacho Heights 加巴乔高地)→Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)
            if (name == this.TB_GabachoHeights2HavajoIndianReservation.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateGabachoHeights2HavajoIndianReservation, "Map4(Gabacho Heights 加巴乔高地)→Map5(Havajo Indian Reservation 哈瓦那印第安人保留地) 传送点失败!");
                return;
            }
            //Map4(Gabacho Heights 加巴乔高地)→Map6(Nobbing Hill 诺丁山)
            if (name == this.TB_GabachoHeights2NobbingHill.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateGabachoHeights2NobbingHill, "Map4(Gabacho Heights 加巴乔高地)→Map6(Nobbing Hill 诺丁山) 传送点失败!");
                return;
            }
            
            
            //Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)→Map4(Gabacho Heights 加巴乔高地)
            if (name == this.TB_HavajoIndianReservation2GabachoHeights.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateHavajoIndianReservation2GabachoHeights, "Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)→Map4(Gabacho Heights 加巴乔高地) 传送点失败!");
                return;
            }
            
            
            //Map6(Nobbing Hill 诺丁山)→Map8(DownTown 市中心)
            if (name == this.TB_NobbingHill2DownTown.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateNobbingHill2DownTown, "Map6(Nobbing Hill 诺丁山)→Map8(DownTown 市中心) 传送点失败!");
                return;
            }
            //Map6(Nobbing Hill 诺丁山)→Map4(Gabacho Heights 加巴乔高地)
            if (name == this.TB_NobbingHill2GabachoHeights.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateNobbingHill2GabachoHeights, "Map6(Nobbing Hill 诺丁山)→Map4(Gabacho Heights 加巴乔高地) 传送点失败!");
                return;
            }
            //Map6(Nobbing Hill 诺丁山)→Map7(Mushroom Marsh 蘑菇沼泽)
            if (name == this.TB_NobbingHill2MushroomMarsh.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateNobbingHill2MushroomMarsh, "Map6(Nobbing Hill 诺丁山)→Map7(Mushroom Marsh 蘑菇沼泽) 传送点失败!");
                return;
            }
            
            
            //Map7(Mushroom Marsh 蘑菇沼泽) 撒旦(Satan)
            if (name == this.TB_MushroomMarsh_Satan.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateMushroomMarsh_Satan, "瞬移到 Map7(Mushroom Marsh 蘑菇沼泽) 撒旦(Satan) 失败!");
                return;
            }
            //Map7(Mushroom Marsh 蘑菇沼泽) 撒旦的老婆(Satan's wife)
            if (name == this.TB_MushroomMarsh_Satan_wife.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateMushroomMarsh_SatanWife, "瞬移到 Map7(Mushroom Marsh 蘑菇沼泽) 撒旦的老婆(Satan's wife) 失败!");
                return;
            }
            //Map7(Mushroom Marsh 蘑菇沼泽)→Map6(Nobbing Hill 诺丁山)
            if (name == this.TB_MushroomMarsh2NobbingHill.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateMushroomMarsh2NobbingHill, "Map7(Mushroom Marsh 蘑菇沼泽)→Map6(Nobbing Hill 诺丁山) 失败!");
                return;
            }
            
            
            //Map8(DownTown 市中心) RonJ大富翁
            if (name == this.TB_Downtown_RonJTowers.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateDowntown_RonJEntrance, "瞬移到 Map8(DownTown 市中心) RonJ大富翁 失败!");
                return;
            }
            //Map8(DownTown 市中心) 天使
            if (name == this.TB_Downtown_Angle.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateDowntown_Angle, "瞬移到 Map8(DownTown 市中心) 天使 失败!");
                return;
            }
            //Map8(DownTown 市中心)→Map9(Man Island 曼岛) 传送点
            if (name == this.TB_DownTown2ManIsland.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateDownTown2ManIsland, "Map8(DownTown 市中心)→Map9(Man Island 曼岛) 传送点失败!");
                return;
            }
            //Map8(DownTown 市中心)→Map6(Nobbing Hill 诺丁山) 传送点
            if (name == this.TB_DownTown2NobbingHill.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateDownTown2NobbingHill, "Map8(DownTown 市中心)→Map6(Nobbing Hill 诺丁山) 传送点失败!");
                return;
            }
            
            
            //Map9(Man Island 曼岛)→高塔入口(Man Needle)
            if (name == this.TB_ManIsland2ManNeedle.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateManIsland_ManNeedle, "Map9(Man Island 曼岛)→高塔入口(Man Needle) 传送点失败!");
                return;
            }
            //Map9(Man Island 曼岛)→Map8(DownTown 市中心)
            if (name == this.TB_ManIsland2DownTown.Name) {
                TRainerHelper.Teleport(TRainerHelper.CoordinateManIsland2DownTown, "Map9(Man Island 曼岛)→Map8(DownTown 市中心) 传送点失败!");
                return;
            }
        }

        private void Light_OnClick(object sender, RoutedEventArgs e) {
            TRainerHelper.PlayClick();
            
            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;

            if (sender is Image image) {
                //先停止掉以前的线程, 否则以前的线程也一直在修改值, 就会n多个线程在改值, 快速闪动!!!
                isStartEditLamp = false;
                string nameImage = image.Name;
                if (nameImage == this.Image_Lamp_State.Name) {
                    _isLampOpen = !_isLampOpen;
                    Uri uri = TRainerHelper.GetSwitchUri(_isLampOpen);
                    this.Image_Lamp_State.Source = new BitmapImage(uri);

                    if (false) {
                        //CPU占用非常高, 50%左右, 线程没有写对???
                        isStartEditLamp = true;
                        StartEditLamp();
                    } else {
                        TRainerHelper.LampLightSet(_isLampOpen, true, true);
                    }
                    isUnfreezeAll = false;
                }
                return;
            }
            
            if (!(sender is System.Windows.Controls.Control control)) return;
            //先停止掉以前的线程, 否则以前的线程也一直在修改值, 就会n多个线程在改值, 快速闪动!!!
            isStartEditBrightness = false;
            
            string name = control.Name;

            //环境亮度设置
            if (name == this.RB_Brightness_Night.Name) {
                if (false) {
                    editBrightnessPosition = 0;
                    isStartEditBrightness = true;
                    StartEditBrightness();
                } else {
                    TRainerHelper.BrightnessSet(0, false, true);
                }
                // isUnFreezeAll = false;
                return;
            }
            if (name == this.RB_Brightness_Evening.Name) {
                if (false) {
                    editBrightnessPosition = 1;
                    isStartEditBrightness = true;
                    StartEditBrightness();
                } else {
                    TRainerHelper.BrightnessSet(1, false, true);
                }
                // isUnFreezeAll = false;
                return;
            }
            if (name == this.RB_Brightness_Noon.Name) {
                if (false) {
                    editBrightnessPosition = 2;
                    isStartEditBrightness = true;
                    StartEditBrightness();
                } else {
                    TRainerHelper.BrightnessSet(2, false, true);
                }
                // isUnFreezeAll = false;
                return;
            }
        }

        private bool isStartEditLamp = false, isStartEditBrightness = false;
        private int editBrightnessPosition = 0;
        private void StartEditLamp() {
            Task.Factory.StartNew((Action)(() => {
                while (isStartEditLamp) {
                    if (!_isProcOpen) return;
                    if (!_isTRainerOpen) return;
                    //1~2ms
                    TRainerHelper.LampLightSet(_isLampOpen, false, false);
                    // Thread.Sleep(1);    //1~10都会闪动
                }
            }));
            TRainerHelper.PlayAng();
        }
        //都会闪动
        private void StartEditBrightness() {
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         //1~8ms                                 参2=false, 也会一直闪动
            //         TRainerHelper.BrightnessSet(editBrightnessPosition, false);
            //         // TRainerHelper.BrightnessSet2(editBrightnessPosition);
            //         Thread.Sleep(10);
            //     }
            // }));
            // return;
            
            
            // UIntPtr code1 = MemoryDllUtils.Memory.GetCode(TRainerHelper.Brightness_Ground_Green2, "");
            // if (code1 == UIntPtr.Zero || code1.ToUInt64() < 65536UL /*0x010000*/)
            //     return /*false*/;
            // UIntPtr code2 = MemoryDllUtils.Memory.GetCode(TRainerHelper.Brightness_Ground_Purper2, "");
            // if (code2 == UIntPtr.Zero || code2.ToUInt64() < 65536UL /*0x010000*/)
            //     return /*false*/;
            // UIntPtr code3 = MemoryDllUtils.Memory.GetCode(TRainerHelper.Brightness_Ground_Yellow2, "");
            // if (code3 == UIntPtr.Zero || code3.ToUInt64() < 65536UL /*0x010000*/)
            //     return /*false*/;
            // UIntPtr code4 = MemoryDllUtils.Memory.GetCode(TRainerHelper.Brightness_Ground_Green, "");
            // if (code4 == UIntPtr.Zero || code4.ToUInt64() < 65536UL /*0x010000*/)
            //     return /*false*/;
            // UIntPtr code5 = MemoryDllUtils.Memory.GetCode(TRainerHelper.Brightness_Ground_Purper, "");
            // if (code5 == UIntPtr.Zero || code5.ToUInt64() < 65536UL /*0x010000*/)
            //     return /*false*/;
            // UIntPtr code6 = MemoryDllUtils.Memory.GetCode(TRainerHelper.Brightness_Ground_Yellow, "");
            // if (code6 == UIntPtr.Zero || code6.ToUInt64() < 65536UL /*0x010000*/)
            //     return /*false*/;

            //也是一直闪动
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer = BitConverter.GetBytes(TRainerHelper.Ground_Green2[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code1, lpBuffer, (UIntPtr) 8, IntPtr.Zero);
            //
            //         byte[] lpBuffer2 = BitConverter.GetBytes(TRainerHelper.Ground_Purper2[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code2, lpBuffer2, (UIntPtr) 8, IntPtr.Zero);
            //
            //         byte[] lpBuffer3 = BitConverter.GetBytes(TRainerHelper.Ground_Yellow2[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code3, lpBuffer3, (UIntPtr) 8, IntPtr.Zero);
            //
            //         byte[] lpBuffer4 = BitConverter.GetBytes(TRainerHelper.Ground_Green[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code4, lpBuffer4, (UIntPtr) 8, IntPtr.Zero);
            //
            //         byte[] lpBuffer5 = BitConverter.GetBytes(TRainerHelper.Ground_Purper[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code5, lpBuffer5, (UIntPtr) 8, IntPtr.Zero);
            //
            //         byte[] lpBuffer6 = BitConverter.GetBytes(TRainerHelper.Ground_Yellow[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code6, lpBuffer6, (UIntPtr) 8, IntPtr.Zero);
            //         
            //         // Thread.Sleep(15);
            //     }
            // }));
            
            
            //下方分成6个线程执行, 也是一直闪动!
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer = BitConverter.GetBytes(TRainerHelper.Ground_Green2[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code1, lpBuffer, (UIntPtr) 8, IntPtr.Zero);
            //     }
            // }));
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer2 = BitConverter.GetBytes(TRainerHelper.Ground_Purper2[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code2, lpBuffer2, (UIntPtr) 8, IntPtr.Zero);
            //     }
            // }));
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer3 = BitConverter.GetBytes(TRainerHelper.Ground_Yellow2[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code3, lpBuffer3, (UIntPtr) 8, IntPtr.Zero);
            //     }
            // }));
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer4 = BitConverter.GetBytes(TRainerHelper.Ground_Green[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code4, lpBuffer4, (UIntPtr) 8, IntPtr.Zero);
            //     }
            // }));
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer5 = BitConverter.GetBytes(TRainerHelper.Ground_Purper[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code5, lpBuffer5, (UIntPtr) 8, IntPtr.Zero);
            //     }
            // }));
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer6 = BitConverter.GetBytes(TRainerHelper.Ground_Yellow[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code6, lpBuffer6, (UIntPtr) 8, IntPtr.Zero);
            //     }
            // }));
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
            TRainerHelper.SetHighJump(0, false, false);
            TRainerHelper.SetFastRun(0, false, false);
            TRainerHelper.FreezeClimax(false);
            TRainerHelper.LampLightSet(true, false, false);
            TRainerHelper.BrightnessSet(2, false, false);
            TRainerHelper.FreezeDiving(false);
            isUnfreezeAll = true;
        }
        
        // 页面卸载时执行 - 这是主要的方法
        private void MyPage_Unloaded(object sender, RoutedEventArgs e) {
            Console.WriteLine("页面卸载 - 在这里清理资源");
            
            isStartEditLamp = false;
            isStartEditBrightness = false;
            
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