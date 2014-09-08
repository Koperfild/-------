using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace AirMapTest
{
    
    public partial class Test_AirMap//Токио или Мали нет на авиакарте
    {
        [TestMethod]
        public void Test_Communicate_Failed()
        {
            AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.AirMap));
            Assert.IsFalse(map.Communicate("Токио", "Питер"));
        }
        [TestMethod]
        public void Test_Communicate_Failed2()
        {
            AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.AirMap));
            Assert.IsFalse(map.Communicate("Токио", "Мали"));
        }

        [TestMethod]
        public void Test_Communicate_Successful()
        {
            AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory,FilesDirectories.AirMap));
            Assert.IsFalse(!map.Communicate("Алушта", "Питер"));
        }
    }
}
