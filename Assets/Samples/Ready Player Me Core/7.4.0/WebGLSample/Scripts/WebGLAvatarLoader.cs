using ReadyPlayerMe.Core;
using UnityEngine;
using UnityEngine.Events;
using BodyType = ReadyPlayerMe.Core.BodyType;

namespace ReadyPlayerMe.Samples.WebGLSample
{
    [RequireComponent(typeof(WebFrameHandler))]
    public class WebGLAvatarLoader : MonoBehaviour
    {
        private const string TAG = nameof(WebGLAvatarLoader);
        public GameObject avatar;
        public string avatarUrl = "";
        private WebFrameHandler webFrameHandler;
        public UnityEvent OnAvatarLoaded;
        public AvatarConfig avatarConfig;
        public RuntimeAnimatorController avatarAnimator;
        private void Start()
        {
            webFrameHandler = GetComponent<WebFrameHandler>();
            webFrameHandler.OnAvatarExport += HandleAvatarLoaded;
            webFrameHandler.OnUserSet += HandleUserSet;
            webFrameHandler.OnUserAuthorized += HandleUserAuthorized;
            LoadAvatar();
        }

        private void OnAvatarLoadCompleted(object sender, CompletionEventArgs args)
        {
            if (avatar) Destroy(avatar);
            avatar = args.Avatar;
            avatar.transform.SetParent(transform, false);
            avatar.GetComponent<Animator>().runtimeAnimatorController = avatarAnimator;
            OnAvatarLoaded.Invoke();
            if (args.Metadata.BodyType == BodyType.HalfBody)
            {
                avatar.transform.position = new Vector3(0, 1, 0);
            }
        }

        private void OnAvatarLoadFailed(object sender, FailureEventArgs args)
        {
            SDKLogger.Log(TAG, $"Avatar Load failed with error: {args.Message}");
        }

        public void HandleAvatarLoaded(string newAvatarUrl)
        {
            LoadAvatarFromUrl(newAvatarUrl);
        }

        public void HandleUserSet(string userId)
        {
            SDKLogger.Log(TAG, $"User set: {userId}");
        }

        public void HandleUserAuthorized(string userId)
        {
            SDKLogger.Log(TAG, $"User authorized: {userId}");
        }

        [ContextMenu("Load Avatar")]
        public void LoadAvatar()
        {
            if (string.IsNullOrEmpty(avatarUrl))
            {
                SDKLogger.Log(TAG, "Avatar URL is empty. Please set a valid URL.");
                return;
            }

            LoadAvatarFromUrl(avatarUrl);
        }

        public void LoadAvatarFromUrl(string newAvatarUrl)
        {
            var avatarLoader = new AvatarObjectLoader();
            if (avatarConfig)
                avatarLoader.AvatarConfig = avatarConfig;
            avatarUrl = newAvatarUrl;
            avatarLoader.OnCompleted += OnAvatarLoadCompleted;
            avatarLoader.OnFailed += OnAvatarLoadFailed;
            avatarLoader.LoadAvatar(avatarUrl);
        }
    }
}