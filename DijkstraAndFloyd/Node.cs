using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAndFloyd
{
    public class Node
    {
        double prelength;
        int before;
        int id;
        public Node()
        {
            prelength = Double.PositiveInfinity;
        }
        public Node(double a, int b)
        {
            prelength = a;
            id = b;
        }
        public void setPrelength(double a)
        {
            prelength = a;
        }
        public void setBefore(int a)
        {
            before = a;
        }
        public int getBefore()
        {
            return before;
        }
        public double getPrelength()
        {
            return prelength;
        }
    }
}
