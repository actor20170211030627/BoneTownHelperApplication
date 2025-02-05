using System;
using System.Diagnostics;
using System.Linq;
using Memory;

/// <summary>
/// https://github.com/erfg12/memory.dll
/// 32 位进程无法访问 64 位进程的模块。 
/// </summary>
namespace BoneTownHelperApplication.Utils {
    public static class MemoryDllUtils {

        //进程id
        // internal static int ProgressId = -1;
        internal static Mem Memory = new Mem();

        /// <summary>打印所有进程</summary>
        public static void PrintProcesses() {
            // 获取系统中所有运行的进程
            Process[] processes = Process.GetProcesses();
            // 遍历并打印进程信息
            foreach (Process process in processes) {
                Console.WriteLine($"进程ID: {process.Id}, 进程名称: {process.ProcessName}");
            }
        }

        /// <summary>打开进程</summary>
        /// <param name="processName">进程名称, 例: wps</param>
        /// <returns></returns>
        public static bool OpenProcess(string processName) {
            bool openProcess = Memory.OpenProcess(processName);
            if (openProcess) {
                // ProgressId = targetProcess.Id;
            } else {
                // ProgressId = -1;
                Console.WriteLine("目标进程未找到！");
            }
            return openProcess;

            // // 获取目标进程
            // Process targetProcess = Process.GetProcessesByName(processName).FirstOrDefault();
            // if (targetProcess == null) {
            //     ProgressId = -1;
            //     Console.WriteLine("目标进程未找到！");
            // } else {
            //     ProgressId = targetProcess.Id;
            // }
            // return targetProcess;
        }

        /// <summary>
        /// 读内存值int
        /// </summary>
        /// <param name="moduleName">module名称, 例: wps.exe, xxx.dll</param>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <returns></returns>
        public static int ReadInt(string code) {
            // 创建 Memory.dll 实例
            // Mem memory = new Mem();
            // bool openSuccess = memory.OpenProcess(ProgressId);
            // if (!openSuccess) {
            //     // 关闭进程句柄
            //     memory.CloseProcess();
            //     Console.WriteLine("打开进程失败!");
            //     return -1;
            // }
            
            // 获取主模块的基地址
            // IntPtr baseAddress = targetProcess.MainModule.BaseAddress;
            // Console.WriteLine($"基地址: {baseAddress.ToString("X")}");
            
            // 获取模块基地址
            // IntPtr baseAddress = memory.GetModuleAddressByName(moduleName);
            // Console.WriteLine($"基地址: 0x{baseAddress.ToString("X")}");
            
            //  address 是内存地址，可以是十六进制字符串（如 "0x12345678"）或十进制值。
            //gamedll_x64_rwdi.dll+0x00532A28,0x2B8,0x478
            int value = Memory.ReadInt(code);
            Console.WriteLine($"读取到的值: {value}");
            
            //如果有多层偏移量（指针链），可以使用 memory.dll 的 GetPointerAddress 方法：
            // int[] offsets = { 0x2B8, 0x478 }; // 多层偏移量
            // int finalAddress = memory.GetPointerAddress(baseAddress.ToString("X"), offsets);
            
            // 关闭进程句柄
            Memory.CloseProcess();
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
        public static bool WriteInt(string code, int value) {
            // 创建 Memory.dll 实例
            // Mem memory = new Mem();
            // bool openSuccess = memory.OpenProcess(ProgressId);
            // if (!openSuccess) {
            //     // 关闭进程句柄
            //     memory.CloseProcess();
            //     Console.WriteLine("打开进程失败!");
            //     return false;
            // }
            
            //  address 是内存地址，可以是十六进制字符串（如 "0x12345678"）或十进制值。
            //gamedll_x64_rwdi.dll+0x00532A28,0x2B8,0x478
            bool success = Memory.WriteMemory(code, "int", value.ToString());
            Console.WriteLine($"写入int: {value}, 成功: {success}");
            
            // 关闭进程句柄
            Memory.CloseProcess();
            return success;
        }
        
        /// <summary>绑定地址值到UI, Bind memory addresses to UI elements</summary>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <param name="type">byte, 2bytes, bytes, float, int, string, double or long.</param>
        /// <param name="address">value to write to address.</param>
        /// <returns></returns>
        public static void BindToUI<T>(string address, Action<string> UIObject) {
            // Mem memory = new Mem();
            // bool openSuccess = memory.OpenProcess(ProgressId);
            // if (!openSuccess) {
            //     // 关闭进程句柄
            //     memory.CloseProcess();
            //     Console.WriteLine("打开进程失败!");
            //     return false;
            // }
            Memory.BindToUI<T>(address, UIObject);
        }
        
        /// <summary>冻结结果, Freeze values (infinte loop writing in threads)</summary>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <param name="type">byte, 2bytes, bytes, float, int, string, double or long.</param>
        /// <param name="value">value to write to address.</param>
        /// <returns></returns>
        public static bool FreezeValue(string address, string type, int value) {
            bool success = Memory.FreezeValue(address, type, value.ToString());
            Console.WriteLine($"冻结结果: {value}, 成功: {success}");
            Memory.CloseProcess();
            return success;
        }

        
        /// <summary>取消冻结结果, Unfreeze a frozen value at an address</summary>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <param name="type">byte, 2bytes, bytes, float, int, string, double or long.</param>
        /// <param name="value">value to write to address.</param>
        /// <returns></returns>
        public static void UnfreezeValue(string address) {
            Memory.UnfreezeValue(address);
        }

        public static void destroy() {
            Memory.CloseProcess();
            Memory = null;
        }
    }
}