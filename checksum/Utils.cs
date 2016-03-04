using System;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace Checksum
{
    public static class Utils
    {
        public static void AddToContextMenu()
        {
            string regkey = (string)Registry.GetValue("HKEY_CLASSES_ROOT\\*\\shell\\Checksum\\command", null, null);
            string regIcon = (string)Registry.GetValue("HKEY_CLASSES_ROOT\\*\\shell\\Checksum", "Icon", null);
            if (regkey == null || !regkey.Contains(Application.ExecutablePath))
                Registry.SetValue("HKEY_CLASSES_ROOT\\*\\shell\\Checksum\\command", null, Application.ExecutablePath + " %1");
            if (regIcon == null || regIcon != Application.ExecutablePath)
                Registry.SetValue("HKEY_CLASSES_ROOT\\*\\shell\\Checksum", "Icon", Application.ExecutablePath);
        }

        public static void RemoveFromContextMenu()
        {
            var checkSumKey = Registry.ClassesRoot.OpenSubKey("*\\shell\\", true);
            checkSumKey.DeleteSubKeyTree("Checksum");
            checkSumKey.Close();
        }

        public static string CalculateMD5Hash(string file)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(file))
            {
                byte[] b = md5.ComputeHash(stream);
                return BitConverter.ToString(b).Replace("-", string.Empty).ToLowerInvariant();
            }
        }

        public static string CalculateSHA1Hash(string file)
        {
            using (SHA1 sha1 = SHA1.Create())
            using (var stream = File.OpenRead(file))
            {
                byte[] b = sha1.ComputeHash(stream);
                return BitConverter.ToString(b).Replace("-", string.Empty).ToLowerInvariant();
            }
        }

        public static string CalculateSHA256Hash(string file)
        {
            using (var sha256 = SHA256.Create())
            using (var stream = File.OpenRead(file))
            {
                byte[] b = sha256.ComputeHash(stream);
                return BitConverter.ToString(b).Replace("-", string.Empty).ToLowerInvariant();
            }
        }

        public static string CalculateSHA512Hash(string file)
        {
            using (var sha512 = SHA512.Create())
            using (var stream = File.OpenRead(file))
            {
                byte[] b = sha512.ComputeHash(stream);
                return BitConverter.ToString(b).Replace("-", string.Empty).ToLowerInvariant();
            }
        }
    }
}
