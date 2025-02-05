using System;
using System.ComponentModel;
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
        private const string Mushroom = ModuleName + "+0x00532A28,0x2B8,0x5AC";
        private const string Cactus   = ModuleName + "+0x00532A28,0x2B8,0x5B0";
        private const string Frog     = ModuleName + "+0x00532A28,0x2B8,0x5B4";
        private const string Crack    = ModuleName + "+0x00532A28,0x2B8,0x5B8";
        private const string XAxis    = ModuleName + "+0x00532A28,0x2B8,0x7C4";
        private const string YAxis    = ModuleName + "+0x00532A28,0x2B8,0x7C8";
        private const string ZAxis    = ModuleName + "+0x00532A28,0x2B8,0x7CC";

        private DispatcherTimer _dispatcherTimer;

        private bool _procOpen;

        public MainWindow() {
            InitializeComponent();
            InitializeTimer();

            //钱Money
            MemoryDllUtils.BindToUI<int>(Money, delegate(string s) {
                Console.WriteLine($"钱Money: {s}");
                // 使用 Dispatcher 切换到 UI 线程
                Dispatcher.Invoke(() => {
                    this.AT_Money.Text = s;
                });
            });
            //啤酒Beer
            MemoryDllUtils.BindToUI<int>(Beer, delegate(string s) {
                Dispatcher.Invoke(() => {
                    // this.AT_Money.Text = s;
                });
            });
            // this.Btn_Find_Process.Click += (sender, args) => {
            //     Console.WriteLine("点击了!!!");
            // };
        }

        private void InitializeTimer() {
            // 创建定时器，间隔为 1 秒
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(300.0);
            _dispatcherTimer.Tick += delegate(object sender, EventArgs args) {
                _procOpen = MemoryDllUtils.OpenProcess(ProcessName);
                Console.WriteLine($"openProcessSuccess: {_procOpen}");
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
            MemoryDllUtils.destroy();
            base.OnClosed(e);
        }
    }
}