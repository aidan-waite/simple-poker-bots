using System.Data;
using System.Diagnostics;

public class HandStrengthEvaluator
{
    private HandStrength EvaluatePreflop(List<Card> holeCards)
    {
        if (holeCards[0].cardRank == holeCards[1].cardRank)
        {
            return HandStrength.Strong;
        }

        if (holeCards[0].suit == holeCards[1].suit)
        {
            return HandStrength.Okay;
        }

        return HandStrength.Weak;
    }
    
    public List<Player> WinningPlayerFor(List<PlayerHandRankAndCards> playerHandRankAndCards)
    {
        // Find the best ranking hands
        HandRank bestRank = HandRank.HighCard;
        foreach (PlayerHandRankAndCards phc in playerHandRankAndCards)
        {
            // The best hand is the initial enum value for rank
            // The lower the int value the better the hand
            if (phc.HandRankAndCards.Rank < bestRank)
            {
                bestRank = phc.HandRankAndCards.Rank;
            }
        }

        // Remove all players who don't have the highest rank
        playerHandRankAndCards.RemoveAll(phc => phc.HandRankAndCards.Rank != bestRank);
        
        // If there's one player only we have a winner
        if (playerHandRankAndCards.Count == 1)
        {
            return new() { playerHandRankAndCards[0].Player };
        }
        
        // More than one player has a highest rank hand
        // Figure out who won based on the type of hand
        switch (bestRank)
        {
            case HandRank.FullHouse:
                CardRank highestThreeCards = CardRank.Deuce;
                foreach (PlayerHandRankAndCards phc in playerHandRankAndCards)
                {
                    Debug.Assert(phc.HandRankAndCards.Cards.Count == 5, "Full houses should always have 5 cards");
                    phc.HandRankAndCards.Cards.Sort((a, b) => b.cardRank - a.cardRank);
                    // The middle card will always be the 3
                    if (phc.HandRankAndCards.Cards[2].cardRank > highestThreeCards)
                    {
                        highestThreeCards = phc.HandRankAndCards.Cards[2].cardRank;
                    }
                }
                return playerHandRankAndCards
                    .Where(o => o.HandRankAndCards.Cards[2].cardRank == highestThreeCards)
                    .Select(o => o.Player)
                    .ToList();
            
            default:
                CardRank highestCardRank = CardRank.Deuce;
                foreach (PlayerHandRankAndCards phc in playerHandRankAndCards)
                {
                    // Sort in place in descending order so the 0th card is the highest
                    phc.HandRankAndCards.Cards.Sort((a, b) => b.cardRank - a.cardRank);
                    if (phc.HandRankAndCards.Cards[0].cardRank > highestCardRank)
                    {
                        highestCardRank = phc.HandRankAndCards.Cards[0].cardRank;
                    }
                }
                
                // Return all players with the highest card
                return playerHandRankAndCards
                    .Where(o => o.HandRankAndCards.Cards[0].cardRank == highestCardRank)
                    .Select(o => o.Player)
                    .ToList();
        }
    }

    public HandStrength Evaluate(List<Card> holeCards, List<Card> communityCards)
    {
        Console.WriteLine("HandStrengthEvaluator - Evaluate hole:" + Utilities.CardsDescription(holeCards) + " community:" + Utilities.CardsDescription(communityCards));
        if (communityCards.Count == 0)
        {
            return EvaluatePreflop(holeCards);
        }

        var allCards = new List<Card>();
        allCards.AddRange(holeCards);
        if (communityCards.Count > 0)
        {
            allCards.AddRange(communityCards);    
        }
        
        var handRankAndCards = Rank(allCards);
        switch (handRankAndCards.Rank)
        {
            case HandRank.Flush:
            case HandRank.Straight:
            case HandRank.StraightFlush:
            case HandRank.FullHouse:
            case HandRank.RoyalFlush:
            case HandRank.FourOfAKind:
            case HandRank.ThreeOfAKind:
                return HandStrength.Strong;
            case HandRank.OnePair:
            case HandRank.TwoPair:
                return HandStrength.Okay;
            case HandRank.HighCard:
                return HandStrength.Weak;
            default:
                Console.WriteLine("Unexpected rank");
                return HandStrength.Weak;
        }
    }
    
