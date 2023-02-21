using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using ImageMagick;
using static 图像压缩.MODE.DisplayOutput;

namespace 图像压缩.MODE
{
    /// <summary>
    /// 图像压缩
    /// </summary>
    public class ImageCompression
    {
        /// <summary>
        /// 压缩图像
        /// </summary>
        /// <param name="FilePathIn">文件名</param>
        /// <param name="FilePathOut">输出文件名</param>
        /// <returns></returns>
        async public static Task<string> Compression(string FilePathIn, string FilePathOut = null)
        {
            if (!File.Exists(FilePathIn))//待处理文件不存在
            {
                return null;
            }
            if (FilePathOut == null || FilePathOut == "")
            {
                FilePathOut = $"{Path.GetDirectoryName(FilePathIn)}/{Path.GetFileNameWithoutExtension(FilePathIn)}.webp";//将文件以同名存至同位置(默认保存成webp)
            }
            if (Path.GetExtension(FilePathIn) == Path.GetExtension(FilePathOut))//如果扩展名一样直接返回原文件位置
            {
                return FilePathIn;
            }
            try
            {
                using (var image = new MagickImage(FilePathIn))
                {
                    ConsoleEnhanced.WriteLine(Path.GetFileName(FilePathIn) + " 添加列队成功!",ConsoleColor.Cyan);
                    await image.WriteAsync(FilePathOut);
                    ConsoleEnhanced.WriteLine(Path.GetFileName(FilePathOut) + " 压缩成功!",ConsoleColor.Green);
                }
            }
            catch(Exception ex)
            {
                ConsoleEnhanced.WriteLine($"错误: {ex.Message} 对于文件 {FilePathIn}",ConsoleColor.Red);
                return null;
            }
            return FilePathOut;
        }
        /// <summary>
        /// 文件夹遍历
        /// </summary>
        /// <param name="dir">目标目录</param>
        /// <param name="list">任务列表</param>
        /// <param name="FilesPath">输出目录</param>
        /// <param name="Format">目标格式</param>
        public static void Director(string dir, List<Task<string>> list, string FilesPath, string Format )
        {
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles();//文件
            DirectoryInfo[] directs = d.GetDirectories();//文件夹
            foreach (FileInfo f in files)
            {
                Task.Factory.StartNew(() => Compression(f.FullName, $"{FilesPath}/{Path.GetFileNameWithoutExtension(f.Name)}.{Format}"));
                //Task<Task<string>> data = Task.Factory.StartNew(() => Compression(f.FullName, $"{FilesPath}/{Path.GetFileNameWithoutExtension(f.Name)}.{Format}"));
                //Console.WriteLine(Data.Wait());
                //Console.WriteLine(f.FullName + " ");
            }
            //获取子文件夹内的文件列表，递归遍历  
            foreach (DirectoryInfo dd in directs)
            {
                Director(dd.FullName, list, FilesPath, Format);
            }
        }


        public static void DirectorStartNewStop(string dir, List<Task<string>> List, string FilesPath, string Format,List<Task> ListFor)
        {
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles();//文件
            DirectoryInfo[] directs = d.GetDirectories();//文件夹
            foreach (FileInfo f in files)
            {
                //ListFor.Add(Task.Factory.StartNew( () =>  Compression(f.FullName, $"{FilesPath}/{Path.GetFileNameWithoutExtension(f.Name)}.{Format}")));
                ListFor.Add(Task.Run( () => Compression(f.FullName, $"{FilesPath}/{Path.GetFileNameWithoutExtension(f.Name)}.{Format}")));
                //data = Task.Factory.StartNew(() => Compression(f.FullName, $"{FilesPath}/{Path.GetFileNameWithoutExtension(f.Name)}.{Format}"));
                //Console.WriteLine(Data.Wait());
                //Console.WriteLine(f.FullName + " ");
            }
            //获取子文件夹内的文件列表，递归遍历  
            foreach (DirectoryInfo dd in directs)
            {
                 DirectorStartNewStop(dd.FullName, List, FilesPath, Format, ListFor);
            }
            //ListFor[ListFor.Count - 1];
            //Console.WriteLine(ListFor.Count);
            //Task.WaitAny(ListFor.ToArray());
        }
    }
}
