using System.Diagnostics;
using System.Numerics;

public static class HandStrengthCalculator
{
    // Returns the value of the best hand that can be made from the cards
    // Values are calculated using the base value for the hand type (0-900) plus the value of the relevant card (2 to 13)
    // For example, a pair of 8s has a value of 100 base + 8 = 108
    public static int Calculate(long h)
    {
        // Input hand format
        // Clubs Spades Diamonds Hearts
        // A A A A     K K K K    Q Q Q Q    J J J J    10 10 10 10     9 9 9 9     8 8 8 8     7 7 7 7     6 6 6 6     5 5 5 5     4 4 4 4     3 3 3 3     2 2 2 2
        // 0 0 0 0     0 0 0 0    0 0 0 0    0 0 0 0    0  0  0  0      0 0 0 0     0 0 0 0     0 0 0 0     0 0 0 0     0 0 0 0     0 0 0 0     0 0 0 0     0 0 0 0
        
        long ranks =
            (((h & 0b_1111_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000) > 0 ? 1 : 0) << 13) | // high A
            (((h & 0b_0000_1111_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000) > 0 ? 1 : 0) << 12) | // K
            (((h & 0b_0000_0000_1111_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000) > 0 ? 1 : 0) << 11) | // Q
            (((h & 0b_0000_0000_0000_1111_0000_0000_0000_0000_0000_0000_0000_0000_0000) > 0 ? 1 : 0) << 10) | // J
            (((h & 0b_0000_0000_0000_0000_1111_0000_0000_0000_0000_0000_0000_0000_0000) > 0 ? 1 : 0) << 9) | // 10
            (((h & 0b_0000_0000_0000_0000_0000_1111_0000_0000_0000_0000_0000_0000_0000) > 0 ? 1 : 0) << 8) | // 9
            (((h & 0b_0000_0000_0000_0000_0000_0000_1111_0000_0000_0000_0000_0000_0000) > 0 ? 1 : 0) << 7) | // 8
            (((h & 0b_0000_0000_0000_0000_0000_0000_0000_1111_0000_0000_0000_0000_0000) > 0 ? 1 : 0) << 6) | // 7
            (((h & 0b_0000_0000_0000_0000_0000_0000_0000_0000_1111_0000_0000_0000_0000) > 0 ? 1 : 0) << 5) | // 6
            (((h & 0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_0000_0000_0000) > 0 ? 1 : 0) << 4) | // 5
            (((h & 0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_0000_0000) > 0 ? 1 : 0) << 3) | // 4
            (((h & 0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_0000) > 0 ? 1 : 0) << 2) | // 3
            (((h & 0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111) > 0 ? 1 : 0) << 1) | // 2
            (((h & 0b_1111_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000) > 0 ? 1 : 0) << 0); // low A
        
        var isStraight = StraightHelpers.MaxConsecutiveRanks(ranks) >= 5;
        
        // If there's a straight, determine the highest card of the straight for later use
        var straightHighCard = 0b_00_0000_0000_0000;

        
        var clubCount =
            BitOperations.PopCount((ulong)(h & 0b_1000_1000_1000_1000_1000_1000_1000_1000_1000_1000_1000_1000_1000));  
        var spadeCount =
            BitOperations.PopCount((ulong)(h & 0b_0100_0100_0100_0100_0100_0100_0100_0100_0100_0100_0100_0100_0100));  
        var diamondCount =
            BitOperations.PopCount((ulong)(h & 0b_0010_0010_0010_0010_0010_0010_0010_0010_0010_0010_0010_0010_0010));  
        var heartCount =
            BitOperations.PopCount((ulong)(h & 0b_0001_0001_0001_0001_0001_0001_0001_0001_0001_0001_0001_0001_0001));  

        var isFlush = clubCount >= 5 || spadeCount >= 5 || diamondCount >= 5 || heartCount >= 5;

        var aceCount = BitOperations.PopCount((ulong)(h >> 48));
        var kingCount = BitOperations.PopCount((ulong)(h >> 44 & 0b_1111));
        var queenCount = BitOperations.PopCount((ulong)(h >> 40 & 0b_1111));
        var jackCount = BitOperations.PopCount((ulong)(h >> 36 & 0b_1111));
        var tenCount = BitOperations.PopCount((ulong)(h >> 32 & 0b_1111));
        var nineCount = BitOperations.PopCount((ulong)(h >> 28 & 0b_1111));
        var eightCount = BitOperations.PopCount((ulong)(h >> 24 & 0b_1111));
        var sevenCount = BitOperations.PopCount((ulong)(h >> 20 & 0b_1111));
        var sixCount = BitOperations.PopCount((ulong)(h >> 16 & 0b_1111));
        var fiveCount = BitOperations.PopCount((ulong)(h >> 12 & 0b_1111));
        var fourCount = BitOperations.PopCount((ulong)(h >> 8 & 0b_1111));
        var threeCount = BitOperations.PopCount((ulong)(h >> 4 & 0b_1111));
        var twoCount = BitOperations.PopCount((ulong)(h & 0b_1111));
        
        // Hand type 1: Royal Flush -- Range 900-1000
        // Hand type 2: Straight Flush -- Range 800-900
        if (isStraight && isFlush)
        {
            // TODO: Check if the straight and flush have 5 overlapping cards
            // TODO: Check if the straight is ace high
            
        }

        // Hand type 3: Four of a Kind -- Range 700-800
        if (aceCount == 4)
        {
            return 700 + 14;
        }
        if (kingCount == 4)
        {
            return 700 + 13;
        }
        if (queenCount == 4)
        {
            return 700 + 12;
        }
        if (jackCount == 4)
        {
            return 700 + 11;
        }
        if (tenCount == 4)
        {
            return 700 + 10;
        }
        if (nineCount == 4)
        {
            return 700 + 9;
        }
        if (eightCount == 4)
        {
            return 700 + 8;
        }
        if (sevenCount == 4)
        {
            return 700 + 7;
        }
        if (sixCount == 4)
        {
            return 700 + 6;
        }
        if (fiveCount == 4)
        {
            return 700 + 5;
        }
        if (fourCount == 4)
        {
            return 700 + 4;
        }
        if (threeCount == 4)
        {
            return 700 + 3;
        }
        if (twoCount == 4)
        {
            return 700 + 2;
        }
        
        // if ((h & 0b_1111) == 0b_1111)
        // {
        //     // four deuces
        //     Console.WriteLine("Four deuces");
        // } else if ((h >> 4 & 0b_1111) == 0b_1111)
        // {
        //     // four threes
        //     Console.WriteLine("Four threes");
        // }
        
        // Hand type 4: Full House

        // Hand type 5: Flush

        // Hand type 6: Straight

        // Hand type 7: Three of a Kind

        // Hand type 8: Two Pair

        // Hand type 9: Pair

        // Hand type 10: High Card

        return 0;
    }
}
