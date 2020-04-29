using System.Collections;
using System.Collections.Generic;

//enumerator for all stats avaiable
enum Stat
{
    HPcurrent,
    HPmax,
    MPcurrent,
    MPmax,
    Strength,
    Dexterity,
    Constitution,
    Inteligence,
    Wisdom,
    Charisma
};

public class ChampionStats
{
    public string championName;

    public int HPcurrent;
    public int HPmax;
    public int MPcurrent;
    public int MPmax;
    public int strength;
    public int dexterity;
    public int constitution;
    public int inteligence;
    public int wisdom;
    public int charisma;

    public override string ToString()
    {
        string text = championName + ":\n"
            + "Max HP - " + HPmax + " / "
            + "Max MP - " + MPmax + " / "
            + "Str - " + strength + " / "
            + "Dex - " + dexterity + " / "
            + "Con - " + constitution + " / "
            + "Int - " + inteligence + " / "
            + "Wis - " + wisdom + " / "
            + "Cha - " + charisma;

        return text;
    }

    //a method that distributes points for player stats
    public void RandomizePlayerStats(string playerNameArg)
    {
        championName = playerNameArg;

        int[] pointDistribution = PointBuyStatGeneration(5, 10, 2, 20);
        HPmax = pointDistribution[0];
        MPmax = pointDistribution[1];

        int[] attributeDistribution = PointBuyStatGeneration(7, 38, 6, 18);
        strength = attributeDistribution[0];
        dexterity = attributeDistribution[1];
        constitution = attributeDistribution[2];
        inteligence = attributeDistribution[3];
        wisdom = attributeDistribution[4];
        charisma = attributeDistribution[5];

 
        HPcurrent = HPmax;
        MPcurrent = MPmax;
    }

    //a method that randomizes all enemy stats
    public void RandomizeEnemyStats(string enemyNameArg)
    {
        championName = enemyNameArg;

        HPmax = Randomizer.RandomInt(3, 7);
        MPmax = Randomizer.RandomInt(3, 7);
        strength = Randomizer.RandomInt(3, 7);
        dexterity = Randomizer.RandomInt(3, 7);
        constitution = Randomizer.RandomInt(3, 7);
        inteligence = Randomizer.RandomInt(3, 7);
        wisdom = Randomizer.RandomInt(3, 7);
        charisma = Randomizer.RandomInt(3, 7);


        HPcurrent = HPmax;
        MPcurrent = MPmax;
    }

    //returns a random int array of stat values based on a point buy system
    public int[] PointBuyStatGeneration(int baseValueOfStats, int numberOfPointsToDistribute, int numberOfStats, int statThreshold)
    {
        int pointsRemaining = numberOfPointsToDistribute;
        int[] stats = new int[numberOfStats];

        //set all stat values to the base value
        for (int idx = 0; idx < stats.Length; idx++)
        {
            stats[idx] = baseValueOfStats;
        }

        //Loop though while loop until there are no more points to be distributed
        while(pointsRemaining > 0)
        {
            //get a random stat index
            int randomStatIndex = Randomizer.RandomInt(0, stats.Length);

            //if the stat will not exceed the threshold... add the point to it
            if (stats[randomStatIndex] + 1 <= statThreshold)
            {
                stats[randomStatIndex]++;
                pointsRemaining--;
            }
        }

        return stats;
    }
}
