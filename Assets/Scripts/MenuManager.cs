using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel,_replayPanel,_gamePanel;
    //[SerializeField] private TextMeshProUGUI _stateText;

    void Awake()
    {
        GameManager.OnGameStateChanged +=GameManagerOnOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }
    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        //_startPanel.SetActive(state == GameState.Start);
        //throw new System.NotImplementedException();

        //_gamePanel.interactable = state == GameState.Play;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
