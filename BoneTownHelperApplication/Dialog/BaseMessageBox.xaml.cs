using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;

namespace BoneTownHelperApplication.Dialog {
    
    /// <summary>
    /// 自定义消息框，支持 Builder 模式，使用标准 MessageBoxButton, MessageBoxImage 和 MessageBoxResult
    /// </summary>
    public partial class BaseMessageBox : Window {

        private MessageBoxResult result = MessageBoxResult.None;
        private Window owner;
        // 状态跟踪
        private bool defaultSet, cancelSet;
        private Button cancelCandidate; // 暂存当前被设为取消的按钮，用于撤销
        
        private BaseMessageBox(Builder builder) {
            InitializeComponent();

            this.owner = builder.Owner;
            this.Title = builder.Title ?? "提示";
            this.MessageText.Text = builder.Message;

            // 设置图标
            if (builder.CustomIcon != null) {
                IconImage.Source = builder.CustomIcon;
                IconImage.Visibility = Visibility.Visible;
            } else if (builder.StandardIcon != MessageBoxImage.None) {
                IconImage.Source = GetStandardIconImageSource(builder.StandardIcon);
                IconImage.Visibility = Visibility.Visible;
            } else {
                IconImage.Visibility = Visibility.Collapsed;
                // 无图标时文本占满
                MessageText.Margin = new Thickness(0);
            }

            // 创建按钮
            CreateButtons(builder);
        }

        /// <summary>
        /// 创建Builder
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <returns></returns>
        public static Builder NewBuilder(string message) => new Builder(message);
        public static Builder NewBuilder(Window owner, string message) => new Builder(owner, message);

        // ---------- Builder 内部类 ----------
        public  class Builder {
            public Builder(string message) {
                this.Message = message;
            }
            
            public Builder(Window owner, string message) {
                this.Owner = owner;
                this.Message = message;
            }

            internal Window Owner { get; }
            internal string Title { get; private set; }
            internal string Message { get; }
            internal MessageBoxButton Button { get; private set; } = MessageBoxButton.OK;
            internal double ButtonMinWidth { get; private set; } = 75;
            internal MessageBoxImage StandardIcon { get; private set; } = MessageBoxImage.None;
            internal ImageSource CustomIcon { get; private set; }
            internal MessageBoxResult DefaultResult { get; private set; } = MessageBoxResult.None;
            internal string OkText { get; private set; }
            internal string CancelText { get; private set; }
            internal string YesText { get; private set; }
            internal string NoText { get; private set; }

            public Builder SetCaption(string caption) { return SetTitle(caption); }
            public Builder SetTitle(string title) { Title = title; return this; }
            
            /// <summary>
            /// 设置几个按钮
            /// </summary>
            /// <param name="button"><see cref="T:System.Windows.MessageBoxButton" /> 按钮类型, 例: <see cref="F:System.Windows.MessageBoxButton.YesNo">MessageBoxButton.YesNo</see></param>
            /// <returns></returns>
            public Builder SetButton(MessageBoxButton button) { Button = button; return this; }

            /// <summary>
            /// 设置Button最小宽度
            /// </summary>
            /// <param name="minWidth">Button最小宽度</param>
            /// <returns></returns>
            public Builder SetButtonMinWidth(double minWidth) {
                ButtonMinWidth = minWidth;
                return this;
            }

            // 标准图标
            public Builder SetIcon(MessageBoxImage icon) { 
                StandardIcon = icon; 
                CustomIcon = null; // 清除自定义图标
                return this; 
            }

            // 自定义图标（ImageSource）
            public Builder SetIcon(ImageSource icon) { 
                CustomIcon = icon; 
                StandardIcon = MessageBoxImage.None; // 清除标准图标
                return this; 
            }
            
            /// <summary>
            /// 设置默认按钮，它的作用是：
            /// 1. 初始焦点：对话框打开时，这个按钮会获得键盘焦点（通常显示为蓝色高亮或虚线框）。
            /// 2. Enter 键触发：用户按下 Enter 键时，会触发这个按钮的点击事件。
            /// 3. 允许切换：用户仍可以通过 Tab 键或方向键（←/→）在按钮间切换焦点，此时 Enter 键会触发当前焦点所在的按钮，而不再是默认按钮。
            /// 它定义了当用户直接按 Enter 时返回的结果，但并不强制用户只能点击这个按钮。
            /// </summary>
            /// <param name="defaultResult"></param>
            /// <returns></returns>
            public Builder SetDefaultResult(MessageBoxResult defaultResult) {
                DefaultResult = defaultResult;
                return this;
            }

