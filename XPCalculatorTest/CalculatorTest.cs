using XPCalculator.App;

namespace XPCalculatorTest
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void RomanNumberParse1D()
        {
            Assert.AreEqual(1, RomanNumber.Parse("I"));
        }

        [TestMethod]
        public void RomanNumberParse2D()
        {
            Assert.AreEqual(4, RomanNumber.Parse("IV"));
            Assert.AreEqual(15, RomanNumber.Parse("XV"));
            Assert.AreEqual(900, RomanNumber.Parse("CM"));
            Assert.AreEqual(400, RomanNumber.Parse("CD"));
            Assert.AreEqual(55, RomanNumber.Parse("LV"));
            Assert.AreEqual(40, RomanNumber.Parse("XL"));
        }

        [TestMethod]
        public void RomanNumberParse3D()
        {
            Assert.AreEqual(401, RomanNumber.Parse("CDI"));
        }

        [TestMethod]
        public void RomanNumberParseInvalidDigit1()
        {
            var exc = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("XXA"); });
            var exp = new ArgumentException("Invalid char A");
            Assert.AreEqual(exp.Message, exc.Message);
        }

        [TestMethod]
        public void RomanNumberParseInvalidDigit2()
        {
            var exc = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("CD@"); });
            var exp = new ArgumentException("Invalid char @");
            Assert.AreEqual(exp.Message, exc.Message);
        }

        [TestMethod]
        public void RomanNumberParseInvalidDigit3()
        {
            var exc = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("CUO"); });
            var exp = new ArgumentException("Invalid char O");
            Assert.AreEqual(exp.Message, exc.Message);
        }

        [TestMethod]
        public void RomanNumberParseInvalidDigit4()
        {
            var exc = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("X X"); });
            var exp = new ArgumentException("Invalid char");
            Assert.IsTrue(exc.Message.Contains(exp.Message));
        }

        [TestMethod]
        public void RomanNumberParseEmpty()
        {
            var exc = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse(""); });
            var exp = new ArgumentException("Empty string not allowed");
            Assert.AreEqual(exp.Message, exc.Message);
            // проверка на отклонение пустой строки
        }

        [TestMethod]
        public void RomanNumberParseNullEx()
        {
            var exc = Assert.ThrowsException<ArgumentNullException>(() => { RomanNumber.Parse(null!); });
            var exp = new ArgumentNullException();
            Assert.AreEqual(exp.GetType(), exc.GetType());
            // проверка на соответствие типов исключения
        }

        [TestMethod]
        public void RomanNumberParseAllowN()
        {
            // check if RomanNumber.Parse returns 0 with "N" argument
            Assert.AreEqual(RomanNumber.Parse("N"), 0);
        }

        [TestMethod]
        public void RomanNumberIsNotNull()
        {
            RomanNumber romanNumber = new();
            Assert.IsNotNull(romanNumber);

            RomanNumber romanNumber2 = new(10);
            Assert.IsNotNull(romanNumber);
        }

        [TestMethod]
        public void RomanNumberToString()
        {
            RomanNumber romanNumber = new(10);
            Assert.AreEqual("X", romanNumber.ToString());
        }

        [TestMethod]
        public void RomanNumberToStringParseCrossTest()
        {
            Random rnd = new Random();
            int number = rnd.Next(0, 2022);

            RomanNumber romanNumber = new(number);
            Assert.AreEqual(number, RomanNumber.Parse(romanNumber.ToString()));
        }

        [TestMethod]
        public void RomanNumberTypeTest()
        {
            RomanNumber rn1 = new(10);
            RomanNumber rn2 = rn1;
            Assert.AreSame(rn1, rn2);

            RomanNumber rn3 = rn1 with { };
            Assert.AreNotSame(rn3, rn1);
            Assert.AreEqual(rn3, rn1);
            Assert.IsTrue(rn1.Equals(rn3));

            RomanNumber rn4 = rn1 with { Number = 30 };
            Assert.AreNotSame(rn4, rn1);
            Assert.AreNotEqual(rn4, rn1);
            Assert.IsFalse(rn1.Equals(rn4));
        }

        [TestMethod]
        public void RomanNumberNegativeParsing()
        {
            Assert.AreEqual(-10, RomanNumber.Parse("-X"));
            Assert.AreEqual(-1999, RomanNumber.Parse("-MCMXCIX"));
            Assert.AreEqual(-900, RomanNumber.Parse("-CM"));
            Assert.AreEqual(-400, RomanNumber.Parse("-CD"));
        }
    }
}

