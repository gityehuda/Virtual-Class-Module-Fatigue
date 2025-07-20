using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class TutorialBucketPartTutorialData
{
    public GameObject BucketPart; 
    public GameObject Part;       
}

public class TutorialModule2 : MonoBehaviour
{
    public static TutorialModule2 instance;

    // Array to hold BucketParts and associated Parts
    public TutorialBucketPartTutorialData[] BucketParts;

    // Prefab for Meja
    public GameObject MejaPrefab;
    private GameObject instantiatedMeja;
    public GameObject truckPrefab;
    private GameObject instantiatedTruck; 

    // New ConversationBeginPanel
    public GameObject[] ConversationBeginPanel;
    private int currentConversationBeginPanelIndex = 0;
    public Button nextConversationBeginPanelButton;
    public Button closeConversationBeginPanelButton; 

    public GameObject[] ConversationTutorial;
    private int currentConversationTutorialIndex = 0;
    public Button nextConversationTutorialButton;
    public Button closeConversationTutorialButton;
    private bool conversationTutorialStarted = false;

    // Declare the new array and index
    public GameObject[] ConversationTutorialRedBox;
    private int currentConversationTutorialRedBoxIndex = 0;

    // Conversation panel array for AfterConversationTutorial
    public GameObject[] AfterConversationTutorial;
    private int currentAfterConversationTutorialIndex = 0;
    public Button nextAfterConversationTutorialButton;
    public Button closeAfterConversationTutorialButton;

    // New array for ConversationTutorialDone
    public GameObject[] ConversationTutorialDone;
    private int currentConversationTutorialDoneIndex = 0;

    // Buttons for navigating the ConversationTutorialDone panels
    public Button nextConversationTutorialDoneButton;
    public Button closeConversationTutorialDoneButton;

    // Button to start the game
    public Button startTutorialButton;

    // Button to start the module (enables dragging and spawning)
    public Button startTutorModuleButton;

    // Button to show when all parts are placed
    public Button finishTutorialButton;

    // Text for progress, showing "X out of 10"
    public TextMeshProUGUI progressText;

    // Panel to show when a new truck is completed
    public GameObject progressPanel;
    private bool previousProgressPanelState = false;
    public GameObject exitButton;

    private Vector2 originalExitButtonPosition;
    private Vector2 movedExitButtonPosition = new Vector2(5, -40);

    // DropZone reference (automatically assigned)
    private GameObject DropZone;

    private GameObject truckDone;

    public bool moduleStarted = false;

    public GameObject orderErrorPanel;

    private List<string> partOrder = new List<string>
    {
        "BadanMobil1(Clone)", "BadanMobil2(Clone)", "BelakangMobil(Clone)", "MobilDepan(Clone)",
        "Stir(Clone)", "PinggirMobil(Clone)", "PinggirMobil(Clone)", "SisiMobil(Clone)", "SisiMobil(Clone)", "AtapMobil(Clone)",
        "Ban(Clone)", "Ban(Clone)", "Ban(Clone)", "Ban(Clone)", "Sekop(Clone)", "PartCrane(Clone)"
    };

    private Dictionary<string, int> placedPartCounts = new Dictionary<string, int>();
    private Dictionary<string, int> maxPartCounts = new Dictionary<string, int>()
    {
        { "Ban(Clone)", 4 },
        { "SisiMobil(Clone)", 2 },
        { "PinggirMobil(Clone)", 2 }
    };

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

    private int currentPartIndex = 0;
    private float assemblyTime = 0f;
    private bool isTiming = false;
    private int trucksCompleted = 0;
    private int totalTrucks = 1;

    public Button StartTheModuleButton;
    private List<GameObject> instantiatedBucketParts = new List<GameObject>();

    public TextMeshProUGUI timerText;
    public GameObject waktuKumulatifPanel;
    public GameObject NextPartPanel;
    public TextMeshProUGUI NextPartText;

    public TextMeshProUGUI BadanMobil1Text;
    public TextMeshProUGUI BadanMobil2Text;
    public TextMeshProUGUI BelakangMobilText;
    public TextMeshProUGUI MobilDepanText;
    public TextMeshProUGUI StirText;
    public TextMeshProUGUI PinggirMobilText;
    public int placedPinggirMobilCount = 0; 
    public TextMeshProUGUI SisiMobilText;
    public int placedSisiMobilCount = 0;
    public TextMeshProUGUI AtapMobilText;
    public TextMeshProUGUI BanText;
    public int placedBanMobilCount = 0;
    public TextMeshProUGUI SekopText;
    public TextMeshProUGUI PartCraneText;

    public bool showFirstPanelOnStart = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startTutorModuleButton.interactable = false;
        finishTutorialButton.gameObject.SetActive(false);
        startTutorModuleButton.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        progressPanel.SetActive(false);
        previousProgressPanelState = progressPanel.activeSelf;
        waktuKumulatifPanel.SetActive(false);
        NextPartPanel.SetActive(false);
        StartTheModuleButton.gameObject.SetActive(false);  // Hide at the start
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

        // Initialize the Next and Close buttons for ConversationOne
        //nextConversationOneButton.onClick.AddListener(OnNextConversationOneClicked);
        //closeConversationOneButton.onClick.AddListener(OnCloseConversationOneClicked);

        // Initialize the Next and Close buttons for ConversationTutorial
        nextConversationTutorialButton.onClick.AddListener(OnNextConversationTutorialClicked);
        closeConversationTutorialButton.onClick.AddListener(OnCloseConversationTutorialClicked);

