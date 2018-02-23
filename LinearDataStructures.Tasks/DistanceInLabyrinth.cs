using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace LinearDataStructures.Tasks
{
    public static class DistanceInLabyrinth
    {
        public static void Solve()
        {
            string[][] matrix = ReadInput();

            int startRow = 0;
            int startCol = 0;

            FindStartIndeces(ref startRow, ref startCol, matrix);

            ColorMatrix(startRow, startCol, matrix, 0);

            PrintMatrix(matrix);
        }

        private static void ColorMatrix(int row, int col, string[][] matrix, int distance)
        {
            Queue<(int row, int col, int distance)> cellsQueue = new Queue<(int row, int col, int distance)>();

            QueueNeighbours(row, col, matrix, cellsQueue, distance);

            while (cellsQueue.Count > 0)
            {
                (int row, int col, int distance) currentElement = cellsQueue.Dequeue();
                matrix[currentElement.row][currentElement.col] = currentElement.distance.ToString();
                QueueNeighbours(currentElement.row, currentElement.col, matrix, cellsQueue, currentElement.distance);
            }

            FillNonReachableCells(matrix);
        }

        private static void FillNonReachableCells(string[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == "0")
                    {
                        matrix[i][j] = "u";
                    }
                }
            }
        }

        private static void QueueNeighbours(int row, int col, string[][] matrix, Queue<(int row, int col, int distance)> queue, int distance)
        {
            if (ShouldColor(row + 1, col, matrix))
            {
                queue.Enqueue((row + 1, col, distance + 1));
            }
            if (ShouldColor(row - 1, col, matrix))
            {
                queue.Enqueue((row - 1, col, distance + 1));
            }
            if (ShouldColor(row, col + 1, matrix))
            {
                queue.Enqueue((row, col + 1, distance + 1));
            }
            if (ShouldColor(row, col - 1, matrix))
            {
                queue.Enqueue((row, col - 1, distance + 1));
            }
        }

        private static bool ShouldColor(int row, int col, string[][] matrix)
        {
            if (row < 0 || row >= matrix.Length)
            {
                return false;
            }
            if (col < 0 || col >= matrix.Length)
            {
                return false;
            }

            return matrix[row][col] == "0";

        }

        private static void PrintMatrix(string[][] matrix)
        {
            foreach (string[] row in matrix)
            {
                Console.WriteLine(string.Join("", row));
            }
        }

        private static void FindStartIndeces(ref int startRow, ref int startCol, string[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == "*")
                    {
                        startRow = i;
                        startCol = j;
                        return;
                    }
                }
            }
        }

        private static string[][] ReadInput()
        {
            int dimensions = int.Parse(Console.ReadLine());

            string[][] result = new string[dimensions][];

            for (int i = 0; i < dimensions; i++)
            {
                string currentLine = Console.ReadLine();
                result[i] = new string[dimensions];
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = currentLine[j].ToString();
                }
            }

            return result;
        }
    }
}
