using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Лаба1;

namespace Test
{
    /// <summary>
    /// Сравнивает истинное расстояние (взятое разработчиком с графа) и расчитываемое методом CalcOptimalWays
    /// Проверки несуществующих маршрутов осуществляются в методе BuildGroundRace
    /// </summary>
    
    public partial class Test_GraphMap
    {
        [TestMethod]
        public void CalcOptimalWays_Successfull()
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
