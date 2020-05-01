using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringManager : MonoBehaviour
{
    //database which contains all random names to pick from
    public string[] nameDatabase = new string[104] 
    {
                "Abigail", "Ace", "Achilles", "Artemis",
                "Bender", "Belle", "Beowulf", "Blue",
                "Caleb", "Caledon", "Cassandra", "Castiel",
                "Daenerys", "Daphne", "Darla", "Deacon",
                "Ebenezer", "Eddard", "Eleanor", "Eloise",
                "Fargo", "Felix", "Forrest", "Fox",
                "Gabriel", "Genevieve", "Geordi", "Gidget",
                "Hamlet", "Harper", "Harriet", "Hazel",
                "Ichabod", "Isaac", "Isabella", "Isla",
                "Jace", "Jackson", "Jacquotte", "Jenni",
                "Katniss", "Kent", "Kermit", "Khadija",
                "Lance", "Landon", "Leia", "Lilith",
                "Macbeth", "Magenta", "Marcia", "Maria",
                "Napoleon", "Nev", "Nixie", "Nora",
                "Olenna", "Olive", "Oscar", "Othello",
                "Penelope", "Phoebe", "Portia", "Praxidike",
                "Quinn", "Quincy", "Qing", "Questa",
                "Rachel", "Ragnard", "Ramona", "Raylan",
                "Sabrina", "Sadie", "Samuel", "Sansa",
                "Theon", "Thurston", "Tiberius", "Titania",
                "Uriah", "Uther", "Umi", "Ursa",
                "Vahn", "Vaiana", "Violet", "Vivian",
                "Webster", "William", "Willson", "Wade",
                "Xorn", "Xenia", "Xola", "Xuan",
                "Ygritte", "Yachi", "Yan", "Yael",
                "Ziggy", "Zac", "Zara", "Zephyr"
    };

    public string randomPlayerName
    {
        get
        {
            return nameDatabase[Randomizer.RandomInt(0, nameDatabase.Length - 1)];
        }
    }
}
