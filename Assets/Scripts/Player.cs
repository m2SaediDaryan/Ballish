using System.Collections;
using UnityEngine;
using UnityEngine.Audio;


public class Player : MonoBehaviour
{
    public AudioClip lose;
    public AudioClip get;
    public AudioSource source;
    public Animator loseAnim;
    private GameManager gameManager;
    public int nowCanReplay;
    [SerializeField] private Color newColor;

    [Header("Colors")]
    private string[] colorPalettePlayer = { "#FDFFFC", "#FF0022", "#41EAD4", "#2E86AB" };
    [SerializeField] private Color colorOfPlayer;
    public AudioMixer audioMixer;
    public string loseSnapshot = "LoseSnapshot";
    public int sameTagCollected;
    public Animator animator;
    public Vector2 mainScale;
    public float maxScale;
    public float scaleUpRate;
    public float scaleDownRate;
    public float scaleUpTimer;
    public float scaleUpStartTime = 6f;


    void Start()
    {
        mainScale = transform.localScale;
        gameManager = FindObjectOfType<GameManager>();
        loseAnim = GetComponent<Animator>();
        StartCoroutine(ChangeColorCo());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (ColorToHex(colorOfPlayer) == other.tag && gameManager.state == GameState.Play)
        {
            Destroy(other.gameObject);
            gameManager.currentScore++;
            source.PlayOneShot(get);
            sameTagCollected++;
            Debug.Log(sameTagCollected);
            if (sameTagCollected > 2 && sameTagCollected < 4)
            {
                animator.enabled = false;
                StartCoroutine(ScaleUp());
            }
        }
        else if (ColorToHex(colorOfPlayer) != other.tag && gameManager.state == GameState.Play)
        {
            animator.enabled = true;
            source.PlayOneShot(lose);
            gameManager.UpdateGameState(GameState.Lose);
            ChangeColorToWhite();
            loseAnim.SetBool("lose", true);
            sameTagCollected = 0;
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
            sameTagCollected = 0;
        }
    }

    //this func added because to get hex name of color
    public string ColorToHex(Color color)
    {
        return $"#{ColorUtility.ToHtmlStringRGB(color)}";
    }

    /*IEnumerator ScaleUp()
    {
        float elapsedTime = 0;
        float transitionPercentage = 0;
        Vector3 startScale = transform.localScale;

        while (transitionPercentage < 1)
        {
            elapsedTime += Time.deltaTime;
            transitionPercentage = elapsedTime / 2f;
            transform.localScale = Vector3.Lerp(startScale, new Vector3(1.8f, 1, 1), transitionPercentage);
            yield return null;
        }
        yield return new WaitForSeconds(4f);
        StartCoroutine(ScaleDown());
    }
    IEnumerator ScaleDown()
    {
        float elapsedTime = 0;
        float transitionPercentage = 0;
        Vector3 startScale = transform.localScale;

        while (transitionPercentage < 1)
        {
            elapsedTime += Time.deltaTime;
            transitionPercentage = elapsedTime / 2f;
            transform.localScale = Vector3.Lerp(startScale, new Vector3(1, 1, 1), transitionPercentage);

            yield return null;
        }
    }*/

    IEnumerator ScaleUp()
    {
        while (true)
        {
            if (scaleUpTimer >= scaleUpStartTime && transform.localScale.x < maxScale)
            {
                float elapsedTime = 0;
                float transitionPercentage = 0;
                Vector3 startScale = transform.localScale;

                while (transitionPercentage < 1 && transform.localScale.x < maxScale)
                {
                    elapsedTime += Time.deltaTime;
                    transitionPercentage = elapsedTime / 2f;
                    transform.localScale = Vector3.Lerp(startScale, new Vector3(transform.localScale.x + scaleUpRate, 1, 1), transitionPercentage);
                    yield return new WaitForSeconds(1f);
                }
            }
            else
            {
                scaleUpTimer += Time.deltaTime;
            }
            //yield return null;
        }

    }



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
