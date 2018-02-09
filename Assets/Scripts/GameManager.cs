using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerTurn
{
    Player1 = 1,
    Player2 = 2
}

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    CannonController player1;
    CannonController player2;

    PlayerTurn actualPlayerTurn;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            player1 = GameObject.FindGameObjectWithTag("Player 1").GetComponent<CannonController>();
            //player2 = GameObject.FindGameObjectWithTag("Player 2").GetComponent<CannonController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        actualPlayerTurn = (PlayerTurn)Random.Range(1, 3);
        actualPlayerTurn = PlayerTurn.Player1;
        GameUIManager.Instance.SetPlayerTurnText(actualPlayerTurn);
    }

    public void OnShootButtonClick()
    {
        int angle;
        int force;
        string strAngle = actualPlayerTurn == PlayerTurn.Player1 ? GameUIManager.Instance.Player1Angle : GameUIManager.Instance.Player2Angle;
        string strForce = actualPlayerTurn == PlayerTurn.Player1 ? GameUIManager.Instance.Player1Force : GameUIManager.Instance.Player2Force;

        if (int.TryParse(strAngle, out angle) && int.TryParse(strForce, out force))
        {
            if (actualPlayerTurn == PlayerTurn.Player1)
            {
                player1.Shoot(angle, force);
            }
            else
            {
                player2.Shoot(angle, force);
            }
        }
        else
        {
            Debug.LogError("Los campos están vacios");
        }
    }

    public bool IsTurnOf(CannonController cannon)
    {
        if (player1 == cannon && actualPlayerTurn == PlayerTurn.Player1)
        {
            return true;
        }
        else if(player2 == cannon && actualPlayerTurn == PlayerTurn.Player2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
