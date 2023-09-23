/**
 * Simulates one hand for some number of players.
 * Takes the players state as input and returns their updated state based on the result of the hand.
 */
public class HandRunner
{
    private Random random;
    private List<Player> players;
    private HandStrengthEvaluator strengthEvaluator = new HandStrengthEvaluator();
    
    public void SimulateHands()
    {
        random = new Random();
        var numberOfPlayers = 6;

        players = new List<Player>();
        
        for (var x = 0; x < numberOfPlayers; x++)
        {
            var decisionPolicies = new List<IDecisionPolicy>();
            decisionPolicies.Add(new BasicDecisionPolicy());
            var p = new Player("Player" + (x + 1), decisionPolicies);
            players.Add(p);
        }

        // Run the hand, log the result, reset stack size
        RunHand(1, 0);
        LogPlayerMoney();
    }

    private void RunHand(int handNumber, int buttonIndex)
    {
        var deck = new CardDeck();
        
        var log = "";
        log += "Run hand #" + handNumber + " with " + players.Count + " players\n";
        
        var smallBlindIndex = (buttonIndex + 1) % players.Count;
        var bigBlindIndex = (buttonIndex + 2) % players.Count;
        var utgIndex = (buttonIndex + 3) % players.Count;
        var utg1Index = (buttonIndex + 4) % players.Count;
        var cutoffIndex = (buttonIndex + 5) % players.Count;

        players[smallBlindIndex].Cards = deck.DrawHoleCards();
        players[bigBlindIndex].Cards = deck.DrawHoleCards();
        players[utgIndex].Cards = deck.DrawHoleCards();
        players[utg1Index].Cards = deck.DrawHoleCards();
        players[cutoffIndex].Cards = deck.DrawHoleCards();
        players[buttonIndex].Cards = deck.DrawHoleCards();

        log += "Button:" + players[buttonIndex].ID + "\n";
        log += "Small blind:" + players[smallBlindIndex].ID + "\n";
        log += "Big blind:" + players[bigBlindIndex].ID + "\n";
        log += "UTG:" + players[utgIndex].ID + "\n";
        log += "UTG1:" + players[utg1Index].ID + "\n";
        log += "Cutoff:" + players[cutoffIndex].ID + "\n";
        Console.WriteLine(log);

        // Amount of money in the pot in cents
        var pot = 0;
        
        // Preflop
        List<PlayerDecision> decisions = new List<PlayerDecision>();
        
        // Handle blinds
        decisions.Add(new PlayerDecision(
            players[smallBlindIndex].ID,
            PlayerDecisionType.Raise,
            1,
            HandPhase.Preflop)
        );
        players[smallBlindIndex].MoneyCents -= 1;
        pot += 1;
        
        decisions.Add(new PlayerDecision(
            players[smallBlindIndex].ID,
            PlayerDecisionType.Raise,
            2,
            HandPhase.Preflop)
        );
        players[bigBlindIndex].MoneyCents -= 2;
        pot += 2;

        var currentPlayerIndex = utgIndex;
        var currentPhase = HandPhase.Preflop;
        List<Card> communityCards = new List<Card>();
        
        // While there are players that need to play the current phase is active
        while (players.Any(x => !x.CompletedHand))
        {
            Console.WriteLine("Starting phase:" + currentPhase);

            foreach (var player in players)
            {
                player.CompletedPhase = false;
            }
            
            while (players.Any(x => !x.CompletedPhase))
            {
                Console.WriteLine("--------\n" + currentPhase + " " + players.Count(x => !x.CompletedPhase) + " players left to act");
                
                // Make decision for current player
                // Then continue to the next player
                var decision = players[currentPlayerIndex].MakeDecision(communityCards, decisions, currentPhase);
                players[currentPlayerIndex].MoneyCents -= decision.AmountCents;
                pot += decision.AmountCents;
                decisions.Add(decision);

                Console.WriteLine(decision.Description());

                players[currentPlayerIndex].CompletedPhase = true;

                switch (decision.DecisionType)
                {
                    case PlayerDecisionType.Fold:
                        Console.WriteLine("Handle fold");
                        players[currentPlayerIndex].CompletedHand = true;
                        break;
                    
                    // When a raise occurs, all players who haven't folded must act again 
                    case PlayerDecisionType.Raise:
                        Console.WriteLine("Handle raise");
                        var activeID = players[currentPlayerIndex].ID;
                        foreach (var p in players)
                        {
                            if (p.ID != activeID && Utilities.LastDecisionForPlayer(decisions, activeID).DecisionType !=
                                PlayerDecisionType.Fold)
                            {
                                p.CompletedPhase = false;
                                Console.Write(p.ID + " has not folded yet so they must act again");
                            }
                        }

                        break;
                }

                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            }
            
            Console.WriteLine("Completed phase:" + currentPhase);
            
            // If there's only one player left then they won
            if (players.Count(x => !x.CompletedHand) == 1)
            {
                var winningPlayer = players.First(x => !x.CompletedHand);
                winningPlayer.MoneyCents += pot;
                return;
            }

            switch (currentPhase)
            {
                case HandPhase.Preflop:
                    currentPhase = HandPhase.Flop;
                    deck.DrawRandomCard(); // burn one for the fans
                    communityCards.Add(deck.DrawRandomCard());
                    communityCards.Add(deck.DrawRandomCard());
                    communityCards.Add(deck.DrawRandomCard());
                    break;
                case HandPhase.Flop:
                    deck.DrawRandomCard(); // burn one for the fans
                    communityCards.Add(deck.DrawRandomCard());
                    currentPhase = HandPhase.Turn;
                    break;
                case HandPhase.Turn:
                    deck.DrawRandomCard(); // burn one for the fans
                    communityCards.Add(deck.DrawRandomCard());
                    currentPhase = HandPhase.River;
                    break;
                case HandPhase.River:
                    Console.WriteLine("Reached showdown");
                    // TODO: Figure out who won the hand and give them money
                    // List<HandRankAndCards> showdownHands = new List<HandRankAndCards>(); 
                    // strengthEvaluator.Evaluate()
                    return;
            }
        }
    }

    private void LogPlayerMoney()
    {
        foreach (var p in players)
        {
            string sign = p.MoneyCents >= 0 ? "+" : "-";
            double val = Double.Abs(p.MoneyCents / 100.0);
            Console.WriteLine(p.ID + " money:" + sign + Int32.Abs(p.MoneyCents) + " cents aka " + sign + "$" + val.ToString("0.##"));
        }
    }
    
    // public static void DebugLogHistory(List<PlayerDecision> history)
    // {
    //     // for (int x = 0; x < history.Count; x++)
    //     // {
    //     //     history[x].DebugLog();
    //     // }
    // }
}