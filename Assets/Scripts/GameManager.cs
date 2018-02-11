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

    [SerializeField]
    float limitTimeForTurn = 5;
    [SerializeField]
    bool useDynamicCamera = true;

    CannonController player1;
    CannonController player2;
    PlayerTurn actualPlayerTurn;

    public CameraController Camera { get; private set; }
    public float LimitTimeForTurn { get { return limitTimeForTurn; } }
    public bool UseDynamicCamera { get { return useDynamicCamera; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            player1 = GameObject.FindGameObjectWithTag("Player 1").GetComponent<CannonController>();
            player2 = GameObject.FindGameObjectWithTag("Player 2").GetComponent<CannonController>();
            Camera = FindObjectOfType<CameraController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetTurnTo((PlayerTurn)Random.Range(1, 3));
    }

    private void SetTurnTo(PlayerTurn playerTurn)
    {
        actualPlayerTurn = playerTurn;
        GameUIManager.Instance.SetPlayerTurnText(playerTurn);

        if (UseDynamicCamera) Camera.MoveTowardsPlayer(actualPlayerTurn == PlayerTurn.Player1 ? player1.transform : player2.transform);
    }

    public void OnShootButtonClick()
    {
        int angle;
        int force;
        string strAngle = GameUIManager.Instance.AngleInput;
        string strForce = GameUIManager.Instance.ForceInput;

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
            GameUIManager.Instance.HideInputsMenu();
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

    public void EndOfThisTurn()
    {
        if(actualPlayerTurn == PlayerTurn.Player1)
        {
            player1.HasShooted = false;
            SetTurnTo(PlayerTurn.Player2);
        }
        else
        {
            player2.HasShooted = false;
            SetTurnTo(PlayerTurn.Player1);
        }
    }
}
