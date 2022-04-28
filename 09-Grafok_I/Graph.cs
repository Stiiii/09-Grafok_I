using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_Grafok_I
{
    class GraphEventArgs<T>
    {
        T from, to;

        public GraphEventArgs(T from, T to)
        {
            this.from = from;
            this.to = to;
        }
    }

    class Graph<T>
    {
        List<T> tartalmak;
        List<List<Edge>> szomszedok;

        public delegate void EventHandler<T>(object source, GraphEventArgs<T> geargs);

        public delegate void ExternalProcessor(string tartalom);

        public event EventHandler<T> GraphEventHandler;

        public Graph()
        {
            tartalmak = new List<T>();
            szomszedok = new List<List<Edge>>();
        }

        public class Edge
        {
            public T to;
        }

        public void AddNode(T item)
        {
            tartalmak.Add(item);
            szomszedok.Add(new List<Edge>());
        }

        public void AddEdge(T from, T to)
        {
            int index = tartalmak.IndexOf(from);
            szomszedok[index].Add(new Graph<T>.Edge()
            {
                to = to
            });
            index = tartalmak.IndexOf(to);
            szomszedok[index].Add(new Graph<T>.Edge()
            {
                to = from
            });
            GraphEventHandler?.Invoke(from, new GraphEventArgs<T>(from, to));
        }


        public bool HasEdge(T from, T to)
        {
            int index = tartalmak.IndexOf(from);
            for (int i = 0; i < szomszedok[index].Count; i++)
            {
                if (szomszedok[index][i].to.ToString() == to.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public List<Edge> Neighbors(T node)
        {
            int index = tartalmak.IndexOf(node);
            return szomszedok[index];
        }

        public void BFS(T honnan, ExternalProcessor _metodus)
        {
            ExternalProcessor metodus = _metodus;
            Queue<T> S = new Queue<T>();
            List<T> F = new List<T>();

            S.Enqueue(honnan);
            F.Add(honnan);

            while (S.Count != 0)
            {
                T k = S.Dequeue();
                metodus?.Invoke(k.ToString());
                foreach (Edge x in Neighbors(k))
                {
                    if (!F.Contains(x.to))
                    {
                        S.Enqueue(x.to);
                        F.Add(x.to);
                    }
                }
            }
        }

        public int BFS_Utvonallal(T honnan, T hova, ExternalProcessor _metodus)
        {
            int db = 0;

            ExternalProcessor metodus = _metodus;
            Queue<T> S = new Queue<T>();
            List<T> F = new List<T>();

            S.Enqueue(honnan);
            F.Add(honnan);

            while (S.Count != 0)
            {
                T k = S.Dequeue();
                metodus?.Invoke(k.ToString());
                foreach (Edge x in Neighbors(k))
                {
                    if (!F.Contains(x.to))
                    {
                        S.Enqueue(x.to);
                        F.Add(x.to);
                        if (!F.Contains(hova))
                        {
                            db++;
                        }
                    }
                }
            }
            return db;
        }

        public void DFS(T k, ExternalProcessor _metodus)
        {
            List<T> F = new List<T>();
            DFS_Util(k, ref F, _metodus);
        }

        public void DFS_Util(T k, ref List<T> F, ExternalProcessor _metodus)
        {
            ExternalProcessor metod = _metodus;
            F.Add(k);
            _metodus?.Invoke(k.ToString());
            foreach (Edge item in Neighbors(k))
            {
                if (!F.Contains(item.to))
                {
                    DFS_Util(item.to, ref F, metod);
                }
            }
        }
    }
}
