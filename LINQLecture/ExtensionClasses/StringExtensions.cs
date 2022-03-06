namespace LINQLecture.ExtensionClasses;

public static class StringExtensions
{
    public static char MiddleChar(this string myString)
    {
        return myString[myString.Length / 2];
    }
}

