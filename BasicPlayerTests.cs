/**
 * Tests simple scenarios using BasicPlayer and the following positions:
 *      Small blind: player2
 *      Big blind:   player3
 *      UTG:         player4
 *      UTG+1:       player5
 *      Cutoff:      player6
 *      Button:      player1 (hero)
 */
public class BasicPlayerTests
{
    private Player player;
    private List<PlayerDecision> history;
    
    public void RunAllTests()
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine("----- Preflop Tests -----");
        Console.WriteLine("-------------------------");
        
        Console.WriteLine("\n--- Begin BasicPlayerTests ---");
        TestPreflopFold();
        Console.WriteLine("--- End BasicPlayerTests ---");

        Console.WriteLine("\n--- Begin TestPreflopCheck ---");
        TestPreflopCheck();
        Console.WriteLine("--- End TestPreflopCheck ---");

        Console.WriteLine("\n--- Begin TestPreflopRaise ---");
        TestPreflopRaise();
        Console.WriteLine("--- End TestPreflopRaise ---");
        
        Console.WriteLine("\n-------------------------");
        Console.WriteLine("----- Flop Tests -----");
        Console.WriteLine("-------------------------");

        Console.WriteLine("\n--- Begin TestFlopCheck ---");
        TestFlopCheck();
        Console.WriteLine("--- End TestFlopCheck ---");

        Console.WriteLine("\n--- Begin TestFlopCall ---");
        TestFlopCall();
        Console.WriteLine("--- End TestFlopCall ---");

        Console.WriteLine("\n--- Begin TestFlopFold ---");
        TestFlopFold();
        Console.WriteLine("--- End TestFlopFold ---");

