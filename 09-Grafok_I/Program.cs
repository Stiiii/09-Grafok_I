using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_Grafok_I
{
    class Program
    {
        static void Kiiro(string item)
        {
            Console.WriteLine(item);
        }

        static void Main(string[] args)
        {
            Graph<string> graf = new Person<string>();

            graf.AddNode("Stew");
            graf.AddNode("Joseph");
            graf.AddNode("Marge");
            graf.AddNode("Gerald");
            graf.AddNode("Zack");
            graf.AddNode("Peter");
            graf.AddNode("Janet");

            graf.AddEdge("Stew", "Joseph");
            graf.AddEdge("Stew", "Marge");
            graf.AddEdge("Marge", "Joseph");
            graf.AddEdge("Joseph", "Gerald");
            graf.AddEdge("Joseph", "Zack");
            graf.AddEdge("Gerald", "Zack");
            graf.AddEdge("Zack", "Peter");
            graf.AddEdge("Peter", "Janet");

            //graf.BFS("Marge", Kiiro);

            Console.WriteLine(graf.BFS_Utvonallal("Peter", "Gerald", Kiiro));

            Console.ReadKey();
        }
    }
}
