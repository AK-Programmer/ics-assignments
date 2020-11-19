//Author: Aadar Kahiri
//File Name: Program.cs
//Project Name: PASS3
//Creation Date: Nov 16, 2020
//Modified Date: Nov 18, 2020
//Description: this class handles the primary flow of the game (War). It prompts the users to flip their cards, handles wars, and checks for wins, among other things.


using System;
using System.Collections.Generic;

namespace PASS3
{
    //This enum makes it more clear which suit a card is
    public enum Suit
    {
        Spades,
        Clubs,
        Hearts,
        Diamonds,
        Null
    }

    class Program
    {
        //Dictionary to map each suit to its unicode character
        static Dictionary<Suit, char> suitToChar = new Dictionary<Suit, char>()
        {
            {Suit.Spades, '\u2660'},
            {Suit.Clubs, '\u2663'},
            {Suit.Hearts, '\u2665'},
            {Suit.Diamonds, '\u2666'},
            {Suit.Null, '?'}
        };

        //Dictionary to map a cards numerical rank to its rank as a string (e.g., it maps 11 to Q, or Queen). 
        static Dictionary<int, string> rankToChar = new Dictionary<int, string>(){{0, "1"},{1, "2"},{2, "3"},{3, "4"},{4, "5"},{5, "6"},{6, "7"},{7, "8"},{8, "9"},{9, "10"},{10, "J"},{11, "Q"},{12, "K"},{-1, "?"}};

        const ConsoleColor player1Color = ConsoleColor.Cyan;
        const ConsoleColor player2Color = ConsoleColor.Magenta;

        const int NUM_CARDS_IN_DECK = 52;
        const int NUM_CARDS_IN_SUIT = 13;


        static void Main(string[] args)
        {
            //Defining 2 piles per player
            //0 is the library, 1 is the discard pile, and 2 is the playing area
            Pile[] player1Piles = new Pile[2];
            Pile[] player2Piles = new Pile[2];
            //Defining the playing areas of each player
            List<Card> p1PlayArea = new List<Card>();
            List<Card> p2PlayArea = new List<Card>();

            //Initializing the piles of each player 
            for(int i = 0; i < 2; i++)
            {
                //The maximum number of cards a pile could contain is set to 52.
                player1Piles[i] = new Pile(NUM_CARDS_IN_DECK);
                player2Piles[i] = new Pile(NUM_CARDS_IN_DECK);
            }

            PlayGame(ref player1Piles, ref player2Piles, ref p1PlayArea, ref p2PlayArea);
        }

