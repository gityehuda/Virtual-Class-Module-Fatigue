using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using TMPro;
using UnityEngine;

public class LoadAssetBundleModuleFatigue : MonoBehaviour
{
    public GameObject panel;
    public GameObject conversationCanvas;
    public GameObject confirmationPanel;
    AssetBundle assetBundle;
    GameObject instantChar;
    private static bool conversationCanvasShown = false;
    private static bool lecturerShown = false;
    private static bool lecturerDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        LoadAssetBundle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadAssetBundle()
    {
       
    }

    private void OnDestroy()
    {
        assetBundle.Unload(true);
        lecturerShown = false;
    }

    public static void ReplaceShaderForEditor(Material material)
    {
        if (material == null)
        {
            return;
        }

        var shaderName = material.shader.name;
        var shader = Shader.Find(shaderName);
        Debug.Log(material);
        if (shader != null)
        {
            material.shader = shader;
        }

    }
}
