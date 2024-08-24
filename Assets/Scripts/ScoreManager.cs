using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Start,
    Play,
    Pause,
    Lose
}
public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    public int highScore;
    [SerializeField]
    public Text currentScoreText_GP;
    public Text currentScoreText_RP;
    public Text highScoreText_SP;
    public Text highScoreText_RP;
    public GameState gameState;


    // Start is called before the first frame update
    void Start()
    {
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
}
