using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainData : MonoBehaviour
{
    public static MainData instance;
    public string courseUsr;
    public string classUsr;
    public string startUsr;
    public string endUsr;
    public string namaUsr;
    public string nimUsr;
    public string apiMainUrl;    
    public string roleUsr;
    public string mode;
    public string sessionId;
    public string binusianId;
    public string roomName;
    public string breakoutRoomParticipantsId;
    public int breakoutRoomParticipantsMaxNumber;
    public string acadCareer = "OS1";
    public string institution = "BNS01";

    public string agoraAppId;
    public string agoraMainToken;
    public string breakoutRoomId = "";
    public string breakoutRoomName = "";
    public string sexUsr;

    //public int virtualLabPJJTDModule = 1;

    public List<string> pathFileMain = new List<string>();

    private void Awake()
    {
        instance = this;

        mode = "dev";
        apiMainUrl = "https://func-bion-3dvirtualclassroom-fe-dev.azurewebsites.net/";
        agoraAppId = "067468506b6644068e91a17af496c230";
    }
}
