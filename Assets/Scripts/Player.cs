using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the object will move
    private ScoreManager scoreManager;
    private PanelManager panelManager;
    private GameManager gameManager;
    public float moveDirection;
    [SerializeField] private Color newColor;
    [SerializeField] private GameObject trigger; // Placeholder for future use if needed

    [Header("Colors")]
    private string[] colorPalettePlayer = { "#FDFFFC", "#FF0022", "#41EAD4", "#2E86AB" };
    [SerializeField] private Color colorOfPlayer;

    //mainscale(0.24f, 0.423f, 0.423f)
    public Vector3 mainScale = new Vector3(0.24f, 0.423f, 0.423f);
    public Vector3[] targetScale = new Vector3[]
    {
        new Vector3(0.34f, 0.6f, 0.423f),
        new Vector3(0.17f, 0.59f, 0.423f),
        new Vector3(0.24f, 0.95f, 0.423f),
        new Vector3(0.32f, 0.423f, 0.423f)
    };

    //private int lastScaleIndex;
    //public Vector3 targetScale2 = new Vector3(0.17f, 0.59f, 0.423f);
    //public Vector3 targetScale3 = new Vector3(0.24f, 0.95f, 0.423f);
    //public Vector3 targetScale4 = new Vector3(0.32f, 0.423f, 0.423f); // The scale you want to reach
    public float duration = 2.0f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        panelManager = FindObjectOfType<PanelManager>();
        //dots = FindObjectOfType<Dots>();
        StartCoroutine(ChangeColorCo());
        //CheckColor();
        //StartCoroutine(ScaleCoroutine(targetScale, duration));
        //CheckStatemant();        
    }

    void Update()
    {
        // Check for user input
        moveDirection = Input.GetAxis("Horizontal");

        // Calculate new position
        Vector3 newPosition = transform.position + new Vector3(moveDirection, 0, 0) * moveSpeed * Time.deltaTime;

        // Apply the new position to the object
        transform.position = newPosition;

        moveDirection = 0f;

        /*if (gameManager.currentScore > 5 && gameManager.currentScore < 7)
        {
            if (targetScale.Length > 0)
            {
                int randomIndex = Random.Range(0, targetScale.Length);
                lastScaleIndex = randomIndex;
                StartCoroutine(ScaleCoroutine(targetScale[randomIndex], duration));
            }
            else
            {
                Debug.LogError("targetScale array is empty.");
            }
        }
        else if (gameManager.currentScore > 10 && gameManager.currentScore < 12)
        {
            StartCoroutine(ScaleBackToMain(mainScale, duration));
        }*/
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
            //Debug.Log(scoreManager.currentScoreText);
        }
        else
        {
            gameManager.UpdateGameState(GameState.Lose);
            //gameManager.gameState = GameState.Lose;
            //panelManager.Lose();
            //Debug.Log("lose");
        }
    }
    public void Buttons()
    {
        moveDirection = 1f;
    }
    void ChangeColor()
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

    IEnumerator ScaleCoroutine(Vector3 targetScale, float duration)
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


    }

    IEnumerator ScaleBackToMain(Vector3 mainScale, float duration)
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
    }

    public void CheckStatemant()
    {

    }

}
