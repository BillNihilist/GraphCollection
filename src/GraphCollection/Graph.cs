using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphCollection
{
    public class Graph<T>
    {
        public Graph()
        {
            Console.WriteLine("I'm a graph of type: " + typeof(T).Name );
        }
    }
}
