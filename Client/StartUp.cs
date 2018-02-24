using System.ComponentModel.Design;
using LinearDataStructures;
using Trees;

namespace Client
{
    internal class StartUp
    {
        private static void Main()
        {
            BinaryTree<int> tree = new BinaryTree<int>(3,
                new BinaryTree<int>(10,
                    new BinaryTree<int>(7),
                    new BinaryTree<int>(9)),
                new BinaryTree<int>(5));

            var result = tree.InOrder();
            result = tree.PreOrder();
            result = tree.PostOrder();
        }
    }
}