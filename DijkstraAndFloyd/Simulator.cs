using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAndFloyd
{
    class Simulator
    {
        public void Simulation()
        {
            long timeDijkstra = 0, timeFloyd = 0, timeFloydthickness = 0;
            Graph Graph = new Graph();
            Graph.loadFromFile();
            Console.WriteLine("Tell me how much A(number of randomizations) is:");
            int a;

            a = int.Parse(Console.ReadLine());
            for (int i = 0; i < a; i++)
            {
                Graph.randomizeLinks();
                timeDijkstra += Graph.dijkstraall();
                timeFloydthickness += Graph.FloydThickness();
                timeFloyd += Graph.Floyd();
            }
            double X = 0, Y = 0;
            X = timeFloyd / a;
            Y = timeDijkstra / a;
            Console.WriteLine("Results for algorithms");
            Console.WriteLine("Time of finding thickest path according to algorithm of Floyd-Warshall: " + Convert.ToString(timeFloydthickness) + "ms");
            Console.WriteLine("Time of finding thichest path according to algorithm of Dijkstra: " + Convert.ToString(timeDijkstra) + "ms");
            Console.WriteLine("Time of finding shortest path according to algorith of Floyd-Warshall: " + Convert.ToString(timeFloyd) + "ms");

            Console.WriteLine("Avreage time of finding thickest path according to algorithm of Floyd-Warshall: " + Convert.ToString(timeFloydthickness/a) + "ms");
            Console.WriteLine("Avreage time of finding thickest path according to algorithm of Dijkstra " + Convert.ToString(Y) + "ms");
            Console.WriteLine("Avreage time of finding shortest path according to algorith of Floyd-Warshall " + Convert.ToString(timeFloyd/a) + "ms");

            Console.ReadLine();




        }

    }
}
