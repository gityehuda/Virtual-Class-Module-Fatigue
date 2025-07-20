using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InBreakoutRoomManager : MonoBehaviour
{
    public static InBreakoutRoomManager instance;

    [SerializeField] Button buttonOutBreakoutroom;
    [SerializeField] TextMeshProUGUI textBreakoutRoomName;

    string tempApiUrl = "https://func-bion-3dvirtualclassroom-fe-uat.azurewebsites.net/";

    private void Awake()
    {
        instance = this;

        if(MainData.instance.mode == "dev")
        {
            tempApiUrl = "https://func-bion-3dvirtualclassroom-fe-dev.azurewebsites.net/";
        }
    }

    private void Start()
    {
        textBreakoutRoomName.text = MainData.instance.breakoutRoomName;
        buttonOutBreakoutroom.onClick.AddListener(delegate
        {
            MainData.instance.roomName = "CreativeClass";
            StartCoroutine(LeaveBreakoutRoom());
        });
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
                MainData.instance.breakoutRoomId = "";
                SceneManager.LoadScene("TransitionScene");
            }
        }
    }
}
