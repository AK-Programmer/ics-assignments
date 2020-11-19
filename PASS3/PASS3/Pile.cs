//Author: Aadar Kahiri
//File Name: Pile.cs
//Project Name: PASS3
//Creation Date: Nov 16, 2020
//Modified Date: Nov 18, 2020
//Description: this class is essentially an implementation of a stack of the card type. 


using System;
using System.Collections.Generic;

namespace PASS3
{
    public class Pile
    {
        //Array that will act as the pile (or stack)
        private Card[] pile;
        //attribute to keep track of the size of the pile (or stack). 
        private int size;

        //Random object to generate random numbers.
        private static Random rnd = new Random();


        //Pre: maxSize should be a positive integer
        //Post: None
        //Description: basic constructor for the pile class. Creates an array of length maxSize and sets size to zero.
        public Pile(int maxSize)
        {
            pile = new Card[maxSize];
            size = 0;
        }

        //Pre: given card should not be a duplicate of a card already in the pile. 
        //Post: none
        //Description: this method adds a card to the top of the pile. 
        public void Push(Card card)
        {
            //If there is room for another card, add it and increment the size.
            if(size < pile.Length)
            {
                pile[size] = card;
                size++;
            }
        }

        //Pre: none
        //Post: returns the card  at the top of the pile
        //Description: this method returns the card and the top of the pile and then removes it from the pile. If the pile is empty, it throws an exception. 
        public Card Pop()
        {
            //Default card to return. 
            Card result;

            //If there is actually a card to pop, set result to that card and decrement the size.
            if(size > 0)
            {
                result = pile[size - 1];
                size--;

            }
            else
            {
                //If there are no cards to pop, throw an exception.
                throw new Exception("No cards in pile.");
            }

            return result;
        }

        //Pre: none.
        //Post: returns the card at the top of the pile.
        //Description: this method returns the card at the top of the pile. If the pile is empty, it throws an exception. 
        public Card Top()
        {
            Card result;

            //If there is actually a card to return, set result to 
            if(size > 0)
            {
                result = pile[size - 1];
            }
            else
            {
                //If there are no cards to pop, throw an exception
                throw new Exception("No cards in pile");
            }

            return result;
        }


        //Pre: none
        //Post: returns the size of the pile
        //Description: returns the size of the pile.
        public int Size()
        {
            return size;
        }

        //Pre: none
        //Post: none
        //Description: randomly changes the order of the cards in the pile
        public void Shuffle()
        {
            //A list to keep track of cards that have already been added to the shuffled pile.
            List<int> usedCards = new List<int>();

            //A new array of cards to store the shuffled pile
            Card[] shuffledPile = new Card[pile.Length];

            //variable that will store the randomly generated indices
            int randIndex;

            //For each card in the pile, generate a random index. If that index hasn't already been used, add the card at that index to the shuffledPile.
            for (int i = 0; i < size; i++)
            {
                //While loop to ensure this process repeats until a random number is found. 
                while(true)
                {
                    //Generate a random number
                    randIndex = rnd.Next(0, size);

                    //if that number isn't in usedCards, add it to usedCards, add the card at that index to shuffledPile, and break.
                    if(!usedCards.Contains(randIndex))
                    {
                        usedCards.Add(randIndex);
                        shuffledPile[i] = pile[randIndex];
                        break;
                    }
                }
            }

            //Set pile to the shuffled pile. 
            pile = shuffledPile;
        }

        //Pre: none
        //Post: none
        //Description: this method clears the pile by instantiating it again
        public void ClearPile()
        {
            size = 0;
            pile = new Card[pile.Length];
        }
    }
}
