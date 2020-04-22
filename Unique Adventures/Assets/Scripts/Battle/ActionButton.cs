using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

//a button for players to select actions
public class ActionButton : MonoBehaviour
{
    private IEnumerator currentCoroutine;

    string buttonInfo;
    BoxCollider2D buttonCollider;
    TextMeshPro buttonText;
    Image buttonImage;

    BattleActions battleAction;
    BaseChampion selectedChampion;

    //class constructor for creating a button
    public void Init(string _stringArg, BattleActions _actionArg, BaseChampion _championArg)
    {
        buttonInfo = _stringArg;
        battleAction = _actionArg;
        selectedChampion = _championArg;
        buttonCollider = gameObject.GetComponent<BoxCollider2D>();
        buttonText = gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();
        buttonImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        buttonText.SetText(buttonInfo);
    }

    //attempt the action of the button that is selected
    public void AttemptButtonAction(BaseChampion selectedChampion)
    {
        selectedChampion.SelectAction(battleAction);
    }

    //when the button gets clicked...
    void OnMouseDown()
    {
        AttemptButtonAction(selectedChampion);
    }
};