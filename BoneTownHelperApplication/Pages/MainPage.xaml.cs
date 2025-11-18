using System.Windows;
using System.Windows.Controls;

namespace BoneTownHelperApplication.Pages {
    public partial class MainPage : Page {

        private Frame contentFrame;
        
        public MainPage(Frame contentFrame) {
            InitializeComponent();
            this.contentFrame = contentFrame;
        }

        private void OnVersionSelectClick(object sender, RoutedEventArgs routedEventArgs) {
            if (!(sender is Button button)) return;
            string name = button.Name;
            
            if (name == this.Btn_Old_Version.Name) {
                contentFrame.Navigate(new TRainer_Old_Page());
                //手动清理历史记录
                contentFrame.NavigationService.RemoveBackEntry();
                return;
            }
            if (name == this.Btn_Second_Coming_Edition.Name) {
                contentFrame.Navigate(new TRainer_Edition_Page());
                //手动清理历史记录
                contentFrame.NavigationService.RemoveBackEntry();
                return;
            }
        }
    }
}