using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace Test
{
    public partial class Test_GraphMap
    {
        [TestMethod]
        public void Test_readMapFromFile_Fail()
        {
            try
            {
                GraphMap map = new GraphMap(FilesDirectories.getFilePath(FilesDirectories.GraphMapTestDirectory,FilesDirectories.GraphMapTest));
            }
            catch (FormatException e)
            {
                StringAssert.Contains(e.Message, GraphMap.ErrorReadFile);
                return;
            }
            catch (OverflowException e)
            {
                StringAssert.Contains(e.Message, GraphMap.TooBigValueInFile);
                return;
            }
            catch (ArgumentNullException e)
            {
                StringAssert.Contains(e.Message, GraphMap.InternalErrorFileReading);
                return;
            }
            Assert.Fail("Exception is supposed to be thrown");
        }

        [TestMethod]
        public void Test_readMapFromFile_Success()
        {
            try
            {
                GraphMap map = new GraphMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory,FilesDirectories.GraphMap));
            }
            catch (Exception e)
            {
                Assert.Fail("No exception is supposed to be thrown");
            }
        }
    }
}
