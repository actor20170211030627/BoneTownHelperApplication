using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Memory;

namespace BoneTownHelperApplication.Utils {
    public class ProcessUtils {

        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;

        public static Process findProcess(string processName) {
            // // 获取系统中所有运行的进程
            // Process[] processes = Process.GetProcesses();
            // // 遍历并打印进程信息
            // foreach (Process process in processes) {
            //     Console.WriteLine($"进程ID: {process.Id}, 进程名称: {process.ProcessName}");
            // }

            
            // 获取目标进程
            Process targetProcess = Process.GetProcessesByName(processName).FirstOrDefault();
            if (targetProcess == null) {
                Console.WriteLine("目标进程未找到！");
                return null;
            }

            // 创建 Memory.dll 实例
            Mem memory = new Mem();
            memory.OpenProcess(targetProcess.Id);
            
            // 获取主模块的基地址
            // IntPtr baseAddress = targetProcess.MainModule.BaseAddress;
            // Console.WriteLine($"基地址: {baseAddress.ToString("X")}");
            
            // 获取模块基地址
            IntPtr baseAddress = memory.GetModuleAddressByName("notepad.exe");
            Console.WriteLine($"基地址: {baseAddress.ToString("X")}");
            
            // 读取内存值
            // int baseAddress = 0x12345678; // 替换为目标进程的实际基地址
            
            //  address 是内存地址，可以是十六进制字符串（如 "0x12345678"）或十进制值。
            int value = memory.ReadInt((baseAddress + 0x2B8).ToString("X"));
            Console.WriteLine($"读取到的值: {value}");
            
            //如果有多层偏移量（指针链），可以使用 memory.dll 的 GetPointerAddress 方法：
            // int[] offsets = { 0x2B8, 0x478 }; // 多层偏移量
            // int finalAddress = memory.GetPointerAddress(baseAddress.ToString("X"), offsets);

            // 修改内存值
            // memory.WriteMemory(baseAddress.ToString("X"), "int", "999");
            // Console.WriteLine("内存值已修改！");

            // 关闭进程句柄
            memory.CloseProcess(); 
            
                    
            // int processId = -1;
            // Process[] processes = Process.GetProcessesByName(processName);
            // if (processes.Length > 0) {
            //     processId = processes[0].Id;
            // }
            // byte[] buffer = new byte[4];
            // try
            // {
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
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"读取内存出错:{ex.Message}");
            //     return false;
            // }


            return null;
        }
        
        /// <summary>
        /// 打开进程，返回进程句柄
        /// </summary>
        /// <param name="dwDesiredAccess">渴望得到的访问权限(标志)，0x1F0FFF表示最高权限</param>
        /// <param name="bInheritHandle">是否继承句柄</param>
        /// <param name="dwProcessId">进程标示符</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        /// <summary>
        /// 读内存
        /// </summary>
        /// <param name="hProcess">远程进程句柄。 被读取者</param>
        /// <param name="lpBaseAddress">远程进程中内存地址。 从具体何处读取</param>
        /// <param name="lpBuffer">本地进程中内存地址. 函数将读取的内容写入此处 </param>
        /// <param name="nSize">要传送的字节数。要写入多少</param>
        /// <param name="lpNumberOfBytesRead">实际传送的字节数. 函数返回时报告实际写入多少</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr lpNumberOfBytesRead);
        
        /// <summary>
        /// 写内存
        /// </summary>
        /// <param name="hProcess">由OpenProcess返回的进程句柄</param>
        /// <param name="lpBaseAddress">要写的内存首地址</param>
        /// <param name="lpBuffer">指向要写的数据的指针</param>
        /// <param name="nSize">要写入的字节数</param>
        /// <param name="lpNumberOfBytesWritten">实际数据的长度</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, int[] lpBuffer, int nSize, IntPtr lpNumberOfBytesWritten);

        /// <summary>
        /// 关闭内核对象
        /// </summary>
        /// <param name="hObject">欲关闭的对象句柄</param>
        [DllImport("kernel32.dll")]
        private static extern void CloseHandle(IntPtr hObject);
    }
}