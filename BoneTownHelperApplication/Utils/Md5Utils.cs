using System;
using System.IO;
using System.Security.Cryptography;

namespace BoneTownHelperApplication.Utils {
    
    public static class Md5Utils {
        
        /// <summary>
        /// 计算文件的MD5哈希值
        /// </summary>
        /// <param name="filePath">文件完整路径</param>
        /// <returns>MD5哈希值（32位小写字符串）, 文件不存在返回null</returns>
        public static string CalculateFileMd5(string filePath) {
            // 验证文件是否存在
            if (!File.Exists(filePath)) {
                Console.WriteLine($"文件不存在: {filePath}");
                return null;
            }

            // 使用using语句自动释放资源
            using (MD5 md5 = MD5.Create()) {
                using (FileStream stream = File.OpenRead(filePath)) {
                    // 计算文件的MD5哈希值
                    byte[] hashBytes = md5.ComputeHash(stream);
                    
                    // 将字节数组转换为32位十六进制字符串
                    return BitConverter.ToString(hashBytes)
                        .Replace("-", "")
                        .ToLowerInvariant();
                }
            }
        }
    }
}