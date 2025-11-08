using System;
using System.IO;
using System.Media;
using System.Windows.Resources;

namespace BoneTownHelperApplication.Utils {
    
    /// 特性           SoundPlayer                 MediaPlayer                           MediaElement
    /// 推荐场景        播放简短的系统提示音或音效    在后台代码中灵活控制音频播放            在XAML界面中集成并控制音频
    /// 支持格式        仅 .wav                     多种格式 (依赖Windows Media Player)   多种格式 (依赖Windows Media Player) 
    /// 多音频同时播放  ❌ 不支持                    ✅ 支持                              ✅ 支持 
    /// 音量控制       ❌ 不支持                    ✅ 支持                              ✅ 支持 
    /// 主要优势       使用简单，无需复杂设置         功能全面，支持暂停、进度控制等          可直接在XAML中使用，支持故事板动画联动
    public static class SoundPlayerUtils {
        
        /// <summary>
        /// 播放文件流
        /// </summary>
        /// <param name="soundPlayer"></param>
        /// <param name="uri">例Resource文件: new Uri("Resources/Medias/xxx.wav", UriKind.Relative);</param>
        public static void Stream(SoundPlayer soundPlayer, Uri uri) {
            StreamResourceInfo streamResourceInfo = System.Windows.Application.GetResourceStream(uri);
            if (streamResourceInfo == null) return;
            soundPlayer.Stream = streamResourceInfo.Stream;
        }
        
        /// <summary>
        /// 设置播放路径
        /// </summary>
        /// <param name="soundPlayer"></param>
        /// <param name="soundLocation">例: System.Environment.CurrentDirectory + "\Music\xxx.wav";</param>
        public static void SoundLocation(SoundPlayer soundPlayer, string soundLocation) {
            soundPlayer.SoundLocation = soundLocation;
        }
        
        /// <summary>
        /// 异步播放，不阻塞主线程
        /// </summary>
        /// <param name="soundPlayer"></param>
        public static void Play(SoundPlayer soundPlayer) {
            soundPlayer.Play();
        }
        
        /// <summary>
        /// 循环播放
        /// </summary>
        /// <param name="soundPlayer"></param>
        public static void PlayLooping(SoundPlayer soundPlayer) {
            soundPlayer.PlayLooping();
        }
        
        /// <summary>
        /// 同步播放（会阻塞当前线程，直到播放完成）
        /// </summary>
        /// <param name="soundPlayer"></param>
        public static void PlaySync(SoundPlayer soundPlayer) {
            soundPlayer.PlaySync();
        }
    }
}