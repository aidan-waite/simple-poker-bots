using System.Diagnostics;

// Stateless helper functions for straights
public static class StraightHelpers
{
    public static long HighCardForStraight(long ranks)
    {
        long straightHighCard = 0b_0000_0000_0000_0000;
        var current = 13;
        var streak = 0;
        while (current >= 1 && streak < 5)
        {
            long val = ranks >> current;
            if ((val & 1) == 1)
            {
                if (streak == 0)
                {
                    straightHighCard = 1 << current;
                }
                streak++;
            }
            else
            {
                streak = 0;
            }

            current--;
        }
        
        Debug.Assert(straightHighCard > 0, "Unable to find high card in straight. Something is wrong!");
        return straightHighCard;
    }
    
    public static int MaxConsecutiveRanks(long val)
    {
        var count = 0;
        while (val != 0)
        {
            // This operation reduces length
            // of every sequence of 1s by one.
            val = (val & (val << 1));
            count++;
        }
 
        return count;
    }
}