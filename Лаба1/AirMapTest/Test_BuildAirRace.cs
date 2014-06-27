using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace AirMapTest
{
    [TestClass]
    public class AirMapTest1
    {
        [TestMethod]
        public void Test_AirportsDoesntCommunicate()//Токи и Мали нет на карте
        {
            AirMap map = new AirMap(FilesDirectories.Test_BuildAirRace);
            var privateObject = new PrivateObject(map);
            try
            {                
                privateObject.Invoke("BuildAirRace", "Токио", "Мали");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, AirMap.AirportsDoesntCommunicate);
                return;
            }
            Assert.Fail(AirMap.AirportsDoesntCommunicate);
        }
    }
}
