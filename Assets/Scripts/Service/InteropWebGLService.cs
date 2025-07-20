#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class InteropWebGLService : IInteropWebGLService
{
#if UNITY_WEBGL

    [DllImport("__Internal")]
    private static extern void DeleteCookie(string cookieName);

    [DllImport("__Internal")]
    private static extern string GetCookie(string cookieName);

    [DllImport("__Internal")]
    private static extern void LoginMicrosoft();

    public void GetLoginMicrosoftWebGL()
    {
        LoginMicrosoft();
    }

    public string GetCookieWebGL(string cookieName)
    {
        return GetCookie(cookieName);
    }

    public void DeleteCookieWebGL(string cookieName)
    {
        DeleteCookie(cookieName);
    }
#endif
}
