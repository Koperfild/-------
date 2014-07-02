using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    abstract public class Map
    {
        public abstract bool Communicate(string from, string to);

        public abstract double Distance(string from, string to);
        protected abstract void readMapFromFile(string mapPath);
        //abstract void readMapFromFile(string path);
        //abstract bool Communicate(string from, string to);убрал ибо в AirMap и GraphMap разные параметры у этого метода. Точнее в AirMap добавлен runwayType
    }
}