        Console.WriteLine("\n--- Begin TestFlopRaise ---");
        TestFlopRaise();
        Console.WriteLine("--- End TestFlopRaise ---");
    }

    private void Cleanup()
    {
        player = null;
        history = null;
    }
    
    private void PreflopSetup()
    {
        var decisionPolicies = new List<IDecisionPolicy>();
        decisionPolicies.Add(new BasicDecisionPolicy());
        player = new Player("player1", decisionPolicies);
 
        // Add small blind and big blind
        history = new List<PlayerDecision>();
        history.Add(new PlayerDecision("player2", PlayerDecisionType.Raise, 1, HandPhase.Preflop));
        history.Add(new PlayerDecision("player3", PlayerDecisionType.Raise, 2, HandPhase.Preflop));
    }
    
    private void TestPreflopFold()
    {
        Cleanup();
        PreflopSetup();

        List<Card> holeCards = new List<Card>();
        holeCards.Add(new Card(Suit.Clubs, CardRank.Four));
        holeCards.Add(new Card(Suit.Spades, CardRank.Nine));
        player.Cards = new List<Card>(holeCards);
        
        var decision = player.MakeDecision(null, history, HandPhase.Preflop);
        var testDecision = decision.DecisionType == PlayerDecisionType.Fold;
        Console.WriteLine("Preflop decision " + Utilities.CardsDescription(player.Cards) + " expected:Fold actual:" + decision.DecisionType + " test result:" + testDecision);
    }

    private void TestPreflopCheck()
    {
        Cleanup();
        var decisionPolicies = new List<IDecisionPolicy>();
        decisionPolicies.Add(new BasicDecisionPolicy());
        player = new Player("player1", decisionPolicies);

        List<Card> holeCards = new List<Card>();
        holeCards.Add(new Card(Suit.Diamonds, CardRank.Deuce));
        holeCards.Add(new Card(Suit.Hearts, CardRank.Seven));
        player.Cards = new List<Card>(holeCards);
        
        var decision = player.MakeDecision(null, history, HandPhase.Preflop);
        var testDecision = decision.DecisionType == PlayerDecisionType.Check;
        Console.WriteLine("Preflop decision " + Utilities.CardsDescription(player.Cards) + " expected:Check actual:" + decision.DecisionType + " test result:" + testDecision);
    }
    
    private void TestPreflopRaise()
    {
        Cleanup();
        PreflopSetup();
        
        List<Card> holeCards = new List<Card>();
        holeCards.Add(new Card(Suit.Diamonds, CardRank.Jack));
        holeCards.Add(new Card(Suit.Hearts, CardRank.Jack));
        player.Cards = new List<Card>(holeCards);
        
        var decision = player.MakeDecision(null, history, HandPhase.Preflop);
        var testDecision = decision.DecisionType == PlayerDecisionType.Raise;
        Console.WriteLine("Preflop decision " + Utilities.CardsDescription(player.Cards) + " expected:Raise actual:" + decision.DecisionType + " test result:" + testDecision);
    }

    // Setup a typical flop where everyone but two players have folded
    private void FlopSetup()
    {
        var decisionPolicies = new List<IDecisionPolicy>();
        decisionPolicies.Add(new BasicDecisionPolicy());
        player = new Player("player1", decisionPolicies);
        
        // Everyone folds preflop except for player1 and player6
        history = new List<PlayerDecision>();
        history.Add(new PlayerDecision("player2", PlayerDecisionType.Raise, 1, HandPhase.Preflop));
        history.Add(new PlayerDecision("player3", PlayerDecisionType.Raise, 2, HandPhase.Preflop));
        history.Add(new PlayerDecision("player4", PlayerDecisionType.Fold, 0, HandPhase.Preflop));
        history.Add(new PlayerDecision("player5", PlayerDecisionType.Fold, 0, HandPhase.Preflop));
        history.Add(new PlayerDecision("player6", PlayerDecisionType.Raise, 4, HandPhase.Preflop));
        history.Add(new PlayerDecision("player1", PlayerDecisionType.Call, 6, HandPhase.Preflop));
        history.Add(new PlayerDecision("player2", PlayerDecisionType.Fold, 1, HandPhase.Preflop));
        history.Add(new PlayerDecision("player3", PlayerDecisionType.Call, 4, HandPhase.Preflop));
    }
    
    private void TestFlopCheck()
    {
        Cleanup();
        FlopSetup();
        
        // hero has a bad hand and is heads up with one villain on flop
        // villain checks
        // expect hero to check
        history.Add(new PlayerDecision("player6", PlayerDecisionType.Check, 0, HandPhase.Flop));
        
        List<Card> holeCards = new List<Card>();
        holeCards.Add(new Card(Suit.Clubs, CardRank.Five));
        holeCards.Add(new Card(Suit.Diamonds, CardRank.Queen));
        player.Cards = new List<Card>(holeCards);

        var communityCards = new List<Card>();
        communityCards.Add(new Card(Suit.Spades, CardRank.Ace));
        communityCards.Add(new Card(Suit.Hearts, CardRank.Three));
        communityCards.Add(new Card(Suit.Diamonds, CardRank.Eight));
        
        var decision = player.MakeDecision(null, history, HandPhase.Flop);
        var testDecision = decision.DecisionType == PlayerDecisionType.Check;
        Console.WriteLine("Flop decision " + Utilities.CardsDescription(player.Cards) + " expected:Check actual:" + decision.DecisionType + " test result:" + testDecision);
    }
    
    private void TestFlopCall()
    {
        Cleanup();
        FlopSetup();
        
        // hero has middle pair with one villain on flop
        // villain makes a small raise
        // expect hero to call
        history.Add(new PlayerDecision("player6", PlayerDecisionType.Raise, 8, HandPhase.Flop));
        
        List<Card> holeCards = new List<Card>();
        holeCards.Add(new Card(Suit.Clubs, CardRank.Eight));
        holeCards.Add(new Card(Suit.Diamonds, CardRank.Queen));
        player.Cards = new List<Card>(holeCards);

        var communityCards = new List<Card>();
        communityCards.Add(new Card(Suit.Spades, CardRank.Ace));
        communityCards.Add(new Card(Suit.Hearts, CardRank.Deuce));
        communityCards.Add(new Card(Suit.Diamonds, CardRank.Eight));
        
        var decision = player.MakeDecision(communityCards, history, HandPhase.Flop);
        var testDecision = decision.DecisionType == PlayerDecisionType.Call;
        Console.WriteLine("Flop decision " + Utilities.CardsDescription(player.Cards) + " expected:Call actual:" + decision.DecisionType + " test result:" + testDecision);
    }
    
    private void TestFlopFold()
    {
        Cleanup();
        FlopSetup();
        
        // Miss flop completely and fold to any bet 
        history.Add(new PlayerDecision("player6", PlayerDecisionType.Raise, 2, HandPhase.Flop));
        
        List<Card> holeCards = new List<Card>();
        holeCards.Add(new Card(Suit.Clubs, CardRank.Eight));
        holeCards.Add(new Card(Suit.Diamonds, CardRank.Queen));
        player.Cards = new List<Card>(holeCards);

        var communityCards = new List<Card>();
        communityCards.Add(new Card(Suit.Spades, CardRank.Ace));
        communityCards.Add(new Card(Suit.Hearts, CardRank.Deuce));
        communityCards.Add(new Card(Suit.Diamonds, CardRank.Nine));
        
        var decision = player.MakeDecision(communityCards, history, HandPhase.Flop);
        var testDecision = decision.DecisionType == PlayerDecisionType.Fold;
        Console.WriteLine("Flop decision " + Utilities.CardsDescription(player.Cards) + " expected:Fold actual:" + decision.DecisionType + " test result:" + testDecision);
    }
    
    private void TestFlopRaise()
    {
        Cleanup();
        FlopSetup();
        
        // Strong hand on flop so expect hero to raise
        history.Add(new PlayerDecision("player6", PlayerDecisionType.Check, 0, HandPhase.Flop));
        
        List<Card> holeCards = new List<Card>();
        holeCards.Add(new Card(Suit.Diamonds, CardRank.Ace));
        holeCards.Add(new Card(Suit.Diamonds, CardRank.King));
        player.Cards = new List<Card>(holeCards);

        var communityCards = new List<Card>();
        communityCards.Add(new Card(Suit.Diamonds, CardRank.Deuce));
        communityCards.Add(new Card(Suit.Diamonds, CardRank.Jack));
        communityCards.Add(new Card(Suit.Diamonds, CardRank.Nine));
        
        var decision = player.MakeDecision(communityCards, history, HandPhase.Flop);
        var testDecision = decision.DecisionType == PlayerDecisionType.Raise;
        Console.WriteLine("Flop decision " + Utilities.CardsDescription(player.Cards) + " expected:Raise actual:" + decision.DecisionType + " test result:" + testDecision);
    }
}