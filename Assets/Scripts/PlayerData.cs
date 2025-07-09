using System;
using Fusion;
using TMPro;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    [Networked] public string NetworkedPlayerName { get; set; }
    public TMP_Text playerName;
    public Camera cam;

    public override void Spawned()
    {
        cam = Camera.main;
        if (HasStateAuthority)
        {
            playerName.gameObject.SetActive(false);
            NetworkedPlayerName = PlayerPrefs.GetString("playerName", "Player" + Runner.LocalPlayer.PlayerId);
        }
        else
        {
            playerName.gameObject.SetActive(true);
            SetName(NetworkedPlayerName);
        }
    }

    private void Update()
    {
        Vector3 direction = playerName.transform.parent.position - cam.transform.position;
        playerName.transform.parent.forward = direction;
    }

    public void SetName(string pName)
    {
        playerName.text = pName;
    }
}