    // Assumes that hole cards are passed in first
    public HandRankAndCards Rank(List<Card> unsortedHand)
    {
        // First sort the cards from lowest to highest
        var holeCards = unsortedHand.Take(2).ToList();
        var hand = unsortedHand.OrderBy(x => (int)x.cardRank).ToList();
        var isFlush = IsFlush(hand);
        var isStraight = IsStraight(hand);

        if (isFlush != null && isStraight != null)
        {
            // There's an edge case where isStraight used another suit for one of the cards
            // So call IsStraight again with just the flush cards
            var overlappingCards = isFlush.Cards.Intersect(IsStraight(isFlush.Cards).Cards)
                .OrderByDescending(x => (int)x.cardRank).ToList();
            if (overlappingCards.Count >= 5)
            {
                if (overlappingCards.Exists(x => x.cardRank == CardRank.Ace) &&
                    overlappingCards.Exists(x => x.cardRank == CardRank.King) &&
                    overlappingCards.Exists(x => x.cardRank == CardRank.Queen) &&
                    overlappingCards.Exists(x => x.cardRank == CardRank.Jack) &&
                    overlappingCards.Exists(x => x.cardRank == CardRank.Ten)
                   )
                {
                    return new HandRankAndCards(HandRank.RoyalFlush, overlappingCards.GetRange(0, 5));
                }

                return new HandRankAndCards(HandRank.StraightFlush, overlappingCards.GetRange(0, 5));
            }
        }

        if (isFlush != null)
        {
            return isFlush;
        }

        if (isStraight != null)
        {
            return isStraight;
        }

        var cardRankFrequency = new Dictionary<CardRank, int>();
        foreach (var card in hand)
        {
            if (cardRankFrequency.ContainsKey(card.cardRank))
            {
                cardRankFrequency[card.cardRank] += 1;
            }
            else
            {
                cardRankFrequency[card.cardRank] = 1;
            }
        }

        var orderedFrequency =
            cardRankFrequency.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        if (orderedFrequency.First().Value == 4)
        {
            return new HandRankAndCards(HandRank.FourOfAKind,
                hand.FindAll(x => x.cardRank == orderedFrequency.First().Key));
        }

        if (orderedFrequency.First().Value == 3 && orderedFrequency.ContainsValue(2))
        {
            var relevantCards = new List<Card>();
            relevantCards.AddRange(hand.FindAll(x => x.cardRank == orderedFrequency.First().Key));

            var twoCardsCardRank = orderedFrequency.First(x => x.Value == 2).Key;
            relevantCards.AddRange(hand.FindAll(x => x.cardRank == twoCardsCardRank));

            return new HandRankAndCards(HandRank.FullHouse, relevantCards);
        }

        if (orderedFrequency.First().Value == 3)
        {
            return new HandRankAndCards(HandRank.ThreeOfAKind,
                hand.FindAll(x => x.cardRank == orderedFrequency.First().Key));
        }

        if (orderedFrequency.ElementAt(0).Value == 2 && orderedFrequency.ElementAt(1).Value == 2)
        {
            // We can have 2 or more pairs here, and we need to make sure we get the highest 2 pairs
            var relevantCards = new List<Card>();
            foreach (var pair in orderedFrequency)
            {
                if (pair.Value == 2)
                {
                    relevantCards.AddRange(hand.FindAll(x => x.cardRank == pair.Key));
                }
            }

            relevantCards = relevantCards.OrderByDescending(x => (int)x.cardRank).ToList();
            return new HandRankAndCards(HandRank.TwoPair, relevantCards.GetRange(0, 4));
        }

        if (orderedFrequency.ElementAt(0).Value == 2)
        {
            return new HandRankAndCards(HandRank.OnePair,
                hand.FindAll(x => x.cardRank == orderedFrequency.ElementAt(0).Key));
        }
        
        return new HandRankAndCards(HandRank.HighCard, holeCards);
    }

