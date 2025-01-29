/**
 * /**
 * WPF 使用 Console.Writeline()无法在输出窗口输出的解决方法
 * 方法1：将 Console.Writeline 更改为 System.Diagnostics.Debug.WriteLine()
 *        将 Console.Writeline 更改为 System.Diagnostics.Debug.Print()
 * 方法2：将 Console.Writeline 更改为 System.Diagnostics.Trace.WriteLine()
 *
 * 
 * 方法3: 日志库，如NLog、log4net, serilog或者.NET Core自带的Microsoft.Extensions.Logging。
 *
 * 方法4: https://www.jianshu.com/p/443d4f749101  WPF：将调试信息输出到控制台 - 简书
 *       使用API函数：AttachConsole、AllocConsole和FreeConsole
 *
 * 
 * https://www.cnblogs.com/lymazj/p/17253018.html
 * .net core下的wpf项目 把框架改成了net framework4.8 之后，使用Console.WriteLine 的值不会输出到VS的Output窗口。
 *
 * 将 Console.WriteLine() 自动替换成 Trace.WriteLine()
 *
 * 使用: 在App中初始化: Console.SetOut(new TraceTextWriter());
 *
 *
 * Others:
 * //Console.BackgroundColor = ConsoleColor.Blue; //设置背景色
 * Console.ForegroundColor = ConsoleColor.White; //设置前景色，即字体颜色
 * Console.WriteLine("第一行白蓝.");
 * Console.ResetColor(); //将控制台的前景色和背景色设为默认值
 * //Console.BackgroundColor = ConsoleColor.Green;
 * Console.ForegroundColor = ConsoleColor.DarkGreen;
 * string value = "第三行 绿暗绿";
 * Console.Write(value); //设置一整行的背景色
 * Console.ForegroundColor = ConsoleColor.Red;
 * value = "红色部分";
 * Console.WriteLine(value.PadRight(Console.WindowWidth - value.Length)); //设置一整行的背景色
 * Console.ReadKey();
 */

using System.Diagnostics;
using System.IO;
using System.Text;

namespace BoneTownHelperApplication.Utils {
    public class TraceTextWriter: TextWriter {
        
        public override Encoding Encoding => Encoding.UTF8;
        
        public override void WriteLine(string value) {
            Trace.WriteLine(value);
            base.WriteLine(value);
        }
    }
}