using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour {

    public static GameUIManager Instance { get; private set; }

    [SerializeField]
    Text playerTurnText;
    [SerializeField]
    GameObject player1Menu;
    [SerializeField]
    InputField player1InputAngle;
    [SerializeField]
    InputField player1InputForce;
    [SerializeField]
    GameObject player2Menu;
    [SerializeField]
    InputField player2InputAngle;
    [SerializeField]
    InputField player2InputForce;

    public string Player1Angle { get { return player1InputAngle.text; } }
    public string Player1Force { get { return player1InputForce.text; } }

    public string Player2Angle { get { return player2InputAngle.text; } }
    public string Player2Force { get { return player2InputForce.text; } }

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
        ShowPlayerMenu(playerTurn);
    }

    private void ShowPlayerMenu(PlayerTurn playerTurn)
    {
        if(playerTurn == PlayerTurn.Player1)
        {
            player2Menu.SetActive(false);
            player1Menu.SetActive(true);
        }
        else
        {
            player1Menu.SetActive(false);
            player2Menu.SetActive(true);
        }

    }

    public void HidePlayerMenu(PlayerTurn playerTurn)
    {
        if(playerTurn == PlayerTurn.Player1)
        {
            player1InputAngle.text = string.Empty;
            player1InputForce.text = string.Empty;
            player1Menu.SetActive(false);
        }
        else
        {
            player2InputAngle.text = string.Empty;
            player2InputForce.text = string.Empty;
            player2Menu.SetActive(false);
        }
    }


}
