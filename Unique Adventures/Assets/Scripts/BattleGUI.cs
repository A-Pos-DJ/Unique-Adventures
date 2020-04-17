using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleGUI : MonoBehaviour
{
    private List<PlayerChampion> players;

    public TextMeshPro charHP1;
    public TextMeshPro charHP2;
    public TextMeshPro charHP3;
    public TextMeshPro charHP4;

    public TextMeshPro battleMessage;

    public Texture2D button;

    //initalizer for this class
    public void Init(List<PlayerChampion> playersArg)
    {
        players = playersArg;

        charHP1 = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        charHP2 = gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();
        charHP3 = gameObject.transform.GetChild(2).GetComponent<TextMeshPro>();
        charHP4 = gameObject.transform.GetChild(3).GetComponent<TextMeshPro>();

        battleMessage = gameObject.transform.GetChild(4).GetComponent<TextMeshPro>();
    }

    //updates the GUI with all of the proper character info
    public void UpdateGUI()
    {
        //clear out pre-existing text
        ClearHPGUIText();

        for (int idx = 0; idx < players.Count; idx++)
        {
            if (idx == 0)
            {
                charHP1.SetText(players[idx].stats.name + "    HP : " + players[idx].stats.HPcurrent);
            }
            else if (idx == 1)
            {
                charHP2.SetText(players[idx].stats.name + "    HP : " + players[idx].stats.HPcurrent);
            }
            else if (idx == 2)
            {
                charHP3.SetText(players[idx].stats.name + "    HP : " + players[idx].stats.HPcurrent);
            }
            else if (idx == 3)
            {
                charHP4.SetText(players[idx].stats.name + "    HP : " + players[idx].stats.HPcurrent);
            }
        }
    }

    //sets the messages for the battle so that the player can follow
    public void SetBattleMessage(string text)
    {
        battleMessage.SetText(text);
    }


    //clears out all HP text
    private void ClearHPGUIText()
    {
        charHP1.SetText("");
        charHP2.SetText("");
        charHP3.SetText("");
        charHP4.SetText("");
    }

    //clears out all pre-existing text
    private void ClearAllGUIText()
    {
        charHP1.SetText("");
        charHP2.SetText("");
        charHP3.SetText("");
        charHP4.SetText("");
        battleMessage.SetText("");
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 100, 20), "Attack"))
        {
            for (int idx = 0; idx < players.Count; idx++)
            {
                players[idx].AttemptAction(Action.Attack);
            }
        }
    }


}
