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
        private const string moduleName = "BoneTown.exe";
        private const string Money    = moduleName + "+0x00532A28,0x2B8,0x478";
        private const string Beer     = moduleName + "+0x00532A28,0x2B8,0x5A0";
        private const string Whiskey  = moduleName + "+0x00532A28,0x2B8,0x5A4";
        private const string Weed     = moduleName + "+0x00532A28,0x2B8,0x5A8";
        private const string Mushroom = moduleName + "+0x00532A28,0x2B8,0x5AC";
        private const string Cactus   = moduleName + "+0x00532A28,0x2B8,0x5B0";
        private const string Frog     = moduleName + "+0x00532A28,0x2B8,0x5B4";
        private const string Crack    = moduleName + "+0x00532A28,0x2B8,0x5B8";
        private const string XAxis    = moduleName + "+0x00532A28,0x2B8,0x7C4";
        private const string YAxis    = moduleName + "+0x00532A28,0x2B8,0x7C8";
        private const string ZAxis    = moduleName + "+0x00532A28,0x2B8,0x7CC";

        private DispatcherTimer _timer;
        private bool ProcOpen;

        public MainWindow() {
            InitializeComponent();
            InitializeTimer();

            this.Btn_Find_Process.Click += (sender, args) => {

                // Mem m = new Mem();
                // int processId = m.GetProcIdFromName(ProcessName);
                // ProcOpen = m.OpenProcess(processId, out var failReason);
                // if (ProcOpen) {
                //     IntPtr gameBase = m.GetModuleAddressByName(ProcessName);
                //     Debug.WriteLine(gameBase.ToString("X"));
                //     
                //     int money = m.ReadMemory<int>($"{ProcessName}+{Money},${Money_Offset0},${Money_Offset1}");
                //     this.AT_Money.Text = money.ToString();
                // } else {
                //     Console.WriteLine(failReason);
                //     this.AT_Money.Text = failReason;
                // }


                /**
                 * SharpMemoryCache
                 */
                // 获取目标进程
                // var process = Process.GetProcessesByName("game").FirstOrDefault();
                // if (process == null) return;
                //
                // // 创建内存操作实例
                // MemoryCache cache = new TrimmingMemoryCache("notepad", null);
                // // var memory = new SharpMemory(process.Id);
                //
                // TSource source = cache.FirstOrDefault();
                //
                // // 读取内存值
                // long address = 0x12345678;
                // int value = memory.ReadInt(address);
                // Console.WriteLine($"读取值: {value}");
                //
                // // 写入内存值
                // memory.WriteInt(address, 999);
            };
        }

        private void InitializeTimer() {
            // 创建定时器，间隔为 1 秒
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            // 启动定时器（也可以在页面加载时启动）
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            /**
             * memory.dll
             */
            MemoryDllUtils.findProcess(ProcessName);
            // 定时任务逻辑（例如更新界面控件）
            // lblTime.Content = DateTime.Now.ToString("HH:mm:ss");
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
            _timer.Stop();
            base.OnClosed(e);
        }
    }
}