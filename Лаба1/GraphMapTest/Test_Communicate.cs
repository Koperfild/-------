using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace Test
{
    
    public partial class Test_GraphMap
    {
        [TestMethod]
        public void Communicate_True()
        {
            GraphMap map = new GraphMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.GraphMap));
            Assert.IsTrue(map.Communicate("Москва", "Кировск"));
        }
        [TestMethod]
        public void Communicate_False()
        {
            GraphMap map = new GraphMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.GraphMap));
            Assert.IsFalse(map.Communicate("Москва", "Мвен-Дитю"));
        }
    }
}
