using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace Test
{
    [TestClass]
    public partial class Test_GraphMap
    {
        [TestMethod]
        public void BuildGroundRace_Failed()
        {
            GraphMap map = new GraphMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.GraphMap));
            var privateObject = new PrivateObject(map);
            try
            {
                privateObject.Invoke("BuildGroundRace", "Токио", "Непал");
                //Assert.Fail(GraphMap.PointsDoesntCommunicate); //Можно так вместо StringAssert
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, GraphMap.PointsDoesntCommunicate);
                return;
            }
            Assert.Fail("Exception is supposed to be thrown");
        }
        [TestMethod]
        public void BuildGroundRace_Failed2()
        {
            GraphMap map = new GraphMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.GraphMap));
            var privateObject = new PrivateObject(map);
            try
            {
                privateObject.Invoke("BuildGroundRace", "Москва", "Непал");
                //Assert.Fail(GraphMap.PointsDoesntCommunicate); //Можно так вместо StringAssert
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, GraphMap.PointsDoesntCommunicate);
                return;
            }
            Assert.Fail("Exception is supposed to be thrown");
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
                Assert.Fail("No exception is supposed to be thrown");
            }
        }        
    }
}

