using UnityEngine;

public class MobileService : MonoBehaviour, IMobileService
{
    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    private void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS
        DontDestroyOnLoad(gameObject);
#else
        Destroy(this.gameObject);
#endif
    }
}
