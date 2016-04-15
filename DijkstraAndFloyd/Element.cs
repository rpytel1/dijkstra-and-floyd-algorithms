using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAndFloyd
{
    public class Element<K, T> where K : IComparable
    {
        K key;
        T data;

        public Element(K k, T t)
        {
            key = k;
            data = t;
        }
        public K getKey()
        {
            return key;
        }
        public T getData()
        {
            return data;
        }
    }
}
