using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 图像压缩.MODE
{
    internal class DisplayOutput
    {
        /// <summary>
        /// 控制台显示增强
        /// </summary>
        public class ConsoleEnhanced
        {
            /// <summary>
            /// 控制台换行显示字符并指定字符颜色
            /// </summary>
            /// <param name="value">字符</param>
            /// <param name="ForeConsoleColor">字体颜色</param>
            public static void WriteLine(string value , ConsoleColor ForeConsoleColor)
            {
                ConsoleColor TempForeConsoleColor = Console.ForegroundColor;//暂存字体颜色
                Console.ForegroundColor = ForeConsoleColor;//设置字体颜色
                Console.WriteLine(value);
                Console.ForegroundColor = TempForeConsoleColor;//恢复字体颜色
            }
            /// <summary>
            /// 控制台换行显示字符并指定字符和背景颜色
            /// </summary>
            /// <param name="value">字符</param>
            /// <param name="ForeConsoleColor">字体颜色</param>
            /// <param name="BackConsoleColor">背景颜色</param>
            public static void WriteLine(string value, ConsoleColor ForeConsoleColor, ConsoleColor BackConsoleColor)
            {
                ConsoleColor TempForeConsoleColor = Console.ForegroundColor;//暂存字体颜色
                ConsoleColor TempBackConsoleColor = Console.BackgroundColor;//暂存背景颜色
                Console.ForegroundColor = ForeConsoleColor;//设置字体颜色
                Console.BackgroundColor = BackConsoleColor;//设置背景颜色
                Console.WriteLine(value);
                Console.ForegroundColor = TempForeConsoleColor;//恢复字体颜色
                Console.BackgroundColor = TempBackConsoleColor;//恢复背景颜色
            }
            /// <summary>
            /// 控制台显示字符并指定字符颜色
            /// </summary>
            /// <param name="value">字符</param>
            /// <param name="ForeConsoleColor">字体颜色</param>
            public static void Write(string value, ConsoleColor ForeConsoleColor)
            {
                ConsoleColor TempForeConsoleColor = Console.ForegroundColor;//暂存字体颜色
                Console.ForegroundColor = ForeConsoleColor;//设置字体颜色
                Console.Write(value);
                Console.ForegroundColor = TempForeConsoleColor;//恢复字体颜色
            }
            /// <summary>
            /// 控制台显示字符并指定字符和背景颜色
            /// </summary>
            /// <param name="value">字符</param>
            /// <param name="ForeConsoleColor">字体颜色</param>
            /// <param name="BackConsoleColor">背景颜色</param>
            public static void Write(string value, ConsoleColor ForeConsoleColor, ConsoleColor BackConsoleColor)
            {
                ConsoleColor TempForeConsoleColor = Console.ForegroundColor;//暂存字体颜色
                ConsoleColor TempBackConsoleColor = Console.BackgroundColor;//暂存背景颜色
                Console.ForegroundColor = ForeConsoleColor;//设置字体颜色
                Console.BackgroundColor = BackConsoleColor;//设置背景颜色
                Console.Write(value);
                Console.ForegroundColor = TempForeConsoleColor;//恢复字体颜色
                Console.BackgroundColor = TempBackConsoleColor;//恢复背景颜色
            }
        }
    }
}
