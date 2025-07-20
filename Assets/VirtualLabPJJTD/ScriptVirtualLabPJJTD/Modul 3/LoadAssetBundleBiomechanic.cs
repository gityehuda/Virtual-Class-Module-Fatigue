using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LoadAssetBundleBiomechanic : MonoBehaviour
{
    AssetBundle assetBundle;
    public Camera mainCamera;
    private GameObject lecturerObject;
    public Module3Manager module3Manager;
    public GameObject conversationCanvas;  
    public GameObject module3Canvas;

    public string couplingTypeTextureName = "CouplingType";
    public string liftingIndexTextureName = "LiftingIndex";
    public string tabelCouplingTextureName = "TabelCoupling";
    public string tabelWorkDurationTextureName = "TabelWorkDuration";
    public string TabelCNSFTextureName = "TabelCNSF";
    public string TabelNIOSHTextureName = "TabelNIOSH";


    void Start()
    {
        LoadAssetBundle();

        // Ensure the main camera is referenced
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        // Check if any ConversationAfterSimulation or ConversationFinish panel is active
        bool isConversationActive = IsConversationPanelActive();

        HandleReplayPanel();
    }

    void LoadAssetBundle()
    {
        foreach (var child in MainData.instance.pathFileMain)
        {
            AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(child));

            assetBundle = createRequest.assetBundle;

            // Load LabAntro prefab
            var prefabRoom = assetBundle.LoadAsset<GameObject>("LabAntro");
            var instantRoom = Instantiate(prefabRoom);

            // Load AnimasiModul 3 prefab
            var prefabAnimasiModul3 = assetBundle.LoadAsset<GameObject>("AnimasiModul 3");
            var prefabNoAnimationModule3 = assetBundle.LoadAsset<GameObject>("NoAnimationModule 3");

            // Load AnimasiModul 3 Ke 2 prefab
            var prefabAnimasiModul3Ke2 = assetBundle.LoadAsset<GameObject>("AnimasiModul 3 Ke 2");

            var prefabAnimasiModul3Ke3 = assetBundle.LoadAsset<GameObject>("AnimasiModul 3 Ke 3");

            // Add Module3DrawLine script to prefabNoAnimationModule3
            if (prefabNoAnimationModule3 != null)
            {
                Module3DrawLine module3DrawLine = prefabNoAnimationModule3.AddComponent<Module3DrawLine>();

                // Load the Line material from the asset bundle
                Material lineMaterial = assetBundle.LoadAsset<Material>("Line");
                if (lineMaterial != null)
                {
                    // Assign the loaded material to the Line Material field in Module3DrawLine
                    module3DrawLine.lineMaterial = lineMaterial;
                }
                else
                {
                    Debug.LogWarning("Line material not found in the asset bundle.");
                }

                // Set the next prefab to spawn
                if (prefabAnimasiModul3Ke2 != null)
                {
                    module3DrawLine.nextPrefabToSpawn = prefabAnimasiModul3Ke2;
                }
                else
                {
                    Debug.LogWarning("AnimasiModul3Ke2 prefab not found in the asset bundle.");
                }

                // Assign PointPairs in Module3DrawLine
                AssignPointPairs(module3DrawLine, prefabNoAnimationModule3);
            }

            // Add Module3DrawLine script to AnimasiModul 3 Ke 2
            if (prefabAnimasiModul3Ke2 != null)
            {
                Module3DrawLine module3DrawLineKe2 = prefabAnimasiModul3Ke2.AddComponent<Module3DrawLine>();

                // Load the Line material from the asset bundle
                Material lineMaterial = assetBundle.LoadAsset<Material>("Line");
                if (lineMaterial != null)
                {
                    // Assign the loaded material to the Line Material field in Module3DrawLine
                    module3DrawLineKe2.lineMaterial = lineMaterial;
                }
                else
                {
                    Debug.LogWarning("Line material not found in the asset bundle.");
                }

                // Assign PointPairs in Module3DrawLine for AnimasiModul 3 Ke 2
                AssignPointPairs(module3DrawLineKe2, prefabAnimasiModul3Ke2);
            }

            if (prefabAnimasiModul3Ke3 != null)
            {
                Module3DrawLine module3DrawLineKe3 = prefabAnimasiModul3Ke3.AddComponent<Module3DrawLine>();

                // Load the Line material from the asset bundle
                Material lineMaterial = assetBundle.LoadAsset<Material>("Line");
                if (lineMaterial != null)
                {
                    // Assign the loaded material to the Line Material field in Module3DrawLine
                    module3DrawLineKe3.lineMaterial = lineMaterial;
                }
                else
                {
                    Debug.LogWarning("Line material not found in the asset bundle.");
                }

                // Assign PointPairs in Module3DrawLine for AnimasiModul 3 Ke 2
                AssignPointPairs(module3DrawLineKe3, prefabAnimasiModul3Ke3);
            }

            // Assign prefabs to Module3Manager
            if (module3Manager != null)
            {
                module3Manager.prefabToSpawn = prefabAnimasiModul3;  // Assign AnimasiModul 3 prefab
                module3Manager.NoAnimationPrefab = prefabNoAnimationModule3;  // Assign NoAnimationModule 3 prefab
                module3Manager.spawnToPrefabTwo = prefabAnimasiModul3Ke3;  // Assign AnimationModule 3 ke 3 prefab
            }
            else
            {
                Debug.LogWarning("Module3Manager is not assigned.");
            }

            // Replace shader for TextMeshPro components in the instantiated prefab
            var searchAllTMP = instantRoom.GetComponentsInChildren<TextMeshPro>();
            foreach (var child2 in searchAllTMP)
            {
                ReplaceShaderForEditor(child2.fontSharedMaterial);
            }

            // Find Lecturer object
            lecturerObject = instantRoom.transform.Find("Lecturer")?.gameObject;
            if (lecturerObject == null)
            {
                Debug.LogWarning("Lecturer object not found in LabAntro prefab.");
            }

            // Instantiate and add RotateObject script to the relevant objects
            AddRotateObjectToPrefab(prefabAnimasiModul3);
            AddRotateObjectToPrefab(prefabNoAnimationModule3);
            AddRotateObjectToPrefab(prefabAnimasiModul3Ke2);
            AddRotateObjectToPrefab(prefabAnimasiModul3Ke3);

            AssignTexturesToImages();
        }
    }

    void AssignTexturesToImages()
    {
        AssignTextureToImage("ConversationCanvas/ImageConversationBeforeSecondSimulation", tabelWorkDurationTextureName);
        AssignTextureToImage("ConversationCanvas/ImageConversationBeforeSecondSimulation 2", tabelCouplingTextureName);
        AssignTextureToImage("ConversationCanvas/ImageConversationBeforeSecondSimulation 4&5", couplingTypeTextureName);
        AssignTextureToImage("ConversationCanvas/ImageConversationBeforeSecondSimulation 17 & 18", liftingIndexTextureName);
        AssignTextureToImage("SoalCanvas/Tabel CNSF/Tabel CNSF", TabelCNSFTextureName);
        AssignTextureToImage("SoalCanvas/Tabel NIOSH", TabelNIOSHTextureName);
    }

    // Helper method to assign the texture to a specific Image, including inactive objects
    void AssignTextureToImage(string imagePath, string textureName)
    {
        GameObject imageObject = FindInactiveObjectByPath(imagePath);

        if (imageObject != null)
        {
            // Activate the object temporarily if it's inactive
            bool wasActive = imageObject.activeSelf;
            if (!wasActive)
            {
                imageObject.SetActive(true);
            }

            // Get the Image component
            Image imageComponent = imageObject.GetComponent<Image>();

            if (imageComponent != null)
            {
                // Load the texture from the AssetBundle
                Texture2D texture = assetBundle.LoadAsset<Texture2D>(textureName);

                if (texture != null)
                {
                    // Convert Texture2D to Sprite
                    Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    // Assign the Sprite to the Image component
                    imageComponent.sprite = newSprite;
                }
                else
                {
                    Debug.LogError($"Failed to load texture '{textureName}' from the AssetBundle.");
                }
            }
            else
            {
                Debug.LogError($"Image component not found on the GameObject at {imagePath}.");
            }

            // Return the object to its previous active state
            if (!wasActive)
            {
                imageObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError($"GameObject at {imagePath} not found.");
        }
    }

    GameObject FindInactiveObjectByPath(string path)
    {
        string[] objectNames = path.Split('/');
        Transform currentTransform = null;

        // Start with the root object (usually the first object in the hierarchy)
        currentTransform = GameObject.Find(objectNames[0])?.transform;

        // Traverse through the children
        for (int i = 1; i < objectNames.Length; i++)
        {
            if (currentTransform != null)
            {
                currentTransform = currentTransform.Find(objectNames[i]);
            }
            else
            {
                return null;
            }
        }

        return currentTransform?.gameObject;
    }

    void HandleReplayPanel()
    {
        if (module3Canvas == null) return;

        var replayPanel = module3Canvas.transform.Find("ReplayPanel");
        if (replayPanel != null && replayPanel.gameObject.activeSelf)
        {
            // Find AnimasiModul 3(Clone) in the scene
            var animasiModul3Clone = GameObject.Find("AnimasiModul 3(Clone)");
            if (animasiModul3Clone != null)
            {
                var rotateObject = animasiModul3Clone.GetComponent<RotateObject>();
                if (rotateObject != null && rotateObject.enabled)
                {
                    rotateObject.enabled = false; // Disable RotateObject
                }
            }
        }
    }


    void AssignPointPairs(Module3DrawLine module3DrawLine, GameObject prefab)
    {
        // Check the name of the prefab to handle different logic
        if (prefab.name == "NoAnimationModule 3")
        {
            // Find Start and End Points for NoAnimationModule 3
            GameObject startPoint1 = prefab.transform.Find("StartPoint 1")?.gameObject;
            GameObject endPoint1 = prefab.transform.Find("EndPoint 1")?.gameObject;

            GameObject startPoint2 = prefab.transform.Find("StartPoint 2")?.gameObject;
            GameObject endPoint2 = prefab.transform.Find("EndPoint 2")?.gameObject;

            GameObject startPoint3 = prefab.transform.Find("StartPoint 3")?.gameObject;
            GameObject endPoint3 = prefab.transform.Find("EndPoint 3")?.gameObject;

            // Assign PointPairs for NoAnimationModule 3
            if (module3DrawLine != null)
            {
                module3DrawLine.pointPairs = new PointPair[3];

                module3DrawLine.pointPairs[0] = new PointPair
                {
                    startPoint = startPoint1,
                    endPoint = endPoint1,
                    predefinedDistance = 24f
                };

                module3DrawLine.pointPairs[1] = new PointPair
                {
                    startPoint = startPoint2,
                    endPoint = endPoint2,
                    predefinedDistance = 75f
                };

                module3DrawLine.pointPairs[2] = new PointPair
                {
                    startPoint = startPoint3,
                    endPoint = endPoint3,
                    predefinedDistance = 0f
                };
            }
        }
        else if (prefab.name == "AnimasiModul 3 Ke 2")
        {
            // Find Start and End Points for AnimasiModul 3 Ke 2
            GameObject startPoint1 = prefab.transform.Find("StartPoint 1")?.gameObject;
            GameObject endPoint1 = prefab.transform.Find("EndPoint 1")?.gameObject;

            GameObject startPoint2 = prefab.transform.Find("StartPoint 2")?.gameObject;
            GameObject endPoint2 = prefab.transform.Find("EndPoint 2")?.gameObject;

            GameObject startPoint3 = prefab.transform.Find("StartPoint 3")?.gameObject;
            GameObject endPoint3 = prefab.transform.Find("EndPoint 3")?.gameObject;

            // Assign PointPairs for AnimasiModul 3 Ke 2
            if (module3DrawLine != null)
            {
                module3DrawLine.pointPairs = new PointPair[3];

                module3DrawLine.pointPairs[0] = new PointPair
                {
                    startPoint = startPoint1,
                    endPoint = endPoint1,
                    predefinedDistance = 34f  // Adjust the predefined distance as per AnimasiModul 3 Ke 2
                };

                module3DrawLine.pointPairs[1] = new PointPair
                {
                    startPoint = startPoint2,
                    endPoint = endPoint2,
                    predefinedDistance = 13f  // Adjust the predefined distance as per AnimasiModul 3 Ke 2
                };

                module3DrawLine.pointPairs[2] = new PointPair
                {
                    startPoint = startPoint3,
                    endPoint = endPoint3,
                    predefinedDistance = 45f  // Adjust the predefined distance as per AnimasiModul 3 Ke 2
                };
            }
        }
        else if (prefab.name == "AnimasiModul 3 Ke 3")
        {
            // Find Start and End Points for AnimasiModul 3 Ke 2
            GameObject startPoint1 = prefab.transform.Find("StartPoint 1")?.gameObject;
            GameObject endPoint1 = prefab.transform.Find("EndPoint 1")?.gameObject;

            GameObject startPoint2 = prefab.transform.Find("StartPoint 2")?.gameObject;
            GameObject endPoint2 = prefab.transform.Find("EndPoint 2")?.gameObject;

            GameObject startPoint3 = prefab.transform.Find("StartPoint 3 ")?.gameObject; //Ingat diganti jika nama di asset dibaikin
            GameObject endPoint3 = prefab.transform.Find("EndPoint 3")?.gameObject;

            // Assign PointPairs for AnimasiModul 3 Ke 2
            if (module3DrawLine != null)
            {
                module3DrawLine.pointPairs = new PointPair[3];

                module3DrawLine.pointPairs[0] = new PointPair
                {
                    startPoint = startPoint1,
                    endPoint = endPoint1,
                    predefinedDistance = 31f  // Adjust the predefined distance as per AnimasiModul 3 Ke 2
                };

                module3DrawLine.pointPairs[1] = new PointPair
                {
                    startPoint = startPoint2,
                    endPoint = endPoint2,
                    predefinedDistance = 59f  // Adjust the predefined distance as per AnimasiModul 3 Ke 2
                };

                module3DrawLine.pointPairs[2] = new PointPair
                {
                    startPoint = startPoint3,
                    endPoint = endPoint3,
                    predefinedDistance = 31f  // Adjust the predefined distance as per AnimasiModul 3 Ke 2
                };
            }
        }
        else
        {
            Debug.LogWarning("Unknown prefab type: " + prefab.name);
        }
    }



    void AddRotateObjectToPrefab(GameObject prefab)
    {
        if (prefab != null)
        {
            // Check the name of the prefab and add RotateObject component if it matches
            if (prefab.name == "AnimasiModul 3" || prefab.name == "NoAnimationModule 3" || prefab.name == "AnimasiModul 3 Ke 2" || prefab.name == "AnimasiModul 3 Ke 3")
            {
                // Add RotateObject script to the prefab instance
                var rotateObjectScript = prefab.AddComponent<RotateObject>();
            }
        }
    }

    private bool IsConversationPanelActive()
    {
        foreach (Transform child in conversationCanvas.transform)
        {
            // Check for either ConversationAfterSimulation or ConversationFinish panel being active
            if ((child.gameObject.name.Contains("conversationAfterTheFirstSimulation") ||
                 child.gameObject.name.Contains("ConversationFinish")) &&
                child.gameObject.activeSelf)
            {
                return true;  // Found an active ConversationAfterSimulation or ConversationFinish panel
            }
        }
        return false;  // No active ConversationAfterSimulation panel found
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

        if (shader != null) material.shader = shader;
    }
}
