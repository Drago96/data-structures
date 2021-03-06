﻿using AStar;
using System;

namespace Trees.Tasks.AStar
{
    public static class AStarStartUp
    {
        private static readonly char[,] map =
       {
            { '-', '-', '*', '-', '-', '-', '-', '-' },
            { '-', '-', '-', '-', '-', '-', '-', '-' },
            { '-', 'W', 'W', 'W', 'W', '-', '-', '-' },
            { '-', '-', '-', '-', 'W', '-', 'W', 'W' },
            { '-', '-', '-', 'P', 'W', '-', '-', '-' },
            { '-', '-', '-', '-', '-', '-', '-', '-' }
        };

        public static void Solve()
        {
            // map = ReadMap();

            var start = FindGoal('P');
            var goal = FindGoal('*');

            var aStar = new AStar(map);
            var path = aStar.GetPath(start, goal);

            foreach (var node in path)
            {
                var row = node.Row;
                var col = node.Col;
                map[row, col] = '@';
            }

            PrintMap();
        }

        private static char[,] ReadMap()
        {
            var n = int.Parse(Console.ReadLine());
            var map = new char[n, n];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                char[] line = Console.ReadLine().ToCharArray();
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = line[j];
                }
            }

            return map;
        }

        private static Node FindGoal(char goal)
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    if (map[row, col] == goal)
                    {
                        return new Node(row, col);
                    }
                }
            }

            throw new ArgumentException("Object not present on map");
        }

        private static void PrintMap()
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    if (map[row, col] == '@')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }

                    Console.Write(map[row, col]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ");
                }

                Console.WriteLine();
            }
        }
    }
}