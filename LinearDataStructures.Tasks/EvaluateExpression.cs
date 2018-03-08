using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LinearDataStructures.Tasks
{
    /*
     * PROBLEM:
     * Evaluate a mathematical expression passed as an input string
     * and print out the result. Allowed operations are:
     *
     * (, ) - bracketing,
     * * - multiplication,
     * / - division,
     * + - addition,
     * '-' - subtraction,
     * ^ - power
     *
     * Example:
     *
     * Input:
     * ((2.1^3)-2.5)*3.3
     *
     * Output:
     * 22.3113
     *
     */

    public static class EvaluateExpression
    {
        private static readonly string[] leftAsociativeOperations = new string[] { "+", "-", "*", "/" };
        private static readonly string[] rightAsociativeOperations = new string[] { "^" };

        public static void Solve()
        {
            string[] input = ReadInput();

            double result = Evaluate(input);

            Console.WriteLine(result);
        }

        private static double Evaluate(string[] input)
        {
            Stack<string> operations = new Stack<string>();
            Stack<double> numbers = new Stack<double>();

            foreach (string expression in input)
            {
                bool isNumber = double.TryParse(expression, out double number);

                if (isNumber)
                {
                    numbers.Push(number);
                }
                else
                {
                    if (expression == ")")
                    {
                        string previousOperation = operations.Pop();

                        while (previousOperation != "(")
                        {
                            PerformOperationAndPushResult(numbers, previousOperation);
                            previousOperation = operations.Pop();
                        }
                    }
                    else
                    {
                        if (operations.Count > 0)
                        {
                            string currentOperation = expression;
                            string lastOperation = operations.Peek();

                            while (lastOperation != "(")
                            {
                                int operationWeight = GetOperationWeight(currentOperation);
                                int lastOperationWeight = GetOperationWeight(lastOperation);

                                Func<double, double, bool> comparator = GetOperationsComparator(expression);

                                if (comparator(lastOperationWeight, operationWeight))
                                {
                                    PerformOperationAndPushResult(numbers, lastOperation);
                                    operations.Pop();

                                    if (operations.Count > 0)
                                    {
                                        currentOperation = lastOperation;
                                        lastOperation = operations.Peek();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        operations.Push(expression);
                    }
                }
            }

            while (operations.Count != 0)
            {
                PerformOperationAndPushResult(numbers, operations.Pop());
            }

            return numbers.Peek();
        }

        private static Func<double, double, bool> GetOperationsComparator(string expression)
        {
            bool isLeftAsociative = leftAsociativeOperations.Contains(expression);

            if (isLeftAsociative)
            {
                return (a, b) => a >= b;
            }

            bool isRightAsociative = rightAsociativeOperations.Contains(expression);

            if (isRightAsociative)
            {
                return (a, b) => a > b;
            }

            throw new InvalidOperationException();
        }

        private static void PerformOperationAndPushResult(Stack<double> numbers, string operation)
        {
            double firstNumber = numbers.Pop();
            double secondNumber = numbers.Pop();

            double result = PerformOperation(firstNumber, secondNumber, operation);
            numbers.Push(result);
        }

        private static double PerformOperation(double firstNumber, double secondNumber, string expression)
        {
            switch (expression)
            {
                case "+": return firstNumber + secondNumber;
                case "-": return secondNumber - firstNumber;
                case "*": return firstNumber * secondNumber;
                case "/": return secondNumber / firstNumber;
                case "^": return Math.Pow(secondNumber, firstNumber);

                default:
                    throw new InvalidOperationException();
            }
        }

        private static int GetOperationWeight(string operation)
        {
            switch (operation)
            {
                case "+":
                case "-":
                    return 0;

                case "*":
                case "/":
                    return 1;

                case "^":
                    return 2;

                case ")":
                case "(":
                    return int.MaxValue;

                default:
                    throw new InvalidOperationException();
            }
        }

        private static string[] ReadInput()
        {
            string input = Console.ReadLine();
            Regex whiteSpaceRegex = new Regex("\\s*");
            Regex elementsRegex = new Regex("((\\d+\\.\\d+)|\\d+|[*\\-+\\/^()])");
            return elementsRegex
                .Matches(whiteSpaceRegex.Replace(input, ""))
                .Select(m => m.Value)
                .ToArray();
        }
    }
}