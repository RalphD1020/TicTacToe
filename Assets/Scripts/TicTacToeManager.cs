using System;
using UnityEditor;
using UnityEngine;

public class TicTacToeManager : MonoBehaviour
{
    private TileState[,] _board = new TileState[3, 3];
    private int _numMoves = 0;
    public static TicTacToeManager Instance;
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

    public void PerformMove(int tileIndex)
    {
        updateBoard(GetRowColumnFromIndex(tileIndex));
        // todo: check victory
        if (victoryAchieved())
        {
            endGame(currentPlayer);
        }
        _numMoves++;
        if (_numMoves >= 9)
        {
            endGame(TileState.Empty);
        }
        switchPlayer();

    }

    private bool victoryAchieved()
    {
        return (rowVictoryAchieved() || colVictoryAchieved() || diagVictoryAchieved());
    }
    
    private bool threeTilesMatch(TileState tileState1, TileState tileState2, TileState tileState3)
    {
        return (tileState1 != TileState.Empty && tileState1 == tileState2 && tileState1 == tileState3);
    }

    private bool rowVictoryAchieved()
    {
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            if (threeTilesMatch(_board[i, 0], _board[i, 1], _board[i, 2]))
            {
                return true;
            }
        }

        return false;
    }

    private bool colVictoryAchieved()
    {
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            if (threeTilesMatch(_board[0, i], _board[1, i], _board[2, i]))
            {
                return true;
            }
        }
        return false;
    }

    private bool diagVictoryAchieved()
    {
        return threeTilesMatch(_board[0, 0], _board[1, 1], _board[2, 2]) ||
               threeTilesMatch(_board[0, 2], _board[1, 1], _board[2, 0]);
    }

    private void endGame(TileState winner)
    {
        if (winner == TileState.Empty)
        {
            Debug.Log("DRAW");
        }
        else
        {
            Debug.Log("Winner: " + winner);
        }
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private void updateBoard((int row, int col) coordinates)
    {
        _board[coordinates.row, coordinates.col] = currentPlayer;
    }

    private void switchPlayer()
    {
        if (currentPlayer == TileState.X)
            currentPlayer = TileState.O;
        else
            currentPlayer = TileState.X;
    }
    
    public (int, int) GetRowColumnFromIndex(int tileIndex)
    {
        if (tileIndex < 0 || tileIndex > 8)
        {
            throw new ArgumentOutOfRangeException("tileIndex", "Index should be between 0 and 8");
        }

        int row = tileIndex / 3;
        int col = tileIndex % 3;

        return (row, col);
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
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _board[i, j] = TileState.Empty;
            }
        }
        
        // X goes first
        currentPlayer = TileState.X;
    }
}