using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GameObject gameOverPopup;
    public TextMeshProUGUI popupText;

    public TextMeshProUGUI wins;
    public TextMeshProUGUI draws;
    public TextMeshProUGUI losses;

    private void Awake()
    {
        Game.Instance.Win += UpdateWins;
        Game.Instance.Draw += UpdateDraws;
        Game.Instance.Loss += UpdateLosses;
        gameOverPopup.SetActive(false);
    }

    public void BackToMenu()
    {
        Game.Instance.ExitGame();
    }

    public void NextRound()
    {
        gameOverPopup.SetActive(false);
        Game.Instance.NextRound();
    }

    private void UpdateWins(int winCount)
    {
        wins.text = "Wins: " + winCount;
        ShowPopup("You Won!");
    }

    private void UpdateDraws(int drawCount)
    {
        draws.text = "Draws: " + drawCount;
        ShowPopup("Draw!");
    }

    private void UpdateLosses(int lossesCount)
    {
        losses.text = "Losses: " + lossesCount;
        ShowPopup("You Lost!");
    }    

    private void ShowPopup(string message)
    {
        gameOverPopup.SetActive(true);
        popupText.text = message;
    }

    private void OnDestroy()
    {
        if (Game.Instance != null)
        {
            Game.Instance.Win -= UpdateWins;
            Game.Instance.Draw -= UpdateDraws;
            Game.Instance.Loss -= UpdateLosses;
        }
    }
}
