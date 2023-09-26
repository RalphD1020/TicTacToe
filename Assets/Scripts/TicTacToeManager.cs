using System;
using UnityEngine;

public class TicTacToeManager : MonoBehaviour
{
    public static TicTacToeManager Instance;

    // Define a public field to represent the current player.
    public TileState currentPlayer;

    // Start is called before the first frame update
    private void Start()
    {
        initializeTicTacToeInstance();
        initializeGameState();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void performMove()
    {
        // todo: perform move
        // todo: check victory
        switchPlayer();
    }

    private void switchPlayer()
    {
        if (currentPlayer == TileState.X)
            currentPlayer = TileState.O;
        else
            currentPlayer = TileState.X;
    }

    private void initializeTicTacToeInstance()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void initializeGameState()
    {
        // X goes first
        currentPlayer = TileState.X;
    }
}