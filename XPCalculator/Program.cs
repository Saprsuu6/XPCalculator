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

try
{
    Console.WriteLine(RomanNumber.Parse("MCMXCIX-"));
    //Console.WriteLine(RomanNumber.Parse("-M-CMXC-"));
    //Console.WriteLine(RomanNumber.Parse("MCMXC-"));
    //Console.WriteLine(RomanNumber.Parse("C-C"));
    //Console.WriteLine(RomanNumber.Parse("--CC"));
    //Console.WriteLine(RomanNumber.Parse("CC--"));
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine(RomanNumber.Parse("-MCMXCXI"));

RomanNumber romanNumber2 = new(-1410);
Console.WriteLine(romanNumber2.ToString());

try
{
    RomanNumber rn = new(10);
    RomanNumber.Add(rn, "-C1");
    //rn.Add("-C1");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

