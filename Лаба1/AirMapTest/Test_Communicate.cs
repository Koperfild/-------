using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace AirMapTest
{
    [TestClass]
    public class AirMapTest2//Токи или Мали нет на авиакарте
    {
        [TestMethod]
        public void Test_Communicate()
        {
            AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory,FilesDirectories.AirMap));
            Assert.IsFalse(map.Communicate("Токи", "Питер"));
        }
    }
}
