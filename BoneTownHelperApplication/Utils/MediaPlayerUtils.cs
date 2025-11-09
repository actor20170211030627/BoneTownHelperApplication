using System;
using System.Windows.Media;

namespace BoneTownHelperApplication.Utils {
    
    /// 特性           SoundPlayer                 MediaPlayer                           MediaElement
    /// 推荐场景        播放简短的系统提示音或音效    在后台代码中灵活控制音频播放            在XAML界面中集成并控制音频
    /// 支持格式        仅 .wav                     多种格式 (依赖Windows Media Player)   多种格式 (依赖Windows Media Player) 
    /// 多音频同时播放  ❌ 不支持                    ✅ 支持                              ✅ 支持 
    /// 音量控制       ❌ 不支持                    ✅ 支持                              ✅ 支持 
    /// 主要优势       使用简单，无需复杂设置         功能全面，支持暂停、进度控制等          可直接在XAML中使用，支持故事板动画联动
    public static class MediaPlayerUtils {

        /// <summary>
        /// 媒体打开监听, 可在媒体文件成功打开后，开始播放, 例: mediaPlayer.Play();
        /// </summary>
        /// <param name="mediaPlayer"></param>
        /// <param name="eventHandler"></param>
        public static void MediaOpenedAdd(MediaPlayer mediaPlayer, EventHandler eventHandler) {
            mediaPlayer.MediaOpened += eventHandler;
        }
        
        public static void MediaOpenedMinus(MediaPlayer mediaPlayer, EventHandler eventHandler) {
            mediaPlayer.MediaOpened -= eventHandler;
        }
        
        
        /// <summary>
        /// 媒体打开失败监听, 可打印错误信息, 例:
        /// Console.WriteLine($"Media failed: {e.ErrorException.Message}");
        /// </summary>
        /// <param name="mediaPlayer"></param>
        /// <param name="eventHandler"></param>
        public static void MediaFailedAdd(MediaPlayer mediaPlayer, EventHandler<ExceptionEventArgs> eventHandler) {
            mediaPlayer.MediaFailed += eventHandler;
        }
        
        public static void MediaFailedMinus(MediaPlayer mediaPlayer, EventHandler<ExceptionEventArgs> eventHandler) {
            mediaPlayer.MediaFailed -= eventHandler;
        }

        /// <summary>
        /// 打开媒体
        /// </summary>
        /// <param name="mediaPlayer"></param>
        /// <param name="uri"> 媒体文件, Resource vs Content 的区别： <br />
        /// 设置		行为											对MediaPlayer的影响 <br />
        /// Resource	文件被嵌入到程序集内部，编译后不存在于独立文件中	MediaPlayer无法直接访问嵌入的资源文件 <br />
        /// Content		文件保持为独立文件，被复制到输出目录				MediaPlayer可以直接访问磁盘上的文件 <br />
        /// <br />
        /// Uri uri = new Uri("E:\\musics/ang.wav"); //测试绝对路径 <br />
        /// <br />
        /// 1.if文件属性设置为 Content: <br />
        /// <tab /> 方式1: 相对路径 (简单常用) <br />
        /// Uri uri = new Uri("Resources/Medias/xxx.wav", UriKind.Relative); <br />
        /// 方式2: 基于站点源的Pack URI (效果同上) <br />
        /// Uri uri = new Uri("pack://siteoforigin:,,,/Resources/Medias/xxx.wav"); <br />
        /// <br />
        /// 2.if文件属性设置为Resource, 自己Copy到外部目录然后再播放??
        /// </param>
        
        // 当文件是Resource的时候:
        // 报错: 对于媒体仅支持源站点包 URI。
        // Uri uri = new Uri("pack://application:,,,/Resources/Medias/ang.wav");
        // 报错: 对于媒体仅支持源站点包 URI。
        // Uri uri = new Uri("pack://application:,,,/Medias/ang.wav");
        // 报错: 找不到媒体文件。
        // Uri uri = new Uri("ang.wav", UriKind.RelativeOrAbsolute);
        // 报错: 对于媒体仅支持源站点包 URI。
        // Uri uri = new Uri("pack://application:,,,/Medias/ang.wav", UriKind.RelativeOrAbsolute);

        //;component/ 是引用组件资源的固定语法。报错: 对于媒体仅支持源站点包 URI。
        // Uri uri = new Uri($"pack://application:,,,{assemblyName};component/Resources/Medias/ang.wav", UriKind.Absolute);
        //或者使用相对URI。 报错: 找不到媒体文件。
        // Uri uri = new Uri($"/{assemblyName};component/Resources/Medias/ang.wav", UriKind.Relative);

        public static void Open(MediaPlayer mediaPlayer, Uri uri) {
            mediaPlayer.Open(uri);
        }


        /// <summary>
        /// 设置声音大小
        /// </summary>
        /// <param name="mediaPlayer"></param>
        /// <param name="volume">取值范围: 0.0~1.0, 默认0.5</param>
        public static void SetVolume(MediaPlayer mediaPlayer, double volume) {
            mediaPlayer.Volume = volume;
        }
        
        /// <summary>
        /// 设置进度
        /// </summary>
        /// <param name="mediaPlayer"></param>
        /// <param name="position">例: TimeSpan.Zero, TimeSpan.FromSeconds(10), MediaElement.Position + TimeSpan.FromSeconds(10), MediaElement.Position - TimeSpan.FromSeconds(10)</param>
        public static void Position(MediaPlayer mediaPlayer, TimeSpan position) {
            mediaPlayer.Position = position;
        }

        /// <summary>
        /// 获取媒体时长
        /// </summary>
        /// <param name="mediaPlayer"></param>
        /// <returns></returns>
        public static TimeSpan GetDuration(MediaPlayer mediaPlayer) {
            if (mediaPlayer.NaturalDuration.HasTimeSpan) return mediaPlayer.NaturalDuration.TimeSpan;
            return TimeSpan.MinValue;
        }
        
        /// <summary>
        /// 开始播放
        /// </summary>
        /// <param name="mediaPlayer"></param>
        public static void Play(MediaPlayer mediaPlayer) {
            mediaPlayer.Play();
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        /// <param name="mediaPlayer"></param>
        public static void Pause(MediaPlayer mediaPlayer) {
            mediaPlayer.Pause();
        }

        /// <summary>
        /// 是否正在播放中, 原生居然没有 isPlaying 方法... <br />
        /// NaturalDuration: 获取介质的自然持续时间。也就是视频播放总时长。 <br />
        /// HasTimeSpan == true：表示媒体有明确的时长，可以通过 NaturalDuration.TimeSpan 获取具体值。 <br />
        /// HasTimeSpan == false：表示媒体时长不确定（例如直播流），此时访问 NaturalDuration.TimeSpan 会抛出异常。 <br />
        ///
        /// </summary>
        /// <param name="mediaPlayer"></param>
        /// <returns></returns>
        public static bool IsPlayFinished(MediaPlayer mediaPlayer) {
            return mediaPlayer.NaturalDuration.HasTimeSpan &&
                   mediaPlayer.Position >= mediaPlayer.NaturalDuration.TimeSpan;
        }
        
        /// <summary>
        /// 停止播放
        /// </summary>
        /// <param name="mediaPlayer"></param>
        public static void Stop(MediaPlayer mediaPlayer) {
            mediaPlayer.Stop();
        }
        
        /// <summary>
        /// 关闭播放器, 释放资源
        /// </summary>
        /// <param name="mediaPlayer"></param>
        public static void Close(MediaPlayer mediaPlayer) {
            mediaPlayer.Close();
        }
    }
}