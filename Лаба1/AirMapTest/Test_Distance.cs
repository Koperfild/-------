using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace AirMapTest
{
    [TestClass]
    public class Test_Distance
    {
        [TestMethod]
        public void TestFalse()
        {
            AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory,FilesDirectories.AirMap));
            try
            {
                map.Distance("Москва", "Токио");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, AirMap.AirportsDoesntCommunicate);
                return;
            }
            Assert.Fail(AirMap.AirportsDoesntCommunicate);
        }
        [TestMethod]
        public void TestTrue()
        {
            AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory,FilesDirectories.AirMap));
            try
            {
                map.Distance("Москва", "Питер");
            }
            catch (Exception e)
            {
                Assert.IsFalse(e.Message == AirMap.AirportsDoesntCommunicate);
            }
            //Assert.Fail(AirMap.AirportsDoesntCommunicate);
        }
    }
}
