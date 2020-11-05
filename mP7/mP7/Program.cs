using System;
using System.Collections.Generic;
using System.IO;
namespace mP7
{
    class Program
    {
        static List<string> expressions = new List<string>();
        static StreamReader reader;
        const string FILE_NAME = "Input.txt";

        static void Main(string[] args)
        {
            CalculateExpression("335+-");
            CalculateExpression("512+4*+3-");
            CalculateExpression("235478+*-++");
            Console.ReadKey();
        }



        public static void GetExpressions()
        {
            string currentLine = "";
            try
            {
                reader = new StreamReader(FILE_NAME);

                do
                {
                    expressions.Add(reader.ReadLine());
                } while (currentLine != "");
            }
            catch(Exception e)
            {
                Console.WriteLine("File could not be read. (Press any key to continue)");
                Console.ReadKey();
            }
        }

        public static void CalculateExpression(string expression)
        { 
            NumberStack stack = new NumberStack(expression.Length);

            double operand1, operand2;

            for(int i = 0; i < expression.Length; i++)
            {
                if (expression[i] - '0' >= 0 && expression[i] - '0' <= 10)
                {
                    stack.Push(expression[i] - '0');
                }
                else
                {
                    operand2 = stack.Pop();
                    operand1 = stack.Pop();

                    switch(expression[i])
                    {
                        case '+':
                            stack.Push(operand1 + operand2);
                            break;
                        case '-':
                            stack.Push(operand1 - operand2);
                            break;
                        case '*':
                            stack.Push(operand1 * operand2);
                            break;
                        case '/':
                            stack.Push(operand1 / operand2);
                            break;
                        default:
                            throw new FormatException("Invalid operator");
                    }
                }
            }
            Console.WriteLine($"{expression} = {stack.Top()}");

        }
    }


}
