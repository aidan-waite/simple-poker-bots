public static class HandDescriber
{
    public static void Describe(long h)
    {
        var v = "";
        
        // Deuce
        if ((1 & h) > 0)
        {
            v += "Deuce of hearts\n";
        }
        
        if ((1 & (h >> 1)) > 0)
        {
            v += "Deuce of diamonds\n";
        }
        
        if ((1 & (h >> 2)) > 0)
        {
            v += "Deuce of spades\n";
        }
        
        if ((1 & (h >> 3)) > 0)
        {
            v += "Deuce of clubs\n";
        }

        // Three
        
        if ((1 & (h >> 4)) > 0)
        {
            v += "Three of hearts\n";
        }
        
        if ((1 & (h >> 5)) > 0)
        {
            v += "Three of diamonds\n";
        }
        
        if ((1 & (h >> 6)) > 0)
        {
            v += "Three of spades\n";
        }
        
        if ((1 & (h >> 7)) > 0)
        {
            v += "Three of clubs\n";
        }
        
        // Four
        
        if ((1 & (h >> 8)) > 0)
        {
            v += "Four of hearts\n";
        }
        
        if ((1 & (h >> 9)) > 0)
        {
            v += "Four of diamonds\n";
        }
        
        if ((1 & (h >> 10)) > 0)
        {
            v += "Four of spades\n";
        }
        
        if ((1 & (h >> 11)) > 0)
        {
            v += "Four of clubs\n";
        }
        
        // Five
        
        if ((1 & (h >> 12)) > 0)
        {
            v += "Five of hearts\n";
        }
        
        if ((1 & (h >> 13)) > 0)
        {
            v += "Five of diamonds\n";
        }
        
        if ((1 & (h >> 14)) > 0)
        {
            v += "Five of spades\n";
        }
        
        if ((1 & (h >> 15)) > 0)
        {
            v += "Five of clubs\n";
        }
        
        // Six
        
        if ((1 & (h >> 16)) > 0)
        {
            v += "Six of hearts\n";
        }
        
        if ((1 & (h >> 17)) > 0)
        {
            v += "Six of diamonds\n";
        }
        
        if ((1 & (h >> 18)) > 0)
        {
            v += "Six of spades\n";
        }
        
        if ((1 & (h >> 19)) > 0)
        {
            v += "Six of clubs\n";
        }
        
        // Seven
        
        if ((1 & (h >> 20)) > 0)
        {
            v += "Seven of hearts\n";
        }
        
        if ((1 & (h >> 21)) > 0)
        {
            v += "Seven of diamonds\n";
        }
        
        if ((1 & (h >> 22)) > 0)
        {
            v += "Seven of spades\n";
        }
        
        if ((1 & (h >> 23)) > 0)
        {
            v += "Seven of clubs\n";
        }
        
        // Eight
        
        if ((1 & (h >> 24)) > 0)
        {
            v += "Eight of hearts\n";
        }
        
        if ((1 & (h >> 25)) > 0)
        {
            v += "Eight of diamonds\n";
        }
        
        if ((1 & (h >> 26)) > 0)
        {
            v += "Eight of spades\n";
        }
        
        if ((1 & (h >> 27)) > 0)
        {
            v += "Eight of clubs\n";
        }
        
        // Nine
        
        if ((1 & (h >> 28)) > 0)
        {
            v += "Nine of hearts\n";
        }
        
        if ((1 & (h >> 29)) > 0)
        {
            v += "Nine of diamonds\n";
        }
        
        if ((1 & (h >> 30)) > 0)
        {
            v += "Nine of spades\n";
        }
        
        if ((1 & (h >> 31)) > 0)
        {
            v += "Nine of clubs\n";
        }
        
        // Ten
        
        if ((1 & (h >> 32)) > 0)
        {
            v += "Ten of hearts\n";
        }
        
        if ((1 & (h >> 33)) > 0)
        {
            v += "Ten of diamonds\n";
        }
        
        if ((1 & (h >> 34)) > 0)
        {
            v += "Ten of spades\n";
        }
        
        if ((1 & (h >> 35)) > 0)
        {
            v += "Ten of clubs\n";
        }
        
        // Jack
        
        if ((1 & (h >> 36)) > 0)
        {
            v += "Jack of hearts\n";
        }
        
        if ((1 & (h >> 37)) > 0)
        {
            v += "Jack of diamonds\n";
        }
        
        if ((1 & (h >> 38)) > 0)
        {
            v += "Jack of spades\n";
        }
        
        if ((1 & (h >> 39)) > 0)
        {
            v += "Jack of clubs\n";
        }
        
        // Queen
        
        if ((1 & (h >> 40)) > 0)
        {
            v += "Queen of hearts\n";
        }
        
        if ((1 & (h >> 41)) > 0)
        {
            v += "Queen of diamonds\n";
        }
        
        if ((1 & (h >> 42)) > 0)
        {
            v += "Queen of spades\n";
        }
        
        if ((1 & (h >> 43)) > 0)
        {
            v += "Queen of clubs\n";
        }
        
        // King
        
        if ((1 & (h >> 44)) > 0)
        {
            v += "King of hearts\n";
        }
        
        if ((1 & (h >> 45)) > 0)
        {
            v += "King of diamonds\n";
        }
        
        if ((1 & (h >> 46)) > 0)
        {
            v += "King of spades\n";
        }
        
        if ((1 & (h >> 47)) > 0)
        {
            v += "King of clubs\n";
        }
        
        // Ace
        
        if ((1 & (h >> 48)) > 0)
        {
            v += "Ace of hearts\n";
        }
        
        if ((1 & (h >> 49)) > 0)
        {
            v += "Ace of diamonds\n";
        }
        
        if ((1 & (h >> 50)) > 0)
        {
            v += "Ace of spades\n";
        }
        
        if ((1 & (h >> 51)) > 0)
        {
            v += "Ace of clubs\n";
        }
        
        Console.WriteLine(v);
    }
}