using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ExitGames.Client.Photon;
using static CustomClass;
using Photon.Voice.PUN;
using System;
using TMPro;
using System.Linq;
using Binus.WebGL.Service;

public class PhotonManager : MonoBehaviourPunCallbacks, IOnEventCallback, IInRoomCallbacks
{
    public static PhotonManager instance;

    [HideInInspector] public PhotonView m_photonView;
    [HideInInspector] public PhotonVoiceView photonVoiceView;

    public GameObject UIWaitForJoin;
    public GameObject UIWaitFoReconnecting;

    [SerializeField] TextMeshProUGUI roomNameText;
    [SerializeField] TextMeshProUGUI roomCapacity;

    private float timer = 0.0f;
    private float interval = 5.0f;

    bool isReconnect = false;
    bool isJoined = false;
    Vector3 tempMyHumanPosition = Vector3.zero;

    public RaiseEventOptions raiseEventOptions = new RaiseEventOptions
    {
        Receivers = ReceiverGroup.Others,
        CachingOption = EventCaching.AddToRoomCache
    };

    public SendOptions sendOptions = new SendOptions
    {
        Reliability = true
    };

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // Increment the timer by the time passed since the last frame
        timer += Time.deltaTime;

        // Check if the timer has reached or exceeded the interval
        if (timer >= interval)
        {
            if (roomCapacity != null)
            {
                roomCapacity.text = PhotonNetwork.CountOfPlayersInRooms.ToString() + " / " + MainData.instance.breakoutRoomParticipantsMaxNumber;
            }

            if (isJoined && !PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Disconnect();
            }

            // Reset the timer
            timer = 0.0f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        WhiteboardManager.instance.isWhiteboardActive = true;
        UIWaitForJoin.SetActive(true);
        UIWaitFoReconnecting.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        if (MainData.instance.mode == "dev" && MainData.instance.sessionId == "") MainData.instance.sessionId = "efa8c515-b0be-4f7d-b347-e95008f30d44";
        var roomId = "";
        if (MainData.instance.breakoutRoomId == "") roomId = MainData.instance.sessionId;
        else roomId = MainData.instance.breakoutRoomId;

        PhotonNetwork.JoinOrCreateRoom(roomId, new RoomOptions() { MaxPlayers = 0 }, null);

        if (roomNameText != null) roomNameText.text = roomId;

        isReconnect = false;
    }

    public override void OnJoinedRoom()
    {
        WhiteboardManager.instance.isWhiteboardActive = false;
        UIWaitForJoin.SetActive(false);
        UIWaitFoReconnecting.SetActive(false);
        GameplayManager.instance.CreateMyHuman(tempMyHumanPosition == Vector3.zero ? Vector3.zero : tempMyHumanPosition);
        isJoined = true;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (ShareScreenManager.instance.isScreenShared && ShareScreenManager.instance.actorNumberIdShare == otherPlayer.ActorNumber)
        {
            ShareScreenManager.instance.HandleStopShareScreen(new DataStopShareScreen());
        }

        var user = GameplayManager.instance.listOfUser[otherPlayer.ActorNumber];
        Destroy(user.decoder);
        ChatManager.instance.ShowChat(user.externalSystemId + " - " + user.name + " has leaved");

        GameplayManager.instance.listOfUser.Remove(otherPlayer.ActorNumber);
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        SendDataSync(0, player.ActorNumber);
    }

    private void SendDataSync(int array, int actorNumber)
    {
        if (array % 50 == 0)
        {
            StartCoroutine(GetRestSync(array + 1, actorNumber));
        }
        else
        {
            try
            {
                var dataSend = new DataSyncWhiteboard(actorNumber, WhiteboardManager.instance.allDataWhiteboards.ElementAt(array));

                PhotonManager.instance.SendData((byte)CustomClass.TypeData.SyncWhiteboard, dataSend, Photon.Realtime.ReceiverGroup.Others, Photon.Realtime.EventCaching.DoNotCache);

                SendDataSync(array + 1, actorNumber);
            }
            catch
            {

            }
        }
    }

