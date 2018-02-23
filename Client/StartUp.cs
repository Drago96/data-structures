using System;
using LinearDataStructures;

namespace Client
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var list = new LinkedList<int>();

            list.Prepend(2);
            list.Prepend(1);
        }
    }
}
