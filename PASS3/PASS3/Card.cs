using System;
using System.Collections.Generic;

namespace PASS3
{
    public class Card
    {
        private readonly int rank;
        private readonly Suit suit;
        private bool isVisible; 
        

        public Card(int rank, Suit suit)
        {
            this.rank = rank;
            this.suit = suit;
            isVisible = true;
        }

        public int GetRank()
        {
            return rank;
        }

        public Suit GetSuit()
        {
            return suit;
        }

        public bool GetIsVisible()
        {
            return isVisible;
        }

        public void SetIsVisible(bool isVisible)
        {
            this.isVisible = isVisible;
        }
    }
}
