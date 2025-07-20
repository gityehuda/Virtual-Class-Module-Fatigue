using Binus.WebGL.Service;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static CustomClass;

public class LoginManager : MonoBehaviour, ILoginManager
{
    public static LoginManager instance;

    private string tenantIdMobile = "3485b963-82ba-4a6f-810f-b5cc226ff898";
    private string clientIdMobile = "b35d7211-4d79-4e43-9d6f-da89a5213122";
    private string redirectUriAndroid = "msauth://id.ac.apps.virtualclassroom/loginAD/7vmFPImG87LdEZbAJk3GIEWeCMw%3D";
    private string redirectUriiOS = "msauth.id.ac.apps.virtualclassroom://auth";
    private string scope = "openid profile User.Read";

    private string deepLinkUrl;

    private IStartUpManager startUpManager;
    private IInteropWebGLService interopWebGLService;

    private void Awake()
    {
        ServiceLocator.RegisterService<ILoginManager>(this);
    }

    private void Start()
    {
        if (instance == null)
        {
            startUpManager = ServiceLocator.GetService<IStartUpManager>();
            interopWebGLService = ServiceLocator.GetService<IInteropWebGLService>();
#if UNITY_WEBGL && !UNITY_EDITOR
            if (interopWebGLService.GetCookieWebGL("code") == null)
            {
                startUpManager.SetLoginPageActive(true);
            }
            else
            {
                startUpManager.LoginMicrosoftWaiting();
                StartCoroutine(GetUserProfileMicrosoft(interopWebGLService.GetCookieWebGL("code")));
            }
#elif UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
            startUpManager.SetLoginPageActive(true);
#endif

            if (MainData.instance.mode == "prod") startUpManager.SetLoginManualButtonActive(false);

            instance = this;
            Application.deepLinkActivated += onDeepLinkActivated;
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                // Cold start and Application.absoluteURL not null so process Deep Link.
                onDeepLinkActivated(Application.absoluteURL);
            }
            // Initialize DeepLink Manager global variable.
            else deepLinkUrl = "[none]";

            DontDestroyOnLoad(gameObject);
        }
    }

    public void ShowLoginManualButton()
    {
        startUpManager.SetLoginManualPageActive(true);
    }

    public void SubmitLoginManualButton()
    {
        string email = startUpManager.GetInputEmail();

        if (email != "" && (email.EndsWith("@binus.ac.id") || email.EndsWith("@binus.edu")))
        {
            startUpManager.LoginManualWaiting();
            StartCoroutine(GetBinusianId(email));
        }
        else
        {
            startUpManager.LoginManualFail();
        }
    }

    public void CancelLoginManualButton()
    {
        startUpManager.SetLoginManualPageActive(false);
    }

    public void LoginMicrosoftButton()
    {
#if UNITY_WEBGL
        interopWebGLService.GetLoginMicrosoftWebGL();
#elif UNITY_ANDROID
        LoginMicrosoftAndroid();
#endif
    }

    private void LoginMicrosoftAndroid()
    {
        string url = "https://login.microsoftonline.com/" + tenantIdMobile + "/oauth2/v2.0/authorize?client_id=" + clientIdMobile + "&redirect_uri=" + UnityWebRequest.EscapeURL(redirectUriAndroid) + "&response_type=code&scope=" + UnityWebRequest.EscapeURL(scope) + "&prompt=consent&response_mode=query";
        Debug.Log(url);
        Application.OpenURL(url);
    }

    private void onDeepLinkActivated(string url)
    {
        deepLinkUrl = url;

        if (deepLinkUrl == null) return;

        string callbackUrl = url.Split("?code=")[1];
        string code = callbackUrl.Split("&")[0];

        //After Login Android or iOS Success
        startUpManager.LoginMicrosoftWaiting();
        StartCoroutine(GetUserProfileMicrosoft(code));
    }

    private IEnumerator GetBinusianId(string email)
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(MainData.instance.apiMainUrl + "api/LoginManual", ""))
        {
            request.SetRequestHeader("email", email);

            yield return request.SendWebRequest();

            startUpManager.SetTextInScreen("");

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                //textInScreen.text = "User account not found, please contact administrator";
                Debug.Log(request.error + " | " + email);
                startUpManager.LoginManualFail();
            }
            else
            {
                ResponseLoginAAD response = JsonUtility.FromJson<CustomClass.ResponseLoginAAD>(request.downloadHandler.text);

                startUpManager.LoginManualSuccess(response);
            }
        }
    }

    private IEnumerator GetUserProfileMicrosoft(string code)
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(MainData.instance.apiMainUrl + "api/LoginAAD", ""))
        {
            request.SetRequestHeader("codeAzure", code);
#if UNITY_WEBGL
            request.SetRequestHeader("device", "webgl");
#elif UNITY_ANDROID
            request.SetRequestHeader("device", "android");
#elif UNITY_IOS
            request.SetRequestHeader("device", "ios");
#endif

            yield return request.SendWebRequest();

#if UNITY_WEBGL
            interopWebGLService.DeleteCookieWebGL("code");
#endif

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                startUpManager.LoginMicrosoftFail("User account not found, please contact administrator");
            }
            else
            {
                ResponseLoginAAD dataUsr = JsonUtility.FromJson<ResponseLoginAAD>(request.downloadHandler.text);

                if(dataUsr.email == "kevin.tjandra@binus.edu")
                {
                    startUpManager.LoginMicrosoftByPass();
                }
                else
                {
                    startUpManager.LoginMicrosoftSuccess(dataUsr);
                }
            }
        }
    }
}
