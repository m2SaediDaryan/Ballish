using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Play,
    Pause,
    Lose
}
public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    public Text currentScoreText;
    public int highScore;
    public Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        //currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = currentScore.ToString();
    }
}
