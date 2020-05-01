//a class used for converting values into seeds
public static class SeedGenerator
{
    //converts a string to an integer that we will use for creating a persistent seed
    public static int StringToInt(string stringToBeConverted)
    {
        //a variable to hold the seed value all seeds start with a '1'
        int randomSeed = 1;

        //check to see if the string is null or empty and replace it with the computer's mac address
        stringToBeConverted = stringToBeConverted ?? string.Empty;
        if (stringToBeConverted == string.Empty) stringToBeConverted = MacFetcher._string;

        //loop though all characters in the string to generate a seed
        for (int idx = 0; idx <= stringToBeConverted.Length -1; idx++)
        {
            //Concatenate the random seed with the char value
            randomSeed = ConcatenateForSeed(randomSeed, CharToInt(stringToBeConverted[idx]));
        }
        //return the random seed
        return randomSeed;
    }

    //concatenates two numbers for the purpose of creating a seed
    public static int ConcatenateForSeed(int firstNumber, int secondNumber)
    {
        int result;
        string combinedString = firstNumber.ToString() + secondNumber.ToString();

        //if the numbers can be combines with no issues, return the result
        if (int.TryParse(combinedString, out result)) return result;
        //if the numbers combined will cause an int overflow... subtract the second number
        if (long.Parse(combinedString) > int.MaxValue) return firstNumber - secondNumber;
        //if the numbers combined will cause an int underflow... add the second number
        else if (long.Parse(combinedString) < int.MinValue) return firstNumber + secondNumber;
        //if all else fails... just return 0
        else return 0;
    }

    //converts a character to an int (makes a numerical char value accurate)
    public static int CharToInt(this char charToBeConverted)
    {
        if (char.IsDigit(charToBeConverted))
            return (int)(charToBeConverted - '0');
        else
            return (int)charToBeConverted;
    }
}
