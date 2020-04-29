using System;

//the randomizer that will generate any random number for the entire project from a presistant seen
public class Randomizer
{
    private static readonly Random random = new Random(GameManager.seed);
    private static readonly object randomLock = new object();

    //a variable with passed in int paramters that generate presistent random values
    public static int RandomInt(params int[] intParams)
    {
        //check the number of ints passed in
        switch (intParams.Length)
        {
            case 1:
                lock (randomLock)
                {
                    return random.Next(intParams[0]);
                }
            case 2:
                lock (randomLock)
                {
                    return random.Next(intParams[0], intParams[1]);
                }
            default:
                lock (randomLock)
                {
                    return random.Next();
                }
        }
    }
}
