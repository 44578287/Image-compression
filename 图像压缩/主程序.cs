using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
                Console.WriteLine("请输入文件夹或文件路径");
                FilesPath = Console.ReadLine();//输入文件路径

                string Format = null;
                Console.WriteLine("请输入目标格式");
                Format = Console.ReadLine();//输入文件路径

                if (File.Exists(FilesPath))//文件
                {
                    Console.WriteLine("努力压缩中...");
                    string FilePathOut = Compression(FilesPath, $"{Path.GetDirectoryName(FilesPath)}/{Path.GetFileNameWithoutExtension(FilesPath)}.{Format}").Result;
                    if(FilePathOut != null)
                        Console.WriteLine($"完成!文件路径:{Path.GetFullPath(FilePathOut)}");
                }
                else if (Directory.Exists(FilesPath))//目录
                {
                    Console.WriteLine("努力压缩中...");
                    Directory.CreateDirectory($"{FilesPath}-ImageCompression");
                    string TargetDir = $"{FilesPath}-ImageCompression";//输出位置
                    List<Task<string>> CompressionQueue = new List<Task<string>>();
                    Director(FilesPath, CompressionQueue, TargetDir, Format);
                    Console.WriteLine($"共有{CompressionQueue.Count}个项目需要处理");
                    Console.WriteLine($"完成!文件夹路径:{Path.GetFullPath(TargetDir)}");
                }
            }
        }
    }
}
