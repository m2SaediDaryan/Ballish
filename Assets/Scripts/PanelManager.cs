using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private ScoreManager scoreManager;
    private GameManager gameManager;
    public GameObject startPanel;
    public GameObject replayPanel;
    public GameObject gamePanel;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        gameManager = FindObjectOfType<GameManager>();
        //startPanel.SetActive(true);
        //replayPanel.SetActive(false);
    }

    public void StartButton()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    /*public void Lose()
    {
        gamePanel.SetActive(false);
        replayPanel.SetActive(true);
    }*/

    public void ReplayButton()
    {
        if (gameManager.state == GameState.Lose)
        {
            gamePanel.SetActive(true);
            replayPanel.SetActive(false);
            gameManager.currentScore = 0;
            gameManager.state = GameState.Play;
        }
    }
}
