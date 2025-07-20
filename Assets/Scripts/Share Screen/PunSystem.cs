using FMETP;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PunSystem : MonoBehaviour
{
    public PhotonView photonView;
    public GameViewDecoder gameViewDecoder;
    public RawImage videoDisplay;

    private void Start()
    {
        if (videoDisplay != null)
        {
            videoDisplay.gameObject.SetActive(false);
        }
    }

    public void OnShareScreenButtonPressed()
    {
        if (videoDisplay != null)
        {
            videoDisplay.gameObject.SetActive(true);
        }

    }

    public void OnCloseButtonPressed()
    {
        if (videoDisplay != null)
        {
            videoDisplay.gameObject.SetActive(false);
        }
    }

    public void SendMessage(byte[] byteData, string message)
    {
        photonView.RPC("RPC_SendMessage", RpcTarget.All, byteData, message);
    }

    [PunRPC]
    private void RPC_SendMessage(byte[] byteData, string message)
    {
        if (message.Contains("VideoShare"))
        {
            gameViewDecoder.Action_ProcessImageData(byteData);
        }
    }
}
