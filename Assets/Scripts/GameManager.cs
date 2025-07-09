using System;
using Fusion;
using StarterAssets;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public FusionBootstrap fusion;
    public UICanvasControllerInput mobileInput;
    public GameObject loadingIcon;
    public GameObject playerNameInputPanel;
    public TMP_Text playerNameTxt;
    public GameObject joinButton;
    public GameObject nameObject;
    private void Awake()
    {
        instance = this;
        mobileInput.gameObject.SetActive(false);
    }

    private void Start()
    {
        string playerName = PlayerPrefs.GetString("playerName", null);
        if (string.IsNullOrEmpty(playerName))
        {
            playerNameInputPanel.SetActive(true);
            joinButton.SetActive(false);
            nameObject.SetActive(false);
        }
        else
        {
            playerNameTxt.text = playerName;
            nameObject.SetActive(true);
        }
    }

    public void SavePlayerName(TMP_InputField name)
    {
        PlayerPrefs.SetString("playerName", name.text);
        nameObject.SetActive(true);
    }

    public void OnPlayerSpawned(StarterAssetsInputs inputs)
    {
        loadingIcon.SetActive(false);
        mobileInput.starterAssetsInputs = inputs;
        mobileInput.gameObject.SetActive(Application.isMobilePlatform);
        nameObject.SetActive(false);
    }

    public void StartGame()
    {
        fusion.StartSharedClient();
    }
}