namespace XPCalculatorTest
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void RomanNumberParse()
        {
            Assert.AreEqual(XPCalculator.App.RomanNumber.Parse("I"), 1, "I == 1");
            Assert.AreEqual(XPCalculator.App.RomanNumber.Parse("IV"), 4, "IV == 4");
            Assert.AreEqual(XPCalculator.App.RomanNumber.Parse("V"), 5, "V == 5");
        }
    }
}

