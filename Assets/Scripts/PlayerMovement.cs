using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 mainPosition;
    public float moveDirection;
    public int movementNumber;
    public GameManager gameManager;
    public bool right;
    public bool left;
    private Vector3 initialTouchPosition;
    private Vector3 initialPlayerPosition;
    public Setting setting;
    
    private void Start()
    {
        setting = FindObjectOfType<Setting>();
        gameManager = FindObjectOfType<GameManager>();
        mainPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (gameManager.state == GameState.Play)
        {
            movementNumber = PlayerPrefs.GetInt("MovementNumber");
            switch (movementNumber)
            {
                case 1:
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Began)
                        {
                            initialTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                            initialTouchPosition.z = 0f;
                            initialPlayerPosition = transform.position;
                        }

                        if (touch.phase == TouchPhase.Moved)
                        {
                            Vector3 currentTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                            currentTouchPosition.z = 0f;

                            Vector3 movementDelta = currentTouchPosition - initialTouchPosition;
                            transform.position = new Vector3(initialPlayerPosition.x + movementDelta.x, initialPlayerPosition.y, transform.position.z);
                        }

                        // Clamp to screen bounds
                        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
                        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -screenWidth, screenWidth), transform.position.y, transform.position.z);
                    }
                    break;
                case 2:
                    if (right && !left)
                    {
                        moveDirection = 4.5f;
                        transform.Translate(moveDirection * Time.deltaTime, 0, 0);
                    }
                    else if (left && !right)
                    {
                        moveDirection = -4.5f;
                        transform.Translate(moveDirection * Time.deltaTime, 0, 0);
                    }
                    break;
                case 3:
                    float h = Input.acceleration.x;
                    transform.Translate(h * 5 * Time.deltaTime, 0, 0);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(setting.playerMovementNumber), setting.playerMovementNumber, null);
            }
        }
        else if (gameManager.state == GameState.Lose)
        {
            transform.position = Vector3.MoveTowards(transform.position, mainPosition, 2f * Time.deltaTime);
        }
    }

    public void Right_D()
    {
        right = true;
    }

    public void Left_D()
    {
        left = true;
    }

    public void Right_U()
    {
        right = false;
    }

    public void Left_U()
    {
        left = false;
    }
}