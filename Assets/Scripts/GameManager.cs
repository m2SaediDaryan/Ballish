using System;
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
    public bool allowToChangeColor;
    private PanelManager panelManager;
    private Player player;
    private PlayerMovement playerMovement;
    public int highScore;
    [SerializeField]
    public Text currentScoreText_GP;
    public Text currentScoreText_RP;
    public Text highScoreText_SP;
    public Text highScoreText_RP;
    public GameState state;//video
    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        /*int maxRefreshRate = Screen.currentResolution.refreshRate;
        Application.targetFrameRate = maxRefreshRate;*/
        Instance = this;
        player = FindObjectOfType<Player>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        panelManager = FindObjectOfType<PanelManager>();
    }
    void Start()
    {
        //int maxRefreshRate = Screen.currentResolution.refreshRate;
        Application.targetFrameRate = 120;
        UpdateGameState(GameState.Start);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText_SP.text = "High Score : " + highScore.ToString();
        highScoreText_RP.text = "High Score : " + highScore.ToString();
    }

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
                allowToChangeColor = false;
                break;
            case GameState.Play:
                allowToChangeColor = true;
                player.ChangeColorToWhite();
                break;
            case GameState.Pause:
                break;
            case GameState.Lose:
                Handheld.Vibrate();
                playerMovement.right = false;
                playerMovement.left = false;
                allowToChangeColor = false;
                panelManager.gamePanel.SetActive(false);
                panelManager.replayPanel.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }
}