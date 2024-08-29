using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject startPanel;
    public GameObject replayPanel;
    public GameObject gamePanel;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartButton()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameManager.state = GameState.Play;
    }

    public void ReplayButton()
    {
        if (gameManager.state == GameState.Lose)
        {
            gamePanel.SetActive(true);
            replayPanel.SetActive(false);
            gameManager.currentScore = 0;
            gameManager.UpdateGameState(GameState.Play);
        }
    }
}
