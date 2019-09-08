using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Game instance;

    public static Game Instance
    {
        get
        {
            if(instance==null)
            {
                GameObject singletonGO = new GameObject();
                instance = singletonGO.AddComponent<Game>();
                singletonGO.name = typeof(Game).ToString() + " (Singleton)";
                DontDestroyOnLoad(singletonGO);
            }

            return instance;
        }        
    }

    public event Action<int> Win;
    public event Action<int> Draw;
    public event Action<int> Loss;

    public Board board;

    private BoardGrid boardGrid;

    private int currentDifficulty;

    private int playerSign;
    private int aiSign;
    private int makingMoveSign;

    private int wins;
    private int draws;
    private int losses;

    private int winnerSign;

    public void StartNewGame(int difficulty)
    {
        boardGrid = new BoardGrid();

        currentDifficulty = difficulty;
        playerSign = 1;
        aiSign = 2;
        makingMoveSign = 1;

        wins = 0;
        draws = 0;
        losses = 0;

        SceneManager.LoadScene("GameScene");
        SceneManager.sceneLoaded += GameReady;        
    }

    public void NextRound()
    {
        board.ClearBoard();
        boardGrid.ClearBoardGrid();
        makingMoveSign = 1;

        if(playerSign==winnerSign)
        {
            playerSign = 1;
            aiSign = 2;
        }
        else if(aiSign==winnerSign)
        {
            aiSign = 1;
            playerSign = 2;
        }
        else
        {
            int t = playerSign;
            playerSign = aiSign;
            aiSign = t;
        }

        winnerSign = 0;

        NextMove();
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void GameReady(Scene gameScene, LoadSceneMode loadSceneMode)
    {
        NextMove();
    }

    public void TryPlacePlayerSign(Coordinates coordinates)
    {
        if(playerSign==makingMoveSign)
        {
            if (boardGrid.cells[coordinates.x, coordinates.y] == 0)
            {
                MakePlayerMove(coordinates);
            }
        }
    }

    private void NextMove()
    {
        if(makingMoveSign==aiSign)
        {
            MakeAiMove();
        }
    }

    private void MakeAiMove()
    {
        Coordinates aiMove = AI.GetBestMove(currentDifficulty, boardGrid, aiSign);

        boardGrid.cells[aiMove.x, aiMove.y] = aiSign;

        board.PlaceSign(aiSign, aiMove.x, aiMove.y);

        makingMoveSign = playerSign;

        CheckWin();
    }

    private void MakePlayerMove(Coordinates coordinates)
    {
        boardGrid.cells[coordinates.x, coordinates.y] = playerSign;

        board.PlaceSign(playerSign, coordinates.x, coordinates.y);

        makingMoveSign = aiSign;

        CheckWin();
    }

    private void CheckWin()
    {
        int result = boardGrid.CheckWinner();

        switch(result)
        {
            case -1:
                NextMove();
                break;
            case 0:
                draws++;
                Draw(draws);
                break;
            case 1:
            case 2:
                winnerSign = result;
                if (playerSign == result)
                {
                    wins++;
                    Win(wins);
                }
                else
                {
                    losses++;
                    Loss(losses);
                }
                break;
        }
    }
}
