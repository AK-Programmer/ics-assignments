using System;
using System.Collections.Generic;

namespace PASS3
{
    public class Pile
    {
        private Card[] pile;
        private int size;

        private static Card NO_ELEMENT = new Card(-1, Suit.Null);
        private static Random rnd = new Random();

        public Pile(int maxSize)
        {
            pile = new Card[maxSize];
            size = 0;
        }

        public void Push(Card card)
        {
            if(size < pile.Length)
            {
                pile[size] = card;
                size++;
            }
        }

        public Card Pop()
        {
            Card result = NO_ELEMENT;

            if(size > 0)
            {
                result = pile[size - 1];
                size--;

            }
            else
            {
                throw new Exception("No cards in pile.");
            }

            return result;
        }

        public Card Top()
        {
            Card result = NO_ELEMENT;

            if(size > 0)
            {
                result = pile[size - 1];
            }
            else
            {
                throw new Exception("No cards in pile");
            }

            return result;
        }

        public int Size()
        {
            return size;
        }

        public void Shuffle()
        {
            List<int> usedCards = new List<int>();

            Card[] shuffledPile = new Card[pile.Length];
            int randIndex;

            for (int i = 0; i < size; i++)
            {
                while(true)
                {
                    randIndex = rnd.Next(0, size);

                    if(!usedCards.Contains(randIndex))
                    {
                        usedCards.Add(randIndex);
                        shuffledPile[i] = pile[randIndex];
                        break;
                    }
                }
            }

            pile = shuffledPile;
        }


        public void DisplayPile()
        {
            for (int i = 0; i < size; i++)
            {
                switch (pile[i].GetSuit())
                {
                    case Suit.Spades:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case Suit.Clubs:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case Suit.Hearts:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case Suit.Diamonds:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        break;
                }

                Console.Write($"{Program.rankToChar[pile[i].GetRank()]}{Program.suitToChar[pile[i].GetSuit()]}, ");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("");
            
        }
    }
}
