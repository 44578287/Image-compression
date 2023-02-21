using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using static 图像压缩.MODE.DisplayOutput;
using static 图像压缩.MODE.ImageCompression;

namespace 图像压缩
{
    public class 主程序
    {
        public static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                string FilesPath = null;
                ConsoleEnhanced.WriteLine("请输入文件夹或文件路径",ConsoleColor.Yellow);
                FilesPath = Console.ReadLine();//输入文件路径

                string Format = null;
                ConsoleEnhanced.WriteLine("请输入目标格式",ConsoleColor.Yellow);
                Format = Console.ReadLine();//输入文件路径

                if (File.Exists(FilesPath))//文件
                {
                    ConsoleEnhanced.WriteLine("努力压缩中...",ConsoleColor.White);
                    string FilePathOut = Compression(FilesPath, $"{Path.GetDirectoryName(FilesPath)}/{Path.GetFileNameWithoutExtension(FilesPath)}.{Format}").Result;
                    if(FilePathOut != null)
                        Console.WriteLine($"完成!文件路径:{Path.GetFullPath(FilePathOut)}");
                }
                else if (Directory.Exists(FilesPath))//目录
                {
                    ConsoleEnhanced.WriteLine("努力压缩中...", ConsoleColor.White);
                    Directory.CreateDirectory($"{FilesPath}-ImageCompression");
                    string TargetDir = $"{FilesPath}-ImageCompression";//输出位置
                    List<Task<string>> CompressionQueue = new List<Task<string>>();
                    List<Task> ListFor = new List<Task> ();
                    DirectorStartNewStop(FilesPath, CompressionQueue, TargetDir, Format, ListFor);
                    ConsoleEnhanced.WriteLine($"共有{ListFor.Count}个项目需要处理",ConsoleColor.Cyan);
                    Task.WaitAll(ListFor.ToArray());
                    //Task.WaitAll(ListFor[ListFor.Count - 1]);
                    ConsoleEnhanced.WriteLine($"完成!文件夹路径:{Path.GetFullPath(TargetDir)}",ConsoleColor.Blue);
                }
            }
        }
    }
}
