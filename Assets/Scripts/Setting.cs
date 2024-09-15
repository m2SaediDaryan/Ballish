using UnityEngine;

public class Setting : MonoBehaviour
{
    public int playerMovementNumber;
    void Awake()
    {
        // Check if it's the first time the app is opened
        if (PlayerPrefs.GetInt("IsFirstTime", 1) == 1)
        {
            // First time the app is opened, set default to "One Hand"
            OneHandButton();
            
            // Mark that it's no longer the first time
            PlayerPrefs.SetInt("IsFirstTime", 0);
            PlayerPrefs.Save();
        }
        else
        {
            // Load the saved movement setting
            playerMovementNumber = PlayerPrefs.GetInt("MovementNumber", 1); // Default to 1 if no value is found
        }

        ApplyMovementSetting();
    }

    // Apply the loaded or default movement setting
    void ApplyMovementSetting()
    {
        if (playerMovementNumber == 1)
        {
            OneHandButton();
        }
        else if (playerMovementNumber == 2)
        {
            TwoHandButton();
        }
        else if (playerMovementNumber == 3)
        {
            Acceleration();
        }
    }

    public void OneHandButton()
    {
        playerMovementNumber = 1;
        PlayerPrefs.SetInt("MovementNumber", playerMovementNumber);
        PlayerPrefs.Save();
    }

    public void TwoHandButton()
    {
        playerMovementNumber = 2;
        PlayerPrefs.SetInt("MovementNumber", playerMovementNumber);
        PlayerPrefs.Save();
    }

    public void Acceleration()
    {
        playerMovementNumber = 3;
        PlayerPrefs.SetInt("MovementNumber", playerMovementNumber);
        PlayerPrefs.Save();
    }
}
