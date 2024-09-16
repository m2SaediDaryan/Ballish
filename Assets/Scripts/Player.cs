using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator loseAnim;
    private GameManager gameManager;
    public int nowCanReplay;
    [SerializeField] private Color newColor;

    [Header("Colors")]
    private string[] colorPalettePlayer = { "#FDFFFC", "#FF0022", "#41EAD4", "#2E86AB" };
    [SerializeField] private Color colorOfPlayer;

    //mainscale(0.24f, 0.423f, 0.423f)
    public Vector2 mainScale = new Vector3(0.24f, 0.423f);
    public Vector2[] targetScale = new Vector2[]
    {
        new Vector2(0.34f, 0.6f),
        new Vector2(0.17f, 0.59f),
        new Vector2(0.24f, 0.95f),
        new Vector2(0.32f, 0.423f)
    };

    //private int lastScaleIndex;
    //public Vector3 targetScale2 = new Vector3(0.17f, 0.59f, 0.423f);
    //public Vector3 targetScale3 = new Vector3(0.24f, 0.95f, 0.423f);
    //public Vector3 targetScale4 = new Vector3(0.32f, 0.423f, 0.423f); // The scale you want to reach

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        loseAnim = GetComponent<Animator>();
        StartCoroutine(ChangeColorCo());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //string tag = other.tag;
        /*if (other.CompareTag("Dot"))
        {
            Destroy(other.gameObject);
            // Debug.Log("Triggered");
        }*/

        if (ColorToHex(colorOfPlayer) == other.tag && gameManager.state == GameState.Play)
        {
            Destroy(other.gameObject);
            gameManager.currentScore++;
            //Debug.Log(gameManager.currentScoreText_GP);
        }
        else if (ColorToHex(colorOfPlayer) != other.tag && gameManager.state == GameState.Play)
        {
            gameManager.UpdateGameState(GameState.Lose);
            ChangeColorToWhite();
            loseAnim.SetBool("lose", true);
            //Debug.Log("lose");
        }
    }

    void ChangeColor()
    {
        if (gameManager.allowToChangeColor == true)
        {
            if (gameManager.currentScore < 1)
            {
                string selectedHexColor = "#FDFFFC";

                if (ColorUtility.TryParseHtmlString(selectedHexColor, out newColor))
                {
                    Renderer renderer = GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = newColor;
                        colorOfPlayer = newColor;
                    }
                }
                else
                {
                    Debug.LogError("Invalid Hex color code: " + selectedHexColor);
                }
            }
            else
            {
                int randomIndex = Random.Range(0, colorPalettePlayer.Length);
                string selectedHexColor = colorPalettePlayer[randomIndex];

                if (ColorUtility.TryParseHtmlString(selectedHexColor, out newColor))
                {
                    Renderer renderer = GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = newColor;
                        colorOfPlayer = newColor;
                    }
                }
                else
                {
                    Debug.LogError("Invalid Hex color code: " + selectedHexColor);
                }
            }
        }
    }

    public void ChangeColorToWhite()
    {
        string selectedHexColor = "#FDFFFC";

        if (ColorUtility.TryParseHtmlString(selectedHexColor, out newColor))
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = newColor;
                colorOfPlayer = newColor;
            }
        }
        else
        {
            Debug.LogError("Invalid Hex color code: " + selectedHexColor);
        }
    }

    IEnumerator ChangeColorCo()
    {
        while (true)
        {
            ChangeColor();
            yield return new WaitForSeconds(12f);
        }
    }

    //this func added because to get hex name of color
    public string ColorToHex(Color color)
    {
        return $"#{ColorUtility.ToHtmlStringRGB(color)}";
    }

    /*IEnumerator ChangeScale()
    {
        //transform.localScale = newScale;
        yield return new WaitForSeconds(5f);
    }*/

    /*IEnumerator ScaleCoroutine(Vector3 targetScale, float duration)
    {
        //if (scoreManager.currentScore > 5)

        Vector3 initialScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        transform.localScale = targetScale; // Ensure the target scale is set at the end


    }*/

    /*IEnumerator ScaleBackToMain(Vector3 mainScale, float duration)
    {
        Vector3 initialScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, mainScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        transform.localScale = mainScale;
        //lastScaleIndex = -1;
    }*/

    public void LoseAnimEnd(int intAnim)
    {
        if (intAnim == 1)
        {
            loseAnim.SetBool("lose", false);
        }
    }

    public void NewPlayerAnimEnd(int intAnim)
    {
        if (intAnim == 1)
        {
            nowCanReplay = 2;
        }
    }
}
