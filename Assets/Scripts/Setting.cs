using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Setting : MonoBehaviour
{
    public AudioMixer allAudioMixer;
    public int playerMovementNumber;
    public Image soundButton;
    public Image vibrateButton;
    public Image oneHandMode;
    public Image twoHandMode;
    public Image acceleration;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Sprite VibrateOnSprite;
    public Sprite VibrateOffSprite;
    public Sprite oneHandOnSprite;
    public Sprite oneHandOffSprite;
    public Sprite twoHandOnSprite;
    public Sprite twoHandOffSprite;
    public Sprite accelerationOnSprite;
    public Sprite accelerationOffSprite;

    void Awake()
    {
        // Check if it's the first time the app is opened
        if (PlayerPrefs.GetInt("IsFirstTime", 1) == 1)
        {
            // First time the app is opened, set default to "One Hand"
            OneHandButton();
            PlayerPrefs.SetInt("Sound", 1);
            PlayerPrefs.SetInt("Vibrate", 1);

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
    void Start()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            allAudioMixer.SetFloat("MasterSound", -80f);
            soundButton.sprite = musicOffSprite;
        }
        else if (PlayerPrefs.GetInt("Sound") == 1)
        {
            allAudioMixer.SetFloat("MasterSound", 0f);
            soundButton.sprite = musicOnSprite;
        }
        else if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
            allAudioMixer.SetFloat("MasterSound", 0f);
            soundButton.sprite = musicOnSprite;
        }

        if (PlayerPrefs.GetInt("Vibrate") == 0)
        {
            PlayerPrefs.SetInt("Vibrate", 0);
            vibrateButton.sprite = VibrateOffSprite;
        }
        else if (PlayerPrefs.GetInt("Vibrate") == 1)
        {
            PlayerPrefs.SetInt("Vibrate", 1);
            vibrateButton.sprite = VibrateOnSprite;
        }
        else if (!PlayerPrefs.HasKey("Vibrate"))
        {
            PlayerPrefs.SetInt("Vibrate", 1);
            vibrateButton.sprite = VibrateOnSprite;
        }

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
        oneHandMode.sprite=oneHandOnSprite;
        twoHandMode.sprite=twoHandOffSprite;
        acceleration.sprite=accelerationOffSprite;
        PlayerPrefs.Save();
    }

    public void TwoHandButton()
    {
        playerMovementNumber = 2;
        PlayerPrefs.SetInt("MovementNumber", playerMovementNumber);
        oneHandMode.sprite=oneHandOffSprite;
        twoHandMode.sprite=twoHandOnSprite;
        acceleration.sprite=accelerationOffSprite;
        PlayerPrefs.Save();
    }

    public void Acceleration()
    {
        playerMovementNumber = 3;
        PlayerPrefs.SetInt("MovementNumber", playerMovementNumber);
        oneHandMode.sprite=oneHandOffSprite;
        twoHandMode.sprite=twoHandOffSprite;
        acceleration.sprite=accelerationOnSprite;
        PlayerPrefs.Save();
    }

    public void OnButtonPressSound()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                PlayerPrefs.SetInt("Sound", 1);
                PlayerPrefs.Save();
                allAudioMixer.SetFloat("MasterSound", 0f);
                soundButton.sprite = musicOnSprite;
            }
            else
            {
                PlayerPrefs.SetInt("Sound", 0);
                PlayerPrefs.Save();
                allAudioMixer.SetFloat("MasterSound", -80f);
                soundButton.sprite = musicOffSprite;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
            PlayerPrefs.Save();
            allAudioMixer.SetFloat("MasterSound", -80f);
            soundButton.sprite = musicOffSprite;
        }
    }

    public void OnButtonPressVibrate()
    {
        if (PlayerPrefs.HasKey("Vibrate"))
        {
            if (PlayerPrefs.GetInt("Vibrate") == 0)
            {
                PlayerPrefs.SetInt("Vibrate", 1);
                vibrateButton.sprite = VibrateOnSprite;
                Handheld.Vibrate();
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.SetInt("Vibrate", 0);
                vibrateButton.sprite = VibrateOffSprite;
                Handheld.Vibrate();
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetInt("Vibrate", 1);
            vibrateButton.sprite = VibrateOffSprite;
            Handheld.Vibrate();
            PlayerPrefs.Save();
        }
    }
}
