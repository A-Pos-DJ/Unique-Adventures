using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

//a button for players to enter their name
public class NameButton : MonoBehaviour
{
    string buttonInfo;
    BoxCollider2D buttonCollider;
    TextMeshPro buttonText;
    Image buttonImage;


    //class constructor for creating a button
    public void Init(string _stringArg)
    {
        buttonInfo = _stringArg;
        buttonCollider = gameObject.GetComponent<BoxCollider2D>();
        buttonText = gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();
        buttonImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        buttonText.SetText(buttonInfo);
    }

    //the action of what happens when the button is pressed
    public void ButtonAction()
    {
        GameManager._game.playerNameEntered = true;
        GameManager.playerName = GameManager._game.nameTextfield;
        GameManager._game.Init();
        Destroy(this.gameObject);
    }

    //when the button gets clicked...
    void OnMouseDown()
    {
        ButtonAction();
    }
};
