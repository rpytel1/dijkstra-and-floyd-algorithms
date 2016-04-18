using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAndFloyd
{
    public class PirorityQueue<K, T> where K : IComparable
    {
        Element<K, T>[] elements;
        int size;
        int possible_size;
        public PirorityQueue()
        {
            size = 0;
            possible_size = 1;
            elements = new Element<K, T>[2];
        }
        public void add(Element<K, T> elem)
        {

            size++;
            if (possible_size == size)
            {
                possible_size *= 2;
                Element<K, T>[] trmp = new Element<K, T>[possible_size];

                for (int w = 0; w < size; w++)
                {
                    trmp[w] = elements[w];
                }
                elements = trmp;
            }
            elements[size] = elem;

            int i = size;
            int parent = size / 2;
            while (parent > 0)
            {
                if (elements[parent].getKey().CompareTo(elements[i].getKey()) > 0)
                {
                    Element<K, T> tmp = elements[i];
                    elements[i] = elements[parent];
                    elements[parent] = tmp;
                    i = parent;
                    parent = i / 2;
                }
                else
                {
                    // heap contition is satisfied, breaking
                    break;
                }
            }

        }

        int left(int i)
        {
            return 2 * i;
        }
        int right(int i)
        {
            return 2 * i + 1;
        }
        public Element<K, T> getMin()
        {
            Element<K, T> result = elements[1];
            //size--;
            elements[1] = elements[size];
            elements[size] = null;
            size--;
            int i = 1;
            int max = 1;
            bool leftbigger = false;

            while (true)
            {
                if (left(i) <= size && elements[left(i)].getKey().CompareTo(elements[i].getKey()) < 0)
                {
                    max = left(i);             
                }

                if (right(i) <= size && elements[right(i)].getKey().CompareTo(elements[i].getKey()) < 0
                   && elements[right(i)].getKey().CompareTo(elements[left(i)].getKey()) < 0)
                {
                    max = right(i);
                }

                if (max == i)
                {
                    break;
                }

                Element<K, T> tmp = elements[i];
                elements[i] = elements[max];
                elements[max] = tmp;

                i = max;

            }
            return result;
        }
        public int getSize()
        {
            return size;
        }

    }  
}
