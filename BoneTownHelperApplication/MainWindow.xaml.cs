using System;
using System.Diagnostics;
using BoneTownHelperApplication.Utils;
using Memory;

namespace BoneTownHelperApplication {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        private const string ProcessName = "BoneTown.exe";
        private const string Money = "0x00532A28";
        private const string Money_Offset0 = "0x2B8";
        private const string Money_Offset1 = "0x478";
        private bool ProcOpen;
        
        public MainWindow() {
            InitializeComponent();

            this.Btn_Find_Process.Click += (sender, args) => {
                ProcessUtils.findProcess("notepad");
                
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

            };
        }
    }
}