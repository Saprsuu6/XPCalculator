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
            // �������� �� ���������� ������ ������
        }

        [TestMethod]
        public void RomanNumberParseNullEx()
        {
            var exc = Assert.ThrowsException<ArgumentNullException>(() => { RomanNumber.Parse(null!); });
            var exp = new ArgumentNullException();
            Assert.AreEqual(exp.GetType(), exc.GetType());
            // �������� �� ������������ ����� ����������
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

            var exc = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("MCMXCIX-"); });
            var exp = new ArgumentException("Minus coul be only one and at start");
            Assert.AreEqual(exp.Message, exc.Message);

            var exc1 = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("-MC-MXCIX"); });
            var exp1 = new ArgumentException("Minus coul be only one and at start");
            Assert.AreEqual(exp1.Message, exc1.Message);

            var exc2 = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("--MC-MXCIX--"); });
            var exp2 = new ArgumentException("Minus coul be only one and at start");
            Assert.AreEqual(exp1.Message, exc1.Message);
        }

        [TestMethod]
        public void RomanNumberNegativeToString()
        {
            //test for negative numbers
            RomanNumber romanNumber = new RomanNumber();
            Assert.AreEqual("N", romanNumber.ToString());

            romanNumber = new RomanNumber(-10);
            Assert.AreEqual("-X", romanNumber.ToString());

            romanNumber = new RomanNumber(-20);
            Assert.AreEqual("-XX", romanNumber.ToString());

            romanNumber = new RomanNumber(-400);
            Assert.AreEqual("-CD", romanNumber.ToString());

            romanNumber = new RomanNumber(-900);
            Assert.AreEqual("-CM", romanNumber.ToString());
        }
    }

    [TestClass]
    public class OperationsTest
    {
        [TestMethod]
        public void AddRNTest()
        {
            RomanNumber rn1 = new(10);
            RomanNumber rn2 = new(20);
            RomanNumber rn3 = new(-5);
            RomanNumber rn4 = new(-10);
            RomanNumber rn5 = new(-15);

            Assert.AreEqual(30, rn2.Add(rn1).Number);
            Assert.AreEqual(40, rn2.Add(rn2).Number);
            Assert.AreEqual(-15, rn3.Add(rn4).Number);
            Assert.AreEqual(-15, rn4.Add(rn3).Number);
            Assert.AreEqual(-15, rn4.Add(rn3).Number);
            Assert.AreEqual(rn5, rn4.Add(rn3));
            Assert.AreEqual(rn5.ToString(), rn4.Add(rn3).ToString());
            Assert.AreEqual("-V", rn5.Add(rn1).ToString());
        }

        [TestMethod]
        public void AddValueTest()
        {
            RomanNumber rn = new(10);
            Assert.AreEqual(20, rn.Add(10).Number);
            Assert.AreEqual("V", rn.Add(-5).ToString());
            Assert.AreEqual(rn, rn.Add("N"));
        }

        [TestMethod]
        public void AddStringTest()
        {
            RomanNumber rn = new(10);
            Assert.AreEqual(30, rn.Add("XX").Number);
            Assert.AreEqual("-XL", rn.Add("-L").ToString());
            Assert.AreEqual(rn, rn.Add("N"));

            Assert.ThrowsException<ArgumentNullException>(() => rn.Add(""));
            Assert.ThrowsException<ArgumentException>(() => rn.Add("-"));
            Assert.ThrowsException<ArgumentException>(() => rn.Add("10"));
            Assert.ThrowsException<ArgumentNullException>(() => rn.Add((string)null!));
        }

        [TestMethod]
        public void AddStaticTest()
        {
            RomanNumber rn5 = RomanNumber.Add(2, 3);
            Assert.AreEqual(5, rn5.Number);

            RomanNumber rn8 = RomanNumber.Add(rn5, 3);
            Assert.ThrowsException<ArgumentNullException>(() => RomanNumber.Add(null!, 3)); // проверка исключительных ситуаций
            Assert.AreEqual(8, rn8.Number);

            RomanNumber rn10 = RomanNumber.Add("I", "IX");
            Assert.ThrowsException<ArgumentNullException>(() => RomanNumber.Add("I", null!)); 
            Assert.AreEqual(10, rn10.Number);

            RomanNumber rn9 = RomanNumber.Add(rn5, "IV");
            Assert.ThrowsException<ArgumentNullException>(() => RomanNumber.Add((RomanNumber)null!, "IV")); 
            Assert.AreEqual(9, rn9.Number);

            RomanNumber rn13 = RomanNumber.Add(rn5, rn8);
            Assert.ThrowsException<ArgumentNullException>(() => RomanNumber.Add((RomanNumber)null!, (RomanNumber)null!)); 
            Assert.AreEqual(13, rn13.Number);
        }
    }
}