            // 自定义按钮文本（可选）
            public Builder SetOkText(string text) { OkText = text; return this; }
            public Builder SetCancelText(string text) { CancelText = text; return this; }
            public Builder SetYesText(string text) { YesText = text; return this; }
            public Builder SetNoText(string text) { NoText = text; return this; }

            public BaseMessageBox Build() {
                return new BaseMessageBox(this);
            }
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <returns>A <see cref="T:System.Nullable`1" /> value of type <see cref="T:System.Boolean" /> that specifies whether the activity was accepted (true) or canceled (false). The return value is the value of the <see cref="P:System.Windows.Window.DialogResult" /> property before a window closes.</returns>
        public new MessageBoxResult Show() {
            // base.Show();
            Window targetOwner = this.owner;
            if (targetOwner == null || !targetOwner.IsVisible) {
                var main = Application.Current.MainWindow;
                if (main != null && main.IsVisible) {
                    targetOwner = main;
                } else targetOwner = null;
            }
            this.Owner = targetOwner;
            this.owner = null;
            bool? showDialog = base.ShowDialog();
            // return MessageBox.Win32ToMessageBoxResult(...);
            return result;
        }

        /// <summary>
        /// 内置标准图标生成（纯 WPF 矢量路径，无 WinForms）
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static ImageSource GetStandardIconImageSource(MessageBoxImage image) {
            string data;
            Color color;
            switch (image) {
                case MessageBoxImage.Information:
                    data = "M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z";
                    color = Color.FromRgb(30, 136, 229); // Blue
                    break;
                case MessageBoxImage.Warning:
                    data = "M1 21h22L12 2 1 21zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z";
                    color = Color.FromRgb(251, 140, 0); // Orange
                    break;
                case MessageBoxImage.Error:
                    data = "M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-2 15l-5-5 1.41-1.41L10 14.17l7.59-7.59L19 8l-9 9z";
                    color = Color.FromRgb(229, 57, 53); // Red
                    break;
                case MessageBoxImage.Question:
                    data = "M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 17h-2v-2h2v2zm2.07-7.75l-.9.92C13.45 12.9 13 13.5 13 15h-2v-.5c0-1.1.45-2.1 1.17-2.83l1.24-1.26c.37-.36.59-.86.59-1.41 0-1.1-.9-2-2-2s-2 .9-2 2H8c0-2.21 1.79-4 4-4s4 1.79 4 4c0 .88-.36 1.68-.93 2.25z";
                    color = Color.FromRgb(30, 136, 229); // Blue
                    break;
                default:
                    return null;
            }
            Geometry geometry = Geometry.Parse(data);
            GeometryDrawing drawing = new GeometryDrawing(new SolidColorBrush(color), null, geometry);
            return new DrawingImage(drawing);
        }

        // ---------- 动态生成按钮 ----------
        private void CreateButtons(Builder builder) {
            this.ButtonsPanel.Children.Clear();

            // 默认文本（中文）
            string okText = string.IsNullOrEmpty(builder.OkText) ? "确定" : builder.OkText;
            string cancelText = string.IsNullOrEmpty(builder.CancelText) ? "取消" : builder.CancelText;
            string yesText = string.IsNullOrEmpty(builder.YesText) ? "是" : builder.YesText;
            string noText = string.IsNullOrEmpty(builder.NoText) ? "否" : builder.NoText;

            switch (builder.Button) {
                case MessageBoxButton.OK:
                    AddButton(builder, okText, MessageBoxResult.OK);
                    break;
                case MessageBoxButton.OKCancel:
                    AddButton(builder, okText, MessageBoxResult.OK);
                    AddButton(builder, cancelText, MessageBoxResult.Cancel);
                    break;
                case MessageBoxButton.YesNo:
                    AddButton(builder, yesText, MessageBoxResult.Yes);
                    AddButton(builder, noText, MessageBoxResult.No);
                    break;
                case MessageBoxButton.YesNoCancel:
                    AddButton(builder, yesText, MessageBoxResult.Yes);
                    AddButton(builder, noText, MessageBoxResult.No);
                    AddButton(builder, cancelText, MessageBoxResult.Cancel);
                    break;
                default:
                    // 默认至少一个 OK 按钮
                    AddButton(builder, okText, MessageBoxResult.OK);
                    break;
            }
            // 如果因为某种原因 defaultSet 仍为 false（例如没有按钮或 defaultResult 不匹配），强制设置第一个为默认
            if (!defaultSet && this.ButtonsPanel.Children.Count > 0) {
                Button firstElement = (Button)this.ButtonsPanel.Children[0];
                firstElement.IsDefault = true;
                SetElementFocus(firstElement);
            }
            cancelCandidate = null;
        }
        
