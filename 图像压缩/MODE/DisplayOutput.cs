using System;

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
            public static void WriteLine(string value, ConsoleColor ForeConsoleColor)
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

            public class ProgressBar
            {
                private static string lastContext = "";//用于记录上次写的内容
                private static readonly object _lock = new object();//加锁保证只有一个输出
                public static void Write(string context)
                {
                    lastContext = context;//记录上次写的内容
                    lock (_lock)
                    {
                        Console.Write(context);
                    }

                }
                /// <summary>
                /// 覆写
                /// </summary>
                /// <param name="context"></param>
                public static void OverWrite(string context = null)
                {
                    lastContext = context;//记录上次写的内容
                    var strLen = context?.Length ?? 0;
                    //空白格的长度，考虑到内容可能超出一行的宽度，所以求余。
                    var blankLen = Console.WindowWidth - strLen % Console.WindowWidth - 1;
                    var rowCount = strLen / Console.WindowWidth;
                    Console.SetCursorPosition(0, Console.CursorTop - rowCount);
                    //空白只需填充最后一行的剩余位置即可。
                    lock (_lock)
                    {
                        Console.Write(context + new string(' ', blankLen));
                    }
                }

                public static void WriteLine(string context = null)
                {
                    ClearConsoleLine();//清除最后一行
                    lock (_lock)
                    {
                        Console.WriteLine(context);
                        if (!string.IsNullOrWhiteSpace(lastContext))
                            Console.Write(lastContext);//重新输出最后一次的内容，否则有较明显的闪烁
                        lastContext = null;
                    }
                }

                public static void ClearConsoleLine(int invertedIndex = 0)
                {
                    int currentLineCursor = Console.CursorTop;
                    int top = Console.CursorTop - invertedIndex;
                    top = top < 0 ? 0 : top;
                    Console.SetCursorPosition(0, top);
                    Console.Write(new string(' ', Console.WindowWidth - 1));
                    Console.SetCursorPosition(0, currentLineCursor);
                }
            }
        }
    }
}