    private IEnumerator GetRestSync(int array, int actorNumber)
    {
        yield return new WaitForSeconds(2f);

        try
        {
            var dataSend = new DataSyncWhiteboard(actorNumber, WhiteboardManager.instance.allDataWhiteboards.ElementAt(array));

            PhotonManager.instance.SendData((byte)CustomClass.TypeData.SyncWhiteboard, dataSend, Photon.Realtime.ReceiverGroup.Others, Photon.Realtime.EventCaching.DoNotCache);

            SendDataSync(array + 1, actorNumber);
        }
        catch
        {

        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        isJoined = false;
        isReconnect = true;
        WhiteboardManager.instance.isWhiteboardActive = true;

        tempMyHumanPosition = GameplayManager.instance.myHuman.transform.localPosition;

        Destroy(GameplayManager.instance.myHuman);
        GameplayManager.instance.myHuman = null;
        foreach (var child in GameplayManager.instance.listOfUser)
        {
            Destroy(child.Value.decoder);
            Destroy(child.Value.human);
        }

        UIWaitFoReconnecting.SetActive(true);
        StartCoroutine(Reconnecting());
    }

    IEnumerator Reconnecting()
    {
        yield return new WaitForSeconds(15f);

        if (isReconnect)
        {
            PhotonNetwork.ConnectUsingSettings();

            StartCoroutine(Reconnecting());
        }
    }

    public byte[] ChangeModelsToBytes(object data)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (MemoryStream memoryStream = new MemoryStream())
        {
            binaryFormatter.Serialize(memoryStream, data);
            return memoryStream.ToArray();
        }
    }


    public object ChangeBytesToModels(byte[] data)
    {
        using (var memStream = new MemoryStream())
        {
            var binForm = new BinaryFormatter();
            memStream.Write(data, 0, data.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = binForm.Deserialize(memStream);
            return obj;
        }
    }

    public void SendData(byte typeData, object data, ReceiverGroup receiver = ReceiverGroup.Others, EventCaching eventCaching = EventCaching.AddToRoomCache)
    {
        raiseEventOptions.Receivers = receiver;
        raiseEventOptions.CachingOption = eventCaching;
        var byteData = ChangeModelsToBytes(data);
        PhotonNetwork.RaiseEvent(typeData, byteData, raiseEventOptions, sendOptions);
    }

    public void SendDataFaceCam(byte[] data, int width, int height)
    {
        CustomClass.DataFaceCam dataFaceCam = new CustomClass.DataFaceCam
        {
            actorNumberId = PhotonNetwork.LocalPlayer.ActorNumber,
            image = data,
            width = width,
            height = height,
        };
        RaiseEventOptions raiseEventOptionsLocal = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.DoNotCache
        };
        var byteData = ChangeModelsToBytes(dataFaceCam);
        PhotonNetwork.RaiseEvent((byte)CustomClass.TypeData.FaceCam, byteData, raiseEventOptionsLocal, sendOptions);
    }

