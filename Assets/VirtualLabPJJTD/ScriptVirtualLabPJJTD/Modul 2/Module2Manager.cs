using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

[System.Serializable]
public class BucketPartData
{
    public GameObject BucketPart; // The main bucket part
    public GameObject Part;       // The associated part
}

public class Module2Manager : MonoBehaviour
{
    // Array to hold BucketParts and associated Parts
    public BucketPartData[] BucketParts;

    // Prefab for Meja
    public GameObject MejaPrefab;
    private GameObject instantiatedMeja;

    // Button to start the game
    public Button startButton;

    // Button to start the module (enables dragging and spawning)
    public Button startModuleButton;

    // Button to show when all parts are placed
    public Button finishButton;

    // Text for progress, showing "X out of 10"
    public TextMeshProUGUI progressText; // New text for progress

    // Panel to show when a new truck is completed
    public GameObject progressPanel;
    private bool previousProgressPanelState = false;
    public GameObject exitButton;

    private Vector2 originalExitButtonPosition;
    private Vector2 movedExitButtonPosition = new Vector2(0, -40);

    // DropZone reference (automatically assigned)
    private GameObject DropZone;

    private GameObject truckDone; // Reference to the TruckDone GameObject

    // Boolean to track if the module has started (dragging and spawning enabled)
    public bool moduleStarted = false;

    // Panel to show when part is placed out of order
    public GameObject orderErrorPanel;

    // List of parts that need to be placed in order, including duplicates
    private List<string> partOrder = new List<string>
    {
        "BadanMobil1(Clone)", "BadanMobil2(Clone)", "BelakangMobil(Clone)", "MobilDepan(Clone)",
        "Stir(Clone)", "PinggirMobil(Clone)", "PinggirMobil(Clone)", "SisiMobil(Clone)", "SisiMobil(Clone)", "AtapMobil(Clone)",
        "Ban(Clone)", "Ban(Clone)", "Ban(Clone)", "Ban(Clone)", "Sekop(Clone)", "PartCrane(Clone)"
    };

    // Track how many of each part has been placed inside TruckDone
    private Dictionary<string, int> placedPartCounts = new Dictionary<string, int>();

    // Max counts for specific parts
    private Dictionary<string, int> maxPartCounts = new Dictionary<string, int>()
    {
        { "Ban(Clone)", 4 },
        { "SisiMobil(Clone)", 2 },
        { "PinggirMobil(Clone)", 2 }
    };

    // Dictionary to map original part names to display-friendly names
    private Dictionary<string, string> partDisplayNames = new Dictionary<string, string>()
    {
        { "BadanMobil1(Clone)", "Lego Body" },
        { "BadanMobil2(Clone)", "Engine" },
        { "BelakangMobil(Clone)", "Back Bumper" },
        { "MobilDepan(Clone)", "Brake Groups" },
        { "Stir(Clone)", "Steering Control" },
        { "PinggirMobil(Clone)", "Stairs" },
        { "SisiMobil(Clone)", "Side Bumper" },
        { "AtapMobil(Clone)", "Cab" },
        { "Ban(Clone)", "Wheel" },
        { "Sekop(Clone)", "Bucket" },
        { "PartCrane(Clone)", "Lift Cylinder" }
    };


    // Track the current index of the part order
    private int currentPartIndex = 0;

    // Timer variables
    private float assemblyTime = 0f;
    private bool isTiming = false;

    // List to store assembly times for each round
    private List<float> assemblyTimes = new List<float>(); // Store assembly times for each game

    // This method returns the list of assembly times (Waktu Kumulatif)
    public List<float> GetAssemblyTimes()
    {
        return assemblyTimes;
    }

    // Example of adding times (this would happen in your game logic)
    public void AddAssemblyTime(float time)
    {
        assemblyTimes.Add(time);
    }

    // Text to show the assembly time
    public TextMeshProUGUI timerText;

    // Text to show the cycle time
    private Transform originalTimerParent; // To store the original parent of timerText
    private Vector2 originalTimerPosition; // To store the original anchored position of timerText

    // Reference to Canvas -> Timer GameObject
    public GameObject waktuKumulatifPanel;

    // Add a reference to the NextPart panel and text
    public GameObject NextPartPanel;
    public TextMeshProUGUI NextPartText;

    // Track how many trucks are completed
    private int trucksCompleted = 0;
    private int totalTrucks = 10; // Assuming there are 10 trucks to complete

    public Button NextGameButton;

    private List<GameObject> instantiatedBucketParts = new List<GameObject>();

    public GameObject[] StartBuildConversation;
    private int currentStartBuildConversationIndex = 0;
    public Button nextStartBuildConversationButton;
    public Button closeStartBuildConversationButton;
    private bool startBuildconversationStarted = false;

    public GameObject[] EndBuildConversation;
    private int currentEndBuildConversationIndex = 0;
    public Button nextEndBuildConversationButton;
    public Button closeEndBuildConversationButton;
    private bool endBuildConversationStarted = false;

    public TextMeshProUGUI BadanMobil1Text;
    public TextMeshProUGUI BadanMobil2Text;
    public TextMeshProUGUI BelakangMobilText;
    public TextMeshProUGUI MobilDepanText;
    public TextMeshProUGUI StirText;
    public TextMeshProUGUI PinggirMobilText;
    public TextMeshProUGUI SisiMobilText;
    public TextMeshProUGUI AtapMobilText;
    public TextMeshProUGUI BanText;
    public TextMeshProUGUI SekopText;
    public TextMeshProUGUI PartCraneText;

    public int BadanMobil1Count = 10;
    public int BadanMobil2Count = 10;
    public int BelakangMobilCount = 10;
    public int MobilDepanCount = 10;
    public int StirCount = 10;
    public int PinggirMobilCount = 20;
    public int SisiMobilCount = 20;
    public int AtapMobilCount = 10;
    public int BanCount = 40;
    public int SekopCount = 10;
    public int PartCraneCount = 10;

    private void Start()
    {
        // Initially hide the finish button, startModule button, timerText, finishPanel, nextButton, and progressPanel
        finishButton.gameObject.SetActive(false);
        startModuleButton.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        progressPanel.SetActive(false); // Hide the progress panel initially
        waktuKumulatifPanel.SetActive(false);
        NextPartPanel.SetActive(false);

        nextStartBuildConversationButton.onClick.AddListener(OnNextStartBuildConversationClicked);
        closeStartBuildConversationButton.onClick.AddListener(OnCloseStartBuildConversationClicked);

        // Initially hide the NextGameButton
        NextGameButton.gameObject.SetActive(false);
        NextGameButton.onClick.AddListener(OnNextGameClicked);

        // Add listeners for the EndBuildConversation buttons
        nextEndBuildConversationButton.onClick.AddListener(OnNextEndBuildConversationClicked);
        closeEndBuildConversationButton.onClick.AddListener(OnCloseEndBuildConversationClicked);

        // Initially hide the EndBuildConversation buttons
        nextEndBuildConversationButton.gameObject.SetActive(false);
        closeEndBuildConversationButton.gameObject.SetActive(false);

        // Initialize progress text
        progressText.text = trucksCompleted + " out of " + totalTrucks;

        // Store the original parent and position of the timerText at the start
        originalTimerParent = timerText.transform.parent;
        originalTimerPosition = timerText.rectTransform.anchoredPosition;

        UpdateNextPartUI();

        // Ensure that startButton is assigned through the Inspector
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }

