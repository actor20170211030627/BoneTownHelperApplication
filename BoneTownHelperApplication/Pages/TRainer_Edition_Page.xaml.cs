using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class TRainer_Edition_Page : Page {
        
        //进程是否打开
        private static bool _isProcOpen = false;
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


        private DispatcherTimer _dispatcherTimer;

        private IKeyboardMouseEvents m_GlobalHook;

        public TRainer_Edition_Page() {
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
            MemoryDllUtils.BindToUI<int>(TRainerEditionHelper.Money, delegate(string s) {
                // Console.WriteLine($"钱Money: {s}");
                // 使用 Dispatcher 切换到 UI 线程
                Dispatcher.Invoke(() => {
                    this.TB_Money.Text = s;
                });
            });
            //啤酒Beer
            MemoryDllUtils.BindToUI<int>(TRainerEditionHelper.Beer, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Beer.Text = s;
                });
            });
            //威士忌Whiskey
            MemoryDllUtils.BindToUI<int>(TRainerEditionHelper.Whiskey, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Whiskey.Text = s;
                });
            });
            //大麻
            MemoryDllUtils.BindToUI<int>(TRainerEditionHelper.Weed, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Nug.Text = s;
                });
            });
            //迷幻蘑菇Shroom
            MemoryDllUtils.BindToUI<int>(TRainerEditionHelper.Shroom, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Shroom.Text = s;
                });
            });
            //佩奥特掌（仙人掌的一种）Peyote
            MemoryDllUtils.BindToUI<int>(TRainerEditionHelper.Peyote, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Peyote.Text = s;
                });
            });
            //青蛙Frog
            MemoryDllUtils.BindToUI<int>(TRainerEditionHelper.Frog, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Frog.Text = s;
                });
            });
            //强效可卡因Crack
            MemoryDllUtils.BindToUI<int>(TRainerEditionHelper.Crack, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Crack.Text = s;
                });
            });
            
            
            /*
             * 下方读取到的xyz值都为0...
             */
            //x轴 XAxis
            // MemoryDllUtils.BindToUI<float>(TRainerEditionHelper.XAxis, delegate(string s) {
            //     Console.WriteLine($"x = {s}");
            //     Dispatcher.Invoke(() => {
            //         this.TB_XAxis.Text = s;
            //     });
            // });
            // //y轴 YAxis
            // MemoryDllUtils.BindToUI<float>(TRainerEditionHelper.YAxis, delegate(string s) {
            //     Console.WriteLine($"y = {s}");
            //     Dispatcher.Invoke(() => {
            //         this.TB_YAxis.Text = s;
            //     });
            // });
            // //z轴 ZAxis
            // MemoryDllUtils.BindToUI<float>(TRainerEditionHelper.ZAxis, delegate(string s) {
            //     Console.WriteLine($"z = {s}");
            //     Dispatcher.Invoke(() => {
            //         this.TB_ZAxis.Text = s;
            //     });
            // });
            
            
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
            _dispatcherTimer.Tick += delegate(object sender, EventArgs args) {
                _isProcOpen = MemoryDllUtils.OpenProcess(TRainerEditionHelper.ProcessName);
                if (_isProcOpen) {
                    //TODO: false 先屏蔽掉显示坐标, true
                    if (false) {
                        //x轴 XAxis
                        float x = MemoryDllUtils.ReadFloat(TRainerEditionHelper.XAxis);
                        //InvariantCulture: 美国英语（en-US）但独立于特定国家或地区，主要用于处理货币、日期、时间等文化敏感数据时避免因地区差异导致格式错误。
                        this.TB_XAxis.Text = x.ToString(CultureInfo.InvariantCulture);
                        //y轴 YAxis
                        float y = MemoryDllUtils.ReadFloat(TRainerEditionHelper.YAxis);
                        this.TB_YAxis.Text = y.ToString(CultureInfo.InvariantCulture);
                        //z轴 ZAxis
                        float z = MemoryDllUtils.ReadFloat(TRainerEditionHelper.ZAxis);
                        this.TB_ZAxis.Text = z.ToString(CultureInfo.InvariantCulture);
                    }
                } else {
                    Console.WriteLine($"openProcessSuccess: {_isProcOpen}");
                }

                this.Border_Running.Visibility = _isProcOpen ? Visibility.Visible : Visibility.Collapsed;
                this.Border_Stopped.Visibility = _isProcOpen ? Visibility.Collapsed : Visibility.Visible;
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
                TRainerEditionHelper.MoneyAdd();
            };
            Action actionBeer = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionHelper.BeerAdd();
            };
            Action actionWhiskey = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionHelper.WhiskeyAdd();
            };
            Action actionNug = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionHelper.NugAdd();
            };
            Action actionShroom = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionHelper.ShroomAdd();
            };
            Action actionPeyote = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionHelper.PeyoteAdd();
            };
            Action actionFrog = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionHelper.FrogAdd();
            };
            Action actionCrack = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                TRainerEditionHelper.CrackAdd();
            };
            Action actionHeightAdd = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionHelper.ZAxisEdit(true, value);
            };
            Action actionHeightMinus = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionHelper.ZAxisEdit(false, value);
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
                TRainerEditionHelper.GoRightOrLeft(true, value);
                return;
            }
            if (e.KeyCode == Keys.Left) {   //西
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionHelper.GoRightOrLeft(false, value);
                return;
            }
            if (e.KeyCode == Keys.Up) {     //北
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionHelper.GoFrontOrBack(true, value);
                return;
            }
            if (e.KeyCode == Keys.Down) {   //南
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionHelper.GoFrontOrBack(false, value);
                return;
            }
        }
        

        private void Btn_OnClick(object sender, RoutedEventArgs e) {
            
            TRainerEditionHelper.PlayClick();

            if (!(sender is FrameworkElement fe)) return;
            string name = fe.Name;
            
            //修改器激活状态
            if (name == this.Image_TRainer_State.Name) {
                _isTRainerOpen = !_isTRainerOpen;
                Uri uri = TRainerEditionHelper.GetSwitchUri(_isTRainerOpen);
                this.Image_TRainer_State.Source = new BitmapImage(uri);
                TRainerEditionHelper.PlayActivate(_isTRainerOpen);
                
                //TODO: 打印坐标, 判断当前是点击运行的程序, 还是打包发布后的程序??? _isTRainerOpen true
                if (_isProcOpen && !true) {
                    // float x = MemoryDllUtils.ReadFloat(XAxis);
                    // float y = MemoryDllUtils.ReadFloat(YAxis);
                    // float z = MemoryDllUtils.ReadFloat(ZAxis);
                    // Console.WriteLine($"x={x}, y={y}, z={z}");
                    Console.WriteLine($"x={this.TB_XAxis.Text}, y={this.TB_YAxis.Text}, z={this.TB_ZAxis.Text}");
                }
                return;
            }
            //关于
            if (name == this.Btn_About.Name) {
                MessageBox.Show(TRainerEditionHelper.StrAbout, "说明(explain):");
                return;
            }
            //问号❓️❔️
            if (name == this.Image_Question_Mark.Name) {
                MessageBox.Show(TRainerEditionHelper.StrBrightness, "亮度修改说明(Brightness explain):");
                return;
            }

            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;
            
            //钱💰
            if (name == this.Btn_Money.Name) {
                TRainerEditionHelper.MoneyAdd();
                return;
            }
            
            //啤酒🍺
            if (name == this.Btn_Beer.Name) {
                TRainerEditionHelper.BeerAdd();
                return;
            }
            //威士忌🤳
            if (name == this.Btn_Whiskey.Name) {
                TRainerEditionHelper.WhiskeyAdd();
                return;
            }
            //大麻🍃
            if (name == this.Btn_Nug.Name) {
                TRainerEditionHelper.NugAdd();
                return;
            }
            //蘑菇🍄
            if (name == this.Btn_Shroom.Name) {
                TRainerEditionHelper.ShroomAdd();
                return;
            }
            //仙人掌🌵
            if (name == this.Btn_Peyote.Name) {
                TRainerEditionHelper.PeyoteAdd();
                return;
            }
            //青蛙🐸
            if (name == this.Btn_Frog.Name) {
                TRainerEditionHelper.FrogAdd();
                return;
            }
            //可卡因
            if (name == this.Btn_Crack.Name) {
                TRainerEditionHelper.CrackAdd();
                return;
            }
            
            //东
            if (name == this.Btn_XAxis_Plus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionHelper.GoRightOrLeft(true, value);
                return;
            }
            //西
            if (name == this.Btn_XAxis_Minus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionHelper.GoRightOrLeft(false, value);
                return;
            }
            
            //北
            if (name == this.Btn_YAxis_Plus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionHelper.GoFrontOrBack(true, value);
                return;
            }
            //南
            if (name == this.Btn_YAxis_Minus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionHelper.GoFrontOrBack(false, value);
                return;
            }
            
            //高度+
            if (name == this.Btn_ZAxis_Plus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionHelper.ZAxisEdit(true, value);
                return;
            }
            //高度-
            if (name == this.Btn_ZAxis_Minus.Name) {
                if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
                TRainerEditionHelper.ZAxisEdit(false, value);
                return;
            }
            //jj性感度加到最大
            if (name == this.Btn_Balls_Max.Name) {
                TRainerEditionHelper.BallsAdd210();
                return;
            }
            //无限健康
            if (name == this.Image_Infinite_Health.Name) {
                isFreezeHealth = !isFreezeHealth;
                Uri uri = TRainerEditionHelper.GetSwitchUri(isFreezeHealth);
                this.Image_Infinite_Health.Source = new BitmapImage(uri);
                TRainerEditionHelper.FreezeHealth(isFreezeHealth);
                return;
            }
            //冻结跳高效果
            if (name == this.Image_Freeze_High_Jump.Name) {
                isFreezeHighJump = !isFreezeHighJump;
                Uri uri = TRainerEditionHelper.GetSwitchUri(isFreezeHighJump);
                this.Image_Freeze_High_Jump.Source = new BitmapImage(uri);
                TRainerEditionHelper.FreezeHighJump(isFreezeHighJump);
                return;
            }
            //冻结快跑效果
            if (name == this.Image_Freeze_Fast_Run.Name) {
                isFreezeFastRun = !isFreezeFastRun;
                Uri uri = TRainerEditionHelper.GetSwitchUri(isFreezeFastRun);
                this.Image_Freeze_Fast_Run.Source = new BitmapImage(uri);
                TRainerEditionHelper.FreezeFastRun(isFreezeFastRun);
                return;
            }
            //冻结快感进度
            if (name == this.Image_Freeze_Climax.Name) {
                isFreezeClimax = !isFreezeClimax;
                Uri uri = TRainerEditionHelper.GetSwitchUri(isFreezeClimax);
                this.Image_Freeze_Climax.Source = new BitmapImage(uri);
                TRainerEditionHelper.FreezeClimax(isFreezeClimax);
                return;
            }
        }


        private void OnTeleportClick(object sender, RoutedEventArgs e) {

            TRainerEditionHelper.PlayClick();

            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;

            if (!(sender is FrameworkElement fe)) return;
            string name = fe.Name;

            //Map1(Missionary Beach 传教士海滩)→Map2(Firm Wood Forest 阔叶林)
            if (name == this.TB_MissionaryBeach2FirmWoodForest.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateMissionaryBeach2FirmWoodForest, "Map1(Missionary Beach 传教士海滩)→Map2(Firm Wood Forest 阔叶林) 传送点失败!");
                return;
            }
            //Map1(Missionary Beach 传教士海滩)→Map4(Gabacho Heights 加巴乔高地)
            if (name == this.TB_MissionaryBeach2GabachoHeights.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateMissionaryBeach2GabachoHeights, "Map1(Missionary Beach 传教士海滩)→Map4(Gabacho Heights 加巴乔高地) 传送点失败!");
                return;
            }
            
            //Map2(Firm Wood Forest 阔叶林)→Map1(Missionary Beach 传教士海滩)
            if (name == this.TB_FirmWoodForest2MissionaryBeach.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateFirmWoodForest2MissionaryBeach, "Map2(Firm Wood Forest 阔叶林)→Map1(Missionary Beach 传教士海滩) 传送点失败!");
                return;
            }
            //Map2(Firm Wood Forest 阔叶林)→Map3(Homeland Trailer Park 国土安全拖车公园)
            if (name == this.TB_FirmWoodForest2HomelandTrailerPark.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateFirmWoodForest2HomelandTrailerPark, "Map2(Firm Wood Forest 阔叶林)→Map3(Homeland Trailer Park 国土安全拖车公园) 传送点失败!");
                return;
            }
            
            //Map3(Homeland Trailer Park 国土安全拖车公园)→Map2(Firm Wood Forest 阔叶林)
            if (name == this.TB_HomelandTrailerPark2FirmWoodForest.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateHomelandTrailerPark2FirmWoodForest, "Map3(Homeland Trailer Park 国土安全拖车公园)→Map2(Firm Wood Forest 阔叶林) 传送点失败!");
                return;
            }

            //Map4(Gabacho Heights 加巴乔高地)→Map1(Missionary Beach 传教士海滩)
            if (name == this.TB_GabachoHeights2MissionaryBeach.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateGabachoHeights2MissionaryBeach, "Map4(Gabacho Heights 加巴乔高地)→Map1(Missionary Beach 传教士海滩) 传送点失败!");
                return;
            }
            //Map4(Gabacho Heights 加巴乔高地)→Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)
            if (name == this.TB_GabachoHeights2HavajoIndianReservation.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateGabachoHeights2HavajoIndianReservation, "Map4(Gabacho Heights 加巴乔高地)→Map5(Havajo Indian Reservation 哈瓦那印第安人保留地) 传送点失败!");
                return;
            }
            //Map4(Gabacho Heights 加巴乔高地)→Map6(Nobbing Hill 诺丁山)
            if (name == this.TB_GabachoHeights2NobbingHill.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateGabachoHeights2NobbingHill, "Map4(Gabacho Heights 加巴乔高地)→Map6(Nobbing Hill 诺丁山) 传送点失败!");
                return;
            }
            
            
            //Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)→Map4(Gabacho Heights 加巴乔高地)
            if (name == this.TB_HavajoIndianReservation2GabachoHeights.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateHavajoIndianReservation2GabachoHeights, "Map5(Havajo Indian Reservation 哈瓦那印第安人保留地)→Map4(Gabacho Heights 加巴乔高地) 传送点失败!");
                return;
            }
            
            
            //Map6(Nobbing Hill 诺丁山)→Map8(DownTown 市中心)
            if (name == this.TB_NobbingHill2DownTown.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateNobbingHill2DownTown, "Map6(Nobbing Hill 诺丁山)→Map8(DownTown 市中心) 传送点失败!");
                return;
            }
            //Map6(Nobbing Hill 诺丁山)→Map4(Gabacho Heights 加巴乔高地)
            if (name == this.TB_NobbingHill2GabachoHeights.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateNobbingHill2GabachoHeights, "Map6(Nobbing Hill 诺丁山)→Map4(Gabacho Heights 加巴乔高地) 传送点失败!");
                return;
            }
            //Map6(Nobbing Hill 诺丁山)→Map7(Mushroom Marsh 蘑菇沼泽)
            if (name == this.TB_NobbingHill2MushroomMarsh.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateNobbingHill2MushroomMarsh, "Map6(Nobbing Hill 诺丁山)→Map7(Mushroom Marsh 蘑菇沼泽) 传送点失败!");
                return;
            }
            
            
            //Map7(Mushroom Marsh 蘑菇沼泽) 撒旦(Satan)
            if (name == this.TB_MushroomMarsh_Satan.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateMushroomMarsh_Satan, "瞬移到 Map7(Mushroom Marsh 蘑菇沼泽) 撒旦(Satan) 失败!");
                return;
            }
            //Map7(Mushroom Marsh 蘑菇沼泽) 撒旦的老婆(Satan's wife)
            if (name == this.TB_MushroomMarsh_Satan_wife.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateMushroomMarsh_SatanWife, "瞬移到 Map7(Mushroom Marsh 蘑菇沼泽) 撒旦的老婆(Satan's wife) 失败!");
                return;
            }
            //Map7(Mushroom Marsh 蘑菇沼泽)→Map6(Nobbing Hill 诺丁山)
            if (name == this.TB_MushroomMarsh2NobbingHill.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateMushroomMarsh2NobbingHill, "Map7(Mushroom Marsh 蘑菇沼泽)→Map6(Nobbing Hill 诺丁山) 失败!");
                return;
            }
            
            
            //Map8(DownTown 市中心) RonJ大富翁
            if (name == this.TB_Downtown_RonJTowers.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateDowntown_RonJEntrance, "瞬移到 Map8(DownTown 市中心) RonJ大富翁 失败!");
                return;
            }
            //Map8(DownTown 市中心) 天使
            if (name == this.TB_Downtown_Angle.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateDowntown_Angle, "瞬移到 Map8(DownTown 市中心) 天使 失败!");
                return;
            }
            //Map8(DownTown 市中心)→Map9(Man Island 曼岛) 传送点
            if (name == this.TB_DownTown2ManIsland.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateDownTown2ManIsland, "Map8(DownTown 市中心)→Map9(Man Island 曼岛) 传送点失败!");
                return;
            }
            //Map8(DownTown 市中心)→Map6(Nobbing Hill 诺丁山) 传送点
            if (name == this.TB_DownTown2NobbingHill.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateDownTown2NobbingHill, "Map8(DownTown 市中心)→Map6(Nobbing Hill 诺丁山) 传送点失败!");
                return;
            }
            
            
            //Map9(Man Island 曼岛)→高塔入口(Man Needle)
            if (name == this.TB_ManIsland2ManNeedle.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateManIsland_ManNeedle, "Map9(Man Island 曼岛)→高塔入口(Man Needle) 传送点失败!");
                return;
            }
            //Map9(Man Island 曼岛)→Map8(DownTown 市中心)
            if (name == this.TB_ManIsland2DownTown.Name) {
                TRainerEditionHelper.Teleport(TRainerEditionHelper.CoordinateManIsland2DownTown, "Map9(Man Island 曼岛)→Map8(DownTown 市中心) 传送点失败!");
                return;
            }
        }

        private void Light_OnClick(object sender, RoutedEventArgs e) {
            TRainerEditionHelper.PlayClick();
            
            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;

            if (sender is Image image) {
                //先停止掉以前的线程, 否则以前的线程也一直在修改值, 就会n多个线程在改值, 快速闪动!!!
                isStartEditLamp = false;
                string nameImage = image.Name;
                if (nameImage == this.Image_Lamp_State.Name) {
                    _isLampOpen = !_isLampOpen;
                    Uri uri = TRainerEditionHelper.GetSwitchUri(_isLampOpen);
                    this.Image_Lamp_State.Source = new BitmapImage(uri);

                    if (false) {
                        //CPU占用非常高, 50%左右, 线程没有写对???
                        isStartEditLamp = true;
                        StartEditLamp();
                    } else {
                        TRainerEditionHelper.LampLightSet(_isLampOpen, true, true);
                    }
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
                    TRainerEditionHelper.BrightnessSet(0, false);
                }
                return;
            }
            if (name == this.RB_Brightness_Evening.Name) {
                if (false) {
                    editBrightnessPosition = 1;
                    isStartEditBrightness = true;
                    StartEditBrightness();
                } else {
                    TRainerEditionHelper.BrightnessSet(1, false);
                }
                return;
            }
            if (name == this.RB_Brightness_Noon.Name) {
                if (false) {
                    editBrightnessPosition = 2;
                    isStartEditBrightness = true;
                    StartEditBrightness();
                } else {
                    TRainerEditionHelper.BrightnessSet(2, false);
                }
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
                    TRainerEditionHelper.LampLightSet(_isLampOpen, false, false);
                    // Thread.Sleep(1);    //1~10都会闪动
                }
            }));
            TRainerEditionHelper.PlayAng();
        }
        //都会闪动
        private void StartEditBrightness() {
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         //1~8ms                                 参2=false, 也会一直闪动
            //         TRainerEditionHelper.BrightnessSet(editBrightnessPosition, false);
            //         // TRainerEditionHelper.BrightnessSet2(editBrightnessPosition);
            //         Thread.Sleep(10);
            //     }
            // }));
            // return;
            
            
            // UIntPtr code1 = MemoryDllUtils.Memory.GetCode(TRainerEditionHelper.Brightness_Ground_Green2, "");
            // if (code1 == UIntPtr.Zero || code1.ToUInt64() < 65536UL /*0x010000*/)
            //     return /*false*/;
            // UIntPtr code2 = MemoryDllUtils.Memory.GetCode(TRainerEditionHelper.Brightness_Ground_Purper2, "");
            // if (code2 == UIntPtr.Zero || code2.ToUInt64() < 65536UL /*0x010000*/)
            //     return /*false*/;
            // UIntPtr code3 = MemoryDllUtils.Memory.GetCode(TRainerEditionHelper.Brightness_Ground_Yellow2, "");
            // if (code3 == UIntPtr.Zero || code3.ToUInt64() < 65536UL /*0x010000*/)
            //     return /*false*/;
            // UIntPtr code4 = MemoryDllUtils.Memory.GetCode(TRainerEditionHelper.Brightness_Ground_Green, "");
            // if (code4 == UIntPtr.Zero || code4.ToUInt64() < 65536UL /*0x010000*/)
            //     return /*false*/;
            // UIntPtr code5 = MemoryDllUtils.Memory.GetCode(TRainerEditionHelper.Brightness_Ground_Purper, "");
            // if (code5 == UIntPtr.Zero || code5.ToUInt64() < 65536UL /*0x010000*/)
            //     return /*false*/;
            // UIntPtr code6 = MemoryDllUtils.Memory.GetCode(TRainerEditionHelper.Brightness_Ground_Yellow, "");
            // if (code6 == UIntPtr.Zero || code6.ToUInt64() < 65536UL /*0x010000*/)
            //     return /*false*/;

            //也是一直闪动
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer = BitConverter.GetBytes(TRainerEditionHelper.Ground_Green2[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code1, lpBuffer, (UIntPtr) 8, IntPtr.Zero);
            //
            //         byte[] lpBuffer2 = BitConverter.GetBytes(TRainerEditionHelper.Ground_Purper2[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code2, lpBuffer2, (UIntPtr) 8, IntPtr.Zero);
            //
            //         byte[] lpBuffer3 = BitConverter.GetBytes(TRainerEditionHelper.Ground_Yellow2[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code3, lpBuffer3, (UIntPtr) 8, IntPtr.Zero);
            //
            //         byte[] lpBuffer4 = BitConverter.GetBytes(TRainerEditionHelper.Ground_Green[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code4, lpBuffer4, (UIntPtr) 8, IntPtr.Zero);
            //
            //         byte[] lpBuffer5 = BitConverter.GetBytes(TRainerEditionHelper.Ground_Purper[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code5, lpBuffer5, (UIntPtr) 8, IntPtr.Zero);
            //
            //         byte[] lpBuffer6 = BitConverter.GetBytes(TRainerEditionHelper.Ground_Yellow[editBrightnessPosition]);
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
            //         byte[] lpBuffer = BitConverter.GetBytes(TRainerEditionHelper.Ground_Green2[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code1, lpBuffer, (UIntPtr) 8, IntPtr.Zero);
            //     }
            // }));
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer2 = BitConverter.GetBytes(TRainerEditionHelper.Ground_Purper2[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code2, lpBuffer2, (UIntPtr) 8, IntPtr.Zero);
            //     }
            // }));
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer3 = BitConverter.GetBytes(TRainerEditionHelper.Ground_Yellow2[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code3, lpBuffer3, (UIntPtr) 8, IntPtr.Zero);
            //     }
            // }));
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer4 = BitConverter.GetBytes(TRainerEditionHelper.Ground_Green[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code4, lpBuffer4, (UIntPtr) 8, IntPtr.Zero);
            //     }
            // }));
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer5 = BitConverter.GetBytes(TRainerEditionHelper.Ground_Purper[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code5, lpBuffer5, (UIntPtr) 8, IntPtr.Zero);
            //     }
            // }));
            // Task.Factory.StartNew((Action)(() => {
            //     while (isStartEditBrightness) {
            //         if (!_isProcOpen) return;
            //         if (!_isTRainerOpen) return;
            //         
            //         byte[] lpBuffer6 = BitConverter.GetBytes(TRainerEditionHelper.Ground_Yellow[editBrightnessPosition]);
            //         Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code6, lpBuffer6, (UIntPtr) 8, IntPtr.Zero);
            //     }
            // }));
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