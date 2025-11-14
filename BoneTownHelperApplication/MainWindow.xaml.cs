using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BoneTownHelperApplication {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        //修改器是否激活
        private bool _isTRainerOpen = true;
        //灯光是否打开
        private bool _isLampOpen = true;

        //进程是否打开
        private static bool _isProcOpen = false;

        //程序集名称: BoneTownHelperApplication
        private string assemblyName = string.Empty;
        
        private DispatcherTimer _dispatcherTimer;

        private IKeyboardMouseEvents m_GlobalHook;

        public MainWindow() {
            InitializeComponent();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            assemblyName = assembly.GetName().Name;

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
            //大麻
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Weed, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Nug.Text = s;
                });
            });
            //迷幻蘑菇Shroom
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Shroom, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Shroom.Text = s;
                });
            });
            //佩奥特掌（仙人掌的一种）Peyote
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Peyote, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Peyote.Text = s;
                });
            });
            //青蛙Frog
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Frog, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Frog.Text = s;
                });
            });
            //强效可卡因Crack
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Crack, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Crack.Text = s;
                });
            });
            
            
            /*
             * 下方读取到的xyz值都为0...
             */
            //x轴 XAxis
            // MemoryDllUtils.BindToUI<float>(TRainerHelper.XAxis, delegate(string s) {
            //     Console.WriteLine($"x = {s}");
            //     Dispatcher.Invoke(() => {
            //         this.TB_XAxis.Text = s;
            //     });
            // });
            // //y轴 YAxis
            // MemoryDllUtils.BindToUI<float>(TRainerHelper.YAxis, delegate(string s) {
            //     Console.WriteLine($"y = {s}");
            //     Dispatcher.Invoke(() => {
            //         this.TB_YAxis.Text = s;
            //     });
            // });
            // //z轴 ZAxis
            // MemoryDllUtils.BindToUI<float>(TRainerHelper.ZAxis, delegate(string s) {
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

        private void InitializeTimer() {
            // 创建定时器，间隔为 1 秒
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(300.0);
            _dispatcherTimer.Tick += delegate(object sender, EventArgs args) {
                _isProcOpen = MemoryDllUtils.OpenProcess(TRainerHelper.ProcessName);
                if (_isProcOpen) {
                    //TODO: false 先屏蔽掉显示坐标, true
                    if (false) {
                        //x轴 XAxis
                        float x = MemoryDllUtils.ReadFloat(TRainerHelper.XAxis);
                        //InvariantCulture: 美国英语（en-US）但独立于特定国家或地区，主要用于处理货币、日期、时间等文化敏感数据时避免因地区差异导致格式错误。
                        this.TB_XAxis.Text = x.ToString(CultureInfo.InvariantCulture);
                        //y轴 YAxis
                        float y = MemoryDllUtils.ReadFloat(TRainerHelper.YAxis);
                        this.TB_YAxis.Text = y.ToString(CultureInfo.InvariantCulture);
                        //z轴 ZAxis
                        float z = MemoryDllUtils.ReadFloat(TRainerHelper.ZAxis);
                        this.TB_ZAxis.Text = z.ToString(CultureInfo.InvariantCulture);
                    }
                } else {
                    Console.WriteLine($"openProcessSuccess: {_isProcOpen}");
                }

                this.Border_Running.Visibility = _isProcOpen ? Visibility.Visible : Visibility.Hidden;
                this.Border_Stopped.Visibility = _isProcOpen ? Visibility.Hidden : Visibility.Visible;
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
            
            PlayClick();

            if (!(sender is FrameworkElement fe)) return;
            string name = fe.Name;
            
            //修改器激活状态
            if (name == this.Image_TRainer_State.Name) {
                _isTRainerOpen = !_isTRainerOpen;
                Uri uri = _isTRainerOpen ? 
                    new Uri($"pack://application:,,,/{assemblyName};component/Resources/Images/icon_switch_green2.png") 
                    : new Uri($"pack://application:,,,/{assemblyName};component/Resources/Images/icon_switch_lightyellow.png");
                this.Image_TRainer_State.Source = new BitmapImage(uri);
                PlayActivate();
                
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
            //大麻🍃
            if (name == this.Btn_Nug.Name) {
                TRainerHelper.NugAdd();
                return;
            }
            //蘑菇🍄
            if (name == this.Btn_Shroom.Name) {
                TRainerHelper.ShroomAdd();
                return;
            }
            //仙人掌🌵
            if (name == this.Btn_Peyote.Name) {
                TRainerHelper.PeyoteAdd();
                return;
            }
            //青蛙🐸
            if (name == this.Btn_Frog.Name) {
                TRainerHelper.FrogAdd();
                return;
            }
            //可卡因
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
        }


        private void OnTeleportClick(object sender, RoutedEventArgs e) {

            PlayClick();

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


        //播放click
        private void PlayClick() {
            Uri uri = new Uri($"Resources/Medias/click.wav", UriKind.Relative);
            SoundPlayerUtils.Stream(TRainerHelper.SoundPlayer, uri);
            SoundPlayerUtils.Play(TRainerHelper.SoundPlayer);
        }

        //播放activate
        private void PlayActivate() {
            Uri uri = _isTRainerOpen ? new Uri($"Resources/Medias/activate.wav", UriKind.Relative)
                :  new Uri($"Resources/Medias/deactivate.wav", UriKind.Relative);
            SoundPlayerUtils.Stream(TRainerHelper.SoundPlayer, uri);
            SoundPlayerUtils.Play(TRainerHelper.SoundPlayer);
        }

        private void Light_OnClick(object sender, RoutedEventArgs e) {
            PlayClick();
            
            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;

            if (sender is Image image) {
                //先停止掉以前的线程, 否则以前的线程也一直在修改值, 就会n多个线程在改值, 快速闪动!!!
                isStartEditLamp = false;
                string nameImage = image.Name;
                if (nameImage == this.Image_Lamp_State.Name) {
                    _isLampOpen = !_isLampOpen;
                    Uri uri = _isLampOpen ? 
                        new Uri($"pack://application:,,,/{assemblyName};component/Resources/Images/icon_switch_green2.png") 
                        : new Uri($"pack://application:,,,/{assemblyName};component/Resources/Images/icon_switch_lightyellow.png");
                    this.Image_Lamp_State.Source = new BitmapImage(uri);

                    if (false) {
                        //CPU占用非常高, 50%左右, 线程没有写对???
                        isStartEditLamp = true;
                        StartEditLamp();
                    } else {
                        TRainerHelper.LampLightSet(_isLampOpen, true, true);
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
                    TRainerHelper.BrightnessSet(0, false);
                }
                return;
            }
            if (name == this.RB_Brightness_Evening.Name) {
                if (false) {
                    editBrightnessPosition = 1;
                    isStartEditBrightness = true;
                    StartEditBrightness();
                } else {
                    TRainerHelper.BrightnessSet(1, false);
                }
                return;
            }
            if (name == this.RB_Brightness_Noon.Name) {
                if (false) {
                    editBrightnessPosition = 2;
                    isStartEditBrightness = true;
                    StartEditBrightness();
                } else {
                    TRainerHelper.BrightnessSet(2, false);
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
        /// DesignerProperties.GetIsInDesignMode 方法不能直接用于区分 “开发工具中运行的程序” 和 “打包发布后的程序”，
        /// 因为它的设计目的是判断代码是否在设计时环境（如 Visual Studio 的设计视图、Blend 的预览界面）中执行，
        /// 而不是判断程序是否处于 “调试运行” 或 “发布运行” 状态。
        /// </summary>
        /// <returns></returns>
        private bool IsInDesignMode() {
            return DesignerProperties.GetIsInDesignMode(this);
        }
        
        protected override void OnClosing(CancelEventArgs e) {
            Console.WriteLine("OnClosing()");
            // MessageBoxResult result = MessageBox.Show("是否保存更改？", "提示", MessageBoxButton.YesNoCancel);
            // if (result == MessageBoxResult.Cancel) {
            //     e.Cancel = true; // 取消关闭
            //     return;
            // }
            // if (result == MessageBoxResult.Yes) {
            //     // SaveData(); // 保存数据
            // }
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e) {
            Console.WriteLine("OnClosed()");

            isStartEditLamp = false;
            isStartEditBrightness = false;
            
            m_GlobalHook.KeyUp -= GlobalHookKeyUp;
            //It is recommened to dispose it
            m_GlobalHook.Dispose();
            
            //停止定时器
            _dispatcherTimer.Stop();
            // _dispatcherTimer.Tick -= ;
            MemoryDllUtils.CloseProcess();
            
            base.OnClosed(e);
        }
    }
}