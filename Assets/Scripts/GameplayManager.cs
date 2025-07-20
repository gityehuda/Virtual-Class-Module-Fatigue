using Binus.WebGL.Service;
using Cinemachine;
using FMETP;
using Photon.Pun;
using Photon.Voice.PUN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour, IGameplayManager
{
    public static GameplayManager instance;

    public TextureEncoder textureEncoder;
    [SerializeField] GameObject parentDecoder;
    [SerializeField] GameObject prefabDecoder;
    GameObject prefabMale;
    GameObject prefabFeMale;
    GameObject parentHuman;
    [HideInInspector] public GameObject myHuman;
    [SerializeField] GameObject parentVideoFace;
    [SerializeField] CinemachineFreeLook cinemachine;
    [HideInInspector] public bool isHumanSitting = false;
    [HideInInspector] public WebCamTexture webCamTexture;
    [HideInInspector] public Dictionary<int, CustomClass.User> listOfUser;
    [HideInInspector] public int photonViewId;
    [HideInInspector] public bool isPlay = false;

    [SerializeField] GameObject buttonResetPosition;
    Vector3 startPosition;
    List<AssetBundle> assetBundles = new List<AssetBundle>();

    [HideInInspector] public float humanYPosition = 0.103f;
    [HideInInspector] public StudentChair[] studentChairs;

    public WindowManagement windowManagement = new WindowManagement();

    private void Awake()
    {
        instance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        LoadAssetBundle();
    }

    private void Start()
    {
        ServiceLocator.RegisterService<IGameplayManager>(this);
        buttonResetPosition.transform.localPosition -= new Vector3(0, MainData.instance.roleUsr != "1" ? 80f : 0f, 0f);

        buttonResetPosition.GetComponent<Button>().onClick.AddListener(delegate
        {
            ResetPosition();
        });
    }

    private GameObject CreateHuman(string sex, string role)
    {
        var human = Instantiate(sex == "M" ? prefabMale : prefabFeMale);

        ChangeLayer(human, "PlayerVirtualClassroom");

        human.transform.SetParent(parentHuman.transform);

        var almameter = human.transform.GetChild(2).transform.GetChild(0).gameObject;
        var batikUngu = human.transform.GetChild(2).transform.GetChild(1).gameObject;
        var batikPutih = human.transform.GetChild(2).transform.GetChild(2).gameObject;

        almameter.SetActive(false);
        batikUngu.SetActive(false);
        batikPutih.SetActive(false);

        if (role == "1")
        {
            (sex == "M" ? batikUngu : batikPutih).SetActive(true);
        }
        else
        {
            almameter.SetActive(true);
        }

        return human;
    }

    public void CreateMyHuman(Vector3 position)
    {
        listOfUser = new Dictionary<int, CustomClass.User>();
        myHuman = CreateHuman(MainData.instance.sexUsr, MainData.instance.roleUsr);

        if (position == Vector3.zero) myHuman.transform.localPosition = new Vector3(5.26f, humanYPosition, -3.94f);
        else myHuman.transform.localPosition = position;

        if (SceneManager.GetActiveScene().name == "DiscussionRoom")
        {
            myHuman.transform.localPosition = new Vector3(0.9737053f, humanYPosition, 0.6743646f);
            myHuman.transform.localEulerAngles = new Vector3(0, -90f, 0);
        }

        if (position == Vector3.zero) startPosition = myHuman.transform.localPosition;

        myHuman.AddComponent<PlayerManager>();

        Destroy(myHuman.transform.GetChild(0).GetComponentInChildren<RawImage>());

        foreach (Transform child in myHuman.transform)
        {
            if (child.gameObject.name == "Cube")
            {
                cinemachine.Follow = child;
                cinemachine.LookAt = child;
            }
            else if (child.gameObject.name == "TextName")
            {
                Destroy(child.gameObject);
            }
            else if (child.gameObject.name == "Canvas")
            {
                Destroy(child.gameObject);
            }
        }

        var photonView = myHuman.AddComponent<PhotonView>();
        var photonVoiceView = myHuman.AddComponent<PhotonVoiceView>();
        PhotonManager.instance.m_photonView = photonView;
        PhotonManager.instance.photonVoiceView = photonVoiceView;

        var photonTransformView = myHuman.AddComponent<PhotonTransformView>();
        var photonAnimatorView = myHuman.AddComponent<PhotonAnimatorView>();

        photonTransformView.m_SynchronizePosition = true;
        photonTransformView.m_SynchronizeRotation = true;

        photonAnimatorView.SetParameterSynchronized("Animation", PhotonAnimatorView.ParameterType.Int, PhotonAnimatorView.SynchronizeType.Discrete);

        photonView.ObservedComponents = new List<Component> { photonTransformView, photonAnimatorView };

        if (PhotonNetwork.AllocateViewID(photonView))
        {
            CustomClass.DataNewUserJoin dataHumanMoving = new CustomClass.DataNewUserJoin
            {
                position = new CustomClass.Vector3Models(myHuman.transform.position.x, myHuman.transform.position.y, myHuman.transform.position.z),
                rotation = new CustomClass.QuaternionModels(myHuman.transform.rotation.x, myHuman.transform.rotation.y, myHuman.transform.rotation.z, myHuman.transform.rotation.w),
                actorNumberId = PhotonNetwork.LocalPlayer.ActorNumber,
                viewId = photonView.ViewID,
                sexUsr = MainData.instance.sexUsr,
                roleUsr = MainData.instance.roleUsr,
                nameUsr = MainData.instance.namaUsr,
                nimUsr = MainData.instance.nimUsr
            };

            photonViewId = photonView.ViewID;
            PhotonManager.instance.m_photonView = photonView;

            StartCoroutine(MutedAudio());

            PhotonManager.instance.SendData((byte)CustomClass.TypeData.NewUserJoin, dataHumanMoving);
            FaceCamManager.instance.isSendActive = true;

            isPlay = true;
        }
        else
        {
            Destroy(myHuman);
        }
    }

    private IEnumerator MutedAudio()
    {
        yield return new WaitForEndOfFrame();
        PhotonManager.instance.photonVoiceView.RecorderInUse.TransmitEnabled = false;
    }

    public void HandleNewHuman(CustomClass.DataNewUserJoin data)
    {
        GameObject player = CreateHuman(data.sexUsr, data.roleUsr);

        player.transform.position = new Vector3(data.position.x, data.position.y, data.position.z);
        player.transform.rotation = new Quaternion(data.rotation.x, data.rotation.y, data.rotation.z, data.rotation.w);

        Destroy(player.GetComponent<Rigidbody>());
        Destroy(player.GetComponent<CapsuleCollider>());

        player.GetComponentInChildren<TextMeshPro>().text = data.nimUsr + " - " + data.nameUsr;

        PhotonView photonView = player.AddComponent<PhotonView>();
        photonView.ViewID = (int)data.viewId;

        var photonVoiceView = player.AddComponent<PhotonVoiceView>();

        var decoder = Instantiate(prefabDecoder);
        decoder.transform.SetParent(parentDecoder.transform);

        var photonTransformView = player.AddComponent<PhotonTransformView>();
        var photonAnimatorView = player.AddComponent<PhotonAnimatorView>();

        photonTransformView.m_SynchronizePosition = true;
        photonTransformView.m_SynchronizeRotation = true;

        photonAnimatorView.SetParameterSynchronized("Animation", PhotonAnimatorView.ParameterType.Int, PhotonAnimatorView.SynchronizeType.Discrete);

        photonView.ObservedComponents = new List<Component> { photonTransformView, photonAnimatorView };

        var outline = player.transform.GetChild(2).gameObject.AddComponent<Outline>();
        outline.OutlineColor = Color.green;
        outline.OutlineWidth = 10f;
        outline.enabled = false;
        VoiceDetectionSpeaker(player, photonVoiceView, outline);

        var user = new CustomClass.User
        {
            decoder = decoder,
            human = player,
            externalSystemId = data.nimUsr,
            name = data.nameUsr,
            isAudio = data.isAudio,
        };

        ChatManager.instance.ShowChat(data.nimUsr + " - " + data.nameUsr + " has joined");

        if (data.roleUsr == "1")
        {
            var dataSync = new CustomClass.DataSyncAudioAndShare
            {
                actorNumberId = PhotonNetwork.LocalPlayer.ActorNumber,
                isAudio = AudioManager.instance.isAudio,
                isShare = ShareScreenManager.instance.isScreenAllowed,
            };

            PhotonManager.instance.SendData((byte)CustomClass.TypeData.SyncAudioAndShare, dataSync);
        }

        listOfUser.Add((int)data.actorNumberId, user);
    }

    private void VoiceDetectionSpeaker(GameObject player, PhotonVoiceView photonVoiceView, Outline outline)
    {
        var voiceSpeaker = player.AddComponent<VoiceSpeakerOutline>();
        voiceSpeaker.photonVoiceView = photonVoiceView;
        voiceSpeaker.outline = outline;
    }

    void ChangeLayer(GameObject parent, string layerName)
    {
        parent.layer = LayerMask.NameToLayer(layerName);
        foreach (Transform child in parent.transform)
        {
            ChangeLayer(child.gameObject, layerName);
        }
    }

    public Vector3 GetMyPosition()
    {
        return myHuman.transform.position;
    }

    public Vector3 GetMyRotation()
    {
        return myHuman.transform.eulerAngles;
    }

    void ResetPosition()
    {
        IPlayerManager playerManager = ServiceLocator.GetService<IPlayerManager>();
        playerManager.StandUp();

        myHuman.transform.localPosition = startPosition;
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        // Set the layer of the current GameObject
        obj.layer = layer;

        // Recursively set the layer for each child GameObject
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    void LoadAssetBundle()
    {
        foreach (var child in MainData.instance.pathFileMain)
        {
            AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(child));

            AssetBundle assetBundle = createRequest.assetBundle;

            assetBundles.Add(assetBundle);

            foreach (var assetName in assetBundle.GetAllAssetNames())
            {
                if (assetName.ToLower().Contains("creativeclass") && MainData.instance.roomName == "CreativeClass")
                {
                    LoadAssetBundleCreativeClass(assetBundle, assetName);
                }
                else if (assetName.ToLower().Contains("discussionroom") && MainData.instance.roomName == "DiscussionRoom")
                {
                    LoadAssetBundleDiscussionRoom(assetBundle, assetName);
                }
                else if (assetName.ToLower().Contains("female"))
                {
                    prefabFeMale = SearchPrefab(assetBundle, assetName);
                }
                else if (assetName.ToLower().Contains("male"))
                {
                    prefabMale = SearchPrefab(assetBundle, assetName);
                }
            }
        }
    }

    private GameObject SearchPrefab(AssetBundle assetBundle, string assetName)
    {
        return assetBundle.LoadAsset<GameObject>(assetName);
    }

    private void LoadAssetBundleCreativeClass(AssetBundle assetBundle, string assetName)
    {
        var prefabRoom = SearchPrefab(assetBundle, assetName);
        var instantRoom = Instantiate(prefabRoom);
        instantRoom.transform.localPosition = Vector3.zero;

        studentChairs = instantRoom.GetComponentsInChildren<StudentChair>();

        instantRoom.GetComponentInChildren<ShareScreenRoom>().gameObject.SetActive(false);

        for (int i = 0; i < studentChairs.Length; i++)
        {
            studentChairs[i].id = i;
        }

        int arrayWhiteboard = 1;
        var whiteboardManager = GetComponentInChildren<WhiteboardManager>();
        whiteboardManager.whiteboards = new List<MyWhiteboard>();

        int newLayer = LayerMask.NameToLayer("EnvironmentVirtualClassroom");

        SetLayerRecursively(instantRoom, newLayer);

        foreach (Transform child3 in instantRoom.transform)
        {
            if (child3.gameObject.name.ToLower().Contains("whiteboard"))
            {
                foreach (Transform child4 in child3)
                {
                    var myWhiteboard = child4.gameObject.AddComponent<MyWhiteboard>();
                    myWhiteboard.brushSize = 20f;
                    myWhiteboard.whichWhiteboard = arrayWhiteboard;

                    whiteboardManager.whiteboards.Add(myWhiteboard);

                    whiteboardManager.CreateTexture2D(myWhiteboard);

                    arrayWhiteboard++;
                }
            }
            else if (child3.gameObject.name.ToLower().Contains("creativeclasslarge"))
            {
                foreach (Transform child4 in child3)
                {
                    if (child4.gameObject.name.ToLower() == "door")
                    {
                        child4.gameObject.AddComponent<BoxCollider>();
                        child4.gameObject.AddComponent<DoorManager>();
                    }
                }
            }
        }

        parentHuman = instantRoom;
    }

    private void LoadAssetBundleDiscussionRoom(AssetBundle assetBundle, string assetName)
    {
        var prefabRoom = SearchPrefab(assetBundle, assetName);
        var instantRoom = Instantiate(prefabRoom);
        instantRoom.transform.localPosition = Vector3.zero;

        instantRoom.GetComponentInChildren<ShareScreenRoom>().gameObject.SetActive(false);

        var whiteboardManager = GetComponentInChildren<WhiteboardManager>();
        whiteboardManager.whiteboards = new List<MyWhiteboard>();

        foreach (Transform child3 in instantRoom.transform)
        {
            if (child3.gameObject.name.ToLower().Contains("whiteboard"))
            {
                foreach (Transform child4 in child3)
                {
                    var myWhiteboard = child4.gameObject.AddComponent<MyWhiteboard>();
                    myWhiteboard.brushSize = 20f;
                    myWhiteboard.whichWhiteboard = 1;

                    whiteboardManager.whiteboards.Add(myWhiteboard);

                    whiteboardManager.CreateTexture2D(myWhiteboard);
                }
            }
            else if (child3.gameObject.name.ToLower() == "door")
            {
                child3.gameObject.AddComponent<BoxCollider>();
                child3.gameObject.AddComponent<DoorManager>();
            }
        }

        parentHuman = instantRoom;
    }

    private void OnDestroy()
    {
        foreach (var assetBundle in assetBundles)
        {
            assetBundle.Unload(true);
        }
    }

    public bool CheckSomeWindowActive()
    {
        return windowManagement.isSomeWindowActive;
    }

    public void ActivateWindow(WindowList window)
    {
        windowManagement.isSomeWindowActive = true;
        windowManagement.windowActive = window;
    }

    public void ActivateWindowUI()
    {
        windowManagement.isSomeWindowActive = true;
        windowManagement.windowActive = WindowList.UI;
    }

    public void DeActivateWindow()
    {
        windowManagement.isSomeWindowActive = false;
        windowManagement.windowActive = WindowList.Empty;
    }
}

[Serializable]
public enum WindowList
{
    Empty,
    Photon,
    Whiteboard,
    BreakoutRoom,
    Help,
    UI,
    Joystick
}

[Serializable]
public class WindowManagement
{
    public bool isSomeWindowActive;
    public WindowList windowActive;
}
