using System.Diagnostics;

public class WhoWonTests
{
    private HandStrengthEvaluator strengthEvaluator = new HandStrengthEvaluator();
    private Stopwatch watch = new System.Diagnostics.Stopwatch();
    
    public void RunAllTests()
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine("----- Who Won Tests -----");
        Console.WriteLine("-------------------------");
        
        TestHighCard1();
        TestTwoPair1();
        TestTwoPair2();
    }
    
    void TestHighCard1()
    {
        watch.Start();
        List<Card> communityCards = new List<Card>();
        communityCards.Add(new Card(Suit.Hearts, CardRank.Deuce));
        communityCards.Add(new Card(Suit.Clubs, CardRank.King));
        communityCards.Add(new Card(Suit.Clubs, CardRank.Three));
        communityCards.Add(new Card(Suit.Hearts, CardRank.Six));
        communityCards.Add(new Card(Suit.Spades, CardRank.Six));

        List<Card> holeCards = new List<Card>();
        
        // player 1 cards
        holeCards.Add(new Card(Suit.Hearts, CardRank.Eight));
        holeCards.Add(new Card(Suit.Hearts, CardRank.Queen));
        
        // player 2 cards
        holeCards.Add(new Card(Suit.Diamonds, CardRank.Ten));
        holeCards.Add(new Card(Suit.Clubs, CardRank.Seven));

        var playerHands = SetupWithCards(holeCards, communityCards);
        
        // Expect player 1 to win with a queen high
        var winningPlayers = strengthEvaluator.WinningPlayerFor(playerHands);
        var testResult = winningPlayers.Count == 1 && winningPlayers[0].ID == "player1";
        watch.Stop();
        Console.WriteLine((testResult ? "✅" : "❌") + "  Test high card 1 expected winner:player1 determined winner:" + winningPlayers[0].ID + " " + watch.ElapsedMilliseconds + " ms");
    }
    
    // Test the case where only one player has two pair
    void TestTwoPair1()
    {
        watch.Start();
        List<Card> communityCards = new List<Card>();
        communityCards.Add(new Card(Suit.Hearts, CardRank.Four));
        communityCards.Add(new Card(Suit.Spades, CardRank.Deuce));
        communityCards.Add(new Card(Suit.Spades, CardRank.Queen));
        communityCards.Add(new Card(Suit.Diamonds, CardRank.Nine));
        communityCards.Add(new Card(Suit.Diamonds, CardRank.Six));

        List<Card> holeCards = new List<Card>();
        
        // player 1 cards
        holeCards.Add(new Card(Suit.Clubs, CardRank.Four));
        holeCards.Add(new Card(Suit.Diamonds, CardRank.Five));
        
        // player 2 cards
        holeCards.Add(new Card(Suit.Spades, CardRank.Four));
        holeCards.Add(new Card(Suit.Spades, CardRank.Nine));
        
        // player 3 cards
        holeCards.Add(new Card(Suit.Hearts, CardRank.Nine));
        holeCards.Add(new Card(Suit.Diamonds, CardRank.Ace));
        
        // player 4 cards
        holeCards.Add(new Card(Suit.Hearts, CardRank.Seven));
        holeCards.Add(new Card(Suit.Clubs, CardRank.Eight));
        
        // player 5 cards
        holeCards.Add(new Card(Suit.Clubs, CardRank.Five));
        holeCards.Add(new Card(Suit.Clubs, CardRank.Queen));
        
        // player 6 cards
        holeCards.Add(new Card(Suit.Hearts, CardRank.Jack));
        holeCards.Add(new Card(Suit.Clubs, CardRank.Ace));

        var playerHands = SetupWithCards(holeCards, communityCards);
        
        // Expect player 2 to win with two pair
        var winningPlayers = strengthEvaluator.WinningPlayerFor(playerHands);
        var testResult = winningPlayers.Count == 1 && winningPlayers[0].ID == "player2";
        watch.Stop();
        Console.WriteLine((testResult ? "✅" : "❌") + "  Test two pair 1 expected winner:player2 determined winner:" + winningPlayers[0].ID + " " + watch.ElapsedMilliseconds + " ms");
    }

    // Test the case where two players have two pair
    void TestTwoPair2()
    {
        watch.Start();
        List<Card> communityCards = new List<Card>();
        communityCards.Add(new Card(Suit.Hearts, CardRank.Four));
        communityCards.Add(new Card(Suit.Spades, CardRank.Deuce));
        communityCards.Add(new Card(Suit.Spades, CardRank.Queen));
        communityCards.Add(new Card(Suit.Diamonds, CardRank.Nine));
        communityCards.Add(new Card(Suit.Diamonds, CardRank.Six));

        List<Card> holeCards = new List<Card>();
        
        // player 1 cards
        holeCards.Add(new Card(Suit.Clubs, CardRank.Four));
        holeCards.Add(new Card(Suit.Diamonds, CardRank.Five));
        
        // player 2 cards
        holeCards.Add(new Card(Suit.Spades, CardRank.Four));
        holeCards.Add(new Card(Suit.Spades, CardRank.Nine));
        
        // player 3 cards
        holeCards.Add(new Card(Suit.Hearts, CardRank.Nine));
        holeCards.Add(new Card(Suit.Diamonds, CardRank.Ace));
        
        // player 4 cards
        holeCards.Add(new Card(Suit.Hearts, CardRank.Seven));
        holeCards.Add(new Card(Suit.Clubs, CardRank.Eight));
        
        // player 5 cards
        holeCards.Add(new Card(Suit.Clubs, CardRank.Queen));
        holeCards.Add(new Card(Suit.Clubs, CardRank.Six));
        
        // player 6 cards
        holeCards.Add(new Card(Suit.Hearts, CardRank.Jack));
        holeCards.Add(new Card(Suit.Clubs, CardRank.Ace));

        var playerHands = SetupWithCards(holeCards, communityCards);
        
        // Expect player 5 to win with a higher two pair
        var winningPlayers = strengthEvaluator.WinningPlayerFor(playerHands);
        var testResult = winningPlayers.Count == 1 && winningPlayers[0].ID == "player5";
        watch.Stop();
        Console.WriteLine((testResult ? "✅" : "❌") + "  Test two pair 2 expected winner:player5 determined winner:" + winningPlayers[0].ID + " " + watch.ElapsedMilliseconds + " ms");
    }
    
    List<PlayerHandRankAndCards> SetupWithCards(List<Card> holeCards, List<Card> communityCards)
    {
        List<PlayerHandRankAndCards> result = new List<PlayerHandRankAndCards>();
        for (int x = 0; x < holeCards.Count; x += 2)
        {
            Player p = new Player("player" + (x / 2 + 1), new List<IDecisionPolicy>());
            p.Cards = holeCards.Skip(x).Take(2).ToList();
            List<Card> hand = new List<Card>();
            hand.AddRange(holeCards.Skip(x).Take(2));
            hand.AddRange(communityCards);
            Debug.Assert(hand.Count == 7, "Expected 5 community cards and 2 hole cards");
            HandRankAndCards handRankAndCards = strengthEvaluator.Rank(hand);
            result.Add(new PlayerHandRankAndCards(handRankAndCards, p));    
        }

        return result;
    }
}