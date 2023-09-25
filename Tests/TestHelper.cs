public static class TestHelper
{
    public static void Test(string name, int expectedValue, int actualValue)
    {
        Console.WriteLine((expectedValue == actualValue ? "🟢" : "🔴") + " Test " + name + " expected:" + expectedValue + " actual:" + actualValue);
    }
    
    public static void TestRank(string name, long expectedValue, long actualValue)
    {
        var expected = RankDescriber.Describe(expectedValue);
        var actual = RankDescriber.Describe(actualValue);
        Console.WriteLine((expectedValue == actualValue ? "🟢" : "🔴") + " Test " + name + " expected:" + expected + " actual:" + actual);
    }
}