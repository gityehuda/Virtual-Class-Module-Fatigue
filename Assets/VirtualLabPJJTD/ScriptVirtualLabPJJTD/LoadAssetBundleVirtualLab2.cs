//using DialogueEditor;
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

public class LoadAssetBundleVirtualLab2 : MonoBehaviour
{
    // Room Antro
    AssetBundle assetBundle;
    //public NPCConversation myConversation;
    public GameObject panel; // Reference to the Panel GameObject in the scene
    public AudioClip snapSound;

    // Start is called before the first frame update
    void Start()
    {
        LoadAssetBundle();
    }

    void LoadAssetBundle()
    {
        foreach (var child in MainData.instance.pathFileMain)
        {
            AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(System.IO.File.ReadAllBytes(child));
            createRequest.completed += (asyncOperation) =>
            {
                assetBundle = createRequest.assetBundle;

                var prefabRoomAntro = assetBundle.LoadAsset<GameObject>("LabAntro");
                var instantRoom = Instantiate(prefabRoomAntro);
                var dummyAntroPrefab = assetBundle.LoadAsset<GameObject>("DummyAntro");
                dummyAntroPrefab.tag = "Bot";

                var searchAllTMP = instantRoom.GetComponentsInChildren<TMPro.TextMeshPro>();

                foreach (var child2 in searchAllTMP)
                {
                    ReplaceShaderForEditor(child2.fontSharedMaterial);
                }

                GameObject tutorAntro = GameObject.Find("TutorAntro");
                if (tutorAntro != null)
                {
                    var tutorAntroScript = tutorAntro.GetComponent<TutorAntro>();
                    if (tutorAntroScript != null)
                    {
                        // Assign the DummyAntro prefab to the prefabToSpawn field
                        tutorAntroScript.prefabToSpawn = dummyAntroPrefab;
                    }
                    else
                    {
                        Debug.LogWarning("TutorAntro component not found on TutorAntro GameObject.");
                    }
                }
                // Load the prefabs
                // Male Asian Standing
                var maleAsianStandingPrefab = assetBundle.LoadAsset<GameObject>("MaleAsianStandingPAwal");
                var maleAsianStandingP1Prefab = assetBundle.LoadAsset<GameObject>("MaleAsianStandingP1");
                var maleAsianStandingP2Prefab = assetBundle.LoadAsset<GameObject>("MaleAsianStandingP2");
                var maleAsianStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("MaleAsianStandingTPose");
                // Male Asian Sitdown
                var maleAsianSitdownPrefab = assetBundle.LoadAsset<GameObject>("MaleAsianSitdownPAwal");
                var maleAsianSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("MaleAsianSitdownP1");
                var maleAsianSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("MaleAsianSitdownP2");
                // Female Asian Standing
                var femaleAsianStandingPrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianStandingPAwal");
                var femaleAsianStandingP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleAsianStandingP1");
                var femaleAsianStandingP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleAsianStandingP2");
                var femaleAsianStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianStandingTPose");
                // Female Asian Sitdown
                var femaleAsianSitdownPrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianSitdownPAwal");
                var femaleAsianSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleAsianSitdownP1");
                var femaleAsianSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleAsianSitdownP2");
                // Male Americas Standing
                var maleAmericanStandingPrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanStandingPAwal");
                var maleAmericanStandingP1Prefab = assetBundle.LoadAsset<GameObject>("MaleAmericanStandingP1");
                var maleAmericanStandingP2Prefab = assetBundle.LoadAsset<GameObject>("MaleAmericanStandingP2");
                var maleAmericanStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanStandingTPose");
                // Male Americas Sitdown
                var maleAmericanSitdownPrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanSitdownPAwal");
                var maleAmericanSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("MaleAmericanSitdownP1");
                var maleAmericanSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("MaleAmericanSitdownP2");
                // Female Americas Standing
                var femaleAmericanStandingPrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanStandingPAwal");
                var femaleAmericanStandingP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanStandingP1");
                var femaleAmericanStandingP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanStandingP2");
                var femaleAmericanStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanStandingTPose");
                // Female Americas Sitdown
                var femaleAmericanSitdownPrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanSitdownPAwal");
                var femaleAmericanSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanSitdownP1");
                var femaleAmericanSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanSitdownP2");
                // Male Eropean Standing
                var maleEuStandingPrefab = assetBundle.LoadAsset<GameObject>("MaleEuStandingPAwal");
                var maleEuStandingP1Prefab = assetBundle.LoadAsset<GameObject>("MaleEuStandingP1");
                var maleEuStandingP2Prefab = assetBundle.LoadAsset<GameObject>("MaleEuStandingP2");
                var maleEuStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("MaleEuStandingTPose");
                // Male Eropean Sitdown
                var maleEuSitdownPrefab = assetBundle.LoadAsset<GameObject>("MaleEuSitdownPAwal");
                var maleEuSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("MaleEuSitdownP1");
                var maleEuSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("MaleEuSitdownP2");
                // Female Eropean Standing
                var femaleEuStandingPrefab = assetBundle.LoadAsset<GameObject>("FemaleEuStandingPAwal");
                var femaleEuStandingP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleEuStandingP1");
                var femaleEuStandingP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleEuStandingP2");
                var femaleEuStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("FemaleEuStandingTPose");
                // Female Eropean Sitdown
                var femaleEuSitdownPrefab = assetBundle.LoadAsset<GameObject>("FemaleEuSitdownPAwal");
                var femaleEuSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleEuSitdownP1");
                var femaleEuSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleEuSitdownP2");
                // Male Asian Hand & Feet
                var maleAsianHandPrefab = assetBundle.LoadAsset<GameObject>("MaleAsianHand");
                var maleAsianFeetPrefab = assetBundle.LoadAsset<GameObject>("MaleAsianFeet");
                // Male American Hand & Feet
                var maleAmericanHandPrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanHand");
                var maleAmericanFeetPrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanFeet");
                // Male Eropean Hand & Feet
                var maleEuHandPrefab = assetBundle.LoadAsset<GameObject>("MaleEuHand");
                var maleEuFeetPrefab = assetBundle.LoadAsset<GameObject>("MaleEuFeet");
                // Female Asian Hand & Feet
                var femaleAsianHandPrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianHand");
                var femaleAsianFeetPrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianFeet");
                // Female American Hand & Feet
                var femaleAmericanHandPrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanHand");
                var femaleAmericanFeetPrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanFeet");
                // Female Eropean Hand & Feet
                var femaleEuHandPrefab = assetBundle.LoadAsset<GameObject>("FemaleEuHand");
                var femaleEuFeetPrefab = assetBundle.LoadAsset<GameObject>("FemaleEuFeet");

                var maleAsianPrefab = assetBundle.LoadAsset<GameObject>("MaleAsian");
                var femaleAsianPrefab = assetBundle.LoadAsset<GameObject>("FemaleAsian");
                var maleAmericanPrefab = assetBundle.LoadAsset<GameObject>("MaleAmerican");
                var femaleAmericanPrefab = assetBundle.LoadAsset<GameObject>("FemaleAmerican");
                var maleEuPrefab = assetBundle.LoadAsset<GameObject>("MaleEu");
                var femaleEuPrefab = assetBundle.LoadAsset<GameObject>("FemaleEu");


                // Find the Panel GameObject and get its CharacterSelectionPanel component
                if (panel != null)
                {
                    var characterSelectionPanel = panel.GetComponent<CharacterSelectionPanel>();
                    if (characterSelectionPanel != null)
                    {
                        // Assign the prefabs to the CharacterSelectionPanel component
                        characterSelectionPanel.maleAsianStandingPrefab = AddObjectControlComponent(maleAsianStandingPrefab);
                        characterSelectionPanel.maleAsianSitdownPrefab = AddObjectControlComponent(maleAsianSitdownPrefab);
                        characterSelectionPanel.femaleAsianStandingPrefab = AddObjectControlComponent(femaleAsianStandingPrefab);
                        characterSelectionPanel.femaleAsianSitdownPrefab = AddObjectControlComponent(femaleAsianSitdownPrefab);
                        characterSelectionPanel.maleEuropeStandingPrefab = AddObjectControlComponent(maleEuStandingPrefab);
                        characterSelectionPanel.maleEuropeSitdownPrefab = AddObjectControlComponent(maleEuSitdownPrefab);
                        characterSelectionPanel.femaleEuropeStandingPrefab = AddObjectControlComponent(femaleEuStandingPrefab);
                        characterSelectionPanel.femaleEuropeSitdownPrefab = AddObjectControlComponent(femaleEuSitdownPrefab);
                        characterSelectionPanel.maleAmericasStandingPrefab = AddObjectControlComponent(maleAmericanStandingPrefab);
                        characterSelectionPanel.maleAmericasSitdownPrefab = AddObjectControlComponent(maleAmericanSitdownPrefab);
                        characterSelectionPanel.femaleAmericasStandingPrefab = AddObjectControlComponent(femaleAmericanStandingPrefab);
                        characterSelectionPanel.femaleAmericasSitdownPrefab = AddObjectControlComponent(femaleAmericanSitdownPrefab);

                        characterSelectionPanel.maleAsianPrefab = (maleAsianPrefab);
                        characterSelectionPanel.femaleAsianPrefab = (femaleAsianPrefab);
                        characterSelectionPanel.maleAmericanPrefab = (maleAmericanPrefab);
                        characterSelectionPanel.femaleAmericanPrefab = (femaleAmericanPrefab);
                        characterSelectionPanel.maleEuPrefab = (maleEuPrefab);
                        characterSelectionPanel.femaleEuPrefab = (femaleEuPrefab);

                        // Add AntroControl2 and Reset components
                        // Male Asian Standing
                        AddAntroControl2Component(maleAsianStandingPrefab);
                        AddAntroControl2Component(maleAsianStandingP1Prefab);
                        AddAntroControl2Component(maleAsianStandingP2Prefab);
                        AddAntroControl2Component(maleAsianStandingTPosePrefab);
                        // Male Asian Sitdown
                        AddAntroControl2Component(maleAsianSitdownPrefab);
                        AddAntroControl2Component(maleAsianSitdownP1Prefab);
                        AddAntroControl2Component(maleAsianSitdownP2Prefab);
                        // Female Asian Standing
                        AddAntroControl2Component(femaleAsianStandingPrefab);
                        AddAntroControl2Component(femaleAsianStandingP1Prefab);
                        AddAntroControl2Component(femaleAsianStandingP2Prefab);
                        AddAntroControl2Component(femaleAsianStandingTPosePrefab);
                        // Female Asian Sitdown
                        AddAntroControl2Component(femaleAsianSitdownPrefab);
                        AddAntroControl2Component(femaleAsianSitdownP1Prefab);
                        AddAntroControl2Component(femaleAsianSitdownP2Prefab);
                        // Male Eu Standing
                        AddAntroControl2Component(maleEuStandingPrefab);
                        AddAntroControl2Component(maleEuStandingP1Prefab);
                        AddAntroControl2Component(maleEuStandingP2Prefab);
                        AddAntroControl2Component(maleEuStandingTPosePrefab);
                        // Male Eu Sitdown
                        AddAntroControl2Component(maleEuSitdownPrefab);
                        AddAntroControl2Component(maleEuSitdownP1Prefab);
                        AddAntroControl2Component(maleEuSitdownP2Prefab);
                        // Female Eu Standing
                        AddAntroControl2Component(femaleEuStandingPrefab);
                        AddAntroControl2Component(femaleEuStandingP1Prefab);
                        AddAntroControl2Component(femaleEuStandingP2Prefab);
                        AddAntroControl2Component(femaleEuStandingTPosePrefab);
                        // Female Eu Sitdown
                        AddAntroControl2Component(femaleEuSitdownPrefab);
                        AddAntroControl2Component(femaleEuSitdownP1Prefab);
                        AddAntroControl2Component(femaleEuSitdownP2Prefab);
                        // Male American Standing
                        AddAntroControl2Component(maleAmericanStandingPrefab);
                        AddAntroControl2Component(maleAmericanStandingP1Prefab);
                        AddAntroControl2Component(maleAmericanStandingP2Prefab);
                        AddAntroControl2Component(maleAmericanStandingTPosePrefab);
                        // Male American Sitdown
                        AddAntroControl2Component(maleAmericanSitdownPrefab);
                        AddAntroControl2Component(maleAmericanSitdownP1Prefab);
                        AddAntroControl2Component(maleAmericanSitdownP2Prefab);
                        // Female American Standing
                        AddAntroControl2Component(femaleAmericanStandingPrefab);
                        AddAntroControl2Component(femaleAmericanStandingP1Prefab);
                        AddAntroControl2Component(femaleAmericanStandingP2Prefab);
                        AddAntroControl2Component(femaleAmericanStandingTPosePrefab);
                        // Female American Sitdown
                        AddAntroControl2Component(femaleAmericanSitdownPrefab);
                        AddAntroControl2Component(femaleAmericanSitdownP1Prefab);
                        AddAntroControl2Component(femaleAmericanSitdownP2Prefab);
                        // Male Asian Hand & Feet
                        AddAntroControl2Component(maleAsianHandPrefab);
                        AddAntroControlComponent(maleAsianFeetPrefab);
                        // Male Eu Hand & Feet
                        AddAntroControl2Component(maleEuHandPrefab);
                        AddAntroControlComponent(maleEuFeetPrefab);
                        // Male American Hand & Feet
                        AddAntroControl2Component(maleAmericanHandPrefab);
                        AddAntroControlComponent(maleAmericanFeetPrefab);
                        // Female Asian Hand & Feet
                        AddAntroControl2Component(femaleAsianHandPrefab);
                        AddAntroControlComponent(femaleAsianFeetPrefab);
                        // Female Eu Hand & Feet
                        AddAntroControl2Component(femaleEuHandPrefab);
                        AddAntroControlComponent(femaleEuFeetPrefab);
                        // Female American Hand & Feet
                        AddAntroControl2Component(femaleAmericanHandPrefab);
                        AddAntroControlComponent(femaleAmericanFeetPrefab);

                        AddAntroControl3Component(dummyAntroPrefab);

                        // Male Asian Standing
                        AddResetComponent(maleAsianStandingPrefab);
                        AddResetComponent(maleAsianStandingP1Prefab);
                        AddResetComponent(maleAsianStandingP2Prefab);
                        AddResetComponent(maleAsianStandingTPosePrefab);
                        // Male Asian Sitdown
                        AddResetComponent(maleAsianSitdownPrefab);
                        AddResetComponent(maleAsianSitdownP1Prefab);
                        AddResetComponent(maleAsianSitdownP2Prefab);
                        // Female Asian Standing
                        AddResetComponent(femaleAsianStandingPrefab);
                        AddResetComponent(femaleAsianStandingP1Prefab);
                        AddResetComponent(femaleAsianStandingP2Prefab);
                        AddResetComponent(femaleAsianStandingTPosePrefab);
                        // Female Asian Sitdown
                        AddResetComponent(femaleAsianSitdownPrefab);
                        AddResetComponent(femaleAsianSitdownP1Prefab);
                        AddResetComponent(femaleAsianSitdownP2Prefab);
                        // Male Eu Standing
                        AddResetComponent(maleEuStandingPrefab);
                        AddResetComponent(maleEuStandingP1Prefab);
                        AddResetComponent(maleEuStandingP2Prefab);
                        AddResetComponent(maleEuStandingTPosePrefab);
                        // Male Eu Sitdown
                        AddResetComponent(maleEuSitdownPrefab);
                        AddResetComponent(maleEuSitdownP1Prefab);
                        AddResetComponent(maleEuSitdownP2Prefab);
                        // Female Eu Standing
                        AddResetComponent(femaleEuStandingPrefab);
                        AddResetComponent(femaleEuStandingP1Prefab);
                        AddResetComponent(femaleEuStandingP2Prefab);
                        AddResetComponent(femaleEuStandingTPosePrefab);
                        // Female Eu Sitdown
                        AddResetComponent(femaleEuSitdownPrefab);
                        AddResetComponent(femaleEuSitdownP1Prefab);
                        AddResetComponent(femaleEuSitdownP2Prefab);
                        // Male American Standing
                        AddResetComponent(maleAmericanStandingPrefab);
                        AddResetComponent(maleAmericanStandingP1Prefab);
                        AddResetComponent(maleAmericanStandingP2Prefab);
                        AddResetComponent(maleAmericanStandingTPosePrefab);
                        // Male American Sitdown
                        AddResetComponent(maleAmericanSitdownPrefab);
                        AddResetComponent(maleAmericanSitdownP1Prefab);
                        AddResetComponent(maleAmericanSitdownP2Prefab);
                        // Female American Standing
                        AddResetComponent(femaleAmericanStandingPrefab);
                        AddResetComponent(femaleAmericanStandingP1Prefab);
                        AddResetComponent(femaleAmericanStandingP2Prefab);
                        AddResetComponent(femaleAmericanStandingTPosePrefab);
                        // Female American Sitdown
                        AddResetComponent(femaleAmericanSitdownPrefab);
                        AddResetComponent(femaleAmericanSitdownP1Prefab);
                        AddResetComponent(femaleAmericanSitdownP2Prefab);
                        // Male Asian Hand & Feet
                        AddResetComponent(maleAsianHandPrefab);
                        AddResetComponent(maleAsianFeetPrefab);
                        // Male American Hand & Feet
                        AddResetComponent(maleAmericanHandPrefab);
                        AddResetComponent(maleAmericanFeetPrefab);
                        // Male Eu Hand & Feet
                        AddResetComponent(maleEuHandPrefab);
                        AddResetComponent(maleEuFeetPrefab);
                        // Female Asian Hand & Feet
                        AddResetComponent(femaleAsianHandPrefab);
                        AddResetComponent(femaleAsianFeetPrefab);
                        // Female Eu Hand & Feet
                        AddResetComponent(femaleEuHandPrefab);
                        AddResetComponent(femaleEuFeetPrefab);
                        // Female American Hand & Feet
                        AddResetComponent(femaleAmericanHandPrefab);
                        AddResetComponent(femaleAmericanFeetPrefab);

                        AddResetComponent(dummyAntroPrefab);

                        ConversationPanel.instance.ShowFirstPanel();
                    }
                    else
                    {
                        Debug.LogWarning("CharacterSelectionPanel component not found on Panel GameObject.");
                    }
                }
                else
                {
                    Debug.LogWarning("Panel GameObject not found.");
                }
            };
        }
    }

