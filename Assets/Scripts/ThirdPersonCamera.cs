using System;
using Cinemachine;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private CinemachineBrain _cinemachineBrain;

    private void Awake()
    {
        _cinemachineBrain = GetComponent<CinemachineBrain>();
    }

    public void SetupPlayer(Transform player)
    {
        _cinemachineBrain.ActiveVirtualCamera.Follow = player;
    }
}