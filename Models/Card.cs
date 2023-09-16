public enum Suit
{
    Spades,
    Clubs,
    Hearts,
    Diamonds
}

public enum CardRank
{
    Deuce,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

public class Card : IEquatable<Card>
{
    public CardRank cardRank;
    public Suit suit;

    public Card(Suit s, CardRank r)
    {
        suit = s;
        cardRank = r;
    }

    public Card(Suit s)
    {
        suit = s;

        var cardRanks = (CardRank[])Enum.GetValues(typeof(CardRank));
        var random = new Random();
        var ind = random.Next(0, cardRanks.Length);
        cardRank = cardRanks[ind];
    }

    public Card(CardRank r)
    {
        cardRank = r;

        var suits = (Suit[])Enum.GetValues(typeof(Suit));
        var random = new Random();
        var ind = random.Next(0, suits.Length);
        suit = suits[ind];
    }

    public bool Equals(Card otherCard)
    {
        if (otherCard == null) return false;

        return suit == otherCard.suit && cardRank == otherCard.cardRank;
    }

    public string ID()
    {
        return RankSymbol() + SuitSymbol();
    }

    string RankSymbol()
    {
        switch (cardRank)
        {
            case CardRank.Ace:
                return "A";
            case CardRank.Deuce:
                return "2";
            case CardRank.Three:
                return "3";
            case CardRank.Four:
                return "4";
            case CardRank.Five:
                return "5";
            case CardRank.Six:
                return "6";
            case CardRank.Seven:
                return "7";
            case CardRank.Eight:
                return "8";
            case CardRank.Nine:
                return "9";
            case CardRank.Ten:
                return "10";
            case CardRank.Jack:
                return "J";
            case CardRank.Queen:
                return "Q";
            case CardRank.King:
                return "K";
            default:
                return "?";
        }
    }

    string SuitSymbol()
    {
        switch (suit)
        {
            case Suit.Clubs:
                return "♣";
            case Suit.Diamonds:
                return "◆";
            case Suit.Hearts:
                return "♥";
            case Suit.Spades:
                return "♠";
            default:
                return "?";
        }
    }
}

public class HandRankAndCards
{
    public List<Card> Cards; // Note: only includes cards relevant to the hand rank
    public HandRank Rank;

    public HandRankAndCards(HandRank r, List<Card> c)
    {
        Rank = r;
        Cards = c;
    }
}

public enum HandRank
{
    RoyalFlush,
    StraightFlush,
    FourOfAKind,
    FullHouse,
    Flush,
    Straight,
    ThreeOfAKind,
    TwoPair,
    OnePair,
    HighCard
}