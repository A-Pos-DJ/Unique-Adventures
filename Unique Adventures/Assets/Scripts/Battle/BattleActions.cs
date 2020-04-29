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
        public override IEnumerator AttemptAction(BaseChampion attackingChampion)
        {
            int damageTaken = 0;
            BaseChampion defendingChampion;

            attackingChampion.targetConfirmed = false;
            yield return StartCoroutine(WaitForTargetSelect(attackingChampion, "Attack"));

            defendingChampion = attackingChampion.targetChampion;
            BattleGUI._battleGUI.SetBattleMessage(attackingChampion.stats.championName + " is attacking " + defendingChampion.stats.championName);
            damageTaken = attackingChampion.stats.strength - (defendingChampion.stats.constitution / 10);
            if (defendingChampion.currentAction is BattleActions.Defend)
                damageTaken = damageTaken / 4;
            defendingChampion.TakeDamage(damageTaken);
            attackingChampion.actionTaken = true;
            yield return null;
        }

    }

    public class Defend : BattleActions                 //a subclass for defending against the opponent
    {
        public override string ToString() { return "Defend"; }
        public override IEnumerator AttemptAction(BaseChampion thisChampion)
        {
            BattleGUI._battleGUI.SetBattleMessage(thisChampion.stats.championName + " is defending...");
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
            BattleGUI._battleGUI.SetBattleMessage(thisChampion.stats.championName + ", select a target to " + nameOfAction);
        while (!thisChampion.targetConfirmed) { yield return null; }
    }


}
