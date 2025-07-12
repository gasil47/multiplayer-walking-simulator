using System;
using UnityEngine;
using Fusion;
using ReadyPlayerMe.Core;
using Unity.VisualScripting;


public class NetworkedCharacterAvatar : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(LoadNetworkedAvatar)), Capacity(58)]
    public string URLAvatar { get; set; }

    public string defaultUrl = "https://models.readyplayer.me/63e9b2126f759e4d1dfabc6d.glb";

    public AvatarConfig avatarConfig;

    public Animator animator;

    public GameObject avatar;

    public RuntimeAnimatorController avatarAnimator;

    private AvatarObjectLoader avatarObjectLoader;

    public void UpdateURL()
    {
        if (Object.HasStateAuthority)
        {
            URLAvatar = defaultUrl;
        }
    }

    public void LoadNetworkedAvatar()
    {
        Debug.Log("changing avatar url to: " + URLAvatar);
        if (!string.IsNullOrEmpty(URLAvatar))
        {
            avatarObjectLoader = new AvatarObjectLoader();
            if (avatarConfig)
                avatarObjectLoader.AvatarConfig = avatarConfig;
            avatarObjectLoader.OnCompleted += OnAvatarLoadCompleted;
            avatarObjectLoader.OnFailed += OnAvatarLoadFailed;
            avatarObjectLoader.LoadAvatar(URLAvatar);
        }
    }
    private void OnAvatarLoadCompleted(object sender, CompletionEventArgs args)
    {
        avatarObjectLoader.OnCompleted -= OnAvatarLoadCompleted;
        avatarObjectLoader.OnFailed -= OnAvatarLoadFailed;
        avatar = args.Avatar;
        if (avatar)
        {
            avatar.transform.SetParent(transform, false);
            AvatarMeshHelper.TransferMesh(avatar, transform.GetChild(1).gameObject);
            animator.Rebind();
            animator.Update(0);
            Destroy(avatar.GetComponent<Animator>());
        }

        if (args.Metadata.BodyType == BodyType.HalfBody)
        {
            avatar.transform.position = new Vector3(0, 1, 0);
        }
    }

    private void OnAvatarLoadFailed(object sender, FailureEventArgs args)
    {
        avatarObjectLoader.OnCompleted -= OnAvatarLoadCompleted;
        avatarObjectLoader.OnFailed -= OnAvatarLoadFailed;

        Debug.LogError("Avatar Load failed: " + args.Message);
    }

    private void OnDestroy()
    {
        if (avatarObjectLoader != null)
        {
            avatarObjectLoader.OnCompleted -= OnAvatarLoadCompleted;
            avatarObjectLoader.OnFailed -= OnAvatarLoadFailed;
        }
    }
}