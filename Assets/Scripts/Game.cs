using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
