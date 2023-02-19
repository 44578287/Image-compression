using System.IO;
using System.Threading.Tasks;
using ImageMagick;

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
            using (var image = new MagickImage(FilePathIn))
            {
                await image.WriteAsync(FilePathOut);
            }
            return FilePathOut;
        }
    }
}
