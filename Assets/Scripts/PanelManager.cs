using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private GameManager gameManager;
    private Player player;
    public GameObject startPanel;
    public GameObject replayPanel;
    public GameObject gamePanel;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartButton()
    {
        gameManager.UpdateGameState(GameState.Play);
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void ReplayButton()
    {
        if (gameManager.state == GameState.Lose && player.nowCanReplay == 2)
        {
            gamePanel.SetActive(true);
            replayPanel.SetActive(false);
            gameManager.currentScore = 0;
            gameManager.UpdateGameState(GameState.Play);
            player.nowCanReplay=0;
        }
    }
}
