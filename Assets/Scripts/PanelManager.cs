using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PanelManager : MonoBehaviour
{
    private GameManager gameManager;
    private Player player;
    public GameObject startPanel;
    public GameObject replayPanel;
    public GameObject gamePanel;
    public GameObject settingPanel;
    public Animator replayPanelAnim;
    public int nowCanTurnOffRP;
    public bool settingFromStart;
    public bool gameStarted;
    [SerializeField] public bool settingTouched = false;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnStartButtonPressed()
    {
        gameStarted = true;
        gameManager.UpdateGameState(GameState.Play);
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void ReplayButton()
    {
        //replayPanelAnim.SetBool("RPIdleEnd", true);
        if (gameManager.state == GameState.Lose && player.nowCanReplay == 2)
        {
            gamePanel.SetActive(true);
            replayPanel.SetActive(false);
            gameManager.currentScore = 0;
            gameManager.UpdateGameState(GameState.Play);
            player.nowCanReplay = 0;
            nowCanTurnOffRP = 0;
        }
    }
    public void SettingButton()
    {
        if (gameManager.state == GameState.Lose)
        {
            replayPanelAnim.SetBool("IdleToSetting", true);
            replayPanel.SetActive(false);
            settingPanel.SetActive(true);
            PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
            ppVolume.enabled = !ppVolume.enabled;
            settingTouched = true;
        }
        else if (gameManager.state == GameState.Start)
        {
            // replayPanelAnim.SetBool("IdleToSetting", true);
            startPanel.SetActive(false);
            settingPanel.SetActive(true);
            PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
            ppVolume.enabled = !ppVolume.enabled;
            settingTouched = true;
            settingFromStart = true;
        }

    }

    public void ExitButton()
    {
        if (settingFromStart == false)
        {
            replayPanel.SetActive(true);
            settingPanel.SetActive(false);
            PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
            ppVolume.enabled = !ppVolume.enabled;
        }
        else if (settingFromStart == true)
        {
            startPanel.SetActive(true);
            settingPanel.SetActive(false);
            PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
            ppVolume.enabled = !ppVolume.enabled;
        }

    }

    /*public void ReplayPanelIdleEnd(int intAnim)
    {
        if (intAnim == 1)
        {
            nowCanTurnOffRP = 2;
        }
    }*/
}
