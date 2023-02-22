using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;
using static 图像压缩.MODE.DisplayOutput;
using static 图像压缩.MODE.ImageCompression;

namespace 图像压缩
{
    public class 主程序
    {
        public static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;//设置黑窗编码
            while (true)
            {
                string FilesPath = null;
                ConsoleEnhanced.WriteLine("请输入文件夹或文件路径", ConsoleColor.Yellow);
                FilesPath = Console.ReadLine();//输入文件路径

                string Format = null;
                ConsoleEnhanced.WriteLine("请输入目标格式", ConsoleColor.Yellow);
                Format = Console.ReadLine();//输入文件路径

                if (File.Exists(FilesPath))//文件
                {
                    ConsoleEnhanced.WriteLine("努力压缩中...", ConsoleColor.White);
                    string FilePathOut = Compression(FilesPath, $"{Path.GetDirectoryName(FilesPath)}/{Path.GetFileNameWithoutExtension(FilesPath)}.{Format}").Result;
                    if (FilePathOut != null)
                        ConsoleEnhanced.WriteLine($"完成!文件路径:{Path.GetFullPath(FilePathOut)}", ConsoleColor.Blue);
                }
                else if (Directory.Exists(FilesPath))//目录
                {
                    ConsoleEnhanced.WriteLine("努力压缩中...", ConsoleColor.White);
                    Directory.CreateDirectory($"{FilesPath}-ImageCompression");
                    string TargetDir = $"{FilesPath}-ImageCompression";//输出位置
                    List<Task<string>> ListFor = new List<Task<string>>();
                    DirectorStartNewStop(FilesPath, ListFor, TargetDir, Format);//遍历压缩
                    ConsoleEnhanced.WriteLine($"共有{ListFor.Count}个项目需要处理", ConsoleColor.Cyan);

                    AnsiConsole.Progress()
                    //.AutoRefresh(false) // 关掉自动刷新
                    //.AutoClear(false)   // 完成后不删除任务列表
                    //.HideCompleted(false)   // 在任务完成后隐藏它们
                    .Columns(new ProgressColumn[]
                    {
                        new TaskDescriptionColumn(),    // 任务描述
                        new ProgressBarColumn(),        // 进度栏
                        new PercentageColumn(),         // 百分比
                        //new RemainingTimeColumn(),      // 余下的时间
                        new SpinnerColumn(),            // 旋转器
                    }).Start(ctx =>//进度条
                    {
                        var Progress = ctx.AddTask("[green]压缩进度[/]");
                        int NumberCompletions = 0;    //记录完成数量
                        //int TempNumberCompletions = 0;//记录完成数量(暂存)
                        Progress.MaxValue = ListFor.Count;//设置百分百大小
                        do//计算完成数量
                        {
                            NumberCompletions = ListFor.Count(t => t.IsCompleted);//获取完成线程数量
                            //Progress.Value = (double)((double)NumberCompletions / (double)ListFor.Count) * 100.0d;
                            Progress.Value = NumberCompletions;
                            //TempNumberCompletions = NumberCompletions;//刷新暂存
                            //Task.Delay(200);
                        }
                        while (NumberCompletions != ListFor.Count);//等待列队完成
                    });
                    //Task.WaitAll();//等待任务完成
                    ConsoleEnhanced.WriteLine($"完成!文件夹路径:{Path.GetFullPath(TargetDir)}", ConsoleColor.Blue);
                }
            }
        }
    }
}
