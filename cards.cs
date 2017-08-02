using System;
using System.Collections.Generic;
namespace cards
{
	public class script
	{
		/*public static void Main()
		{
			Deck d = new Deck();
			for(int i = 0; i < 52; i++)
				Console.WriteLine(d.draw().flavor());
			d.setCards();
			d.shuffle();
			//for(int i = 0; i < 52; i++)
				//Console.WriteLine(d.draw().flavor());	
			Player p = new Player("john");
			p.draw(d);
			p.draw(d);
			p.draw(d);
			p.draw(d);
			p.draw(d);
			for(int i = 0; i < 5; i++)
				Console.WriteLine(p.discard(0).flavor());
			if(p.discard(0)==null)
				Console.WriteLine("Null!");
		}*/
	}
	public class Card
	{
		public string stringval;
		public string suit;
		public int val;
		public Card(string Suit, int Val)
		{
			suit = Suit;
			val = Val;
			switch(Val){
				case  1: stringval = "Ace";break;
				case  2: stringval = "2";break;
				case  3: stringval = "3";break;
				case  4: stringval = "4";break;
				case  5: stringval = "5";break;
				case  6: stringval = "6";break;
				case  7: stringval = "7";break;
				case  8: stringval = "8";break;
				case  9: stringval = "9";break;
				case 10: stringval = "10";break;
				case 11: stringval = "Jack";break;
				case 12: stringval = "Queen";break;
				case 13: stringval = "King";break;
			}
		}
		public string flavor()
		{
			return stringval + " of " + suit;
		}
	}
	public class Deck
	{
		public List<Card> cards;
		public Deck()
		{
			setCards();
		}
		public void setCards()
		{	
			cards = new List<Card>();
			string[] suits = new string[]{
				"Clubs", "Spades", "Hearts", "Diamonds"};
			for(int i = 0; i < 4; i++)
				for(int j = 1; j < 14; j++)
					cards.Add(new Card(suits[i], j));
		}
		public Card draw()
		{
			Card card = cards[cards.Count-1];
			cards.RemoveAt(cards.Count-1);
			return card;
		}
		public void shuffle()
		{
			Random rand = new Random(Environment.TickCount);
			Console.WriteLine(Environment.TickCount);
			for(int i = 0; i < 52; i++)
			{
				Card temp = cards[i];
				int ridx = rand.Next(0, 52);
				cards[i] = cards[ridx];
				cards[ridx] = temp;
			}
		}
	}
	public class Player
	{
		public string name;
		public List<Card> hand;
		public Player(string Name)
		{
			name = Name;
			hand = new List<Card>();
		}
		public void draw(Deck d)
		{
			hand.Add(d.draw());
		}
		public Card discard(int index)
		{
			if(hand.Count - 1 < index)
				return null;
			Card card = hand[index];
			hand.RemoveAt(index);
			return card;	
		}
	}
}
