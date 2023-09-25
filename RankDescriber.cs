public static class RankDescriber
{
    public static string Describe(long ranks)
    {
        var v = "";
        
        if ((1 & ranks) > 0)
        {
            v += "Low Ace\n";
        }
        
        if ((1 & (ranks >> 1)) > 0)
        {
            v += "Deuce\n";
        }
        
        if ((1 & (ranks >> 2)) > 0)
        {
            v += "Three\n";
        }
        
        if ((1 & (ranks >> 3)) > 0)
        {
            v += "Four\n";
        }
        
        if ((1 & (ranks >> 4)) > 0)
        {
            v += "Five\n";
        }
        
        if ((1 & (ranks >> 5)) > 0)
        {
            v += "Six\n";
        }
        
        if ((1 & (ranks >> 6)) > 0)
        {
            v += "Seven\n";
        }
        
        if ((1 & (ranks >> 7)) > 0)
        {
            v += "Eight\n";
        }
        
        if ((1 & (ranks >> 8)) > 0)
        {
            v += "Nine\n";
        }
        
        if ((1 & (ranks >> 9)) > 0)
        {
            v += "Ten\n";
        }
        
        if ((1 & (ranks >> 10)) > 0)
        {
            v += "Jack\n";
        }
        
        if ((1 & (ranks >> 11)) > 0)
        {
            v += "Queen\n";
        }
        
        if ((1 & (ranks >> 12)) > 0)
        {
            v += "King\n";
        }
        
        if ((1 & (ranks >> 13)) > 0)
        {
            v += "High Ace\n";
        }

        return v;
    }
}