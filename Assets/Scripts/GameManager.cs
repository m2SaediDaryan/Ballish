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
    private PanelManager panelManager;
    //public PlayerMovment playerMovment;
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
            Debug.Log("PanelManager is not assigned or attached!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //playerMovment = FindObjectOfType<PlayerMovment>();
        //panelManager = FindObjectOfType<PanelManager>();
        UpdateGameState(GameState.Start);
        //currentScore = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText_SP.text = "High Score :" + highScore.ToString();
        highScoreText_RP.text = "High Score :" + highScore.ToString();
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
                //state = GameState.Play;
                break;
            case GameState.Play:
                //playerMovment.isPressed = false;
                break;
            case GameState.Pause:
                break;
            case GameState.Lose:
                panelManager.gamePanel.SetActive(false);
                panelManager.replayPanel.SetActive(true);
                //playerMovment.isPressed = false;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }
}


/*NullReferenceException: Object reference not set to an instance of an object
GameManager.UpdateGameState (GameState newState) (at Assets/Scripts/GameManager.cs:75)
PanelManager.ReplayButton () (at Assets/Scripts/PanelManager.cs:37)
UnityEngine.Events.InvokableCall.Invoke () (at <874d3e57e8754567b5753a47cbb1ae54>:0)
UnityEngine.Events.UnityEvent.Invoke () (at <874d3e57e8754567b5753a47cbb1ae54>:0)
UnityEngine.UI.Button.Press () (at Library/PackageCache/com.unity.ugui@1.0.0/Runtime/UI/Core/Button.cs:70)
UnityEngine.UI.Button.OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData) (at Library/PackageCache/com.unity.ugui@1.0.0/Runtime/UI/Core/Button.cs:114)
UnityEngine.EventSystems.ExecuteEvents.Execute (UnityEngine.EventSystems.IPointerClickHandler handler, UnityEngine.EventSystems.BaseEventData eventData) (at Library/PackageCache/com.unity.ugui@1.0.0/Runtime/EventSystem/ExecuteEvents.cs:57)
UnityEngine.EventSystems.ExecuteEvents.Execute[T] (UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents+EventFunction`1[T1] functor) (at Library/PackageCache/com.unity.ugui@1.0.0/Runtime/EventSystem/ExecuteEvents.cs:272)
UnityEngine.EventSystems.EventSystem:Update() (at Library/PackageCache/com.unity.ugui@1.0.0/Runtime/EventSystem/EventSystem.cs:517)*/