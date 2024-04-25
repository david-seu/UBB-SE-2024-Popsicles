using System;
using System.Drawing;
using System.IO;

namespace UBB_SE_2024_Popsicles.Utils
{
    internal class ImageConverter
    {
        public static byte[] ImageToByteArray(Image inputImage, ImageFormat imageFormat)
        {
            using (var memoryStream = new MemoryStream())
            {
                // imageIn.Save(ms, format);
                return memoryStream.ToArray();
            }
        }

        public static Image ByteArrayToImage(byte[] inputByteArray)
        {
            using (var memoryStream = new MemoryStream(inputByteArray))
            {
                return Image.FromStream(memoryStream);
            }
        }
    }

    public enum ImageFormat
    {
        Jpeg,
        Png
    }
}
