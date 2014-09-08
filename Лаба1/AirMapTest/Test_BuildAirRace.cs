using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace AirMapTest
{
    [TestClass]
    public partial class Test_AirMap
    {
        [TestMethod]
        public void BuildAirRace_Failed()//Токи и Мали нет на карте
        {
            AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.AirMap));
            var privateObject = new PrivateObject(map);
            try
            {                
                privateObject.Invoke("BuildAirRace", "Токио", "Мали");
                Assert.Fail("Error");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, AirMap.AirportsDoesntCommunicate);
            }
        }
        [TestMethod]
        public void BuildAirRace_Failed2()//Токи и Мали нет на карте
        {
            AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.AirMap));
            var privateObject = new PrivateObject(map);
            try
            {
                privateObject.Invoke("BuildAirRace", "Москва", "Мали");
                Assert.Fail("Error");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, AirMap.AirportsDoesntCommunicate);
            }
        }
        [TestMethod]
        public void BuildAirRace_Successful()//Токи и Мали нет на карте
        {
            AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory,FilesDirectories.AirMap));
            var privateObject = new PrivateObject(map);
            try
            {
                privateObject.Invoke("BuildAirRace", "Москва", "Алушта");
                Assert.Fail(AirMap.AirportsDoesntCommunicate);
            }
             catch (Exception e)
            {
                if (e.GetType() is AssertFailedException)
                {
                    Assert.Fail("Something is wrong in BuildAirRace");
                }
            }
        }
    }
}
