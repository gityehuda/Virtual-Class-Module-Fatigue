using DialogueEditor;
using ExitGames.Demos.DemoPunVoice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

public class LoadAssetBundleVirtualLabSoalModul2 : MonoBehaviour
{
    AssetBundle assetBundle;
    public NPCConversation myConversation;
    public GameObject confirmationCanvas;

    private ConversationManager conversationManager;

    // Start is called before the first frame update
    void Start()
    {
        // Check if the QuizManager GameObject exists
        GameObject quizManagerModul2 = GameObject.Find("QuizManager");
        if (quizManagerModul2 != null)
        {
            Debug.Log("QuizManager GameObject is present in the scene.");

            // Check if the QuizManager GameObject has the QuizManager script component
            var quizManagerModul2Script = quizManagerModul2.GetComponent<QuizManagerModul2>();
            if (quizManagerModul2Script != null)
            {
                Debug.Log("QuizManager GameObject has the QuizManager script component.");
            }
            else
            {
                Debug.LogWarning("QuizManager GameObject does not have the QuizManager script component.");
            }
        }
        else
        {
            Debug.LogWarning("QuizManager GameObject is not present in the scene.");
        }

        LoadAssetBundle();

        // Find and store the ConversationManager component
        conversationManager = FindObjectOfType<ConversationManager>();
    }

    void LoadAssetBundle()
    {
        foreach (var child in MainData.instance.pathFileMain)
        {
            AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(child));

            assetBundle = createRequest.assetBundle;

            var prefabRoom = assetBundle.LoadAsset<GameObject>("LabAntro");

            var instantRoom = Instantiate(prefabRoom);

            var searchAllTMP = instantRoom.GetComponentsInChildren<TextMeshPro>();

            foreach (var child2 in searchAllTMP)
            {
                ReplaceShaderForEditor(child2.fontSharedMaterial);
            }

            var triggerCube = instantRoom.transform.Find("TriggerCube");
            if (triggerCube != null)
            {
                var conversationStarter = triggerCube.GetComponent<ConversationStarter>();
                if (conversationStarter == null)
                {
                    conversationStarter = triggerCube.gameObject.AddComponent<ConversationStarter>();
                }
                conversationStarter.myConversation = myConversation;

                var lecturer = instantRoom.transform.Find("Lecturer");
                if (lecturer != null)
                {
                    lecturer.gameObject.tag = "Bot";
                    conversationStarter.objectToToggle = lecturer.gameObject;
                }
            }
        }
    }
    private void OnDestroy()
    {
        assetBundle.Unload(true);
    }

    public static void ReplaceShaderForEditor(Material material)
    {
        if (material == null) return;

        var shaderName = material.shader.name;
        var shader = Shader.Find(shaderName);

        Debug.Log(material);

        if (shader != null) material.shader = shader;
    }   
}
