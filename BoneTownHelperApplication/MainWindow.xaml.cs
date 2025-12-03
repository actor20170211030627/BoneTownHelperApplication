using System;
using System.ComponentModel;
using BoneTownHelperApplication.Pages;

namespace BoneTownHelperApplication {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        public MainWindow() {
            InitializeComponent();
            //导航历史
            // this.ContentFrame.JournalOwnership = JournalOwnership.Automatic;
            
            this.ContentFrame.Navigate(new MainPage(ContentFrame));
            //手动清理历史记录
            this.ContentFrame.NavigationService.RemoveBackEntry();
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
            base.OnClosed(e);
        }
    }
}