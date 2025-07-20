using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomClass : MonoBehaviour
{
    [Serializable]
    public class Vector3Models
    {
        public Vector3Models(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }

    [Serializable]
    public class QuaternionModels
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public QuaternionModels(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }

    [Serializable]
    public class Color32Models
    {
        byte r;
        byte g;
        byte b;
        byte a;

        public Color32Models(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }

    [Serializable]
    public class SendData
    {
        public TypeData type { get; set; }
        public byte[] data { get; set; }
    }

    [Serializable]
    public class DataPublicChat
    {
        public string nimUsr { get; set; }
        public string nameUsr { get; set; }
        public string message { get; set; }
    }

    [Serializable] public class DataPrivateChat
    { 
        public string nimUsr { get; set; }
        public string nameUsr { get; set; }
        public string message { get; set; }
        public string targetNim { get; set; } 
    }

    [Serializable]
    public class DataNewUserJoin
    {
        public Vector3Models position { get; set; }
        public QuaternionModels rotation { get; set; }
        public int actorNumberId { get; set; }
        public int viewId { get; set; }
        public bool isAudio { get; set; }
        public bool isShare { get; set; }
        public string nimUsr { get; set; }
        public string nameUsr { get; set; }
        public string roleUsr { get; set; }
        public string sexUsr { get; set; }
    }

    [Serializable]
    public class DataFaceCam
    {
        public int actorNumberId;
        public byte[] image;
        public int width;
        public int height;
    }

    [Serializable]
    public class DataStartStopFaceCam
    {
        public int actorNumberId;
        public bool isMuted;
    }

    [Serializable]
    public class DataWhiteboard
    {
        public Guid idData;
        public WhiteboardDataModels[] whiteboardData;

        public DataWhiteboard(Guid idData, WhiteboardDataModels[] whiteboardData)
        {
            this.idData = idData;
            this.whiteboardData = whiteboardData;
        }
    }

    [Serializable]
    public class DataAudio
    {
        public int actorNumberId;
        public bool isAudio;
    }

    [Serializable]
    public class DataGiveShare
    {
        public int actorNumberId;
        public bool isShare;
    }

    [Serializable]
    public class DataGiveMute
    {
        public int actorNumberId;
        public bool isAudio;
    }

    [Serializable]
    public class DataShareScreen
    {
        public int actorNumberId;
        public byte[] image;
        public int width;
        public int height;
        public DateTime dateTime;
    }

    [Serializable]
    public class DataStartShareScreen
    {
        public int actorNumberId;
    }

    [Serializable]
    public class DataTestStartShareScreen
    {
        public string data;
    }

    [Serializable]
    public class DataStopShareScreen
    {
        public int actorNumberId;
    }

    [Serializable]
    public class DataSyncAudioAndShare
    {
        public int actorNumberId;
        public bool isAudio;
        public bool isShare;
    }

    [Serializable]
    public class BreakoutRoom
    {
        public string id;
        public string name;
        public int current;
        public int max;
    }

    [Serializable]
    public class ResponseBreakoutRoom
    {
        public List<BreakoutRoom> data;
    }

    [Serializable]
    public class ResponseLoadSessionToken
    {
        public bool isJoin;
        public string agoraToken;
        public string sessionId;
        public string externalSystemId;
        public string name;
        public string role;
        public string sex;
    }

    [Serializable]
    public class ResponseJoinBreakoutRoom
    {
        public string breakoutRoomParticipantsId;
        public string agoraToken;
    }

    [Serializable]
    public class ResponseLoginAAD
    {
        public string email;
        public string emplId;
        public bool isVirtualLabAccess;
    }

    [Serializable]
    public class WhiteboardDataModels
    {
        public float startX;
        public float startY;
        public float endX;
        public float endY;
        public float colorR;
        public float colorG;
        public float colorB;
        public float colorA;
        public float bottomLeftX;
        public float bottomLeftY;
        public float bottomLeftZ;
        public float topRightX;
        public float topRightY;
        public float topRightZ;
        public int whichWhiteboard;
        public bool isEraser;

        public WhiteboardDataModels(float startX, float startY, float endX, float endY, float colorR, float colorG, float colorB, float colorA, float bottomLeftX, float bottomLeftY, float bottomLeftZ, float topRightX, float topRightY, float topRightZ, int whichWhiteboard, bool isEraser)
        {
            this.startX = startX;
            this.startY = startY;
            this.endX = endX;
            this.endY = endY;
            this.colorR = colorR;
            this.colorG = colorG;
            this.colorB = colorB;
            this.colorA = colorA;
            this.bottomLeftX = bottomLeftX;
            this.bottomLeftY = bottomLeftY;
            this.bottomLeftZ = bottomLeftZ;
            this.topRightX = topRightX;
            this.topRightY = topRightY;
            this.topRightZ = topRightZ;
            this.whichWhiteboard = whichWhiteboard;
            this.isEraser = isEraser;
        }
    }

    [Serializable]
    public class Vector2Models
    {
        public Vector2Models(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float x;
        public float y;
    }

    [Serializable]
    public class User
    {
        public GameObject decoder;
        public GameObject human;
        public string externalSystemId;
        public string name;
        public bool isAudio;
        public bool isShare;
        public CharacterGesture characterGesture;
    }

    [Serializable]
    public class GetHomeResponseDTO
    {
        public int roleId;
        public string role;
        public string externalSystemId;
        public string acadCareer;
        public string institution;
        public List<STRMDesc> strm;
        public List<GetTableResponseDTO> tableData;
        public string now;
    }

    [Serializable]
    public class STRMDesc
    {
        public string strm;
        public string desc;
        public string beginDt;
        public string endDt;
    }

    [Serializable]
    public class GetTableResponseDTO
    {
        public string courseCode;
        public string courseTitle ;
        public string classSection ;
        public string classType;
        public string startDate;
        public string meetingTimeStart ;
        public string meetingTimeEnd ;
    }

    [Serializable]
    public class DataSyncWhiteboard
    {
        public int actorNumber;
        public KeyValuePair<Guid, WhiteboardDataModels[]> data;

        public DataSyncWhiteboard(int actorNumber, KeyValuePair<Guid, WhiteboardDataModels[]> data)
        {
            this.actorNumber = actorNumber;
            this.data = data;
        }
    }

    [Serializable]
    public class GetTableOutputDTO
    {
        public List<GetTableResponseDTO> data;
    }

    [Serializable]
    public class DataCharacterSit
    {
        public int actorNumber;
        public int idChairs;
        public bool isSit;
    }

    [Serializable]
    public class DataNeedStudentSit
    {
        public int actorNumber;
        public int idChairs;
    }

    // Start is called before the first frame update
    public enum TypeData
    {
        None,
        NewUserJoin,
        PublicChat,
        MakeHuman,
        HumanMoving,
        Whiteboard,
        ShareScreen,
        StartShareScreen,
        StopShareScreen,
        targetPlayer,
        PrivateChat,
        GiveMuteAll,
        GiveMuteUnMuteTarget,
        GiveShareAll,
        GiveShareTarget,
        ForceStopShare,
        FaceCam,
        StartStopFaceCam,
        TestShareScreen,
        SyncAudioAndShare,
        SyncWhiteboard,
        CharacterSit,
        NeedStudentSit,
    }

    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public enum CharacterGesture
    {
        Idle = 1,
        Walk = 2,
        Sit = 3,
    }
}
