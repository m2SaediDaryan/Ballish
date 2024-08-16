using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float numberCollected;
    public float moveSpeed = 5f; // Speed at which the object will move
    public ScoreManager scoreManager;
    [SerializeField] public GameObject trigger;

    [Header("Colors")]
    string White = "#FDFFFC";
    string Red = "#FF0022";
    string AbiLow = "#41EAD4";
    string AbiHigh = "#2E86AB";
    public string[] colorPalletePlayer;

    void Start()
    {
        colorPalletePlayer = new string[] { White, Red, AbiLow, AbiHigh };
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
        if (other.tag == "Dot")
        {
            Destroy(other.gameObject);
            Debug.Log("Triggerd");
        }

    }

    void ChangeColor()
    {
        int randomIndex = Random.Range(0, colorPalletePlayer.Length);
        string selectHexColor = colorPalletePlayer[randomIndex];
        Debug.Log("in change color");

        Color newColor;
        if (ColorUtility.TryParseHtmlString(selectHexColor, out newColor))
        {
            // Apply the color to the player's material
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = newColor;
                Debug.Log("color Changed");
            }
        }
        else
        {
            Debug.LogError("Invalid Hex color code: " + selectHexColor);
        }
    }

    IEnumerator ChangeColorCo()
    {
        //if()
        yield return null;
    }
}
