// var hand = 0b_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111;
// var hand = 0b_1001_1111_1111_0110_1000_0000_0010_0000_0000_0000_0000_0001_0000;
var hand1 = 0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000;
var hand2 = 0b_0000_0100_0000_0000_0100_0000_0000_0000_0100_0110_0010_0100_0001;
var hand3 = 0b_0010_0100_0000_0000_0100_0000_0000_0000_0100_000_0010_0100_0001;

var handStrength = new ShowdownHandStrength();

Console.WriteLine("\n---\nhand1");
handStrength.DescribeHand(hand1);
handStrength.HandStrength(hand1);

Console.WriteLine("\n---\nhand2");
handStrength.DescribeHand(hand2);
handStrength.HandStrength(hand2);

Console.WriteLine("\n---\nhand3");
handStrength.DescribeHand(hand3);
handStrength.HandStrength(hand3);