using System;
using System.Linq;

namespace HashTables.Tasks
{
    class StartUp
    {
        static void Main(string[] args)
        {
            //HashTable<int, int> table = new HashTable<int, int>();

            //for (int i = 0; i < 1000000; i++)
            //{
            //    table.Add(i, 1000000 - i);
            //}

            //var keys = table.Keys.ToList();
            //var values = table.Values.ToList();

            HashSet<int> set = new HashSet<int> {1, 3, 5, 3};

            set.Remove(3);

            foreach (var element in set)
            {
                Console.WriteLine(element);
            }

            HashSet<int> secondSet = new HashSet<int> {1, 10};

            foreach (var el in set.SymetricExcept(secondSet))
            {
                Console.WriteLine(el);
            }

        }
    }
}
