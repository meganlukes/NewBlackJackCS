using System;
using System.Collections.Generic;

namespace NewBlackJackCS
{
    class Card
    {
        public string name { get; set; }
        public string suit { get; set; }
        public int value { get; set; }

        public Card(string cardName, string suitName, int faceValue)
        {
            name = cardName;
            suit = suitName;
            value = faceValue;
        }
    }
    class Program
    {

        static string GreetPlayer()
        {
            Console.WriteLine();
            Console.Write("Welcome to the Casino Royale. What is your name?   ");
            var playerName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine(playerName + "? Hah! Your disguises are getting worse. I know it is you, Mr. Bond! Unfortunately I have not been able to ban you yet, but I'll be watching!");
            Console.WriteLine("In the meantime, I'm obligated to allow you to enjoy the amenities, including the BlackJack tables. Enjoy your game.");
            return playerName;
        }
        public static List<Card> ShuffleDeck(List<Card> newDeck, int deckSize)
        {
            for (var rightIndex = deckSize - 1; rightIndex >= 0; rightIndex--)
            {
                var leftIndex = new Random().Next(0, rightIndex);
                var rightCard = newDeck[rightIndex];
                newDeck[rightIndex] = newDeck[leftIndex];
                newDeck[leftIndex] = rightCard;
            }
            return newDeck;
        }
        public static List<Card> GenerateComputerHand(List<Card> newDeck)
        {
            var computerHand = new List<Card>() { newDeck[0], newDeck[1] };
            return computerHand;
        }
        public static int CalculateHandValue(List<Card> hand)
        {
            int valueOfHand = 0;
            int handSize = hand.Count;
            for (int i = 0; i < handSize; i++)
            {
                valueOfHand = valueOfHand + hand[i].value;
            }
            return valueOfHand;
        }
        public static void DisplayCards(List<Card> hand, int handcount)
        {
            for (int i = 0; i < handcount; i++)
            {
                Console.WriteLine(hand[i].name);
            }
        }


