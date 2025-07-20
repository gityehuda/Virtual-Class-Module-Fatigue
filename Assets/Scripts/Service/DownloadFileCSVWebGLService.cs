using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DownloadFileCSVWebGLService : MonoBehaviour, IDownloadFileCSVWebGLService
{
#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void DownloadFile(string filename, string content);
#endif

    public void DownloadFileCSV(string filename, string content)
    {
#if UNITY_WEBGL
        DownloadFile(filename,content);
#endif
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