        //Pre: must pass references to each of the players' piles and playing areas. Player Piles should have only 2 elements (library and discard)
        //Post: none
        //Description: this method handles the majority of the logic of the game. It prompts each of the players to flip their cards, handles wars, declares winners, and more.
        public static void PlayGame(ref Pile[] player1Piles, ref Pile[] player2Piles, ref List<Card> p1PlayArea, ref List<Card> p2PlayArea)
        {
            //Resetting the piles before each game
            ResetGame(ref player1Piles, ref player2Piles, ref p1PlayArea, ref p2PlayArea);

            //While neither have the players have zero cards in *both* of their libraries, continue playing
            while((player1Piles[0].Size() != 0 || player1Piles[1].Size() != 0) && (player2Piles[0].Size() != 0 || player2Piles[1].Size() != 0))
            {
                //Transferring all the cards in the discard pile to the library (only if necessary)
                FromDiscardToLib(ref player1Piles);
                FromDiscardToLib(ref player2Piles);

                //Drawing the game
                Console.Clear();
                DrawGame(ref player1Piles, ref player2Piles, ref p1PlayArea, ref p2PlayArea);

                //Prompting the user to press enter
                Console.WriteLine("Press ENTER to flip the next card.");
                Console.ReadLine();

                //variables to store the card ranks of each of the flipped cards.
                int p1CardRank, p2CardRank;

                p1CardRank = player1Piles[0].Top().GetRank();
                p2CardRank = player2Piles[0].Top().GetRank();

                //Flipping the cards at the top of each player's library and adding to their play area
                p1PlayArea.Add(player1Piles[0].Pop());
                p2PlayArea.Add(player2Piles[0].Pop());

                //Re-drawing the game to reflect changes
                Console.Clear();
                DrawGame(ref player1Piles, ref player2Piles, ref p1PlayArea, ref p2PlayArea);

                //If player 1's card has higher rank, indicate that they have won this round
                if(p1CardRank > p2CardRank)
                {
                    //For each card in a player's play area, set its visibility to true and add it to the discard pile
                    for(int i = 0; i < p1PlayArea.Count; i++)
                    {
                        p1PlayArea[i].SetIsVisible(true);
                        p2PlayArea[i].SetIsVisible(true);

                        player1Piles[1].Push(p1PlayArea[i]);
                        player1Piles[1].Push(p2PlayArea[i]);
                    }

                    //Clear both of the playing areas since those cards have just been moved to discard
                    p1PlayArea.Clear();
                    p2PlayArea.Clear();

                    Console.ForegroundColor = player1Color;
                    Console.Write("Player 1 ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("wins this round!");
                    Console.WriteLine("Press ENTER to continue...");
                    Console.ReadLine();
                }
                //If player 2's card has higher rank, indicate that they have won this round
                else if (p1CardRank < p2CardRank)
                {
                    //For each card in a player's play area, set its visibility to true and add it to the discard pile
                    for (int i = 0; i < p2PlayArea.Count; i++)
                    {
                        p1PlayArea[i].SetIsVisible(true);
                        p2PlayArea[i].SetIsVisible(true);

                        player2Piles[1].Push(p1PlayArea[i]);
                        player2Piles[1].Push(p2PlayArea[i]);
                    }

                    //Clear both of the playing areas since those cards have just been moved to discard
                    p1PlayArea.Clear();
                    p2PlayArea.Clear();

                    Console.ForegroundColor = player2Color;
                    Console.Write("Player 2 ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("wins this round!");
                    Console.WriteLine("Press ENTER to continue...");
                    Console.ReadLine();
                }
                //If neither card is higher than the other, then they are equal in rank and we have a war. 
                else
                {
                    //If player 1 has no cards left in their library to place face down
                    if(player1Piles[0].Size() == 0)
                    {
                        //For each card in a player's play area, set its visibility to true and add it to the discard pile
                        for (int i = 0; i < p2PlayArea.Count; i++)
                        {
                            p1PlayArea[i].SetIsVisible(true);
                            p2PlayArea[i].SetIsVisible(true);

                            player2Piles[1].Push(p1PlayArea[i]);
                            player2Piles[1].Push(p2PlayArea[i]);
                        }

                        //Clear both of the playing areas since those cards have just been moved to discard
                        p1PlayArea.Clear();
                        p2PlayArea.Clear();

                        Console.ForegroundColor = player2Color;
                        Console.Write("Player 2 ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("has won this war! (Press ENTER to continue).");
                        Console.Read();
                    }
                    //If player player 2 has no cards left in their library to place face down, player 1 wins and collects all cards in player 2's playing area
                    else if (player2Piles[0].Size() == 0)
                    {
                        //For each card in a player's play area, set its visibility to true and add it to the discard pile
                        for (int i = 0; i < p2PlayArea.Count; i++)
                        {
                            p1PlayArea[i].SetIsVisible(true);
                            p2PlayArea[i].SetIsVisible(true);

                            player2Piles[1].Push(p1PlayArea[i]);
                            player2Piles[1].Push(p2PlayArea[i]);
                        }

                        //Clear both of the playing areas since those cards have just been moved to discard
                        p1PlayArea.Clear();
                        p2PlayArea.Clear();

                        Console.ForegroundColor = player1Color;
                        Console.Write("Player 1 ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("has won this war! (Press ENTER to continue).");
                        Console.Read();
                    }
                    //If both players have a card to place face down, place a card face down for each and declare a war
                    else
                    {
                        //Draw the top card of the library face down, and put it in the playing area
                        player1Piles[0].Top().SetIsVisible(false);
                        p1PlayArea.Add(player1Piles[0].Pop());

                        player2Piles[0].Top().SetIsVisible(false);
                        p2PlayArea.Add(player2Piles[0].Pop());

                        //Declare a war
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("WAR!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press ENTER to continue...");
                        Console.ReadLine();
                    }
                }
            }

            //If both of player 1's piles are empty, player 2 has won. 
            if(player1Piles[0].Size() == 0 && player1Piles[1].Size() == 0)
            {
                Console.ForegroundColor = player2Color;
                Console.Write("Player 2 ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("has won the game! Would you like to play again? \n('y' for yes, anything else for no.)");
            }
            //Otherwise, player 1 has one. 
            else
            {
                Console.ForegroundColor = player1Color;
                Console.Write("Player 1 ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("has won the game! Would you like to play again? \n('y' for yes, anything else for no.)");
            }

            //If the user enters 'y', call PlayGame again to play again. Otherwise, quit. 
            if(Console.ReadLine() == "y")
            {
                PlayGame(ref player1Piles, ref player2Piles, ref p1PlayArea, ref p2PlayArea);
            }
        }

        //Pre: must pass references to each of the players' piles and playing areas. Player Piles should have only 2 elements (library and discard)
        //Post: none
        //Description: this method resets the game. it clears all piles and playing areas, shuffles the deck, and deals 26 cards to each player. 
        public static void ResetGame(ref Pile[] player1Piles, ref Pile[] player2Piles, ref List<Card> p1PlayArea, ref List<Card> p2PlayArea)
        {
            //Clearing the playing area
            p1PlayArea.Clear();
            p2PlayArea.Clear();

            //Clears all piles
            player1Piles[0].ClearPile();
            player1Piles[1].ClearPile();
            player2Piles[0].ClearPile();
            player2Piles[1].ClearPile();

            //Define a new pile that will store all cards
            Pile pile = new Pile(NUM_CARDS_IN_DECK);

            //Add all 52 cards to the deck
            for (int i = 0; i < NUM_CARDS_IN_DECK; i++)
            {
                // i%13 ensures cards only have ranks between 0 and 12. i/13 ensures that there is exactly one card of each rank per suit.
                //The first 13 cards will be of suit 0 (casting to Suit makes it Spades), the next 13 will be of suit 1, and so on. 
                pile.Push(new Card(i % NUM_CARDS_IN_SUIT, (Suit)(i / NUM_CARDS_IN_SUIT)));
            }

            //Shuffling the deck
            pile.Shuffle();

            //Deal 26 cards to each player.
            for(int i = 0; i <  26; i++)
            {
                player1Piles[0].Push(pile.Pop());
                player2Piles[0].Push(pile.Pop());
            }
        }

        //Pre: must pass references to each of the players' piles and playing areas. Player Piles should have only 2 elements (library and discard)
        //Post: none
        //Description: this method draws the game: the number of cards in player piles, and the cards in the play area.
        public static void DrawGame(ref Pile[] player1, ref Pile[] player2, ref List<Card> p1PlayArea, ref List<Card> p2PlayArea)
        {
            Console.Clear();
            Console.ForegroundColor = player1Color;
            Console.Write($"P1-Library: {player1[0].Size()} \t P1-Discards: {player1[1].Size()}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" || ");
            Console.ForegroundColor = player2Color;
            Console.WriteLine($"P2-Library: {player2[0].Size()} \t P2-Discards: {player2[1].Size()}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Player 1: ");
            DisplayPlayingArea(p1PlayArea);
            Console.Write($"Player 2: ");
            DisplayPlayingArea(p2PlayArea);
        }

        //Pre: No real requirements. Playing area should be a list of the specified type (card).
        //Post: none
        //This method displays the given playing area
        public static void DisplayPlayingArea(List<Card> playingArea)
        {
            foreach(Card card in playingArea)
            {
                

                //If the card isn't visible, simply display two yellow asterisks. 
                if(!card.GetIsVisible())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"** ");
                }
                //Otherwise, set the colour of the card and display it.
                else
                {
                    //Setting the colour depending on the suit of the card
                    switch (card.GetSuit())
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
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }

                    //Displaying the rank and suit of the card.
                    Console.Write($"{rankToChar[card.GetRank()]}{suitToChar[card.GetSuit()]} ");
                }
            }

            //Resetting the colour to white
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }

        //Pre: playerPile should have only 2 elements
        //Post: none
        //Description: this method checks if a player has no cards left in their library. If so, it transfers all cards from their discard pile to their library.
        public static void FromDiscardToLib(ref Pile[] playerPile)
        {
            //If a player has no cards left in their library, move all their cards from discard to the library. 
            if (playerPile[0].Size() == 0)
            {
                //Record the initial size of the discard pile
                int discardSize = playerPile[1].Size();

                //For each of the cards in the discard pile, pop it from the discard pile and push it to the library.
                for (int i = 0; i < discardSize; i++)
                {
                    playerPile[0].Push(playerPile[1].Pop());
                }

                //After moving all cards, shuffle the library.
                playerPile[0].Shuffle();
            }
        }
    }
}