        //nextConversationTwoTutorialButton.onClick.AddListener(OnNextConversationTwoTutorialClicked);
        //closeConversationTwoTutorialButton.onClick.AddListener(OnCloseConversationTwoTutorialClicked);

        // Initialize the Next and Close buttons for AfterConversationTutorial
        nextAfterConversationTutorialButton.onClick.AddListener(OnNextAfterConversationTutorialClicked);
        closeAfterConversationTutorialButton.onClick.AddListener(OnCloseAfterConversationTutorialClicked);

        // Initialize the Next and Close buttons for ConversationTutorialDone
        nextConversationTutorialDoneButton.onClick.AddListener(OnNextConversationTutorialDoneClicked);
        closeConversationTutorialDoneButton.onClick.AddListener(OnCloseConversationTutorialDoneClicked);

        // Initialize the Next and Close buttons for ConversationBeginPanel
        nextConversationBeginPanelButton.onClick.AddListener(OnNextConversationBeginPanelClicked);
        closeConversationBeginPanelButton.onClick.AddListener(OnCloseConversationBeginPanelClicked);


        StartTheModuleButton.onClick.AddListener(OnNextGameClicked);
        progressText.text = trucksCompleted + " out of " + totalTrucks;

        UpdateNextPartUI();

        if (showFirstPanelOnStart) ShowFirstPanel();

        if (startTutorModuleButton != null)
        {
            startTutorModuleButton.onClick.AddListener(OnStartModuleClicked);
        }
        else
        {
            Debug.LogError("StartModule button not assigned.");
        }

