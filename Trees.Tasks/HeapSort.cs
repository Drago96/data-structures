using System;

namespace Trees.Tasks
{
    public static class HeapSort<T> where T : IComparable
    {
        public static void Sort(T[] arr)
        {
            for (int i = arr.Length / 2; i >= 0; i--)
            {
                HeapifyDown(i, arr, arr.Length);
            }

            for (int i = arr.Length - 1; i >= 1; i--)
            {
                Swap(0, i, arr);
                HeapifyDown(0, arr, i);
            }
        }

        private static void HeapifyDown(int index, T[] arr, int maxIndex)
        {
            if (index >= arr.Length / 2)
            {
                return;
            }

            int left = index * 2 + 1;
            int right = index * 2 + 2;

            int greaterChild;

            if (right == arr.Length)
            {
                greaterChild = left;
            }
            else
            {
                greaterChild =
                    IsGreater(left, right, arr) ? left : right;
            }

            if (right >= maxIndex)
            {
                greaterChild = left;
                if (left >= maxIndex)
                {
                    return;
                }
            }

            if (IsGreater(greaterChild, index, arr))
            {
                Swap(greaterChild, index, arr);
                HeapifyDown(greaterChild, arr, maxIndex);
            }
        }

        private static void Swap(int firstElementIndex, int secondElementIndex, T[] arr)
        {
            T temp = arr[firstElementIndex];
            arr[firstElementIndex] = arr[secondElementIndex];
            arr[secondElementIndex] = temp;
        }

        private static bool IsGreater(int firstElementIndex, int secondElementIndex, T[] arr)
            => arr[firstElementIndex].CompareTo(arr[secondElementIndex]) > 0;
    }
}