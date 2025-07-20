using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] GameObject btnAudio;
    [SerializeField] Sprite audioMuted;
    [SerializeField] Sprite audioUnMuted;

    [Header("Pop Up Unmute")]
    [SerializeField] GameObject popUpUnmute;
    [SerializeField] Button btnUnmute;
    [SerializeField] Button btnStayMuted;

    Image imageAudio;
    [HideInInspector] public bool isAudio = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        popUpUnmute.SetActive(false);
        imageAudio = btnAudio.transform.GetChild(0).GetComponent<Image>();
        imageAudio.sprite = audioMuted;

        btnAudio.GetComponent<Button>().onClick.AddListener(delegate
        {
            AudioMutedOrUnmuted();
        });

        btnStayMuted.onClick.AddListener(delegate
        {
            popUpUnmute.SetActive(false);
        });

        btnUnmute.onClick.AddListener(delegate
        {
            AudioMutedOrUnmuted();

            popUpUnmute.SetActive(false);
        });
    }

    void AudioMutedOrUnmuted()
    {
        isAudio = !isAudio;
        imageAudio.sprite = isAudio ? audioUnMuted : audioMuted;
        PhotonManager.instance.photonVoiceView.RecorderInUse.TransmitEnabled = isAudio;

        var dataSync = new CustomClass.DataSyncAudioAndShare
        {
            actorNumberId = PhotonNetwork.LocalPlayer.ActorNumber,
            isAudio = AudioManager.instance.isAudio,
            isShare = ShareScreenManager.instance.isScreenAllowed,
        };

        PhotonManager.instance.SendData((byte)CustomClass.TypeData.SyncAudioAndShare, dataSync);
    }

    public void HandleGiveMuteAll(CustomClass.DataGiveMute data)
    {
        isAudio = data.isAudio;
        imageAudio.sprite = isAudio ? audioUnMuted : audioMuted;
        PhotonManager.instance.photonVoiceView.RecorderInUse.TransmitEnabled = isAudio;
    }

    public void HandleGiveMuteUnMuteTarget(CustomClass.DataGiveMute data)
    {
        if (data.actorNumberId != PhotonNetwork.LocalPlayer.ActorNumber) return;
        if(isAudio == true)
        {
            AudioMutedOrUnmuted();
        }
        else
        {
            popUpUnmute.SetActive(true);
        }
    }
}
