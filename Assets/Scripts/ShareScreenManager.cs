using Binus.WebGL.Service;
using FMETP;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class ShareScreenManager : MonoBehaviour
{
    public static ShareScreenManager instance;
    public TextureEncoder textureEncoder;
    public GameViewDecoder gameViewDecoder;
    public GameObject btnShareScreen;
    public List<SpriteOfShareScreen> spriteShareScreen;
    [SerializeField] GameObject btnCloseUIShare;
    public GameObject parentShareScreenTemplate;
    [SerializeField] RawImage shareScreenUI;
    [SerializeField] RawImage shareScreenRawImageMine;

    [HideInInspector] public bool isScreenShared = false;
    [HideInInspector] public bool isScreenAllowed = false;
    [HideInInspector] public int actorNumberIdShare;

#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void StartShareScreen();

    [DllImport("__Internal")]
    private static extern void StopShareScreen();

    [DllImport("__Internal")]
    private static extern void CaptureFrameAndSendToUnity();
#endif

    float time = 0;
    [HideInInspector] public bool isMyShared = false;
    public bool isShareUI = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (MainData.instance.roleUsr != "1")
        {
            BtnDisableShareScreen();
        }

        btnShareScreen.GetComponent<Button>().onClick.AddListener(delegate
        {
#if UNITY_WEBGL
            StartShareScreen();
#elif UNITY_ANDROID
            OpenShareScreenAndroid();
#endif
            //JSHandleStartShareScreen(1);
        });

        btnCloseUIShare.GetComponent<Button>().onClick.AddListener(delegate
        {
            parentShareScreenTemplate.SetActive(false);
            isShareUI = false;
            GameplayManager.instance.DeActivateWindow();
        });
    }

    private void OpenShareScreenAndroid()
    {
        PopUpManager popUpManager = ServiceLocator.GetService<PopUpManager>();
        popUpManager.OpenPopUp("<b>Fitur Share Screen belum didukung di perangkat Android.</b>\nGunakan Chrome atau Edge di desktop untuk pengalaman terbaik.");
    }

    private void BtnDisableShareScreen()
    {
        btnShareScreen.GetComponent<Button>().interactable = false;
        btnShareScreen.transform.GetChild(0).GetComponent<Image>().sprite = spriteShareScreen.FirstOrDefault(x => x.status == StatusShareScreenForSprite.DisableShareScreen).sprite;
    }

    private void BtnEnableShareScreen()
    {
        btnShareScreen.GetComponent<Button>().interactable = true;
        btnShareScreen.transform.GetChild(0).GetComponent<Image>().sprite = spriteShareScreen.FirstOrDefault(x => x.status == StatusShareScreenForSprite.EnableShareScreen).sprite;

        btnShareScreen.GetComponent<Button>().onClick.RemoveAllListeners();
        btnShareScreen.GetComponent<Button>().onClick.AddListener(delegate
        {
#if UNITY_WEBGL
            StartShareScreen();
#endif
        });
    }

    private void BtnStopShareScreen()
    {
        btnShareScreen.transform.GetChild(0).GetComponent<Image>().sprite = spriteShareScreen.FirstOrDefault(x => x.status == StatusShareScreenForSprite.StopShareScreen).sprite;
        btnShareScreen.GetComponent<Button>().onClick.RemoveAllListeners();
        btnShareScreen.GetComponent<Button>().onClick.AddListener(delegate
        {
            if (isMyShared == true)
            {
#if UNITY_WEBGL
                StopShareScreen();
#endif
            }
            else ForceStopShareScreen();
        });
    }

    private void ForceStopShareScreen()
    {
        PhotonManager.instance.SendData((byte)CustomClass.TypeData.ForceStopShare, new CustomClass.DataShareScreen());
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (isScreenShared && isMyShared && time > 0.33f)
        {
#if UNITY_WEBGL
            CaptureFrameAndSendToUnity();
#endif
            time = 0;

            Invoke("FixedUpdate", 0.05f);
        }
    }

    public void JSHandleStartShareScreen(int isSuccess)
    {
        if (isSuccess == 1)
        {
            isScreenShared = true;
            isMyShared = true;
            ShareScreenRoom.instance.StartShareScreen();
            BtnStopShareScreen();
            LockCharacter();
            shareScreenRawImageMine.gameObject.SetActive(true);

            actorNumberIdShare = 0;

            CustomClass.DataStartShareScreen data = new CustomClass.DataStartShareScreen
            {
                actorNumberId = PhotonNetwork.LocalPlayer.ActorNumber,
            };

            PhotonManager.instance.SendData((byte)CustomClass.TypeData.StartShareScreen, data);
        }
    }

    public void JSHandleStopShareScreen(int isStopped)
    {
        if (isStopped == 1)
        {
            isShareUI = false;
            isMyShared = false;
            isScreenShared = false;
            parentShareScreenTemplate.SetActive(false);
            shareScreenRawImageMine.gameObject.SetActive(false);
            ShareScreenRoom.instance.StopShareScreen();
            BtnEnableShareScreen();
            ReleaseCharacter();

            CustomClass.DataStopShareScreen data = new CustomClass.DataStopShareScreen
            {
                actorNumberId = PhotonNetwork.LocalPlayer.ActorNumber,
            };

            PhotonManager.instance.SendData((byte)CustomClass.TypeData.StopShareScreen, data);
        }
    }

    public void HandleShareScreen(CustomClass.DataShareScreen data)
    {
        var user = GameplayManager.instance.listOfUser[data.actorNumberId];
        Texture2D texture = new Texture2D(data.width, data.height, TextureFormat.RGB24, false); // You might need to specify dimensions

        if (texture.LoadImage(data.image))
        {
            ApplyVideoTexture(texture);
        }
        else
        {
            Debug.LogError("Failed to load video texture from encoded data.");
        }

    }

    public void HandleStartShareScreen(CustomClass.DataStartShareScreen data)
    {
        if (MainData.instance.roleUsr == "1" && isMyShared)
        {
            BtnStopShareScreen();
        }
        else
        {
            BtnDisableShareScreen();
        }
        actorNumberIdShare = data.actorNumberId;
        var user = GameplayManager.instance.listOfUser[data.actorNumberId];
        isScreenShared = true;
        ShareScreenRoom.instance.StartShareScreen();
        GetComponent<TextureEncoder>().enabled = false;
    }

    public void HandleStopShareScreen(CustomClass.DataStopShareScreen data)
    {
        if (MainData.instance.roleUsr == "1" || isScreenAllowed)
        {
            BtnEnableShareScreen();
        }
        else
        {
            BtnDisableShareScreen();
        }
        actorNumberIdShare = 0;
        isScreenShared = false;
        parentShareScreenTemplate.SetActive(false);
        ShareScreenRoom.instance.StopShareScreen();
        GetComponent<TextureEncoder>().enabled = true;
        isShareUI = false;
    }

    public void HandleForceStopShare()
    {
        if (isMyShared == true)
        {
#if UNITY_WEBGL
            StopShareScreen();
#endif
        }
    }

    public void HandleGiveShareAll(CustomClass.DataGiveShare data)
    {
        isScreenAllowed = data.isShare;
        if (isMyShared == true) return;
        if (isScreenAllowed && !isScreenShared)
        {
            BtnEnableShareScreen();
        }
        else
        {
            BtnDisableShareScreen();
        }
    }

    public void HandleGiveShareTarget(CustomClass.DataGiveShare data)
    {
        if (data.actorNumberId != PhotonNetwork.LocalPlayer.ActorNumber) return;
        isScreenAllowed = data.isShare;
        if (isScreenAllowed && !isScreenShared)
        {
            BtnEnableShareScreen();
        }
        else
        {
            BtnDisableShareScreen();
        }
    }

    public void OnVideoFrame(string messageData)
    {
        string[] parameters = messageData.Split(',');
        string encodedData = parameters[0];
        int width = int.Parse(parameters[1]);
        int height = int.Parse(parameters[2]);

        byte[] imageData = System.Convert.FromBase64String(encodedData);

        Texture2D videoTexture = new Texture2D(width, height, TextureFormat.RGB24, false); // You might need to specify dimensions

        if (videoTexture.LoadImage(imageData))
        {
            Color[] pixels = videoTexture.GetPixels();
            videoTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
            videoTexture.SetPixels(pixels);
            videoTexture.Apply();

            if (videoTexture != null) textureEncoder.StreamTexture = videoTexture;
            ApplyVideoTexture(videoTexture);
        }
        else
        {
            Debug.LogError("Failed to load video texture from encoded data.");
        }
    }

    private void ApplyVideoTexture(Texture2D videoTexture)
    {
        if (isMyShared) shareScreenRawImageMine.texture = videoTexture;
        else if (isShareUI) shareScreenUI.texture = videoTexture;
        else ShareScreenRoom.instance.shareScreenImage.texture = videoTexture;
    }

    public void LockCharacter()
    {
        IPlayerManager playerManager = ServiceLocator.GetService<IPlayerManager>();

        playerManager.StandUp();
        playerManager.isNotWalking = false;

        var myHuman = GameplayManager.instance.myHuman;
        myHuman.transform.localPosition = new Vector3(7.211011f, myHuman.transform.localPosition.y, -2.580234f);
        myHuman.transform.localRotation = Quaternion.Euler(new Vector3(0, -90f, 0f));
        CameraController.instance.cinemachineFreeLook.m_YAxis.Value = 0.53f;
        CameraController.instance.cinemachineFreeLook.m_XAxis.Value = -87.66f;
        if (SceneManager.GetActiveScene().name == "DiscussionRoom")
        {
            myHuman.transform.localPosition = new Vector3(-4.176586f, myHuman.transform.localPosition.y, 0.8685403f);
            myHuman.transform.localRotation = Quaternion.Euler(new Vector3(0, 90f, 0f));
            CameraController.instance.cinemachineFreeLook.m_YAxis.Value = 0.5468783f;
            CameraController.instance.cinemachineFreeLook.m_XAxis.Value = 96.0983f;
        }
        CameraController.instance.isCameraMoving = false;
    }

    public void ReleaseCharacter()
    {
        IPlayerManager playerManager = ServiceLocator.GetService<IPlayerManager>();

        playerManager.isNotWalking = true;

        CameraController.instance.isCameraMoving = true;
    }
}

[Serializable]
public class SpriteOfShareScreen
{
    public StatusShareScreenForSprite status;
    public Sprite sprite;
}

[Serializable]
public enum StatusShareScreenForSprite
{
    EnableShareScreen,
    DisableShareScreen,
    StopShareScreen
}