        if (finishTutorialButton != null)
        {
            finishTutorialButton.onClick.AddListener(OnFinishClicked);
        }
    }

    public void ShowFirstPanel()
    {
        // Show the first panel in the ConversationBeginPanel array
        ShowConversationBeginPanel(0);
    }

    private void ShowPartMobilText()
    {
        if (BadanMobil1Text != null)
        {
            BadanMobil1Text.text = "Lego Body\n1x";
            BadanMobil1Text.gameObject.SetActive(true);
        }
        if (BadanMobil2Text != null)
        {
            BadanMobil2Text.text = "Engine\n1x";
            BadanMobil2Text.gameObject.SetActive(true);
        }
        if (BelakangMobilText != null)
        {
            BelakangMobilText.text = "Back Bumper\n1x";
            BelakangMobilText.gameObject.SetActive(true); 
        }
        if (MobilDepanText != null)
        {
            MobilDepanText.text = "Brake Group\n1x";
            MobilDepanText.gameObject.SetActive(true);
        }
        if (StirText != null)
        {
            StirText.text = "Steering Wheel\n1x";
            StirText.gameObject.SetActive(true);
        }
        if (PinggirMobilText != null)
        {
            PinggirMobilText.text = "Stairs\n2x";
            PinggirMobilText.gameObject.SetActive(true);
        }
        if (SisiMobilText != null)
        {
            SisiMobilText.text = "Side Bumper\n2x";
            SisiMobilText.gameObject.SetActive(true);
        }
        if (AtapMobilText != null)
        {
            AtapMobilText.text = "Cab\n1x";
            AtapMobilText.gameObject.SetActive(true);
        }
        if (BanText != null)
        {
            BanText.text = "Wheels\n4x";
            BanText.gameObject.SetActive(true);
        }
        if (SekopText != null)
        {
            SekopText.text = "Bucket\n1x";
            SekopText.gameObject.SetActive(true);
        }
        if (PartCraneText != null)
        {
            PartCraneText.text = "Lift Cylinder\n1x";
            PartCraneText.gameObject.SetActive(true);
        }
    }


    private void SpawnTruck()
    {
        // Ensure truckPrefab is assigned
        if (truckPrefab == null)
        {
            Debug.LogError("Truck prefab not assigned!");
            return;
        }

        // Instantiate the Truck prefab at the same position, rotation, and scale as defined in the prefab
        instantiatedTruck = Instantiate(truckPrefab); // No need to specify position, rotation, or scale

        // Add the RotateTruck script to the instantiated Truck (if it doesn't already exist)
        if (instantiatedTruck.GetComponent<RotateTruck>() == null)
        {
            instantiatedTruck.AddComponent<RotateTruck>();
        }

        Debug.Log("Truck with RotateTruck script spawned, using prefab's original transform properties.");

        // Find the GameObject named "LabAntro(Clone)"
        GameObject labAntro = GameObject.Find("LabAntro(Clone)");
        if (labAntro != null)
        {
            // Find the child GameObject named "Lecturer" inside LabAntro
            Transform lecturerTransform = labAntro.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                lecturerTransform.gameObject.SetActive(false);
                Debug.Log("Lecturer found and destroyed.");
            }
            else
            {
                Debug.LogWarning("Lecturer not found inside LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogWarning("LabAntro(Clone) not found in the scene.");
        }

        // Open the first conversation panel in the ConversationOne array
        //ShowConversationOnePanel(0);
    }

    private void ShowConversationBeginPanel(int index)
    {
        // Hide all panels in the ConversationBeginPanel array
        foreach (GameObject panel in ConversationBeginPanel)
        {
            panel.SetActive(false);
        }

        // Show the current panel
        if (index < ConversationBeginPanel.Length)
        {
            ConversationBeginPanel[index].SetActive(true);
            nextConversationBeginPanelButton.gameObject.SetActive(true);
            closeConversationBeginPanelButton.gameObject.SetActive(false);

            // If it's the last panel, show the Close button instead of Next
            if (index == ConversationBeginPanel.Length - 1)
            {
                nextConversationBeginPanelButton.gameObject.SetActive(false);
                closeConversationBeginPanelButton.gameObject.SetActive(true);
            }
        }
    }


    private void OnNextConversationBeginPanelClicked()
    {
        currentConversationBeginPanelIndex++;
        ShowConversationBeginPanel(currentConversationBeginPanelIndex);
    }

    private void OnCloseConversationBeginPanelClicked()
    {
        ConversationBeginPanel[currentConversationBeginPanelIndex].SetActive(false);
        closeConversationBeginPanelButton.gameObject.SetActive(false);

        GameObject labAntro = GameObject.Find("LabAntro(Clone)");
        if (labAntro != null)
        {
            Transform lecturerTransform = labAntro.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                lecturerTransform.gameObject.SetActive(false); // Hide the Lecturer
                Debug.Log("Lecturer found and hidden.");
            }
            else
            {
                Debug.LogWarning("Lecturer not found inside LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogWarning("LabAntro(Clone) not found in the scene.");
        }
        StartGame();
    }


    /*private void ShowConversationOnePanel(int index)
    {
        // Hide all panels in the ConversationOne array
        foreach (GameObject panel in ConversationOne)
        {
            panel.SetActive(false);
        }

        // Show the current panel
        if (index < ConversationOne.Length)
        {
            ConversationOne[index].SetActive(true);
            nextConversationOneButton.gameObject.SetActive(true);
            closeConversationOneButton.gameObject.SetActive(false);

            // If it's the last panel, show the Close button instead of Next
            if (index == ConversationOne.Length - 1)
            {
                nextConversationOneButton.gameObject.SetActive(false);
                closeConversationOneButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnNextConversationOneClicked()
    {
        currentConversationOneIndex++;
        ShowConversationOnePanel(currentConversationOneIndex);
    }

    private void OnCloseConversationOneClicked()
    {
        ConversationOne[currentConversationOneIndex].SetActive(false);
        closeConversationOneButton.gameObject.SetActive(false);
    }*/

    private void ShowAfterConversationTutorialPanel(int index)
    {
        // Hide all panels in the AfterConversationTutorial array
        foreach (GameObject panel in AfterConversationTutorial)
        {
            panel.SetActive(false);
        }

        // Show the current panel
        if (index < AfterConversationTutorial.Length)
        {
            AfterConversationTutorial[index].SetActive(true);
            nextAfterConversationTutorialButton.gameObject.SetActive(true);
            closeAfterConversationTutorialButton.gameObject.SetActive(false);

            // If it's the last panel, show the Close button instead of Next
            if (index == AfterConversationTutorial.Length - 1)
            {
                nextAfterConversationTutorialButton.gameObject.SetActive(false);
                closeAfterConversationTutorialButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnNextAfterConversationTutorialClicked()
    {
        currentAfterConversationTutorialIndex++;
        ShowAfterConversationTutorialPanel(currentAfterConversationTutorialIndex);
    }

    private void OnCloseAfterConversationTutorialClicked()
    {
        // Close the current panel and hide the close button
        AfterConversationTutorial[currentAfterConversationTutorialIndex].SetActive(false);
        closeAfterConversationTutorialButton.gameObject.SetActive(false);
    }


    private void Update()
    {
        // If the startTutorModuleButton becomes visible, open the ConversationTutorial
        if (startTutorModuleButton.gameObject.activeSelf && !conversationTutorialStarted)
        {
            conversationTutorialStarted = true;
            ShowConversationTutorialPanel(0); // Show the first panel of the ConversationTutorial

            // Show the progress panel, waktuKumulatifPanel, and NextPartPanel when the startTutorModuleButton appears
            progressPanel.SetActive(true);
            waktuKumulatifPanel.SetActive(true);
            NextPartPanel.SetActive(true);

            // Initialize the timer text to 00:00 and show it
            assemblyTime = 0f;
            timerText.text = "00:00";
            timerText.gameObject.SetActive(true);
        }

        if (isTiming)
        {
            assemblyTime += Time.deltaTime;
            timerText.text = FormatTime(assemblyTime);
        }

        if (progressPanel.activeSelf != previousProgressPanelState)
        {
            previousProgressPanelState = progressPanel.activeSelf;
            OnProgressPanelStateChanged(progressPanel.activeSelf);
        }
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

    private void ShowConversationTutorialPanel(int index)
    {
        // Disable the startTutorModuleButton while tutorial panels are showing
        startTutorModuleButton.interactable = false;

        // Hide all panels in the ConversationTutorial array
        foreach (GameObject panel in ConversationTutorial)
        {
            panel.SetActive(false);
        }

        // Hide all panels in the ConversationTutorialRedBox array
        foreach (GameObject panel in ConversationTutorialRedBox)
        {
            panel.SetActive(false);
        }

        // Show the current panel for ConversationTutorial
        if (index < ConversationTutorial.Length)
        {
            ConversationTutorial[index].SetActive(true);
            nextConversationTutorialButton.gameObject.SetActive(true);
            closeConversationTutorialButton.gameObject.SetActive(false);

            // If it's the last panel, show the Close button instead of Next
            if (index == ConversationTutorial.Length - 1)
            {
                nextConversationTutorialButton.gameObject.SetActive(false);
                closeConversationTutorialButton.gameObject.SetActive(true);
            }
        }

        // Show the corresponding panel for ConversationTutorialRedBox if within bounds
        if (index < ConversationTutorialRedBox.Length)
        {
            ConversationTutorialRedBox[index].SetActive(true);
        }
    }

    private void OnNextConversationTutorialClicked()
    {
        currentConversationTutorialIndex++;
        currentConversationTutorialRedBoxIndex++;

        // Ensure that the RedBox index does not exceed its length
        if (currentConversationTutorialRedBoxIndex >= ConversationTutorialRedBox.Length)
        {
            currentConversationTutorialRedBoxIndex = ConversationTutorialRedBox.Length - 1;
        }

        ShowConversationTutorialPanel(currentConversationTutorialIndex);
    }

    private void OnCloseConversationTutorialClicked()
    {
        // Close the current panel and hide the close button
        ConversationTutorial[currentConversationTutorialIndex].SetActive(false);

        // Close the current panel of ConversationTutorialRedBox if within bounds
        if (currentConversationTutorialRedBoxIndex < ConversationTutorialRedBox.Length)
        {
            ConversationTutorialRedBox[currentConversationTutorialRedBoxIndex].SetActive(false);
        }

        closeConversationTutorialButton.gameObject.SetActive(false);

        // Enable the startTutorModuleButton now that the panels are closed
        startTutorModuleButton.interactable = true;
    }



    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void StartGame()
    {
        // Hide the startTutorialButton after it's pressed
        startTutorialButton.gameObject.SetActive(false);

        // Destroy the instantiated Truck if it exists
        if (instantiatedTruck != null)
        {
            Destroy(instantiatedTruck);
            Debug.Log("Truck destroyed.");
        }

        // Show the Start Module button
        startTutorModuleButton.gameObject.SetActive(true);

        // Instantiate Meja with its original position, rotation, and scale
        instantiatedMeja = Instantiate(MejaPrefab, MejaPrefab.transform.position, MejaPrefab.transform.rotation);
        instantiatedMeja.transform.localScale = MejaPrefab.transform.localScale;

        // Set the DropZone reference
        DropZone = instantiatedMeja.transform.Find("Cube").gameObject;
        if (DropZone == null)
        {
            Debug.LogError("DropZone (Meja->Cube) not found.");
            return;
        }

        // Instantiate TruckDone with a default rotation and position
        truckDone = new GameObject("TruckDone");
        truckDone.transform.position = new Vector3(2.3f, 1.7f, 5.61f); // Example position
        truckDone.transform.rotation = Quaternion.identity;

        // Add RotateTruck script to TruckDone
        if (truckDone.GetComponent<RotateTruck>() == null)
        {
            truckDone.AddComponent<RotateTruck>();
            Debug.Log("RotateTruck script added to TruckDone");
        }

        // Move Camera to the desired position and rotation
        Camera.main.transform.position = new Vector3(2.32f, 3.05f, 4.14f); // Set camera position
        Camera.main.transform.rotation = Quaternion.Euler(48f, 0f, 0f);    // Set camera rotation with X = 48 degrees

        Debug.Log("Camera moved to the specified position and rotation.");

        // Instantiate the BucketParts
        foreach (TutorialBucketPartTutorialData bucketPartData in BucketParts)
        {
            GameObject bucketPartInstance = Instantiate(bucketPartData.BucketPart, bucketPartData.BucketPart.transform.position, bucketPartData.BucketPart.transform.rotation);
            bucketPartInstance.transform.localScale = bucketPartData.BucketPart.transform.localScale;

            bucketPartInstance.tag = "Tutorial";

            instantiatedBucketParts.Add(bucketPartInstance);

            TutorialBucketPartClickHandler clickHandler = bucketPartInstance.AddComponent<TutorialBucketPartClickHandler>();
            clickHandler.correspondingPart = bucketPartData.Part;
            clickHandler.DropZone = DropZone;
            clickHandler.TruckDone = truckDone;
            clickHandler.moduleManager2 = this;

            // Show `BadanMobil1Text` when spawning BucketParts
            ShowPartMobilText();
        }


        Debug.Log("Game started, Meja, BucketParts instantiated, TruckDone created.");
    }


    public void OnStartModuleClicked()
    {
        Debug.Log("Start Module button clicked. Dragging and spawning enabled.");

        moduleStarted = true;
        waktuKumulatifPanel.SetActive(true);
        NextPartPanel.SetActive(true);
        timerText.gameObject.SetActive(true);

        assemblyTime = 0f;
        isTiming = true;

        startTutorModuleButton.gameObject.SetActive(false);
        progressPanel.SetActive(true);

        // Disable the startTutorModuleButton to prevent further clicks while the panels are open
        startTutorModuleButton.interactable = false;

        // Show the first panel of ConversationTwoTutorial
        //ShowConversationTwoTutorialPanel(0);
    }
    /*private void ShowConversationTwoTutorialPanel(int index)
    {
        // Set the flag to true when the tutorial is open
        isConversationTwoTutorialOpen = true;

        startTutorModuleButton.interactable = false;

        foreach (GameObject panel in ConversationTwoTutorial)
        {
            panel.SetActive(false);
        }

        if (index < ConversationTwoTutorial.Length)
        {
            ConversationTwoTutorial[index].SetActive(true);
            nextConversationTwoTutorialButton.gameObject.SetActive(true);
            closeConversationTwoTutorialButton.gameObject.SetActive(false);

            if (index == ConversationTwoTutorial.Length - 1)
            {
                nextConversationTwoTutorialButton.gameObject.SetActive(false);
                closeConversationTwoTutorialButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnNextConversationTwoTutorialClicked()
    {
        currentConversationTwoTutorialIndex++;
        ShowConversationTwoTutorialPanel(currentConversationTwoTutorialIndex);
    }

    private void OnCloseConversationTwoTutorialClicked()
    {
        // Set the flag to false when the tutorial is closed
        isConversationTwoTutorialOpen = false;

        ConversationTwoTutorial[currentConversationTwoTutorialIndex].SetActive(false);
        closeConversationTwoTutorialButton.gameObject.SetActive(false);

        startTutorModuleButton.interactable = true;
    }*/

    private void ShowConversationTutorialDonePanel(int index)
    {
        // Hide all panels in the ConversationTutorialDone array
        foreach (GameObject panel in ConversationTutorialDone)
        {
            panel.SetActive(false);
        }

        // Show the current panel
        if (index < ConversationTutorialDone.Length)
        {
            ConversationTutorialDone[index].SetActive(true);
            nextConversationTutorialDoneButton.gameObject.SetActive(true);
            closeConversationTutorialDoneButton.gameObject.SetActive(false);

            // If it's the last panel, show the Close button instead of Next
            if (index == ConversationTutorialDone.Length - 1)
            {
                nextConversationTutorialDoneButton.gameObject.SetActive(false);
                closeConversationTutorialDoneButton.gameObject.SetActive(true);
            }

            // Move the camera to the specified position and rotation
            Camera.main.transform.position = new Vector3(2.32f, 1.64f, 4.358f);
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);

            // Check if it's the first panel (index 0) and spawn the truck
            if (index == 0)
            {
                SpawnTruck();
            }
        }
    }

    // Function to handle clicking the Next button in the ConversationTutorialDone panels
    private void OnNextConversationTutorialDoneClicked()
    {
        currentConversationTutorialDoneIndex++;
        ShowConversationTutorialDonePanel(currentConversationTutorialDoneIndex);
    }

    // Function to handle clicking the Close button in the ConversationTutorialDone panels
    private void OnCloseConversationTutorialDoneClicked()
    {
        // Close the current panel and hide the close button
        ConversationTutorialDone[currentConversationTutorialDoneIndex].SetActive(false);
        closeConversationTutorialDoneButton.gameObject.SetActive(false);
    }

    public bool IsCorrectPartOrder(string partName)
    {
        return partName == partOrder[currentPartIndex];
    }

    public void UpdateNextPartUI()
    {
        if (currentPartIndex < partOrder.Count)
        {
            string nextPartName = partOrder[currentPartIndex];

            if (partDisplayNames.ContainsKey(nextPartName))
            {
                nextPartName = partDisplayNames[nextPartName];
            }

            if (maxPartCounts.ContainsKey(partOrder[currentPartIndex]))
            {
                int remainingCount = maxPartCounts[partOrder[currentPartIndex]] - GetPlacedCount(partOrder[currentPartIndex]);
                NextPartText.text = "Next Part: " + nextPartName + " " + remainingCount + "x";
            }
            else
            {
                NextPartText.text = "Next Part: " + nextPartName;
            }
        }
        else
        {
            NextPartText.text = "All parts placed!";
        }
    }

    public void IncrementPartOrder(string partName)
    {
        // Track placed part count
        if (!placedPartCounts.ContainsKey(partName))
        {
            placedPartCounts[partName] = 0;
        }

        placedPartCounts[partName]++;
        currentPartIndex++;

        // Update the UI to reflect the next part
        UpdateNextPartUI();

        // Check if all parts have been placed inside TruckDone
        if (CheckIfAllPartsPlaced())
        {
            Debug.Log("All parts have been placed inside TruckDone!");
        }

        // Check if TruckDone has all 16 parts
        if (truckDone.transform.childCount == 16)
        {
            // Stop the timer immediately when TruckDone reaches 16 parts
            isTiming = false;

            // Activate the finish button
            finishTutorialButton.gameObject.SetActive(true);

            // Add RotateTruck script to TruckDone if not already added
            if (truckDone.GetComponent<RotateTruck>() == null)
            {
                truckDone.AddComponent<RotateTruck>();
                Debug.Log("RotateTruck script added to TruckDone");
            }

            // Open the first panel of AfterConversationTutorial when the finish button appears
            ShowAfterConversationTutorialPanel(0);

            // Update the progress panel when 16 parts are placed
            progressText.text = "Complete!";
            progressPanel.SetActive(true);  // Ensure the progress panel is visible
        }
    }

    public IEnumerator ShowOrderErrorPanel()
    {
        orderErrorPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        orderErrorPanel.SetActive(false);
    }

    public int GetPlacedCount(string partName)
    {
        if (!placedPartCounts.ContainsKey(partName))
        {
            placedPartCounts[partName] = 0;
        }

        return placedPartCounts[partName];
    }

    private bool CheckIfAllPartsPlaced()
    {
        Dictionary<string, int> partsInTruck = new Dictionary<string, int>();

        foreach (Transform child in truckDone.transform)
        {
            string partName = child.name;

            if (partsInTruck.ContainsKey(partName))
            {
                partsInTruck[partName]++;
            }
            else
            {
                partsInTruck[partName] = 1;
            }
        }

        foreach (string part in partOrder)
        {
            if (partsInTruck.ContainsKey(part))
            {
                partsInTruck[part]--;
                if (partsInTruck[part] < 0)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void OnFinishClicked()
    {
        isTiming = false;
        NextPartPanel.SetActive(false);

        // Destroy TruckDone
        if (truckDone != null)
        {
            Destroy(truckDone);
            Debug.Log("TruckDone destroyed.");
        }

        // Destroy instantiated bucket parts
        foreach (GameObject bucketPart in instantiatedBucketParts)
        {
            if (bucketPart != null)
            {
                Destroy(bucketPart);
            }
        }

        instantiatedBucketParts.Clear();

        // Destroy Meja (table)
        if (instantiatedMeja != null)
        {
            Destroy(instantiatedMeja);
            Debug.Log("Meja destroyed.");
        }

        trucksCompleted++;

        if (trucksCompleted >= totalTrucks)
        {
            Debug.Log("All trucks completed! Game over.");
            progressText.text = "All trucks completed!";
            progressPanel.SetActive(false);
            finishTutorialButton.gameObject.SetActive(false);
            waktuKumulatifPanel.SetActive(false);

            // Show StartTheModuleButton here
            StartTheModuleButton.gameObject.SetActive(true);

            // Open the ConversationTutorialDone panels
            ShowConversationTutorialDonePanel(0);

            return;
        }

        progressText.text = trucksCompleted + " out of " + totalTrucks;

        moduleStarted = true;
        waktuKumulatifPanel.SetActive(true);
        NextPartPanel.SetActive(true);
        timerText.gameObject.SetActive(true);

        assemblyTime = 0f;
        isTiming = true;

        progressPanel.SetActive(true);
    }

    public void OnNextGameClicked()
    {
        StartTheModuleButton.gameObject.SetActive(false);
        
        // Destroy the truck if it exists
        if (instantiatedTruck != null)
        {
            Destroy(instantiatedTruck);
            Debug.Log("Truck destroyed on StartTheModuleButton click.");
        }
    }

    public bool CanPlaceMoreParts(string partName)
    {
        if (maxPartCounts.ContainsKey(partName))
        {
            return GetPlacedCount(partName) < maxPartCounts[partName];
        }
        return true;
    }
}

public class TutorialBucketPartClickHandler : MonoBehaviour
{
    public GameObject correspondingPart;
    public GameObject DropZone;
    public GameObject TruckDone;
    public TutorialModule2 moduleManager2;
    private GameObject spawnedPart;

    private void OnMouseDown()
    {
        // Prevent spawning if ConversationTwoTutorial is still open
        /*if (moduleManager2.isConversationTwoTutorialOpen)
        {
            Debug.Log("Cannot spawn parts. ConversationTwoTutorial is still open.");
            return;
        }*/

        if (!moduleManager2.moduleStarted)
        {
            Debug.Log("Cannot spawn parts. Module not started yet.");
            return;
        }

        if (TruckDone.transform.childCount >= 16)
        {
            Debug.Log("Cannot spawn more parts. TruckDone already has 16 parts.");
            return;
        }

        if (CanSpawnPart())
        {
            Vector3 spawnPosition = transform.position;
            spawnedPart = Instantiate(correspondingPart, spawnPosition, correspondingPart.transform.rotation);
            spawnedPart.transform.localScale = correspondingPart.transform.localScale / 3f;
            DragPart dragPartScript = spawnedPart.AddComponent<DragPart>();
            dragPartScript.TruckDone = TruckDone;

            // Update the text to "Lego Body 0x" if the part is `BadanMobil1(Clone)`
            if (spawnedPart.name == "BadanMobil1(Clone)")
            {
                UpdatePartText("BadanMobil1(Clone)", 0);
            }
            else if (spawnedPart.name == "BadanMobil2(Clone)")
            {
                UpdatePartText("BadanMobil2(Clone)", 0);
            }
            else if (spawnedPart.name == "BelakangMobil(Clone)")
            {
                UpdatePartText("BelakangMobil(Clone)", 0);
            }
            else if (spawnedPart.name == "MobilDepan(Clone)")
            {
                UpdatePartText("MobilDepan(Clone)", 0);
            }
            else if (spawnedPart.name == "Stir(Clone)")
            {
                UpdatePartText("Stir(Clone)", 0);
            }
            else if (spawnedPart.name == "PinggirMobil(Clone)")
            {
                int remainingCount = 2 - moduleManager2.placedPinggirMobilCount - 1; // Remaining count after this spawn
                UpdatePartText("PinggirMobil(Clone)", remainingCount);
            }
            else if (spawnedPart.name == "SisiMobil(Clone)")
            {
                int remainingCount = 2 - moduleManager2.placedSisiMobilCount - 1; // Remaining count after this spawn
                UpdatePartText("SisiMobil(Clone)", remainingCount);
            }
            else if (spawnedPart.name == "AtapMobil(Clone)")
            {
                UpdatePartText("AtapMobil(Clone)", 0);
            }
            else if (spawnedPart.name == "Ban(Clone)")
            {
                int remainingCount = 4 - moduleManager2.placedBanMobilCount - 1; // Remaining count after this spawn
                UpdatePartText("Ban(Clone)", remainingCount);
            }
            else if (spawnedPart.name == "Sekop(Clone)")
            {
                UpdatePartText("Sekop(Clone)", 0);
            }
            else if (spawnedPart.name == "PartCrane(Clone)")
            {
                UpdatePartText("PartCrane(Clone)", 0);
            }

            StartCoroutine(CheckForMouseHold());
        }
        else
        {
            Debug.Log("Max count reached for " + correspondingPart.name);
        }
    }

    private System.Collections.IEnumerator CheckForMouseHold()
    {
        while (spawnedPart != null)
        {
            if (!Input.GetMouseButton(0))
            {
                if (!IsInDropZone())
                {
                    if (spawnedPart.name == "BadanMobil1(Clone)")
                    {
                        UpdatePartText("BadanMobil1(Clone)", 1);
                    }
                    else if (spawnedPart.name == "BadanMobil2(Clone)")
                    {
                        UpdatePartText("BadanMobil2(Clone)", 1);
                    }
                    else if (spawnedPart.name == "BelakangMobil(Clone)")
                    {
                        UpdatePartText("BelakangMobil(Clone)", 1);
                    }
                    else if (spawnedPart.name == "MobilDepan(Clone)")
                    {
                        UpdatePartText("MobilDepan(Clone)", 1);
                    }
                    else if (spawnedPart.name == "Stir(Clone)")
                    {
                        UpdatePartText("Stir(Clone)", 1);
                    }
                    else if (spawnedPart.name == "PinggirMobil(Clone)")
                    {
                        int remainingCount = 2 - moduleManager2.placedPinggirMobilCount; // Reset to current count
                        UpdatePartText("PinggirMobil(Clone)", remainingCount);
                    }
                    else if (spawnedPart.name == "SisiMobil(Clone)")
                    {
                        int remainingCount = 2 - moduleManager2.placedSisiMobilCount; // Reset to current count
                        UpdatePartText("SisiMobil(Clone)", remainingCount);
                    }
                    else if (spawnedPart.name == "AtapMobil(Clone)")
                    {
                        UpdatePartText("AtapMobil(Clone)", 1);
                    }
                    else if (spawnedPart.name == "Ban(Clone)")
                    {
                        int remainingCount = 4 - moduleManager2.placedBanMobilCount; // Reset to current count
                        UpdatePartText("Ban(Clone)", remainingCount);
                    }
                    else if (spawnedPart.name == "Sekop(Clone)")
                    {
                        UpdatePartText("Sekop(Clone)", 1);
                    }
                    else if (spawnedPart.name == "PartCrane(Clone)")
                    {
                        UpdatePartText("PartCrane(Clone)", 1);
                    }

                    Destroy(spawnedPart);
                }
                else
                {
                    Quaternion originalRotation = Quaternion.identity;
                    TruckDone.transform.rotation = originalRotation;
                    yield return null;

                    if (moduleManager2.IsCorrectPartOrder(spawnedPart.name))
                    {
                        SnapObjectToPosition(spawnedPart);
                        spawnedPart.transform.SetParent(TruckDone.transform);
                        moduleManager2.IncrementPartOrder(spawnedPart.name);

                        if (spawnedPart.name == "PinggirMobil(Clone)")
                        {
                            moduleManager2.placedPinggirMobilCount++;
                            int remainingCount = 2 - moduleManager2.placedPinggirMobilCount;
                            UpdatePartText("PinggirMobil(Clone)", remainingCount);
                        }
                        else if (spawnedPart.name == "SisiMobil(Clone)")
                        {
                            moduleManager2.placedSisiMobilCount++;
                            int remainingSisiCount = 2 - moduleManager2.placedSisiMobilCount;
                            UpdatePartText("SisiMobil(Clone)", remainingSisiCount);
                        }
                        else if (spawnedPart.name == "Ban(Clone)")
                        {
                            moduleManager2.placedBanMobilCount++;
                            int remainingBanCount = 4 - moduleManager2.placedBanMobilCount;
                            UpdatePartText("Ban(Clone)", remainingBanCount);
                        }

                        yield break;
                    }
                    else
                    {
                        StartCoroutine(moduleManager2.ShowOrderErrorPanel());

                        if (spawnedPart.name == "BadanMobil1(Clone)")
                        {
                            UpdatePartText("BadanMobil1(Clone)", 1);
                        }
                        else if (spawnedPart.name == "BadanMobil2(Clone)")
                        {
                            UpdatePartText("BadanMobil2(Clone)", 1);
                        }
                        else if (spawnedPart.name == "BelakangMobil(Clone)")
                        {
                            UpdatePartText("BelakangMobil(Clone)", 1);
                        }
                        else if (spawnedPart.name == "MobilDepan(Clone)")
                        {
                            UpdatePartText("MobilDepan(Clone)", 1);
                        }
                        else if (spawnedPart.name == "Stir(Clone)")
                        {
                            UpdatePartText("Stir(Clone)", 1);
                        }
                        else if (spawnedPart.name == "PinggirMobil(Clone)")
                        {
                            int remainingPinggirCount = 2 - moduleManager2.placedPinggirMobilCount;
                            UpdatePartText("PinggirMobil(Clone)", remainingPinggirCount);
                        }
                        else if (spawnedPart.name == "SisiMobil(Clone)")
                        {
                            int remainingSisiCount = 2 - moduleManager2.placedSisiMobilCount;
                            UpdatePartText("SisiMobil(Clone)", remainingSisiCount);
                        }
                        else if (spawnedPart.name == "AtapMobil(Clone)")
                        {
                            UpdatePartText("AtapMobil(Clone)", 1);
                        }
                        else if (spawnedPart.name == "Ban(Clone)")
                        {
                            int remainingBanCount = 4 - moduleManager2.placedBanMobilCount;
                            UpdatePartText("Ban(Clone)", remainingBanCount);
                        }
                        else if (spawnedPart.name == "Sekop(Clone)")
                        {
                            UpdatePartText("Sekop(Clone)", 1);
                        }
                        else if (spawnedPart.name == "PartCrane(Clone)")
                        {
                            UpdatePartText("PartCrane(Clone)", 1);
                        }

                        Destroy(spawnedPart);
                    }
                }
            }

            yield return null;
        }
    }


    private bool IsInDropZone()
    {
        Collider dropZoneCollider = DropZone.GetComponent<Collider>();
        Collider partCollider = spawnedPart.GetComponent<Collider>();

        if (dropZoneCollider != null && partCollider != null)
        {
            return dropZoneCollider.bounds.Intersects(partCollider.bounds);
        }

        return false;
    }

    private bool CanSpawnPart()
    {
        return moduleManager2.CanPlaceMoreParts(correspondingPart.name);
    }

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
            int pinggirMobilCount = moduleManager2.GetPlacedCount("PinggirMobil(Clone)");
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
            int sisiMobilCount = moduleManager2.GetPlacedCount("SisiMobil(Clone)");
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
            int banCount = moduleManager2.GetPlacedCount("Ban(Clone)");
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
        if (partName == "BadanMobil1(Clone)" && moduleManager2.BadanMobil1Text != null)
        {
            moduleManager2.BadanMobil1Text.text = "Lego Body\n" + count + "x";
        }
        else if (partName == "BadanMobil2(Clone)" && moduleManager2.BadanMobil2Text != null)
        {
            moduleManager2.BadanMobil2Text.text = "Engine\n" + count + "x";
        }
        else if (partName == "BelakangMobil(Clone)" && moduleManager2.BelakangMobilText != null)
        {
            moduleManager2.BelakangMobilText.text = "Back Bumper\n" + count + "x";
        }
        else if (partName == "MobilDepan(Clone)" && moduleManager2.MobilDepanText != null)
        {
            moduleManager2.MobilDepanText.text = "Brake Group\n" + count + "x";
        }
        else if (partName == "Stir(Clone)" && moduleManager2.StirText != null)
        {
            moduleManager2.StirText.text = "Steering Wheel\n" + count + "x";
        }
        else if (partName == "PinggirMobil(Clone)" && moduleManager2.PinggirMobilText != null)
        {
            int currentCount = 2 - count;
            moduleManager2.PinggirMobilText.text = "Stairs\n" + count + "x";
        }
        else if (partName == "SisiMobil(Clone)" && moduleManager2.SisiMobilText != null)
        {
            moduleManager2.SisiMobilText.text = "Side Bumper\n" + count + "x";
        }
        else if (partName == "AtapMobil(Clone)" && moduleManager2.AtapMobilText != null)
        {
            moduleManager2.AtapMobilText.text = "Cab\n" + count + "x";
        }
        else if (partName == "Ban(Clone)" && moduleManager2.BanText != null)
        {
            moduleManager2.BanText.text = "Wheels\n" + count + "x";
        }
        else if (partName == "Sekop(Clone)" && moduleManager2.SekopText != null)
        {
            moduleManager2.SekopText.text = "Bucket\n" + count + "x";
        }
        else if (partName == "PartCrane(Clone)" && moduleManager2.PartCraneText != null)
        {
            moduleManager2.PartCraneText.text = "Lift Cylinder\n" + count + "x";
        }
    }


    private void OnDestroy()
    {
        // Hide the BadanMobil1Text and BadanMobil2Text when this BucketPart prefab is destroyed
        if (moduleManager2.BadanMobil1Text != null)
        {
            moduleManager2.BadanMobil1Text.gameObject.SetActive(false);
        }
        if (moduleManager2.BadanMobil2Text != null)
        {
            moduleManager2.BadanMobil2Text.gameObject.SetActive(false);
        }
        if (moduleManager2.BelakangMobilText != null)
        {
            moduleManager2.BelakangMobilText.gameObject.SetActive(false);
        }
        if (moduleManager2.MobilDepanText != null)
        {
            moduleManager2.MobilDepanText.gameObject.SetActive(false);
        }
        if (moduleManager2.StirText != null)
        {
            moduleManager2.StirText.gameObject.SetActive(false);
        }
        if (moduleManager2.PinggirMobilText != null)
        {
            moduleManager2.PinggirMobilText.gameObject.SetActive(false);
        }
        if (moduleManager2.SisiMobilText != null)
        {
            moduleManager2.SisiMobilText.gameObject.SetActive(false);
        }
        if (moduleManager2.AtapMobilText != null)
        {
            moduleManager2.AtapMobilText.gameObject.SetActive(false);
        }
        if (moduleManager2.BanText != null)
        {
            moduleManager2.BanText.gameObject.SetActive(false);
        }
        if (moduleManager2.SekopText != null)
        {
            moduleManager2.SekopText.gameObject.SetActive(false);
        }
        if (moduleManager2.PartCraneText != null)
        {
            moduleManager2.PartCraneText.gameObject.SetActive(false);
        }
    }

}