using System;
using System.Drawing;
using System.IO;

namespace AntiCaptchaApi.Net.Internal.Helpers
{
    internal static class StringHelper
    {
        internal static string ImageFileToBase64String(string path)
        {
            try
            {
                using (var image = Image.FromFile(path))
                {
                    using (var m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        var imageBytes = m.ToArray();
                        return Convert.ToBase64String(imageBytes);
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}