using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private GameManager gameManager;
    //private PlayerMovment playerMovment;
    public GameObject startPanel;
    public GameObject replayPanel;
    public GameObject gamePanel;

    void Awake()
    {
        //playerMovment= FindObjectOfType<PlayerMovment>();
        gameManager = FindObjectOfType<GameManager>();
        //startPanel.SetActive(true);
        //replayPanel.SetActive(false);
    }

    public void StartButton()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameManager.state = GameState.Play;
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
            //playerMovment.isPressed=false;
            gameManager.UpdateGameState(GameState.Play);
        }
    }
}
