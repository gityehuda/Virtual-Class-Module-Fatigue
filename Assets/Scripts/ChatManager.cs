using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Binus.WebGL.Service;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public static ChatManager instance;
    [SerializeField] GameObject chatUI;
    [SerializeField] GameObject prefabChatText;
    [SerializeField] GameObject parentChatText;
    [SerializeField] TMP_InputField inputMsg;
    [SerializeField] Button btnChat;
    [SerializeField] Button btnSend;
    [SerializeField] Button btnClose;
    [SerializeField] VerticalLayoutGroup verticalLayoutGroupChatText;

    private Dictionary<string, List<string>> privateMessages = new Dictionary<string, List<string>>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inputMsg.onValueChanged.AddListener(delegate
        {
            DetectEnter();
        });

        inputMsg.onSelect.AddListener(delegate
        {
            OnTextingOn();
        });

        inputMsg.onDeselect.AddListener(delegate
        {
            OnTextingOff();
        });

        btnChat.onClick.AddListener(delegate
        {
            OnChatActive();
        });

        btnSend.onClick.AddListener(delegate
        {
            FunctionSend();
        });

        btnClose.onClick.AddListener(delegate
        {
            OnChatUnActive();
        });
    }

    public void FunctionSend()
    {
        string message = inputMsg.text;

        // Check if the message is empty or contains only white spaces
        if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
        {
            // Tidak melakukan apa-apa jika pesan kosong atau hanya berisi spasi.
            return;
        }

        if (message.StartsWith("/whisper"))
        {
            // Parse the target player's name from the message
            string[] parts = message.Split(' ');
            if (parts.Length >= 3)
            {
                string privateMessageTarget = parts[1];
                message = message.Replace("/whisper " + privateMessageTarget + " ", "");

                // Handle whisper message
                HandleWhisper(privateMessageTarget, message);
            }
            else
            {
                // Tidak melakukan apa-apa jika perintah /whisper tidak valid.
                return;
            }
        }
        else
        {
            // Normal public chat message
            ShowChat("[Me] : " + message);

            string msg = message;

            CustomClass.DataPublicChat dataChat = new CustomClass.DataPublicChat
            {
                nimUsr = MainData.instance.nimUsr,
                nameUsr = MainData.instance.namaUsr,
                message = msg
            };

            PhotonManager.instance.SendData((byte)CustomClass.TypeData.PublicChat, dataChat, Photon.Realtime.ReceiverGroup.Others, Photon.Realtime.EventCaching.DoNotCache);
        }

        inputMsg.text = "";
    }

    public void HandlePublicChat(CustomClass.DataPublicChat data)
    {
        ShowChat("[" + data.nimUsr + " - " + data.nameUsr + "] : " + data.message);
    }

    private void HandleWhisper(string targetNim, string message)
    {
        // Access user data using MainData.instance.nimUsr and MainData.instance.namaUsr

        // Example: Send a whisper message
        string whisperMessage = $"[Whisper to {targetNim}] : {message}";

        CustomClass.DataPrivateChat dataChat = new CustomClass.DataPrivateChat
        {
            nimUsr = MainData.instance.nimUsr,
            nameUsr = MainData.instance.namaUsr,
            targetNim = targetNim,
            message = whisperMessage
        };

        CustomClass.SendData dataModels = new CustomClass.SendData
        {
            type = CustomClass.TypeData.PrivateChat,
            //data = AgoraManager.instance.ChangeModelsToBytes(dataChat)
        };
    }

    public void ShowChat(string msg, bool isPrivate = false)
    {
        var instantiate = Instantiate(prefabChatText);
        instantiate.transform.SetParent(parentChatText.transform);
        instantiate.transform.localScale = new Vector3(1, 1, 1);
        TextMeshProUGUI textMeshPro = instantiate.GetComponent<TextMeshProUGUI>();

        if (isPrivate)
        {
            // Set the text color to indicate it's a private message
            textMeshPro.color = Color.red;
        }

        textMeshPro.text = msg;
        StartCoroutine(UpdateLayoutGroup(verticalLayoutGroupChatText));

        if (parentChatText.transform.childCount > 25)
        {
            Destroy(parentChatText.transform.GetChild(0).gameObject);
        }
    }

    public void ShowPrivateChat(string targetPlayer)
    {
        if (privateMessages.ContainsKey(targetPlayer))
        {
            foreach (string msg in privateMessages[targetPlayer])
            {
                ShowChat(msg, true);
            }
        }
    }


    IEnumerator UpdateLayoutGroup(VerticalLayoutGroup verticalLayoutGroup)
    {
        verticalLayoutGroup.enabled = false;
        yield return new WaitForEndOfFrame();
        verticalLayoutGroup.enabled = true;
    }

    public void DetectEnter()
    {
        if (inputMsg.text.EndsWith("\n"))
        {
            inputMsg.text = inputMsg.text.Remove(inputMsg.text.Length - 1);
            FunctionSend();
        }
        else if (inputMsg.text.Length > 250)
        {
            inputMsg.text = inputMsg.text.Remove(inputMsg.text.Length - 1);
        }
    }

    public void OnChatActive()
    {
        chatUI.SetActive(true);
        btnChat.interactable = false;
    }

    public void OnChatUnActive()
    {
        chatUI.SetActive(false);
        btnChat.interactable = true;
    }

    public void OnTextingOn()
    {
        IPlayerManager playerManager = ServiceLocator.GetService<IPlayerManager>();
        (playerManager as PlayerManager).enabled = false;

        CameraController.instance.enabled = false;
    }

    public void OnTextingOff()
    {
        IPlayerManager playerManager = ServiceLocator.GetService<IPlayerManager>();
        (playerManager as PlayerManager).enabled = true;

        CameraController.instance.enabled = true;
    }
}
