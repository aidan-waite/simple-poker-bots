public class PlayerDecision
{
    public string PlayerID;
    public PlayerDecisionType DecisionType;
    
    // The amount of money that was called or raised
    public int AmountCents;
    
    public HandPhase Phase;

    public PlayerDecision(string playerIDValue, PlayerDecisionType decisionTypeValue, int amountCentsValue,
        HandPhase phaseValue)
    {
        PlayerID = playerIDValue;
        DecisionType = decisionTypeValue;
        AmountCents = amountCentsValue;
        Phase = phaseValue;
    }

    public string Description()
    {
        return "Player:" + PlayerID + " DecisionType:" + DecisionType + " AmountCentsValue:" + AmountCents + " Phase:" + Phase;
    }
}

public enum PlayerDecisionType
{
    Fold, Check, Call, Raise
}