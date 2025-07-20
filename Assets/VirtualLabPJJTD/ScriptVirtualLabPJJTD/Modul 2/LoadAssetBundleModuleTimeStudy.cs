using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LoadAssetBundleModuleTimeStudy : MonoBehaviour
{
    AssetBundle assetBundle;
    public Camera mainCamera; // Reference to the main camera
    public Module2Manager module2Manager; // Reference to Module2Manager script
    public TutorialModule2 tutorialModule2; // Reference to TutorialModule2 script
    public string tabelKomponenLegoTextureName = "TABEL KOMPONEN LEGO"; 
    public string workStationTextureName = "WORK STATION"; 
    public string tabelPerakitanTextureName = "Tabel Perakitan";
    public string tabelWPRTextureName = "Tabel WPR";
    public string tabelAllowanceTextureName = "Tabel Allowance";

    void Update()
    {
        // Check and disable the BucketBadanMobil1 collider if needed
        CheckAndDisableBucketCollider();
    }

    void Start()
    {
        LoadAssetBundle();

        // Ensure the main camera is referenced
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Find the Module2Manager in the scene, if not already assigned
        if (module2Manager == null)
        {
            module2Manager = FindObjectOfType<Module2Manager>();
        }

        // Find the TutorialModule2 in the scene, if not already assigned
        if (tutorialModule2 == null)
        {
            tutorialModule2 = FindObjectOfType<TutorialModule2>();
        }
    }

    void LoadAssetBundle()
    {
        foreach (var child in MainData.instance.pathFileMain)
        {
            AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(System.IO.File.ReadAllBytes(child));
            createRequest.completed += (asyncOperation) =>
            {
                assetBundle = createRequest.assetBundle;

                // Load Bucket Parts and their corresponding Part prefabs from the AssetBundle for both Module2Manager and TutorialModule2
                AddBucketPartToModule2Manager("BucketBadanMobil1", "BadanMobil1");
                AddBucketPartToModule2Manager("BucketBadanMobil2", "BadanMobil2");
                AddBucketPartToModule2Manager("BucketBelakangMobil", "BelakangMobil");
                AddBucketPartToModule2Manager("BucketMobilDepan", "MobilDepan");
                AddBucketPartToModule2Manager("BucketStir", "Stir");
                AddBucketPartToModule2Manager("BucketPinggirMobil", "PinggirMobil");
                AddBucketPartToModule2Manager("BucketSisiMobil", "SisiMobil");
                AddBucketPartToModule2Manager("BucketAtapMobil", "AtapMobil");
                AddBucketPartToModule2Manager("BucketBan", "Ban");
                AddBucketPartToModule2Manager("BucketBucket", "Sekop");
                AddBucketPartToModule2Manager("BucketPartCrane", "PartCrane");

                // Load Meja and Truck prefabs
                LoadMejaAndTruck();

                // Load "LabAntro" prefab (LabAwal) and instantiate it
                LoadLabAntro();

                // Assign the textures to the Canvas images
                AssignTexturesToImages();

                TutorialModule2.instance.ShowFirstPanel();
            };
        }
    }

    void AddBucketPartToModule2Manager(string bucketPartName, string partName)
    {
        if (module2Manager == null && tutorialModule2 == null) return;

        var bucketPartPrefab = assetBundle.LoadAsset<GameObject>(bucketPartName);
        var partPrefab = assetBundle.LoadAsset<GameObject>(partName);

        if (bucketPartPrefab != null && partPrefab != null)
        {
            // Add to Module2Manager if it's available
            if (module2Manager != null)
            {
                var bucketPart = new BucketPartData(); // Using BucketPartData for Module2Manager

                bucketPart.BucketPart = bucketPartPrefab;
                bucketPart.Part = partPrefab;

                var bucketPartsList = module2Manager.BucketParts.ToList(); // Convert array to a list for modification
                bucketPartsList.Add(bucketPart); // Add the new bucket part
                module2Manager.BucketParts = bucketPartsList.ToArray(); // Convert back to an array

                Debug.Log($"{bucketPartName} and {partName} added to the Module2Manager Bucket Parts array.");
            }

            // Add to TutorialModule2 if it's available
            if (tutorialModule2 != null)
            {
                var tutorialBucketPart = new TutorialBucketPartTutorialData(); // Using TutorialBucketPartTutorialData for TutorialModule2

                tutorialBucketPart.BucketPart = bucketPartPrefab;
                tutorialBucketPart.Part = partPrefab;

                var tutorialBucketPartsList = tutorialModule2.BucketParts.ToList(); // Convert array to a list for modification
                tutorialBucketPartsList.Add(tutorialBucketPart); // Add the new bucket part
                tutorialModule2.BucketParts = tutorialBucketPartsList.ToArray(); // Convert back to an array

                Debug.Log($"{bucketPartName} and {partName} added to the TutorialModule2 Bucket Parts array.");
            }
        }
        else
        {
            Debug.LogError($"Failed to load {bucketPartName} or {partName} from the AssetBundle.");
        }
    }

    void LoadMejaAndTruck()
    {
        // Load the "MejaModul2" prefab
        var prefabMeja = assetBundle.LoadAsset<GameObject>("MejaModul2");

        if (prefabMeja != null)
        {
            // Assign the prefab to the MejaPrefab field in Module2Manager
            if (module2Manager != null)
            {
                module2Manager.MejaPrefab = prefabMeja;
                Debug.Log("MejaModul2 prefab assigned to Module2Manager successfully.");
            }

            // Assign the prefab to the MejaPrefab field in TutorialModule2
            if (tutorialModule2 != null)
            {
                tutorialModule2.MejaPrefab = prefabMeja;
                Debug.Log("MejaModul2 prefab assigned to TutorialModule2 successfully.");
            }
        }
        else
        {
            Debug.LogError("MejaModul2 prefab not found in the AssetBundle.");
        }
        var searchAllTMP = prefabMeja.GetComponentsInChildren<TextMeshPro>();

        foreach (var childTMP in searchAllTMP)
        {
            ReplaceShaderForEditor(childTMP.fontSharedMaterial);
        }

        // Load the "Truck" prefab
        var prefabTruck = assetBundle.LoadAsset<GameObject>("Truck");

        if (prefabTruck != null)
        {
            // Assign the prefab to the TruckPrefab field in TutorialModule2
            if (tutorialModule2 != null)
            {
                tutorialModule2.truckPrefab = prefabTruck;
                Debug.Log("Truck prefab assigned to TutorialModule2 successfully.");
            }
        }
        else
        {
            Debug.LogError("Truck prefab not found in the AssetBundle.");
        }
    }

    void LoadLabAntro()
    {
        // Load the "LabAntro" prefab from the AssetBundle
        var prefabRoomAntro = assetBundle.LoadAsset<GameObject>("LabAntro");

        if (prefabRoomAntro != null)
        {
            // Instantiate the LabAntro prefab
            var instantRoom = Instantiate(prefabRoomAntro);
            Debug.Log("LabAntro prefab instantiated.");

            // Optionally, update shaders for TMP components in the prefab
            var searchAllTMP = instantRoom.GetComponentsInChildren<TextMeshPro>();

            foreach (var childTMP in searchAllTMP)
            {
                ReplaceShaderForEditor(childTMP.fontSharedMaterial);
            }

            AssignLecturerToWorkPerformanceRating(instantRoom);
        }
        else
        {
            Debug.LogError("LabAntro prefab not found in the AssetBundle.");
        }
    }

    void AssignLecturerToWorkPerformanceRating(GameObject labAntroInstance)
    {
        // Find the Lecturer object inside the instantiated LabAntro(Clone)
        Transform lecturerTransform = labAntroInstance.transform.Find("Lecturer");
        if (lecturerTransform != null)
        {
            // Find the WorkPerformanceRating script in the scene
            WorkPerformanceRating workPerformanceRating = FindObjectOfType<WorkPerformanceRating>();

            if (workPerformanceRating != null)
            {
                // Assign the Lecturer GameObject to the WorkPerformanceRating script
                workPerformanceRating.lecturerObject = lecturerTransform.gameObject;
                Debug.Log("Lecturer object successfully assigned to WorkPerformanceRating.");
            }
            else
            {
                Debug.LogError("WorkPerformanceRating script not found in the scene.");
            }
        }
        else
        {
            Debug.LogError("Lecturer object not found inside LabAntro(Clone).");
        }
    }

    private void OnDestroy()
    {
        if (assetBundle != null)
        {
            assetBundle.Unload(true);
        }
    }

    public static void ReplaceShaderForEditor(Material material)
    {
        if (material == null) return;

        var shaderName = material.shader.name;
        var shader = Shader.Find(shaderName);

        Debug.Log(material);

        if (shader != null) material.shader = shader;
    }

    void AssignTexturesToImages()
    {
        AssignTextureToImage("ConversationCanvas/NextTeks (8)/Panel/Image", tabelKomponenLegoTextureName);
        AssignTextureToImage("ConversationCanvas/NextTeks (9)/Panel/Image", tabelKomponenLegoTextureName);
        AssignTextureToImage("ConversationCanvas/NextTeks (10)/Panel/Image", workStationTextureName);
        AssignTextureToImage("WorkPerformanceRatingPanel/WPR Data", tabelWPRTextureName);
        AssignTextureToImage("SoalModulCanvas/Soal/Soal3/Tabel", tabelWPRTextureName);
        AssignTextureToImage("SoalModulCanvas/Soal/Soal1/Tabel", tabelPerakitanTextureName);
        AssignTextureToImage("SoalModulCanvas/Soal/Soal4/Tabel", tabelAllowanceTextureName);
        AssignTextureToImage("RecomendedAllowancePanel/ILO Data", tabelAllowanceTextureName);
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
                    Debug.Log($"{textureName} texture successfully assigned to the Image at {imagePath}.");
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

    // Helper method to find an inactive GameObject by path
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

    void CheckAndDisableBucketCollider()
    {
        // Find TruckDone in the scene
        GameObject truckDone = GameObject.Find("TruckDone");

        if (truckDone != null)
        {
            // Dictionary of part names with standard single-instance buckets
            Dictionary<string, string> partBucketPairs = new Dictionary<string, string>()
        {
            { "BadanMobil1(Clone)", "BucketBadanMobil1(Clone)" },
            { "BadanMobil2(Clone)", "BucketBadanMobil2(Clone)" },
            { "BelakangMobil(Clone)", "BucketBelakangMobil(Clone)" },
            { "MobilDepan(Clone)", "BucketMobilDepan(Clone)" },
            { "Stir(Clone)", "BucketStir(Clone)" },
            { "AtapMobil(Clone)", "BucketAtapMobil(Clone)" },
            { "Sekop(Clone)", "BucketBucket(Clone)" },
            { "PartCrane(Clone)", "BucketPartCrane(Clone)" }
        };

            // Check single-instance parts and disable their corresponding bucket colliders
            foreach (var pair in partBucketPairs)
            {
                string partName = pair.Key;
                string bucketName = pair.Value;

                // Check if TruckDone has a child with the specified part name
                if (truckDone.transform.Find(partName) != null)
                {
                    // Find the corresponding Bucket object with the "Tutorial" tag
                    GameObject bucketObject = GameObject.Find(bucketName);

                    if (bucketObject != null && bucketObject.CompareTag("Tutorial"))
                    {
                        // Disable the collider on the Bucket object
                        Collider bucketCollider = bucketObject.GetComponent<Collider>();
                        if (bucketCollider != null)
                        {
                            bucketCollider.enabled = false;
                            DestroyChildObject(bucketObject, partName); // Destroy the part inside the bucket
                        }
                    }
                }
            }

            // Special handling for parts with quantity limits
            CheckAndDisableBucketWithLimit(truckDone, "PinggirMobil(Clone)", "BucketPinggirMobil(Clone)", 2);
            CheckAndDisableBucketWithLimit(truckDone, "SisiMobil(Clone)", "BucketSisiMobil(Clone)", 2);
            CheckAndDisableBucketWithLimit(truckDone, "Ban(Clone)", "BucketBan(Clone)", 4);
        }
    }

    // Helper method to check quantity-limited parts and disable their corresponding bucket collider if it has the "Tutorial" tag
    void CheckAndDisableBucketWithLimit(GameObject truckDone, string partName, string bucketName, int limit)
    {
        // Count the number of children with the specified part name in TruckDone
        int partCount = truckDone.transform.Cast<Transform>().Count(child => child.name == partName);

        // If the count meets or exceeds the limit, disable the collider for the specified bucket if it has the "Tutorial" tag
        if (partCount >= limit)
        {
            GameObject bucketObject = GameObject.Find(bucketName);
            if (bucketObject != null && bucketObject.CompareTag("Tutorial"))
            {
                Collider bucketCollider = bucketObject.GetComponent<Collider>();
                if (bucketCollider != null)
                {
                    bucketCollider.enabled = false;
                    DestroyChildObject(bucketObject, partName); // Destroy the part inside the bucket
                }
            }
        }
    }

    // Helper method to find and destroy the specified child object within a parent bucket object
    void DestroyChildObject(GameObject bucketObject, string originalChildObjectName)
    {
        // Determine the actual child object name based on the bucket object name
        string childObjectName;

        if (bucketObject.name == "BucketBadanMobil2(Clone)")
        {
            childObjectName = "BadanMobil"; // For BucketBadanMobil2, target "BadanMobil" child
        }
        else
        {
            // Remove "(Clone)" from the original child object name if it exists
            childObjectName = originalChildObjectName.Replace("(Clone)", "");
        }

        // Search for the child object by the determined name
        Transform childTransform = bucketObject.transform.Find(childObjectName);
        if (childTransform != null)
        {
            Destroy(childTransform.gameObject);
        }
    }
}
