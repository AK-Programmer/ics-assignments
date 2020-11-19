//Author: Aadar Kahiri
//File Name: Card.cs
//Project Name: PASS3
//Creation Date: Nov 16, 2020
//Modified Date: Nov 18, 2020
//Description: this class contains the key information defining each card: its rank, suit, and visibility. It also contains get and set methods to view these properties and
// alter some of them. 
namespace PASS3
{
    public class Card
    {
        private readonly int rank;
        private readonly Suit suit;
        private bool isVisible; 
        

        //Pre: rank should be between 0 and 12, and suit should be either Spades, Clubs, Diamonds, or Hearts.
        //Post: none
        //Description: basic constructor for the card class. Sets attributes to the given arguments.
        public Card(int rank, Suit suit)
        {
            this.rank = rank;
            this.suit = suit;
            isVisible = true;
        }

        //Pre: none
        //Post: returns the rank of the card
        //Description: get method for rank.
        public int GetRank()
        {
            return rank;
        }

        //Pre: none
        //Post: returns the suit of the card
        //Description: get method for suit.
        public Suit GetSuit()
        {
            return suit;
        }

        //Pre: none
        //Post: returns the visibility of the card
        //Description: get method for isVisible.

        public bool GetIsVisible()
        {
            return isVisible;
        }

        //Pre: input should be a valid boolean.
        //Post: none
        //Description: set method for isVisible.
        public void SetIsVisible(bool isVisible)
        {
            //Sets the isVisible attribute the the given argument. 
            this.isVisible = isVisible;
        }
    }
}