    public void SendDataShareScreen(byte[] data, int width, int height)
    {
        CustomClass.DataShareScreen dataShareScreen = new CustomClass.DataShareScreen
        {
            actorNumberId = PhotonNetwork.LocalPlayer.ActorNumber,
            image = data,
            width = width,
            height = height,
            dateTime = DateTime.Now,
        };
        RaiseEventOptions raiseEventOptionsLocal = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.DoNotCache
        };
        var byteData = ChangeModelsToBytes(dataShareScreen);
        PhotonNetwork.RaiseEvent((byte)CustomClass.TypeData.ShareScreen, byteData, raiseEventOptionsLocal, sendOptions);

    }

    public void OnEvent(EventData photonEvent)
    {
        try
        {
            if (photonEvent.Code == (byte)CustomClass.TypeData.NewUserJoin)
            {
                GameplayManager.instance.HandleNewHuman((CustomClass.DataNewUserJoin)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }
            else if (photonEvent.Code == (byte)CustomClass.TypeData.PublicChat)
            {
                ChatManager.instance.HandlePublicChat((CustomClass.DataPublicChat)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }
            else if (photonEvent.Code == (byte)CustomClass.TypeData.Whiteboard)
            {
                WhiteboardManager.instance.HandleWhiteBoard((CustomClass.DataWhiteboard)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }
            else if (photonEvent.Code == (byte)CustomClass.TypeData.FaceCam)
            {
                FaceCamManager.instance.HandleFaceCam((CustomClass.DataFaceCam)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }
            else if (photonEvent.Code == (byte)CustomClass.TypeData.StartStopFaceCam)
            {
                FaceCamManager.instance.HandleStartStopFaceCam((CustomClass.DataStartStopFaceCam)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }
            else if (photonEvent.Code == (byte)CustomClass.TypeData.ShareScreen)
            {
                ShareScreenManager.instance.HandleShareScreen((CustomClass.DataShareScreen)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }
            else if (photonEvent.Code == (byte)CustomClass.TypeData.StartShareScreen)
            {
                ShareScreenManager.instance.HandleStartShareScreen((CustomClass.DataStartShareScreen)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }
            else if (photonEvent.Code == (byte)CustomClass.TypeData.StopShareScreen)
            {
                ShareScreenManager.instance.HandleStopShareScreen((CustomClass.DataStopShareScreen)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }
            else if (photonEvent.Code == (byte)CustomClass.TypeData.ForceStopShare)
            {
                ShareScreenManager.instance.HandleForceStopShare();
            }
            else if (photonEvent.Code == (byte)CustomClass.TypeData.GiveShareAll)
            {
                ShareScreenManager.instance.HandleGiveShareAll((CustomClass.DataGiveShare)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }
            else if (photonEvent.Code == (byte)CustomClass.TypeData.GiveShareTarget)
            {
                ShareScreenManager.instance.HandleGiveShareTarget((CustomClass.DataGiveShare)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }
            else if (photonEvent.Code == (byte)CustomClass.TypeData.SyncAudioAndShare)
            {
                ListOfUserManager.instance.HandleSyncAudioAndShare((CustomClass.DataSyncAudioAndShare)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }
            else if (photonEvent.Code == (byte)CustomClass.TypeData.GiveMuteAll)
            {
                AudioManager.instance.HandleGiveMuteAll((CustomClass.DataGiveMute)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }

            else if (photonEvent.Code == (byte)CustomClass.TypeData.GiveMuteUnMuteTarget)
            {
                AudioManager.instance.HandleGiveMuteUnMuteTarget((CustomClass.DataGiveMute)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }

            else if (photonEvent.Code == (byte)CustomClass.TypeData.SyncWhiteboard)
            {
                var data = (DataSyncWhiteboard)ChangeBytesToModels((byte[])photonEvent.CustomData);
                if (data.actorNumber != PhotonNetwork.LocalPlayer.ActorNumber) return;
                WhiteboardManager.instance.HandleSyncWhiteboard(data.data);
            }
            else if (photonEvent.Code == (byte)TypeData.CharacterSit)
            {
                ISittingManager sittingManager = ServiceLocator.GetService<ISittingManager>();

                sittingManager.HandleOtherCharacterSit((DataCharacterSit)ChangeBytesToModels((byte[])photonEvent.CustomData));
            }
            else if (photonEvent.Code == (byte)TypeData.NeedStudentSit)
            {
                DataNeedStudentSit data = (DataNeedStudentSit)ChangeBytesToModels((byte[])photonEvent.CustomData);

                ISittingManager sittingManager = ServiceLocator.GetService<ISittingManager>();
                sittingManager.HandleNeedStudentChair(data);
            }
        }
        catch
        {

        }
    }
}