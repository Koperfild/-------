using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace AirMapTest
{
    
    public partial class Test_AirMap
    {
        [TestMethod]
        public void Distance_Fail()//Токио нет на карте. 
        {
            AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory,FilesDirectories.AirMap));
            try
            {
                map.Distance("Мали", "Токио");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, AirMap.AirportsDoesntCommunicate);
                return;
            }
            Assert.Fail(AirMap.AirportsDoesntCommunicate);
        }
        [TestMethod]
        public void Distance_Fail2()//Токио нет на карте. 
        {
            AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.AirMap));
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
        public void Distance_Successful()
        {
            AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory,FilesDirectories.AirMap));
            try
            {
                map.Distance("Москва", "Питер");
            }
            catch (Exception e)
            {
                Assert.Fail("Error");
            }
        }
    }
}
