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

        HPmax = Randomizer.RandomInt(5, 20);
        MPmax = Randomizer.RandomInt(5, 20);
        strength = Randomizer.RandomInt(5, 20);
        dexterity = Randomizer.RandomInt(5, 20);
        constitution = Randomizer.RandomInt(5, 20);
        inteligence = Randomizer.RandomInt(5, 20);
        wisdom = Randomizer.RandomInt(5, 20);
        charisma = Randomizer.RandomInt(5, 20);


        HPcurrent = HPmax;
        MPcurrent = MPmax;
        
    }

    //a method that randomizes all enemy stats
    public void RandomizeEnemyStats()
    {
        name = "Goblin";

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
}
