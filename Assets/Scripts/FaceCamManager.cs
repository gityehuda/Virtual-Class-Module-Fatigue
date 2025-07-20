using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using FMETP;

public class FaceCamManager : MonoBehaviour
{
    public static FaceCamManager instance;

    [SerializeField] TextureEncoder textureEncoder;
    [SerializeField] GameObject btnFaceCam;
    [SerializeField] Sprite faceCamVideoOn;
    [SerializeField] Sprite faceCamVideoOff;
    public WebcamManager webCamManager;

    public RawImage imageFaceCam;

    [HideInInspector] public bool isMuted = false;
    [HideInInspector] public bool isSendActive = false;

    [HideInInspector] public WebCamTexture StreamWebCamTexture;
    public WebCamTexture SetStreamWebCamTexture { set { StreamWebCamTexture = value; } }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        btnFaceCam.GetComponent<Button>().onClick.AddListener(delegate
        {
            OnOffFaceCam();
        });
#if UNITY_ANDROID || UNITY_IOS
        webCamManager.useFrontCam = true;
#endif
    }

    public void OnOffFaceCam()
    {
        isMuted = !isMuted;

        if (isMuted == true) webCamManager.Action_StopWebcam();
        else webCamManager.Action_StartWebcam();

        CustomClass.DataStartStopFaceCam data = new CustomClass.DataStartStopFaceCam
        {
            actorNumberId = PhotonNetwork.LocalPlayer.ActorNumber,
            isMuted = isMuted,
        };

        PhotonManager.instance.SendData((byte)CustomClass.TypeData.StartStopFaceCam, data);

        btnFaceCam.transform.GetChild(0).GetComponent<Image>().sprite = isMuted == true ? faceCamVideoOff : faceCamVideoOn;
        imageFaceCam.color = isMuted == true ? new Color(0f, 0f, 0f, 0f) : Color.white;
    }

    private void Update()
    {
        if (StreamWebCamTexture != null && imageFaceCam.texture == null)
        {
            imageFaceCam.color = new Color(255f, 255f, 255f, 255f);
            imageFaceCam.texture = StreamWebCamTexture;
        }
    }

    public void HandleFaceCam(CustomClass.DataFaceCam data)
    {
        var user = GameplayManager.instance.listOfUser[data.actorNumberId];

        Texture2D texture = new Texture2D(data.width, data.height, TextureFormat.RGB24, false); // You might need to specify dimensions
        if (texture.LoadImage(data.image))
        {
            foreach (Transform child in user.human.transform)
            {
                if (child.gameObject.name == "Canvas")
                {
                    child.GetComponentInChildren<RawImage>().color = new Color(255, 255, 255, 255);
                    ApplyVideoTexture(child.GetComponentInChildren<RawImage>(), texture);
                }
            }
        }
        else
        {
            Debug.LogError("Failed to load video texture from encoded data.");
        }
    }

    public void HandleStartStopFaceCam(CustomClass.DataStartStopFaceCam data)
    {
        var user = GameplayManager.instance.listOfUser[data.actorNumberId];

        user.human.transform.GetChild(0).GetComponentInChildren<RawImage>().enabled = !data.isMuted;
    }

    private void ApplyVideoTexture(RawImage rawImage, Texture2D videoTexture)
    {
        rawImage.texture = videoTexture;
    }
}
