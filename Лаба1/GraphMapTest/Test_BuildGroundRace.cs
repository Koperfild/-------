using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace Test
{
    [TestClass]
    public class Test_BuildGroundRace
    {
        [TestMethod]
        public void BuildAirRace_Failed()
        {
            GraphMap map = new GraphMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.GraphMap));
            var privateObject = new PrivateObject(map);
            try
            {
                privateObject.Invoke("BuildGroundRace", "Токио", "Непал");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, GraphMap.PointsDoesntCommunicate);
            }        
        }
        
        [TestMethod]
        public void BuildGroundRace_Successfull()
        {
            GraphMap map = new GraphMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.GraphMap));
            var privateObject = new PrivateObject(map);
            try
            {
                privateObject.Invoke("BuildGroundRace", "Москва", "Питер");
            }
            catch (Exception e)
            {
                if (e.Message == GraphMap.PointsDoesntCommunicate)
                {
                    Assert.Fail(GraphMap.PointsDoesntCommunicate);
                }
            }
        }
    }
}

