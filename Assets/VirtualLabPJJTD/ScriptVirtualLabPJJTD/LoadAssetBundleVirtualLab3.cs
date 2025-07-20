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

public class LoadAssetBundleVirtualLab3 : MonoBehaviour
{
    AssetBundle assetBundle;
    public GameObject confirmationCanvas; // Reference to the ConfirmationCanvas
    public UnityEngine.UI.Button soalButton; // Reference to the Soal button
    private bool kursiGuruSpawned = false; // Flag to track if KursiGuru has been spawned
    public UnityEngine.UI.Button closeButton; // Reference to the Close button in the ConversationCanvas
    public UnityEngine.UI.Button nextDialogueButton; // Reference to the Next Dialogue button
    private GameObject soalAntroInstance; // Reference to the SoalAntro instance
    public UnityEngine.UI.Button exitButton;
    private GameObject soalCanvas;

    void Start()
    {
        GameObject quizManager = GameObject.Find("QuizManager");
        if (quizManager != null)
        {
            Debug.Log("QuizManager GameObject is present in the scene.");

            var quizManagerScript = quizManager.GetComponent<QuizManager>();
            if (quizManagerScript != null)
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

        if (soalButton != null)
        {
            soalButton.gameObject.SetActive(false); // Hide the Soal button initially
            soalButton.onClick.AddListener(OnSoalButtonClick);
        }

        // Find and add listener to the Close button
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(DestroyLecturer);
        }

        // Add listener to the Next Dialogue button
        if (nextDialogueButton != null)
        {
            nextDialogueButton.onClick.AddListener(OnNextDialogueClick);
            nextDialogueButton.gameObject.SetActive(false); // Hide the button initially
        }

        if (exitButton == null && confirmationCanvas != null)
        {
            // Find the Exit button under ConfirmationCanvas
            exitButton = confirmationCanvas.transform.Find("Exit").GetComponent<UnityEngine.UI.Button>();
        }
        soalCanvas = GameObject.Find("SoalCanvas");
        // Load and set images for Soal1 through Soal7
        SetImagesForSoal();
    }

    void Update()
    {
        if (exitButton != null && soalCanvas != null)
        {
            // Disable Exit button if any SoalCanvas panel is active
            exitButton.interactable = !IsAnySoalPanelActive();
        }
    }