    public static HandRankAndCards IsStraight(List<Card> unsortedHand)
    {
        var printLogs = false;
        var id = Guid.NewGuid();

        var hand = unsortedHand.OrderBy(x => (int)x.cardRank).ToList();

        if (printLogs)
        {
            foreach (var card in hand)
            {
                Console.WriteLine(id + " " + card.ID());
            }
        }

        // To cover the A, 2, 3, 4, 5 straight, add ace to the beginning if present
        if (hand.Last().cardRank == CardRank.Ace)
        {
            hand.Insert(0, new Card(hand.Last().suit, hand.Last().cardRank));
        }

        var streak = new List<Card> { hand[0] };
        var bestStreak = new List<Card>();

        for (var x = 1; x < hand.Count; x++)
        {
            if (printLogs)
            {
                Console.WriteLine(id + " loop #" + x);
                Console.WriteLine(id + " previous card: " + hand[x - 1].ID());
                Console.WriteLine(id + " current card: " + hand[x].ID());
            }

            // Case 1: Same rank so ignore
            if (hand[x].cardRank == hand[x - 1].cardRank)
            {
                if (printLogs)
                {
                    Console.WriteLine(id + " case 1: same rank so ignore");
                }

                continue;
            }

            // Case 2: Ascending rank so add to streak
            if ((int)hand[x].cardRank - 1 == (int)hand[x - 1].cardRank ||
                (hand[x - 1].cardRank == CardRank.Ace && hand[x].cardRank == CardRank.Deuce))
            {
                if (printLogs)
                {
                    Console.WriteLine(id + " case 2: ascending rank so add to streak");
                }

                streak.Add(hand[x]);
                continue;
            }

            // Case 3: Not ascending so optionally save then start a new streak
            if (streak.Count >= bestStreak.Count)
            {
                bestStreak = new List<Card>(streak);
                if (printLogs)
                {
                    Console.WriteLine(id + " update bestStreak. New count is " + bestStreak.Count);
                }
            }

            streak.Clear();
            streak.Add(hand[x]);
        }

        // We might have ended on a streak
        if (streak.Count >= bestStreak.Count)
        {
            bestStreak = streak;
        }

        if (printLogs)
        {
            Console.WriteLine(id + " bestStreak count is " + bestStreak.Count);
            foreach (var card in bestStreak)
            {
                Console.WriteLine(id + " best streak " + card.ID());
            }
        }

        if (bestStreak.Count >= 5)
        {
            bestStreak = bestStreak.OrderBy(x => (int)x.cardRank).ToList();
            return new HandRankAndCards(HandRank.Straight, bestStreak.GetRange(bestStreak.Count - 5, 5));
        }

        return null;
    }

    public static HandRankAndCards IsFlush(List<Card> hand)
    {
        var spadeCount = 0;
        var clubCount = 0;
        var diamondCount = 0;
        var heartCount = 0;

        foreach (var card in hand)
        {
            switch (card.suit)
            {
                case Suit.Spades:
                    spadeCount++;
                    break;
                case Suit.Clubs:
                    clubCount++;
                    break;
                case Suit.Diamonds:
                    diamondCount++;
                    break;
                case Suit.Hearts:
                    heartCount++;
                    break;
            }
        }

        if (spadeCount < 5 && clubCount < 5 && diamondCount < 5 && heartCount < 5)
        {
            return null;
        }

        var cards = new List<Card>();
        if (spadeCount >= 5)
        {
            cards = hand.FindAll(x => x.suit == Suit.Spades);
        }
        else if (clubCount >= 5)
        {
            cards = hand.FindAll(x => x.suit == Suit.Clubs);
        }
        else if (diamondCount >= 5)
        {
            cards = hand.FindAll(x => x.suit == Suit.Diamonds);
        }
        else
        {
            cards = hand.FindAll(x => x.suit == Suit.Hearts);
        }

        return new HandRankAndCards(HandRank.Flush, cards.OrderBy(x => (int)x.cardRank).ToList());
    }

    private static string TextForRank(HandRank rank)
    {
        switch (rank)
        {
            case HandRank.RoyalFlush:
                return "Royal flush";

            case HandRank.StraightFlush:
                return "Straight flush";

            case HandRank.FourOfAKind:
                return "Four of a kind";

            case HandRank.FullHouse:
                return "Full house";

            case HandRank.Flush:
                return "Flush";

            case HandRank.Straight:
                return "Straight";

            case HandRank.ThreeOfAKind:
                return "Three of a kind";

            case HandRank.TwoPair:
                return "Two pair";

            case HandRank.OnePair:
                return "One pair";

            case HandRank.HighCard:
                return "High card";
        }

        return "";
    }
}

public enum HandStrength
{
    Weak,
    Okay,
    Strong
}