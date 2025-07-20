using Cinemachine;
using ExitGames.Demos.DemoPunVoice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAssetBundleVirtualLab : MonoBehaviour
{
    // Ruang Awal
    public CinemachineFreeLook cinemachineFreeLook;
    public ConfirmationPanel confirmationPanel;
    public GameObject panel;
    public GameObject conversationCanvas;
    public Transform cam;
    public Transform player;
    public ThirdPersonMovement playerMovementScript;
    public Button closeButton;

    AssetBundle assetBundle;
    GameObject instantChar1;

    // Static variables to track if conversation canvas and Lecturer object have been shown
    private static bool conversationCanvasShown = false;
    private static bool lecturerShown = false;
    private static bool lecturerDestroyed = false;

    public GameObject arrowModule1;
    public GameObject arrowModule2;
    //public Testing testingScript;

    void Start()
    {
        // Disable player movement script initially
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }

        // Show conversation canvas only if it hasn't been shown before
        if (!conversationCanvasShown && conversationCanvas != null)
        {
            conversationCanvas.SetActive(true);
            conversationCanvasShown = true;
        }
        else if (conversationCanvas != null)
        {
            conversationCanvas.SetActive(false);
        }

        // Add listener to the close button
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }
        LoadAssetBundle();
    }

    void LoadAssetBundle()
    {
        foreach (var child in MainData.instance.pathFileMain)
        {
            AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(child));

            assetBundle = createRequest.assetBundle;

            var prefabRoom = assetBundle.LoadAsset<GameObject>("Lab");

            var instantRoom = Instantiate(prefabRoom);

            var searchAllTMP = instantRoom.GetComponentsInChildren<TextMeshPro>();

            var prefabChar = assetBundle.LoadAsset<GameObject>("mahasiswa_female");

            var instantChar = Instantiate(prefabChar);
            instantChar.tag = "Player";

            var renderers = instantChar.GetComponentsInChildren<Renderer>(true);  // true to include inactive objects
            foreach (var renderer in renderers)
            {
                foreach (var material in renderer.materials)
                {
                    // Check for material name "binus_logo_SHD" and set to Standard shader
                    if (material.name.Contains("binus_logo_SHD"))
                    {
                        Shader standardShader = Shader.Find("Standard (Specular setup)");
                        if (standardShader != null)
                        {
                            material.shader = standardShader;
                            Debug.Log("Shader for binus_logo_SHD material successfully set to Standard (Specular setup).");
                        }
                        else
                        {
                            Debug.LogWarning("Standard (Specular setup) shader not found.");
                        }
                    }
                }
            }

            /*// Add a BoxCollider to the instantiated character if it doesn't already have one
            if (instantChar.GetComponent<BoxCollider>() == null)
            {
                instantChar.AddComponent<BoxCollider>();
            }*/


            // Only instantiate the Lecturer if it hasn't been shown before and hasn't been destroyed
            if (!lecturerShown && !lecturerDestroyed)
            {
                var prefabLecturer = assetBundle.LoadAsset<GameObject>("LecturerAwal");
                instantChar1 = Instantiate(prefabLecturer);
                instantChar1.tag = "Bot";
                lecturerShown = true;
            }

            // Set the position & Scale Char    
            instantChar.transform.position = new Vector3(7.65f, -0.8642919f, -4.389999f);
            instantChar.transform.localScale = new Vector3(1.769f, 1.769f, 1.769f);

            if (instantChar1 != null)
            {
                instantChar1.transform.position = new Vector3(7.73f, -0.853f, 1.42f);
            }

            foreach (var child2 in searchAllTMP)
            {
                ReplaceShaderForEditor(child2.fontSharedMaterial);
            }

            playerMovementScript = instantChar.GetComponent<ThirdPersonMovement>();
            if (playerMovementScript == null)
            {
                playerMovementScript = instantChar.AddComponent<ThirdPersonMovement>();
            }
            playerMovementScript.confirmationPanel = panel;
            playerMovementScript.conversationCanvas = conversationCanvas; // Assign conversation canvas
            playerMovementScript.cam = cam;
            player = instantChar.transform;

            var target = instantChar.transform.Find("GameObject");
            if (target != null)
            {
                if (cinemachineFreeLook != null)
                {
                    cinemachineFreeLook.Follow = target;
                    cinemachineFreeLook.LookAt = target;
                }
            }
            else
            {
                Debug.LogWarning("No child named 'GameObject' found under 'mahasiswa_female'.");
            }

            // ARROW MODULE 1
            /*arrowModule1 = GameObject.Find("ArrowModule 1");
            if (arrowModule1 != null)
            {
                arrowModule1.SetActive(false);
            }
            else
            {
                Debug.LogWarning("ArrowModule 1 not found at the start.");
            }*/

            // ARROW MODULE 2
            /*arrowModule2 = GameObject.Find("ArrowModule 2");
            if (arrowModule2 != null)
            {
                arrowModule2.SetActive(false);
            }
            else
            {
                Debug.LogWarning("ArrowModule 2 not found at the start.");
            }*/

            GameObject modul1 = GameObject.Find("Modul 1");
            GameObject modul2 = GameObject.Find("Modul 2");
            GameObject modul3 = GameObject.Find("Modul 3");
            GameObject modul4 = GameObject.Find("Modul 4");

            if (modul1 != null)
            {
                modul1.SetActive(false);  // Hide Modul 1
            }

            /*if (modul1 != null)
            {
                // Assign the found "Modul 1" to the Module1 field of the Testing script
                if (testingScript != null)
                {
                    testingScript.Module1 = modul1;
                }
                else
                {
                    Debug.LogWarning("Testing script reference is not set in LoadAssetBundleVirtualLab.");
                }
            }*/

            if (modul2 != null)
            {
                modul2.SetActive(false);  // Hide Modul 2
            }

            if (modul3 != null)
            {
                modul3.SetActive(false);  // Hide Modul 3
            }

            if (modul4 != null)
            {
                modul4.SetActive(false);                    
            }

            // Find "Box antropometri" and assign its Collider to ConfirmationPanel
            Transform boxAntropometri = instantRoom.transform.Find("Box antropemetri");
            if (boxAntropometri != null)
            {
                Collider boxCollider = boxAntropometri.GetComponent<Collider>();
                if (boxCollider != null)
                {
                    confirmationPanel.item1Collider = boxCollider;
                }
                else
                {
                    Debug.LogWarning("No Collider found on 'Box antropemetri'.");
                }
            }
            else
            {
                Debug.LogWarning("No 'Box antropemetri' found in 'Lab'.");
            }

            // Find "Time Study" and assign its Collider to ConfirmationPanel
            Transform timestudy = instantRoom.transform.Find("TimeStudy");
            if (timestudy != null)
            {
                Collider boxcollider = timestudy.GetComponent<Collider>();
                if (boxcollider != null)
                {
                    confirmationPanel.item2Collider = boxcollider;
                }
                else
                {
                    Debug.LogWarning("no collider found on 'timestudy'.");
                }
            }
            else
            {
                Debug.LogWarning("no 'timestudy' found in 'lab'.");
            }

            // Find "Biomechanics and Design of Manual Handling​" and assign its Collider to ConfirmationPanel
            Transform biomechanicsAndDesignOfManualHandling​ = instantRoom.transform.Find("Biomechanics and Design of Manual Handling​");
            if (biomechanicsAndDesignOfManualHandling​ != null)
            {
                Collider boxCollider = biomechanicsAndDesignOfManualHandling​.GetComponent<Collider>();
                if (boxCollider != null)
                {
                    confirmationPanel.item3Collider = boxCollider;
                }
                else
                {
                    Debug.LogWarning("No Collider found on 'Biomechanics and Design of Manual Handling​'.");
                }
            }
            else
            {
                Debug.LogWarning("No 'Biomechanics and Design of Manual Handling​' found in 'Lab'.");
            }

            // Find "Fatigue & Energy Consumption" and assign its Collider to ConfirmationPanel
            Transform fatigueAndEnergyConsumption = instantRoom.transform.Find("Fatigue & Energy Consumption");
            if (fatigueAndEnergyConsumption != null)
            {
                Collider boxCollider = fatigueAndEnergyConsumption.GetComponent<Collider>();
                if (boxCollider != null)
                {
                    confirmationPanel.item4Collider = boxCollider;
                }
                else
                {
                    Debug.LogWarning("No Collider found on 'Fatigue & Energy Consumption​'.");
                }
            }
            else
            {
                Debug.LogWarning("No 'Fatigue & Energy Consumption​' found in 'Lab'.");
            }

            //// Find "Work Environment Design" and assign its Collider to ConfirmationPanel
            //Transform workEnvironmentDesign​ = instantRoom.transform.Find("Work Environment Design​");
            //if (workEnvironmentDesign​ != null)
            //{
            //    Collider boxCollider = workEnvironmentDesign​.GetComponent<Collider>();
            //    if (boxCollider != null)
            //    {
            //        confirmationPanel.item5Collider = boxCollider;
            //    }
            //    else
            //    {
            //        Debug.LogWarning("No Collider found on 'Work Environment Design​​'.");
            //    }
            //}
            //else
            //{
            //    Debug.LogWarning("No 'Work Environment Design​' found in 'Lab'.");
            //}

            foreach (Transform child2 in instantRoom.transform)
            {
                if (child2.gameObject.name == "Box antropemetri")
                {
                    if (child2.gameObject.name.Contains("Box antropemetri"))
                    {
                        var tempObject = child2.gameObject.AddComponent<ObjectClickHandler>();
                        tempObject.confirmationPanel = confirmationPanel;
                        tempObject.player = player;
                    }
                    Debug.Log(child2.gameObject.name);
                }
            }

            foreach (Transform child2 in instantRoom.transform)
            {
                if (child2.gameObject.name == "TimeStudy")
                {
                    if (child2.gameObject.name.Contains("TimeStudy"))
                    {
                        var tempObject = child2.gameObject.AddComponent<ObjectClickHandler>();
                        tempObject.confirmationPanel = confirmationPanel;
                        tempObject.player = player;
                    }
                    Debug.Log(child2.gameObject.name);
                }
            }

            foreach (Transform child2 in instantRoom.transform)
            {
                if (child2.gameObject.name == "Biomechanics and Design of Manual Handling​")
                {
                    if (child2.gameObject.name.Contains("Biomechanics and Design of Manual Handling​"))
                    {
                        var tempObject = child2.gameObject.AddComponent<ObjectClickHandler>();
                        tempObject.confirmationPanel = confirmationPanel;
                        tempObject.player = player;
                    }
                    Debug.Log(child2.gameObject.name);
                }
            }

            foreach (Transform child2 in instantRoom.transform)
            {
                if (child2.gameObject.name == "Fatigue & Energy Consumption")
                {
                    if (child2.gameObject.name.Contains("Fatigue & Energy Consumption"))
                    {
                        var tempObject = child2.gameObject.AddComponent<ObjectClickHandler>();
                        tempObject.confirmationPanel = confirmationPanel;
                        tempObject.player = player;
                    }
                    Debug.Log(child2.gameObject.name);
                }
            }

            // foreach (Transform child2 in instantRoom.transform)
            // {
            //     if (child2.gameObject.name == "Work Environment Design​")
            //     {
            //         if (child2.gameObject.name.Contains("Work Environment Design​"))
            //         {
            //             var tempObject = child2.gameObject.AddComponent<ObjectClickHandler>();
            //             tempObject.confirmationPanel = confirmationPanel;
            //             tempObject.player = player;
            //         }
            //         Debug.Log(child2.gameObject.name);
            //     }
            // }
        }
    }

    private void OnDestroy()
    {
        assetBundle.Unload(true);
        lecturerShown = false; 
    }

    public static void ReplaceShaderForEditor(Material material)
    {
        if (material == null) return;

        var shaderName = material.shader.name;
        var shader = Shader.Find(shaderName);

        Debug.Log(material);

        if (shader != null) material.shader = shader;
    }

    void OnCloseButtonClicked()
    {
        // Destroy the lecturer instance if it exists
        GameObject lecturer = GameObject.FindGameObjectWithTag("Bot");
        if (lecturer != null)
        {
            Destroy(lecturer);
            lecturerShown = false;
            lecturerDestroyed = true; 
        }

        /*if (arrowModule1 != null)
        {
            arrowModule1.SetActive(true);
        }
        else
        {
            Debug.LogWarning("ArrowModule 1 is not assigned or found.");
        }

        if (arrowModule2 != null)
        {
            arrowModule2.SetActive(true);
        }
        else
        {
            Debug.LogWarning("ArrowModule 2 is not assigned or found.");
        }*/
    }
}
