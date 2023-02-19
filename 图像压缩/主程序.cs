using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using 图像压缩.MODE;

namespace 图像压缩
{
    internal class 主程序
    {
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            //System.Console.WriteLine(data["title"]);
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
                    string FilePathOut = ImageCompression.Compression(FilesPath, $"{Path.GetDirectoryName(FilesPath)}/{Path.GetFileNameWithoutExtension(FilesPath)}.{Format}").Result;
                    Console.WriteLine($"完成!文件路径:{Path.GetFullPath(FilePathOut)}");
                }
                else if (Directory.Exists(FilesPath))//目录
                {
                    Console.WriteLine("努力压缩中...");
                    Directory.CreateDirectory($"{FilesPath}-ImageCompression");
                    string TargetDir = $"{FilesPath}-ImageCompression";//输出位置
                    List<Task<string>> CompressionQueue = new List<Task<string>>();
                    Director(FilesPath, CompressionQueue, TargetDir, Format);
                    //CompressionQueue.W
                    Task.WaitAll(CompressionQueue.ToArray());
                    Console.WriteLine($"完成!文件夹路径:{Path.GetFullPath(TargetDir)}");
                }
            }
        }
        public static void Director(string dir, List<Task<string>> list, string FilesPath, string Format)
        {
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles();//文件
            DirectoryInfo[] directs = d.GetDirectories();//文件夹
            foreach (FileInfo f in files)
            {
                list.Add(ImageCompression.Compression(f.FullName, $"{FilesPath}/{Path.GetFileNameWithoutExtension(f.Name)}.{Format}"));
                Console.WriteLine(f.FullName + " 开始压缩");
                //list.Add(f.Name);//添加文件名到列表中  
            }
            //获取子文件夹内的文件列表，递归遍历  
            foreach (DirectoryInfo dd in directs)
            {
                Director(dd.FullName, list, FilesPath, Format);
            }
        }
    }
}
