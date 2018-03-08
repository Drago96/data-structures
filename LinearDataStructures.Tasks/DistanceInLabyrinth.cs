using System;
using System.Collections.Generic;
using System.Text;

namespace LinearDataStructures.Tasks
{
    /*
     * PROBLEM:
     * Having a square matrix with values 0, x and *,
     * where 0 marks an empty cell and x represents an impassible wall,
     * find the minimal distance from the cell marked with * to all empty
     * cells.
     *
     * Input:
     * Line 1 : n -> the dimensions of the matrix
     * Line 2 -> n+1 : each row of the matrix
     *
     * Output:
     * The initial matrix, where all empty cells are replaced with their
     * distance to the target cell, or 'u' if no path was found.
     *
     * Example:
     *
     * Input:
     * 3
     * 0x0
     * x0*
     * 000
     *
     * Output:
     * ux1
     * x1*
     * 321
     *
     */

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
            StringBuilder output = new StringBuilder();
            foreach (string[] row in matrix)
            {
                output.AppendLine(string.Join("", row));
            }
            Console.WriteLine(output);
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