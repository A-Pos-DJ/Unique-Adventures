using System;


//the randomizer that will generate any random number for the entire project from a presistant seen
public static class Randomizer
{
    //Random instance class to hold the random seed statically without crashing unity
    private class RandomInstance
    {
        public Random _random { get; private set; }

        //class constructor that creates a new random instance with the seed passed in from the game manger
        public RandomInstance()
        {
            if (_random == null)
                _random = new Random(GameManager.seed);
        }
    }

    //create a random instance class if there is not one already existing
    private static RandomInstance _randomInstanceClass = _randomInstanceClass ?? new RandomInstance();




    //a method to get a random int
    public static int RandomInt()
    {
        int randomInt = _randomInstanceClass._random.Next();
        return randomInt;
    }

    //a method to get a random int based on the passed in value
    public static int RandomInt(int intValue)
    {
        int randomInt = _randomInstanceClass._random.Next(intValue);
        return randomInt;
    }

    //a method to get a random int between two values
    public static int RandomInt(int minInt, int maxInt)
    {
        int randomInt = _randomInstanceClass._random.Next(minInt, maxInt); 
        return randomInt;
    }
}
