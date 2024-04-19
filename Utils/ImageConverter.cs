using System;
using System.Drawing;
using System.IO;

namespace UBB_SE_2024_Popsicles.Utils
{
    internal class ImageConverter
    {
        public static byte[] ImageToByteArray(Image imageIn, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                // imageIn.Save(ms, format);
                return ms.ToArray();
            }
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }
    }

    public enum ImageFormat
    {
        Jpeg,
        Png
    }
}