        // Add listener for the StartModule button (if it is assigned in the Inspector)
        if (startModuleButton != null)
        {
            startModuleButton.onClick.AddListener(OnStartModuleClicked);
        }
        else
        {
            Debug.LogError("StartModule button not assigned.");
        }

        // Add listener for the Finish button
        if (finishButton != null)
        {
            finishButton.onClick.AddListener(OnFinishReached);
        }

        // Find the Exit button in ConfirmationCanvas -> Exit
        exitButton = FindInactiveObjectByPath("ConfirmationCanvas/Exit");

        // Store the original position of the Exit button
        if (exitButton != null)
        {
            originalExitButtonPosition = exitButton.GetComponent<RectTransform>().anchoredPosition;
        }
        else
        {
            Debug.LogError("Exit button not found in ConfirmationCanvas -> Exit.");
        }

        previousProgressPanelState = progressPanel.activeSelf;

        BadanMobil1Text.gameObject.SetActive(false);
        BadanMobil2Text.gameObject.SetActive(false);
        BelakangMobilText.gameObject.SetActive(false);
        MobilDepanText.gameObject.SetActive(false);
        StirText.gameObject.SetActive(false);
        PinggirMobilText.gameObject.SetActive(false);
        SisiMobilText.gameObject.SetActive(false);
        AtapMobilText.gameObject.SetActive(false);
        BanText.gameObject.SetActive(false);
        SekopText.gameObject.SetActive(false);
        PartCraneText.gameObject.SetActive(false);
        UpdatePartMobilText("BadanMobil1", BadanMobil1Count);
        UpdatePartMobilText("BadanMobil2", BadanMobil2Count);
        UpdatePartMobilText("BelakangMobil", BelakangMobilCount);
        UpdatePartMobilText("MobilDepan", MobilDepanCount);
        UpdatePartMobilText("Stir", StirCount);
        UpdatePartMobilText("PinggirMobil", PinggirMobilCount);
        UpdatePartMobilText("SisiMobil", SisiMobilCount);
        UpdatePartMobilText("AtapMobil", AtapMobilCount);
        UpdatePartMobilText("Ban", BanCount);
        UpdatePartMobilText("Sekop", SekopCount);
        UpdatePartMobilText("PartCrane", PartCraneCount);
    }

    // Generalized method to update text for each part
    public void UpdatePartMobilText(string partName, int count)
    {
        if (partName == "BadanMobil1" && BadanMobil1Text != null)
        {
            BadanMobil1Text.text = "Lego Body\n" + count + "x";
        }
        else if (partName == "BadanMobil2" && BadanMobil2Text != null)
        {
            BadanMobil2Text.text = "Engine\n" + count + "x";
        }
        else if (partName == "BelakangMobil" && BelakangMobilText != null)
        {
            BelakangMobilText.text = "Back Bumper\n" + count + "x";
        }
        else if (partName == "MobilDepan" && MobilDepanText != null)
        {
            MobilDepanText.text = "Brake Groups\n" + count + "x";
        }
        else if (partName == "Stir" && StirText != null)
        {
            StirText.text = "Steering Control\n" + count + "x";
        }
        else if (partName == "PinggirMobil" && PinggirMobilText != null)
        {
            PinggirMobilText.text = "Stairs\n" + count + "x";
        }
        else if (partName == "SisiMobil" && SisiMobilText != null)
        {
            SisiMobilText.text = "Side Bumper\n" + count + "x";
        }
        else if (partName == "AtapMobil" && AtapMobilText != null)
        {
            AtapMobilText.text = "Cab\n" + count + "x";
        }
        else if (partName == "Ban" && BanText != null)
        {
            BanText.text = "Wheel\n" + count + "x";
        }
        else if (partName == "Sekop" && SekopText != null)
        {
            SekopText.text = "Bucket\n" + count + "x";
        }
        else if (partName == "PartCrane" && PartCraneText != null)
        {
            PartCraneText.text = "Lift Cylinder\n" + count + "x";
        }
    }

    // Generalized increment method for each part
    public void IncrementPartCount(string partName)
    {
        if (partName == "BadanMobil1")
        {
            BadanMobil1Count++;
            UpdatePartMobilText("BadanMobil1", BadanMobil1Count);
        }
        else if (partName == "BadanMobil2")
        {
            BadanMobil2Count++;
            UpdatePartMobilText("BadanMobil2", BadanMobil2Count);
        }
        else if (partName == "BelakangMobil")
        {
            BelakangMobilCount++;
            UpdatePartMobilText("BelakangMobil", BelakangMobilCount);
        }
        else if (partName == "MobilDepan")
        {
            MobilDepanCount++;
            UpdatePartMobilText("MobilDepan", MobilDepanCount);
        }
        else if (partName == "Stir")
        {
            StirCount++;
            UpdatePartMobilText("Stir", StirCount);
        }
        else if (partName == "PinggirMobil")
        {
            PinggirMobilCount++;
            UpdatePartMobilText("PinggirMobil", PinggirMobilCount);
        }
        else if (partName == "SisiMobil")
        {
            SisiMobilCount++;
            UpdatePartMobilText("SisiMobil", SisiMobilCount);
        }
        else if (partName == "AtapMobil")
        {
            AtapMobilCount++;
            UpdatePartMobilText("AtapMobil", AtapMobilCount);
        }
        else if (partName == "Ban")
        {
            BanCount++;
            UpdatePartMobilText("Ban", BanCount);
        }
        else if (partName == "Sekop")
        {
            SekopCount++;
            UpdatePartMobilText("Sekop", SekopCount);
        }
        else if (partName == "PartCrane")
        {
            PartCraneCount++;
            UpdatePartMobilText("PartCrane", PartCraneCount);
        }
    }

    public void DecrementPartCount(string partName)
    {
        if (partName == "BadanMobil1")
        {
            BadanMobil1Count = Mathf.Max(BadanMobil1Count - 1, 0);
            UpdatePartMobilText("BadanMobil1", BadanMobil1Count);
        }
        else if (partName == "BadanMobil2")
        {
            BadanMobil2Count = Mathf.Max(BadanMobil2Count - 1, 0);
            UpdatePartMobilText("BadanMobil2", BadanMobil2Count);
        }
        else if (partName == "BelakangMobil")
        {
            BelakangMobilCount = Mathf.Max(BelakangMobilCount - 1, 0);
            UpdatePartMobilText("BelakangMobil", BelakangMobilCount);
        }
        else if (partName == "MobilDepan")
        {
            MobilDepanCount = Mathf.Max(MobilDepanCount - 1, 0);
            UpdatePartMobilText("MobilDepan", MobilDepanCount);
        }
        else if (partName == "Stir")
        {
            StirCount = Mathf.Max(StirCount - 1, 0);
            UpdatePartMobilText("Stir", StirCount);
        }
        else if (partName == "PinggirMobil")
        {
            PinggirMobilCount = Mathf.Max(PinggirMobilCount - 1, 0);
            UpdatePartMobilText("PinggirMobil", PinggirMobilCount);
        }
        else if (partName == "SisiMobil")
        {
            SisiMobilCount = Mathf.Max(SisiMobilCount - 1, 0);
            UpdatePartMobilText("SisiMobil", SisiMobilCount);
        }
        else if (partName == "AtapMobil")
        {
            AtapMobilCount = Mathf.Max(AtapMobilCount - 1, 0);
            UpdatePartMobilText("AtapMobil", AtapMobilCount);
        }
        else if (partName == "Ban")
        {
            BanCount = Mathf.Max(BanCount - 1, 0);
            UpdatePartMobilText("Ban", BanCount);
        }
        else if (partName == "Sekop")
        {
            SekopCount = Mathf.Max(SekopCount - 1, 0);
            UpdatePartMobilText("Sekop", SekopCount);
        }
        else if (partName == "PartCrane")
        {
            PartCraneCount = Mathf.Max(PartCraneCount - 1, 0);
            UpdatePartMobilText("PartCrane", PartCraneCount);
        }
    }

    private void ShowPartMobilText()
    {
        if (BadanMobil1Text != null)
        {
            BadanMobil1Text.text = "Lego Body\n10x";
            BadanMobil1Text.gameObject.SetActive(true);
        }
        if (BadanMobil2Text != null)
        {
            BadanMobil2Text.text = "Engine\n10x";
            BadanMobil2Text.gameObject.SetActive(true);
        }
        if (BelakangMobilText != null)
        {
            BelakangMobilText.text = "Back Bumper\n10x";
            BelakangMobilText.gameObject.SetActive(true);
        }
        if (MobilDepanText != null)
        {
            MobilDepanText.text = "Brake Group\n10x";
            MobilDepanText.gameObject.SetActive(true);
        }
        if (StirText != null)
        {
            StirText.text = "Steering Wheel\n10x";
            StirText.gameObject.SetActive(true);
        }
        if (PinggirMobilText != null)
        {
            PinggirMobilText.text = "Stair\n20x";
            PinggirMobilText.gameObject.SetActive(true);
        }
        if (SisiMobilText != null)
        {
            SisiMobilText.text = "Side Bumper\n20x";
            SisiMobilText.gameObject.SetActive(true);
        }
        if (AtapMobilText != null)
        {
            AtapMobilText.text = "Cab\n10x";
            AtapMobilText.gameObject.SetActive(true);
        }
        if (BanText != null)
        {
            BanText.text = "Wheel\n40x";
            BanText.gameObject.SetActive(true);
        }
        if (SekopText != null)
        {
            SekopText.text = "Bucket\n10x";
            SekopText.gameObject.SetActive(true);
        }
        if (PartCraneText != null)
        {
            PartCraneText.text = "Lift Cylinder\n10x";
            PartCraneText.gameObject.SetActive(true);
        }
    }
    private void DisableBucketsForPlacedParts()
    {
        if (trucksCompleted == totalTrucks - 1) // Check if it's 9 out of 10
        {
            Dictionary<string, string> partBucketPairs = new Dictionary<string, string>()
        {
            { "BadanMobil1(Clone)", "BucketBadanMobil1(Clone)" },
            { "BadanMobil2(Clone)", "BucketBadanMobil2(Clone)" },
            { "BelakangMobil(Clone)", "BucketBelakangMobil(Clone)" },
            { "MobilDepan(Clone)", "BucketMobilDepan(Clone)" },
            { "Stir(Clone)", "BucketStir(Clone)" },
            { "AtapMobil(Clone)", "BucketAtapMobil(Clone)" },
            { "Sekop(Clone)", "BucketBucket(Clone)" },
            { "PartCrane(Clone)", "BucketPartCrane(Clone)" },
        };

            foreach (var part in partBucketPairs)
            {
                string partName = part.Key;
                string bucketName = part.Value;

                if (truckDone.transform.Find(partName) != null)
                {
                    GameObject bucketObject = GameObject.Find(bucketName);
                    if (bucketObject != null)
                    {
                        Collider bucketCollider = bucketObject.GetComponent<Collider>();
                        if (bucketCollider != null)
                        {
                            bucketCollider.enabled = false; // Disable the collider

                            // Destroy the specific child object inside the bucket when the collider is disabled
                            DestroySpecificChildObject(bucketObject, partName);
                        }
                    }
                }
            }

            // Disable colliders and destroy child objects for parts with quantity limits
            CheckAndDisableBucketWithLimit("PinggirMobil(Clone)", "BucketPinggirMobil(Clone)", 2);
            CheckAndDisableBucketWithLimit("SisiMobil(Clone)", "BucketSisiMobil(Clone)", 2);
            CheckAndDisableBucketWithLimit("Ban(Clone)", "BucketBan(Clone)", 4);
        }
    }

    // Helper method to find and destroy a specific child object inside the bucket when the collider is disabled
    private void DestroySpecificChildObject(GameObject bucketObject, string partName)
    {
        string childObjectName;

        // Define the specific child object name based on the bucket
        if (bucketObject.name == "BucketBadanMobil2(Clone)")
        {
            childObjectName = "BadanMobil"; // For BucketBadanMobil2, destroy BadanMobil inside
        }
        else
        {
            childObjectName = partName.Replace("(Clone)", ""); // General case for other parts
        }

        // Search for and destroy the specific child object
        Transform childTransform = bucketObject.transform.Find(childObjectName);
        if (childTransform != null)
        {
            Destroy(childTransform.gameObject);
        }
    }

    // Modified CheckAndDisableBucketWithLimit to include DestroySpecificChildObject call
    private void CheckAndDisableBucketWithLimit(string partName, string bucketName, int limit)
    {
        int partCount = truckDone.transform.Cast<Transform>().Count(child => child.name == partName);

        if (partCount >= limit)
        {
            GameObject bucketObject = GameObject.Find(bucketName);
            if (bucketObject != null)
            {
                Collider bucketCollider = bucketObject.GetComponent<Collider>();
                if (bucketCollider != null)
                {
                    bucketCollider.enabled = false; // Disable the collider

                    // Destroy the specific child object inside the bucket when the collider is disabled
                    DestroySpecificChildObject(bucketObject, partName);
                }
            }
        }
    }


    void Update()
    {
        if (startModuleButton.gameObject.activeSelf && !startBuildconversationStarted)
        {
            startBuildconversationStarted = true;
            ShowStartBuildConversationPanel(0); // Show the first panel of the ConversationTutorial
        }
        if (finishButton.gameObject.activeSelf && !endBuildConversationStarted)
        {
            endBuildConversationStarted = true;
            ShowEndBuildConversationPanel(0); // Show the first panel of the EndBuildConversation
        }

        // Update the timer if it's running
        if (isTiming)
        {
            assemblyTime += Time.deltaTime;

            // Round the assembly time down to the nearest integer
            float roundedTime = Mathf.Floor(assemblyTime);  // Use Mathf.Floor to always round down

            // Update the timer text with the rounded-down time
            timerText.text = FormatTime(roundedTime);  // Display rounded time

            // Log the same rounded-down time in the debug log
            Debug.Log("Stopwatch: " + FormatTime(roundedTime));  // Use the same rounded time for the debug log
        }

        if (progressPanel.activeSelf != previousProgressPanelState)
        {
            previousProgressPanelState = progressPanel.activeSelf;
            OnProgressPanelStateChanged(progressPanel.activeSelf);
        }
        DisableBucketsForPlacedParts();
    }
    private void ShowStartBuildConversationPanel(int index)
    {
        // Disable the startTutorModuleButton while tutorial panels are showing
        startModuleButton.interactable = false;

        // Hide all panels in the ConversationTutorial array
        foreach (GameObject panel in StartBuildConversation)
        {
            panel.SetActive(false);
        }

        // Show the current panel
        if (index < StartBuildConversation.Length)
        {
            StartBuildConversation[index].SetActive(true);
            nextStartBuildConversationButton.gameObject.SetActive(true);
            closeStartBuildConversationButton.gameObject.SetActive(false);

            // If it's the last panel, show the Close button instead of Next
            if (index == StartBuildConversation.Length - 1)
            {
                nextStartBuildConversationButton.gameObject.SetActive(false);
                closeStartBuildConversationButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnNextStartBuildConversationClicked()
    {
        currentStartBuildConversationIndex++;
        ShowStartBuildConversationPanel(currentStartBuildConversationIndex);
    }

    private void OnCloseStartBuildConversationClicked()
    {
        // Close the current panel and hide the close button
        StartBuildConversation[currentStartBuildConversationIndex].SetActive(false);
        closeStartBuildConversationButton.gameObject.SetActive(false);

        // Enable the startTutorModuleButton now that the panels are closed
        startModuleButton.interactable = true;
    }

    private void ShowEndBuildConversationPanel(int index)
    {
        // Disable the finishButton while tutorial panels are showing
        finishButton.interactable = false;

        // Hide all panels in the EndBuildConversation array
        foreach (GameObject panel in EndBuildConversation)
        {
            panel.SetActive(false);
        }

        // Show the current panel
        if (index < EndBuildConversation.Length)
        {
            EndBuildConversation[index].SetActive(true);
            nextEndBuildConversationButton.gameObject.SetActive(true);
            closeEndBuildConversationButton.gameObject.SetActive(false);

            // If it's the last panel, show the Close button instead of Next
            if (index == EndBuildConversation.Length - 1)
            {
                nextEndBuildConversationButton.gameObject.SetActive(false);
                closeEndBuildConversationButton.gameObject.SetActive(true);
            }
        }
    }

    // Method to handle the next button click in EndBuildConversation
    private void OnNextEndBuildConversationClicked()
    {
        currentEndBuildConversationIndex++;
        ShowEndBuildConversationPanel(currentEndBuildConversationIndex);
    }

    // Method to handle the close button click in EndBuildConversation
    private void OnCloseEndBuildConversationClicked()
    {
        // Close the current panel and hide the close button
        EndBuildConversation[currentEndBuildConversationIndex].SetActive(false);
        closeEndBuildConversationButton.gameObject.SetActive(false);

        // Enable the finishButton now that the panels are closed
        finishButton.interactable = true;
    }

    private void OnProgressPanelStateChanged(bool isProgressPanelActive)
    {
        if (exitButton != null)
        {
            RectTransform exitButtonRectTransform = exitButton.GetComponent<RectTransform>();

            if (isProgressPanelActive)
            {
                // Move the Exit button to the new anchored position when the ProgressPanel is active
                exitButtonRectTransform.anchoredPosition = movedExitButtonPosition;
                Debug.Log("Exit button moved to new anchored position (Y: -40) because ProgressPanel is active.");
            }
            else
            {
                // Move the Exit button back to its original anchored position when the ProgressPanel is inactive
                exitButtonRectTransform.anchoredPosition = originalExitButtonPosition;
                Debug.Log("Exit button returned to its original anchored position because ProgressPanel is inactive.");
            }
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

    // Method to format time as MM:SS
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);  // Get the minutes
        int seconds = Mathf.FloorToInt(time % 60);  // Get the seconds
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void StartGame()
    {
        // Hide the start button after it's pressed
        startButton.gameObject.SetActive(false);

        // Show the StartModule button
        startModuleButton.gameObject.SetActive(true);

        // Instantiate Meja with its original position, rotation, and scale
        instantiatedMeja = Instantiate(MejaPrefab, MejaPrefab.transform.position, MejaPrefab.transform.rotation);
        instantiatedMeja.transform.localScale = MejaPrefab.transform.localScale;

        // Automatically find the DropZone object (Cube) under Meja
        DropZone = instantiatedMeja.transform.Find("Cube").gameObject;
        if (DropZone == null)
        {
            Debug.LogError("DropZone (Meja->Cube) not found.");
            return;
        }

        // Instantiate TruckDone at the specified location with its default rotation
        Vector3 truckDonePosition = new Vector3(2.3f, 1.7f, 5.61f);
        Quaternion truckDoneRotation = Quaternion.identity; // Default rotation (no rotation)
        truckDone = new GameObject("TruckDone");
        truckDone.transform.position = truckDonePosition;
        truckDone.transform.rotation = truckDoneRotation; // Ensure TruckDone starts with no rotation

        // Immediately add the RotateTruck script when TruckDone is created
        if (truckDone.GetComponent<RotateTruck>() == null)
        {
            truckDone.AddComponent<RotateTruck>();
            Debug.Log("RotateTruck script added to TruckDone");
        }

        // Iterate through the BucketParts array and instantiate each BucketPart
        foreach (BucketPartData bucketPartData in BucketParts)
        {
            // Instantiate the BucketPart
            GameObject bucketPartInstance = Instantiate(bucketPartData.BucketPart, bucketPartData.BucketPart.transform.position, bucketPartData.BucketPart.transform.rotation);
            bucketPartInstance.transform.localScale = bucketPartData.BucketPart.transform.localScale;

            // Track the instantiated BucketPart
            instantiatedBucketParts.Add(bucketPartInstance); // Add to the list

            // Add the click handler to the BucketPart and set the corresponding Part
            BucketPartClickHandler clickHandler = bucketPartInstance.AddComponent<BucketPartClickHandler>();
            clickHandler.correspondingPart = bucketPartData.Part;
            clickHandler.DropZone = DropZone;    // Automatically assign the DropZone
            clickHandler.TruckDone = truckDone;  // Automatically assign the TruckDone
            clickHandler.TutorialModule2 = this;   // Pass reference to Module2Manager

            ShowPartMobilText();
        }

        Debug.Log("Game started, Meja, BucketParts instantiated, TruckDone created.");

        // Move Camera to the desired position and rotation
        Camera.main.transform.position = new Vector3(2.32f, 3.05f, 4.14f); // Set camera position
        Camera.main.transform.rotation = Quaternion.Euler(48f, 0f, 0f);
    }

    // Method to enable the drag and spawn actions when the StartModule button is clicked
    public void OnStartModuleClicked()
    {
        Debug.Log("Start Module button clicked. Dragging and spawning enabled.");

        // Enable dragging and spawning
        moduleStarted = true;

        // Show the Assembly Timer panel and the Next Part panel
        waktuKumulatifPanel.SetActive(true); // Show the Assembly Timer panel
        NextPartPanel.SetActive(true);      // Show the Next Part panel
        timerText.gameObject.SetActive(true);

        assemblyTime = 0f; // Reset the timer
        isTiming = true;   // Start the timer

        // Hide the StartModule button after it's clicked
        startModuleButton.gameObject.SetActive(false);

        // Show the progress panel
        progressPanel.SetActive(true);
    }

    // Method to check if the part is placed in the correct order
    public bool IsCorrectPartOrder(string partName)
    {
        return partName == partOrder[currentPartIndex];
    }

    // Method to update the NextPart panel with the current part that needs to be placed
    public void UpdateNextPartUI()
    {
        // Check if the currentPartIndex is still within the bounds of the partOrder list
        if (currentPartIndex < partOrder.Count)
        {
            string nextPartName = partOrder[currentPartIndex]; // Get the next part

            // Check if the part name exists in the dictionary and replace it with the display-friendly name
            if (partDisplayNames.ContainsKey(nextPartName))
            {
                nextPartName = partDisplayNames[nextPartName]; // Use display-friendly name
            }

            // Check if the part has a maximum count defined (for multiple parts)
            if (maxPartCounts.ContainsKey(partOrder[currentPartIndex]))
            {
                // Calculate how many more of this part are needed
                int remainingCount = maxPartCounts[partOrder[currentPartIndex]] - GetPlacedCount(partOrder[currentPartIndex]);

                // Update the text on the Next Part Panel to show the remaining count (e.g., "Ban 4x", "Ban 3x", etc.)
                NextPartText.text = "Next Part: " + nextPartName + " " + remainingCount + "x";
            }
            else
            {
                // For single parts, just show the part name
                NextPartText.text = "Next Part: " + nextPartName;
            }
        }
        else
        {
            // If all parts are placed, hide or update the panel
            NextPartText.text = "All parts placed!";
        }
    }

    // Method to move to the next part in the order and update placed counts
    public void IncrementPartOrder(string partName)
    {
        if (!placedPartCounts.ContainsKey(partName))
        {
            placedPartCounts[partName] = 0;
        }

        placedPartCounts[partName]++;
        currentPartIndex++;

        // Update the NextPart panel
        UpdateNextPartUI();

        // Check if all parts are placed inside TruckDone
        if (truckDone.transform.childCount == 16)
        {
            Debug.Log("All parts have been placed inside TruckDone!");

            // Stop the timer immediately when TruckDone reaches 16 parts
            isTiming = false;

            // Update the progress panel immediately when TruckDone reaches 16 parts
            progressText.text = (trucksCompleted + 1) + " out of " + totalTrucks;

            // Delay for 1 second before proceeding to destroy TruckDone
            StartCoroutine(DestroyTruckAfterDelay());
        }
    }

    // Coroutine to handle 1-second delay before destroying TruckDone
    private IEnumerator DestroyTruckAfterDelay()
    {
        // Wait for 1 second before destroying TruckDone
        yield return new WaitForSeconds(2f);

        // Check if this is the last truck
        if (trucksCompleted + 1 >= totalTrucks)
        {
            // Stop the timer for the last truck
            isTiming = false;

            // Show the Finish button instead of resetting the game
            finishButton.gameObject.SetActive(true);
        }
        else
        {
            // Automatically finish the round, save time, destroy TruckDone, and reset the game
            OnFinishReached();
        }
    }

    public void OnLastTruckFinishClicked()
    {
        // Destroy the TruckDone object
        if (truckDone != null)
        {
            Destroy(truckDone);
            Debug.Log("TruckDone destroyed.");
        }

        // Destroy all instantiated BucketParts
        foreach (GameObject bucketPart in instantiatedBucketParts)
        {
            if (bucketPart != null)
            {
                Destroy(bucketPart);
            }
        }
        instantiatedBucketParts.Clear(); // Clear the list after destroying all BucketParts

        // Hide the Finish button
        finishButton.gameObject.SetActive(false);

        // Show the NextGameButton for transitioning to the next stage or starting a new game
        NextGameButton.gameObject.SetActive(true);

        Debug.Log("Bucket parts destroyed, and NextGameButton displayed.");
    }


    // Show the error panel for 0.5 seconds
    public IEnumerator ShowOrderErrorPanel()
    {
        orderErrorPanel.SetActive(true); // Show the panel
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
        orderErrorPanel.SetActive(false); // Hide the panel
    }

    // Method to get how many times a part has been placed
    public int GetPlacedCount(string partName)
    {
        if (!placedPartCounts.ContainsKey(partName))
        {
            placedPartCounts[partName] = 0;
        }

        return placedPartCounts[partName];
    }

    // Method to check if all parts are placed inside TruckDone
    private bool CheckIfAllPartsPlaced()
    {
        // Create a dictionary to track how many of each part are inside TruckDone
        Dictionary<string, int> partsInTruck = new Dictionary<string, int>();

        // Iterate through each child of TruckDone
        foreach (Transform child in truckDone.transform)
        {
            string partName = child.name;

            // Count the part occurrences
            if (partsInTruck.ContainsKey(partName))
            {
                partsInTruck[partName]++;
            }
            else
            {
                partsInTruck[partName] = 1;
            }
        }

        // Now compare the counts in partsInTruck to the expected counts in partOrder
        foreach (string part in partOrder)
        {
            if (partsInTruck.ContainsKey(part))
            {
                partsInTruck[part]--; // Decrement the count for each occurrence
                if (partsInTruck[part] < 0)
                {
                    return false; // If we have fewer parts than expected, return false
                }
            }
            else
            {
                return false; // If a part is missing, return false
            }
        }

        // If we checked all parts and found them in the correct quantities, return true
        return true;
    }

    private void HideAllPartTexts()
    {
        BadanMobil1Text.gameObject.SetActive(false);
        BadanMobil2Text.gameObject.SetActive(false);
        BelakangMobilText.gameObject.SetActive(false);
        MobilDepanText.gameObject.SetActive(false);
        StirText.gameObject.SetActive(false);
        PinggirMobilText.gameObject.SetActive(false);
        SisiMobilText.gameObject.SetActive(false);
        AtapMobilText.gameObject.SetActive(false);
        BanText.gameObject.SetActive(false);
        SekopText.gameObject.SetActive(false);
        PartCraneText.gameObject.SetActive(false);
    }

    // Method to stop the timer, move the timer text, and update the displayed time
    public void OnFinishReached()
    {
        isTiming = false; // Stop the timer

        NextPartPanel.SetActive(false);

        // Round the Waktu Proses (stopwatch time)
        float waktuProses = Mathf.Round(assemblyTime);

        // If this is the first round, Waktu Kumulatif is the same as Waktu Proses
        float waktuKumulatif;
        if (assemblyTimes.Count == 0)
        {
            waktuKumulatif = waktuProses;
        }
        else
        {
            // For subsequent rounds, Waktu Kumulatif is the sum of the current Waktu Proses and the previous Waktu Kumulatif
            waktuKumulatif = assemblyTimes[assemblyTimes.Count - 1] + waktuProses;
        }

        // Store the current Waktu Kumulatif
        assemblyTimes.Add(waktuKumulatif);

        Debug.Log("Waktu Proses: " + waktuProses + " detik");
        Debug.Log("Waktu Kumulatif: " + waktuKumulatif + " detik");

        // Save both Waktu Kumulatif and Waktu Proses
        SaveTimes(waktuKumulatif, waktuProses);

        // Check if this is the final round
        if (trucksCompleted + 1 >= totalTrucks)
        {
            // Log all Waktu Kumulatif and Waktu Proses from the 1st to the last round
            Debug.Log("------ Summary of All Waktu Kumulatif and Waktu Proses ------");

            for (int i = 0; i < assemblyTimes.Count; i++)
            {
                float currentWaktuKumulatif = assemblyTimes[i];
                float currentWaktuProses = (i == 0) ? currentWaktuKumulatif : assemblyTimes[i] - assemblyTimes[i - 1];

                Debug.Log("Round " + (i + 1) + ": Waktu Kumulatif = " + currentWaktuKumulatif + " detik, Waktu Proses = " + Mathf.Abs(currentWaktuProses) + " detik");
            }

            Debug.Log("------------------------------------------------------------");
        }

        // Destroy the TruckDone object
        if (truckDone != null)
        {
            Destroy(truckDone); // Destroy the TruckDone object
            Debug.Log("TruckDone destroyed.");
        }

        // Destroy all instantiated BucketPart objects
        foreach (GameObject bucketPart in instantiatedBucketParts)
        {
            if (bucketPart != null)
            {
                Destroy(bucketPart); // Destroy the instantiated BucketPart
            }
        }

        // Clear the list after destroying all BucketParts
        instantiatedBucketParts.Clear();

        // Increment the trucks completed count
        trucksCompleted++;

        // Check if all trucks have been completed (last round)
        if (trucksCompleted >= totalTrucks)
        {
            HideAllPartTexts();
            Debug.Log("All trucks completed! Game over.");
            progressText.text = "All trucks completed!";

            // Hide the progress panel, finish button, and waktukumulatif panel
            progressPanel.SetActive(false);
            finishButton.gameObject.SetActive(false); // Hide the finish button
            waktuKumulatifPanel.SetActive(false); // Hide the Waktu Kumulatif panel

            // Show the NextGame button
            NextGameButton.gameObject.SetActive(true); // Make the NextGame button visible

            return; // Exit the method
        }

        // Update the progress text after the TruckDone is destroyed
        progressText.text = trucksCompleted + " out of " + totalTrucks;

        // Reset the game state for the next round
        ResetGame();

        // Logic from StartModule Button
        Debug.Log("Start Module button clicked. Dragging and spawning enabled.");

        // Enable dragging and spawning
        moduleStarted = true;

        // Show the Assembly Timer panel and the Next Part panel
        waktuKumulatifPanel.SetActive(true); // Show the Assembly Timer panel
        NextPartPanel.SetActive(true);      // Show the Next Part panel
        timerText.gameObject.SetActive(true);

        assemblyTime = 0f; // Reset the timer
        isTiming = true;   // Start the timer

        progressPanel.SetActive(true);
    }


    // Method to save the assembly and cycle times using PlayerPrefs
    private void SaveTimes(float waktuKumulatif, float waktuProses)
    {
        // Round the Waktu Kumulatif and Waktu Proses
        float roundedWaktuKumulatif = Mathf.Round(waktuKumulatif);
        float roundedWaktuProses = Mathf.Round(waktuProses);

        // Save the rounded Waktu Kumulatif using PlayerPrefs
        PlayerPrefs.SetFloat("WaktuKumulatif", roundedWaktuKumulatif);

        // Save the rounded Waktu Proses using PlayerPrefs
        PlayerPrefs.SetFloat("WaktuProses", roundedWaktuProses);

        // Ensure the data is saved
        PlayerPrefs.Save();

        // Debug log the saved times
        Debug.Log("Times saved: WaktuKumulatif = " + roundedWaktuKumulatif + " detik, WaktuProses = " + roundedWaktuProses + " detik.");
    }

    // Method to handle the NextGame button click
    public void OnNextGameClicked()
    {
        Debug.Log("NextGame button clicked, destroying instantiated Meja prefab.");

        // Destroy the instantiated Meja in the scene
        if (instantiatedMeja != null)
        {
            Destroy(instantiatedMeja);
            Debug.Log("Meja instance in the scene destroyed.");
        }

        // Hide the NextGameButton after destroying the Meja
        NextGameButton.gameObject.SetActive(false);
    }

    // Method to reset the game state for a new round
    private void ResetGame()
    {
        Debug.Log("Resetting the game...");

        // Reset part order index
        currentPartIndex = 0; // Reset the part order index to start from the beginning

        // Reset placed part counts
        placedPartCounts.Clear();

        // Reset the timer
        assemblyTime = 0f;
        isTiming = false;

        // Reset timerText to its original parent and position
        timerText.transform.SetParent(originalTimerParent);
        timerText.rectTransform.anchoredPosition = originalTimerPosition;
        timerText.gameObject.SetActive(false); // Hide the timer text

        // Hide finish and next buttons
        finishButton.gameObject.SetActive(false);

        // Reinstantiate a new TruckDone at the original position
        Vector3 truckDonePosition = new Vector3(2.3f, 1.7f, 5.61f);
        truckDone = new GameObject("TruckDone");
        truckDone.transform.position = truckDonePosition;

        // Add the RotateTruck script when TruckDone is created
        truckDone.AddComponent<RotateTruck>();
        Debug.Log("RotateTruck script added to TruckDone");

        // Reinstantiate all BucketParts so they can be clicked to spawn parts again
        foreach (BucketPartData bucketPartData in BucketParts)
        {
            // Instantiate the BucketPart
            GameObject bucketPartInstance = Instantiate(bucketPartData.BucketPart, bucketPartData.BucketPart.transform.position, bucketPartData.BucketPart.transform.rotation);
            bucketPartInstance.transform.localScale = bucketPartData.BucketPart.transform.localScale;

            // Track the instantiated BucketPart
            instantiatedBucketParts.Add(bucketPartInstance); // Add to the list

            // Add the click handler to the BucketPart and set the corresponding Part
            BucketPartClickHandler clickHandler = bucketPartInstance.AddComponent<BucketPartClickHandler>();
            clickHandler.correspondingPart = bucketPartData.Part;
            clickHandler.DropZone = DropZone;    // Automatically assign the DropZone
            clickHandler.TruckDone = truckDone;  // Automatically assign the new TruckDone
            clickHandler.TutorialModule2 = this;   // Pass reference to Module2Manager
        }

        // Update the Next Part panel with the first part
        UpdateNextPartUI(); // Ensure the panel shows the first part in the new game

        Debug.Log("Game reset complete. Ready for the next round.");
    }

    // Method to check if more of the given part can be placed based on max counts
    public bool CanPlaceMoreParts(string partName)
    {
        if (maxPartCounts.ContainsKey(partName))
        {
            return GetPlacedCount(partName) < maxPartCounts[partName];
        }
        return true; // If no max count is defined, allow placing more parts
    }
}

public class BucketPartClickHandler : MonoBehaviour
{
    public GameObject correspondingPart; // The corresponding part to be spawned
    public GameObject DropZone;          // The DropZone where the Part won't be destroyed
    public GameObject TruckDone;         // Reference to TruckDone for parenting the part
    public Module2Manager TutorialModule2; // Reference to Module2Manager
    private GameObject spawnedPart;      // To keep a reference to the spawned part

    private void OnMouseDown()
    {
        // Prevent spawning if the module has not started
        if (!TutorialModule2.moduleStarted)
        {
            Debug.Log("Cannot spawn parts. Module not started yet.");
            return;
        }

        // Check if TruckDone already has 16 parts
        if (TruckDone.transform.childCount >= 16)
        {
            Debug.Log("Cannot spawn more parts. TruckDone already has 16 parts.");
            return; // Prevent further part spawning
        }

        // Check if the corresponding part can still be spawned based on max allowed counts
        if (CanSpawnPart())
        {
            Vector3 spawnPosition = transform.position;
            spawnedPart = Instantiate(correspondingPart, spawnPosition, correspondingPart.transform.rotation);
            spawnedPart.transform.localScale = correspondingPart.transform.localScale / 3f;
            DragPart dragPartScript = spawnedPart.AddComponent<DragPart>();
            dragPartScript.TruckDone = TruckDone; // Assign the TruckDone reference

            // Check part name and decrement its count
            if (spawnedPart.name == "BadanMobil1(Clone)")
            {
                TutorialModule2.DecrementPartCount("BadanMobil1");
            }
            else if (spawnedPart.name == "BadanMobil2(Clone)")
            {
                TutorialModule2.DecrementPartCount("BadanMobil2");
            }
            else if (spawnedPart.name == "BelakangMobil(Clone)")
            {
                TutorialModule2.DecrementPartCount("BelakangMobil");
            }
            else if (spawnedPart.name == "MobilDepan(Clone)")
            {
                TutorialModule2.DecrementPartCount("MobilDepan");
            }
            else if (spawnedPart.name == "Stir(Clone)")
            {
                TutorialModule2.DecrementPartCount("Stir");
            }
            else if (spawnedPart.name == "PinggirMobil(Clone)")
            {
                TutorialModule2.DecrementPartCount("PinggirMobil");
            }
            else if (spawnedPart.name == "SisiMobil(Clone)")
            {
                TutorialModule2.DecrementPartCount("SisiMobil");
            }
            else if (spawnedPart.name == "AtapMobil(Clone)")
            {
                TutorialModule2.DecrementPartCount("AtapMobil");
            }
            else if (spawnedPart.name == "Ban(Clone)")
            {
                TutorialModule2.DecrementPartCount("Ban");
            }
            else if (spawnedPart.name == "Sekop(Clone)")
            {
                TutorialModule2.DecrementPartCount("Sekop");
            }
            else if (spawnedPart.name == "PartCrane(Clone)")
            {
                TutorialModule2.DecrementPartCount("PartCrane");
            }

            // Start checking whether the left mouse button is held
            StartCoroutine(CheckForMouseHold());
        }
        else
        {
            Debug.Log("Max count reached for " + correspondingPart.name);
        }
    }

    private System.Collections.IEnumerator CheckForMouseHold()
    {
        // Continue checking until the part is destroyed or moved to the drop zone
        while (spawnedPart != null)
        {
            // If the mouse button is not held down, check if part is in DropZone
            if (!Input.GetMouseButton(0)) // 0 is for left mouse button
            {
                // If part is not in DropZone, destroy it
                if (!IsInDropZone())
                {
                    // Increase count if part is destroyed without being placed
                    if (spawnedPart.name == "BadanMobil1(Clone)")
                    {
                        TutorialModule2.IncrementPartCount("BadanMobil1");
                    }
                    else if (spawnedPart.name == "BadanMobil2(Clone)")
                    {
                        TutorialModule2.IncrementPartCount("BadanMobil2");
                    }
                    else if (spawnedPart.name == "BelakangMobil(Clone)")
                    {
                        TutorialModule2.IncrementPartCount("BelakangMobil");
                    }
                    else if (spawnedPart.name == "MobilDepan(Clone)")
                    {
                        TutorialModule2.IncrementPartCount("MobilDepan");
                    }
                    else if (spawnedPart.name == "Stir(Clone)")
                    {
                        TutorialModule2.IncrementPartCount("Stir");
                    }
                    else if (spawnedPart.name == "PinggirMobil(Clone)")
                    {
                        TutorialModule2.IncrementPartCount("PinggirMobil");
                    }
                    else if (spawnedPart.name == "SisiMobil(Clone)")
                    {
                        TutorialModule2.IncrementPartCount("SisiMobil");
                    }
                    else if (spawnedPart.name == "AtapMobil(Clone)")
                    {
                        TutorialModule2.IncrementPartCount("AtapMobil");
                    }
                    else if (spawnedPart.name == "Ban(Clone)")
                    {
                        TutorialModule2.IncrementPartCount("Ban");
                    }
                    else if (spawnedPart.name == "Sekop(Clone)")
                    {
                        TutorialModule2.IncrementPartCount("Sekop");
                    }
                    else if (spawnedPart.name == "PartCrane(Clone)")
                    {
                        TutorialModule2.IncrementPartCount("PartCrane");
                    }

                    Destroy(spawnedPart);
                }
                else
                {
                    // First, return TruckDone to its normal rotation
                    Quaternion originalRotation = Quaternion.identity;
                    TruckDone.transform.rotation = originalRotation;

                    // Wait for a frame to ensure the TruckDone rotation is set
                    yield return null;

                    // Check if the part is in the correct order
                    if (TutorialModule2.IsCorrectPartOrder(spawnedPart.name))
                    {
                        // Use the SnapObjectToPosition method to position the part
                        SnapObjectToPosition(spawnedPart);

                        // Make the part a child of TruckDone
                        spawnedPart.transform.SetParent(TruckDone.transform);

                        // Move to the next part in the order and check if all parts are placed
                        TutorialModule2.IncrementPartOrder(spawnedPart.name);

                        yield break; // Exit the coroutine, the part is now in the drop zone
                    }
                    else
                    {
                        // Show error panel if part is out of order
                        StartCoroutine(TutorialModule2.ShowOrderErrorPanel());

                        // Increase count if part is destroyed due to incorrect order
                        if (spawnedPart.name == "BadanMobil1(Clone)")
                        {
                            TutorialModule2.IncrementPartCount("BadanMobil1");
                        }
                        else if (spawnedPart.name == "BadanMobil2(Clone)")
                        {
                            TutorialModule2.IncrementPartCount("BadanMobil2");
                        }
                        else if (spawnedPart.name == "BelakangMobil(Clone)")
                        {
                            TutorialModule2.IncrementPartCount("BelakangMobil");
                        }
                        else if (spawnedPart.name == "MobilDepan(Clone)")
                        {
                            TutorialModule2.IncrementPartCount("MobilDepan");
                        }
                        else if (spawnedPart.name == "Stir(Clone)")
                        {
                            TutorialModule2.IncrementPartCount("Stir");
                        }
                        else if (spawnedPart.name == "PinggirMobil(Clone)")
                        {
                            TutorialModule2.IncrementPartCount("PinggirMobil");
                        }
                        else if (spawnedPart.name == "SisiMobil(Clone)")
                        {
                            TutorialModule2.IncrementPartCount("SisiMobil");
                        }
                        else if (spawnedPart.name == "AtapMobil(Clone)")
                        {
                            TutorialModule2.IncrementPartCount("AtapMobil");
                        }
                        else if (spawnedPart.name == "Ban(Clone)")
                        {
                            TutorialModule2.IncrementPartCount("Ban");
                        }
                        else if (spawnedPart.name == "Sekop(Clone)")
                        {
                            TutorialModule2.IncrementPartCount("Sekop");
                        }
                        else if (spawnedPart.name == "PartCrane(Clone)")
                        {
                            TutorialModule2.IncrementPartCount("PartCrane");
                        }

                        // Destroy the part and reset
                        Destroy(spawnedPart);
                    }
                }
            }

            // Wait for the next frame before checking again
            yield return null;
        }
    }

    private bool IsInDropZone()
    {
        Collider dropZoneCollider = DropZone.GetComponent<Collider>();
        Collider partCollider = spawnedPart.GetComponent<Collider>();
        return dropZoneCollider != null && partCollider != null && dropZoneCollider.bounds.Intersects(partCollider.bounds);
    }

    // Method to check if the corresponding part can still be spawned
    private bool CanSpawnPart()
    {
        // Check if the part can be placed in TruckDone based on max allowed counts
        return TutorialModule2.CanPlaceMoreParts(correspondingPart.name);
    }

    // Updated method for snapping objects based on their names and TruckDone's position
    private void SnapObjectToPosition(GameObject selectedObject)
    {
        if (selectedObject.name == "BadanMobil1(Clone)")
        {
            Vector3 targetPosition = new Vector3(0.0340004f, -0.477f, -0.066f);
            selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
        }
        else if (selectedObject.name == "BadanMobil2(Clone)")
        {
            Vector3 targetPosition = new Vector3(0.0385f, -0.3192f, 0.1127f);
            selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
        }
        else if (selectedObject.name == "BelakangMobil(Clone)")
        {
            Vector3 targetPosition = new Vector3(0.0345f, -0.3688f, 0.2155f);
            Quaternion targetRotation = Quaternion.Euler(270f, 270f, 270f);
            selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
            selectedObject.transform.rotation = targetRotation;
        }
        else if (selectedObject.name == "MobilDepan(Clone)")
        {
            Vector3 targetPosition = new Vector3(0.0340004f, -0.3185f, -0.2715f);
            selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
        }
        else if (selectedObject.name == "Stir(Clone)")
        {
            Vector3 targetPosition = new Vector3(0.0340004f, -0.2742f, -0.0985f);
            Quaternion targetRotation = Quaternion.Euler(-90f, 180f, 180f);
            selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
            selectedObject.transform.rotation = targetRotation;
        }
        else if (selectedObject.name == "PinggirMobil(Clone)")
        {
            int pinggirMobilCount = TutorialModule2.GetPlacedCount("PinggirMobil(Clone)");
            if (pinggirMobilCount == 0)
            {
                Vector3 targetPosition = new Vector3(0.1363f, -0.3932f, -0.1539f);
                Quaternion targetRotation = Quaternion.Euler(-90f, 0f, 180f);
                selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
                selectedObject.transform.rotation = targetRotation;
            }
            else
            {
                Vector3 targetPosition = new Vector3(-0.0672f, -0.3932f, -0.1539f);
                Quaternion targetRotation = Quaternion.Euler(-90f, 0f, 0f);
                selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
                selectedObject.transform.rotation = targetRotation;
            }
        }
        else if (selectedObject.name == "SisiMobil(Clone)")
        {
            int sisiMobilCount = TutorialModule2.GetPlacedCount("SisiMobil(Clone)");
            if (sisiMobilCount == 0)
            {
                Vector3 targetPosition = new Vector3(0.1275f, -0.3208f, -0.0827f);
                Quaternion targetRotation = Quaternion.Euler(-90f, 0f, 180f);
                selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
                selectedObject.transform.rotation = targetRotation;
            }
            else
            {
                Vector3 targetPosition = new Vector3(-0.0585f, -0.3208f, -0.0827f);
                Quaternion targetRotation = Quaternion.Euler(-90f, 0f, 0f);
                selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
                selectedObject.transform.rotation = targetRotation;
            }
        }
        else if (selectedObject.name == "AtapMobil(Clone)")
        {
            Vector3 targetPosition = new Vector3(0.0340004f, -0.1616f, -0.0681f);
            selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
        }
        else if (selectedObject.name == "Ban(Clone)")
        {
            int banCount = TutorialModule2.GetPlacedCount("Ban(Clone)");
            if (banCount == 0)
            {
                Vector3 targetPosition = new Vector3(0.2222f, -0.4914f, -0.2751f);
                Quaternion targetRotation = Quaternion.Euler(0f, 90f, 90f);
                selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
                selectedObject.transform.rotation = targetRotation;
            }
            else if (banCount == 1)
            {
                Vector3 targetPosition = new Vector3(0.2222f, -0.4914f, 0.063f);
                Quaternion targetRotation = Quaternion.Euler(0f, 90f, 90f);
                selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
                selectedObject.transform.rotation = targetRotation;
            }
            else if (banCount == 2)
            {
                Vector3 targetPosition = new Vector3(-0.1547f, -0.4914f, -0.2751f);
                Quaternion targetRotation = Quaternion.Euler(0f, 270f, 90f);
                selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
                selectedObject.transform.rotation = targetRotation;
            }
            else if (banCount == 3)
            {
                Vector3 targetPosition = new Vector3(-0.1547f, -0.4914f, 0.063f);
                Quaternion targetRotation = Quaternion.Euler(0f, 270f, 90f);
                selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
                selectedObject.transform.rotation = targetRotation;
            }
        }
        else if (selectedObject.name == "Sekop(Clone)")
        {
            Vector3 targetPosition = new Vector3(0.035f, -0.35f, -0.46f);
            selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
        }
        else if (selectedObject.name == "PartCrane(Clone)")
        {
            Vector3 targetPosition = new Vector3(0.0340004f, -0.32f, -0.4611f);
            Quaternion targetRotation = Quaternion.Euler(0f, -90f, 0f);
            selectedObject.transform.position = TruckDone.transform.TransformPoint(targetPosition);
            selectedObject.transform.rotation = targetRotation;
        }
        else
        {
            selectedObject.transform.position = DropZone.transform.position;
        }
    }

    public void UpdatePartText(string partName, int count)
    {
        if (partName == "BadanMobil1(Clone)" && TutorialModule2.BadanMobil1Text != null)
        {
            TutorialModule2.BadanMobil1Text.text = "Lego Body\n" + count + "x";
        }
        else if (partName == "BadanMobil2(Clone)" && TutorialModule2.BadanMobil2Text != null)
        {
            TutorialModule2.BadanMobil2Text.text = "Engine\n" + count + "x";
        }
        else if (partName == "BelakangMobil(Clone)" && TutorialModule2.BelakangMobilText != null)
        {
            TutorialModule2.BelakangMobilText.text = "Back Bumper\n" + count + "x";
        }
        else if (partName == "MobilDepan(Clone)" && TutorialModule2.MobilDepanText != null)
        {
            TutorialModule2.MobilDepanText.text = "Brake Group\n" + count + "x";
        }
        else if (partName == "Stir(Clone)" && TutorialModule2.StirText != null)
        {
            TutorialModule2.StirText.text = "Steering Wheel\n" + count + "x";
        }
        else if (partName == "PinggirMobil(Clone)" && TutorialModule2.PinggirMobilText != null)
        {
            int currentCount = 2 - count;
            TutorialModule2.PinggirMobilText.text = "Stair\n" + count + "x";
        }
        else if (partName == "SisiMobil(Clone)" && TutorialModule2.SisiMobilText != null)
        {
            TutorialModule2.SisiMobilText.text = "Side Bumper\n" + count + "x";
        }
        else if (partName == "AtapMobil(Clone)" && TutorialModule2.AtapMobilText != null)
        {
            TutorialModule2.AtapMobilText.text = "Cab\n" + count + "x";
        }
        else if (partName == "Ban(Clone)" && TutorialModule2.BanText != null)
        {
            TutorialModule2.BanText.text = "Wheel\n" + count + "x";
        }
        else if (partName == "Sekop(Clone)" && TutorialModule2.SekopText != null)
        {
            TutorialModule2.SekopText.text = "Bucket\n" + count + "x";
        }
        else if (partName == "PartCrane(Clone)" && TutorialModule2.PartCraneText != null)
        {
            TutorialModule2.PartCraneText.text = "Lift Cylinder\n" + count + "x";
        }
    }
}