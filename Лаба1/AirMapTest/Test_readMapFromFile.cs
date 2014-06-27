using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace Test
{
    [TestClass]
    public class AirMapTest3
    {
        [TestMethod]
        public void Test_readMapFromFile()
        {
            try
            {
                AirMap map = new AirMap(FilesDirectories.AirMapTest);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, AirMap.ErrorReadingMapFromFile);
                return;
            }
            Assert.Fail("No exception was thrown");
        }
    }
}
