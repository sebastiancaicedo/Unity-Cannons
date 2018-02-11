using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour {

    public static GameUIManager Instance { get; private set; }

    [SerializeField]
    Text playerTurnText;
    [SerializeField]
    RectTransform inputsMenu;
    [SerializeField]
    Text menuTitle;
    [SerializeField]
    InputField inputfieldAngle;
    [SerializeField]
    InputField inputfieldForce;
    //[SerializeField]
    //GameObject player2Menu;
    //[SerializeField]
    //InputField player2InputAngle;
    //[SerializeField]
    //InputField player2InputForce;

    public string AngleInput { get { return inputfieldAngle.text; } }
    public string ForceInput { get { return inputfieldForce.text; } }

    //public string Player2Angle { get { return player2InputAngle.text; } }
    //public string Player2Force { get { return player2InputForce.text; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerTurnText(PlayerTurn playerTurn)
    {
        playerTurnText.text = string.Format("It's Player {0} Turn", (int)playerTurn);
        menuTitle.text = string.Format("Player {0}", (int)playerTurn);
        ShowMenuForPlayer(playerTurn);
    }

    private void ShowMenuForPlayer(PlayerTurn playerTurn)
    {
        int sign = playerTurn == PlayerTurn.Player1 ? 1 : -1;
        float xAnchor = playerTurn == PlayerTurn.Player1 ? 0 : 1;
        inputsMenu.gameObject.SetActive(true);
        inputsMenu.pivot = new Vector2(0.5f, 0);
        inputsMenu.anchorMax = new Vector2(xAnchor, 0);
        inputsMenu.anchorMin = new Vector2(xAnchor, 0);
        inputsMenu.anchoredPosition = new Vector2((inputsMenu.sizeDelta.x / 2) * sign, 0);
        //if (playerTurn == PlayerTurn.Player1)
        //{
        //    player2Menu.SetActive(false);
        //    inputsMenu.SetActive(true);
        //}
        //else
        //{
        //    inputsMenu.SetActive(false);
        //    player2Menu.SetActive(true);
        //}

    }

    public void HideInputsMenu()
    {
        inputfieldAngle.text = string.Empty;
        inputfieldForce.text = string.Empty;
        inputsMenu.gameObject.SetActive(false);
    }


}
