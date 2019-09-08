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

    private Difficulty currentDifficulty;

    private int playerIndex;
    private int aiIndex;
    private int makingMoveIndex;

    public void StartNewGame(Difficulty difficulty)
    {
        currentDifficulty = difficulty;
        playerIndex = 1;
        aiIndex = 2;
        makingMoveIndex = 1;

        SceneManager.LoadScene("GameScene");
    }

    public void TryPlacePlayerSign(Coordinates coordinates)
    {
        if(playerIndex==makingMoveIndex)
        {
            MakePlayerMove(coordinates);           
        }
    }

    private void MakePlayerMove(Coordinates coordinates)
    {
        board.PlaceSign(playerIndex, coordinates.x, coordinates.y);

        makingMoveIndex = 2;

        CheckWin();
    }

    private void CheckWin()
    {

    }
}
