//using agora_gaming_rtc;
using Binus.WebGL.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class BreakoutRoomManager : MonoBehaviour
{
    public static BreakoutRoomManager instance;
    [Header("Creator")] 
    [SerializeField] TMP_InputField inputFieldRoomNameGlobal;
    [SerializeField] TMP_InputField inputFieldMaxParticipantsGlobal;
    [SerializeField] GameObject breakOutRoomParentForCreate;
    [SerializeField] GameObject btnBreakoutRoom;
    [SerializeField] GameObject contentBreakoutRoom;
    [SerializeField] Button btnCreateRoom;
    [SerializeField] Button btnClose;
    [Header("Member")]
    [SerializeField] GameObject breakoutRoomParentMember;
    [SerializeField] GameObject contentBreakoutRoomMember;
    [SerializeField] Button btnCloseMember;
    [Header("Button")]
    [SerializeField] GameObject exampleButton;
    [Header("Icons")]
    [SerializeField] Sprite joinIcon;
    [SerializeField] Sprite deleteIcon;
    [SerializeField] Sprite renameIcon;
    [SerializeField] Sprite confirmIcon;
    [SerializeField] Sprite cancelIcon;
    [Header("Input Field")]
    [SerializeField] GameObject exampleInputField;
    int roomCount = 1;
    GameObject tempRename;

    List<CustomClass.BreakoutRoom> roomList;

    bool isCreated = false;
    bool isGenerated = false;

    string tempApiUrl = "https://func-bion-3dvirtualclassroom-fe-uat.azurewebsites.net/";

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        roomList = new List<CustomClass.BreakoutRoom>();
    }

    private void Start()
    {
        btnBreakoutRoom.SetActive(true);

        if (MainData.instance.roleUsr != "1")
        {
            breakOutRoomParentForCreate.SetActive(false);

            btnBreakoutRoom.GetComponent<Button>().onClick.AddListener(delegate
            {
                OpenBreakoutRoomMember();
            });
        }
        else
        {
            btnBreakoutRoom.GetComponent<Button>().onClick.AddListener(delegate
            {
                OpenBreakoutRoomCreator();
            });
        }

        if (MainData.instance.mode == "dev")
        {
            tempApiUrl = "https://func-bion-3dvirtualclassroom-fe-dev.azurewebsites.net/";
        }

        inputFieldRoomNameGlobal.onValueChanged.AddListener(delegate
        {
            DetectEnterRoomName();
        });

        inputFieldRoomNameGlobal.onSelect.AddListener(delegate
        {
            OnTextingOn();
        });

        inputFieldRoomNameGlobal.onDeselect.AddListener(delegate
        {
            OnTextingOff();
        });

        inputFieldMaxParticipantsGlobal.onValueChanged.AddListener(delegate
        {
            DetectEnterMaxRoom();
        });

        inputFieldMaxParticipantsGlobal.onSelect.AddListener(delegate
        {
            OnTextingOn();
        });

        inputFieldMaxParticipantsGlobal.onDeselect.AddListener(delegate
        {
            OnTextingOff();
        });

        btnCreateRoom.onClick.AddListener(delegate
        {
            CreateRoomList();
        });

        btnClose.onClick.AddListener(delegate
        {
            CloseBreakoutRoomCreator();
        });

        btnCloseMember.onClick.AddListener(delegate
        {
            CloseBreakoutRoomMember();
        });
    }

    public void CreateRoomList()
    {
        if (isCreated == true) return;
        isCreated = true;
        try
        {
            if (inputFieldRoomNameGlobal.text == "" || inputFieldMaxParticipantsGlobal.text == "" || int.Parse(inputFieldMaxParticipantsGlobal.text) > 10) return;
        }
        catch
        {
            return;
        }
        if (roomList.Count > 14)
        {
            IPopUpManager popUpManager = ServiceLocator.GetService<IPopUpManager>();
            popUpManager.OpenPopUp("You have reached the limit of the breakout room quantity");
            return;
        }
        roomCount++;
        StartCoroutine(CreateBreakoutRoom());
    }

    public void FillRoomList(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        var number = 1;
        foreach (var child in roomList)
        {
            var array = number;

            var childObject = new GameObject();
            childObject.transform.SetParent(parent.transform);
            childObject.transform.localScale = Vector3.one;
            childObject.AddComponent<RectTransform>();

            var roomObject = new GameObject();
            roomObject.transform.SetParent(childObject.transform);
            roomObject.transform.localScale = Vector3.one;
            roomObject.transform.localPosition = Vector3.zero;

            var roomObjectRect = roomObject.AddComponent<RectTransform>();
            roomObjectRect.anchorMin = new Vector2(0, 1);
            roomObjectRect.anchorMax = new Vector2(0, 1);
            roomObjectRect.pivot = new Vector2(0, 1);
            roomObjectRect.sizeDelta = new Vector2(250f, 25f);

            var roomText = roomObject.AddComponent<TextMeshProUGUI>();
            roomText.text = number + "\t" + child.name;
            roomText.color = Color.white;
            roomText.fontSize = 14f;
            roomText.verticalAlignment = VerticalAlignmentOptions.Middle;

            var maxRoomObject = new GameObject();
            maxRoomObject.transform.SetParent(childObject.transform);
            maxRoomObject.transform.localScale = Vector3.one;
            maxRoomObject.transform.localPosition = Vector3.zero;

            var maxRoomObjectRect = maxRoomObject.AddComponent<RectTransform>();
            maxRoomObjectRect.anchorMin = new Vector2(0, 1);
            maxRoomObjectRect.anchorMax = new Vector2(0, 1);
            maxRoomObjectRect.pivot = new Vector2(0, 1);
            maxRoomObjectRect.sizeDelta = new Vector2(40f, 25f);
            maxRoomObjectRect.anchoredPosition = new Vector2(280f, 0);

            var maxRoomText = maxRoomObject.AddComponent<TextMeshProUGUI>();
            maxRoomText.text = child.current + "/" + child.max;
            maxRoomText.color = Color.white;
            maxRoomText.fontSize = 14f;
            maxRoomText.verticalAlignment = VerticalAlignmentOptions.Middle;
            maxRoomText.horizontalAlignment = HorizontalAlignmentOptions.Right;

            if (contentBreakoutRoomMember == parent)
            {
                var joinButton = CreateButton(childObject.transform, "Button Join", joinIcon, new Vector2(0, 0), new Vector2(7f, 7f));
                joinButton.onClick.AddListener(delegate
                {
                    //AgoraManager.instance.waitForJoin.SetActive(true);
                    //AgoraManager.instance.background.SetActive(true);
                    StartCoroutine(JoinBreakoutRoom(child.id, child.name, child.max));
                });
            }
            else
            {
                var joinButton = CreateButton(childObject.transform, "Button Join", joinIcon, new Vector2(-60f, 0), new Vector2(7f, 7f));

                var renameButton = CreateButton(childObject.transform, "Button Rename", renameIcon, new Vector2(-30f, 0), new Vector2(7f, 7f));

                var cancelRenameButton = CreateButton(childObject.transform, "Button Rename Cancel", cancelIcon, new Vector2(-30f, 0), new Vector2(7f, 7f));
                cancelRenameButton.gameObject.SetActive(false);

                var confirmRenameButton = CreateButton(childObject.transform, "Button Rename Confirm", confirmIcon, new Vector2(-60f, 0), new Vector2(7f, 7f));
                confirmRenameButton.gameObject.SetActive(false);

                var deleteButton = CreateButton(childObject.transform, "Button Delete", deleteIcon, Vector2.zero, new Vector2(4f, 4f));

                var inputFieldRenameLocal = CreateInputField(childObject.transform, "Input Field", Vector2.zero, new Vector2(270f, 25f));
                inputFieldRenameLocal.lineType = TMP_InputField.LineType.MultiLineNewline;

                var inputFieldMaxLocal = CreateInputField(childObject.transform, "Input Field", new Vector2(280f, 0f), new Vector2(40f, 25f));
                inputFieldMaxLocal.lineType = TMP_InputField.LineType.MultiLineNewline;

                renameButton.onClick.AddListener(delegate
                {
                    if (tempRename != null)
                    {
                        UnActiveRename(tempRename.transform.GetChild(0).gameObject, tempRename.transform.GetChild(7).gameObject, tempRename.transform.GetChild(8).gameObject, tempRename.transform.GetChild(1).gameObject, tempRename.transform.GetChild(2).gameObject, tempRename.transform.GetChild(3).gameObject, tempRename.transform.GetChild(5).gameObject, tempRename.transform.GetChild(4).gameObject);
                    }
                    ActiveRename(roomObject, inputFieldRenameLocal.gameObject, inputFieldMaxLocal.gameObject, maxRoomObject, child.max.ToString(), joinButton.gameObject, renameButton.gameObject, confirmRenameButton.gameObject, cancelRenameButton.gameObject);
                    tempRename = childObject;
                });

                confirmRenameButton.onClick.AddListener(delegate
                {
                    //RenameRoomList(array - 1, inputFieldRename.text);
                    StartCoroutine(EditBreakoutRoom(inputFieldRenameLocal, inputFieldMaxLocal, child.id));
                    tempRename = null;
                });

                cancelRenameButton.onClick.AddListener(delegate
                {
                    UnActiveRename(tempRename.transform.GetChild(0).gameObject, tempRename.transform.GetChild(7).gameObject, tempRename.transform.GetChild(8).gameObject, tempRename.transform.GetChild(1).gameObject, tempRename.transform.GetChild(2).gameObject, tempRename.transform.GetChild(3).gameObject, tempRename.transform.GetChild(5).gameObject, tempRename.transform.GetChild(4).gameObject);
                    tempRename = null;
                });

                joinButton.onClick.AddListener(delegate
                {
                    //AgoraManager.instance.waitForJoin.SetActive(true);
                    //AgoraManager.instance.background.SetActive(true);
                    StartCoroutine(JoinBreakoutRoom(child.id, child.name, child.max));
                });

                deleteButton.onClick.AddListener(delegate
                {
                    //RemoveRoomList(array - 1);
                    StartCoroutine(DeleteBreakoutRoom(child.id));
                    tempRename = null;
                });

                inputFieldMaxLocal.onSelect.AddListener(delegate
                {
                    OnTextingOn();
                });

                inputFieldMaxLocal.onDeselect.AddListener(delegate
                {
                    OnTextingOff();
                });

                inputFieldMaxLocal.onSelect.AddListener(delegate
                {
                    OnTextingOn();
                });

                inputFieldMaxLocal.onDeselect.AddListener(delegate
                {
                    OnTextingOff();
                });

                inputFieldRenameLocal.onValueChanged.AddListener(delegate
                {
                    var value = DetectEnterRename(inputFieldRenameLocal);
                    if (value == true)
                    {
                        //RenameRoomList(array - 1, inputFieldRename.text);
                        inputFieldMaxLocal.Select();
                    }
                });

                inputFieldMaxLocal.onValueChanged.AddListener(delegate
                {
                    tempRename = null;
                });
            }

            number++;
        }
    }

    public Button CreateButton(Transform parentObject, string nameOfGameObject, Sprite icon, Vector2 position, Vector2 offSetIconScale)
    {
        var go = Instantiate(exampleButton);
        go.name = nameOfGameObject;
        go.transform.SetParent(parentObject);
        go.transform.localScale = Vector3.one;

        var goRect = go.GetComponent<RectTransform>();
        goRect.anchorMin = new Vector2(1f, 0.5f);
        goRect.anchorMax = new Vector2(1f, 0.5f);
        goRect.pivot = new Vector2(1f, 0.5f);
        goRect.anchoredPosition = position;
        goRect.sizeDelta = new Vector2(25f, 25f);

        var childGORect = go.transform.GetChild(0).transform.GetComponent<RectTransform>();
        childGORect.offsetMin = offSetIconScale;
        childGORect.offsetMax = offSetIconScale * -1f;

        var childImage = go.transform.GetChild(0).GetComponent<Image>();
        childImage.sprite = icon;

        return go.GetComponent<Button>();
    }

    public TMP_InputField CreateInputField(Transform parentObject, string nameOfObject, Vector2 position, Vector2 size)
    {
        var go = Instantiate(exampleInputField);
        go.name = nameOfObject;
        go.transform.SetParent(parentObject);
        go.transform.localScale = Vector3.one;

        var goRect = go.GetComponent<RectTransform>();
        goRect.anchorMin = new Vector2(0, 0.5f);
        goRect.anchorMax = new Vector2(0, 0.5f);
        goRect.pivot = new Vector2(0, 0.5f);
        goRect.sizeDelta = size;
        goRect.anchoredPosition = position;

        go.SetActive(false);

        return go.GetComponent<TMP_InputField>();
    }

    public void OpenBreakoutRoomCreator()
    {
        if (isGenerated == true) return;
        isGenerated = true;
        GameplayManager.instance.ActivateWindow(WindowList.BreakoutRoom);
        btnBreakoutRoom.GetComponent<Button>().interactable = false;
        AutoFillRoomName();
        StartCoroutine(GetBreakoutRoom(contentBreakoutRoom, breakOutRoomParentForCreate));
    }

    public void CloseBreakoutRoomCreator()
    {
        GameplayManager.instance.DeActivateWindow();
        btnBreakoutRoom.GetComponent<Button>().interactable = true;
        breakOutRoomParentForCreate.SetActive(false);
    }

    public void OpenBreakoutRoomMember()
    {
        GameplayManager.instance.ActivateWindow(WindowList.BreakoutRoom);
        StartCoroutine(GetBreakoutRoom(contentBreakoutRoomMember, breakoutRoomParentMember));
    }

    public void CloseBreakoutRoomMember()
    {
        GameplayManager.instance.DeActivateWindow();
        breakoutRoomParentMember.SetActive(false);
    }

    public void AutoFillRoomName()
    {
        inputFieldRoomNameGlobal.text = "Room #" + roomCount;
        inputFieldMaxParticipantsGlobal.text = "10";
    }

    public void DetectEnterRoomName()
    {
        if (inputFieldRoomNameGlobal.text.EndsWith("\n"))
        {
            inputFieldRoomNameGlobal.text = inputFieldRoomNameGlobal.text.Remove(inputFieldRoomNameGlobal.text.Length - 1);
            inputFieldMaxParticipantsGlobal.Select();
        }
        else if (inputFieldRoomNameGlobal.text.Length > 20)
        {
            inputFieldRoomNameGlobal.text = inputFieldRoomNameGlobal.text.Remove(inputFieldRoomNameGlobal.text.Length - 1);
        }
    }

    public void DetectEnterMaxRoom()
    {
        try
        {
            if (inputFieldMaxParticipantsGlobal.text.EndsWith("\n"))
            {
                inputFieldMaxParticipantsGlobal.text = inputFieldMaxParticipantsGlobal.text.Remove(inputFieldMaxParticipantsGlobal.text.Length - 1);
                CreateRoomList();
            }
            else if (int.Parse(inputFieldMaxParticipantsGlobal.text) > 10)
            {
                inputFieldMaxParticipantsGlobal.text = inputFieldMaxParticipantsGlobal.text.Remove(inputFieldMaxParticipantsGlobal.text.Length - 1);
            }
        }
        catch
        {
            try
            {
                inputFieldMaxParticipantsGlobal.text = inputFieldMaxParticipantsGlobal.text.Remove(inputFieldMaxParticipantsGlobal.text.Length - 1);
            }
            catch
            {

            }
        }
    }

    public void DetectEnterEditMaxRoom()
    {
        try
        {
            if (inputFieldMaxParticipantsGlobal.text.EndsWith("\n"))
            {
                inputFieldMaxParticipantsGlobal.text = inputFieldMaxParticipantsGlobal.text.Remove(inputFieldMaxParticipantsGlobal.text.Length - 1);
                //CreateRoomList();
            }
            else if (int.Parse(inputFieldMaxParticipantsGlobal.text) > 10)
            {
                inputFieldMaxParticipantsGlobal.text = inputFieldMaxParticipantsGlobal.text.Remove(inputFieldMaxParticipantsGlobal.text.Length - 1);
            }
        }
        catch
        {
            try
            {
                inputFieldMaxParticipantsGlobal.text = inputFieldMaxParticipantsGlobal.text.Remove(inputFieldMaxParticipantsGlobal.text.Length - 1);
            }
            catch
            {

            }
        }
    }

    public void ActivateBreakoutRoomCreate()
    {
        breakOutRoomParentForCreate.SetActive(true);
    }

    public void OnTextingOn()
    {
        IPlayerManager playerManager = ServiceLocator.GetService<IPlayerManager>();
        (playerManager as PlayerManager).enabled = false;
    }

    public void OnTextingOff()
    {
        IPlayerManager playerManager = ServiceLocator.GetService<IPlayerManager>();
        (playerManager as PlayerManager).enabled = true;
    }

    //private void RenameRoomList(int array, string newValue)
    //{
    //    MainData.instance.roomList[array].roomName = newValue;
    //    FillRoomList(contentBreakoutRoom);

    //    AgoraManager.instance.isUpdate = true;
    //    StartCoroutine(SendData());
    //}

    //private void RemoveRoomList(int array)
    //{
    //    MainData.instance.roomList.RemoveAt(array);
    //    FillRoomList(contentBreakoutRoom);

    //    AgoraManager.instance.isUpdate = true;
    //    StartCoroutine(SendData());
    //}

    private void ActiveRename(GameObject roomObjectText, GameObject inputFieldRename, GameObject inputFieldMax, GameObject maxText, string max, GameObject btnJoin, GameObject btnRename, GameObject btnConfirm, GameObject btnCancel)
    {
        roomObjectText.SetActive(false);
        maxText.SetActive(false);

        inputFieldRename.SetActive(true);
        inputFieldMax.SetActive(true);

        var inputField = inputFieldRename.GetComponent<TMP_InputField>();
        inputField.text = roomObjectText.GetComponent<TextMeshProUGUI>().text.Substring(roomObjectText.GetComponent<TextMeshProUGUI>().text.LastIndexOf("\t") + 1);

        inputFieldMax.GetComponent<TMP_InputField>().text = max;

        btnJoin.GetComponent<RectTransform>().anchoredPosition = new Vector2(-90f, 0);

        btnRename.SetActive(false);
        btnConfirm.SetActive(true);
        btnCancel.SetActive(true);
    }

    private void UnActiveRename(GameObject roomObjectText, GameObject inputFieldRename, GameObject inputFieldMax, GameObject maxText, GameObject btnJoin, GameObject btnRename, GameObject btnConfirm, GameObject btnCancel)
    {
        roomObjectText.SetActive(true);

        inputFieldRename.SetActive(false);
        inputFieldMax.SetActive(false);

        btnJoin.GetComponent<RectTransform>().anchoredPosition = new Vector2(-60f, 0);

        maxText.SetActive(true);
        btnRename.SetActive(true);
        btnConfirm.SetActive(false);
        btnCancel.SetActive(false);
    }

    private bool DetectEnterRename(TMP_InputField inputField)
    {
        if (inputField.text.EndsWith("\n"))
        {
            inputField.text = inputField.text.Remove(inputField.text.Length - 1);
            return true;
        }
        else if (inputField.text.Length > 20)
        {
            inputField.text = inputField.text.Remove(inputField.text.Length - 1);
        }
        return false;
    }

    private IEnumerator JoinBreakoutRoom(string breakoutRoomId, string breakoutRoomName, int maxParticipants)
    {
        using (UnityWebRequest web = UnityWebRequest.PostWwwForm(tempApiUrl + "api/JoinBreakoutRoom", ""))
        {
            web.SetRequestHeader("breakoutRoomId", breakoutRoomId);
            web.SetRequestHeader("externalSystemId", MainData.instance.nimUsr);
            web.SetRequestHeader("binusianId", MainData.instance.binusianId);

            yield return web.SendWebRequest();

            if (web.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(web.error);
            }
            else
            {
                //IRtcEngine.Destroy();
                var response = JsonUtility.FromJson<CustomClass.ResponseJoinBreakoutRoom>(web.downloadHandler.text);
                MainData.instance.breakoutRoomId = breakoutRoomId;
                MainData.instance.breakoutRoomName = breakoutRoomName;
                MainData.instance.breakoutRoomParticipantsId = response.breakoutRoomParticipantsId;
                MainData.instance.breakoutRoomParticipantsMaxNumber = maxParticipants;
                MainData.instance.roomName = "DiscussionRoom";
                SceneManager.LoadScene("TransitionScene");
            }
        }
    }

    public IEnumerator LeaveBreakoutRoom()
    {
        using (UnityWebRequest web = UnityWebRequest.PostWwwForm(tempApiUrl + "api/LeaveBreakoutRoom", ""))
        {
            web.SetRequestHeader("breakoutRoomParticipantsId", MainData.instance.breakoutRoomParticipantsId);
            web.SetRequestHeader("binusianId", MainData.instance.binusianId);

            yield return web.SendWebRequest();

            if (web.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(web.error);
            }
            else
            {
                //IRtcEngine.Destroy();
                SceneManager.LoadScene("TransitionScene");
                MainData.instance.breakoutRoomId = "";
            }
        }
    }

    IEnumerator CreateBreakoutRoom()
    {
        using (UnityWebRequest web = UnityWebRequest.PostWwwForm(tempApiUrl + "api/CreateBreakoutRoom", ""))
        { 
            web.SetRequestHeader("binusianId", MainData.instance.binusianId);
            web.SetRequestHeader("nameOfRoom", inputFieldRoomNameGlobal.text);
            web.SetRequestHeader("sessionId", MainData.instance.sessionId);
            web.SetRequestHeader("maxParticipants", inputFieldMaxParticipantsGlobal.text);
            yield return web.SendWebRequest();

            if (web.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(web.error);
            }
            else
            {
                AutoFillRoomName();
                StartCoroutine(GetBreakoutRoom(contentBreakoutRoom, breakOutRoomParentForCreate));
            }
        }
    }

    IEnumerator EditBreakoutRoom(TMP_InputField inputNameOfRoomLocal, TMP_InputField inputMaxParticipantsLocal, string breakoutRoomId)
    {
        using (UnityWebRequest web = UnityWebRequest.PostWwwForm(tempApiUrl + "api/EditBreakoutRoom", ""))
        {
            web.SetRequestHeader("binusianId", MainData.instance.binusianId);
            web.SetRequestHeader("nameOfRoom", inputNameOfRoomLocal.text);
            web.SetRequestHeader("breakoutRoomId", breakoutRoomId);
            web.SetRequestHeader("maxParticipants", inputMaxParticipantsLocal.text);
            yield return web.SendWebRequest();

            if (web.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(web.error);
            }
            else
            {
                StartCoroutine(GetBreakoutRoom(contentBreakoutRoom, breakOutRoomParentForCreate));
            }
        }
    }

    IEnumerator DeleteBreakoutRoom(string id)
    {
        using (UnityWebRequest web = UnityWebRequest.PostWwwForm(tempApiUrl + "api/DeleteBreakoutRoom", ""))
        {
            web.SetRequestHeader("breakoutRoomId", id);
            web.SetRequestHeader("binusianId", MainData.instance.binusianId);

            yield return web.SendWebRequest();

            if (web.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(web.error);
            }
            else
            {
                StartCoroutine(GetBreakoutRoom(contentBreakoutRoom, breakOutRoomParentForCreate));
            }
        }
    }

    IEnumerator GetBreakoutRoom(GameObject content, GameObject activeObject)
    {
        using (UnityWebRequest web = UnityWebRequest.PostWwwForm(tempApiUrl + "api/GetBreakoutRoom", ""))
        {
            web.SetRequestHeader("sessionId", MainData.instance.sessionId);

            yield return web.SendWebRequest();

            Debug.Log(web.responseCode);
            if (web.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(web.error);
            }
            else
            {
                isCreated = false;
                isGenerated = false;
                var response = JsonUtility.FromJson<CustomClass.ResponseBreakoutRoom>(web.downloadHandler.text);
                roomList = response.data;
                activeObject.SetActive(true);
                FillRoomList(content);
            }
        }
    }
}
