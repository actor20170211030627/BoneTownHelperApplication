using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using BoneTownHelperApplication.Utils;

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

        public MainWindow() {
            InitializeComponent();
            InitializeTimer();
            
            // this.Btn_Find_Process.Click += (sender, args) => {
            //     Console.WriteLine("点击了!!!");
            // };

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
            };
            _dispatcherTimer.Start();
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
            //停止定时器
            _dispatcherTimer.Stop();
            MemoryDllUtils.CloseProcess();
            base.OnClosed(e);
        }

        private void Btn_OnClick(object sender, RoutedEventArgs e) {
            if (!(sender is Button button)) return;
            string name = button.Name;
            //东
            if (name == this.Btn_XAxis_Plus.Name) {
                float x = MemoryDllUtils.ReadFloat(XAxis);;
                bool isSuccessEast = MemoryDllUtils.WriteFloat(XAxis, x + 5.0f);
                if (!isSuccessEast) {
                    Console.WriteLine("向东+5失败!");
                }
                return;
            }
            //西
            if (name == this.Btn_XAxis_Minus.Name) {
                float x = MemoryDllUtils.ReadFloat(XAxis);
                bool isSuccessEast = MemoryDllUtils.WriteFloat(XAxis, x - 5.0f);
                if (!isSuccessEast) {
                    Console.WriteLine("向西+5失败!");
                }
                return;
            }
            
            //北
            if (name == this.Btn_YAxis_Plus.Name) {
                float y = MemoryDllUtils.ReadFloat(YAxis);
                bool isSuccessEast = MemoryDllUtils.WriteFloat(YAxis, y + 5.0f);
                if (!isSuccessEast) {
                    Console.WriteLine("向北+5失败!");
                }
                return;
            }
            //南
            if (name == this.Btn_YAxis_Minus.Name) {
                float y = MemoryDllUtils.ReadFloat(YAxis);
                bool isSuccessEast = MemoryDllUtils.WriteFloat(YAxis, y - 5.0f);
                if (!isSuccessEast) {
                    Console.WriteLine("向南+5失败!");
                }
                return;
            }
            
            //高度+
            if (name == this.Btn_ZAxis_Plus.Name) {
                float z = MemoryDllUtils.ReadFloat(ZAxis);
                bool isSuccessEast = MemoryDllUtils.WriteFloat(ZAxis, z + 5.0f);
                if (!isSuccessEast) {
                    Console.WriteLine("高度+5失败!");
                }
                return;
            }
            //高度-
            if (name == this.Btn_ZAxis_Minus.Name) {
                float z = MemoryDllUtils.ReadFloat(ZAxis);
                bool isSuccessEast = MemoryDllUtils.WriteFloat(ZAxis, z - 5.0f);
                if (!isSuccessEast) {
                    Console.WriteLine("高度-5失败!");
                }
                return;
            }
        }
    }
}