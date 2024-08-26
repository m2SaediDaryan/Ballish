using System;
using System.Diagnostics;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public enum GameState
{
    Start,
    Play,
    Pause,
    Lose
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int currentScore = 0;
    private PanelManager panelManager;
    public int highScore;
    [SerializeField]
    public Text currentScoreText_GP;
    public Text currentScoreText_RP;
    public Text highScoreText_SP;
    public Text highScoreText_RP;
    //public GameState gameState;
    public GameState state;//video
    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
        panelManager = FindObjectOfType<PanelManager>();
        if (panelManager == null)
        {
            //Debug.Log("PanelManager is not assigned or attached!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //panelManager = FindObjectOfType<PanelManager>();
        UpdateGameState(GameState.Start);
        //currentScore = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText_SP.text = "High Score" + highScore.ToString();
        highScoreText_RP.text = "High Score" + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        currentScoreText_GP.text = currentScore.ToString();
        currentScoreText_RP.text = currentScore.ToString();
        if (currentScore > highScore)
        {
            highScore = currentScore;
            highScoreText_SP.text = highScore.ToString();
            highScoreText_RP.text = highScore.ToString();
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.Start:
                currentScore = 0;
                panelManager.startPanel.SetActive(true);
                state = GameState.Play;
                break;
            case GameState.Play:
                break;
            case GameState.Pause:
                break;
            case GameState.Lose:
                panelManager.gamePanel.SetActive(false);
                panelManager.replayPanel.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }
}
