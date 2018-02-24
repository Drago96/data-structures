using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LinearDataStructures.Tasks
{
    public static class EvaluateExpression
    {
        public static void Solve()
        {
            string[] input = ReadInput();

            int result = Evaluate(input);

            Console.WriteLine(result);
        }

        private static int Evaluate(string[] input)
        {
            Stack<string> operations = new Stack<string>();
            Stack<int> numbers = new Stack<int>();

            foreach (string expression in input)
            {
                bool isNumber = int.TryParse(expression, out int number);

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
                        if (expression != "(")
                        {
                            if (operations.Count > 0)
                            {
                                int operationWeight = GetOperationWeight(expression);

                                string lastOperation = operations.Peek();
                                int lastOperationWeight = GetOperationWeight(operations.Peek());

                                while (lastOperationWeight >= operationWeight && lastOperation != "(")
                                {
                                    PerformOperationAndPushResult(numbers, lastOperation);
                                    operations.Pop();

                                    if (operations.Count > 0)
                                    {
                                        lastOperation = operations.Peek();
                                        lastOperationWeight = GetOperationWeight(lastOperation);
                                    }
                                    else
                                    {
                                        lastOperationWeight = -1;
                                    }
                                }
                            }
                        }
                        operations.Push(expression);
                    }
                }
            }

            while (operations.Count != 0)
            {
                PerformOperationAndPushResult(numbers,operations.Pop());
            }

            return numbers.Peek();
        }

        private static void PerformOperationAndPushResult(Stack<int> numbers, string operation)
        {
            int firstNumber = numbers.Pop();
            int secondNumber = numbers.Pop();

            int result = PerformOperation(firstNumber, secondNumber, operation);
            numbers.Push(result);
        }

        private static int PerformOperation(int firstNumber, int secondNumber, string expression)
        {
            switch (expression)
            {
                case "+": return firstNumber + secondNumber;
                case "-": return firstNumber - secondNumber;
                case "*": return firstNumber * secondNumber;
                case "/": return secondNumber / firstNumber;
                case "^": return (int)Math.Pow(secondNumber, firstNumber);

                default:
                    throw new InvalidOperationException();
            }
        }

        private static string[] ReadInput()
        {
            string input = Console.ReadLine();
            Regex whiteSpaceRegex = new Regex("\\s*");
            Regex elementsRegex = new Regex("(\\d+|.)");
            input = whiteSpaceRegex.Replace(input, "");
            return elementsRegex.Matches(input).Select(m => m.Value).ToArray();
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
                    return 3;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
