using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using BoneTownHelperApplication.Utils;
using Gma.System.MouseKeyHook;
using Button = System.Windows.Controls.Button;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace BoneTownHelperApplication {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        private const string ProcessName = "BoneTown";
        private const string ModuleName = "BoneTown.exe";
        private const string Money    = ModuleName + "+0x00532A28,0x2B8,0x478";
        private const string Beer     = ModuleName + "+0x00532A28,0x2B8,0x5A0";
        private const string Whiskey  = ModuleName + "+0x00532A28,0x2B8,0x5A4";
        private const string Weed     = ModuleName + "+0x00532A28,0x2B8,0x5A8";
        private const string Shroom   = ModuleName + "+0x00532A28,0x2B8,0x5AC";
        private const string Peyote   = ModuleName + "+0x00532A28,0x2B8,0x5B0";
        private const string Frog     = ModuleName + "+0x00532A28,0x2B8,0x5B4";
        private const string Crack    = ModuleName + "+0x00532A28,0x2B8,0x5B8";
        private const string XAxis    = ModuleName + "+0x00532A28,0x2B8,0x7C4";
        private const string YAxis    = ModuleName + "+0x00532A28,0x2B8,0x7C8";
        private const string ZAxis    = ModuleName + "+0x00532A28,0x2B8,0x7CC";

        private DispatcherTimer _dispatcherTimer;

        //进程是否打开
        private bool _isProcOpen = false;
        //修改器是否激活
        private bool _isTRainerOpen = true;
        private bool _False = false;
        
        //程序集名称: BoneTownHelperApplication
        private string assemblyName = string.Empty;
        private string strAbout = "1.本软件针对英文版, 因为我没有中文版本. (这游戏剧情不难, 汉化版也只汉化了菜单那几个按钮, 所以没有安装中文版)\n" +
                                  "2.如果你使用了另外的修改器, 可以和其他修改器混用.\n" +
                                  "3.使用示例:\n" +
                                  "  1.游戏左下角地图面向地图的\"N\"北极, 东南西北平移更直观.\n" +
                                  "  2.和妹子友好交流的时候, 也可以平移, 能够看见更多细节哟(^_^).\n" +
                                  "  3.打Boss的时候也可以直接平移到他头顶, 然后一直在空中放闪电技能(在空中的时候不要走动, 否则会掉下来).\n" +
                                  "4.有问题请在百度贴吧发帖子反馈: https://tieba.baidu.com/f?kw=bonetown, (我想起来的时候会去看看).\n" +
                                  "5.杀毒软件报毒: 请自己添加进白名单.\n" +
                                  "6.作者 actor2015\n" +
                                  "7.版本 20230507 & v1.0\n" +
                                  "\n" +
                                  "1.This trainer not support Chinese menu version game.\n" +
                                  "2.If you use other trainers, you can use this with others.\n" +
                                  "3.Use example:\n" +
                                  "  1.When you use the function of Translation to [E, W, N, S], you should let role face the North, and you will use visual.\n" +
                                  "  2.If you make with the girl, you can translation too, and you can see more details(^_^).\n" +
                                  "  3.If you hit boss, you can translation to hi's head, and release lightning(don't move, or you will drop down).\n" +
                                  "4.If you have any issues, Pls issue at https://tieba.baidu.com/f?kw=bonetown(Chinese webside) to feedback.(Pls explain you country and issues in webside, i will see sometimes.)\n" +
                                  "5.If the antivirus software reports an error, Pls add this to whitelist.\n" +
                                  "6.Author actor2015\n" +
                                  "7.Version 20230507 & v1.0";

        private IKeyboardMouseEvents m_GlobalHook;
        private System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer();

        public MainWindow() {
            InitializeComponent();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            assemblyName = assembly.GetName().Name;

            InitializeTimer();
            InitializeMouseKeyHook();
            
            // this.Btn_Find_Process.Click += (sender, args) => {
            //     Console.WriteLine("点击了!!!");
            // };
            
            
            // 监听媒体成功打开的事件
            // MediaPlayerUtils.MediaOpenedAdd(mediaPlayer, (sender1, e1) => {
            //     // 媒体文件成功打开后，开始播放
            //     mediaPlayer.Play();
            //     Console.WriteLine("Media opened successfully.");
            // });
            // // 监听媒体打开失败的事件
            // MediaPlayerUtils.MediaFailedAdd(mediaPlayer, (sender1, e2) => {
            //     // 这里会显示具体的错误信息
            //     Console.WriteLine($"Media failed: {e2.ErrorException.Message}");
            // });
            

            //钱Money
            MemoryDllUtils.BindToUI<int>(Money, delegate(string s) {
                // Console.WriteLine($"钱Money: {s}");
                // 使用 Dispatcher 切换到 UI 线程
                Dispatcher.Invoke(() => {
                    this.TB_Money.Text = s;
                });
            });
            //啤酒Beer
            MemoryDllUtils.BindToUI<int>(Beer, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Beer.Text = s;
                });
            });
            //威士忌Whiskey
            MemoryDllUtils.BindToUI<int>(Whiskey, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Whiskey.Text = s;
                });
            });
            //大麻
            MemoryDllUtils.BindToUI<int>(Weed, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Nug.Text = s;
                });
            });
            //迷幻蘑菇Shroom
            MemoryDllUtils.BindToUI<int>(Shroom, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Shroom.Text = s;
                });
            });
            //佩奥特掌（仙人掌的一种）Peyote
            MemoryDllUtils.BindToUI<int>(Peyote, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Peyote.Text = s;
                });
            });
            //青蛙Frog
            MemoryDllUtils.BindToUI<int>(Frog, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Frog.Text = s;
                });
            });
            //强效可卡因Crack
            MemoryDllUtils.BindToUI<int>(Crack, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Crack.Text = s;
                });
            });
            
            
            /**
             * 下方读取到的xyz值都为0...
             */
            //x轴 XAxis
            // MemoryDllUtils.BindToUI<float>(XAxis, delegate(string s) {
            //     Console.WriteLine($"x = {s}");
            //     Dispatcher.Invoke(() => {
            //         this.TB_XAxis.Text = s;
            //     });
            // });
            // //y轴 YAxis
            // MemoryDllUtils.BindToUI<float>(YAxis, delegate(string s) {
            //     Console.WriteLine($"y = {s}");
            //     Dispatcher.Invoke(() => {
            //         this.TB_YAxis.Text = s;
            //     });
            // });
            // //z轴 ZAxis
            // MemoryDllUtils.BindToUI<float>(ZAxis, delegate(string s) {
            //     Console.WriteLine($"z = {s}");
            //     Dispatcher.Invoke(() => {
            //         this.TB_ZAxis.Text = s;
            //     });
            // });
            
            
            /**
             * 监听按键: 只能在窗口获取焦点时才管用
             */
            if (_False) {
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
                _isProcOpen = MemoryDllUtils.OpenProcess(ProcessName);
                if (_isProcOpen) {
                    //x轴 XAxis
                    float x = MemoryDllUtils.ReadFloat(XAxis);
                    //InvariantCulture: 美国英语（en-US）但独立于特定国家或地区，主要用于处理货币、日期、时间等文化敏感数据时避免因地区差异导致格式错误。
                    this.TB_XAxis.Text = x.ToString(CultureInfo.InvariantCulture);
                    //y轴 YAxis
                    float y = MemoryDllUtils.ReadFloat(YAxis);
                    this.TB_YAxis.Text = y.ToString(CultureInfo.InvariantCulture);
                    //z轴 ZAxis
                    float z = MemoryDllUtils.ReadFloat(ZAxis);
                    this.TB_ZAxis.Text = z.ToString(CultureInfo.InvariantCulture);
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
                MoneyAdd();
            };
            Action actionBeer = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                BeerAdd();
            };
            Action actionWhiskey = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                WhiskeyAdd();
            };
            Action actionNug = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                NugAdd();
            };
            Action actionShroom = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                ShroomAdd();
            };
            Action actionPeyote = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                PeyoteAdd();
            };
            Action actionFrog = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                FrogAdd();
            };
            Action actionCrack = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                CrackAdd();
            };
            Action actionHeightAdd = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                ZAxisEdit(true);
            };
            Action actionHeightMinus = () => {
                if (!_isProcOpen) return;
                if (!_isTRainerOpen) return;
                ZAxisEdit(false);
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
                XAxisEdit(true);
                return;
            }
            if (e.KeyCode == Keys.Left) {   //西
                XAxisEdit(false);
                return;
            }
            if (e.KeyCode == Keys.Up) {     //北
                YAxisEdit(true);
                return;
            }
            if (e.KeyCode == Keys.Down) {   //南
                YAxisEdit(false);
                return;
            }
        }
        

        private void Btn_OnClick(object sender, RoutedEventArgs e) {
            if (!_isProcOpen) return;
            
            if (!(sender is Button button)) return;
            
            string name = button.Name;
            //修改器激活状态
            if (name == this.Btn_TRainer_State.Name) {
                _isTRainerOpen = !_isTRainerOpen;
                Uri uri = _isTRainerOpen ? 
                    new Uri($"pack://application:,,,/{assemblyName};component/Resources/Images/icon_switch_green2.png") 
                    : new Uri($"pack://application:,,,/{assemblyName};component/Resources/Images/icon_switch_lightyellow.png");
                this.Image_TRainer_State.Source = new BitmapImage(uri);
                return;
            }
            //关于
            if (name == this.Btn_About.Name) {
                MessageBox.Show(strAbout, "说明(explain):");
                return;
            }

            if (!_isTRainerOpen) return;
            
            //钱💰
            if (name == this.Btn_Money.Name) {
                MoneyAdd();
                return;
            }
            
            //啤酒🍺
            if (name == this.Btn_Beer.Name) {
                BeerAdd();
                return;
            }
            //威士忌🤳
            if (name == this.Btn_Whiskey.Name) {
                WhiskeyAdd();
                return;
            }
            //大麻🍃
            if (name == this.Btn_Nug.Name) {
                NugAdd();
                return;
            }
            //蘑菇🍄
            if (name == this.Btn_Shroom.Name) {
                ShroomAdd();
                return;
            }
            //仙人掌🌵
            if (name == this.Btn_Peyote.Name) {
                PeyoteAdd();
                return;
            }
            //青蛙🐸
            if (name == this.Btn_Frog.Name) {
                FrogAdd();
                return;
            }
            //可卡因
            if (name == this.Btn_Crack.Name) {
                CrackAdd();
                return;
            }
            
            //东
            if (name == this.Btn_XAxis_Plus.Name) {
                XAxisEdit(true);
                return;
            }
            //西
            if (name == this.Btn_XAxis_Minus.Name) {
                XAxisEdit(false);
                return;
            }
            
            //北
            if (name == this.Btn_YAxis_Plus.Name) {
                YAxisEdit(true);
                return;
            }
            //南
            if (name == this.Btn_YAxis_Minus.Name) {
                YAxisEdit(false);
                return;
            }
            
            //高度+
            if (name == this.Btn_ZAxis_Plus.Name) {
                ZAxisEdit(true);
                return;
            }
            //高度-
            if (name == this.Btn_ZAxis_Minus.Name) {
                ZAxisEdit(false);
                return;
            }
        }

        private void MoneyAdd() {
            int money = MemoryDllUtils.ReadInt(Money);
            bool isSuccess = MemoryDllUtils.WriteInt(Money, money + 1000);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("钱+1000失败!");
            }
        }

        private void BeerAdd() {
            int beer = MemoryDllUtils.ReadInt(Beer);
            bool isSuccess = MemoryDllUtils.WriteInt(Beer, beer + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("啤酒+100失败!");
            }
        }

        private void WhiskeyAdd() {
            int whiskey = MemoryDllUtils.ReadInt(Whiskey);
            bool isSuccess = MemoryDllUtils.WriteInt(Whiskey, whiskey + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("威士忌+100失败!");
            }
        }
        
        private void NugAdd() {
            int nug = MemoryDllUtils.ReadInt(Weed);
            bool isSuccess = MemoryDllUtils.WriteInt(Weed, nug + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("大麻+100失败!");
            }
        }

        private void ShroomAdd() {
            int shroom = MemoryDllUtils.ReadInt(Shroom);
            bool isSuccess = MemoryDllUtils.WriteInt(Shroom, shroom + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("蘑菇+100失败!");
            }
        }

        private void PeyoteAdd() {
            int peyote = MemoryDllUtils.ReadInt(Peyote);
            bool isSuccess = MemoryDllUtils.WriteInt(Peyote, peyote + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("仙人掌+100失败!");
            }
        }

        private void FrogAdd() {
            int frog = MemoryDllUtils.ReadInt(Frog);
            bool isSuccess = MemoryDllUtils.WriteInt(Frog, frog + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("青蛙+100失败!");
            }
        }

        private void CrackAdd() {
            int crack = MemoryDllUtils.ReadInt(Crack);
            bool isSuccess = MemoryDllUtils.WriteInt(Crack, crack + 100);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("可卡因+100失败!");
            }
        }

        private void XAxisEdit(bool isEast) {
            float x = MemoryDllUtils.ReadFloat(XAxis);
            bool isSuccess = MemoryDllUtils.WriteFloat(XAxis, isEast ? x + 5.0f : x - 5.0f);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine(isEast ? "向东+5失败!" : "向西+5失败!");
            }
        }
        
        private void YAxisEdit(bool isNorth) {
            float y = MemoryDllUtils.ReadFloat(YAxis);
            bool isSuccess = MemoryDllUtils.WriteFloat(YAxis, isNorth ? y + 5.0f : y - 5.0f);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine(isNorth ? "向北+5失败!" : "向南+5失败!");
            }
        }
        
        private void ZAxisEdit(bool isUp) {
            float z = MemoryDllUtils.ReadFloat(ZAxis);
            bool isSuccess = MemoryDllUtils.WriteFloat(ZAxis, isUp ? z + 5.0f : z - 5.0f);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine(isUp ? "高度+5失败!" : "高度-5失败!");
            }
        }
        
        //播放ang
        private void PlayAng() {
            Uri uri = new Uri("Resources/Medias/ang.wav", UriKind.Relative);
            SoundPlayerUtils.Stream(soundPlayer, uri);
            SoundPlayerUtils.Play(soundPlayer);
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