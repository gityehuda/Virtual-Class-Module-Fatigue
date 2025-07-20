using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Binus.WebGL.Service;
using static CustomClass;
#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class StartUpManager : MonoBehaviour, IStartUpManager
{
    [SerializeField] TextMeshProUGUI textInScreen;
    ListAssetBundle listAssetBundle;
    float downloadProgress = 0f;

    [Header("Asset Bundle")]
    public AssetBundleVersion assetBundleVirtualClassroomWebGL;
    public AssetBundleVersion assetBundleVirtualClassroomMobile;
    public AssetBundleVersion assetBundleVirtualLab;

    [Header("Virtual")]
    public GameObject virtualMode;
    public GameObject virtualClassRoom;
    public GameObject virtualLab;

    [Header("Home")]
    public GameObject home;
    public TextMeshProUGUI roleNameText;
    public TextMeshProUGUI acadCareerNameText;
    public TextMeshProUGUI institutionNameText;
    public TMP_Dropdown strmDropDown;
    public TMP_Dropdown monthDropDown;
    public TextMeshProUGUI tableStatusText;
    public GameObject table;
    public GameObject tableContent;
    public GameObject tableValue;

    [Header("Login")]
    public GameObject loginPage;
    public Button loginButton;

    [Header("LoginManual")]
    public GameObject loginManualPage;
    public TMP_InputField tmpInputEmail;
    public Button loginManualButton;
    public Button loginManualCancel;
    public GameObject wrongEmail;

    [Header("Download Asset Bundle")]
    public GameObject downloadAssetBundle;
    public Slider sliderDownloadAssetBundle;
    public TextMeshProUGUI progressDownloadAssetBundleText;

    DateTime now;

    CustomClass.GetHomeResponseDTO getHomeResponseDTO;

    Dictionary<string, List<CustomClass.GetTableResponseDTO>> cacheTableData = new Dictionary<string, List<CustomClass.GetTableResponseDTO>>();

    IInteropWebGLService interopWebGLService;

    private void Awake()
    {
        ServiceLocator.RegisterService<IStartUpManager>(this);
    }

    private void Start()
    {
#if UNITY_WEBGL
        interopWebGLService = ServiceLocator.GetService<IInteropWebGLService>();
#endif
        home.SetActive(false);
        loginManualPage.SetActive(false);
        downloadAssetBundle.SetActive(false);
        virtualMode.SetActive(false);

        virtualClassRoom.GetComponent<Button>().onClick.AddListener(delegate
        {
            VirtualClassRoom();
        });

        virtualLab.GetComponent<Button>().onClick.AddListener(delegate
        {
            VirtualLab();
        });
    }

    public void SetLoginPageActive(bool active)
    {
        loginPage.SetActive(active);
    }

    public void SetTextInScreen(string text)
    {
        textInScreen.text = text;
    }

    public void LoginManualWaiting()
    {
        SetLoginPageActive(false);
        SetTextInScreen("WAITING FOR LOAD DATA...");
    }

    public void LoginManualFail()
    {
        SetLoginPageActive(true);
        wrongEmail.SetActive(true);
    }

    public void LoginManualSuccess(ResponseLoginAAD dataUser)
    {
        MainData.instance.binusianId = dataUser.emplId;
        virtualLab.GetComponent<Button>().interactable = dataUser.isVirtualLabAccess;
        SetTextInScreen("");

        virtualMode.SetActive(true);
    }

    public void LoginMicrosoftWaiting()
    {
        SetLoginPageActive(false);
        SetTextInScreen("WAITING FOR LOAD DATA...");
    }

    public void LoginMicrosoftFail(string text)
    {
        SetTextInScreen(text);
    }

    public void LoginMicrosoftSuccess(ResponseLoginAAD dataUsr)
    {
        MainData.instance.binusianId = dataUsr.emplId;
        SetTextInScreen("");
        virtualLab.GetComponent<Button>().interactable = dataUsr.isVirtualLabAccess;
        virtualMode.SetActive(true);
    }

    public void LoginMicrosoftByPass()
    {
        SetTextInScreen("");
        SetLoginPageActive(true);
        SetLoginManualButtonActive(true);
    }

    public string GetInputEmail()
    {
        return tmpInputEmail.text;
    }

    public void SetLoginManualPageActive(bool active)
    {
        //--dosen
        //--BN123619010 annisa.arrahmah@binus.ac.id

        //--mhs
        //--BN124989013 kevin.suhartono@binus.ac.id
        //--BN124989133 haldhira.ladiva@binus.ac.id
        //--BN124989285 fafan.utama @binus.ac.id
        wrongEmail.SetActive(false);
        loginManualPage.SetActive(active);
    }

    public void SetLoginManualButtonActive(bool active)
    {
        loginButton.gameObject.SetActive(active);
    }

    IEnumerator GetToken()
    {
        textInScreen.text = "WAITING FOR JOIN...";
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(MainData.instance.apiMainUrl + "api/SessionToken", ""))
        {
            request.SetRequestHeader("binusianId", MainData.instance.binusianId);
            request.SetRequestHeader("courseCode", MainData.instance.courseUsr);
            request.SetRequestHeader("classCode", MainData.instance.classUsr);
            request.SetRequestHeader("start", MainData.instance.startUsr.ToString());
            request.SetRequestHeader("end", MainData.instance.endUsr.ToString());

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error : " + request.error);
                textInScreen.text = "You are not eligible to join this class";
            }
            else
            {
                var response = JsonUtility.FromJson<CustomClass.ResponseLoadSessionToken>(request.downloadHandler.text);

                if (response.isJoin == false)
                {
                    textInScreen.text = "WAITING FOR HOST TO JOIN...";
                    StartCoroutine(WaitForJoin());
                }
                else
                {
                    textInScreen.text = "WAITING FOR JOIN...";
                    MainData.instance.namaUsr = response.name;
                    MainData.instance.nimUsr = response.externalSystemId;
                    MainData.instance.roleUsr = response.role;
                    MainData.instance.sessionId = response.sessionId;
                    MainData.instance.sexUsr = response.sex;
                    MainData.instance.roomName = "CreativeClass";

                    AssetBundleVersion bundleVersion = null;
#if UNITY_WEBGL
                    bundleVersion = assetBundleVirtualClassroomWebGL;
#elif UNITY_ANDROID || UNITY_IOS
                    bundleVersion = assetBundleVirtualClassroomMobile;
#endif
                    DownloadAssetBundle("virtualclassroom", bundleVersion.assetBundleNamesAndVersions, "CreativeClass");
                }
            }
        }
    }

    IEnumerator WaitForJoin()
    {
        yield return new WaitForSeconds(10f);

        StartCoroutine(GetToken());
    }

    IEnumerator GetHome()
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(MainData.instance.apiMainUrl + "api/GetHome", ""))
        {
            request.SetRequestHeader("binusianid", MainData.instance.binusianId);
            request.SetRequestHeader("acadcareer", MainData.instance.acadCareer);
            request.SetRequestHeader("institution", MainData.instance.institution);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error : " + request.error);
                textInScreen.text = "You are not eligible to open this application, please contact administrator";
            }
            else
            {
                textInScreen.text = "";
                getHomeResponseDTO = JsonUtility.FromJson<CustomClass.GetHomeResponseDTO>(request.downloadHandler.text);

                now = DateTime.Parse(getHomeResponseDTO.now);

                roleNameText.text = getHomeResponseDTO.role;
                acadCareerNameText.text = getHomeResponseDTO.acadCareer;
                institutionNameText.text = getHomeResponseDTO.institution;

                MainData.instance.roleUsr = getHomeResponseDTO.roleId.ToString();

                strmDropDown.ClearOptions();
                var strmOptionData = new List<TMP_Dropdown.OptionData>();
                foreach (var child in getHomeResponseDTO.strm)
                {
                    var optionData = new TMP_Dropdown.OptionData();
                    optionData.text = child.desc;
                    strmOptionData.Add(optionData);
                }

                strmDropDown.AddOptions(strmOptionData);

                GetMonth(DateTime.Parse(getHomeResponseDTO.strm[0].beginDt).Month, DateTime.Parse(getHomeResponseDTO.strm[0].endDt).Month);

                strmDropDown.onValueChanged.AddListener(delegate
                {
                    var tempStrm = getHomeResponseDTO.strm.FirstOrDefault(x => x.desc == strmDropDown.captionText.text);

                    GetMonth(DateTime.Parse(tempStrm.beginDt).Month, DateTime.Parse(tempStrm.endDt).Month);

                    table.SetActive(false);
                    if (cacheTableData.ContainsKey(tempStrm.strm))
                    {
                        GetTableBaseOnMonth(tempStrm.strm, DateTime.Parse(tempStrm.beginDt).Month);
                    }
                    else
                    {
                        tableStatusText.text = "Loading Data..";
                        StartCoroutine(GetTable(DateTime.Parse(tempStrm.beginDt), tempStrm.strm));
                    }
                });

                monthDropDown.onValueChanged.AddListener(delegate
                {
                    var tempStrm = getHomeResponseDTO.strm.FirstOrDefault(x => x.desc == strmDropDown.captionText.text);

                    Enum.TryParse(typeof(CustomClass.Month), monthDropDown.captionText.text, true, out var result);
                    GetTableBaseOnMonth(tempStrm.strm, (int)result);
                });

                cacheTableData.Add(getHomeResponseDTO.strm[0].strm, getHomeResponseDTO.tableData);

                GetTableBaseOnMonth(getHomeResponseDTO.strm[0].strm, DateTime.Parse(getHomeResponseDTO.strm[0].beginDt).Month);

                home.SetActive(true);
            }
        }
    }

    IEnumerator GetTable(DateTime begin, string strm)
    {
        monthDropDown.interactable = false;
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(MainData.instance.apiMainUrl + "api/GetTable", ""))
        {
            request.SetRequestHeader("binusianid", MainData.instance.binusianId);
            request.SetRequestHeader("acadcareer", MainData.instance.acadCareer);
            request.SetRequestHeader("institution", MainData.instance.institution);
            request.SetRequestHeader("roleid", MainData.instance.roleUsr);
            request.SetRequestHeader("strm", strm);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error : " + request.error);
                //textInScreen.text = "You are not eligible to join this class";
            }
            else
            {
                var tempData = JsonUtility.FromJson<CustomClass.GetTableOutputDTO>(request.downloadHandler.text);

                //getHomeResponseDTO.tableData = tempData.data;

                if (!cacheTableData.ContainsKey(strm))
                {
                    cacheTableData.Add(strm, tempData.data);
                }
                tableStatusText.text = "";
                GetTableBaseOnMonth(strm, begin.Month);
                monthDropDown.interactable = true;
            }
        }
    }

    private void GetMonth(int begin, int end)
    {
        var data = new List<string>();
        if (begin <= end)
        {
            for (int i = begin; i <= end; i++)
            {
                data.Add(((CustomClass.Month)i).ToString());
            }
        }
        else
        {
            for (int i = begin; i <= 12; i++)
            {
                data.Add(((CustomClass.Month)i).ToString());
            }
            for (int i = 1; i <= end; i++)
            {
                data.Add(((CustomClass.Month)i).ToString());
            }
        }

        monthDropDown.ClearOptions();
        var monthOptionData = new List<TMP_Dropdown.OptionData>();

        foreach (var child in data)
        {
            var optionData = new TMP_Dropdown.OptionData();
            optionData.text = child;
            monthOptionData.Add(optionData);
        }

        monthDropDown.AddOptions(monthOptionData);
    }

    private void GetTableBaseOnMonth(string strm, int begin)
    {
        for (int i = 0; i < tableContent.transform.childCount; i++)
        {
            Destroy(tableContent.transform.GetChild(i).gameObject);
        }

        var getTableDataFilterByMonth = cacheTableData[strm].Where(x => DateTime.Parse(x.startDate).Month == begin).ToList();

        if (getTableDataFilterByMonth == null || getTableDataFilterByMonth.Count == 0)
        {
            table.SetActive(false);
            tableStatusText.text = "No Data..";
            return;
        }
        tableStatusText.text = "";

        var topData = getTableDataFilterByMonth.Where(x => DateTime.Parse(x.startDate).Date >= now.Date);

        foreach (var data in topData)
        {
            if (now.Date == DateTime.Parse(data.startDate).Date && now.TimeOfDay > DateTime.Parse(data.meetingTimeEnd).AddHours(1).TimeOfDay) continue;
            var goTableValue = Instantiate(tableValue);
            goTableValue.transform.SetParent(tableContent.transform);

            var setTableValue = goTableValue.GetComponent<SetDataValue>();
            setTableValue.matakuliahText.text = data.courseTitle;
            setTableValue.kodeMatakuliahText.text = data.courseCode;
            setTableValue.kelasText.text = data.classSection;
            setTableValue.tanggalText.text = DateTime.Parse(data.startDate).ToString("dd MMM yyyy");
            setTableValue.jamText.text = DateTime.Parse(data.meetingTimeStart).ToString("HH:mm") + " - " + DateTime.Parse(data.meetingTimeEnd).ToString("HH:mm");

            goTableValue.transform.localScale = Vector3.one;

            if (now.Date == DateTime.Parse(data.startDate).Date && now.AddHours(1).TimeOfDay >= DateTime.Parse(data.meetingTimeStart).TimeOfDay)
            {
                setTableValue.joinButton.SetActive(true);
                setTableValue.joinButton.GetComponent<Button>().onClick.AddListener(delegate
                {
                    MainData.instance.courseUsr = data.courseCode;
                    MainData.instance.classUsr = data.classSection;
                    MainData.instance.startUsr = DateTime.Parse(data.startDate).ToString("yyyy-MM-dd") + "T" + DateTime.Parse(data.meetingTimeStart).ToString("HH:mm");
                    MainData.instance.endUsr = DateTime.Parse(data.startDate).ToString("yyyy-MM-dd") + "T" + DateTime.Parse(data.meetingTimeEnd).ToString("HH:mm");
                    home.SetActive(false);
                    textInScreen.text = "WAITING FOR JOIN...";
                    StartCoroutine(GetToken());
                });
            }
            else
            {
                setTableValue.joinButton.SetActive(false);
            }
        }

        var bottomData = getTableDataFilterByMonth.Where(x => DateTime.Parse(x.startDate).Date <= now.Date);

        foreach (var data in bottomData)
        {
            if (now.Date == DateTime.Parse(data.startDate).Date && now.TimeOfDay <= DateTime.Parse(data.meetingTimeEnd).AddHours(1).TimeOfDay) continue;
            var goTableValue = Instantiate(tableValue);
            goTableValue.transform.SetParent(tableContent.transform);

            var setTableValue = goTableValue.GetComponent<SetDataValue>();
            setTableValue.matakuliahText.text = data.courseTitle;
            setTableValue.kodeMatakuliahText.text = data.courseCode;
            setTableValue.kelasText.text = data.classSection;
            setTableValue.tanggalText.text = DateTime.Parse(data.startDate).ToString("dd MMM yyyy");
            setTableValue.jamText.text = DateTime.Parse(data.meetingTimeStart).ToString("HH:mm") + " - " + DateTime.Parse(data.meetingTimeEnd).ToString("HH:mm");

            goTableValue.transform.localScale = Vector3.one;

            setTableValue.joinButton.SetActive(false);
        }

        table.SetActive(true);
    }

    void VirtualClassRoom()
    {
        textInScreen.text = "WAITING FOR LOAD DATA...";
        virtualMode.SetActive(false);
        StartCoroutine(GetHome());
    }

    void VirtualLab()
    {
        textInScreen.text = "WAITING FOR LOAD DATA...";
        virtualMode.SetActive(false);

        StartCoroutine(SetVirtualLabUserLog());
    }

    IEnumerator SetVirtualLabUserLog()
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(MainData.instance.apiMainUrl + "api/VirtualLabAbsent", ""))
        {
            request.SetRequestHeader("binusianId", MainData.instance.binusianId);

            yield return request.SendWebRequest();

            DownloadAssetBundle("virtualclassroom", assetBundleVirtualLab.assetBundleNamesAndVersions, "LabAwal");
        }
    }

    public void DownloadAssetBundle(string folderName, List<AssetBundleNamesAndVersion> assetBundleNamesAndVersions, string sceneName)
    {
        listAssetBundle = new ListAssetBundle();
        listAssetBundle.folderName = folderName;
        listAssetBundle.assetBundleNamesAndVersions = assetBundleNamesAndVersions;

        textInScreen.text = "";
        downloadAssetBundle.SetActive(true);

        NextAssetBundle(listAssetBundle.folderName, 0, sceneName);
    }

    public void NextAssetBundle(string folderName, int array, string sceneName)
    {
        if (listAssetBundle.assetBundleNamesAndVersions.Count - 1 < array)
        {
            SetProgressDownloadAssetBundle(100f);
            SceneManager.LoadScene(sceneName);
            return;
        }

        var fileName = listAssetBundle.assetBundleNamesAndVersions[array].assetBundleNames.ToString();
        var pathFile = GetPath(folderName, fileName + listAssetBundle.assetBundleNamesAndVersions[array].version.ToString(), "unity3d");

        MainData.instance.pathFileMain.Add(pathFile);

        if (File.Exists(pathFile))
        {
            downloadProgress += (100 / listAssetBundle.assetBundleNamesAndVersions.Count);
            SetProgressDownloadAssetBundle(downloadProgress);
            NextAssetBundle(folderName, array + 1, sceneName);
            return;
        }

        StartCoroutine(DownloadAssetBundleFromServer(listAssetBundle.assetBundleNamesAndVersions[array].urlAssetBundle, pathFile, folderName, array, sceneName));
    }

    private void SetProgressDownloadAssetBundle(float progress)
    {
        sliderDownloadAssetBundle.value = ((float)(progress / 100f));
        progressDownloadAssetBundleText.text = (int)Math.Ceiling(progress) + " %";
    }

    public string GetPath(string folderName, string fileName, string fileExtension)
    {
        string pathDir = Application.persistentDataPath + "/AssetBundle";
        string pathFile = pathDir + "/" + fileName + "." + fileExtension;

        if (!Directory.Exists(pathDir))
        {
            Directory.CreateDirectory(pathDir);
        }

        return pathFile;
    }

    IEnumerator DownloadAssetBundleFromServer(string url, string pathFile, string folderName, int array, string sceneName)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);

        StartCoroutine(DownloadProgressBar(www));
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(url);
            Debug.Log(www.error);
        }
        else
        {
            downloadProgress += (100 / listAssetBundle.assetBundleNamesAndVersions.Count);
            SaveAssetBundleToLocal(www.downloadHandler.data, pathFile, folderName, array, sceneName);
        }
    }

    IEnumerator DownloadProgressBar(UnityWebRequest request)
    {
        while (!request.isDone)
        {
            float tempDownloadProgress = downloadProgress;
            tempDownloadProgress += ((request.downloadProgress * 100) / listAssetBundle.assetBundleNamesAndVersions.Count);
            SetProgressDownloadAssetBundle((tempDownloadProgress));
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void SaveAssetBundleToLocal(byte[] data, string pathFile, string folderName, int array, string sceneName)
    {
        File.WriteAllBytes(pathFile, data);
        NextAssetBundle(folderName, array + 1, sceneName);
    }
}

[Serializable]
public enum AssetBundleNames
{
    virtualclassroomglobal,
    virtualclassroomcreativeclass,
    virtualclassroomdiscussionroom,
    virtuallab,
}

[Serializable]
public class ListAssetBundle
{
    public string folderName;
    public List<AssetBundleNamesAndVersion> assetBundleNamesAndVersions;
}

[Serializable]
public class AssetBundleNamesAndVersion
{
    public AssetBundleNames assetBundleNames;
    public int version;
    [HideInInspector] public string urlAssetBundle;

    public void GenerateAssetBundleUrl()
    {
        string baseUrl = GetBaseUrl();

        this.urlAssetBundle = baseUrl + assetBundleNames.ToString() + "_ver" + version.ToString() + "/" + assetBundleNames.ToString() + ".unity3d";
    }

    public string GetBaseUrl()
    {
        string baseUrl = "";
#if UNITY_ANDROID
        baseUrl = "https://cdn-bion-3dvirtualclassroom-assets.azureedge.net/bol/AR_Asset/Android_ver1/VirtualClassroom/";
#elif UNITY_IOS
        baseUrl = "https://cdn-bion-3dvirtualclassroom-assets.azureedge.net/bol/AR_Asset/iOS_ver1/VirtualClassroom/";
#else
        baseUrl = "https://cdn-bion-3dvirtualclassroom-assets.azureedge.net/bol/AR_Asset/WebGL/";
#endif
        return baseUrl;
    }
}
