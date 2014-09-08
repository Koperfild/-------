using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace AirMapTest
{
    
    public partial class Test_AirMap
    {
        [TestMethod]
        public void readMapFromFile_Fail()
        {
            try
            {
                AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.AirMapTestDirectory, FilesDirectories.AirMapTest));
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, AirMap.ErrorReadingMapFromFile);
                return;
            }
            Assert.Fail("No exception was thrown");
        }
        [TestMethod]
        public void readMapFromFile_Successful()
        {
            try
            {
                AirMap map = new AirMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.AirMap));
            }
            catch (Exception e)
            {
                Assert.Fail("No exception should appear");
            }
            
        }
    }
}
