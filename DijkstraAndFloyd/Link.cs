using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAndFloyd
{
    class Link
    {
        public int id;
        public int start = 1;
        public int end = 2;
        public double thickness = 3;

        public Link(int id1, double w, int p, int k)
        {
            id = id1;
            start = p;
            end = k;
            thickness = w;
        }
        public Link()
        {

        }
    }
}
