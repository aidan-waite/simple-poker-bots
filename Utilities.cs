public static class Utilities
{
    // From https://stackoverflow.com/a/58599209
    public static List<T> Shuffle<T>(List<T> _list)
    {
        var random = new Random();
        for (var i = 0; i < _list.Count; i++)
        {
            var temp = _list[i];
            var randomIndex = random.Next(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }

        return _list;
    }

    public static string CardsDescription(List<Card> cards)
    {
        if (cards == null || cards.Count == 0)
        {
            return "None";
        }
        
        // sort cards lowest first
        cards = cards.OrderBy(x => x.cardRank).ToList();
        string output = "";

        for (int x = 0; x < cards.Count; x++)
        {
            if (x > 0)
            {
                output += " ";
            }

            output += cards[x].ID();
        }
        return output;
    }
    
    public static int LargestRaiseInPhase(HandPhase phase, List<PlayerDecision> handHistory)
    {
        int largestRaise = 0;
        
        if (handHistory == null || handHistory.Count == 0)
        {
            return largestRaise;
        }
        
        // Get the hand history from this phase to see what our choices are
        List<PlayerDecision> currentPhaseHistory = handHistory.Where(x => x.Phase == phase).ToList();
        if (currentPhaseHistory.Count == 0)
        {
            return largestRaise;
        }

        if (currentPhaseHistory.Exists(x => x.DecisionType == PlayerDecisionType.Raise))
        {
            var raises = currentPhaseHistory.FindAll(x => x.DecisionType == PlayerDecisionType.Raise);
            // TODO: replace this with a linq one liner
            foreach (var raise in raises)
            {
                if (raise.AmountCents > largestRaise)
                {
                    largestRaise = raise.AmountCents;
                }
            }
        }

        return largestRaise;
    }
    
    public static PlayerDecision LastDecisionForPlayer(List<PlayerDecision> history, string playerID)
    {
        PlayerDecision lastDecision = null;
        foreach (var decision in history)
        {
            if (decision.PlayerID == playerID)
            {
                lastDecision = decision;
            }
        }
    
        return lastDecision;
    }
}