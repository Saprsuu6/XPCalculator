using XPCalculator.App;
//tests for the calculator
Console.WriteLine(RomanNumber.Parse("CM"));
Console.WriteLine(RomanNumber.Parse("CD"));
Console.WriteLine(RomanNumber.Parse("CCIC"));
Console.WriteLine(RomanNumber.Parse("MCMXC"));
Console.WriteLine(RomanNumber.Parse("CC"));
Console.WriteLine(RomanNumber.Parse("MCMXCXI"));
//RomanNumber.Parse(null!);
RomanNumber romanNumber = new(10);
Console.WriteLine(romanNumber.ToString());
// test for negative
Console.WriteLine(RomanNumber.Parse("-CM"));
Console.WriteLine(RomanNumber.Parse("-CD"));
Console.WriteLine(RomanNumber.Parse("-CCIC"));
Console.WriteLine(RomanNumber.Parse("-MCMXC"));
Console.WriteLine(RomanNumber.Parse("-CC"));
Console.WriteLine(RomanNumber.Parse("-MCMXCXI"));

RomanNumber romanNumber2 = new(-1410);
Console.WriteLine(romanNumber2.ToString());