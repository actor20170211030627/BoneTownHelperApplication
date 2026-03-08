using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace BoneTownHelperApplication.Utils {
    
    public static class ProcessUtils {

        // 导入Windows API所需的函数和常量
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool QueryFullProcessImageName(IntPtr hProcess, uint dwFlags, StringBuilder lpExeName, ref uint lpdwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        // 进程访问权限常量
        private const uint PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;



        /// <summary>
        /// 根据Process名称, 返回Process路径
        /// </summary>
        /// <param name="processName">例Chrome, 不要写成Chrome.exe, 没找到就返回null</param>
        /// <returns></returns>
        public static string GetProcessPath(string processName) {
            // 1. 查找指定进程
            Process[] targetProcesses = Process.GetProcessesByName(processName);
                
            //未找到进程！
            if (targetProcesses.Length == 0) return null;

            // 2. 获取第一个匹配进程的主模块文件路径
            Process targetProcess = targetProcesses[0];
            
            //报错❌️: System.ComponentModel.Win32Exception: 32 位进程无法访问 64 位进程的模块。
            // ProcessModule processModule = targetProcess.MainModule;
            // if (processModule == null) return null;
            //         
            // try {
            //     // 获取进程主模块路径（需要管理员权限才能访问某些进程）
            //     string exePath = processModule.FileName;
            //     return exePath;
            // } catch (Exception ex) {
            //     Console.WriteLine($"获取进程路径失败：{ex.Message}\n请以管理员身份运行程序");
            //     return null;
            // }
            
            return GetProcessPath2(targetProcess.Id);
        }
        
        /// <summary>
        /// 兼容32/64位的进程路径获取方法
        /// </summary>
        /// <param name="processId">进程ID</param>
        /// <returns>进程文件完整路径</returns>
        public static string GetProcessPath2(int processId) {
            // 打开进程（获取有限查询权限）
            IntPtr hProcess = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, processId);
            if (hProcess == IntPtr.Zero) {
                // throw new System.ComponentModel.Win32Exception();
                Console.WriteLine("打开进程失败!");
                return null;
            }

            try {
                // 初始化字符串缓冲区
                uint bufferSize = 1024;
                StringBuilder exeName = new StringBuilder((int) bufferSize);

                // 查询进程完整路径
                if (QueryFullProcessImageName(hProcess, 0, exeName, ref bufferSize)) {
                    return exeName.ToString();
                } else {
                    // throw new System.ComponentModel.Win32Exception();
                    Console.WriteLine("查询进程完整路径失败!");
                }
            } finally {
                // 释放进程句柄
                CloseHandle(hProcess);
            }
            return null;
        }
    }
}