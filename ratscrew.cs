using System;
using System.Collections.Generic;
using cards;
namespace ratscrew
{
	public class game
	{
		public static Deck deck;
		public static Player[] players;

		public static int turn;
		public static int numtilturn;
		public static int prevnumtilturn;
		public static int takepot;
		public static Card temp;

		public static List<Card> pot;

		public static void Main()
		{
			/*Console.WriteLine((char)0x2665);//heart
			Console.WriteLine((char)0x2660);//spades
			Console.WriteLine((char)0x2666);//diamond
			Console.WriteLine((char)0x2663);//clubs*/

			StartGame();
			MainGameLoop();
		}
		public static void StartGame()
		{
			/*Player p1 = new Player("Player1");
			char c = Console.ReadKey().KeyChar;
			Console.WriteLine("char is " + c);*/
			players = new Player[2];
			
			Console.WriteLine("Enter Your name, Player one: ");
			players[0] = new Player(Console.ReadLine());
			Console.WriteLine("Enter Your name, Player two: ");
			players[1] = new Player(Console.ReadLine());
			Console.WriteLine("Welcome " + players[0].name + " and "
																	 + players[1].name + "!");
			
			deck = new Deck();
			deck.shuffle();

			for(int i = 0; i < 52; i++)
				if(i%2==0)
					players[0].draw(deck);
				else
					players[1].draw(deck);

		}
		public static void MainGameLoop()
		{
			Console.WriteLine("Player one start! (controls " +
				"z:P1 flip, x:P1 slap, .:P2 flip, /:P2 slap)");
			char input;
			turn = 0;
			numtilturn = 0;
			prevnumtilturn = numtilturn;
			takepot = -1;
			pot = new List<Card>();
			do
			{
				if( turn > 1 )
					turn = 0;
				input = Console.ReadKey().KeyChar;
				//Console.Clear();
				Console.WriteLine();
				if(numtilturn > 0)
					Console.WriteLine((numtilturn-1) + " cards left...");
				switch(input)
				{
					case 'z':
						playertakepot();
						flipCard(0);
						break;
					case 'x':
						slapCard(0);
						playertakepot();
						break;
					case '/':
						playertakepot();
						flipCard(1);
						break;
					case '.':
						slapCard(1);
						playertakepot();
						break;
					default: 
						playertakepot();
						break;
				}
				for(int i = 0; i<2;i++)
					Console.WriteLine(players[i].name +" cards: " + players[i].hand.Count);
				
			}
			while(true);
		}
		public static void playertakepot()
		{
			if(takepot >= 0)
			{
				players[takepot].hand.AddRange(pot);
				pot = new List<Card>();
				Console.WriteLine(players[takepot].name + " takes the pot!");
				takepot = -1;
			}
		}
		public static void KillGame(){}

		public static int isface(Card card)
		{
			switch(card.val)
			{
				case 1:return 4;
				case 11: return 1;
				case 12: return 2;
				case 13: return 3;
				default: return 0;
			}
		}
		public static bool flipCard(int index)
		{
			if(turn == index)
			{
				temp = players[index].discard(0);
				if(temp!=null)
				{
					pot.Add(temp);
					Console.WriteLine(temp.flavor());
					prevnumtilturn = numtilturn;
					numtilturn--;
					int numtiltemp = isface(temp);
					if(numtiltemp > 0)
					{
						prevnumtilturn=numtiltemp;
						numtilturn = numtiltemp;
						turn++;
					}
					else if(numtilturn <= 0)
					{
						turn++;
						if(prevnumtilturn > 0)
							takepot = mod(index-1, 2);
					}
				}
				else
				{
					Console.WriteLine(players[mod(index-1,2)].name + " wins!");
					return true;
				}
			}
			return false;
		}
		public static void slapCard(int index)
		{
			if(pot.Count > 1)
			{
				if(pot[pot.Count-1].val == pot[pot.Count-2].val)
				{
					players[index].hand.AddRange(pot);
					pot = new List<Card>();
					Console.Write(players[index].name + " took the pot!\n");
					takepot = -1;
					numtilturn = 0;
					prevnumtilturn = 0;
					return;
				}
				if(pot.Count > 2)
					if(pot[pot.Count-1].val == pot[pot.Count-3].val)
					{
						players[index].hand.AddRange(pot);
						pot = new List<Card>();
						Console.Write(players[index] + " took the pot!\n");
						takepot = -1;
						numtilturn = 0;
						prevnumtilturn = 0;
						return;
					}
			}
			temp = players[index].discard(0);
			if(temp != null)
			{
				pot.Add(temp);
				Console.WriteLine(temp.flavor());
			}
			temp = players[index].discard(0);
			if(temp != null)
			{
				pot.Add(temp);
				Console.WriteLine(temp.flavor());
			}
			Console.Write(players[index] + " was too greedy, discarded two into the pot!\n");
		}
		public static int mod(int x, int m)
		{
			return (x%m + m)%m;
		}
	}
}
