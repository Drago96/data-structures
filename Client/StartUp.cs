using LinearDataStructures;

namespace Client
{
    internal class StartUp
    {
        private static void Main()
        {
            var queue = new Queue<int>();

            for (int i = 0; i < 20; i++)
            {
                queue.Enqueue(3);
                queue.Enqueue(5);
                queue.Enqueue(7);
            }

            for (int i = 0; i < 5; i++)
            {
                queue.Dequeue();
            }

            for (int i = 0; i < 30; i++)
            {
                queue.Enqueue(5);
            }
        }
    }
}