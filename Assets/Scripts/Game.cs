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

    private Difficulty currentDifficulty;

    public void StartNewGame(Difficulty difficulty)
    {
        currentDifficulty = difficulty;

        SceneManager.LoadScene("GameScene");
    }
}
