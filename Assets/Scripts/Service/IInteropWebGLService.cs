
public interface IInteropWebGLService
{
#if UNITY_WEBGL
    void GetLoginMicrosoftWebGL();
    string GetCookieWebGL(string cookieName);
    void DeleteCookieWebGL(string cookieName);
#endif
}
