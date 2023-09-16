public interface IDecisionPolicy
{
    public PlayerDecision MakeDecision(
        Player player,
        List<Card> communityCards,
        List<PlayerDecision> handHistory,
        HandPhase phase);
}