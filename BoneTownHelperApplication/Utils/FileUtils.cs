using System.Diagnostics;
using System.IO;

namespace BoneTownHelperApplication.Utils {
    
    /// <summary>
    /// 文件工具类
    /// </summary>
    public static class FileUtils {

        /// <summary>打开文件所在目录, or 打开文件夹
        /// </summary>
        /// <param name="path">文件/文件夹路径, 例eg:  D:\XxxFolder or D:\XxxFolder\abc.txt</param>
        /// <param name="isSelectFile">打开文件夹后, 是否选中path指定的文件</param>
        /// <returns></returns>
        public static bool OpenFolder(string path, bool isSelectFile) {
            if (string.IsNullOrEmpty(path)) return false;
            // 规范化路径：将正斜杠替换为反斜杠，确保 Windows 路径格式, 这会自动处理分隔符并解析绝对路径
            path = Path.GetFullPath(path);
            if (File.Exists(path)) {
                if (isSelectFile) {
                    // 参数需要用引号括起来，以处理路径中的空格
                    Process.Start("explorer.exe", $"/select, \"{path}\"");
                    return true;
                }
                path = Path.GetDirectoryName(path);
            }
            if (!Directory.Exists(path)) return false;
            // Process.Start(folder);  // 等同于双击该文件夹，会打开资源管理器
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            return true;
        }

        /// <summary>获取文件所在路径, or 直接返回传入的文件夹路径
        /// </summary>
        /// <param name="path">文件/文件夹路径, 例eg:  D:\XxxFolder or D:\XxxFolder\abc.txt</param>
        /// <returns>eg: D:\XxxFolder</returns>
        public static string GetDirectoryName(string path) {
            if (string.IsNullOrEmpty(path)) return null;
            path = Path.GetFullPath(path);
            // if 是目录, 直接返回, 否则返回可能存在的上一级目录
            return Directory.Exists(path) ? path : Path.GetDirectoryName(path);
        }
    }
}