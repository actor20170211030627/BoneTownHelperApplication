using System;
using System.Windows;

namespace BoneTownHelperApplication.Utils {
    
    /// <summary>
    /// 弹窗工具类
    /// </summary>
    public class MessageBoxUtils {
        // 默认按钮文本
        private static readonly object DefaultOk = Application.Current.Resources["MessageBox.Ok"];
        private static readonly object DefaultCancel = Application.Current.Resources["MessageBox.Cancel"];
        private static readonly object DefaultYes = Application.Current.Resources["MessageBox.Yes"];
        private static readonly object DefaultNo = Application.Current.Resources["MessageBox.No"];
        
        /// <summary>
        /// 创建弹窗
        /// </summary>
        /// <param name="messageBoxText">弹框提示内容</param>
        /// <returns></returns>
        public static MessageBoxBuilder NewMessageBox(string messageBoxText) {
            return new MessageBoxBuilder(messageBoxText);
        }

        /// <summary>
        /// 创建弹窗
        /// </summary>
        /// <param name="owner">不要传null</param>
        /// <param name="messageBoxText">弹框提示内容</param>
        /// <returns></returns>
        public static MessageBoxBuilder NewMessageBox(Window owner, string messageBoxText) {
            // if (owner == null) throw new ArgumentNullException(nameof(owner));
            return new MessageBoxBuilder(owner, messageBoxText);
        }

        public class MessageBoxBuilder {
            private Window owner;
            private string messageBoxText;
            private string caption;
            private MessageBoxButton button = MessageBoxButton.OK;
            private MessageBoxImage icon = MessageBoxImage.None;
            private MessageBoxResult defaultResult = MessageBoxResult.None;
            private MessageBoxOptions options = MessageBoxOptions.None;
            
            private bool _isChangeOk, _isChangeOkCancel, _isChangeYesNoCancel, _isChangeYesNo;
            private bool _isChangeCancel, _isChangeYes, _isChangeNo;
            
            /// <summary>
            /// 设置提示内容
            /// </summary>
            /// <param name="messageBoxText">弹框提示内容</param>
            public MessageBoxBuilder(string messageBoxText) {
                this.messageBoxText = messageBoxText;
            }
            /// <summary>
            /// 设置提示内容
            /// </summary>
            /// <param name="owner">不要传null</param>
            /// <param name="messageBoxText">弹框提示内容</param>
            public MessageBoxBuilder(Window owner, string messageBoxText) {
                this.owner = owner;
                this.messageBoxText = messageBoxText;
            }
            
            /// <summary>
            /// 设置标题
            /// </summary>
            /// <param name="caption">弹框顶部标题</param>
            /// <returns></returns>
            public MessageBoxBuilder SetCaption(string caption) {
                this.caption = caption;
                return this;
            }
            
            /// <summary>
            /// 设置按钮
            /// </summary>
            /// <param name="button"><see cref="T:System.Windows.MessageBoxButton" /> 按钮类型, 例: <see cref="F:System.Windows.MessageBoxButton.YesNo">MessageBoxButton.YesNo</see></param>
            /// <returns></returns>
            public MessageBoxBuilder SetButton(MessageBoxButton button) {
                this.button = button;
                return this;
            }
            
            /// <summary>
            /// 设置按钮"Ok"
            /// </summary>
            /// <param name="buttonOk">给"Ok"按钮设置显示内容</param>
            /// <returns></returns>
            public MessageBoxBuilder SetButtonOk(string buttonOk) {
                // this.button = MessageBoxButton.OK;
                try {
                    // 动态设置临时按钮文本
                    Application.Current.Resources["MessageBox.Ok"] = buttonOk;
                    _isChangeOk = true;
                } catch (Exception e) {
                    Console.WriteLine(e);
                    // throw;
                } finally {
                    // 恢复默认按钮文本
                    Application.Current.Resources["MessageBox.Ok"] = DefaultOk;
                }
                return this;
            }

