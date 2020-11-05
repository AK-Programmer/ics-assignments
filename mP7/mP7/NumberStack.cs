//Author: Adar Kahiri
//File Name: NumberStack.cs
//Project Name: SimpleStack
//Creation Date: Nov. 3, 2020
//Modified Date: Nov. 3, 2020
//Description: An implementation of a primitive data type stack

using System;

class NumberStack
{
    //Maintain the only two attributes of a stack, itself and its size
    private double[] stack;
    private int size;

    //A public constant to used to check if a returned value is a bad result
    public const double NO_ELEMENT = Double.MinValue;

    public NumberStack(int maxSize)
    {
        //Question: Is maxSize still needed if we implement the stack using a List?

        //Instantiate the stack to a specified size with no elements
        stack = new double[maxSize];
        size = 0;
    }

    //Pre: num is a valid integer
    //Post: None
    //Description: Add num to the stack if there is room
    public void Push(double num)
    {
        if (size < stack.Length)
        {
            stack[size] = num;
            size++;
        }
    }

    //Pre: None
    //Post: Returns the top element of the stack
    //Description: returns and removes the element on the top of the stack, NO_ELEMENT if
    //             none exists
    public double Pop()
    {
        double result = NO_ELEMENT;

        if (!IsEmpty())
        {
            result = stack[size - 1];
        }

        //For an array implementation of a primitive data type stack,
        //this is what deletes the element. We simply pretend it no longer exists
        size--;

        return result;
    }

    //Pre: None
    //Post: Returns the top element of the stack
    //Description: returns the element on the top of the stack, NO_ELEMENT if
    //             none exists
    public double Top()
    {
        double result = NO_ELEMENT;

        if (!IsEmpty())
        {
            result = stack[size - 1];
        }

        return result;
    }

    public void DisplayStack()
    {
        //For each index in the stack that actually has a value, print the value. This starts from the 'top' of the stack.
        for (int i = size - 1; i >= 0; i--)
        {
            Console.Write(stack[i] + "\t");
        }
        Console.WriteLine();
    }

    //Pre: None
    //Post: Returns the current size of the stack
    //Description: Returns the current number of elements on the stack
    public int Size()
    {
        return size;
    }

    //Pre: None
    //Post: Returns true if the size of the stack is 0, false otherwise
    //Description: Compare the size of the stack against 0 to determine its empty status
    public bool IsEmpty()
    {
        //returns the result of the comparison between size and 0
        return size == 0;
    }
}