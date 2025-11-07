using System;
using System.Diagnostics;
using Memory;

namespace BoneTownHelperApplication.Utils {

    /// <summary>
    /// https://github.com/erfg12/memory.dll
    /// 32 位进程无法访问 64 位进程的模块。 
    /// </summary>
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

        /// <summary>读内存值int</summary>
        /// <param name="moduleName">module名称, 例: wps.exe, xxx.dll</param>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <returns></returns>
        public static int ReadInt(string code) {
            //  address 是内存地址，可以是十六进制字符串（如 "0x12345678"）或十进制值。
            //gamedll_x64_rwdi.dll+0x00532A28,0x2B8,0x478
            return Memory.ReadInt(code);
        }

        /// <summary>将int写入内存</summary>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <param name="type">byte, 2bytes, bytes, float, int, string, double or long.</param>
        /// <param name="value">value to write to address.</param>
        /// <returns></returns>
        public static bool WriteInt(string code, int value) {
            return Memory.WriteMemory(code, "int", value.ToString());
        }

        /// <summary>读内存值float</summary>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <returns></returns>
        public static float ReadFloat(string code) {
            return Memory.ReadFloat(code, "", false);
        }

        /// <summary>将float写入内存</summary>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <param name="type">byte, 2bytes, bytes, float, int, string, double or long.</param>
        /// <param name="value">value to write to address.</param>
        /// <returns>是否写入成功</returns>
        public static bool WriteFloat(string code, float value) {
            return Memory.WriteMemory(code, "float", value.ToString());
        }

        /// <summary>读内存值byte</summary>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <returns></returns>
        public static int ReadByte(string code) {
            return Memory.ReadByte(code);
        }

        /// <summary>将byte写入内存</summary>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <param name="type">byte, 2bytes, bytes, float, int, string, double or long.</param>
        /// <param name="value">value to write to address.</param>
        /// <returns>是否写入成功</returns>
        public static bool WriteByte(string code, byte value) {
            return Memory.WriteMemory(code, "byte", value.ToString());
        }

        /// TODO: 为什么 BindToUI<float> 的值都=0???
        /// <summary>绑定地址值到UI, Bind memory addresses to UI elements</summary>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <param name="type">byte, 2bytes, bytes, float, int, string, double or long.</param>
        /// <param name="address">value to write to address.</param>
        /// <returns></returns>
        public static void BindToUI<T>(string address, Action<string> UIObject) {
            Memory.BindToUI<T>(address, UIObject);
        }

        /// <summary>冻结结果, Freeze values (infinte loop writing in threads)</summary>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <param name="type">byte, 2bytes, bytes, float, int, string, double or long.</param>
        /// <param name="value">value to write to address.</param>
        /// <returns></returns>
        public static bool FreezeValue(string address, string type, object value) {
            return Memory.FreezeValue(address, type, value.ToString());
        }


        /// <summary>取消冻结结果, Unfreeze a frozen value at an address</summary>
        /// <param name="code">address, module + pointer + offset, module + offset OR label in .ini file.</param>
        /// <param name="type">byte, 2bytes, bytes, float, int, string, double or long.</param>
        /// <param name="value">value to write to address.</param>
        /// <returns></returns>
        public static void UnfreezeValue(string address) {
            Memory.UnfreezeValue(address);
        }

        public static void CloseProcess() {
            // 关闭进程句柄
            Memory.CloseProcess();
            Memory = null;
        }
    }
}