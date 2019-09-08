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

    }

    private void MakePlayerMove(Coordinates coordinates)
    {
        board.PlaceSign(playerSign, coordinates.x, coordinates.y);

        makingMoveSign = 2;

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