        static void Main(string[] args)
        {
            //Create deck
            var suits = new string[] { "Hearts", "Diamonds", "Clubs", "Spades" };
            var deck = new List<Card>();

            for (var j = 0; j < 4; j++)    //Create face cards and aces
            {
                var kingCard = new Card("King of " + suits[j], suits[j], 10);
                deck.Add(kingCard);
                var queenCard = new Card("Queen of " + suits[j], suits[j], 10);
                deck.Add(queenCard);
                var jackCard = new Card("Jack of " + suits[j], suits[j], 10);
                deck.Add(jackCard);
                var aceCard = new Card("Ace of " + suits[j], suits[j], 11);
                deck.Add(aceCard);

            }
            for (var i = 2; i < 11; i++)    //Create numbered cards
            {
                deck.Add(new Card(i + " of " + suits[0], suits[0], i));
                deck.Add(new Card(i + " of " + suits[1], suits[1], i));
                deck.Add(new Card(i + " of " + suits[2], suits[2], i));
                deck.Add(new Card(i + " of " + suits[3], suits[3], i));
            }
            int deckLength = deck.Count;

            //Greet Player
            GreetPlayer();

            //Begin playing loop
            bool playing = true;
            do
            {
                //shuffle deck
                deck = ShuffleDeck(deck, deckLength);

                //generate computer hand
                var computerHand = GenerateComputerHand(deck);
                var computerHandCount = computerHand.Count;
                //Calculate computer hand value
                int computerHandValue = CalculateHandValue(computerHand);

                //Generate first card in player's hand
                var playerHand = new List<Card>();
                playerHand.Add(deck[3]);

                //set variables
                int playerHandLength = 0;
                int playerHandValue = 0;

                //Hitting loop
                bool hitting = true;
                do
                {
                    //Add card to player hand
                    playerHandLength = playerHand.Count;
                    playerHand.Add(deck[playerHandLength + 3]);
                    playerHandLength = playerHand.Count;
                    //Display cards to player
                    Console.WriteLine();
                    Console.WriteLine("Your cards are: ");
                    DisplayCards(playerHand, playerHandLength);
                    //Calculate hand value
                    playerHandValue = CalculateHandValue(playerHand);
                    //Hit or Stay
                    if (playerHandValue > 21)
                    {
                        hitting = false;
                    }
                    else if (playerHandValue == 21)
                    {
                        hitting = false;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write("Would you like to Hit or Stay?   ");
                        string answer = Console.ReadLine();
                        if (answer == "stay" || answer == "Stay")
                        {
                            hitting = false;
                        }
                    }
                } while (hitting == true);
                if (playerHandValue > 21)
                {
                    Console.WriteLine("I think you've had one too many vodka martinis, Mr. Bond, you've gone bust!");
                }
                else
                {

                    //Display computer hand to player
                    Console.WriteLine();
                    Console.WriteLine("The House holds:");
                    DisplayCards(computerHand, computerHandCount);
                    Console.WriteLine();
                    while (computerHandValue < 17)
                    {
                        Console.WriteLine("House hits.");
                        computerHandCount = computerHand.Count;
                        computerHand.Add(deck[(computerHandCount + playerHandLength)]);
                        computerHandValue = CalculateHandValue(computerHand);
                        computerHandCount = computerHand.Count;
                        Console.WriteLine();
                        Console.WriteLine("House now holds:");
                        DisplayCards(computerHand, computerHandCount);
                        Console.WriteLine();
                    }
                    if (computerHandValue > 21)
                    {
                        Console.WriteLine("House busts. You got lucky this time, Mr. Bond, but luck always runs out!");
                    }
                    //Determine outcome of game
                    //If playerHand>21, display bust message

                    //If computer gets blackjack, display failed blackjack message
                    else if (computerHandValue == 21)
                    {
                        Console.WriteLine("Your little attempt to count cards has failed you, Mr. Bond, house wins with Blackjack!");
                    }
                    //If player gets blackjack but computer doesn't, display blackjack message
                    else if (playerHandValue == 21)
                    {
                        Console.WriteLine("Blackjack?? I knew you've been cheating! Let's see what security has to say about this!");
                    }
                    //If computer>player, display lost message
                    else if (computerHandValue > playerHandValue)
                    {
                        Console.WriteLine("House's " + computerHandValue + " beats your " + playerHandValue + ". Perhaps you should spend less time flirting with the pretty waitress, Mr. Bond, and more time on your cards!");
                    }
                    //If computer=player, display push message
                    else if (computerHandValue == playerHandValue)
                    {
                        Console.WriteLine("Push. If I were you, Mr. Bond, I'd cash out and leave while I still could.");
                    }
                    //if player>computer, display win message
                    else if (computerHandValue < playerHandValue)
                    {
                        Console.WriteLine("You won this hand, Mr. Bond, but mark my words, you will not get away with this card counting scheme of yours!");
                    }
                    //else error
                    else
                    {
                        Console.WriteLine("I'm going to fire that waiter for spilling drinks on the cards!");
                        Console.WriteLine("Thanks to his clumsiness, we'll never know who won this round.");
                    }
                }


                //Wipe playerHand
                playerHand.Clear();
                //Wipe computerHand
                computerHand.Clear();
                //Wipe computerHand value
                computerHandValue = 0;
                //wipe computer hand length
                computerHandCount = 0;

                //Ask if player wishes to play again
                Console.WriteLine();
                Console.WriteLine("Counting cards is not permitted at the Casino Royale and will result in a lifetime ban and your chips being confiscated!");
                Console.WriteLine("Do you want to leave freely, or do you want to stay for another game while we wait for our expert observer to arrive so he can prove you're cheating? (Leave or Stay)    ");
                string response = Console.ReadLine();
                if (response == "Leave" || response == "leave")
                {
                    Console.WriteLine("And never come back, or next time it will be casino security escorting you out!");
                    playing = false;
                }

            } while (playing == true);
        }
    }
}
