using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;


namespace DijkstraAndFloyd
{
    class Graph
    {
        //tablica poLinkn
        double[,] thicknessTable;
        Link[] links = new Link[1];
        int nodeNumber;
        int linkNumber;
        double[,] adjacenyMatrix;
        int[,] pathTable;
        double[,] prethicknessTable;
        Node[] nodes;

        double infinity = Double.PositiveInfinity;
        Node[,] FloydTable;

        Node[,] FloydTable_thickness;
        public void dijkstraAB(int start)
        {
            Node[] prelengthAB = new Node[nodeNumber + 1];

            for (int i = 1; i <= nodeNumber; i++)
            {
                prelengthAB[i] = new Node();
            }

            PirorityQueue<double, int> queue = new PirorityQueue<double, int>();

            for (int i = 1; i <= nodeNumber; i++)
            {
                if (i != start)
                {

                    prelengthAB[i].setPrelength(thicknessTable[start, i]);
                    prelengthAB[i].setBefore(start);
                    //nastepnie dodanie swiezo ocechowanych wezlow
                    if (prelengthAB[i].getPrelength() != infinity)
                    {
                        Element<double, int> newElem = new Element<double, int>(prelengthAB[i].getPrelength(), i);
                        queue.add(newElem);
                    }
                }
            }

            int currNode = 0;
            while ((queue.getSize() != 0))
            {
                Element<double, int> curr = queue.getMin();
                currNode = curr.getData();
                for (int i = 1; i <= nodeNumber; i++)
                {
                    if ((i != currNode) && (thicknessTable[currNode, i] != infinity))
                    {
                        double newThickness = min(prelengthAB[currNode].getPrelength(), thicknessTable[currNode, i]);
                        //thickness of the path through currNode

                        double oldThickness = prelengthAB[i].getPrelength();
                        bool isThicker = false;
                        if (oldThickness < newThickness)
                            isThicker = true;
                        if ((oldThickness == infinity) && (newThickness != infinity))
                            isThicker = true;

                        if (isThicker)
                        //if path is thicher through currNode
                        {
                            prelengthAB[i].setPrelength(newThickness);
                            prelengthAB[i].setBefore(currNode);
                            //next adding some freshly chenged Nodes
                            Element<double, int> newElem = new Element<double, int>(prelengthAB[i].getPrelength(), i);
                            queue.add(newElem);
                        }
                    }
                }


            }
            //getting info through what nodes and using what edges you get from A to B
            for (int i = 1; i <= nodeNumber; i++)
            {
                pathTable[start, i] = prelengthAB[i].getBefore();
                prethicknessTable[start, i] = prelengthAB[i].getPrelength();
            }


        }
        double min(double a, double b)
        {
            if (a < b)
                return a;
            else return b;
        }
        public long dijkstraall()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            long time = 0;
            prethicknessTable = new double[nodeNumber + 1, nodeNumber + 1];
            pathTable = new int[nodeNumber + 1, nodeNumber + 1];


