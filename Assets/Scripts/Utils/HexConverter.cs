using System;
using System.Linq;
using System.Numerics;

public class HexConverter
{
    public static string StringToHex(string input)
    {
        return "0x" + string.Concat(input.Select(c => ((int)c).ToString("x2")));
    }

    public static string StringToFelt(string input)
    {
        BigInteger feltNumber = 0;
        BigInteger baseMultiplier = 1;
        BigInteger maxFeltValue = BigInteger.Pow(2, 251) - 1;

        foreach (char c in input)
        {
            // Convert each character to its ASCII value and add it to the BigInteger
            BigInteger charValue = (int)c;
            feltNumber += charValue * baseMultiplier;

            // Increase the base multiplier for the next character
            baseMultiplier *= 256;

            // Check if feltNumber exceeds the max felt value
            if (feltNumber > maxFeltValue)
            {
                throw new ArgumentOutOfRangeException("The resulting number is out of range for a StarkNet 'felt'.");
            }
        }

        return feltNumber.ToString();
    }
}