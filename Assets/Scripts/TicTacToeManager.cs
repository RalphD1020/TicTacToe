using System;
using UnityEditor;
using UnityEngine;

public class TicTacToeManager : MonoBehaviour
{
    private readonly TileState[,] _board = new TileState[3, 3];
    private int _numMoves;
    private bool _playerHasWon;
    public static TicTacToeManager Instance;
    public TileState currentPlayer;

    // Start is called before the first frame update
    private void Start()
    {
        InitializeTicTacToeInstance();
        InitializeGameState();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void PerformMove(int tileIndex)
    {
        UpdateBoard(GetRowColumnFromIndex(tileIndex));
        if (VictoryAchieved())
        {
            _playerHasWon = true;
            EndGame(currentPlayer);
        }
        if (!_playerHasWon) {
            _numMoves++;
            if (_numMoves >= 9)
            {
                EndGame(TileState.Empty);
            }
            SwitchPlayer();
            NotifyPlayerTurn();
        }

    }

    private bool VictoryAchieved()
    {
        return (RowVictoryAchieved() || ColVictoryAchieved() || DiagVictoryAchieved());
    }
    
    private static bool ThreeTilesMatch(TileState tileState1, TileState tileState2, TileState tileState3)
    {
        return (tileState1 != TileState.Empty && tileState1 == tileState2 && tileState1 == tileState3);
    }

    private bool RowVictoryAchieved()
    {
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            if (ThreeTilesMatch(_board[i, 0], _board[i, 1], _board[i, 2]))
            {
                return true;
            }
        }

        return false;
    }

    private bool ColVictoryAchieved()
    {
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            if (ThreeTilesMatch(_board[0, i], _board[1, i], _board[2, i]))
            {
                return true;
            }
        }
        return false;
    }

    private bool DiagVictoryAchieved()
    {
        return ThreeTilesMatch(_board[0, 0], _board[1, 1], _board[2, 2]) ||
               ThreeTilesMatch(_board[0, 2], _board[1, 1], _board[2, 0]);
    }

    private void EndGame(TileState winner)
    {
        if (winner == TileState.Empty && !_playerHasWon)
        {
            Debug.Log("DRAW");
        }
        else
        {
            Debug.Log("Winner: " + winner);
        }
        EditorApplication.isPlaying = false;
    }

    private void UpdateBoard((int row, int col) coordinates)
    {
        _board[coordinates.row, coordinates.col] = currentPlayer;
    }

    private void SwitchPlayer()
    {
        if (currentPlayer == TileState.X)
            currentPlayer = TileState.O;
        else
            currentPlayer = TileState.X;
    }
    
    private void NotifyPlayerTurn()
    {
        Debug.Log("Current player is: " + currentPlayer);
    }
    
    private static (int, int) GetRowColumnFromIndex(int tileIndex)
    {
        if (tileIndex is < 0 or > 8)
        {
            throw new ArgumentOutOfRangeException(nameof(tileIndex), "Index should be between 0 and 8");
        }

        int row = tileIndex / 3;
        int col = tileIndex % 3;

        return (row, col);
    }


    private void InitializeTicTacToeInstance()
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

    private void InitializeGameState()
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
        NotifyPlayerTurn();
    }
}