            /// <summary>
            /// 设置按钮"Ok" and "Cancel"
            /// </summary>
            /// <param name="buttonOk">给"Ok"按钮设置显示内容</param>
            /// <param name="buttonCancel">给"Cancel"按钮设置显示内容</param>
            /// <returns></returns>
            // public MessageBoxBuilder SetButtonCancel(string buttonOk, string buttonCancel) {
            public MessageBoxBuilder SetButtonCancel(string buttonCancel) {
                // this.button = MessageBoxButton.OKCancel;
                try {
                    // Application.Current.Resources["MessageBox.Ok"] = buttonOk;
                    // _isChangeOkCancel = true;
                    _isChangeCancel = true;
                    Application.Current.Resources["MessageBox.Cancel"] = buttonCancel;
                } catch (Exception e) {
                    Console.WriteLine(e);
                    // throw;
                } finally {
                    // Application.Current.Resources["MessageBox.Ok"] = DefaultOk;
                    Application.Current.Resources["MessageBox.Cancel"] = DefaultCancel;
                }
                return this;
            }

            /// <summary>
            /// 设置按钮"Yes" and "No" and "Cancel"
            /// </summary>
            /// <param name="Yes">给"Yes"按钮设置显示内容</param>
            /// <param name="buttonNo">给"No"按钮设置显示内容</param>
            /// <param name="buttonCancel">给"Cancel"按钮设置显示内容</param>
            /// <returns></returns>
            // public MessageBoxBuilder SetButtonYesNoCancel(string buttonYes, string buttonNo, string buttonCancel) {
            public MessageBoxBuilder SetButtonYes(string buttonYes) {
                // this.button = MessageBoxButton.YesNoCancel;
                try {
                    Application.Current.Resources["MessageBox.Yes"] = buttonYes;
                    // _isChangeYesNoCancel = true;
                    _isChangeYes = true;
                    // Application.Current.Resources["MessageBox.No"] = buttonNo;
                    // Application.Current.Resources["MessageBox.Cancel"] = buttonCancel;
                } catch (Exception e) {
                    Console.WriteLine(e);
                    // throw;
                } finally {
                    Application.Current.Resources["MessageBox.Yes"] = DefaultYes;
                    // Application.Current.Resources["MessageBox.No"] = DefaultNo;
                    // Application.Current.Resources["MessageBox.Cancel"] = DefaultCancel;
                }
                return this;
            }

            /// <summary>
            /// 设置按钮"Yes" and "No"
            /// </summary>
            /// <param name="Yes">给"Yes"按钮设置显示内容</param>
            /// <param name="buttonNo">给"No"按钮设置显示内容</param>
            /// <returns></returns>
            // public MessageBoxBuilder SetButtonYesNo(string buttonYes, string buttonNo) {
            public MessageBoxBuilder SetButtonNo(string buttonNo) {
                // this.button = MessageBoxButton.YesNo;
                try {
                    // Application.Current.Resources["MessageBox.Yes"] = buttonYes;
                    // _isChangeYesNo = true;
                    Application.Current.Resources["MessageBox.No"] = buttonNo;
                    _isChangeNo = true;
                } catch (Exception e) {
                    Console.WriteLine(e);
                    // throw;
                } finally {
                    // Application.Current.Resources["MessageBox.Yes"] = DefaultYes;
                    Application.Current.Resources["MessageBox.No"] = DefaultNo;
                }
                return this;
            }



            /// <summary>
            /// 设置图标
            /// </summary>
            /// <param name="icon"><see cref="T:System.Windows.MessageBoxImage" /> 弹框图标, 例: <br />
            ///     <see cref="F:System.Windows.MessageBoxImage.None">MessageBoxImage.None</see> (默认) <br />
            ///     <see cref="F:System.Windows.MessageBoxImage.Error">MessageBoxImage.Error</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxImage.Hand">MessageBoxImage.Hand</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxImage.Stop">MessageBoxImage.Stop</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxImage.Question">MessageBoxImage.Question</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxImage.Exclamation">MessageBoxImage.Exclamation</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxImage.Warning">MessageBoxImage.Warning</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxImage.Asterisk">MessageBoxImage.Asterisk</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxImage.Information">MessageBoxImage.Information</see> <br />
            /// </param>
            /// <returns></returns>
            public MessageBoxBuilder SetIcon(MessageBoxImage icon) {
                this.icon = icon;
                return this;
            }

