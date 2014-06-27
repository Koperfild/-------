using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_readMapFromFile_Fail()
        {
            try
            {
                GraphMap map = new GraphMap(FilesDirectories.GraphMapTest);
            }
            catch (FormatException e)
            {
                StringAssert.Contains(e.Message, GraphMap.ErrorReadFile);
            }
            catch (OverflowException e)
            {
                StringAssert.Contains(e.Message, GraphMap.TooBigValueInFile);
            }
            catch (ArgumentNullException e)
            {
                StringAssert.Contains(e.Message, GraphMap.InternalErrorFileReading);
            }
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
                StringAssert.Contains(e.Message, GraphMap.ErrorReadFile);
            }
        }
    }
}