    bool IsAnySoalPanelActive()
    {
        foreach (Transform child in soalCanvas.transform)
        {
            if (child.gameObject.activeSelf)
            {
                return true; // A panel is active
            }
        }
        return false; // No active panels
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
        }
    }

    void SetImagesForSoal()
    {
        string[] soalNames = { "Soal1", "Soal2", "Soal3", "Soal4", "Soal5", "Soal6", "Soal7" };
        string[] imageNames = { "Nomor1", "Nomor2", "Nomor3", "Nomor4", "Nomor5", "Nomor6", "Nomor7" };

        GameObject soalCanvas = GameObject.Find("SoalCanvas");

        if (soalCanvas != null)
        {
            for (int i = 0; i < soalNames.Length; i++)
            {
                Transform soalTransform = soalCanvas.transform.Find($"{soalNames[i]}/Image");
                if (soalTransform != null)
                {
                    UnityEngine.UI.Image imageComponent = soalTransform.GetComponent<UnityEngine.UI.Image>();
                    if (imageComponent != null)
                    {
                        Texture2D texture = assetBundle.LoadAsset<Texture2D>(imageNames[i]);
                        if (texture != null)
                        {
                            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            imageComponent.sprite = sprite;
                            Debug.Log($"Image for {soalNames[i]} has been set successfully.");
                        }
                        else
                        {
                            Debug.LogError($"Texture '{imageNames[i]}' not found in the asset bundle.");
                        }
                    }
                    else
                    {
                        Debug.LogError($"Image component not found on {soalNames[i]}/Image GameObject.");
                    }
                }
                else
                {
                    Debug.LogError($"{soalNames[i]}/Image GameObject not found under SoalCanvas.");
                }
            }
        }
        else
        {
            Debug.LogError("SoalCanvas GameObject not found.");
        }
    }

    void OnConversationStarted()
    {
        if (soalButton != null)
        {
            soalButton.interactable = false;
        }
    }

    void OnConversationEnded()
    {
        if (soalButton != null)
        {
            soalButton.interactable = true;
        }
    }

    void OnSoalButtonClick()
    {
        // Destroy the SoalAntro object when the Soal button is clicked
        if (soalAntroInstance != null)
        {
            Destroy(soalAntroInstance);
            Debug.Log("SoalAntro object has been destroyed.");
        }

        // Perform other actions, like spawning the KursiGuru
        if (soalButton != null && soalButton.interactable)
        {
            SpawnKursiGuru();
        }

        if (soalButton != null)
        {
            soalButton.gameObject.SetActive(false);
            Debug.Log("Soal button has been hidden after click.");
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

    void DestroyLecturer()
    {
        // Find the Lecturer GameObject
        GameObject lecturer = GameObject.Find("Lecturer");
        if (lecturer != null)
        {
            // Destroy the Lecturer GameObject
            Destroy(lecturer);
            Debug.Log("Lecturer object has been destroyed.");

            // Load and instantiate the SoalAntro prefab from the asset bundle
            var soalAntroPrefab = assetBundle.LoadAsset<GameObject>("SoalAntro");
            soalAntroPrefab.AddComponent<Reset>();

            if (soalAntroPrefab != null)
            {
                // Specify the position and rotation for SoalAntro
                Vector3 spawnPosition = new Vector3(2.591402f, 0.8227471f, 8.501642f);
                Quaternion spawnRotation = Quaternion.Euler(180f, -90f, 0f);

                // Instantiate SoalAntro in the scene
                soalAntroInstance = Instantiate(soalAntroPrefab, spawnPosition, spawnRotation);
                Debug.Log("SoalAntro object has been instantiated and moved to the specified location.");

                // Add ObjectControl component to SoalAntro
                var objectController = soalAntroInstance.AddComponent<ObjectControl>();

                // Configure ObjectControl parameters
                objectController.moveSpeed = 5f;
                objectController.rotateSpeed = 3f;
                objectController.zoomSpeed = 100f;
                objectController.minX = 0f;
                objectController.maxX = 5f;
                objectController.minY = 0f;
                objectController.maxY = 2.5f;

                Debug.Log("ObjectControl component added to SoalAntro and configured.");
            }
            else
            {
                Debug.LogError("SoalAntro prefab not found in the asset bundle.");
            }

            // Auto-click the nextDialogueButton when the Lecturer is destroyed
            if (nextDialogueButton != null)
            {
                nextDialogueButton.gameObject.SetActive(true);  // Show the button
                nextDialogueButton.onClick.Invoke();  // Auto-click the button
                nextDialogueButton.gameObject.SetActive(false); // Optionally hide it again

                // Show the Soal button after the dialogue is closed
                if (soalButton != null)
                {
                    soalButton.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.LogWarning("Lecturer object not found.");
        }
    }

    void OnNextDialogueClick()
    {
        Debug.Log("Next Dialogue button clicked.");
        // Add the logic to handle what happens when the Next Dialogue button is clicked
    }

    void SpawnKursiGuru()
    {
        if (!kursiGuruSpawned)
        {
            var kursiGuruPrefab = assetBundle.LoadAsset<GameObject>("KursiGuru");

            if (kursiGuruPrefab != null)
            {
                Vector3 spawnPosition = new Vector3(2.259f, 0.707037f, 7.69f);
                GameObject kursiGuruInstance = Instantiate(kursiGuruPrefab, spawnPosition, Quaternion.Euler(0, -180, 0));

                kursiGuruInstance.AddComponent<Reset>();
                var objectController = kursiGuruInstance.AddComponent<ObjectControl>();

                objectController.moveSpeed = 5f;
                objectController.rotateSpeed = 3f;
                objectController.zoomSpeed = 100f;
                objectController.minX = 0f;
                objectController.maxX = 5f;
                objectController.minY = 0f;
                objectController.maxY = 2.5f;

                Transform pointChild = kursiGuruInstance.transform.Find("Point");
                if (pointChild != null)
                {
                    for (int i = 0; i <= 6; i++)
                    {
                        Transform cubeChild = pointChild.Find($"Cube{(i == 0 ? "" : $" ({i})")}");
                        if (cubeChild != null)
                        {
                            var openPanelScript = cubeChild.gameObject.AddComponent<OpenPanelOnClick>();
                            GameObject soalCanvas = GameObject.Find("SoalCanvas");
                            if (soalCanvas != null)
                            {
                                GameObject soal = soalCanvas.transform.Find($"Soal{i + 1}").gameObject;
                                if (soal != null)
                                {
                                    openPanelScript.panel = soal;
                                }
                                else
                                {
                                    Debug.LogError($"Soal{i + 1} GameObject not found within SoalCanvas.");
                                }
                            }
                            else
                            {
                                Debug.LogError("SoalCanvas GameObject not found.");
                            }
                        }
                        else
                        {
                            Debug.LogError($"Cube{(i == 0 ? "" : $" ({i})")} child object not found under Point.");
                        }
                    }
                }
                else
                {
                    Debug.LogError("Point child object not found in the KursiGuru prefab.");
                }

                GameObject[] botObjects = GameObject.FindGameObjectsWithTag("Bot");
                foreach (GameObject botObject in botObjects)
                {
                    Destroy(botObject);
                }

                kursiGuruSpawned = true;

                GameObject quizManager = GameObject.Find("QuizManager");
                if (quizManager != null)
                {
                    var quizManagerScript = quizManager.GetComponent<QuizManager>();
                    if (quizManagerScript != null)
                    {
                        string[] cubeNames = { "Cube", "Cube (1)", "Cube (2)", "Cube (3)", "Cube (4)", "Cube (5)", "Cube (6)" };
                        quizManagerScript.objectsWithOpenPanelOnClick = new OpenPanelOnClick[cubeNames.Length];

                        for (int i = 0; i < cubeNames.Length; i++)
                        {
                            Transform cubeChild = kursiGuruInstance.transform.Find($"Point/{cubeNames[i]}");
                            if (cubeChild != null)
                            {
                                quizManagerScript.objectsWithOpenPanelOnClick[i] = cubeChild.GetComponent<OpenPanelOnClick>();
                            }
                            else
                            {
                                Debug.LogError($"{cubeNames[i]} GameObject not found under KursiGuru->Point.");
                            }
                        }

                        Debug.Log("Assigned Cube, Cube (1), Cube (2), and Cube (3) to QuizManager's objectsWithOpenPanelOnClick field.");
                    }
                    else
                    {
                        Debug.LogError("QuizManager script not found on QuizManager GameObject.");
                    }
                }
                else
                {
                    Debug.LogError("QuizManager GameObject not found.");
                }
            }
            else
            {
                Debug.LogError("KursiGuru prefab not found in the asset bundle.");
            }
        }
        else
        {
            Debug.LogWarning("KursiGuru has already been spawned.");
        }
    }

    void AddOpenPanelOnClickToChildren(GameObject parent)
    {
        string[] childNames = { "Cube", "Cube(1)", "Cube(2)", "Cube(3)", "Cube(4)", "Cube(5)", "Cube(6)" };

        foreach (var childName in childNames)
        {
            Transform childTransform = parent.transform.Find(childName);
            if (childTransform != null)
            {
                childTransform.gameObject.AddComponent<OpenPanelOnClick>();
            }
            else
            {
                Debug.LogWarning($"Child with name {childName} not found.");
            }
        }
    }
}
