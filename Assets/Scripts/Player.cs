using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the object will move
    //private Dots dots;
    [SerializeField] private Color newColor;
    [SerializeField] private GameObject trigger; // Placeholder for future use if needed

    [Header("Colors")]
    private string[] colorPalettePlayer = { "#FDFFFC", "#FF0022", "#41EAD4", "#2E86AB" };
    [SerializeField] private Color colorOfPlayer;

    void Start()
    {
        //dots = FindObjectOfType<Dots>();
        StartCoroutine(ChangeColorCo());
        //CheckColor();
    }

    void Update()
    {
        // Check for user input
        float moveDirection = Input.GetAxis("Horizontal");

        // Calculate new position
        Vector3 newPosition = transform.position + new Vector3(moveDirection, 0, 0) * moveSpeed * Time.deltaTime;

        // Apply the new position to the object
        transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //string tag = other.tag;
        /*if (other.CompareTag("Dot"))
        {
            Destroy(other.gameObject);
            // Debug.Log("Triggered");
        }*/

        if (ColorToHex(colorOfPlayer) == other.tag)
        {

            Destroy(other.gameObject);
            //Debug.Log("Same object you find");
        }
        else
        {
            Debug.Log("lose");
        }
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
}
