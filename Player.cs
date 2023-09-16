public class Player
{
    public string ID;
    public int ProfitCents;
    public int MoneyCents;
    public List<Card> Cards;
    public bool CompletedPhase;
    public bool CompletedHand;

    /// <summary>
    /// A list of decision policies which get applied in order.
    /// If a decision policy returns a decision then it is used.
    /// If it returns null then the next decision is used.
    /// If the last decision is reached it must be used.
    /// </summary>
    public List<IDecisionPolicy> DecisionPolicies;

    public Player(string idValue, List<IDecisionPolicy> decisionPolicyValues)
    {
        ID = idValue;
        MoneyCents = 0;
        Cards = null;
        DecisionPolicies = decisionPolicyValues;
    }

    public PlayerDecision MakeDecision(
        List<Card> communityCards,
        List<PlayerDecision> handHistory,
        HandPhase phase)
    {
        foreach (var policy in DecisionPolicies)
        {
            var decision = policy.MakeDecision(this, communityCards, handHistory, phase);
            if (decision != null)
            {
                return decision;
            }
        }

        Console.WriteLine("Something went wrong. The last decision policy should always return a decision");
        return null;
    }
}