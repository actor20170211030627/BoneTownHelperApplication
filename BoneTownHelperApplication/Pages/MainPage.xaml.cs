using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Actor.CustomMessageBox;
using BoneTownHelperApplication.Utils;

namespace BoneTownHelperApplication.Pages {
    public partial class MainPage : Page {

        //老版本
        private const string Md5_Old_Version = "704a0ac5b4bb82d9399c7e21e8d60572";
        //XD下载的重置版: https://www.xdgame.com/game/1393.html
        private const string Md5_Xd_Game     = "c385409d1fbe2d0d44ccbed525ac8392";
        //QQ群下载的重置版
        private const string Md5_QQ_Group_32 = "bb6539e40a05161f9140956957aff8d3";
        private const string Md5_QQ_Group_64 = "42633ee17d4a8880356673ecbbd65692";
        //Steam正版
        private const string Md5_Stream_32   = "dc69f146ca1f901362b7efeeec9427e1";
        private const string Md5_Stream_64   = "0e49ac2c6d568498533805432a96b1d1";
        
        private const string Str_64Bit_Tip   = "你打开的BoneTown.exe是64位(修改器还没适配), 请打开BoneTown32.exe, 是否打开文件夹并选中BoneTown32.exe?\n\n" + 
                                               "U opened BoneTown.exe is 64bit(Trainer haven't support), Pls open BoneTown32.exe, open the folder and select BoneTown32.exe now?";

        private readonly Frame _contentFrame;
        private readonly DispatcherTimer _dispatcherTimer;
        
        public MainPage(Frame contentFrame) {
            InitializeComponent();
            this._contentFrame = contentFrame;
            
            this.Loaded += MyPage_Loaded;  // 订阅Loaded事件
            this.Unloaded += MyPage_Unloaded;  // 订阅Unloaded事件

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(600.0);
            _dispatcherTimer.Tick += delegate(object sender, EventArgs args) {
                //D:\Program Files (x86)\BoneTown\BoneTown.exe
                string processPath = ProcessUtils.GetProcessPath(TRainerHelper.ProcessName);
                if (processPath == null) {
                    processPath = ProcessUtils.GetProcessPath(TRainerEditionQQGroup32Helper.ProcessName);
                }
                if (processPath == null) return;
                //计算文件的MD5值
                string md5Hash = Md5Utils.CalculateFileMd5(processPath);
                Console.WriteLine($"文件路径：{processPath}\nMD5值：{md5Hash}");
                if (md5Hash == null) return;

                switch (md5Hash) {
                    case Md5_Old_Version:
                        Go2OldVersion();
                        break;
                    case Md5_Xd_Game:
                        Go2Xd_Game();
                        break;
                    case Md5_QQ_Group_32:
                        Go2QQ_Group_32();
                        break;
                    case Md5_QQ_Group_64:
                        MessageBoxResult result0 = MessageBox2.NewBuilder(Str_64Bit_Tip)
                            .SetCaption("修改器提示(Trainer tips)")
                            .SetIcon(MessageBoxImage.Question)
                            .SetButton(MessageBoxButton.YesNo)
                            .Build()
                            .ShowDialog();
                        if (result0 == MessageBoxResult.Yes) {
                            string folder = FileUtils.GetDirectoryName(processPath);
                            FileUtils.OpenFolder(folder + "/BoneTown32.exe", true);
                        }
                        break;
                    case Md5_Stream_32:
                        Go2Steam_32();
                        break;
                    case Md5_Stream_64:
                        MessageBoxResult result1 = MessageBox2.NewBuilder(Str_64Bit_Tip)
                            .SetCaption("修改器提示(Trainer tips)")
                            .SetIcon(MessageBoxImage.Question)
                            .SetButton(MessageBoxButton.YesNo)
                            .Build()
                            .ShowDialog();
                        if (result1 == MessageBoxResult.Yes) {
                            string folder = FileUtils.GetDirectoryName(processPath);
                            FileUtils.OpenFolder(folder + "/BoneTown32.exe", true);
                        }
                        break;
                    default:
                        MessageBox2.NewBuilder("你在哪儿下载的版本? 我这儿没有这个版本, 请加Q群206483634反馈\n" +
                                               "Can't find you game version, Pls join the QQ group: 206483634 to feedback"
                                               )
                            .SetCaption("修改器提示(Trainer tips)")
                            .SetIcon(MessageBoxImage.Error)
                            .Build()
                            .ShowDialog();
                        break;
                }
            };
            _dispatcherTimer.Start();
        }

        private void Go2OldVersion() {
            _contentFrame.Navigate(new TRainer_Old_Page(_contentFrame));
            //手动清理历史记录
            _contentFrame.NavigationService.RemoveBackEntry();
        }

        private void Go2Xd_Game() {
            _contentFrame.Navigate(new TRainer_Edition_XD_Game_Page(_contentFrame));
            //手动清理历史记录
            _contentFrame.NavigationService.RemoveBackEntry();
        }

        private void Go2QQ_Group_32() {
            _contentFrame.Navigate(new TRainer_Edition_QQ_Group_32_Page(_contentFrame));
            //手动清理历史记录
            _contentFrame.NavigationService.RemoveBackEntry();
        }

        private void Go2Steam_32() {
            _contentFrame.Navigate(new TRainer_Edition_Steam_32_Page(_contentFrame));
            //手动清理历史记录
            _contentFrame.NavigationService.RemoveBackEntry();
        }

        private void OnVersionSelectClick(object sender, RoutedEventArgs routedEventArgs) {
            if (!(sender is Button button)) return;
            string name = button.Name;
            
            if (name == this.Btn_Old_Version.Name) {
                Go2OldVersion();
                return;
            }
            //在xdgame下载
            if (name == this.Btn_Second_Coming_Edition_XD.Name) {
                Go2Xd_Game();
                return;
            }
            //在Q群下载
            if (name == this.Btn_Second_Coming_Edition_QQ_Group.Name) {
                MessageBoxResult result = MessageBox2.NewBuilder("请确保打开的是BoneTown32.exe(64位没适配)\nPls ensure opened BoneTown32.exe(64bit no support)")
                    .SetCaption("修改器提示(Trainer tips)")
                    .SetIcon(MessageBoxImage.Warning)
                    .SetButton(MessageBoxButton.OKCancel)
                    .Build()
                    .ShowDialog();
                if (result == MessageBoxResult.OK) {
                    Go2QQ_Group_32();
                }
                return;
            }
            //在Steam正版下载
            if (name == this.Btn_Second_Coming_Edition_Steam.Name) {
                MessageBoxResult result = MessageBox2.NewBuilder("请确保打开的是BoneTown32.exe(64位没适配)\nPls ensure opened BoneTown32.exe(64bit no support)")
                    .SetCaption("修改器提示(Trainer tips)")
                    .SetIcon(MessageBoxImage.Warning)
                    .SetButton(MessageBoxButton.OKCancel)
                    .Build()
                    .ShowDialog();
                if (result == MessageBoxResult.OK) {
                    Go2Steam_32();
                }
                return;
            }
        }


        private void MyPage_Loaded(object sender, RoutedEventArgs e) {
            Console.WriteLine("页面加载完成 - 在这里初始化数据");
            // 加载数据、绑定事件等
        }

        // 页面卸载时执行 - 这是主要的方法
        private void MyPage_Unloaded(object sender, RoutedEventArgs e) {
            Console.WriteLine("页面卸载 - 在这里清理资源");
            //停止定时器
            _dispatcherTimer.Stop();
            // _dispatcherTimer.Tick -= ;
        }
    }
}