using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private ScoreManager scoreManager;
    public GameObject startPanel;
    public GameObject replayPanel;
    public GameObject gamePanel;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        startPanel.SetActive(true);
        //replayPanel.SetActive(false);
    }

    public void StartButton()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void Lose()
    {
        gamePanel.SetActive(false);
        replayPanel.SetActive(true);
    }

    public void ReplayButton()
    {
        if (scoreManager.gameState == GameState.Lose)
        {
            gamePanel.SetActive(true);
            replayPanel.SetActive(false);
            scoreManager.currentScore = 0;
            scoreManager.gameState = GameState.Play;
        }
    }
}
