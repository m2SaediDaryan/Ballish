using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 mainPosition;
    //public Vector3 mainposition =new Vector3(0,-3.1960001f,0);
    public float moveDirection;
    public int movementNumber;
    public GameManager gameManager;
    //public PlayerMovementSelect newMovementSelect;
    public bool right;
    public bool left;
    public bool stopCo;
    private Vector3 initialTouchPosition;
    private Vector3 initialPlayerPosition;
    public Setting setting;
    //public float duration = 2.0f;
    private Coroutine resetCoroutine;
    public float elapsedTime;

    private void Start()
    {
        //StartCoroutine(SmoothResetPosition());
        //movementSelect = PlayerMovementSelect.Acceleration;
        setting = FindObjectOfType<Setting>();
        gameManager = FindObjectOfType<GameManager>();
        mainPosition = transform.position;
    }

    void FixedUpdate()
    {
        //currentMovementType = LoadMovementSetting();
        if (gameManager.state == GameState.Play)
        {
            //StopCoroutine(SmoothResetPosition());
            //movementSelect = newMovementSelect;
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
            ResetPlayerPosition();
            return;
        }
    }

    public void ResetPlayerPosition()
    {
        /*Vector3 targetPosition;
        Vector3 initialPosition = transform.position;
        float elapsedTime = 0f;
        float duration =1f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, mainposition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            //Debug.Log(elapsedTime);
            //Debug.Log();
            // Wait until the next frame
        }
        //Debug.Log(elapsedTime);
        transform.position = mainposition;*/
        /*if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
        }*/
        StartCoroutine(SmoothResetPosition(mainPosition, 1.0f));
    }

    /*IEnumerator SmoothResetPosition()
    {
        while(true)
        {
            ResetPlayerPosition();
            yield return new WaitUntil();
        }
    }*/
    IEnumerator SmoothResetPosition(Vector3 targetPosition, float duration)
    {
        Vector3 initialPosition = transform.position;
        elapsedTime = 0f;

        while (elapsedTime <= duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
            //gameManager.stopCo=false;
            //Debug.Log(elapsedTime);
            //Debug.Log();
            // Wait until the next frame
        }
        transform.position = targetPosition;
    }

    /*public void StopResetCoroutine()
    {
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }
    }*/

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
