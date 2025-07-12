using System;
using ReadyPlayerMe.Core;
using ReadyPlayerMe.Samples.QuickStart;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    public static AvatarManager Instance;
    public Avatar Avatar;
    private string avatarUrl = "";
    
    private void Awake()
    {
        Instance = this;
    }

    public void OnAvatarLoaded()
    {
        Avatar = GetComponentInChildren<Animator>().avatar;
    }
}