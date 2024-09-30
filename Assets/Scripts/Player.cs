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
    public SpawnMaker spawnMaker;
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
    public GameObject GaurdObject;
    public float maxScale;
    public float scaleUpRate;
    public float scaleDownRate;
    public float scaleUpTimer;
    public float scaleUpStartTime = 6f;
    public bool scaleUpBool = false;
    public bool scaleDownBool = false;

    void Start()
    {
        spawnMaker = FindObjectOfType<SpawnMaker>();
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
                scaleUpBool = true;
                //StartCoroutine(ScaleUp());

            }
            if (sameTagCollected > 2 && gameManager.currentScore > 4)
            {
                CollectSameTag();
            }
            if (gameManager.currentScore > 4 && gameManager.currentScore % 5 == 0)
            {
                //spawnMaker.SpawnPrefabsGravity();
                //spawnMaker.SpawnPrefabsHeart();
                spawnMaker.SpawnPrefabsGuard();
            }
        }
        else if ("Heart" == other.tag && gameManager.state == GameState.Play)
        {
            gameManager.heartScore++;
            Destroy(other.gameObject);
        }
        else if("Guard" == other.tag && gameManager.state == GameState.Play)
        {
            GaurdObject.SetActive(true);
        }
        else if (ColorToHex(colorOfPlayer) != other.tag && gameManager.state == GameState.Play && gameManager.heartScore > 0)
        {
            gameManager.heartScore--;
            Destroy(other.gameObject);
        }
        else if (ColorToHex(colorOfPlayer) != other.tag && gameManager.state == GameState.Play && gameManager.heartScore == 0 && "Walls" != other.tag)
        {
            animator.enabled = true;
            source.PlayOneShot(lose);
            gameManager.UpdateGameState(GameState.Lose);
            ChangeColorToWhite();
            loseAnim.SetBool("lose", true);
            sameTagCollected = 0;
        }
    }

    void Update()
    {
        if (scaleUpBool && transform.localScale.x <= 2f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x + scaleUpRate, 1, 1), 1.5f * Time.deltaTime);
        }

        if (scaleDownBool && transform.localScale.x > 1f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, mainScale, 1.5f * Time.deltaTime);
        }

        if (!scaleUpBool && !scaleDownBool)
        {
            scaleUpTimer += Time.deltaTime;
            if (scaleUpTimer >= scaleUpStartTime)
            {
                scaleUpBool = true;
            }
        }
    }

    public void CollectSameTag()
    {
        if (scaleUpBool)
        {
            scaleUpBool = false;
            scaleDownBool = true;
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
            scaleUpBool = false;
            scaleDownBool = false;
        }
    }

    //this func added because to get hex name of color
    public string ColorToHex(Color color)
    {
        return $"#{ColorUtility.ToHtmlStringRGB(color)}";
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