    private GameObject AddObjectControlComponent(GameObject prefab)
    {
        if (prefab != null)
        {
            // Add ObjectControl component
            var objectControl = prefab.AddComponent<ObjectControl>();
            if (objectControl != null)
            {
                objectControl.moveSpeed = 5f;
                objectControl.rotateSpeed = 3f;
                objectControl.zoomSpeed = 100f;
                objectControl.minX = 0.5f;
                objectControl.maxX = 4f;
                objectControl.minY = 1.5f;
                objectControl.maxY = 2.5f;
            }
            else
            {
                Debug.LogWarning("Failed to add ObjectControl component to prefab: " + prefab.name);
            }
        }
        else
        {
            Debug.LogWarning("Prefab is null.");
        }
        return prefab;
    }

    private void AddAntroControl2Component(GameObject prefab)
    {
        if (prefab != null)
        {
            // Add AntroControl2 component
            var antroControl2 = prefab.AddComponent<AntroControl2>();
            if (antroControl2 == null)
            {
                Debug.LogWarning("Failed to add AntroControl2 component to prefab: " + prefab.name);
                return;
            }

            // Set the Snap distance threshold to 0.1
            antroControl2.snapDistanceThreshold = 0.1f;

            // Set the object to destroy as the prefab itself
            antroControl2.objectToDestroy = prefab;

            // Set the object controller field to the prefab itself
            antroControl2.objectController = prefab.GetComponent<ObjectControl>();

            // Find the ConfirmationCanvas GameObject
            var confirmationCanvas = GameObject.Find("ConfirmationCanvas");
            if (confirmationCanvas != null)
            {
                // Find the Distance Text component in the ConfirmationCanvas child
                var distanceText = confirmationCanvas.transform.Find("Distance")?.GetComponent<TMPro.TextMeshProUGUI>();
                if (distanceText != null)
                {
                    // Set the Distance Text in AntroControl2
                    antroControl2.distanceText = distanceText;
                }
                else
                {
                    Debug.LogWarning("Distance Text not found in ConfirmationCanvas child.");
                }
                var exitButton = confirmationCanvas.transform.Find("Exit")?.GetComponent<UnityEngine.UI.Button>();
                var chooseButton = confirmationCanvas.transform.Find("Choose")?.GetComponent<UnityEngine.UI.Button>();
                var nextPointButton = GameObject.Find("AntroControl")?.transform.Find("NextPoint")?.GetComponent<UnityEngine.UI.Button>();

                antroControl2.buttonsToDisable = new UnityEngine.UI.Button[] { exitButton, chooseButton, nextPointButton };
            }
            else
            {
                Debug.LogWarning("ConfirmationCanvas GameObject not found.");
            }

            // Load the Line material from the asset bundle
            var lineMaterial = assetBundle.LoadAsset<Material>("Line");
            if (lineMaterial != null)
            {
                // Set the Line material of the AntroControl2 component
                antroControl2.lineMaterial = lineMaterial;
            }

            // Set the audio clip if it's assigned
            if (snapSound != null)
            {
                antroControl2.snapSound = snapSound; 
            }

            // Set specific properties based on the prefab name
            switch (prefab.name)
            {
                // Male Asian Standing
                case "MaleAsianStandingPAwal":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D1 (Stature)", distance = "169.35" },
                        new PredefinedDistance { label = "D2 (Eye Height)", distance = "157.53" },
                        new PredefinedDistance { label = "D3 (Shoulder Height)", distance = "137.88" },
                        new PredefinedDistance { label = "D4 (Elbow Height)", distance = "106.87" },
                        new PredefinedDistance { label = "D5 (Hip Height)", distance = "83.07" },
                        new PredefinedDistance { label = "D6 (Knuckle Height)", distance = "87.00" },
                        new PredefinedDistance { label = "D7 (fingertip Height)", distance = "65.33" }
                    };
                    SetStartAndEndPointsForMaleAsianStandingPAwal(antroControl2, prefab);
                    break;
                case "MaleAsianStandingP1":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D24 (Upper limb length)", distance = "72.01" },
                        new PredefinedDistance { label = "D25 (Shoulder-grip length)", distance = "65.00" },
                        new PredefinedDistance { label = "D36 (Head length)", distance = "69.09" }
                    };
                    SetStartAndEndPointsForMaleAsianStandingP1(antroControl2, prefab);
                    break;
                case "MaleAsianStandingP2":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D34 (Vertical grip reach (standing))", distance = "211.33" }
                    };
                    SetStartAndEndPointsForMaleAsianStandingP2(antroControl2, prefab);
                    break;
                case "MaleAsianStandingTPose":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D32 (Span)", distance = "165.71" },
                        new PredefinedDistance { label = "D33 (Elbow span)", distance = "82.23" }
                    };
                    SetStartAndEndPointsForMaleAsianStandingTPose(antroControl2, prefab);
                    break;
                // Male Asian Sitdown
                case "MaleAsianSitdownPAwal":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D27 (Head breadth)", distance = "14.71" },
                        new PredefinedDistance { label = "D18 (Shoulder breadth (biacromial))", distance = "38.23" },
                        new PredefinedDistance { label = "D17 (Shoulder breadth (bideltoid))", distance = "44.60" },
                        new PredefinedDistance { label = "D19 (Hip breadth)", distance = "32.13" },
                        new PredefinedDistance { label = "D26 (Head length)", distance = "21.80" },
                        new PredefinedDistance { label = "D20 (Chest (bust) depth)", distance = "24.51" },
                        new PredefinedDistance { label = "D21 (Abdominal depth)", distance = "22.71" },
                        new PredefinedDistance { label = "D13 (Buttock-knee length)", distance = "55.60" },
                        new PredefinedDistance { label = "D14 (Buttock-popliteal length)", distance = "45.83" }
                    };
                    SetStartAndEndPointsForMaleAsianSitdownPAwal(antroControl2, prefab);
                    break;
                case "MaleAsianSitdownP1":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D8 (Sitting height)", distance = "91.13" },
                        new PredefinedDistance { label = "D9 (Sitting eye height)", distance = "79.60" },
                        new PredefinedDistance { label = "D10 (Sitting shoulder height)", distance = "56.39" },
                        new PredefinedDistance { label = "D11 (Sitting elbow height)", distance = "26.53" },
                        new PredefinedDistance { label = "D12 (Thigh clearance)", distance = "16.69" },
                        new PredefinedDistance { label = "D16 (Popliteal height)", distance = "40.68" },
                        new PredefinedDistance { label = "D15 (Sitting knee height)", distance = "50.80" }
                    };
                    SetStartAndEndPointsForMaleAsianSitdownP1(antroControl2, prefab);
                    break;
                case "MaleAsianSitdownP2":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D23 (Elbow-fingertip length)", distance = "43.79" },
                        new PredefinedDistance { label = "D22 (Shoulder-elbow length)", distance = "31.00" },
                        new PredefinedDistance { label = "D35 (Vertical grip reach (sitting))", distance = "133.03" }
                    };
                    SetStartAndEndPointsForMaleAsianSitdownP2(antroControl2, prefab);
                    break;
                // Male Eu Standing
                case "MaleEuStandingPAwal":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D1 (Stature)", distance = "174.00" },
                        new PredefinedDistance { label = "D2 (Eye Height)", distance = "163.00" },
                        new PredefinedDistance { label = "D3 (Shoulder Height)", distance = "142.50" },
                        new PredefinedDistance { label = "D4 (Elbow Height)", distance = "109.00" },
                        new PredefinedDistance { label = "D5 (Hip Height)", distance = "92.00" },
                        new PredefinedDistance { label = "D6 (Knuckle Height)", distance = "75.50" },
                        new PredefinedDistance { label = "D7 (fingertip Height)", distance = "65.50" }
                    };
                    SetStartAndEndPointsForMaleEuStandingPAwal(antroControl2, prefab);
                    break;
                case "MaleEuStandingP1":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D24 (Upper limb length)", distance = "78.00" },
                        new PredefinedDistance { label = "D25 (Shoulder-grip length)", distance = "66.50" },
                        new PredefinedDistance { label = "D36 (Head length)", distance = "78.00" }
                    };
                    SetStartAndEndPointsForMaleEuStandingP1(antroControl2, prefab);
                    break;
                case "MaleEuStandingP2":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D34 (Vertical grip reach (standing))", distance = "206.00" }
                    };
                    SetStartAndEndPointsForMaleEuStandingP2(antroControl2, prefab);
                    break;
                case "MaleEuStandingTPose":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D32 (Span)", distance = "179.00" },
                        new PredefinedDistance { label = "D33 (Elbow span)", distance = "94.50" }
                    };
                    SetStartAndEndPointsForMaleEuStandingTPose(antroControl2, prefab);
                    break;
                // Male Eu Sitdown
                case "MaleEuSitdownPAwal":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D27 (Head breadth)", distance = "15.5" },
                        new PredefinedDistance { label = "D18 (Shoulder breadth (biacromial))", distance = "40" },
                        new PredefinedDistance { label = "D17 (Shoulder breadth (bideltoid))", distance = "46.5" },
                        new PredefinedDistance { label = "D19 (Hip breadth)", distance = "36" },
                        new PredefinedDistance { label = "D26 (Head length)", distance = "19.5" },
                        new PredefinedDistance { label = "D20 (Chest (bust) depth)", distance = "25" },
                        new PredefinedDistance { label = "D21 (Abdominal depth)", distance = "27" },
                        new PredefinedDistance { label = "D13 (Buttock-knee length)", distance = "59.5" },
                        new PredefinedDistance { label = "D14 (Buttock-popliteal length)", distance = "49.5" }
                    };
                    SetStartAndEndPointsForMaleEuSitdownPAwal(antroControl2, prefab);
                    break;
                case "MaleEuSitdownP1":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D8 (Sitting height)", distance = "91.00" },
                        new PredefinedDistance { label = "D9 (Sitting eye height)", distance = "79.00" },
                        new PredefinedDistance { label = "D10 (Sitting shoulder height)", distance = "59.50" },
                        new PredefinedDistance { label = "D11 (Sitting elbow height)", distance = "24.50" },
                        new PredefinedDistance { label = "D12 (Thigh clearance)", distance = "16.00" },
                        new PredefinedDistance { label = "D16 (Popliteal height)", distance = "44.00" },
                        new PredefinedDistance { label = "D15 (Sitting knee height)", distance = "54.50" }
                    };
                    SetStartAndEndPointsForMaleEuSitdownP1(antroControl2, prefab);
                    break;
                case "MaleEuSitdownP2":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D23 (Elbow-fingertip length)", distance = "36.50" },
                        new PredefinedDistance { label = "D22 (Shoulder-elbow length)", distance = "47.50" },
                        new PredefinedDistance { label = "D25 (Vertical grip reach (sitting))", distance = "124.50" }
                    };
                    SetStartAndEndPointsForMaleEuSitdownP2(antroControl2, prefab);
                    break;
                // Female Asian Standing
                case "FemaleAsianStandingPAwal":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D1 (Stature)", distance = "157.50" },
                        new PredefinedDistance { label = "D2 (Eye Height)", distance = "145.98" },
                        new PredefinedDistance { label = "D3 (Shoulder Height)", distance = "127.88" },
                        new PredefinedDistance { label = "D4 (Elbow Height)", distance = "99.23" },
                        new PredefinedDistance { label = "D5 (Hip Height)", distance = "77.80" },
                        new PredefinedDistance { label = "D6 (Knuckle Height)", distance = "83.67" },
                        new PredefinedDistance { label = "D7 (fingertip Height)", distance = "61.17" }
                    };
                    SetStartAndEndPointsForFemaleAsianStandingPAwal(antroControl2, prefab);
                    break;
                case "FemaleAsianStandingP1":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D24 (Upper limb length)", distance = "68.77" },
                        new PredefinedDistance { label = "D25 (Shoulder-grip length)", distance = "57.18" },
                        new PredefinedDistance { label = "D36 (Head length)", distance = "69.09" }
                    };
                    SetStartAndEndPointsForFemaleAsianStandingP1(antroControl2, prefab);
                    break;
                case "FemaleAsianStandingP2":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D34 (Vertical grip reach (standing))", distance = "194.30" }
                    };
                    SetStartAndEndPointsForFemaleAsianStandingP2(antroControl2, prefab);
                    break;
                case "FemaleAsianStandingTPose":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D32 (Span)", distance = "153.03" },
                        new PredefinedDistance { label = "D33 (Elbow span)", distance = "75.60" }
                    };
                    SetStartAndEndPointsForFemaleAsianStandingTPose(antroControl2, prefab);
                    break;
                // Female Asian Sitdown
                case "FemaleAsianSitdownPAwal":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D27 (Head breadth)", distance = "15.87" },
                        new PredefinedDistance { label = "D18 (Shoulder breadth (biacromial))", distance = "34.55" },
                        new PredefinedDistance { label = "D17 (Shoulder breadth (bideltoid))", distance = "40.38" },
                        new PredefinedDistance { label = "D19 (Hip breadth)", distance = "32.28" },
                        new PredefinedDistance { label = "D26 (Head length)", distance = "20.73" },
                        new PredefinedDistance { label = "D20 (Chest (bust) depth)", distance = "21.91" },
                        new PredefinedDistance { label = "D21 (Abdominal depth)", distance = "21.82" },
                        new PredefinedDistance { label = "D13 (Buttock-knee length)", distance = "52.95" },
                        new PredefinedDistance { label = "D14 (Buttock-popliteal length)", distance = "43.95" }
                    };
                    SetStartAndEndPointsForFemaleAsianSitdownPAwal(antroControl2, prefab);
                    break;
                case "FemaleAsianSitdownP1":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D8 (Sitting height)", distance = "85.48" },
                        new PredefinedDistance { label = "D9 (Sitting eye height)", distance = "74.10" },
                        new PredefinedDistance { label = "D10 (Sitting shoulder height)", distance = "65.56" },
                        new PredefinedDistance { label = "D11 (Sitting elbow height)", distance = "25.53" },
                        new PredefinedDistance { label = "D12 (Thigh clearance)", distance = "61.37" },
                        new PredefinedDistance { label = "D16 (Popliteal height)", distance = "46.67" },
                        new PredefinedDistance { label = "D15 (Sitting knee height)", distance = "37.68" }
                    };
                    SetStartAndEndPointsForFemaleAsianSitdownP1(antroControl2, prefab);
                    break;
                case "FemaleAsianSitdownP2":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D23 (Elbow-fingertip length)", distance = "41.10" },
                        new PredefinedDistance { label = "D22 (Shoulder-elbow length)", distance = "28.53" },
                        new PredefinedDistance { label = "D25 (Vertical grip reach (sitting))", distance = "122.47" }
                    };
                    SetStartAndEndPointsForFemaleAsianSitdownP2(antroControl2, prefab);
                    break;
                // Female Eu Standing
                case "FemaleEuStandingPAwal":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D1 (Stature)", distance = "161.00" },
                        new PredefinedDistance { label = "D2 (Eye Height)", distance = "150.50" },
                        new PredefinedDistance { label = "D3 (Shoulder Height)", distance = "131.00" },
                        new PredefinedDistance { label = "D4 (Elbow Height)", distance = "100.50" },
                        new PredefinedDistance { label = "D5 (Hip Height)", distance = "81.00" },
                        new PredefinedDistance { label = "D6 (Knuckle Height)", distance = "72.00" },
                        new PredefinedDistance { label = "D7 (fingertip Height)", distance = "62.50" }
                    };
                    SetStartAndEndPointsForFemaleEuStandingPAwal(antroControl2, prefab);
                    break;
                case "FemaleEuStandingP1":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D24 (Upper limb length)", distance = "70.50" },
                        new PredefinedDistance { label = "D25 (Shoulder-grip length)", distance = "60.00" },
                        new PredefinedDistance { label = "D36 (Head length)", distance = "70.50" }
                    };
                    SetStartAndEndPointsForFemaleEuStandingP1(antroControl2, prefab);
                    break;
                case "FemaleEuStandingP2":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D34 (Vertical grip reach (standing))", distance = "190.50" }
                    };
                    SetStartAndEndPointsForFemaleEuStandingP2(antroControl2, prefab);
                    break;
                case "FemaleEuStandingTPose":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D32 (Span)", distance = "160.50" },
                        new PredefinedDistance { label = "D33 (Elbow span)", distance = "85.00" }
                    };
                    SetStartAndEndPointsForFemaleEuStandingTPose(antroControl2, prefab);
                    break;
                // Female Eu Sitdown
                case "FemaleEuSitdownPAwal":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D27 (Head breadth)", distance = "14.50" },
                        new PredefinedDistance { label = "D18 (Shoulder breadth (biacromial))", distance = "35.50" },
                        new PredefinedDistance { label = "D17 (Shoulder breadth (bideltoid))", distance = "39.50" },
                        new PredefinedDistance { label = "D19 (Hip breadth)", distance = "37.00" },
                        new PredefinedDistance { label = "D26 (Head length)", distance = "18.00" },
                        new PredefinedDistance { label = "D20 (Chest (bust) depth)", distance = "25.00" },
                        new PredefinedDistance { label = "D21 (Abdominal depth)", distance = "25.50" },
                        new PredefinedDistance { label = "D13 (Buttock-knee length)", distance = "57.00" },
                        new PredefinedDistance { label = "D14 (Buttock-popliteal length)", distance = "48.00" }
                    };
                    SetStartAndEndPointsForFemaleEuSitdownPAwal(antroControl2, prefab);
                    break;
                case "FemaleEuSitdownP1":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D8 (Sitting height)", distance = "85.00" },
                        new PredefinedDistance { label = "D9 (Sitting eye height)", distance = "74.00" },
                        new PredefinedDistance { label = "D10 (Sitting shoulder height)", distance = "55.50" },
                        new PredefinedDistance { label = "D11 (Sitting elbow height)", distance = "23.50" },
                        new PredefinedDistance { label = "D12 (Thigh clearance)", distance = "15.50" },
                        new PredefinedDistance { label = "D16 (Popliteal height)", distance = "40.00" },
                        new PredefinedDistance { label = "D15 (Sitting knee height)", distance = "50.00" }
                    };
                    SetStartAndEndPointsForFemaleEuSitdownP1(antroControl2, prefab);
                    break;
                case "FemaleEuSitdownP2":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D23 (Elbow-fingertip length)", distance = "43.00" },
                        new PredefinedDistance { label = "D22 (Shoulder-elbow length)", distance = "33.00" },
                        new PredefinedDistance { label = "D25 (Vertical grip reach (sitting))", distance = "115.00" }
                    };
                    SetStartAndEndPointsForFemaleEuSitdownP2(antroControl2, prefab);
                    break;
                // Male American Standing
                case "MaleAmericanStandingPAwal":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D1 (Stature)", distance = "175.49" },
                        new PredefinedDistance { label = "D2 (Eye Height)", distance = "164.01" },
                        new PredefinedDistance { label = "D3 (Shoulder Height)", distance = "143.89" },
                        new PredefinedDistance { label = "D4 (Elbow Height)", distance = "108.31" },
                        new PredefinedDistance { label = "D5 (Hip Height)", distance = "89.89" },
                        new PredefinedDistance { label = "D6 (Knuckle Height)", distance = "73.10" },
                        new PredefinedDistance { label = "D7 (fingertip Height)", distance = "65.41" }
                    };
                    SetStartAndEndPointsForMaleAmericanStandingPAwal(antroControl2, prefab);
                    break;
                case "MaleAmericanStandingP1":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D24 (Upper limb length)", distance = "78.59" },
                        new PredefinedDistance { label = "D25 (Shoulder-grip length)", distance = "71.09" },
                        new PredefinedDistance { label = "D36 (Head length)", distance = "19.99" }
                    };
                    SetStartAndEndPointsForMaleAmericanStandingP1(antroControl2, prefab);
                    break;
                case "MaleAmericanStandingP2":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D34 (Vertical grip reach (standing))", distance = "213.89" }
                    };
                    SetStartAndEndPointsForMaleAmericanStandingP2(antroControl2, prefab);
                    break;
                case "MaleAmericanStandingTPose":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D32 (Span)", distance = "181.20" },
                        new PredefinedDistance { label = "D33 (Elbow span)", distance = "85.19" }
                    };
                    SetStartAndEndPointsForMaleAmericanStandingTPose(antroControl2, prefab);
                    break;
                // Male American Sitdown
                case "MaleAmericanSitdownPAwal":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D27 (Head breadth)", distance = "15.39" },
                        new PredefinedDistance { label = "D18 (Shoulder breadth (biacromial))", distance = "41.50" },
                        new PredefinedDistance { label = "D17 (Shoulder breadth (bideltoid))", distance = "50.90" },
                        new PredefinedDistance { label = "D19 (Hip breadth)", distance = "34.39" },
                        new PredefinedDistance { label = "D26 (Head length)", distance = "19.99" },
                        new PredefinedDistance { label = "D20 (Chest (bust) depth)", distance = "25.30" },
                        new PredefinedDistance { label = "D21 (Abdominal depth)", distance = "25.10" },
                        new PredefinedDistance { label = "D13 (Buttock-knee length)", distance = "61.70" },
                        new PredefinedDistance { label = "D14 (Buttock-popliteal length)", distance = "50.19" }
                    };
                    SetStartAndEndPointsForMaleAmericanSitdownPAwal(antroControl2, prefab);
                    break;
                case "MaleAmericanSitdownP1":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D8 (Sitting height)", distance = "91.80" },
                        new PredefinedDistance { label = "D9 (Sitting eye height)", distance = "80.39" },
                        new PredefinedDistance { label = "D10 (Sitting shoulder height)", distance = "60.30" },
                        new PredefinedDistance { label = "D11 (Sitting elbow height)", distance = "24.61" },
                        new PredefinedDistance { label = "D12 (Thigh clearance)", distance = "18.01" },
                        new PredefinedDistance { label = "D16 (Popliteal height)", distance = "43.00" },
                        new PredefinedDistance { label = "D15 (Sitting knee height)", distance = "55.30" }
                    };
                    SetStartAndEndPointsForMaleAmericanSitdownP1(antroControl2, prefab);
                    break;
                case "MaleAmericanSitdownP2":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D23 (Elbow-fingertip length)", distance = "48.01" },
                        new PredefinedDistance { label = "D22 (Shoulder-elbow length)", distance = "36.30" },
                        new PredefinedDistance { label = "D35 (Vertical grip reach (sitting))", distance = "130.20" }
                    };
                    SetStartAndEndPointsForMaleAmericanSitdownP2(antroControl2, prefab);
                    break;
                // Female American Standing
                case "FemaleAmericanStandingPAwal":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D1 (Stature)", distance = "162.61" },
                        new PredefinedDistance { label = "D2 (Eye Height)", distance = "151.69" },
                        new PredefinedDistance { label = "D3 (Shoulder Height)", distance = "133.20" },
                        new PredefinedDistance { label = "D4 (Elbow Height)", distance = "100.20" },
                        new PredefinedDistance { label = "D5 (Hip Height)", distance = "84.40" },
                        new PredefinedDistance { label = "D6 (Knuckle Height)", distance = "68.61" },
                        new PredefinedDistance { label = "D7 (fingertip Height)", distance = "61.29" }
                    };
                    SetStartAndEndPointsForFemaleAmericanStandingPAwal(antroControl2, prefab);
                    break;
                case "FemaleAmericanStandingP1":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D24 (Upper limb length)", distance = "72.01" },
                        new PredefinedDistance { label = "D25 (Shoulder-grip length)", distance = "65.00" },
                        new PredefinedDistance { label = "D36 (Head length)", distance = "19.00" }
                    };
                    SetStartAndEndPointsForFemaleAmericanStandingP1(antroControl2, prefab);
                    break;
                case "FemaleAmericanStandingP2":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D34 (Vertical grip reach (standing))", distance = "196.39" }
                    };
                    SetStartAndEndPointsForFemaleAmericanStandingP2(antroControl2, prefab);
                    break;
                case "FemaleAmericanStandingTPose":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D32 (Span)", distance = "165.71" },
                        new PredefinedDistance { label = "D33 (Elbow span)", distance = "78.13" }
                    };
                    SetStartAndEndPointsForFemaleAmericanStandingTPose(antroControl2, prefab);
                    break;
                // Female Eu Sitdown
                case "FemaleAmericanSitdownPAwal":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D27 (Head breadth)", distance = "14.71" },
                        new PredefinedDistance { label = "D18 (Shoulder breadth (biacromial))", distance = "36.50" },
                        new PredefinedDistance { label = "D17 (Shoulder breadth (bideltoid))", distance = "45.01" },
                        new PredefinedDistance { label = "D19 (Hip breadth)", distance = "35.31" },
                        new PredefinedDistance { label = "D26 (Head length)", distance = "19.00" },
                        new PredefinedDistance { label = "D20 (Chest (bust) depth)", distance = "24.51" },
                        new PredefinedDistance { label = "D21 (Abdominal depth)", distance = "22.71" },
                        new PredefinedDistance { label = "D13 (Buttock-knee length)", distance = "58.90" },
                        new PredefinedDistance { label = "D14 (Buttock-popliteal length)", distance = "48.31" }
                    };
                    SetStartAndEndPointsForFemaleAmericanSitdownPAwal(antroControl2, prefab);
                    break;
                case "FemaleAmericanSitdownP1":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D8 (Sitting height)", distance = "85.70" },
                        new PredefinedDistance { label = "D9 (Sitting eye height)", distance = "74.80" },
                        new PredefinedDistance { label = "D10 (Sitting shoulder height)", distance = "56.39" },
                        new PredefinedDistance { label = "D11 (Sitting elbow height)", distance = "23.29" },
                        new PredefinedDistance { label = "D12 (Thigh clearance)", distance = "16.69" },
                        new PredefinedDistance { label = "D16 (Popliteal height)", distance = "38.71" },
                        new PredefinedDistance { label = "D15 (Sitting knee height)", distance = "51.00" }
                    };
                    SetStartAndEndPointsForFemaleAmericanSitdownP1(antroControl2, prefab);
                    break;
                case "FemaleAmericanSitdownP2":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D23 (Elbow-fingertip length)", distance = "43.79" },
                        new PredefinedDistance { label = "D22 (Shoulder-elbow length)", distance = "33.40" },
                        new PredefinedDistance { label = "D25 (Vertical grip reach (sitting))", distance = "65.00" }
                    };
                    SetStartAndEndPointsForFemaleAmericanSitdownP2(antroControl2, prefab);
                    break;
                // Male Asian Hand
                case "MaleAsianHand":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D28 (Hand length)", distance = "18.65" },
                        new PredefinedDistance { label = "D29 (Hand breadth)", distance = "7.80" }
                    };
                    SetStartAndEndPointsForMaleAsianHand(antroControl2, prefab);
                    break;
                // Male Eu Hand
                case "MaleEuHand":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D28 (Hand length)", distance = "19.00" },
                        new PredefinedDistance { label = "D29 (Hand breadth)", distance = "8.50" }
                    };
                    SetStartAndEndPointsForMaleEuHand(antroControl2, prefab);
                    break;
                // Male American Hand
                case "MaleAmericanHand":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D28 (Hand length)", distance = "19.30" },
                        new PredefinedDistance { label = "D29 (Hand breadth)", distance = "8.79" }
                    };
                    SetStartAndEndPointsForMaleAmericanHand(antroControl2, prefab);
                    break;
                // Female Asian Hand
                case "FemaleAsianHand":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D28 (Hand length)", distance = "17.20" },
                        new PredefinedDistance { label = "D29 (Hand breadth)", distance = "7.79" }
                    };
                    SetStartAndEndPointsForFemaleAsianHand(antroControl2, prefab);
                    break;
                // Female American Hand
                case "FemaleAmericanHand":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D28 (Hand length)", distance = "18.01" },
                        new PredefinedDistance { label = "D29 (Hand breadth)", distance = "7.80" }
                    };
                    SetStartAndEndPointsForFemaleAmericanHand(antroControl2, prefab);
                    break;
                // Female Eu Hand
                case "FemaleEuHand":
                    antroControl2.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D28 (Hand length)", distance = "17.50" },
                        new PredefinedDistance { label = "D29 (Hand breadth)", distance = "7.50" }
                    };
                    SetStartAndEndPointsForFemaleEuHand(antroControl2, prefab);
                    break;
                default:
                    Debug.LogWarning("Prefab name not recognized: " + prefab.name);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Prefab is null. Cannot add AntroControl2 component.");
        }
    }

    private void AddAntroControl3Component(GameObject prefab)
    {
        if (prefab != null)
        {
            // Add AntroControl3 component
            var antroControl3 = prefab.AddComponent<AntroControl3>();
            if (antroControl3 == null)
            {
                Debug.LogWarning("Failed to add AntroControl3 component to prefab: " + prefab.name);
                return;
            }

            // Set the Snap distance threshold to 0.1
            antroControl3.snapDistanceThreshold = 0.1f;

            // Set the object to destroy as the prefab itself
            antroControl3.objectToDestroy = prefab;

            // Set the object controller field to the prefab itself
            antroControl3.objectController = prefab.GetComponent<ObjectControl>();

            // Find the ConfirmationCanvas GameObject
            var confirmationCanvas = GameObject.Find("ConfirmationCanvas");
            if (confirmationCanvas != null)
            {
                // Find the Distance Text component in the ConfirmationCanvas child
                var distanceText = confirmationCanvas.transform.Find("Distance")?.GetComponent<TMPro.TextMeshProUGUI>();
                if (distanceText != null)
                {
                    // Set the Distance Text in AntroControl2
                    antroControl3.distanceText = distanceText;
                }
                else
                {
                    Debug.LogWarning("Distance Text not found in ConfirmationCanvas child.");
                }
            }
            else
            {
                Debug.LogWarning("ConfirmationCanvas GameObject not found.");
            }

            // Load the Line material from the asset bundle
            var lineMaterial = assetBundle.LoadAsset<Material>("Line");
            if (lineMaterial != null)
            {
                // Set the Line material of the AntroControl2 component
                antroControl3.lineMaterial = lineMaterial;
            }
            else
            {
                Debug.LogWarning("Line material not found in asset bundle.");
            }

            // Set specific properties based on the prefab name
            switch (prefab.name)
            {
                // Dummy
                case "DummyAntro":
                    antroControl3.predefinedDistances = new PredefinedDistances[]
                    {
                        new PredefinedDistances { label = "D1 (Stature)", distance = "169.35" },
                    };
                    SetStartAndEndPointsForDummyAntro(antroControl3, prefab);
                    break;
                default:
                    Debug.LogWarning("Prefab name not recognized: " + prefab.name);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Prefab is null. Cannot add AntroControl2 component.");
        }
    }

    private void AddAntroControlComponent(GameObject prefab)
    {
        if (prefab != null)
        {
            // Add AntroControl component
            var antroControl = prefab.AddComponent<AntroControl>();
            if (antroControl == null)
            {
                Debug.LogWarning("Failed to add AntroControl component to prefab: " + prefab.name);
                return;
            }

            // Set the Snap distance threshold 
            antroControl.snapDistanceThreshold = 0.2f;

            // Set the object controller field to the prefab itself
            antroControl.objectControl = prefab.GetComponent<ObjectControl>();

            // Set the audio clip if it's assigned
            if (snapSound != null)
            {
                antroControl.snapSound = snapSound;
            }

            // Find the ConfirmationCanvas GameObject
            var confirmationCanvas = GameObject.Find("ConfirmationCanvas");
            if (confirmationCanvas != null)
            {
                // Find the Distance Text component in the ConfirmationCanvas child
                var distanceText = confirmationCanvas.transform.Find("Distance")?.GetComponent<TMPro.TextMeshProUGUI>();
                if (distanceText != null)
                {
                    // Set the Distance Text in AntroControl
                    antroControl.distanceText = distanceText;
                }

                var exitButton = confirmationCanvas.transform.Find("Exit")?.GetComponent<UnityEngine.UI.Button>();
                var chooseButton = confirmationCanvas.transform.Find("Choose")?.GetComponent<UnityEngine.UI.Button>();
                var nextPointButton = GameObject.Find("AntroControl")?.transform.Find("NextPoint")?.GetComponent<UnityEngine.UI.Button>();

                antroControl.buttonsToDisable = new UnityEngine.UI.Button[] { exitButton, chooseButton, nextPointButton };

                // Find the Excel child GameObject
                var excel = confirmationCanvas.transform.Find("Excel");
                if (excel != null)
                {
                    // Set the panel field in AntroControl
                    antroControl.panel = excel.gameObject;

                    // Find the Button component in the Excel child GameObject
                    var openExcelButton = excel.Find("Button")?.GetComponent<UnityEngine.UI.Button>();
                    if (openExcelButton != null)
                    {
                        // Set the openExcelButton field in AntroControl
                        antroControl.openExcelButton = openExcelButton;

                        // Ensure the click listener is added only once
                        openExcelButton.onClick.RemoveAllListeners();
                        openExcelButton.onClick.AddListener(() => OpenExcelLink(confirmationCanvas, prefab));
                    }
                    else
                    {
                        Debug.LogWarning("Button component not found in Excel GameObject.");
                    }
                }
                else
                {
                    Debug.LogWarning("Excel child GameObject not found in ConfirmationCanvas.");
                }
            }
            else
            {
                Debug.LogWarning("ConfirmationCanvas GameObject not found.");
            }

            // Load the Line material from the asset bundle
            var lineMaterial = assetBundle.LoadAsset<Material>("Line");
            if (lineMaterial != null)
            {
                // Set the Line material of the AntroControl component
                antroControl.lineMaterial = lineMaterial;
            }
            else
            {
                Debug.LogWarning("Line material not found in asset bundle.");
            }

            //// Load the CharacterPrefab from the asset bundle
            //var characterPrefab = assetBundle.LoadAsset<GameObject>("Lecturer 2");
            //if (characterPrefab != null)
            //{
            //    // Set the CharacterPrefab field in AntroControl
            //    antroControl.characterPrefab = characterPrefab;

            //    // Tag the characterPrefab with "Bot2"
            //    characterPrefab.tag = "Bot2";

            //    // Find the TriggerCube within the Lecturer 2 prefab
            //    var triggerCube = characterPrefab.transform.Find("TriggerCube");
            //    if (triggerCube != null)
            //    {
            //        // Check if ConversationStarter component already exists
            //        var existingConversationStarter = triggerCube.GetComponent<ConversationStarter>();
            //        if (existingConversationStarter == null)
            //        {
            //            // Add ConversationStarter component to the TriggerCube
            //            var conversationStarter = triggerCube.gameObject.AddComponent<ConversationStarter>();
            //            if (conversationStarter == null)
            //            {
            //                Debug.LogWarning("Failed to add ConversationStarter component to TriggerCube.");
            //            }
            //            else
            //            {
            //                // Set ObjectToToggle field to Lecturer 2
            //                conversationStarter.objectToToggle = characterPrefab;

            //                // Set MyConversation field to myConversation2
            //                conversationStarter.myConversation = myConversation2;
            //            }
            //        }
            //        else
            //        {
            //            Debug.LogWarning("ConversationStarter component already exists on TriggerCube.");
            //        }
            //    }
            //    else
            //    {
            //        Debug.LogWarning("TriggerCube not found in Lecturer 2 prefab.");
            //    }
            //}
            //else
            //{
            //    Debug.LogWarning("Character prefab 'Lecturer2' not found in asset bundle.");
            //}

            // Load the CharacterSpawnPoint from the asset bundle
            //var characterSpawnPoint = assetBundle.LoadAsset<GameObject>("Lecturer 2")?.transform;
            //if (characterSpawnPoint != null)
            //{
            //    // Set the CharacterSpawnPoint field in AntroControl
            //    antroControl.characterSpawnPoint = characterSpawnPoint;
            //}
            //else
            //{
            //    Debug.LogWarning("Character spawn point 'Lecturer2' not found in asset bundle.");
            //}

            // Set the ExcelLink field based on the prefab name
            switch (prefab.name)
            {
                case "MaleAsianFeet":
                    antroControl.excelLink = "/*https://docs.google.com/spreadsheets/d/1gAOOwIiLfgMVNK7pfkMrUHIEMnGyEIbJ/edit?pli=1#gid=73904180*/";
                    antroControl.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D30 (Foot length)", distance = "24.36" },
                        new PredefinedDistance { label = "D31 (Foot breadth)", distance = "9.30" }
                    };
                    SetStartAndEndPointsForMaleAsianFeet(antroControl, prefab);
                    break;
                case "MaleAmericanFeet":
                    antroControl.excelLink = "/*https://docs.google.com/spreadsheets/d/1gAOOwIiLfgMVNK7pfkMrUHIEMnGyEIbJ/edit?pli=1#gid=73904180*/";
                    antroControl.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D30 (Foot length)", distance = "27.10" },
                        new PredefinedDistance { label = "D31 (Foot breadth)", distance = "10.21" }
                    };
                    SetStartAndEndPointsForMaleAmericanFeet(antroControl, prefab);
                    break;
                case "MaleEuFeet":
                    antroControl.excelLink = "/*https://docs.google.com/spreadsheets/d/1gAOOwIiLfgMVNK7pfkMrUHIEMnGyEIbJ/edit?pli=1#gid=2026117396*/";
                    antroControl.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D30 (Foot length)", distance = "26.50" },
                        new PredefinedDistance { label = "D31 (Foot breadth)", distance = "9.50" }
                    };
                    SetStartAndEndPointsForMaleEuFeet(antroControl, prefab);
                    break;
                case "FemaleAsianFeet":
                    antroControl.excelLink = "/*https://docs.google.com/spreadsheets/d/1gAOOwIiLfgMVNK7pfkMrUHIEMnGyEIbJ/edit?pli=1#gid=949322798*/";
                    antroControl.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D30 (Foot length)", distance = "22.94" },
                        new PredefinedDistance { label = "D31 (Foot breadth)", distance = "8.40" }
                    };
                    SetStartAndEndPointsForFemaleAsianFeet(antroControl, prefab);
                    break;
                case "FemaleAmericanFeet":
                    antroControl.excelLink = "/*https://docs.google.com/spreadsheets/d/1gAOOwIiLfgMVNK7pfkMrUHIEMnGyEIbJ/edit?pli=1#gid=949322798*/";
                    antroControl.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D30 (Foot length)", distance = "24.36" },
                        new PredefinedDistance { label = "D31 (Foot breadth)", distance = "9.30" }
                    };
                    SetStartAndEndPointsForFemaleAmericanFeet(antroControl, prefab);
                    break;
                case "FemaleEuFeet":
                    antroControl.excelLink = "/*https://docs.google.com/spreadsheets/d/1gAOOwIiLfgMVNK7pfkMrUHIEMnGyEIbJ/edit?pli=1#gid=838150155*/";
                    antroControl.predefinedDistances = new PredefinedDistance[]
                    {
                        new PredefinedDistance { label = "D30 (Foot length)", distance = "23.50" },
                        new PredefinedDistance { label = "D31 (Foot breadth)", distance = "9.00" }
                    };
                    SetStartAndEndPointsForFemaleEuFeet(antroControl, prefab);
                    break;
                default:
                    Debug.LogWarning("Prefab name not recognized: " + prefab.name);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Prefab is null. Cannot add AntroControl component.");
        }
    }

    private void OpenExcelLink(GameObject confirmationCanvas, GameObject prefab)
    {
        // Destroy previous object if exists
        var previousObject = GameObject.FindWithTag("Bot");
        if (previousObject != null)
        {
            Destroy(previousObject);
        }

        string url = "";

        // Check for active objects in the scene
        if (GameObject.FindObjectsOfType<GameObject>().Any(go => go.name == "MaleAsianFeet(Clone)" && go.activeInHierarchy))
        {
            url = "https://cdn-bion-3dvirtualclassroom-assets.azureedge.net/bol/AR_Asset/WebGL/VirtualLabFile/Anthropometri/ManAsian.xlsx";
        }
        else if (GameObject.FindObjectsOfType<GameObject>().Any(go => go.name == "MaleEuFeet(Clone)" && go.activeInHierarchy))
        {
            url = "https://cdn-bion-3dvirtualclassroom-assets.azureedge.net/bol/AR_Asset/WebGL/VirtualLabFile/Anthropometri/ManEurope.xlsx";
        }
        else if (GameObject.FindObjectsOfType<GameObject>().Any(go => go.name == "MaleAmericanFeet(Clone)" && go.activeInHierarchy))
        {
            url = "https://cdn-bion-3dvirtualclassroom-assets.azureedge.net/bol/AR_Asset/WebGL/VirtualLabFile/Anthropometri/ManAmerican.xlsx";
        }
        else if (GameObject.FindObjectsOfType<GameObject>().Any(go => go.name == "FemaleAsianFeet(Clone)" && go.activeInHierarchy))
        {
            url = "https://cdn-bion-3dvirtualclassroom-assets.azureedge.net/bol/AR_Asset/WebGL/VirtualLabFile/Anthropometri/WomanAsian.xlsx";
        }
        else if (GameObject.FindObjectsOfType<GameObject>().Any(go => go.name == "FemaleEuFeet(Clone)" && go.activeInHierarchy))
        {
            url = "https://cdn-bion-3dvirtualclassroom-assets.azureedge.net/bol/AR_Asset/WebGL/VirtualLabFile/Anthropometri/WomanEurope.xlsx";
        }
        else if (GameObject.FindObjectsOfType<GameObject>().Any(go => go.name == "FemaleAmericanFeet(Clone)" && go.activeInHierarchy))
        {
            url = "https://cdn-bion-3dvirtualclassroom-assets.azureedge.net/bol/AR_Asset/WebGL/VirtualLabFile/Anthropometri/WomanAmerican.xlsx";
        }

        if (!string.IsNullOrEmpty(url))
        {
            // Open the URL in the default web browser
            Application.OpenURL(url);

            // Optionally, you can add a delay before loading the next scene to ensure the URL opens
            StartCoroutine(LoadNextSceneWithDelay());
        }

        // Disable only the Excel panel
        var excelPanel = confirmationCanvas.transform.Find("Excel");
        if (excelPanel != null)
        {
            excelPanel.gameObject.SetActive(false);
        }

        // Instantiate Lecturer 2 if it doesn't exist
        //if (!GameObject.FindWithTag("Bot2"))
        //{
        //    var lecturer2Prefab = assetBundle.LoadAsset<GameObject>("Lecturer 2");
        //    if (lecturer2Prefab != null)
        //    {
        //        var lecturer2Instance = Instantiate(lecturer2Prefab);
        //        lecturer2Instance.tag = "Bot2";
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Lecturer 2 prefab not found in asset bundle.");
        //    }
        //}
    }

    private IEnumerator LoadNextSceneWithDelay()
    {
        // Wait for 1 second to ensure the URL opens
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SoalAntropometri");
    }

    private void SetStartAndEndPointsForDummyAntro(AntroControl3 antroControl3, GameObject prefab)
    {
        // Customize start and end points for DummyAntro prefab
        antroControl3.startPoints = new GameObject[1];
        antroControl3.endPoints = new GameObject[1];

        // Load DummyAntro prefab from asset bundle
        var dummyAntroPrefab = assetBundle.LoadAsset<GameObject>("DummyAntro");
        var objectController = dummyAntroPrefab.GetComponent<ObjectControl>();
        if (objectController == null)
        {
            // Add Object Control script to the prefab if it's not already attached
            objectController = dummyAntroPrefab.AddComponent<ObjectControl>();
            Debug.Log("Object Controller component added to DummyAntro prefab.");

            // Set Object Control settings
            objectController.moveSpeed = 5f;
            objectController.rotateSpeed = 3f;
            objectController.zoomSpeed = 100f;
            objectController.minX = 0.5f;
            objectController.maxX = 4f;
            objectController.minY = 1.5f;
            objectController.maxY = 2.5f;
        }

        // Assign the Object Controller component to antroControl3
        antroControl3.objectController = objectController;

        // Set start and end points according to your requirements
        for (int i = 1; i <= 1; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl3.startPoints[i - 1] = startPoint.gameObject;
            }
            else
            {
                Debug.LogWarning($"Start point {i} not found in {prefab.name} prefab.");
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl3.endPoints[i - 1] = endPoint.gameObject;
            }
            else
            {
                Debug.LogWarning($"End point {i} not found in {prefab.name} prefab.");
            }
        }

        // Set panelLine with GameObjects that are children of Tutor
        var tutorObject = GameObject.Find("Tutor");
        if (tutorObject != null)
        {
            antroControl3.panelLine = new GameObject[2];
            antroControl3.panelLine[0] = tutorObject.transform.Find("Tutor1")?.gameObject;
            antroControl3.panelLine[1] = tutorObject.transform.Find("Tutor2")?.gameObject;

            // Verify assignments
            Debug.Log($"PanelLine assigned with: {antroControl3.panelLine[0]?.name} and {antroControl3.panelLine[1]?.name}");
        }

        // Set panelScore with GameObjects that are children of Tutor
        if (tutorObject != null)
        {
            antroControl3.panelScore = new GameObject[2];
            antroControl3.panelScore[0] = tutorObject.transform.Find("Tutor3")?.gameObject;
            antroControl3.panelScore[1] = tutorObject.transform.Find("Tutor4")?.gameObject;

            // Verify assignments
            Debug.Log($"PanelScore assigned with: {antroControl3.panelScore[0]?.name} and {antroControl3.panelScore[1]?.name}");
        }

        // Set nextButton with GameObject named 'Next' under 'Tutor'
        if (tutorObject != null)
        {

            // Set panelLineNextButton with GameObject named 'Next1' under 'Tutor'
            var panelLineNextButtonObject = tutorObject.transform.Find("next1")?.gameObject;
            if (panelLineNextButtonObject != null)
            {
                antroControl3.panelLineNextButton = panelLineNextButtonObject.GetComponent<UnityEngine.UI.Button>();

                // Verify assignment
                Debug.Log($"PanelLineNextButton assigned with: {antroControl3.panelLineNextButton?.name}");
            }

            // Set panelScoreNextButton with GameObject named 'Next2' under 'Tutor'
            var panelScoreNextButtonObject = tutorObject.transform.Find("next2")?.gameObject;
            if (panelScoreNextButtonObject != null)
            {
                antroControl3.panelScoreNextButton = panelScoreNextButtonObject.GetComponent<UnityEngine.UI.Button>();

                // Verify assignment
                Debug.Log($"PanelScoreNextButton assigned with: {antroControl3.panelScoreNextButton?.name}");
            }
        }


        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            var chooseButtonObject = confirmationCanvasObject.transform.Find("Choose")?.gameObject;
            if (chooseButtonObject != null)
            {
                antroControl3.chooseButton = chooseButtonObject.GetComponent<UnityEngine.UI.Button>();

                // Verify assignment
                Debug.Log($"ChooseButton assigned with: {antroControl3.chooseButton?.name}");
            }
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl3.distancePanel = distancePanelObject;
            }
        }
    }

    private void SetStartAndEndPointsForMaleAsianStandingPAwal(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAsianStandingPAwal prefab
        // Example:
        antroControl2.startPoints = new GameObject[7];
        antroControl2.endPoints = new GameObject[7];
        // Rotation
        antroControl2.rotationAngles = new float[7]; 
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 180f;
        antroControl2.rotationAngles[4] = 180f;
        antroControl2.rotationAngles[5] = 180f;
        antroControl2.rotationAngles[6] = 180f;

        // Load MaleAsianStandingP1 prefab from asset bundle
        var maleAsianStandingP1Prefab = assetBundle.LoadAsset<GameObject>("MaleAsianStandingP1");
        if (maleAsianStandingP1Prefab != null)
        {
            antroControl2.nextPrefab = maleAsianStandingP1Prefab;
        }

        // Load MaleAsianStandingPAwal prefab from asset bundle
        var maleAsianStandingPAwalPrefab = assetBundle.LoadAsset<GameObject>("MaleAsianStandingPAwal");
        if (maleAsianStandingPAwalPrefab != null)
        {
            maleAsianStandingPAwalPrefab.tag = "Bot";
            if (maleAsianStandingPAwalPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAsianStandingPAwal prefab
                var objectController = maleAsianStandingPAwalPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAsianStandingPAwalPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAsianStandingPAwal prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 7 and find the corresponding start and end points
        for (int i = 1; i <= 7; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }  
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {
               
                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }
        
        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianMale/1")?.gameObject; 
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianMale/2")?.gameObject; 
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AsianMale/3")?.gameObject; 
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AsianMale/4")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AsianMale/5")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AsianMale/6")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AsianMale/7")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAsianStandingP1(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAsianStandingP1 prefab
        // Example:
        antroControl2.startPoints = new GameObject[3];
        antroControl2.endPoints = new GameObject[3];
        // Rotation
        antroControl2.rotationAngles = new float[3];
        antroControl2.rotationAngles[0] = 90f;
        antroControl2.rotationAngles[1] = 90f;
        antroControl2.rotationAngles[2] = 90f;

        // Load MaleAsianStandingP2 prefab from asset bundle
        var maleAsianStandingP2Prefab = assetBundle.LoadAsset<GameObject>("MaleAsianStandingP2");
        if (maleAsianStandingP2Prefab != null)
        {
            antroControl2.nextPrefab = maleAsianStandingP2Prefab;
        }

        // Load MaleAsianStandingP1 prefab from asset bundle
        var maleAsianStandingP1Prefab = assetBundle.LoadAsset<GameObject>("MaleAsianStandingP1");
        if (maleAsianStandingP1Prefab != null)
        {
            maleAsianStandingP1Prefab.tag = "Bot";
            if (maleAsianStandingP1Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAsianStandingP1 prefab
                var objectController = maleAsianStandingP1Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAsianStandingP1Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAsianStandingP1 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 3 and find the corresponding start and end points
        for (int i = 1; i <= 3; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianMale/24")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianMale/25")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AsianMale/36")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAsianStandingP2(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAsianStandingP2 prefab
        // Example:
        antroControl2.startPoints = new GameObject[1];
        antroControl2.endPoints = new GameObject[1];
        // Rotation
        antroControl2.rotationAngles = new float[1];
        antroControl2.rotationAngles[0] = 180f;

        // Load MaleAsianStandingTPose prefab from asset bundle
        var maleAsianStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("MaleAsianStandingTPose");
        if (maleAsianStandingTPosePrefab != null)
        {
            antroControl2.nextPrefab = maleAsianStandingTPosePrefab;
        }

        // Load MaleAsianStandingP2 prefab from asset bundle
        var maleAsianStandingP2Prefab = assetBundle.LoadAsset<GameObject>("MaleAsianStandingP2");
        if (maleAsianStandingP2Prefab != null)
        {
            maleAsianStandingP2Prefab.tag = "Bot";
            if (maleAsianStandingP2Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAsianStandingP2 prefab
                var objectController = maleAsianStandingP2Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAsianStandingP2Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAsianStandingP2 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 1; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianMale/34")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAsianStandingTPose(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAsianStandingTPose prefab
        // Example:
        antroControl2.startPoints = new GameObject[2];
        antroControl2.endPoints = new GameObject[2];
        // Rotation
        antroControl2.rotationAngles = new float[2];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;

        // Load MaleAsianSitdownPAwal prefab from asset bundle
        var maleAsianSitdownPAwal = assetBundle.LoadAsset<GameObject>("MaleAsianSitdownPAwal");
        if (maleAsianSitdownPAwal != null)
        {
            antroControl2.nextPrefab = maleAsianSitdownPAwal;
        }

        // Load MaleAsianStandingTpose prefab from asset bundle
        var maleAsianStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("MaleAsianStandingTPose");
        if (maleAsianStandingTPosePrefab != null)
        {
            maleAsianStandingTPosePrefab.tag = "Bot";
            if (maleAsianStandingTPosePrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAsianStandingTpose prefab
                var objectController = maleAsianStandingTPosePrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAsianStandingTPosePrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAsianStandingTpose prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }
            else
            {
                Debug.LogWarning($"Start point {i} not found in {prefab.name} prefab.");
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
            else
            {
                Debug.LogWarning($"End point {i} not found in {prefab.name} prefab.");
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianMale/32")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianMale/33")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAsianSitdownPAwal(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAsianSitdownPAwal prefab
        // Example:
        antroControl2.startPoints = new GameObject[9];
        antroControl2.endPoints = new GameObject[9];
        // Rotation
        antroControl2.rotationAngles = new float[9];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 0f;
        antroControl2.rotationAngles[4] = 90f;
        antroControl2.rotationAngles[5] = 90f;
        antroControl2.rotationAngles[6] = -90f;
        antroControl2.rotationAngles[7] = 90f;
        antroControl2.rotationAngles[8] = -90f;

        // Load MaleAsianStandingP1 prefab from asset bundle
        var maleAsianSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("MaleAsianSitdownP1");
        if (maleAsianSitdownP1Prefab != null)
        {
            antroControl2.nextPrefab = maleAsianSitdownP1Prefab;
        }

        // Load MaleAsianSitdownPAwal prefab from asset bundle
        var maleAsianSitdownPAwalPrefab = assetBundle.LoadAsset<GameObject>("MaleAsianSitdownPAwal");
        if (maleAsianSitdownPAwalPrefab != null)
        {
            maleAsianSitdownPAwalPrefab.tag = "Bot"; 
            if (maleAsianSitdownPAwalPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAsianStandingPAwal prefab
                var objectController = maleAsianSitdownPAwalPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAsianSitdownPAwalPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAsianSitdownPAwal prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        for (int i = 1; i <= 9; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject confirmationCanvas = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvas != null)
        {
            // Find the TeksAntro panel within the ConfirmationCanvas
            Transform teksAntroPanelTransform = confirmationCanvas.transform.Find("TeksAntro");
            if (teksAntroPanelTransform != null)
            {
                antroControl2.teksAntroPanel = teksAntroPanelTransform.gameObject;
            }
            
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvas.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }             
            }           
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianMale/27")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianMale/18")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AsianMale/17")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AsianMale/19")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AsianMale/26")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AsianMale/20")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AsianMale/21")?.gameObject;
            antroControl2.predefinedDistances[7].panel = antroData.transform.Find("AsianMale/13")?.gameObject;
            antroControl2.predefinedDistances[8].panel = antroData.transform.Find("AsianMale/14")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAsianSitdownP1(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAsianSitdownP1 prefab
        // Example:
        antroControl2.startPoints = new GameObject[7];
        antroControl2.endPoints = new GameObject[7];
        // Rotation
        antroControl2.rotationAngles = new float[7];
        antroControl2.rotationAngles[0] = 90f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 180f;
        antroControl2.rotationAngles[4] = 180f;
        antroControl2.rotationAngles[5] = 180f;
        antroControl2.rotationAngles[6] = 180f;

        // Load maleAsianSitdownP2 prefab from asset bundle
        var maleAsianSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("MaleAsianSitdownP2");
        if (maleAsianSitdownP2Prefab != null)
        {
            antroControl2.nextPrefab = maleAsianSitdownP2Prefab;
        }

        // Load MaleAsianSitdownP1 prefab from asset bundle
        var maleAsianSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("MaleAsianSitdownP1");
        if (maleAsianSitdownP1Prefab != null)
        {
            maleAsianSitdownP1Prefab.tag = "Bot";
            if (maleAsianSitdownP1Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAsianSitdownP1 prefab
                var objectController = maleAsianSitdownP1Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAsianSitdownP1Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to maleAsianSitdownP1 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 7; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianMale/8")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianMale/9")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AsianMale/10")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AsianMale/11")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AsianMale/12")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AsianMale/16")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AsianMale/15")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAsianSitdownP2(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAsianSitdownP2 prefab
        // Example:
        antroControl2.startPoints = new GameObject[3];
        antroControl2.endPoints = new GameObject[3];
        // Rotation
        antroControl2.rotationAngles = new float[3];
        antroControl2.rotationAngles[0] = -90f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;

        // Load maleAsianHand prefab from asset bundle
        var maleAsianHandPrefab = assetBundle.LoadAsset<GameObject>("MaleAsianHand");
        if (maleAsianHandPrefab != null)
        {
            antroControl2.nextPrefab = maleAsianHandPrefab;
        }

        // Load MaleAsianSitdownP2 prefab from asset bundle
        var maleAsianSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("MaleAsianSitdownP2");
        if (maleAsianSitdownP2Prefab != null)
        {
            maleAsianSitdownP2Prefab.tag = "Bot";
            if (maleAsianSitdownP2Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAsianSitdownP2 prefab
                var objectController = maleAsianSitdownP2Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAsianSitdownP2Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to maleAsianSitdownP2 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 3; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianMale/22")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianMale/23")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AsianMale/35")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAsianHand(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAsianHand prefab
        // Example:
        antroControl2.startPoints = new GameObject[2];
        antroControl2.endPoints = new GameObject[2];

        // Load MaleAsianFeet prefab from asset bundle
        var maleAsianFeetPrefab = assetBundle.LoadAsset<GameObject>("MaleAsianFeet");
        if (maleAsianFeetPrefab != null)
        {
            antroControl2.nextPrefab = maleAsianFeetPrefab;
        }

        // Load MaleAsianHand prefab from asset bundle
        var maleAsianHandPrefab = assetBundle.LoadAsset<GameObject>("MaleAsianHand");
        if (maleAsianHandPrefab != null)
        {
            maleAsianHandPrefab.tag = "Bot";
            if (maleAsianHandPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAsianHand prefab
                var objectController = maleAsianHandPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAsianHandPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAsianHand prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 2f;
                    objectController.maxX = 6f;
                    objectController.minY = 0.5f;
                    objectController.maxY = 1.5f;
                    objectController.nonRotatableObject = maleAsianHandPrefab;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianMale/28")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianMale/29")?.gameObject;
        }
    }
    private void SetStartAndEndPointsForMaleAsianFeet(AntroControl antroControl, GameObject prefab)
    {
        // Customize start and end points for MaleAsianFeet prefab
        // Example:
        antroControl.startPoints = new GameObject[2];
        antroControl.endPoints = new GameObject[2];

        // Load MaleAsianFeet prefab from asset bundle
        var maleAsianFeetPrefab = assetBundle.LoadAsset<GameObject>("MaleAsianFeet");
        if (maleAsianFeetPrefab != null)
        {
            maleAsianFeetPrefab.tag = "Bot";
            if (maleAsianFeetPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAsianFeet prefab
                var objectController = maleAsianFeetPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAsianFeetPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAsianFeet prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 1f;
                    objectController.maxX = 4f;
                    objectController.minY = 1f;
                    objectController.maxY = 1.5f;
                    objectController.nonRotatableObject = maleAsianFeetPrefab;
                }
                // Assign the Object Controller component to antroControl
                antroControl.objectControl = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 2 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }
      
        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl.distancePanel = distancePanelObject;
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl.predefinedDistances[0].panel = antroData.transform.Find("AsianMale/30")?.gameObject;
            antroControl.predefinedDistances[1].panel = antroData.transform.Find("AsianMale/31")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAsianStandingPAwal(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAsianStandingPAwal prefab
        // Example:
        antroControl2.startPoints = new GameObject[7];
        antroControl2.endPoints = new GameObject[7];
        // Rotation
        antroControl2.rotationAngles = new float[7];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 180f;
        antroControl2.rotationAngles[4] = 180f;
        antroControl2.rotationAngles[5] = 180f;
        antroControl2.rotationAngles[6] = 180f;

        // Load FemaleAsianStandingP1 prefab from asset bundle
        var FemaleAsianStandingP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleAsianStandingP1");
        if (FemaleAsianStandingP1Prefab != null)
        {
            antroControl2.nextPrefab = FemaleAsianStandingP1Prefab;
        }

        // Load FemaleAsianStandingPAwal prefab from asset bundle
        var femaleAsianStandingPAwalPrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianStandingPAwal");
        if (femaleAsianStandingPAwalPrefab != null)
        {
            femaleAsianStandingPAwalPrefab.tag = "Bot"; 
            if (femaleAsianStandingPAwalPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAsianStandingPAwal prefab
                var objectController = femaleAsianStandingPAwalPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAsianStandingPAwalPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAsianStandingPAwal prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 7 and find the corresponding start and end points
        for (int i = 1; i <= 7; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianFemale/1")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianFemale/2")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AsianFemale/3")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AsianFemale/4")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AsianFemale/5")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AsianFemale/6")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AsianFemale/7")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAsianStandingP1(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAsianStandingP1 prefab
        // Example:
        antroControl2.startPoints = new GameObject[3];
        antroControl2.endPoints = new GameObject[3];
        // Rotation
        antroControl2.rotationAngles = new float[3];
        antroControl2.rotationAngles[0] = 90f;
        antroControl2.rotationAngles[1] = 90f;
        antroControl2.rotationAngles[2] = 90f;

        // Load FemaleAsianStandingP2 prefab from asset bundle
        var femaleAsianStandingP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleAsianStandingP2");
        if (femaleAsianStandingP2Prefab != null)
        {
            antroControl2.nextPrefab = femaleAsianStandingP2Prefab;
        }

        // Load FemaleAsianStandingP1 prefab from asset bundle
        var femaleAsianStandingP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleAsianStandingP1");
        if (femaleAsianStandingP1Prefab != null)
        {
            femaleAsianStandingP1Prefab.tag = "Bot"; 
            if (femaleAsianStandingP1Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAsianStandingP1 prefab
                var objectController = femaleAsianStandingP1Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAsianStandingP1Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAsianStandingP1 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 3 and find the corresponding start and end points
        for (int i = 1; i <= 3; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianFemale/24")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianFemale/25")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AsianFemale/36")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAsianStandingP2(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAsianStandingP2 prefab
        // Example:
        antroControl2.startPoints = new GameObject[1];
        antroControl2.endPoints = new GameObject[1];
        // Rotation
        antroControl2.rotationAngles = new float[1];
        antroControl2.rotationAngles[0] = 180f;

        // Load FemaleAsianStandingTPose prefab from asset bundle
        var femaleAsianStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianStandingTPose");
        if (femaleAsianStandingTPosePrefab != null)
        {
            antroControl2.nextPrefab = femaleAsianStandingTPosePrefab;
        }

        // Load FemaleAsianStandingP2 prefab from asset bundle
        var femaleAsianStandingP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleAsianStandingP2");
        if (femaleAsianStandingP2Prefab != null)
        {
            femaleAsianStandingP2Prefab.tag = "Bot";
            if (femaleAsianStandingP2Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAsianStandingP2 prefab
                var objectController = femaleAsianStandingP2Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAsianStandingP2Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAsianStandingP2 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 1; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianFemale/34")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAsianStandingTPose(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAsianStandingTPose prefab
        // Example:
        antroControl2.startPoints = new GameObject[2];
        antroControl2.endPoints = new GameObject[2];
        // Rotation
        antroControl2.rotationAngles = new float[2];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;

        // Load FemaleAsianSitdownPAwal prefab from asset bundle
        var femaleAsianSitdownPAwalPrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianSitdownPAwal");
        if (femaleAsianSitdownPAwalPrefab != null)
        {
            antroControl2.nextPrefab = femaleAsianSitdownPAwalPrefab;
        }

        // Load FemaleAsianStandingTpose prefab from asset bundle
        var femaleAsianStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianStandingTPose");
        if (femaleAsianStandingTPosePrefab != null)
        {
            femaleAsianStandingTPosePrefab.tag = "Bot";
            if (femaleAsianStandingTPosePrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAsianStandingTPose prefab
                var objectController = femaleAsianStandingTPosePrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAsianStandingTPosePrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAsianStandingTPose prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianFemale/32")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianFemale/33")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAsianSitdownPAwal(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAsianSitdownPAwal prefab
        // Example:
        antroControl2.startPoints = new GameObject[9];
        antroControl2.endPoints = new GameObject[9];
        // Rotation
        antroControl2.rotationAngles = new float[9];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 0f;
        antroControl2.rotationAngles[4] = 90f;
        antroControl2.rotationAngles[5] = 90f;
        antroControl2.rotationAngles[6] = -90f;
        antroControl2.rotationAngles[7] = 90f;
        antroControl2.rotationAngles[8] = -90f;

        // Load FemaleAsianSitdownPAwal prefab from asset bundle
        var femaleAsianSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleAsianSitdownP1");
        if (femaleAsianSitdownP1Prefab != null)
        {
            antroControl2.nextPrefab = femaleAsianSitdownP1Prefab;
        }

        // Load FemleAsianSitdownPAwal prefab from asset bundle
        var femaleAsianSitdownPAwalPrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianSitdownPAwal");
        if (femaleAsianSitdownPAwalPrefab != null)
        {
            femaleAsianSitdownPAwalPrefab.tag = "Bot"; 
            if (femaleAsianSitdownPAwalPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAsianSitdownPAwal prefab
                var objectController = femaleAsianSitdownPAwalPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAsianSitdownPAwalPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAsianSitdownPAwal prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        for (int i = 1; i <= 9; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject confirmationCanvas = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvas != null)
        {
            // Find the TeksAntro panel within the ConfirmationCanvas
            Transform teksAntroPanelTransform = confirmationCanvas.transform.Find("TeksAntro");
            if (teksAntroPanelTransform != null)
            {
                antroControl2.teksAntroPanel = teksAntroPanelTransform.gameObject;
            }
            
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvas.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianFemale/27")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianFemale/18")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AsianFemale/17")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AsianFemale/19")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AsianFemale/26")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AsianFemale/20")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AsianFemale/21")?.gameObject;
            antroControl2.predefinedDistances[7].panel = antroData.transform.Find("AsianFemale/13")?.gameObject;
            antroControl2.predefinedDistances[8].panel = antroData.transform.Find("AsianFemale/14")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAsianSitdownP1(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAsianSitdownP1 prefab
        // Example:
        antroControl2.startPoints = new GameObject[7];
        antroControl2.endPoints = new GameObject[7];
        // Rotation
        antroControl2.rotationAngles = new float[7];
        antroControl2.rotationAngles[0] = 90f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 180f;
        antroControl2.rotationAngles[4] = 180f;
        antroControl2.rotationAngles[5] = 180f;
        antroControl2.rotationAngles[6] = 180f;

        // Load FemaleAsianSitdownP2 prefab from asset bundle
        var femaleAsianSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleAsianSitdownP2");
        if (femaleAsianSitdownP2Prefab != null)
        {
            antroControl2.nextPrefab = femaleAsianSitdownP2Prefab;
        }

        // Load FemaleAsianSitdownP1 prefab from asset bundle
        var femaleAsianSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleAsianSitdownP1");
        if (femaleAsianSitdownP1Prefab != null)
        {
            femaleAsianSitdownP1Prefab.tag = "Bot";
            if (femaleAsianSitdownP1Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAsianSitdownP1 prefab
                var objectController = femaleAsianSitdownP1Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAsianSitdownP1Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAsianSitdown1 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 7; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianFemale/8")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianFemale/9")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AsianFemale/10")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AsianFemale/11")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AsianFemale/12")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AsianFemale/16")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AsianFemale/15")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAsianSitdownP2(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAsianSitdownP2 prefab
        // Example:
        antroControl2.startPoints = new GameObject[3];
        antroControl2.endPoints = new GameObject[3];
        // Rotation
        antroControl2.rotationAngles = new float[3];
        antroControl2.rotationAngles[0] = -90f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;

        // Load FemmaleAsianHand prefab from asset bundle
        var femaleAsianHandPrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianHand");
        if (femaleAsianHandPrefab != null)
        {
            antroControl2.nextPrefab = femaleAsianHandPrefab;
        }

        // Load FemaleAsianStandingP1 prefab from asset bundle
        var femaleAsianSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleAsianSitdownP2");
        if (femaleAsianSitdownP2Prefab != null)
        {
            femaleAsianSitdownP2Prefab.tag = "Bot";
            if (femaleAsianSitdownP2Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAsianSitdownP2 prefab
                var objectController = femaleAsianSitdownP2Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAsianSitdownP2Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAsianSitdownP2 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 3; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianFemale/22")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianFemale/23")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AsianFemale/35")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAsianHand(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAsianHand prefab
        // Example:
        antroControl2.startPoints = new GameObject[2];
        antroControl2.endPoints = new GameObject[2];

        // Load FemaleAsianFeet prefab from asset bundle
        var femaleAsianFeetPrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianFeet");
        if (femaleAsianFeetPrefab != null)
        {
            antroControl2.nextPrefab = femaleAsianFeetPrefab;
        }

        // Load FemaleAsianHand prefab from asset bundle
        var femaleAsianHandPrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianHand");
        if (femaleAsianHandPrefab != null)
        {
            femaleAsianHandPrefab.tag = "Bot";
            if (femaleAsianHandPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAsianHand prefab
                var objectController = femaleAsianHandPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAsianHandPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAsianHand prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 2f;
                    objectController.maxX = 6f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                    objectController.nonRotatableObject = femaleAsianHandPrefab;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianFemale/28")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianFemale/29")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAsianFeet(AntroControl antroControl, GameObject prefab)
    {
        // Customize start and end points for FemaleAsianFeet prefab
        // Example:
        antroControl.startPoints = new GameObject[2];
        antroControl.endPoints = new GameObject[2];

        // Load FemaleAsianFeet prefab from asset bundle
        var femaleAsianFeetPrefab = assetBundle.LoadAsset<GameObject>("FemaleAsianFeet");
        if (femaleAsianFeetPrefab != null)
        {
            femaleAsianFeetPrefab.tag = "Bot";
            if (femaleAsianFeetPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAsianFeet prefab
                var objectController = femaleAsianFeetPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAsianFeetPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAsianFeet prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 1f;
                    objectController.maxX = 4f;
                    objectController.minY = 1f;
                    objectController.maxY = 1.5f;
                    objectController.nonRotatableObject = femaleAsianFeetPrefab;
                }

                // Assign the Object Controller component to antroControl
                antroControl.objectControl = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 2 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl.predefinedDistances[0].panel = antroData.transform.Find("AsianFemale/30")?.gameObject;
            antroControl.predefinedDistances[1].panel = antroData.transform.Find("AsianFemale/31")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleEuStandingPAwal(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleEuStandingPAwal prefab
        // Example:
        antroControl2.startPoints = new GameObject[7];
        antroControl2.endPoints = new GameObject[7];
        // Rotation
        antroControl2.rotationAngles = new float[7];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 180f;
        antroControl2.rotationAngles[4] = 180f;
        antroControl2.rotationAngles[5] = 180f;
        antroControl2.rotationAngles[6] = 180f;

        // Load maleEuStandingP1 prefab from asset bundle
        var maleEuStandingP1Prefab = assetBundle.LoadAsset<GameObject>("MaleEuStandingP1");
        if (maleEuStandingP1Prefab != null)
        {
            antroControl2.nextPrefab = maleEuStandingP1Prefab;
        }

        // Load MaleEuStandingPAwal prefab from asset bundle
        var maleEuStandingPAwalPrefab = assetBundle.LoadAsset<GameObject>("MaleEuStandingPAwal");
        if (maleEuStandingPAwalPrefab != null)
        {
            maleEuStandingPAwalPrefab.tag = "Bot"; 
            if (maleEuStandingPAwalPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleEuStandingPAwal prefab
                var objectController = maleEuStandingPAwalPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleEuStandingPAwalPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleEuStandingPAwal prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 7 and find the corresponding start and end points
        for (int i = 1; i <= 7; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuMale/1")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuMale/2")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("EuMale/3")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("EuMale/4")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("EuMale/5")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("EuMale/6")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("EuMale/7")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleEuStandingP1(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleEuStandingP1 prefab
        // Example:
        antroControl2.startPoints = new GameObject[3];
        antroControl2.endPoints = new GameObject[3];
        // Rotation
        antroControl2.rotationAngles = new float[3];
        antroControl2.rotationAngles[0] = 90f;
        antroControl2.rotationAngles[1] = 90f;
        antroControl2.rotationAngles[2] = 90f;

        // Load MaleEuStandingP2 prefab from asset bundle
        var maleEuStandingP2Prefab = assetBundle.LoadAsset<GameObject>("MaleEuStandingP2");
        if (maleEuStandingP2Prefab != null)
        {
            antroControl2.nextPrefab = maleEuStandingP2Prefab;
        }

        // Load MaleEuStandingP1 prefab from asset bundle
        var maleEuStandingP1Prefab = assetBundle.LoadAsset<GameObject>("MaleEuStandingP1");
        if (maleEuStandingP1Prefab != null)
        {
            maleEuStandingP1Prefab.tag = "Bot"; // Assigning "Bot" tag to MaleEuStandingP1 prefab

            // Check if the prefab has the "Bot" tag
            if (maleEuStandingP1Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleEuStandingP1 prefab
                var objectController = maleEuStandingP1Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleEuStandingP1Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleEuStandingP1 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 3 and find the corresponding start and end points
        for (int i = 1; i <= 3; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                } 
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuMale/24")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuMale/25")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("EuMale/36")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleEuStandingP2(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleEuStandingP2 prefab
        // Example:
        antroControl2.startPoints = new GameObject[1];
        antroControl2.endPoints = new GameObject[1];
        // Rotation
        antroControl2.rotationAngles = new float[1];
        antroControl2.rotationAngles[0] = 180f;

        // Load MaleEuStandingTPose prefab from asset bundle
        var maleEuStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("MaleEuStandingTPose");
        if (maleEuStandingTPosePrefab != null)
        {
            antroControl2.nextPrefab = maleEuStandingTPosePrefab;
        }

        // Load MaleEuStandingP2 prefab from asset bundle
        var maleEuStandingP2Prefab = assetBundle.LoadAsset<GameObject>("MaleEuStandingP2");
        if (maleEuStandingP2Prefab != null)
        {
            maleEuStandingP2Prefab.tag = "Bot";
            if (maleEuStandingP2Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleEuStandingP2 prefab
                var objectController = maleEuStandingP2Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleEuStandingP2Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleEuStandingP2 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 1; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuMale/34")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleEuStandingTPose(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleEuStandingTPose prefab
        // Example:
        antroControl2.startPoints = new GameObject[2];
        antroControl2.endPoints = new GameObject[2];
        // Rotation
        antroControl2.rotationAngles = new float[2];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;

        // Load MaleEuSitdownPAwal prefab from asset bundle
        var maleEuSitdownPAwalPrefab = assetBundle.LoadAsset<GameObject>("MaleEuSitdownPAwal");
        if (maleEuSitdownPAwalPrefab != null)
        {
            antroControl2.nextPrefab = maleEuSitdownPAwalPrefab;
        }

        // Load MaleEuTPose prefab from asset bundle
        var maleEuStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("MaleEuStandingTPose");
        if (maleEuStandingTPosePrefab != null)
        {
            maleEuStandingTPosePrefab.tag = "Bot";
            if (maleEuStandingTPosePrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleEuStandingTPose prefab
                var objectController = maleEuStandingTPosePrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleEuStandingTPosePrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleEuStandingTPose prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuMale/32")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuMale/33")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleEuSitdownPAwal(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleEuSitdownPAwal prefab
        // Example:
        antroControl2.startPoints = new GameObject[9];
        antroControl2.endPoints = new GameObject[9];
        // Rotation
        antroControl2.rotationAngles = new float[9];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 0f;
        antroControl2.rotationAngles[4] = 90f;
        antroControl2.rotationAngles[5] = 90f;
        antroControl2.rotationAngles[6] = -90f;
        antroControl2.rotationAngles[7] = 90f;
        antroControl2.rotationAngles[8] = -90f;

        // Load MaleEuSitdownP1 prefab from asset bundle
        var MaleEuSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("MaleEuSitdownP1");
        if (MaleEuSitdownP1Prefab != null)
        {
            antroControl2.nextPrefab = MaleEuSitdownP1Prefab;
        }

        // Load MaleEuSitdownPAwal prefab from asset bundle
        var maleEuSitdownPAwalPrefab = assetBundle.LoadAsset<GameObject>("MaleEuSitdownPAwal");
        if (maleEuSitdownPAwalPrefab != null)
        {
            maleEuSitdownPAwalPrefab.tag = "Bot"; 
            if (maleEuSitdownPAwalPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleEuSitdownPAwal prefab
                var objectController = maleEuSitdownPAwalPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleEuSitdownPAwalPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleEuSitdownPAwal prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        for (int i = 1; i <= 9; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        GameObject confirmationCanvas = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvas != null)
        {
            // Find the TeksAntro panel within the ConfirmationCanvas
            Transform teksAntroPanelTransform = confirmationCanvas.transform.Find("TeksAntro");
            if (teksAntroPanelTransform != null)
            {
                antroControl2.teksAntroPanel = teksAntroPanelTransform.gameObject;
            }
            
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvas.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuMale/27")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuMale/18")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("EuMale/17")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("EuMale/19")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("EuMale/26")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("EuMale/20")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("EuMale/21")?.gameObject;
            antroControl2.predefinedDistances[7].panel = antroData.transform.Find("EuMale/13")?.gameObject;
            antroControl2.predefinedDistances[8].panel = antroData.transform.Find("EuMale/14")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleEuSitdownP1(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleEuSitdownP1 prefab
        // Example:
        antroControl2.startPoints = new GameObject[7];
        antroControl2.endPoints = new GameObject[7];
        // Rotation
        antroControl2.rotationAngles = new float[7];
        antroControl2.rotationAngles[0] = 90f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 180f;
        antroControl2.rotationAngles[4] = 180f;
        antroControl2.rotationAngles[5] = 180f;
        antroControl2.rotationAngles[6] = 180f;

        // Load maleEuSitdownP2 prefab from asset bundle
        var maleEuSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("MaleEuSitdownP2");
        if (maleEuSitdownP2Prefab != null)
        {
            antroControl2.nextPrefab = maleEuSitdownP2Prefab;
        }

        // Load MaleEuStandingP1 prefab from asset bundle
        var maleEuSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("MaleEuSitdownP1");
        if (maleEuSitdownP1Prefab != null)
        {
            maleEuSitdownP1Prefab.tag = "Bot";
            if (maleEuSitdownP1Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleEuStandingP1 prefab
                var objectController = maleEuSitdownP1Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleEuSitdownP1Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleEuStandingP1 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 7; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuMale/8")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuMale/9")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("EuMale/10")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("EuMale/11")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("EuMale/12")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("EuMale/16")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("EuMale/15")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleEuSitdownP2(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleEuSitdownP2 prefab
        // Example:
        antroControl2.startPoints = new GameObject[3];
        antroControl2.endPoints = new GameObject[3];
        // Rotation
        antroControl2.rotationAngles = new float[3];
        antroControl2.rotationAngles[0] = -90f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;

        // Load maleEuHand prefab from asset bundle
        var maleEuHandPrefab = assetBundle.LoadAsset<GameObject>("MaleEuHand");
        if (maleEuHandPrefab != null)
        {
            antroControl2.nextPrefab = maleEuHandPrefab;
        }

        // Load MaleEuSitdownP2 prefab from asset bundle
        var maleEuSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("MaleEuSitdownP2");
        if (maleEuSitdownP2Prefab != null)
        {
            maleEuSitdownP2Prefab.tag = "Bot";
            if (maleEuSitdownP2Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the maleEuSitdownP2 prefab
                var objectController = maleEuSitdownP2Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleEuSitdownP2Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to maleEuSitdownP2 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 3; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuMale/22")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuMale/23")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("EuMale/35")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleEuHand(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleEuHand prefab
        // Example:
        antroControl2.startPoints = new GameObject[2];
        antroControl2.endPoints = new GameObject[2];

        // Load MaleEuFeet prefab from asset bundle
        var maleEuFeetPrefab = assetBundle.LoadAsset<GameObject>("MaleEuFeet");
        if (maleEuFeetPrefab != null)
        {
            antroControl2.nextPrefab = maleEuFeetPrefab;
        }

        // Load MaleEuHand prefab from asset bundle
        var maleEuHandPrefab = assetBundle.LoadAsset<GameObject>("MaleEuHand");
        if (maleEuHandPrefab != null)
        {
            maleEuHandPrefab.tag = "Bot";
            if (maleEuHandPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleEuHand prefab
                var objectController = maleEuHandPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleEuHandPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleEuHand prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 2f;
                    objectController.maxX = 6f;
                    objectController.minY = 0.5f;
                    objectController.maxY = 1.5f;
                    objectController.nonRotatableObject = maleEuHandPrefab;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuMale/28")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuMale/29")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleEuFeet(AntroControl antroControl, GameObject prefab)
    {
        // Customize start and end points for MaleEuFeet prefab
        // Example:
        antroControl.startPoints = new GameObject[2];
        antroControl.endPoints = new GameObject[2];

        // Load MaleEuFeet prefab from asset bundle
        var maleEuFeetPrefab = assetBundle.LoadAsset<GameObject>("MaleEuFeet");
        if (maleEuFeetPrefab != null)
        {
            maleEuFeetPrefab.tag = "Bot";
            if (maleEuFeetPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleEuFeet prefab
                var objectController = maleEuFeetPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleEuFeetPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleEuFeet prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 1f;
                    objectController.maxX = 4f;
                    objectController.minY = 1f;
                    objectController.maxY = 1.5f;
                    objectController.nonRotatableObject = maleEuFeetPrefab;
                }

                // Assign the Object Controller component to antroControl
                antroControl.objectControl = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 2 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl.predefinedDistances[0].panel = antroData.transform.Find("EuMale/30")?.gameObject;
            antroControl.predefinedDistances[1].panel = antroData.transform.Find("EuMale/31")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleEuStandingPAwal(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleEuStandingPAwal prefab
        // Example:
        antroControl2.startPoints = new GameObject[7];
        antroControl2.endPoints = new GameObject[7];
        // Rotation
        antroControl2.rotationAngles = new float[7];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 180f;
        antroControl2.rotationAngles[4] = 180f;
        antroControl2.rotationAngles[5] = 180f;
        antroControl2.rotationAngles[6] = 180f;

        // Load FemaleEuStandingP1 prefab from asset bundle
        var FemaleEuStandingP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleEuStandingP1");
        if (FemaleEuStandingP1Prefab != null)
        {
            antroControl2.nextPrefab = FemaleEuStandingP1Prefab;
        }

        // Load FemaleEuStandingPAwal prefab from asset bundle
        var femaleEuStandingPAwalPrefab = assetBundle.LoadAsset<GameObject>("FemaleEuStandingPAwal");
        if (femaleEuStandingPAwalPrefab != null)
        {
            femaleEuStandingPAwalPrefab.tag = "Bot"; 
            if (femaleEuStandingPAwalPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleEuStandingPAwal prefab
                var objectController = femaleEuStandingPAwalPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleEuStandingPAwalPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleEuStandingPAwal prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 7 and find the corresponding start and end points
        for (int i = 1; i <= 7; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuFemale/1")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuFemale/2")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("EuFemale/3")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("EuFemale/4")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("EuFemale/5")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("EuFemale/6")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("EuFemale/7")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleEuStandingP1(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleEuStandingP1 prefab
        // Example:
        antroControl2.startPoints = new GameObject[3];
        antroControl2.endPoints = new GameObject[3];
        // Rotation
        antroControl2.rotationAngles = new float[3];
        antroControl2.rotationAngles[0] = 90f;
        antroControl2.rotationAngles[1] = 90f;
        antroControl2.rotationAngles[2] = 90f;

        // Load FemaleEuStandingP2 prefab from asset bundle
        var femaleEuStandingP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleEuStandingP2");
        if (femaleEuStandingP2Prefab != null)
        {
            antroControl2.nextPrefab = femaleEuStandingP2Prefab;
        }

        // Load FemaleEuStandingP1 prefab from asset bundle
        var femaleEuStandingP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleEuStandingP1");
        if (femaleEuStandingP1Prefab != null)
        {
            femaleEuStandingP1Prefab.tag = "Bot"; 
            if (femaleEuStandingP1Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleEuStandingP1 prefab
                var objectController = femaleEuStandingP1Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleEuStandingP1Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleEuStandingP1 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 3 and find the corresponding start and end points
        for (int i = 1; i <= 3; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuFemale/24")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuFemale/25")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("EuFemale/36")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleEuStandingP2(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleEuStandingP2 prefab
        // Example:
        antroControl2.startPoints = new GameObject[1];
        antroControl2.endPoints = new GameObject[1];
        // Rotation
        antroControl2.rotationAngles = new float[1];
        antroControl2.rotationAngles[0] = 180f;

        // Load FemaleEuStandingTPose prefab from asset bundle
        var femaleEuStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("FemaleEuStandingTPose");
        if (femaleEuStandingTPosePrefab != null)
        {
            antroControl2.nextPrefab = femaleEuStandingTPosePrefab;
        }

        // Load FemaleEuStandingP2 prefab from asset bundle
        var femaleEuStandingP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleEuStandingP2");
        if (femaleEuStandingP2Prefab != null)
        {
            femaleEuStandingP2Prefab.tag = "Bot";
            if (femaleEuStandingP2Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleEuStandingP2 prefab
                var objectController = femaleEuStandingP2Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleEuStandingP2Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleEuStandingP2 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 1; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuFemale/34")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleEuStandingTPose(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleEuStandingTPose prefab
        // Example:
        antroControl2.startPoints = new GameObject[2];
        antroControl2.endPoints = new GameObject[2];
        // Rotation
        antroControl2.rotationAngles = new float[2];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;

        // Load FemaleEuSitdownPAwal prefab from asset bundle
        var femaleEuSitdownPAwalPrefab = assetBundle.LoadAsset<GameObject>("FemaleEuSitdownPAwal");
        if (femaleEuSitdownPAwalPrefab != null)
        {
            antroControl2.nextPrefab = femaleEuSitdownPAwalPrefab;
        }

        // Load FemaleEuTPose prefab from asset bundle
        var femaleEuStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("FemaleEuStandingTPose");
        if (femaleEuStandingTPosePrefab != null)
        {
            femaleEuStandingTPosePrefab.tag = "Bot";
            if (femaleEuStandingTPosePrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleEuStandingTPose prefab
                var objectController = femaleEuStandingTPosePrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleEuStandingTPosePrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleEuStandingTPose prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuFemale/32")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuFemale/33")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleEuSitdownPAwal(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleEuSitdownPAwal prefab
        // Example:
        antroControl2.startPoints = new GameObject[9];
        antroControl2.endPoints = new GameObject[9];
        // Rotation
        antroControl2.rotationAngles = new float[9];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 0f;
        antroControl2.rotationAngles[4] = 90f;
        antroControl2.rotationAngles[5] = 90f;
        antroControl2.rotationAngles[6] = -90f;
        antroControl2.rotationAngles[7] = 90f;
        antroControl2.rotationAngles[8] = -90f;

        // Load FemaleEuStandingP1 prefab from asset bundle
        var FemaleEuSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleEuSitdownP1");
        if (FemaleEuSitdownP1Prefab != null)
        {
            antroControl2.nextPrefab = FemaleEuSitdownP1Prefab;
        }

        // Load FemaleEuSitdownPAwal prefab from asset bundle
        var femaleEuSitdownPAwalPrefab = assetBundle.LoadAsset<GameObject>("FemaleEuSitdownPAwal");
        if (femaleEuSitdownPAwalPrefab != null)
        {
            femaleEuSitdownPAwalPrefab.tag = "Bot";
            if (femaleEuSitdownPAwalPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleEuSitdownPAwalPrefab 
                var objectController = femaleEuSitdownPAwalPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleEuSitdownPAwalPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleEuSitdownPAwalPrefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        for (int i = 1; i <= 9; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        GameObject confirmationCanvas = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvas != null)
        {
            // Find the TeksAntro panel within the ConfirmationCanvas
            Transform teksAntroPanelTransform = confirmationCanvas.transform.Find("TeksAntro");
            if (teksAntroPanelTransform != null)
            {
                antroControl2.teksAntroPanel = teksAntroPanelTransform.gameObject;
            }

            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvas.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AsianMale/27")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AsianMale/18")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AsianMale/17")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AsianMale/19")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AsianMale/26")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AsianMale/20")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AsianMale/21")?.gameObject;
            antroControl2.predefinedDistances[7].panel = antroData.transform.Find("AsianMale/13")?.gameObject;
            antroControl2.predefinedDistances[8].panel = antroData.transform.Find("AsianMale/14")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleEuSitdownP1(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleEuSitdownP1 prefab
        // Example:
        antroControl2.startPoints = new GameObject[7];
        antroControl2.endPoints = new GameObject[7];
        // Rotation
        antroControl2.rotationAngles = new float[7];
        antroControl2.rotationAngles[0] = 90f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 180f;
        antroControl2.rotationAngles[4] = 180f;
        antroControl2.rotationAngles[5] = 180f;
        antroControl2.rotationAngles[6] = 180f;

        // Load FemaleEuSitdownP2 prefab from asset bundle
        var femaleEuSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleEuSitdownP2");
        if (femaleEuSitdownP2Prefab != null)
        {
            antroControl2.nextPrefab = femaleEuSitdownP2Prefab;
        }

        // Load FemaleEuSitdownP1 prefab from asset bundle
        var femaleEuSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleEuSitdownP1");
        if (femaleEuSitdownP1Prefab != null)
        {
            femaleEuSitdownP1Prefab.tag = "Bot";
            if (femaleEuSitdownP1Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleEuSitdownP1 prefab
                var objectController = femaleEuSitdownP1Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleEuSitdownP1Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleEuSitdownP1 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 7; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuFemale/8")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuFemale/9")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("EuFemale/10")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("EuFemale/11")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("EuFemale/12")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("EuFemale/16")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("EuFemale/15")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleEuSitdownP2(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleEuSitdownP2 prefab
        // Example:
        antroControl2.startPoints = new GameObject[3];
        antroControl2.endPoints = new GameObject[3];
        // Rotation
        antroControl2.rotationAngles = new float[3];
        antroControl2.rotationAngles[0] = -90f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;

        // Load FemaleEuHand prefab from asset bundle
        var femaleEuHandPrefab = assetBundle.LoadAsset<GameObject>("FemaleEuHand");
        if (femaleEuHandPrefab != null)
        {
            antroControl2.nextPrefab = femaleEuHandPrefab;
        }

        // Load FemaleEuSitdownP2 prefab from asset bundle
        var femaleEuSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleEuSitdownP2");
        if (femaleEuSitdownP2Prefab != null)
        {
            femaleEuSitdownP2Prefab.tag = "Bot";
            if (femaleEuSitdownP2Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleEuSitdownP2 prefab
                var objectController = femaleEuSitdownP2Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleEuSitdownP2Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleEuSitdownP2 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 3; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuFemale/22")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuFemale/23")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("EuFemale/35")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleEuHand(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleEuHand prefab
        // Example:
        antroControl2.startPoints = new GameObject[2];
        antroControl2.endPoints = new GameObject[2];

        // Load FemaleEuFeet prefab from asset bundle
        var femaleEuFeetPrefab = assetBundle.LoadAsset<GameObject>("FemaleEuFeet");
        if (femaleEuFeetPrefab != null)
        {
            antroControl2.nextPrefab = femaleEuFeetPrefab;
        }

        // Load FemaleEuHand prefab from asset bundle
        var femaleEuHandPrefab = assetBundle.LoadAsset<GameObject>("FemaleEuHand");
        if (femaleEuHandPrefab != null)
        {
            femaleEuHandPrefab.tag = "Bot";
            if (femaleEuHandPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleEuHand prefab
                var objectController = femaleEuHandPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleEuHandPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleEuHand prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 2f;
                    objectController.maxX = 6f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                    objectController.nonRotatableObject = femaleEuHandPrefab;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("EuFemale/28")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("EuFemale/29")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleEuFeet(AntroControl antroControl, GameObject prefab)
    {
        // Customize start and end points for FemaleEuFeet prefab
        // Example:
        antroControl.startPoints = new GameObject[2];
        antroControl.endPoints = new GameObject[2];

        // Load FemaleEuFeet prefab from asset bundle
        var femaleEuFeetPrefab = assetBundle.LoadAsset<GameObject>("FemaleEuFeet");
        if (femaleEuFeetPrefab != null)
        {
            femaleEuFeetPrefab.tag = "Bot";
            if (femaleEuFeetPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleEuFeet prefab
                var objectController = femaleEuFeetPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleEuFeetPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleEuFeet prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 1f;
                    objectController.maxX = 4f;
                    objectController.minY = 1f;
                    objectController.maxY = 1.5f;
                    objectController.nonRotatableObject = femaleEuFeetPrefab;
                }

                // Assign the Object Controller component to antroControl
                antroControl.objectControl = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 2 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl.predefinedDistances[0].panel = antroData.transform.Find("EuFemale/30")?.gameObject;
            antroControl.predefinedDistances[1].panel = antroData.transform.Find("EuFemale/31")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAmericanStandingPAwal(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAmericanStandingPAwal prefab
        // Example:
        antroControl2.startPoints = new GameObject[7];
        antroControl2.endPoints = new GameObject[7];
        // Rotation
        antroControl2.rotationAngles = new float[7];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 180f;
        antroControl2.rotationAngles[4] = 180f;
        antroControl2.rotationAngles[5] = 180f;
        antroControl2.rotationAngles[6] = 180f;

        // Load MaleAmericanStandingP1 prefab from asset bundle
        var maleAmericanStandingP1Prefab = assetBundle.LoadAsset<GameObject>("MaleAmericanStandingP1");
        if (maleAmericanStandingP1Prefab != null)
        {
            antroControl2.nextPrefab = maleAmericanStandingP1Prefab;
        }

        // Load MaleAmericanStandingPAwal prefab from asset bundle
        var maleAmericanStandingPAwalPrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanStandingPAwal");
        if (maleAmericanStandingPAwalPrefab != null)
        {
            maleAmericanStandingPAwalPrefab.tag = "Bot"; 
            if (maleAmericanStandingPAwalPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAmericanStandingPAwalprefab 
                var objectController = maleAmericanStandingPAwalPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAmericanStandingPAwalPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAmericanStandingPAwalprefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 7 and find the corresponding start and end points
        for (int i = 1; i <= 7; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanMale/1")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanMale/2")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AmericanMale/3")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AmericanMale/4")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AmericanMale/5")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AmericanMale/6")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AmericanMale/7")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAmericanStandingP1(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAmericanStandingP1 prefab
        // Example:
        antroControl2.startPoints = new GameObject[3];
        antroControl2.endPoints = new GameObject[3];
        // Rotation
        antroControl2.rotationAngles = new float[3];
        antroControl2.rotationAngles[0] = 90f;
        antroControl2.rotationAngles[1] = 90f;
        antroControl2.rotationAngles[2] = 90f;

        // Load MaleAmericanStandingP2 prefab from asset bundle
        var maleAmericanStandingP2Prefab = assetBundle.LoadAsset<GameObject>("MaleAmericanStandingP2");
        if (maleAmericanStandingP2Prefab != null)
        {
            antroControl2.nextPrefab = maleAmericanStandingP2Prefab;
        }

        // Load MaleAmericanStandingP1 prefab from asset bundle
        var maleAmericanStandingP1Prefab = assetBundle.LoadAsset<GameObject>("MaleAmericanStandingP1");
        if (maleAmericanStandingP1Prefab != null)
        {
            maleAmericanStandingP1Prefab.tag = "Bot"; 
            if (maleAmericanStandingP1Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAmericanStandingP1 prefab
                var objectController = maleAmericanStandingP1Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAmericanStandingP1Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAmericanStandingP1 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 3 and find the corresponding start and end points
        for (int i = 1; i <= 3; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanMale/24")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanMale/25")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AmericanMale/36")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAmericanStandingP2(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAmericanStandingP2 prefab
        // Example:
        antroControl2.startPoints = new GameObject[1];
        antroControl2.endPoints = new GameObject[1];
        // Rotation
        antroControl2.rotationAngles = new float[1];
        antroControl2.rotationAngles[0] = 180f;

        // Load MaleAmericanStandingTPose prefab from asset bundle
        var maleAmericanStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanStandingTPose");
        if (maleAmericanStandingTPosePrefab != null)
        {
            antroControl2.nextPrefab = maleAmericanStandingTPosePrefab;
        }

        // Load MaleAmericanStandingP2 prefab from asset bundle
        var maleAmericanStandingP2Prefab = assetBundle.LoadAsset<GameObject>("MaleAmericanStandingP2");
        if (maleAmericanStandingP2Prefab != null)
        {
            maleAmericanStandingP2Prefab.tag = "Bot";
            if (maleAmericanStandingP2Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAmericanStandingP2 prefab
                var objectController = maleAmericanStandingP2Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAmericanStandingP2Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAmericanStandingP2 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 1; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanMale/34")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAmericanStandingTPose(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAmericanStandingTPose prefab
        // Example:
        antroControl2.startPoints = new GameObject[2];
        antroControl2.endPoints = new GameObject[2];
        // Rotation
        antroControl2.rotationAngles = new float[2];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;

        // Load MaleAmericanSitdownPAwal prefab from asset bundle
        var maleAmericanSitdownPAwalPrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanSitdownPAwal");
        if (maleAmericanSitdownPAwalPrefab != null)
        {
            antroControl2.nextPrefab = maleAmericanSitdownPAwalPrefab;
        }

        // Load MaleAmericanStandingTpose prefab from asset bundle
        var maleAmericanStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanStandingTPose");
        if (maleAmericanStandingTPosePrefab != null)
        {
            maleAmericanStandingTPosePrefab.tag = "Bot";
            if (maleAmericanStandingTPosePrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAmericanStandingTpose prefab
                var objectController = maleAmericanStandingTPosePrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAmericanStandingTPosePrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAmericanStandingTpose prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanMale/32")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanMale/33")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAmericanSitdownPAwal(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAmericanSitdownPAwal prefab
        // Example:
        antroControl2.startPoints = new GameObject[9];
        antroControl2.endPoints = new GameObject[9];
        // Rotation
        antroControl2.rotationAngles = new float[9];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 0f;
        antroControl2.rotationAngles[4] = 90f;
        antroControl2.rotationAngles[5] = 90f;
        antroControl2.rotationAngles[6] = -90f;
        antroControl2.rotationAngles[7] = 90f;
        antroControl2.rotationAngles[8] = -90f;

        // Load MaleAmericanSitdownP1 prefab from asset bundle
        var maleAmericanSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("MaleAmericanSitdownP1");
        if (maleAmericanSitdownP1Prefab != null)
        {
            antroControl2.nextPrefab = maleAmericanSitdownP1Prefab;
        }

        // Load MaleAmericanSitdownPAwal prefab from asset bundle
        var maleAmericanSitdownPAwalPrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanSitdownPAwal");
        if (maleAmericanSitdownPAwalPrefab != null)
        {
            maleAmericanSitdownPAwalPrefab.tag = "Bot";
            if (maleAmericanSitdownPAwalPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAmericanSitdownPAwalprefab 
                var objectController = maleAmericanSitdownPAwalPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAmericanSitdownPAwalPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAmericanSitdownPAwalprefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        for (int i = 1; i <= 9; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        GameObject confirmationCanvas = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvas != null)
        {
            // Find the TeksAntro panel within the ConfirmationCanvas
            Transform teksAntroPanelTransform = confirmationCanvas.transform.Find("TeksAntro");
            if (teksAntroPanelTransform != null)
            {
                antroControl2.teksAntroPanel = teksAntroPanelTransform.gameObject;
            }

            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvas.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            } 
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanMale/27")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanMale/18")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AmericanMale/17")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AmericanMale/19")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AmericanMale/26")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AmericanMale/20")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AmericanMale/21")?.gameObject;
            antroControl2.predefinedDistances[7].panel = antroData.transform.Find("AmericanMale/13")?.gameObject;
            antroControl2.predefinedDistances[8].panel = antroData.transform.Find("AmericanMale/14")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAmericanSitdownP1(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAmericanSitdownP1 prefab
        // Example:
        antroControl2.startPoints = new GameObject[7];
        antroControl2.endPoints = new GameObject[7];
        // Rotation
        antroControl2.rotationAngles = new float[7];
        antroControl2.rotationAngles[0] = 90f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 180f;
        antroControl2.rotationAngles[4] = 180f;
        antroControl2.rotationAngles[5] = 180f;
        antroControl2.rotationAngles[6] = 180f;

        // Load maleAmericanSitdownP2 prefab from asset bundle
        var maleAmericanSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("MaleAmericanSitdownP2");
        if (maleAmericanSitdownP2Prefab != null)
        {
            antroControl2.nextPrefab = maleAmericanSitdownP2Prefab;
        }

        // Load MaleAmericanSitdownP1 prefab from asset bundle
        var maleAmericanSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("MaleAmericanSitdownP1");
        if (maleAmericanSitdownP1Prefab != null)
        {
            maleAmericanSitdownP1Prefab.tag = "Bot";
            if (maleAmericanSitdownP1Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAmericanSitdownP1 prefab
                var objectController = maleAmericanSitdownP1Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAmericanSitdownP1Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to maleAmericanSitdownP1 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 7; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanMale/8")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanMale/9")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AmericanMale/10")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AmericanMale/11")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AmericanMale/12")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AmericanMale/16")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AmericanMale/15")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAmericanSitdownP2(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAmericanSitdownP2 prefab
        // Example:
        antroControl2.startPoints = new GameObject[3];
        antroControl2.endPoints = new GameObject[3];
        // Rotation
        antroControl2.rotationAngles = new float[3];
        antroControl2.rotationAngles[0] = -90f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;

        // Load maleAmericanHand prefab from asset bundle
        var maleAmericanHandPrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanHand");
        if (maleAmericanHandPrefab != null)
        {
            antroControl2.nextPrefab = maleAmericanHandPrefab;
        }

        // Load MaleAmericanSitdownP2 prefab from asset bundle
        var maleAmericanSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("MaleAmericanSitdownP2");
        if (maleAmericanSitdownP2Prefab != null)
        {
            maleAmericanSitdownP2Prefab.tag = "Bot";
            if (maleAmericanSitdownP2Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAmericanSitdownP2 prefab
                var objectController = maleAmericanSitdownP2Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAmericanSitdownP2Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to maleAmericanSitdownP2 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 3; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanMale/22")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanMale/23")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AmericanMale/35")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAmericanHand(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for MaleAmericanHand prefab
        // Example:
        antroControl2.startPoints = new GameObject[2];
        antroControl2.endPoints = new GameObject[2];

        // Load MaleAmericanFeet prefab from asset bundle
        var maleAmericanFeetPrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanFeet");
        if (maleAmericanFeetPrefab != null)
        {
            antroControl2.nextPrefab = maleAmericanFeetPrefab;
        }

        // Load MaleAmericanHand prefab from asset bundle
        var maleAmericanHandPrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanHand");
        if (maleAmericanHandPrefab != null)
        {
            maleAmericanHandPrefab.tag = "Bot";
            if (maleAmericanHandPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAmericanHand prefab
                var objectController = maleAmericanHandPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAmericanHandPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAmericanHand prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 2f;
                    objectController.maxX = 6f;
                    objectController.minY = 0.5f;
                    objectController.maxY = 1.5f;
                    objectController.nonRotatableObject = maleAmericanHandPrefab;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanMale/28")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanMale/29")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForMaleAmericanFeet(AntroControl antroControl, GameObject prefab)
    {
        // Customize start and end points for MaleAmericanFeet prefab
        // Example:
        antroControl.startPoints = new GameObject[2];
        antroControl.endPoints = new GameObject[2];

        // Load MaleAmericanFeet prefab from asset bundle
        var maleAmericanFeetPrefab = assetBundle.LoadAsset<GameObject>("MaleAmericanFeet");
        if (maleAmericanFeetPrefab != null)
        {
            maleAmericanFeetPrefab.tag = "Bot";
            if (maleAmericanFeetPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the MaleAmericanFeet prefab
                var objectController = maleAmericanFeetPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = maleAmericanFeetPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to MaleAmericanFeet prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 1f;
                    objectController.maxX = 4f;
                    objectController.minY = 1f;
                    objectController.maxY = 1.5f;
                    objectController.nonRotatableObject = maleAmericanFeetPrefab;
                }

                // Assign the Object Controller component to antroControl
                antroControl.objectControl = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 2 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl.predefinedDistances[0].panel = antroData.transform.Find("AmericanMale/30")?.gameObject;
            antroControl.predefinedDistances[1].panel = antroData.transform.Find("AmericanMale/31")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAmericanStandingPAwal(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAmericanStandingPAwal prefab
        // Example:
        antroControl2.startPoints = new GameObject[7];
        antroControl2.endPoints = new GameObject[7];
        // Rotation
        antroControl2.rotationAngles = new float[7];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 180f;
        antroControl2.rotationAngles[4] = 180f;
        antroControl2.rotationAngles[5] = 180f;
        antroControl2.rotationAngles[6] = 180f;

        // Load FemaleAmericanStandingP1 prefab from asset bundle
        var FemaleAmericanStandingP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanStandingP1");
        if (FemaleAmericanStandingP1Prefab != null)
        {
            antroControl2.nextPrefab = FemaleAmericanStandingP1Prefab;
        }

        // Load FemaleAmericanStandingPAwal prefab from asset bundle
        var femaleAmericanStandingPAwalPrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanStandingPAwal");
        if (femaleAmericanStandingPAwalPrefab != null)
        {
            femaleAmericanStandingPAwalPrefab.tag = "Bot";
            if (femaleAmericanStandingPAwalPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAmericanStandingPAwalprefab 
                var objectController = femaleAmericanStandingPAwalPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAmericanStandingPAwalPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAmericanStandingPAwalprefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 7 and find the corresponding start and end points
        for (int i = 1; i <= 7; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanFemale/1")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanFemale/2")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AmericanFemale/3")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AmericanFemale/4")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AmericanFemale/5")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AmericanFemale/6")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AmericanFemale/7")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAmericanStandingP1(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAmericanStandingP1 prefab
        // Example:
        antroControl2.startPoints = new GameObject[3];
        antroControl2.endPoints = new GameObject[3];
        // Rotation
        antroControl2.rotationAngles = new float[3];
        antroControl2.rotationAngles[0] = 90f;
        antroControl2.rotationAngles[1] = 90f;
        antroControl2.rotationAngles[2] = 90f;

        // Load FemaleAmericanStandingP2 prefab from asset bundle
        var femaleAmericanStandingP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanStandingP2");
        if (femaleAmericanStandingP2Prefab != null)
        {
            antroControl2.nextPrefab = femaleAmericanStandingP2Prefab;
        }

        // Load FemaleAmericanStandingP1 prefab from asset bundle
        var femaleAmericanStandingP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanStandingP1");
        if (femaleAmericanStandingP1Prefab != null)
        {
            femaleAmericanStandingP1Prefab.tag = "Bot";
            if (femaleAmericanStandingP1Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAmericanStandingP1 prefab
                var objectController = femaleAmericanStandingP1Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAmericanStandingP1Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAmericanStandingP1 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 3 and find the corresponding start and end points
        for (int i = 1; i <= 3; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanFemale/24")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanFemale/25")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AmericanFemale/36")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAmericanStandingP2(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAmericanStandingP2 prefab
        // Example:
        antroControl2.startPoints = new GameObject[1];
        antroControl2.endPoints = new GameObject[1];
        // Rotation
        antroControl2.rotationAngles = new float[1];
        antroControl2.rotationAngles[0] = 180f;

        // Load FemaleAmericanStandingTPose prefab from asset bundle
        var femaleAmericanStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanStandingTPose");
        if (femaleAmericanStandingTPosePrefab != null)
        {
            antroControl2.nextPrefab = femaleAmericanStandingTPosePrefab;
        }

        // Load FemaleAmericanStandingP2 prefab from asset bundle
        var femaleAmericanStandingP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanStandingP2");
        if (femaleAmericanStandingP2Prefab != null)
        {
            femaleAmericanStandingP2Prefab.tag = "Bot";
            if (femaleAmericanStandingP2Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAmericanStandingP2 prefab
                var objectController = femaleAmericanStandingP2Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAmericanStandingP2Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAmericanStandingP2 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 1; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanFemale/34")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAmericanStandingTPose(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAmericanStandingTPose prefab
        // Example:
        antroControl2.startPoints = new GameObject[2];
        antroControl2.endPoints = new GameObject[2];
        // Rotation
        antroControl2.rotationAngles = new float[2];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;

        // Load FemaleAmericanSitdownPAwal prefab from asset bundle
        var femaleAmericanSitdownPAwalPrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanSitdownPAwal");
        if (femaleAmericanSitdownPAwalPrefab != null)
        {
            antroControl2.nextPrefab = femaleAmericanSitdownPAwalPrefab;
        }

        // Load FemaleAmericanStandingTpose prefab from asset bundle
        var femaleAmericanStandingTPosePrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanStandingTPose");
        if (femaleAmericanStandingTPosePrefab != null)
        {
            femaleAmericanStandingTPosePrefab.tag = "Bot";
            if (femaleAmericanStandingTPosePrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAmericanStandingTPose prefab
                var objectController = femaleAmericanStandingTPosePrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAmericanStandingTPosePrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAmericanStandingTPose prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanFemale/32")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanFemale/33")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAmericanSitdownPAwal(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAmericanSitdownPAwal prefab
        // Example:
        antroControl2.startPoints = new GameObject[9];
        antroControl2.endPoints = new GameObject[9];
        // Rotation
        antroControl2.rotationAngles = new float[9];
        antroControl2.rotationAngles[0] = 180f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 0f;
        antroControl2.rotationAngles[4] = 90f;
        antroControl2.rotationAngles[5] = 90f;
        antroControl2.rotationAngles[6] = -90f;
        antroControl2.rotationAngles[7] = 90f;
        antroControl2.rotationAngles[8] = -90f;

        // Load FemaleAmericanSitdownP1 prefab from asset bundle
        var femaleAmericanSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanSitdownP1");
        if (femaleAmericanSitdownP1Prefab != null)
        {
            antroControl2.nextPrefab = femaleAmericanSitdownP1Prefab;
        }

        // Load FemaleAmericanSitdownPAwal prefab from asset bundle
        var femaleAmericanSitdownPAwalPrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanSitdownPAwal");
        if (femaleAmericanSitdownPAwalPrefab != null)
        {
            femaleAmericanSitdownPAwalPrefab.tag = "Bot";
            if (femaleAmericanSitdownPAwalPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAmericanSitdownPAwalprefab 
                var objectController = femaleAmericanSitdownPAwalPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAmericanSitdownPAwalPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAmericanSitdownPAwalprefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        for (int i = 1; i <= 9; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        GameObject confirmationCanvas = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvas != null)
        {
            // Find the TeksAntro panel within the ConfirmationCanvas
            Transform teksAntroPanelTransform = confirmationCanvas.transform.Find("TeksAntro");
            if (teksAntroPanelTransform != null)
            {
                antroControl2.teksAntroPanel = teksAntroPanelTransform.gameObject;
            }
       
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvas.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanFemale/27")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanFemale/18")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AmericanFemale/17")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AmericanFemale/19")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AmericanFemale/26")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AmericanFemale/20")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AmericanFemale/21")?.gameObject;
            antroControl2.predefinedDistances[7].panel = antroData.transform.Find("AmericanFemale/13")?.gameObject;
            antroControl2.predefinedDistances[8].panel = antroData.transform.Find("AmericanFemale/14")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAmericanSitdownP1(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAmericanSitdownP1 prefab
        // Example:
        antroControl2.startPoints = new GameObject[7];
        antroControl2.endPoints = new GameObject[7];
        // Rotation
        antroControl2.rotationAngles = new float[7];
        antroControl2.rotationAngles[0] = 90f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;
        antroControl2.rotationAngles[3] = 180f;
        antroControl2.rotationAngles[4] = 180f;
        antroControl2.rotationAngles[5] = 180f;
        antroControl2.rotationAngles[6] = 180f;

        // Load FemaleAmericanSitdownP2 prefab from asset bundle
        var femaleAmericanSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanSitdownP2");
        if (femaleAmericanSitdownP2Prefab != null)
        {
            antroControl2.nextPrefab = femaleAmericanSitdownP2Prefab;
        }

        // Load FemaleAmericanSitdownP1 prefab from asset bundle
        var femaleAmericanSitdownP1Prefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanSitdownP1");
        if (femaleAmericanSitdownP1Prefab != null)
        {
            femaleAmericanSitdownP1Prefab.tag = "Bot";
            if (femaleAmericanSitdownP1Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAmericanSitdownP1 prefab
                var objectController = femaleAmericanSitdownP1Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAmericanSitdownP1Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAmericanSitdownP1 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 7; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanFemale/8")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanFemale/9")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AmericanFemale/10")?.gameObject;
            antroControl2.predefinedDistances[3].panel = antroData.transform.Find("AmericanFemale/11")?.gameObject;
            antroControl2.predefinedDistances[4].panel = antroData.transform.Find("AmericanFemale/12")?.gameObject;
            antroControl2.predefinedDistances[5].panel = antroData.transform.Find("AmericanFemale/16")?.gameObject;
            antroControl2.predefinedDistances[6].panel = antroData.transform.Find("AmericanFemale/15")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAmericanSitdownP2(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAmericanSitdownP2 prefab
        // Example:
        antroControl2.startPoints = new GameObject[3];
        antroControl2.endPoints = new GameObject[3];
        // Rotation
        antroControl2.rotationAngles = new float[3];
        antroControl2.rotationAngles[0] = -90f;
        antroControl2.rotationAngles[1] = 180f;
        antroControl2.rotationAngles[2] = 180f;

        // Load FemmaleAmericanHand prefab from asset bundle
        var femaleAmericanHandPrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanHand");
        if (femaleAmericanHandPrefab != null)
        {
            antroControl2.nextPrefab = femaleAmericanHandPrefab;
        }

        // Load FemaleAmericanSitdownP2 prefab from asset bundle
        var femaleAmericanSitdownP2Prefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanSitdownP2");
        if (femaleAmericanSitdownP2Prefab != null)
        {
            femaleAmericanSitdownP2Prefab.tag = "Bot";
            if (femaleAmericanSitdownP2Prefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAmericanSitdownP2 prefab
                var objectController = femaleAmericanSitdownP2Prefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAmericanSitdownP2Prefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAmericanSitdownP2 prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 0.5f;
                    objectController.maxX = 4f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 3; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanFemale/22")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanFemale/23")?.gameObject;
            antroControl2.predefinedDistances[2].panel = antroData.transform.Find("AmericanFemale/35")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAmericanHand(AntroControl2 antroControl2, GameObject prefab)
    {
        // Customize start and end points for FemaleAmericanHand prefab
        // Example:
        antroControl2.startPoints = new GameObject[2];
        antroControl2.endPoints = new GameObject[2];

        // Load FemaleAmericanFeet prefab from asset bundle
        var femaleAmericanFeetPrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanFeet");
        if (femaleAmericanFeetPrefab != null)
        {
            antroControl2.nextPrefab = femaleAmericanFeetPrefab;
        }

        // Load FemaleAmericanHand prefab from asset bundle
        var femaleAmericanHandPrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanHand");
        if (femaleAmericanHandPrefab != null)
        {
            femaleAmericanHandPrefab.tag = "Bot";
            if (femaleAmericanHandPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAmericanHand prefab
                var objectController = femaleAmericanHandPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAmericanHandPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAmericanHand prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 2f;
                    objectController.maxX = 6f;
                    objectController.minY = 1.5f;
                    objectController.maxY = 2.5f;
                    objectController.nonRotatableObject = femaleAmericanHandPrefab;
                }

                // Assign the Object Controller component to antroControl2
                antroControl2.objectController = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 1 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl2.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl2.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl2.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl2.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl2.predefinedDistances[0].panel = antroData.transform.Find("AmericanFemale/28")?.gameObject;
            antroControl2.predefinedDistances[1].panel = antroData.transform.Find("AmericanFemale/29")?.gameObject;
        }
    }

    private void SetStartAndEndPointsForFemaleAmericanFeet(AntroControl antroControl, GameObject prefab)
    {
        // Customize start and end points for FemaleAmericanFeet prefab
        // Example:
        antroControl.startPoints = new GameObject[2];
        antroControl.endPoints = new GameObject[2];

        // Load FemaleAmericanFeet prefab from asset bundle
        var femaleAmericanFeetPrefab = assetBundle.LoadAsset<GameObject>("FemaleAmericanFeet");
        if (femaleAmericanFeetPrefab != null)
        {
            femaleAmericanFeetPrefab.tag = "Bot";
            if (femaleAmericanFeetPrefab.CompareTag("Bot"))
            {
                // Add or get Object Controller component from the FemaleAmericanFeet prefab
                var objectController = femaleAmericanFeetPrefab.GetComponent<ObjectControl>();
                if (objectController == null)
                {
                    // Add Object Control script to the prefab if it's not already attached
                    objectController = femaleAmericanFeetPrefab.AddComponent<ObjectControl>();
                    Debug.Log("Object Controller component added to FemaleAmericanFeet prefab.");

                    // Set Object Control settings
                    objectController.moveSpeed = 5f;
                    objectController.rotateSpeed = 3f;
                    objectController.zoomSpeed = 100f;
                    objectController.minX = 1f;
                    objectController.maxX = 4f;
                    objectController.minY = 1f;
                    objectController.maxY = 1.5f;
                    objectController.nonRotatableObject = femaleAmericanFeetPrefab;
                }

                // Assign the Object Controller component to antroControl
                antroControl.objectControl = objectController;
            }
        }

        // Set start and end points according to your requirements
        // Loop through the numbers 1 to 2 and find the corresponding start and end points
        for (int i = 1; i <= 2; i++)
        {
            // Find the start point game object using the hierarchy path
            Transform startPoint = prefab.transform.Find($"Point/Start/{i}");
            if (startPoint != null)
            {
                // Set the start point game object to the array
                antroControl.startPoints[i - 1] = startPoint.gameObject;
            }

            // Find the end point game object using the hierarchy path
            Transform endPoint = prefab.transform.Find($"Point/End/{i}");
            if (endPoint != null)
            {
                // Set the end point game object to the array
                antroControl.endPoints[i - 1] = endPoint.gameObject;
            }
        }

        GameObject antroControlGameObject = GameObject.Find("AntroControl");
        if (antroControlGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform nextPointTransform = antroControlGameObject.transform.Find("NextPoint");
            if (nextPointTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button nextPointButton = nextPointTransform.GetComponent<UnityEngine.UI.Button>();
                if (nextPointButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.nextPointButton = nextPointButton;
                    Debug.Log("Next Point Button field assigned successfully.");
                }
            }
        }

        GameObject antroDataGameObject = GameObject.Find("AntroData");
        if (antroDataGameObject != null)
        {
            // Find NextPoint child GameObject
            Transform deskripsiTransform = antroDataGameObject.transform.Find("Deskripsi");
            if (deskripsiTransform != null)
            {
                // Get the Button component from NextPoint GameObject
                UnityEngine.UI.Button deskripsiButton = deskripsiTransform.GetComponent<UnityEngine.UI.Button>();
                if (deskripsiButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.descriptionButton = deskripsiButton;
                }
            }

            Transform closeTransform = antroDataGameObject.transform.Find("Close");
            if (closeTransform != null)
            {

                UnityEngine.UI.Button closeButton = closeTransform.GetComponent<UnityEngine.UI.Button>();
                if (closeButton != null)
                {
                    // Assign NextPoint Button component to Next Point Button field
                    antroControl.closeDescriptionButton = closeButton;
                }
            }
        }

        // Set chooseButton with GameObject named 'Choose' under 'ConfirmationCanvas'
        var confirmationCanvasObject = GameObject.Find("ConfirmationCanvas");
        if (confirmationCanvasObject != null)
        {
            // Set DistancePanel with GameObject named 'DistancePanel' under 'ConfirmationCanvas'
            var distancePanelObject = confirmationCanvasObject.transform.Find("DistancePanel")?.gameObject;
            if (distancePanelObject != null)
            {
                antroControl.distancePanel = distancePanelObject;
            }
        }

        // Set the Panel fields in predefinedDistances based on specific object mappings
        GameObject antroData = GameObject.Find("AntroData");
        if (antroData != null)
        {
            antroControl.predefinedDistances[0].panel = antroData.transform.Find("AmericanFemale/30")?.gameObject;
            antroControl.predefinedDistances[1].panel = antroData.transform.Find("AmericanFemale/31")?.gameObject;
        }
    }

    private void AddResetComponent(GameObject prefab)
    {
        if (prefab != null)
        {
            // Add Reset component
            var resetComponent = prefab.AddComponent<Reset>();
            if (resetComponent == null)
            {
                Debug.LogWarning("Failed to add Reset component to prefab: " + prefab.name);
            }
            else
            {
                // Make the "Reset" game object visible
                resetComponent.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("Prefab is null. Cannot add Reset component.");
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
}