            for (int i = 1; i <= nodeNumber; i++)
            {

                dijkstraAB(i);

            }
            watch.Stop();
            time = watch.ElapsedMilliseconds;
            return time;
        }
        public long Floyd()
        {

            long time = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            FloydTable = new Node[nodeNumber + 1, nodeNumber + 1];
            for (int i = 1; i <= nodeNumber; i++)
                for (int j = 1; j <= nodeNumber; j++)
                {
                    FloydTable[i, j] = new Node();
                    FloydTable[i, j].setPrelength(infinity);
                    FloydTable[i, j].setBefore(1000000);
                    if (i == j)
                        FloydTable[i, j].setPrelength(0);
                }
            for (int k = 1; k <= linkNumber; k++)
            {
                FloydTable[links[k].start, links[k].end].setPrelength(adjacenyMatrix[links[k].start, links[k].end]);
                FloydTable[links[k].start, links[k].end].setBefore(links[k].start);
            }
            for (int u = 1; u <= nodeNumber; u++)
                for (int v1 = 1; v1 <= nodeNumber; v1++)
                    for (int v2 = 1; v2 <= nodeNumber; v2++)
                    {
                        double newPrelength = FloydTable[v1, u].getPrelength() + FloydTable[u, v2].getPrelength();
                        double oldPrelength = FloydTable[v1, v2].getPrelength();
                        if (oldPrelength > newPrelength)
                        {
                            FloydTable[v1, v2].setPrelength(newPrelength);
                            FloydTable[v1, v2].setBefore(FloydTable[u, v2].getBefore());
                        }
                    }
            watch.Stop();
            time = watch.ElapsedMilliseconds;

            return time;
        }
        public long FloydThickness()
        {

            long time = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            FloydTable_thickness = new Node[nodeNumber + 1, nodeNumber + 1];
            for (int i = 1; i <= nodeNumber; i++)
                for (int j = 1; j <= nodeNumber; j++)
                {
                    FloydTable_thickness[i, j] = new Node();
                    FloydTable_thickness[i, j].setPrelength(infinity);//nieskonczone
                    FloydTable_thickness[i, j].setBefore(1000000);//niezdefiniowane
                    if (i == j)
                        FloydTable_thickness[i, j].setPrelength(0);
                }
            for (int k = 1; k <= linkNumber; k++)
            {
                FloydTable_thickness[links[k].start, links[k].end].setPrelength(adjacenyMatrix[links[k].start, links[k].end]);
                FloydTable_thickness[links[k].start, links[k].end].setBefore(links[k].start);
            }
            for (int u = 1; u <= nodeNumber; u++)
                for (int v1 = 1; v1 <= nodeNumber; v1++)
                    for (int v2 = 1; v2 <= nodeNumber; v2++)
                    {

                        double newPrelength = min(FloydTable_thickness[v1, u].getPrelength(), FloydTable_thickness[u, v2].getPrelength());
                        double oldPrelength = FloydTable_thickness[v1, v2].getPrelength();

                        bool isThicker = false;
                        if (oldPrelength < newPrelength)
                            isThicker = true;
                        if ((oldPrelength == infinity) && (newPrelength != infinity))
                            isThicker = true;

                        if (isThicker)
                        {
                            FloydTable_thickness[v1, v2].setPrelength(newPrelength);
                            FloydTable_thickness[v1, v2].setBefore(FloydTable_thickness[u, v2].getBefore());
                        }
                    }
            watch.Stop();
            time = watch.ElapsedMilliseconds;

            return time;
        }
        public void loadFromFile()
        {
            #region loading from file
            Random rand = new Random(DateTime.Now.Millisecond);
            string filePath;
            StreamReader read;
            string[] words;
            Console.WriteLine("Drag input file here and press ENTER");
            filePath = Console.ReadLine();
            if (filePath[0] == '\"') filePath = filePath.Substring(1, filePath.Length - 2);
            Console.WriteLine(" ");
            read = new StreamReader(filePath);
            String line = "";
            while (line.Length < 2 || line[0] == '#')
            {
                line = read.ReadLine();
            }
            words = line.Split(' ');
            if (words[0] == "NODES") nodeNumber = int.Parse(words[2]);
            line = read.ReadLine();
            while (line.Length < 2 || line[0] == '#')
            {
                line = read.ReadLine();
            }
            words = line.Split(' ');
            if (words[0] == "LINKS") linkNumber = int.Parse(words[2]);

            nodes = new Node[nodeNumber + 1];
            links = new Link[linkNumber + 1]; 
            nodes[0] = null;
            links[0] = null;

            for (int i = 1; i <= nodeNumber; i++)
            {
                nodes[i] = new Node(infinity, i);
            }


            line = read.ReadLine(); 
            for (int i = 1; i <= linkNumber; i++)
            {
                line = read.ReadLine();
                words = line.Split(' ');
                links[i] = new Link(int.Parse(words[0]), rand.NextDouble(), int.Parse(words[1]), int.Parse(words[2]));
            }
            #endregion

            #region building graph
            int a, b;
            adjacenyMatrix = new double[nodeNumber + 1, nodeNumber + 1];
            thicknessTable = new double[nodeNumber + 1, nodeNumber + 1];
            for (int j = 1; j <= nodeNumber; j++)
                for (int k = 1; k <= nodeNumber; k++)
                {
                    thicknessTable[j, k] = infinity;
                    adjacenyMatrix[j, k] = infinity;
                }

            for (int i = 1; i <= linkNumber; i++)
            {
                a = links[i].start;
                b = links[i].end;

                adjacenyMatrix[a, b] = links[i].thickness;
                thicknessTable[a, b] = links[i].thickness;


            }





            #endregion
        }
        public void randomizeLinks()
        {
            Random rnd = new Random();
            for (int i = 1; i <= linkNumber; i++)
            {
                int a, b;
                links[i].thickness = rnd.NextDouble();
                a = links[i].start;
                b = links[i].end;

                adjacenyMatrix[a, b] = links[i].thickness;
                thicknessTable[a, b] = links[i].thickness;
            }
        }

    }
}
