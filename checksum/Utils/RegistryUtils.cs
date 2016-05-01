using Microsoft.Win32;
using System.Windows.Forms;

namespace Checksum
{
    public static class RegistryUtils
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
    }
}
