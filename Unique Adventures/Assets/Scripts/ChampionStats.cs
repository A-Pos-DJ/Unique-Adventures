using System.Collections;

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
    public string name;

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
        string text = name + ":\n"
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

    //a method that randomizes all player stats
    public void RandomizePlayerStats()
    {
        name = "Rando";
        int randomStat = Randomizer.RandomInt(5, 20);

        HPmax = randomStat;
        MPmax = randomStat;
        strength = randomStat;
        dexterity = randomStat;
        constitution = randomStat;
        inteligence = randomStat;
        wisdom = randomStat;
        charisma = randomStat;


        HPcurrent = HPmax;
        MPcurrent = MPmax;
        
    }

    //a method that randomizes all enemy stats
    public void RandomizeEnemyStats()
    {
        name = "Goblin";
        int randomStat = Randomizer.RandomInt(3, 7);

        HPmax = randomStat;
        MPmax = randomStat;
        strength = randomStat;
        dexterity = randomStat;
        constitution = randomStat;
        inteligence = randomStat;
        wisdom = randomStat;
        charisma = randomStat;


        HPcurrent = HPmax;
        MPcurrent = MPmax;

    }
}
