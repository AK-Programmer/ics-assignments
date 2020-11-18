using System;
using System.Collections.Generic;
using System.Threading;

namespace PASS3
{
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
        public static Dictionary<Suit, char> suitToChar = new Dictionary<Suit, char>()
        {
            {Suit.Spades, '\u2660'},
            {Suit.Clubs, '\u2663'},
            {Suit.Hearts, '\u2665'},
            {Suit.Diamonds, '\u2666'},
            {Suit.Null, '?'}
        };

       public static Dictionary<int, string> rankToChar = new Dictionary<int, string>(){{0, "1"},{1, "2"},{2, "3"},{3, "4"},{4, "5"},{5, "6"},{6, "7"},{7, "8"},{8, "9"},{9, "10"},{10, "J"},{11, "Q"},{12, "K"},{-1, "?"}};


        static void Main(string[] args)
        {
            //0 is the library, 1 is the discard pile, and 2 is the playing area
            Pile[] player1Piles = new Pile[2];
            Pile[] player2Piles = new Pile[2];
            List<Card> p1PlayArea = new List<Card>();
            List<Card> p2PlayArea = new List<Card>();

            for(int i = 0; i < 2; i++)
            {
                player1Piles[i] = new Pile(52);
                player2Piles[i] = new Pile(52);
            }

            PlayGame(ref player1Piles, ref player2Piles, ref p1PlayArea, ref p2PlayArea);


        }

        public static void PlayGame(ref Pile[] player1Piles, ref Pile[] player2Piles, ref List<Card> p1PlayArea, ref List<Card> p2PlayArea)
        {
            ResetGame(ref player1Piles, ref player2Piles, ref p1PlayArea, ref p2PlayArea);

            while((player1Piles[0].Size() != 0 || player1Piles[1].Size() != 0) && (player2Piles[0].Size() != 0 || player2Piles[1].Size() != 0))
            {
                FromDiscardToLib(ref player1Piles);
                FromDiscardToLib(ref player2Piles);

                Console.Clear();
                DrawGame(ref player1Piles, ref player2Piles, ref p1PlayArea, ref p2PlayArea);

                Console.WriteLine("Press ENTER to flip the next card.");
                Console.ReadLine();

                int p1CardRank, p2CardRank;

                p1CardRank = player1Piles[0].Top().GetRank();
                p2CardRank = player2Piles[0].Top().GetRank();

                p1PlayArea.Add(player1Piles[0].Pop());
                p2PlayArea.Add(player2Piles[0].Pop());


                Console.Clear();
                DrawGame(ref player1Piles, ref player2Piles, ref p1PlayArea, ref p2PlayArea);

                if(p1CardRank > p2CardRank)
                {
                    for(int i = 0; i < p1PlayArea.Count; i++)
                    {
                        p1PlayArea[i].SetIsVisible(true);
                        p2PlayArea[i].SetIsVisible(true);
                        player1Piles[1].Push(p1PlayArea[i]);
                        player1Piles[1].Push(p2PlayArea[i]);
                    }
                    
                    p1PlayArea.Clear();
                    p2PlayArea.Clear();

                    Console.WriteLine("Player 1 wins this round!");
                    Console.WriteLine("Press ENTER to continue...");
                    Console.ReadLine();
                }
                else if (p1CardRank < p2CardRank)
                {

                    for (int i = 0; i < p2PlayArea.Count; i++)
                    {
                        p1PlayArea[i].SetIsVisible(true);
                        p2PlayArea[i].SetIsVisible(true);

                        player2Piles[1].Push(p1PlayArea[i]);
                        player2Piles[1].Push(p2PlayArea[i]);
                    }

                    p1PlayArea.Clear();
                    p2PlayArea.Clear();

                    Console.WriteLine("Player 2 wins this round!");
                    Console.WriteLine("Press ENTER to continue...");
                    Console.ReadLine();
                }
                else
                {
                    try
                    {
                        FromDiscardToLib(ref player1Piles);
                        FromDiscardToLib(ref player2Piles);

                        player1Piles[0].Top().SetIsVisible(false);
                        p1PlayArea.Add(player1Piles[0].Pop());

                        player2Piles[0].Top().SetIsVisible(false);
                        p2PlayArea.Add(player2Piles[0].Pop());

                        Console.WriteLine("WAR!");
                        Console.WriteLine("Press ENTER to continue...");
                        Console.ReadLine();
                    }
                    catch(Exception)
                    {
                        break;
                    }
                }
            }

            if(player1Piles[0].Size() != 0 || player1Piles[1].Size() != 0)
            {
                Console.WriteLine("Player 2 has won the game! Would you like to play again?  ('y' for yes, anything else for no.)");
            }
            else
            {
                Console.WriteLine("Player 1 has won the game! Would you like to play again?  ('y' for yes, anything else for no.)");
            }

            if(Console.ReadLine() == "y")
            {
                PlayGame(ref player1Piles, ref player2Piles, ref p1PlayArea, ref p2PlayArea);
            }
            
        }

        public static void ResetGame(ref Pile[] player1Piles, ref Pile[] player2Piles, ref List<Card> p1PlayArea, ref List<Card> p2PlayArea)
        {
            p1PlayArea.Clear();
            p2PlayArea.Clear();

            Pile pile = new Pile(52);

            //Getting all cards
            for (int i = 0; i < 52; i++)
            {
                pile.Push(new Card(i % 13, (Suit)(i / 13)));
            }
            //Shuffling the deck
            pile.Shuffle();

            //Deal cards to each player
            for(int i = 0; i <  26; i++)
            {
                player1Piles[0].Push(pile.Pop());
                player2Piles[0].Push(pile.Pop());
            }
        }

        public static void DrawGame(ref Pile[] player1, ref Pile[] player2, ref List<Card> p1PlayArea, ref List<Card> p2PlayArea)
        {
            Console.Clear();
            Console.WriteLine($"P1-Library: {player1[0].Size()} \t P1-Discards: {player1[1].Size()} || P2-Library: {player2[0].Size()} \t P2-Discards: {player2[1].Size()}");
            Console.Write($"Player 1: ");
            DisplayPlayingArea(p1PlayArea);
            Console.Write($"Player 2: ");
            DisplayPlayingArea(p2PlayArea);
        }

        public static void DisplayPlayingArea(List<Card> playingArea)
        {
            foreach(Card card in playingArea)
            {
                switch(card.GetSuit())
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

                if(!card.GetIsVisible())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"** ");
                }
                else
                {
                    Console.Write($"{rankToChar[card.GetRank()]}{suitToChar[card.GetSuit()]} ");
                }

                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("");
        }


        public static void FromDiscardToLib(ref Pile[] playerPile)
        {
            int discardSize = playerPile[1].Size();

            if (playerPile[0].Size() == 0)
            {
                for(int i = 0; i < discardSize; i++)
                {
                    playerPile[0].Push(playerPile[1].Pop());
                }

                playerPile[0].Shuffle();
            }
        }


    }
}
