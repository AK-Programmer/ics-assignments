//Author: Adar Kahiri
//File Name: SequenceQueue.cs
//Project Name: mP9
//Creation Date: Nov. 13, 2020
//Modified Date: Nov. 13, 2020
//Description: An implementation of an character queue. Used to store the sequence of moves the user would like to attempt.

using System;
using System.Collections.Generic;

namespace mP9
{
    public class SequenceQueue
    {
        List<char> queue = new List<char>();



        //Pre: char must be one of "wasd"
        //Post: None
        //Description: Add player's move to the back of the queue
        public void Enqueue(char move)
        {
            //If the char is not one of "wasd" throw an exception
            if (move != 'w' && move != 'a' && move != 's' && move != 'd')
            {
                throw new ArgumentException("This move is invalid");
            }

            queue.Add(move);
        }

        //Pre: none
        //Post: returns the front element of the queue
        //Description: returns and removes the element at the front of the queue. Returns null if there are no elements.
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

        //Pre: None
        //Post: Returns the front element of the queue
        //Description: returns the element at the front of the queue, null if none exists
        public char? Peek()
        {
            char? result = null;

            if (queue.Count > 0)
            {
                result = queue[0];
            }

            return result;
        }

        //Pre: none
        //Post: returns the length/size of the queue
        //Description: returns the length/size of the queue
        public int Size()
        {
            return queue.Count;
        }


        //Pre: none
        //Post: none
        //Description: clears the entire sequence
        public void Clear()
        {
            queue.Clear();
        }
    }
}
