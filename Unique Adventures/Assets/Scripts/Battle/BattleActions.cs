using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a class used for implementing battle actions
public class BattleActions : MonoBehaviour
{
    //public virtual IEnumerator AttemptAction(BaseChampion thisChampion) { yield return null; }
    public virtual IEnumerator AttemptAction(BaseChampion thisChampion) { yield return null; }

    public class Attack : BattleActions                 //a subclass for attacking the opponent
    {
        public override string ToString() { return "Attack"; }
        public override IEnumerator AttemptAction(BaseChampion thisChampion)
        {
            thisChampion.targetConfirmed = false;
            yield return StartCoroutine(WaitForTargetSelect(thisChampion, "Attack"));
            BattleGUI._battleGUI.SetBattleMessage(thisChampion.stats.name + " is attacking " + thisChampion.targetChampion.stats.name);
            thisChampion.actionTaken = true;
            yield return null;
        }

    }

    public class Defend : BattleActions                 //a subclass for defending against the opponent
    {
        public override string ToString() { return "Defend"; }
        public override IEnumerator AttemptAction(BaseChampion thisChampion)
        {
            BattleGUI._battleGUI.SetBattleMessage(thisChampion.stats.name + " is defending...");
            thisChampion.actionTaken = true;
            yield return null;
        }
    }

    public class Skills : BattleActions { }                 //a subclass for using skills in battle
    public class Magic : BattleActions { }                  //a subclass for using magic in battle
    public class Item : BattleActions { }                   //a subclass for using items in battle


    private IEnumerator WaitForTargetSelect(BaseChampion thisChampion, string nameOfAction)
    {
        if (thisChampion.GetComponent<PlayerChampion>() != null)
            BattleGUI._battleGUI.SetBattleMessage(thisChampion.stats.name + ", select a target to " + nameOfAction);
        while (!thisChampion.targetConfirmed) { yield return null; }
    }


}
