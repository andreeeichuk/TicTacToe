using System.Collections;
using System.Collections.Generic;
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

    public Board board;

    private BoardGrid boardGrid;

    private Difficulty currentDifficulty;

    private int playerSign;
    private int aiSign;
    private int makingMoveSign;

    public void StartNewGame(Difficulty difficulty)
    {
        boardGrid = new BoardGrid();

        currentDifficulty = difficulty;
        playerSign = 1;
        aiSign = 2;
        makingMoveSign = 1;

        SceneManager.LoadScene("GameScene");
        SceneManager.sceneLoaded += GameReady;        
    }

    private void GameReady(Scene gameScene, LoadSceneMode loadSceneMode)
    {
        NextMove();
    }

    public void TryPlacePlayerSign(Coordinates coordinates)
    {
        if(playerSign==makingMoveSign)
        {
            MakePlayerMove(coordinates);           
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
        }
    }
}
