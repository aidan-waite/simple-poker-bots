public class CardDeck
{
    private List<Card> cards;
    private int index;
    
    public CardDeck()
    {
        cards = new List<Card>();
        
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (CardRank cardRank in Enum.GetValues(typeof(CardRank)))
            {
                cards.Add(new Card(suit, cardRank));
            }
        }

        cards = Utilities.Shuffle(cards);
    }

    public Card DrawRandomCard()
    {
        // We're never going to use all the cards so don't care about checking
        Card c = cards[index];
        index++;
        return c;
    }

    public List<Card> DrawHoleCards()
    {
        List<Card> holeCards = new List<Card>();
        holeCards.Add(DrawRandomCard());
        holeCards.Add(DrawRandomCard());
        return holeCards;
    }
}