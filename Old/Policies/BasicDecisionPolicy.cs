public class BasicDecisionPolicy: IDecisionPolicy
{
    private HandStrengthEvaluator strengthEvaluator = new HandStrengthEvaluator();
    private Random random = new Random();

    private HandStrengthEvaluator evaluator()
    {
        return strengthEvaluator;
    }
    
    public PlayerDecision MakeDecision(
        Player player,
        List<Card> communityCards,
        List<PlayerDecision> handHistory,
        HandPhase phase)
    {
        int largestRaise = Utilities.LargestRaiseInPhase(phase, handHistory);
        Console.WriteLine("BasicDecisionPolicy largest raise:" + largestRaise);

        int bet = 0;
        int multiplier = 0;
        int randy;

        // Now evaluate the strength of our hand to decide what to do
        var strength = evaluator().Evaluate(player.Cards, communityCards);
        switch (strength)
        {
            case HandStrength.Okay:
                Console.WriteLine("BasicDecisionPolicy hand strength is okay");
                if (largestRaise <= 0.2f)
                {
                    return new PlayerDecision(player.ID, PlayerDecisionType.Call, 0, phase);
                }

                randy = random.Next(100);
                
                // Raise 20% of the time
                if (randy < 20)
                {
                    multiplier = 2 + random.Next(3);
                    bet = Int32.Min(largestRaise * multiplier, player.MoneyCents);
                    return new PlayerDecision(player.ID, PlayerDecisionType.Raise, bet, phase);
                }
                
                // Call 20% of the time
                if (randy < 40)
                {
                    return new PlayerDecision(player.ID, PlayerDecisionType.Call, bet, phase);
                }
                
                // Fold 60% of the time
                return new PlayerDecision(player.ID, PlayerDecisionType.Fold, 0, phase);

            case HandStrength.Strong:
                Console.WriteLine("BasicDecisionPolicy hand strength is strong");

                randy = random.Next(100);

                // Call if we're all in or 15% of the time
                if (largestRaise >= player.MoneyCents || randy < 15)
                {
                    return new PlayerDecision(player.ID, PlayerDecisionType.Call, 0, phase);
                }

                multiplier = 2 + random.Next(3);
                bet = Int32.Min(largestRaise * multiplier, player.MoneyCents);
                return new PlayerDecision(player.ID, PlayerDecisionType.Raise, bet, phase);
            
            case HandStrength.Weak:
                Console.WriteLine("BasicDecisionPolicy hand strength is weak");
                
                // This player can only be the last raiser if it's preflop
                // In that case, check rather than fold
                var lastRaise = handHistory.FindLast(x => x.DecisionType == PlayerDecisionType.Raise);
                if (lastRaise != null && lastRaise.PlayerID == player.ID)
                {
                    return new PlayerDecision(player.ID, PlayerDecisionType.Check, 0, phase);
                }
                
                // Check if it's free to do so
                if (largestRaise == 0)
                {
                    return new PlayerDecision(player.ID, PlayerDecisionType.Check, 0, phase);
                }
                
                // 20% of the time fire off a bluff
                randy = random.Next(100);
                if (randy < 20)
                {
                    multiplier = 2 + random.Next(3);
                    bet = Int32.Min(largestRaise * multiplier, player.MoneyCents);
                    return new PlayerDecision(player.ID, PlayerDecisionType.Raise, bet, phase);
                }

                return new PlayerDecision(player.ID, PlayerDecisionType.Fold, 0, phase);
        }

        Console.WriteLine("Something is broken, shouldn't get here");
        return new PlayerDecision(player.ID, PlayerDecisionType.Fold, 0, phase);
    }
}