//Author: Adar Kahiri
//File Name: CharQueue.cs
//Project Name: PASS4
//Creation Date: Jan 25, 2021
//Modified Date: Jan 25, 2021
//Description: a basic queue for chars. It will be used to store and execute the command sequences that the player enters.
using System;
using System.Collections.Generic;

namespace PASS4
{
    public class CharQueue
    {
        List<char> queue = new List<char>();

        //Pre: none
        //Post: none
        //Description: basic constructor
        public CharQueue()
        {

        }

        //Pre: any character
        //Post: none
        //Description: Adds a character to the queue
        public void Enqueue (char newChar)
        {
            queue.Add(newChar);
        }

        //Pre: none
        //Post: returns either the first char in the queue or null
        //Description: returns the first char in the queue and removes it from the queue, or returns null if the queue is empty.
        public char? Dequeue()
        {
            char? result = null;

            if(queue.Count > 0)
            {
                result = queue[0];
                queue.RemoveAt(0);
            }

            return result;
        }

        //Pre: none
        //Post: returns the first char in the queue if the queue is not empty. Otherwise it returns null.
        //Description: returns the first char in the queue without removing it from the queue.
        public char? Peek()
        {
            char? result = null;

            if(queue.Count > 0)
            {
                result = queue[0];
            }

            return result;
        }

        //Pre: none
        //Post: returns the size of the queue
        //Description: returns the size of the queue
        public int Size()
        {
            return queue.Count;
        }

        //Pre: none
        //Post: returns a boolean
        //Description: returns true if the queue is empty, and false otherwise
        public bool IsEmpty()
        {
            return queue.Count == 0;
        }
    }
}
