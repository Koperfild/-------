using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace Test
{
    [TestClass]
    public class Test_CalcOptimalWays
    {
        [TestMethod]
        public void Test__CalcOptimalWays()
        {
            string from="Питер";
            string to="Сыктывкар";
            double realDistance = 35;
            GraphMap map = new GraphMap(FilesDirectories.getFilePath(FilesDirectories.Лаба1оDirectory, FilesDirectories.GraphMap));
            var privateObject = new PrivateObject(map);
            privateObject.Invoke("CalcOptimalWays");
            double gotDistance = map.Distance(from, to);
            Assert.AreEqual(realDistance, gotDistance, 0.01, "Wrong optimal way between 2 points");
        }
    }
}
