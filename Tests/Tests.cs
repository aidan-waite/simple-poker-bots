public class Tests
{
    // var hand = 0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000;
    
    public void RunAllTests()
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine("Run all tests");
        Console.WriteLine("--------------------------");

        Console.WriteLine("Four of a kind tests");
        TestFourOfAKind();
        Console.WriteLine("--------------------------");
        
        Console.WriteLine("High card for straight tests");
        HighCardForStraightTest();
        Console.WriteLine("--------------------------");
    }

    private void HighCardForStraightTest()
    {
        TestHelper.TestRank("Ace high straight test", 0b_1_0000_0000_0000_0, StraightHelpers.HighCardForStraight(0b_1_1111_0000_0000_1));
        TestHelper.TestRank("King high straight test", 0b_0_1000_0000_0000_0, StraightHelpers.HighCardForStraight(0b_0_1111_1000_0110_0));
        
        TestHelper.TestRank("Ten high straight test", 0b_0_0001_0000_0000_0, StraightHelpers.HighCardForStraight(0b_0_0001_1111_1110_0));
        TestHelper.TestRank("Wheel straight test", 0b_0_0000_0000_1000_0, StraightHelpers.HighCardForStraight(0b_0_0000_0000_1111_1));
    }
    
    private void TestFourOfAKind()
    {
        TestHelper.Test("Four aces 1", 714, HandStrengthCalculator.Calculate(0b_1111_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000));
        TestHelper.Test("Four aces 2", 714, HandStrengthCalculator.Calculate(0b_1111_1110_1011_0110_1110_0111_0110_0000_1111_0000_0000_0000_0000));
        
        TestHelper.Test("Four kings 1", 713, HandStrengthCalculator.Calculate(0b_0000_1111_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000));
        TestHelper.Test("Four kings 2", 713, HandStrengthCalculator.Calculate(0b_1100_1111_1111_1111_1110_0111_0110_0000_1111_0000_0000_0000_0000));
        
        TestHelper.Test("Four queens 1", 712, HandStrengthCalculator.Calculate(0b_0000_0100_1111_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000));
        TestHelper.Test("Four queens 2", 712, HandStrengthCalculator.Calculate(0b_0000_0100_1111_1111_1110_0111_0110_0000_1111_0000_0000_0000_0000));
        
        TestHelper.Test("Four jacks 1", 711, HandStrengthCalculator.Calculate(0b_0100_0000_0000_1111_0000_0000_0000_0000_0000_0000_0000_0000_0000));
        TestHelper.Test("Four jacks 2", 711, HandStrengthCalculator.Calculate(0b_0001_0010_0000_1111_1110_0111_0110_0000_1111_0000_0000_0000_0000));
        
        TestHelper.Test("Four tens 1", 710, HandStrengthCalculator.Calculate(0b_0100_0000_0000_0011_1111_0000_0000_0000_0000_0000_0000_0000_0000));
        TestHelper.Test("Four tens 2", 710, HandStrengthCalculator.Calculate(0b_0001_0010_0000_0001_1111_0111_0110_0000_1111_0000_0000_0000_0000));
        
        TestHelper.Test("Four nines 1", 709, HandStrengthCalculator.Calculate(0b_0100_0000_0000_1000_0000_1111_0000_0000_0000_0000_0000_0000_0000));
        TestHelper.Test("Four nines 2", 709, HandStrengthCalculator.Calculate(0b_0001_0010_0000_0000_0000_1111_0110_0000_1111_0000_0000_0000_0000));
        
        TestHelper.Test("Four eights 1", 708, HandStrengthCalculator.Calculate(0b_0100_0000_0000_1000_0000_0000_1111_0000_0110_0000_0100_0000_0000));
        TestHelper.Test("Four eights 2", 708, HandStrengthCalculator.Calculate(0b_0001_0010_0000_0000_0000_1000_1111_0000_1111_0000_1000_0000_0000));
        
        TestHelper.Test("Four sevens 1", 707, HandStrengthCalculator.Calculate(0b_0100_0000_0000_1000_0000_0000_0000_1111_0110_0000_0100_0000_0000));
        TestHelper.Test("Four sevens 2", 707, HandStrengthCalculator.Calculate(0b_0001_0010_0000_0000_0000_1000_0000_1111_1111_0000_1000_0000_0000));
        
        TestHelper.Test("Four sixes 1", 706, HandStrengthCalculator.Calculate(0b_0100_0000_0000_1000_0000_0000_0000_0000_1111_0000_0100_0000_0000));
        TestHelper.Test("Four sixes 2", 706, HandStrengthCalculator.Calculate(0b_0001_0010_0000_0000_0000_1000_0000_0001_1111_0000_1000_0000_0000));
        
        TestHelper.Test("Four fives 1", 705, HandStrengthCalculator.Calculate(0b_0100_0000_0000_1000_0000_0000_0000_0000_1000_1111_0100_0000_0000));
        TestHelper.Test("Four fives 2", 705, HandStrengthCalculator.Calculate(0b_0001_0010_0000_0000_0000_1000_0000_0001_0000_1111_1000_0000_0000));
        
        TestHelper.Test("Four fours 1", 704, HandStrengthCalculator.Calculate(0b_0100_0000_0000_1000_0000_0000_0000_0000_1000_0000_1111_0000_0000));
        TestHelper.Test("Four fours 2", 704, HandStrengthCalculator.Calculate(0b_0001_0010_0000_0000_0000_1000_0000_0001_0000_1000_1111_0000_0000));
        
        TestHelper.Test("Four threes 1", 703, HandStrengthCalculator.Calculate(0b_0100_0000_0000_1000_0000_0000_0000_0000_1000_0000_0000_1111_0000));
        TestHelper.Test("Four threes 2", 703, HandStrengthCalculator.Calculate(0b_0001_0010_0000_0000_0000_1000_0000_0001_0000_1000_0000_1111_0000));
        
        TestHelper.Test("Four deuces 1", 702, HandStrengthCalculator.Calculate(0b_0100_0000_0000_1000_0000_0000_0000_0000_1000_0000_0000_1000_1111));
        TestHelper.Test("Four deuces 2", 702, HandStrengthCalculator.Calculate(0b_0001_0010_0000_0000_0000_1000_0000_0001_0000_1000_0000_0011_1111));
    }
}