using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovment : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public bool isPressed = false;
    public GameObject Player;
    public GameManager gameManager;
    public float Force;

    void Start()
    {
        //gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        /*if (gameManager.state == GameState.Lose)
        {
            isPressed = false;
        }*/
        if (isPressed)
        {
            Player.transform.Translate(Force * Time.deltaTime, 0, 0);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

}
