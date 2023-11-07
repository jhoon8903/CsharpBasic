namespace Algorithm_Csharp;

public class BalckJack
{
    public enum Suit { Hearts, Diamonds, Clubs, Spades }
public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }


// 카드 한 장을 표현하는 클래스
public class Card
{
        // 카드의 무늬를 나타냄
        public Suit Suit { get; private set; }
        // 카드의 숫자를 나타냄
        public Rank Rank { get; private set; }

        public Card(Suit s, Rank r)
        {
            Suit = s;
            Rank = r;
        }

        // 카드의 블랙잭에서의 점수를 반환하는 메소드
        public int GetValue()
        {
            if ((int)Rank <= 10)
            {
                return (int)Rank;
            }
            else if ((int)Rank <= 13)
            {
                return 10;
            }
            else
            {
                return 11;
            }
        }

        // 카드의 무늬와 숫자를 문자열로 반환하는 메소드
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }

    // 덱을 표현하는 클래스
    public class Deck
    {
        // 덱에 있는 카드들
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();

            // 모든 무늬와 숫자의 조합에 대해 카드를 생성
            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(s, r));
                }
            }

            // 카드를 섞는다
            Shuffle();
        }

        // 카드를 섞는 메소드
        public void Shuffle()
        {
            Random rand = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int j = rand.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        // 카드 한 장을 뽑는 메소드
        public Card DrawCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }

    // 플레이어의 패를 표현하는 클래스
    public class Hand
    {
        // 패에 있는 카드들
        private List<Card> cards;

        public Hand()
        {
            cards = new List<Card>();
        }

        // 카드를 패에 추가하는 메소드
        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        // 패의 총점을 계산하는 메소드
        public int GetTotalValue()
        {
            int total = 0;
            int aceCount = 0;

            foreach (Card card in cards)
            {
                if (card.Rank == Rank.Ace)
                {
                    aceCount++;
                }
                total += card.GetValue();
            }

            // 에이스가 있고 총점이 21점을 넘을 때, 에이스를 1점으로 취급
            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }
    }

    // 플레이어를 표현하는 클래스
    public class Player
    {
        // 플레이어의 패
        public Hand Hand { get; private set; }

        public Player()
        {
            Hand = new Hand();
        }

        // 카드를 뽑는 메소드
        public Card DrawCardFromDeck(Deck deck)
        {
            Card drawnCard = deck.DrawCard();
            Hand.AddCard(drawnCard);
            return drawnCard;
        }
    }

    // 딜러를 표현하는 클래스
    public class Dealer : Player
    {
        // 딜러는 총점이 17점 미만일 때 계속해서 카드를 뽑는다
        public void KeepDrawingCards(Deck deck)
        {
            while (Hand.GetTotalValue() < 17)
            {
                Card drawnCard = DrawCardFromDeck(deck);
                Console.WriteLine($"딜러는 '{drawnCard}'을(를) 뽑았습니다. 현재 총합은 {Hand.GetTotalValue()}점입니다.");
            }
        }
    }

    // 블랙잭 게임을 표현하는 클래스
    public class Blackjack
    {
        private Player player;
        private Dealer dealer;
        private Deck deck;

        public void PlayGame()
        {
            deck = new Deck();
            player = new Player();
            dealer = new Dealer();

            // 게임 시작, 플레이어와 딜러는 각각 두 장의 카드를 뽑는다
            for (int i = 0; i < 2; i++)
            {
                player.DrawCardFromDeck(deck);
                dealer.DrawCardFromDeck(deck);
            }

            Console.WriteLine("게임을 시작합니다!");
            Console.WriteLine($"플레이어의 초기 카드 합: {player.Hand.GetTotalValue()}");
            Console.WriteLine($"딜러의 초기 카드 합: {dealer.Hand.GetTotalValue()}");

            // 플레이어의 차례, 21점이 넘지 않는다면 계속해서 카드를 뽑을 수 있다
            while (player.Hand.GetTotalValue() < 21)
            {
                Console.Write("카드를 더 뽑으시겠습니까? (y/n): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "y")
                {
                    Card drawnCard = player.DrawCardFromDeck(deck);
                    Console.WriteLine($"'{drawnCard}'을(를) 뽑았습니다. 현재 총합은 {player.Hand.GetTotalValue()}점입니다.");
                }
                else
                {
                    break;
                }
            }

            // 딜러의 차례, 총합이 17점이 넘을 때까지 계속해서 카드를 뽑는다
            Console.WriteLine("딜러의 차례입니다.");
            dealer.KeepDrawingCards(deck);
            Console.WriteLine($"딜러의 총합은 {dealer.Hand.GetTotalValue()}점입니다.");

            // 승자 판정
            if (player.Hand.GetTotalValue() > 21)
            {
                Console.WriteLine("플레이어의 카드 합이 21점을 초과했습니다. 딜러의 승리입니다.");
            }
            else if (dealer.Hand.GetTotalValue() > 21)
            {
                Console.WriteLine("딜러의 카드 합이 21점을 초과했습니다. 플레이어의 승리입니다.");
            }
            else if (player.Hand.GetTotalValue() > dealer.Hand.GetTotalValue())
            {
                Console.WriteLine("플레이어의 카드 합이 더 높습니다. 플레이어의 승리입니다.");
            }
            else
            {
                Console.WriteLine("딜러의 카드 합이 더 높거나 같습니다. 딜러의 승리입니다.");
            }
        }
    }

    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //         Blackjack game = new Blackjack();
    //         game.PlayGame();
    //     }
    // }
}