        private void AddButton(Builder builder, string content, MessageBoxResult btnResult) {
            var btn = new Button {
                Content = content,
                MinWidth = builder.ButtonMinWidth,
                // Width = 75,
                Height = 26, // 保留固定高度（可改为统一外观）
                Padding = new Thickness(8, 4, 8, 4),// 内边距让文字不贴边
                Margin = new Thickness(8, 0, 0, 0),
                // IsDefault = isDefault,
                // IsCancel = isCancel,
                Tag = btnResult
            };
            // 设置默认按钮：优先匹配 defaultResult，否则第一个按钮为默认
            if (!defaultSet) {
                if (builder.DefaultResult != MessageBoxResult.None && btnResult == builder.DefaultResult) {
                    btn.IsDefault = true;
                    defaultSet = true;
                    SetElementFocus(btn);
                } else if (builder.DefaultResult == MessageBoxResult.None && this.ButtonsPanel.Children.Count == 0) {
                    // 如果没有指定默认结果，则第一个按钮为默认
                    btn.IsDefault = true;
                    defaultSet = true;
                    SetElementFocus(btn);
                }
            }
            // 取消按钮逻辑：优先 Cancel，其次 No，且只保留最优先的一个
            if (btnResult == MessageBoxResult.Cancel) {
                // 如果之前有候选取消按钮（可能是No），撤销它的 IsCancel
                if (cancelCandidate != null) cancelCandidate.IsCancel = false;
                btn.IsCancel = true;
                cancelCandidate = btn;
                cancelSet = true;
            } else if (btnResult == MessageBoxResult.No && !cancelSet) {
                // 只有当尚未设置任何取消按钮时，才将 No 设为取消
                btn.IsCancel = true;
                cancelCandidate = btn;
                cancelSet = true;
            }
            
            btn.Click += (s, e) => {
                this.result = (MessageBoxResult)((Button)s).Tag;
                this.Close();
            };
            this.ButtonsPanel.Children.Add(btn);
        }

        /// <summary>
        /// 让焦点落到该按钮上，否则初始焦点可能在整个窗口上，按 Enter 虽然会触发默认按钮（因为 WPF 会处理），但用户看不到蓝色高亮
        /// 延迟设置焦点，确保窗口已加载
        /// Dispatcher.BeginInvoke 将焦点设置操作放入消息队列，在窗口布局完成、所有 Loaded 事件触发之后执行，此时视觉树已经完整，焦点能够正确设置。
        /// Background 优先级确保它不会阻塞 UI 渲染，几乎瞬间完成。
        /// </summary>
        /// <param name="element"></param>
        private void SetElementFocus(FrameworkElement element) {
            //设置后冇效果, 算求...
            if (true) return;
            // 确保焦点样式不为空
            if (element.FocusVisualStyle == null) {
                // 从系统资源恢复默认
                element.FocusVisualStyle = (Style)FindResource(SystemParameters.FocusVisualStyleKey);
            }
            // 使用 Dispatcher 延迟到布局和输入准备完成后执行
            this.Dispatcher.BeginInvoke(
                new Action(() => { element.Focus(); }),
                System.Windows.Threading.DispatcherPriority.Input);
        }

        // 处理点右上角 X 关闭
        protected override void OnClosing(CancelEventArgs e) {
            // result 保持 None
            base.OnClosing(e);
        }
    }
}