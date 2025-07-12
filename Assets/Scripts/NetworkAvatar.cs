using UnityEngine;
using Fusion;

public class NetworkAvatar : NetworkBehaviour
{
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcLoad()
    {
        Animator anim = GetComponent<Animator>();
        anim.enabled = false;
        anim.avatar = AvatarManager.Instance.Avatar;
        anim.enabled = true;
        AvatarManager.Instance.transform.GetChild(0).GetChild(0).SetParent(transform, false);
        AvatarManager.Instance.transform.GetChild(0).GetChild(0).SetParent(transform, false);
        Destroy(AvatarManager.Instance.transform.GetChild(0).gameObject);
        anim.Rebind();
        anim.Update(0f);
    }
}