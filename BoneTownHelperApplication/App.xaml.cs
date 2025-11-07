using System;
using System.Windows;

namespace BoneTownHelperApplication {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            //https://www.bilibili.com/video/BV1bV411A7Eq
            Console.WriteLine("1.OnStartup 被执行"); //加载本地数据
        }
    }
}