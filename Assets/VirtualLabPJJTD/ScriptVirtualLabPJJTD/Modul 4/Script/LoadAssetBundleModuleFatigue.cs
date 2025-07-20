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
        foreach(var child in MainData.instance.pathFileMain)
        {
            AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(child));
            assetBundle = createRequest.assetBundle;

            var prefabRoom = assetBundle.LoadAsset<GameObject>("LabAntro");
            var instantRoom = Instantiate(prefabRoom);
            var searchAllTMP = instantRoom.GetComponentsInChildren<TextMeshPro>();

            var renderers = instantChar.GetComponentsInChildren<Renderer>(true);
            foreach (var renderer in renderers) 
            {
                foreach(var material in renderer.materials)
                {
                    if(material.name.Contains(""))
                    {
                        Shader standardShader = Shader.Find("Standard (Specular setup)");
                        if(standardShader != null)
                        {
                            material.shader = standardShader;
                            Debug.Log("Shader for binus_logo_SHD material successfully set to Standard (Specular setup)");
                        }
                        else
                        {
                            Debug.LogWarning("Standard (Specular setup) shader not found");
                        }
                    }
                  
                }
            }

            if(!lecturerShown && !lecturerDestroyed)
            {
                var prefabLecturer = assetBundle.LoadAsset<GameObject>("Lecturer");
                instantChar = Instantiate(prefabLecturer);
                instantChar.tag = "Bot";
                lecturerShown = true;
            }

            instantChar.transform.position = new Vector3(7.65f, -0.8642919f, -4.389999f);
            instantChar.transform.localScale = new Vector3(1.769f, 1.769f, 1.769f);
            
            if(instantChar != null)
            {
                instantChar.transform.position = new Vector3(7.73f, -0.853f, 1.42f);
            }

            foreach(var child2 in searchAllTMP)
            {
               ReplaceShaderForEditor(child2.fontSharedMaterial);
            }

        }
       
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

        void OnCloseButtonClicked()
        {
            GameObject lecturer = GameObject.FindGameObjectWithTag("Bot");
            if (lecturer != null)
            {
                Destroy(lecturer);
                lecturerShown = false;
                lecturerDestroyed = true;
            }

        }
    }
}
