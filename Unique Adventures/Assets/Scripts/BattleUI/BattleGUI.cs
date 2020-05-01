using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

//a battle GUI class for handling battle buttons and UI info
public class BattleGUI : MonoBehaviour
{
    public static BattleGUI _battleGUI;

    private float XButtonStartPos = -8.65f;
    private float YButtonStartPos = 6f;

    private List<PlayerChampion> players;
    private List<ActionButton> actionButtons;

    public TextMeshPro battleMessage;
    public Image battleMessagePanel;

    public List<PlayerInfoPanel> championInfo;


    //initalizer for this class
    public void Init(List<PlayerChampion> playersArg)
    {
        _battleGUI = this;
        players = playersArg;
        championInfo = new List<PlayerInfoPanel>();
        actionButtons = new List<ActionButton>();

        battleMessage = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro>();
        battleMessagePanel = gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();

        for (int idx = 0; idx <= 4 - 1; idx++)
        {
            championInfo.Add(gameObject.transform.GetChild(idx + 1).GetComponent<PlayerInfoPanel>());
            if (players.Count - 1 >= idx)
                championInfo[idx].Init(players[idx]);
            else
                championInfo[idx].Init(null);
        }
    }

    //updates the GUI with all of the proper character info
    public void UpdateInfoPanels()
    {
        for (int idx = 0; idx <= championInfo.Count - 1; idx++)
        {
            championInfo[idx].ClearAllInfo();
            championInfo[idx].UpdateInfo();
        }
    }

    //sets the messages for the battle so that the player can follow
    public void SetBattleMessage(string text)
    {
        battleMessage.SetText(text);
    }

    //clears out all champion info text
    private void ClearChampionInfoPanelText()
    {
        for (int idx = 0; idx < championInfo.Count - 1; idx++)
        {
            championInfo[idx].ClearAllInfo();
        }
    }
    
    //clears out all pre-existing text
    private void ClearAllGUIText()
    {
        ClearChampionInfoPanelText();
        battleMessage.SetText("");
    }

    //creates buttons for actions so a player to choose an action
    public void UpdateActionsAvailable(BaseChampion currentChampion)
    {
        //clear pre-existing actions
        ClearActionButtons();

        //if the actions being updated are not for a player... return
        if (!(currentChampion is PlayerChampion)) return;

        //loop though actions avaiable and creates buttons for them
        for (int idx = 0; idx <= currentChampion.actions.Count - 1; idx++)
        {
            Vector3 bPosition = new Vector3(XButtonStartPos, YButtonStartPos - idx, 0);
            string actionName = currentChampion.actions[idx].ToString();

            CreateActionButton(bPosition, actionName, currentChampion.actions[idx], currentChampion);
        }
    }

    //creates a button for a player to use an action and ass it to the action button list
    private void CreateActionButton(Vector3 position, string buttonName, BattleActions buttonAction, BaseChampion championSelected)
    {
        GameObject newButtonObject = Instantiate(GameManager._prefabs.actionButtonPrefab, position, Quaternion.identity);
        ActionButton newButton = newButtonObject.GetComponent<ActionButton>();

        newButton.Init(buttonName, buttonAction, championSelected);
        actionButtons.Add(newButton);
    }

    //clears all action buttons from the screen 
    private void ClearActionButtons()
    {
        //loop though all buttons and destroy them
        for (int idx = 0; idx <= actionButtons.Count - 1; idx++)
        {
            Destroy(actionButtons[idx].gameObject);
        }

        actionButtons.Clear();
    }

    //resets action buttons and clears text for the next turn of combat
    public void UpdateGUIForNextTurn()
    {
        UpdateInfoPanels();
        ClearActionButtons();
    }
}
