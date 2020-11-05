//Author: Adar Kahiri
//File Name: NumberStack.cs
//Project Name: SimpleStack
//Creation Date: Nov. 4, 2020
//Modified Date: Nov. 4, 2020
//Description: An implementation of in-fix addition using stacks.

using System;
using System.Collections.Generic;
using System.IO;
namespace mP7
{
    class Program
    {
        //List that will store all the expressions read from the file
        static List<string> expressions = new List<string>();
        static StreamReader reader;
        const string FILE_NAME = "Input.txt";


        static void Main(string[] args)
        {
            GetExpressions();

            foreach (string expression in expressions)
            {
                CalculateExpression(expression);
            }

        }


        //Pre: none
        //Post: none
        //Description: This expression reads all the expressions in Input.txt and adds each of them to the expressions List.
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

        //Pre: expression must be a valid expression. 
        //Post: none
        public static void CalculateExpression(string expression)
        {
            //The stack that will store numbers and 'pre-results' while operations are being carried out
            NumberStack stack = new NumberStack(expression.Length);

            //Store the top 2 values in the stack
            double operand1, operand2;

            //For each character in the expression, determine whether it's an operand or operator. If the former, add it to the stack. If the latter, operate on the top 2 values in the stack.
            for(int i = 0; i < expression.Length; i++)
            {
                //for two chars a and b, a - b gives the integer difference of their unicode value.
                //Since the 10 digits have consecutive values in unicode, subtracting '0' from a digit gives its integer value.
                //If the unicode difference is between zero and 10, expression[i] is an integer so push it to the stack
                if (expression[i] - '0' >= 0 && expression[i] - '0' <= 10)
                {
                    stack.Push(expression[i] - '0');
                }
                //Otherwise, expression[i] is either an operator or invalid character.
                else
                {
                    //Setting the values of the 2 operands and removing them from stack.
                    operand2 = stack.Pop();
                    operand1 = stack.Pop();

                    //Carry out the operation that expression[i] represents
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
                        //If expression[i] is none of these, the expression is invalid, so throw an exception. 
                        default:
                            throw new FormatException("Invalid operator");
                    }
                }
            }
            Console.WriteLine($"{expression} = {stack.Top()}");

        }
    }


}