            /// <summary>
            /// 设置默认按钮, if用户按下回车键，默认按钮就会被点击<br />
            /// 经测试: if只有1个按钮, 就算调用本方法设置了, 按Enter or 点击↗角 ❌️ 还是会触发MessageBoxResult.OK, 无语...
            /// </summary>
            /// <param name="defaultResult"><see cref="T:System.Windows.MessageBoxResult" /> 默认按钮, 例: <br />
            ///     <see cref="F:System.Windows.MessageBoxResult.None">MessageBoxResult.None</see> (默认) <br />
            ///     <see cref="F:System.Windows.MessageBoxResult.OK">MessageBoxResult.OK</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxResult.Cancel">MessageBoxResult.Cancel</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxResult.Yes">MessageBoxResult.Yes</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxResult.No">MessageBoxResult.No</see>
            /// </param>
            /// <returns></returns>
            public MessageBoxBuilder SetDefaultResult(MessageBoxResult defaultResult) {
                this.defaultResult = defaultResult;
                return this;
            }

            /// <summary>
            /// 设置消息选项
            /// </summary>
            /// <param name="options"><see cref="T:System.Windows.MessageBoxOptions" /> 消息选项, 例: <br />
            ///     <see cref="F:System.Windows.MessageBoxOptions.None">MessageBoxOptions.None</see> (默认) <br />
            ///     <see cref="F:System.Windows.MessageBoxOptions.ServiceNotification">MessageBoxOptions.ServiceNotification</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxOptions.DefaultDesktopOnly">MessageBoxOptions.DefaultDesktopOnly</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxOptions.RightAlign">MessageBoxOptions.RightAlign</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxOptions.RtlReading">MessageBoxOptions.RtlReading</see>
            /// </param>
            /// <returns></returns>
            public MessageBoxBuilder SetOptions(MessageBoxOptions options) {
                this.options = options;
                return this;
            }

            /// <summary>
            /// Show()
            /// </summary>
            /// <returns>返回被点击的按钮, 例: <br />
            ///     <see cref="F:System.Windows.MessageBoxResult.None">MessageBoxResult.None</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxResult.OK">MessageBoxResult.OK</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxResult.Cancel">MessageBoxResult.Cancel</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxResult.Yes">MessageBoxResult.Yes</see> <br />
            ///     <see cref="F:System.Windows.MessageBoxResult.No">MessageBoxResult.No</see>
            /// </returns>
            public MessageBoxResult Show() {
                MessageBoxResult result;
                if (owner == null) {
                    result = MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);
                } else {
                    result = MessageBox.Show(owner, messageBoxText, caption, button, icon, defaultResult, options);
                }

                //_isChangeOk, _isChangeOkCancel, _isChangeYesNoCancel, _isChangeYesNo
                // if (_isChangeOk) {
                //     Application.Current.Resources["MessageBox.Ok"] = DefaultOk;
                // } else if (_isChangeOkCancel) {
                //     Application.Current.Resources["MessageBox.Ok"] = DefaultOk;
                //     Application.Current.Resources["MessageBox.Cancel"] = DefaultCancel;
                // } else if (_isChangeYesNoCancel) {
                //     Application.Current.Resources["MessageBox.Cancel"] = DefaultCancel;
                //     Application.Current.Resources["MessageBox.Yes"] = DefaultYes;
                //     Application.Current.Resources["MessageBox.No"] = DefaultNo;
                // } else if (_isChangeYesNo) {
                //     Application.Current.Resources["MessageBox.Yes"] = DefaultYes;
                //     Application.Current.Resources["MessageBox.No"] = DefaultNo;
                // }
                
                //_isChangeOk, _isChangeCancel, _isChangeYes, _isChangeNo
                if (_isChangeOk) {
                    Application.Current.Resources["MessageBox.Ok"] = DefaultOk;
                }
                if (_isChangeCancel) {
                    Application.Current.Resources["MessageBox.Cancel"] = DefaultCancel;
                }
                if (_isChangeYes) {
                    Application.Current.Resources["MessageBox.Yes"] = DefaultYes;
                }
                if (_isChangeNo) {
                    Application.Current.Resources["MessageBox.No"] = DefaultNo;
                }
                return result;
            }
        }
    }
}