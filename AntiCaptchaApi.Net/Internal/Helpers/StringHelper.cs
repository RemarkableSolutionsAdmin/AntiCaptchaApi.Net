using System;
using System.IO;

namespace AntiCaptchaApi.Net.Internal.Helpers
{
    internal static class StringHelper
    {
        internal static string ImageFileToBase64String(string path)
        {
            try
            {
                return Convert.ToBase64String(File.ReadAllBytes(path));
            }
            catch
            {
                return null;
            }
        }
    }
}