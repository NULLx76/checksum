using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;

namespace Checksum.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void CheckRegistryTest1()
        {
            RegistryUtils.AddToContextMenu();
            Assert.IsTrue(Registry.ClassesRoot.OpenSubKey("*\\shell\\Checksum") != null);
        }
        [TestMethod()]
        public void CheckRegistryTest2()
        {
            //Assert.IsTrue(Registry.ClassesRoot.OpenSubKey("*\\shell\\Checksum") != null);
            RegistryUtils.RemoveFromContextMenu();
            Assert.IsTrue(Registry.ClassesRoot.OpenSubKey("*\\shell\\Checksum") == null);
        }
        // Note: run Visual Studio as admin; CTRL+SHIFT+CLICK
    }
}