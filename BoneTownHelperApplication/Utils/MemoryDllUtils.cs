using System;
using System.Diagnostics;
using System.Linq;
using Memory;

/// <summary>
/// https://github.com/erfg12/memory.dll
/// 32 位进程无法访问 64 位进程的模块。 
/// </summary>
namespace BoneTownHelperApplication.Utils {
    public class MemoryDllUtils {

        //进程id
        protected static int ProgressId = -1;

        /// <summary>打印所有进程</summary>
        public static void printProcesses() {
            // 获取系统中所有运行的进程
            Process[] processes = Process.GetProcesses();
            // 遍历并打印进程信息
            foreach (Process process in processes) {
                Console.WriteLine($"进程ID: {process.Id}, 进程名称: {process.ProcessName}");
            }
        }

        /// <summary>查找进程</summary>
        /// <param name="processName">进程名称, 例: wps</param>
        /// <returns></returns>
        public static Process findProcess(string processName) {
            // 获取目标进程
            Process targetProcess = Process.GetProcessesByName(processName).FirstOrDefault();
            if (targetProcess == null) {
                ProgressId = -1;
                Console.WriteLine("目标进程未找到！");
            } else {
                ProgressId = targetProcess.Id;
            }
            return targetProcess;
            
                    
            // int processId = -1;
            // Process[] processes = Process.GetProcessesByName(processName);
            // if (processes.Length > 0) {
            //     processId = processes[0].Id;
            // }
            // byte[] buffer = new byte[4];
            // try {
            //     int PROCESS_ALL_ACCESS = 0x1F0FFF;
            //     //获取缓冲区地址 :固定数组元素的不安全地址
            //     IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            //     //获取进程的最高权限
            //     IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, processId);
            //     //将指定内存中的值读入缓冲区 
            //     ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, 4, IntPtr.Zero);
            //     CloseHandle(hProcess);
            //     memoryValue = Marshal.ReadInt32(byteAddress);
            //     return true;
            // }
            // catch (Exception ex) {
            //     Console.WriteLine($"读取内存出错:{ex.Message}");
            //     return false;
            // }
            // return null;
        }

        /// <summary>
        /// 读内存值int
        /// </summary>
        /// <param name="moduleName">module名称, 例: wps.exe, xxx.dll</param>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <returns></returns>
        public static int readInt(string code) {
            // 创建 Memory.dll 实例
            Mem memory = new Mem();
            bool openSuccess = memory.OpenProcess(ProgressId);
            if (!openSuccess) {
                // 关闭进程句柄
                memory.CloseProcess();
                Console.WriteLine("打开进程失败!");
                return -1;
            }
            
            // 获取主模块的基地址
            // IntPtr baseAddress = targetProcess.MainModule.BaseAddress;
            // Console.WriteLine($"基地址: {baseAddress.ToString("X")}");
            
            // 获取模块基地址
            // IntPtr baseAddress = memory.GetModuleAddressByName(moduleName);
            // Console.WriteLine($"基地址: 0x{baseAddress.ToString("X")}");
            
            //  address 是内存地址，可以是十六进制字符串（如 "0x12345678"）或十进制值。
            //gamedll_x64_rwdi.dll+0x00532A28,0x2B8,0x478
            int value = memory.ReadInt(code);
            Console.WriteLine($"读取到的值: {value}");
            
            //如果有多层偏移量（指针链），可以使用 memory.dll 的 GetPointerAddress 方法：
            // int[] offsets = { 0x2B8, 0x478 }; // 多层偏移量
            // int finalAddress = memory.GetPointerAddress(baseAddress.ToString("X"), offsets);
            
            // 关闭进程句柄
            memory.CloseProcess();
            return value;

            // 修改内存值
            // memory.WriteMemory(baseAddress.ToString("X"), "int", "999");
            // Console.WriteLine("内存值已修改！");
        }
        
        /// <summary>
        /// 将int写入内存
        /// </summary>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <param name="type">byte, 2bytes, bytes, float, int, string, double or long.</param>
        /// <param name="value">value to write to address.</param>
        /// <returns></returns>
        public static bool writeInt(string code, int value) {
            // 创建 Memory.dll 实例
            Mem memory = new Mem();
            bool openSuccess = memory.OpenProcess(ProgressId);
            if (!openSuccess) {
                // 关闭进程句柄
                memory.CloseProcess();
                Console.WriteLine("打开进程失败!");
                return false;
            }
            
            //  address 是内存地址，可以是十六进制字符串（如 "0x12345678"）或十进制值。
            //gamedll_x64_rwdi.dll+0x00532A28,0x2B8,0x478
            bool success = memory.WriteMemory(code, "int", value.ToString());
            Console.WriteLine($"写入int: {value}, 成功: {success}");
            
            // 关闭进程句柄
            memory.CloseProcess();
            return success;
        }
    }
}