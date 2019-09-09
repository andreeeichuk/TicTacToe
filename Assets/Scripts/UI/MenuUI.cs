using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject menuLayout;
    public GameObject difficultyLayout;

    private void Awake()
    {
        menuLayout.SetActive(true);
        difficultyLayout.SetActive(false);
    }

    public void NewGame()
    {
        menuLayout.SetActive(false);
        difficultyLayout.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        menuLayout.SetActive(true);
        difficultyLayout.SetActive(false);
    }

    public void ChooseDifficulty(int difficulty)
    {
        Game.Instance.StartNewGame(difficulty);
    }
}
