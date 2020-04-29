using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerInfoPanel : MonoBehaviour
{
    PlayerChampion attachedChampion;

    BoxCollider2D panelCollider;
    TextMeshPro nameText;
    TextMeshPro HPText;
    TextMeshPro MPText;
    Image panelImage;

    //initalizer for the pannel
    public void Init(PlayerChampion championArg)
    {
        //if there is no champion to pass in... hide this panel
        if (championArg == null)
        {
            HidePanel();
            return;
        }

        attachedChampion = championArg;
        panelCollider = gameObject.GetComponent<BoxCollider2D>();
        nameText = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        HPText = gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();
        MPText = gameObject.transform.GetChild(2).GetComponent<TextMeshPro>();
        panelImage = gameObject.transform.GetChild(3).GetComponent<Image>();

        UpdateInfo();
    }

    //updates the information on the panel to match the current stats of the attached character
    public void UpdateInfo()
    {
        if (attachedChampion == null) return;

        nameText.SetText(attachedChampion.stats.championName);
        HPText.SetText("HP : " + attachedChampion.stats.HPcurrent.ToString());
        MPText.SetText("MP : " + attachedChampion.stats.MPcurrent.ToString());
    }

    //clears all of the info off of the panel
    public void ClearAllInfo()
    {
        if (attachedChampion == null) return;

        nameText.SetText("");
        HPText.SetText("");
        MPText.SetText("");
    }

    //clear all info, then hide this panel
    public void HidePanel()
    {
        ClearAllInfo();
        this.gameObject.SetActive(false);
    }

    //shows the panel
    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }

    //when the button gets clicked...
    void OnMouseDown()
    {
        ChampionOrder.currentChampionTurn.SelectTarget(attachedChampion);
    }
}
