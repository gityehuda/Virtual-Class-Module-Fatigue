using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using Binus.WebGL.Service;

public class Module3Manager : MonoBehaviour
{
    public GameObject[] conversationAwalPanels;
    public Button nextStartconversationAwalButton;
    public Button prevStartconversationAwalButton;
    public Button closeStartconversationAwalButton;

    public GameObject[] conversationAfterSelectingPanels;
    public Button nextConversationAfterSelectingButton;
    public Button prevConversationAfterSelectingButton;
    public Button closeConversationAfterSelectingButton;

    public GameObject[] conversationAfterTheFirstSimulationPanels;
    public Button nextConversationAfterTheFirstSimulationButton;
    public Button prevConversationAfterTheFirstSimulationButton;
    public Button closeConversationAfterTheFirstSimulationButton;

    public GameObject[] conversationAfterTheFirstSimulation2Panels;
    public Button nextConversationAfterTheFirstSimulation2Button;
    public Button prevConversationAfterTheFirstSimulation2Button;
    public Button closeConversationAfterTheFirstSimulation2Button;

    public GameObject[] ConversationBeforeSecondSimulationPanels;
    public GameObject panelImageConversationBeforeSecondSimulation;
    public Button nextConversationBeforeSecondSimulationButton;
    public Button prevConversationBeforeSecondSimulationButton;
    public Button closeConversationBeforeSecondSimulationButton;

    public GameObject[] ConversationAlmostFinishPanel;
    public Button nextConversationAlmostFinishPanelButton;
    public Button prevConversationAlmostFinishPanelButton;
    public Button closeConversationAlmostFinishPanelButton;
    private int almostFinishPanelIndex = 0;

    public GameObject[] ConversationFinish;
    public Button nextConversationFinishButton;
    public Button prevConversationFinishButton;
    public Button closeConversationFinishButton;

    public GameObject ReplayPanel2;
    public Button YesReplay2;
    public Button NoReplay2;

    public GameObject[] ClickedNoPanel; 
    public Button NextClickedNoButton;
    public Button prevClickedNoButton;
    public Button CloseClickedNoButton;

    public GameObject[] ClickedYesPanel;
    public Button NextClickedYesButton;
    public Button prevClickedYesButton;
    public Button CloseClickedYesButton;

    private int currentClickedNoPanelIndex = 0;
    private int currentClickedYesPanelIndex = 0;

    public Button soalButton;

    public GameObject inputPanel;
    public TMP_InputField beratBebanInput;
    public TMP_InputField waktuInput;
    public Button submitButton;

    public Button startModuleButton;
    public GameObject prefabToSpawn;
    public GameObject spawnToPrefabTwo;

    public GameObject NoAnimationPrefab;
    private GameObject spawnedNoAnimationPrefab;
    private GameObject animasiModul3Ke3Clone;
    private bool hasAnimasiModul3Ke3CloneSpawned = false;

    private GameObject animasiModul3Ke2Clone;
    private bool conversationAfterTheFirstSimulationTwo = false;
    private bool hasAnimasiModul3Ke2CloneSpawned = false;

    public GameObject ReplayPanel;
    public Animator targetAnimator;
    public Animator targetAnimator2;
    public Animator targetAnimator3;
    public Button yesButton;
    public Button noButton;
    private GameObject spawnedPrefab;
    private bool isReplayPanelShown = false;

    private int currentPanelIndex = 0;
    private int currentAfterSelectingPanelIndex = 0;
    private int currentconversationAfterTheFirstSimulationPanelIndex = 0;
    private int conversationAfterTheFirstSimulation2PanelIndex = 0;
    private int ConversationBeforeSecondSimulationPanelsIndex = 0;
    private int currentFinishPanelIndex = 0;

    public TextMeshProUGUI assumptionText;
    public TextMeshProUGUI FMResultText;
    public TextMeshProUGUI assumptionText2;
    private readonly float V = 30f;

    public GameObject BeratBebanWarning;
    public GameObject WaktuWarning;
    public GameObject BeratBebanWaktuWarning;

    private int savedBeratBeban;
    private int savedWaktu;
    private int savedRepitisi;
    private float savedFM; 


    private int currentRepitisiCount = 0;

    public GameObject ImageConversationBeforeSecondSimulation17and18;
    public GameObject ImageConversationBeforeSecondSimulation4and5;
    public GameObject ImageConversationBeforeSecondSimulation2;

    public Button DownloadButton1;
    public Button DownloadButton2;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    //public Button openConversationButton;

    public string targetObjectName = "AnimasiModul 3(Clone)"; 
    public GameObject panelTimer;
    public TextMeshProUGUI timerText; 

    private float countdownTime = 60f; 
    private GameObject targetObject; 
    private bool isCountingDown = false;

    void Start()
    {
        ShowPanel(currentPanelIndex);

        nextStartconversationAwalButton.onClick.AddListener(NextPanel);
        prevStartconversationAwalButton.onClick.AddListener(PrevPanel);
        closeStartconversationAwalButton.onClick.AddListener(CloseConversation);
        submitButton.onClick.AddListener(SubmitData);
        prevStartconversationAwalButton.gameObject.SetActive(false);
        closeStartconversationAwalButton.gameObject.SetActive(false);
        inputPanel.SetActive(false);

        nextConversationAfterSelectingButton.onClick.AddListener(NextPanelAfterSelecting);
        prevConversationAfterSelectingButton.onClick.AddListener(PrevPanelAfterSelecting);
        closeConversationAfterSelectingButton.onClick.AddListener(CloseConversationAfterSelecting);
        nextConversationAfterSelectingButton.gameObject.SetActive(false);
        prevConversationAfterSelectingButton.gameObject.SetActive(false);
        closeConversationAfterSelectingButton.gameObject.SetActive(false);

        nextConversationAfterTheFirstSimulationButton.onClick.AddListener(NextConversationAfterTheFirstSimulationPanels);
        prevConversationAfterTheFirstSimulationButton.onClick.AddListener(PrevConversationAfterTheFirstSimulationPanels);
        closeConversationAfterTheFirstSimulationButton.onClick.AddListener(CloseConversationAfterTheFirstSimulationPanels);
        prevConversationAfterTheFirstSimulationButton.gameObject.SetActive(false);

        nextConversationAfterTheFirstSimulation2Button.onClick.AddListener(NextConversationAfterTheFirstSimulation2Panels);
        prevConversationAfterTheFirstSimulation2Button.onClick.AddListener(PrevConversationAfterTheFirstSimulation2Panels);
        closeConversationAfterTheFirstSimulation2Button.onClick.AddListener(CloseConversationAfterTheFirstSimulation2Panels);
        prevConversationAfterTheFirstSimulation2Button.gameObject.SetActive(false);

        nextConversationBeforeSecondSimulationButton.onClick.AddListener(NextConversationBeforeSecondSimulationPanels);
        prevConversationBeforeSecondSimulationButton.onClick.AddListener(PrevConversationBeforeSecondSimulationPanels);
        closeConversationBeforeSecondSimulationButton.onClick.AddListener(CloseConversationBeforeSecondSimulationPanels);
        prevConversationBeforeSecondSimulationButton.gameObject.SetActive(false);

        nextConversationAlmostFinishPanelButton.onClick.AddListener(NextConversationAlmostFinishPanel);
        prevConversationAlmostFinishPanelButton.onClick.AddListener(PrevConversationAlmostFinishPanels);
        closeConversationAlmostFinishPanelButton.onClick.AddListener(CloseConversationAlmostFinishPanel);
        prevConversationAlmostFinishPanelButton.gameObject.SetActive(false);

        nextConversationFinishButton.onClick.AddListener(NextPanelFinish);
        prevConversationFinishButton.onClick.AddListener(PrevConversationFinishPanels);
        closeConversationFinishButton.onClick.AddListener(CloseConversationFinish);
        nextConversationFinishButton.gameObject.SetActive(false);
        prevConversationFinishButton.gameObject.SetActive(false);
        closeConversationFinishButton.gameObject.SetActive(false);

        soalButton.onClick.AddListener(OnSoalButtonClicked);
        soalButton.gameObject.SetActive(false);

        startModuleButton.gameObject.SetActive(false);
        startModuleButton.onClick.AddListener(SpawnPrefab);
        startModuleButton.onClick.AddListener(OnStartModuleButtonClicked);

        beratBebanInput.contentType = TMP_InputField.ContentType.IntegerNumber;
        waktuInput.contentType = TMP_InputField.ContentType.IntegerNumber;

        // Initially hide ReplayPanel
        ReplayPanel.SetActive(false);
        NoReplay2.onClick.AddListener(OnNoReplay2Clicked);
        YesReplay2.onClick.AddListener(OnYesReplay2Clicked);

        NextClickedNoButton.onClick.AddListener(NextClickedNoPanel);
        prevClickedNoButton.onClick.AddListener(PrevClickedNoPanels);
        CloseClickedNoButton.onClick.AddListener(CloseClickedNoPanel);
        prevClickedNoButton.gameObject.SetActive(false);

        NextClickedYesButton.onClick.AddListener(NextClickedYesPanel);
        prevClickedYesButton.onClick.AddListener(PrevClickedYesPanels);
        CloseClickedYesButton.onClick.AddListener(CloseClickedYesPanel);
        prevClickedYesButton.gameObject.SetActive(false);

        // Add listeners to Yes and No buttons
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);

        YesReplay2.onClick.AddListener(ResetTimer);
        yesButton.onClick.AddListener(ResetTimer);

        assumptionText.text = "";

        // Attach the event listener to DownloadButton1
        if (DownloadButton1 != null)
        {
            DownloadButton1.onClick.AddListener(OnDownloadButton1Clicked);
        }
        else
        {
            Debug.LogError("DownloadButton1 is not assigned.");
        }

        // Attach the event listener to DownloadButton2
        if (DownloadButton2 != null)
        {
            DownloadButton2.onClick.AddListener(OnDownloadButton2Clicked);
        }
        else
        {
            Debug.LogError("DownloadButton1 is not assigned.");
        }

        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                originalPosition = lecturerTransform.position;
                originalRotation = lecturerTransform.rotation;
            }
        }

        GameObject redBox = FindRedBox();
        if (redBox != null)
        {
            redBox.SetActive(false);
        }

        /*if (openConversationButton != null)
        {
            openConversationButton.onClick.AddListener(OnOpenConversationButtonClicked);  // Add listener to button
        }
        else
        {
            Debug.LogError("Open Conversation Button is not assigned.");
        }*/

    }

    GameObject FindRedBox()
    {
        GameObject conversationCanvas = GameObject.Find("ConversationCanvas");
        if (conversationCanvas == null)
        {
            Debug.LogError("ConversationCanvas not found in the scene.");
            return null;
        }

        Transform imageConversationTransform = conversationCanvas.transform.Find("ImageConversationBeforeSecondSimulation");
        if (imageConversationTransform == null)
        {
            Debug.LogError("ImageConversationBeforeSecondSimulation not found in ConversationCanvas.");
            return null;
        }

        Transform redBoxTransform = imageConversationTransform.Find("Red Box");
        if (redBoxTransform == null)
        {
            Debug.LogError("Red Box not found under ImageConversationBeforeSecondSimulation.");
            return null;
        }

        return redBoxTransform.gameObject;
    }

    void OnYesReplay2Clicked()
    {
        // Find the LabAntro(Clone) object in the scene
        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            // Find the Lecturer object under LabAntro(Clone)
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                // Hide the Lecturer object
                lecturerTransform.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Lecturer object not found under LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogError("LabAntro(Clone) object not found in the scene.");
        }

        // Hide the ReplayPanel2
        if (ReplayPanel2 != null)
        {
            ReplayPanel2.SetActive(false);
        }

        SpawnPrefab();

        // After spawning, apply the animation speed based on the selected repetition
        SetAnimationSpeed(savedRepitisi);
        StartCoroutine(PlayAnimationMultipleTimes(savedRepitisi));
    }

    // Coroutine to play animation multiple times
    IEnumerator PlayAnimationMultipleTimes(int repetitions)
    {
        for (int i = 0; i < repetitions; i++)
        {
            // Start the animation (this assumes you already have the animation set up)
            if (targetAnimator != null)
            {
                targetAnimator.SetTrigger("StartAnimation"); // Use your animation trigger here
            }

            // Wait for the animation to finish
            float animationDuration = targetAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationDuration);
        }

        // Now destroy the prefab and unhide the Lecturer object
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);
            spawnedPrefab = null;  // Clear reference to the prefab
        }

        // Unhide the Lecturer object
        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                lecturerTransform.gameObject.SetActive(true);  // Unhide the Lecturer object
            }
            else
            {
                Debug.LogError("Lecturer object not found under LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogError("LabAntro(Clone) object not found in the scene.");
        }

        ShowClickedYesPanel(currentClickedYesPanelIndex);
    }


    void OnNoReplay2Clicked()
    {
        // Hide ReplayPanel2
        if (ReplayPanel2 != null)
        {
            ReplayPanel2.SetActive(false);
        }

        // Show the first panel in the ClickedNoPanel
        ShowClickedNoPanel(currentClickedNoPanelIndex);
    }

    void ShowClickedYesPanel(int index)
    {
        // Hide all panels first
        foreach (GameObject panel in ClickedNoPanel)
        {
            panel.SetActive(false);
        }

        // Activate the current panel
        if (index < ClickedNoPanel.Length)
        {
            ClickedNoPanel[index].SetActive(true);
        }

        if (index == 0) // First panel
        {
            prevClickedYesButton.gameObject.SetActive(false); // Hide the Previous button
        }
        else
        {
            prevClickedYesButton.gameObject.SetActive(true); // Show the Previous button
        }

        // Update button visibility based on the panel index
        if (index == ClickedNoPanel.Length - 1)
        {
            // Show Close button on the last panel and hide Next button
            NextClickedYesButton.gameObject.SetActive(false);
            CloseClickedYesButton.gameObject.SetActive(true);
        }
        else
        {
            // Show Next button on all other panels and hide Close button
            NextClickedYesButton.gameObject.SetActive(true);
            CloseClickedYesButton.gameObject.SetActive(false);
        }
    }

    void NextClickedYesPanel()
    {
        if (currentClickedYesPanelIndex < ClickedYesPanel.Length - 1)
        {
            currentClickedYesPanelIndex++;
            ShowClickedYesPanel(currentClickedYesPanelIndex);
        }
    }

    void PrevClickedYesPanels()
    {
        if (currentClickedYesPanelIndex > 0)
        {
            currentClickedYesPanelIndex--;
            ShowClickedYesPanel(currentClickedYesPanelIndex);
        }
    }

    void CloseClickedYesPanel()
    {
        // Hide all panels in ClickedNoPanel
        foreach (GameObject panel in ClickedNoPanel)
        {
            panel.SetActive(false);
        }

        // Hide the Lecturer object when ClickedYesPanel is closed
        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                lecturerTransform.gameObject.SetActive(false);  // Hide the Lecturer object
            }
            else
            {
                Debug.LogError("Lecturer object not found under LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogError("LabAntro(Clone) object not found in the scene.");
        }

        if (spawnToPrefabTwo != null)
        {
            Instantiate(spawnToPrefabTwo);  // Spawn the prefab at default position and rotation
        }
        else
        {
            Debug.LogError("SpawnToPrefabTwo prefab is not assigned.");
        }

        // Hide the Close button as we close the ClickedNoPanel
        CloseClickedYesButton.gameObject.SetActive(false);
        prevClickedYesButton.gameObject.SetActive(false);
    }

    void ShowClickedNoPanel(int index)
    {
        // Hide all panels first
        foreach (GameObject panel in ClickedNoPanel)
        {
            panel.SetActive(false);
        }

        // Activate the current panel
        if (index < ClickedNoPanel.Length)
        {
            ClickedNoPanel[index].SetActive(true);
        }

        if (index == 0) // First panel
        {
            prevClickedNoButton.gameObject.SetActive(false); // Hide the Previous button
        }
        else
        {
            prevClickedNoButton.gameObject.SetActive(true); // Show the Previous button
        }

        // Update button visibility based on the panel index
        if (index == ClickedNoPanel.Length - 1)
        {
            // Show Close button on the last panel and hide Next button
            NextClickedNoButton.gameObject.SetActive(false);
            CloseClickedNoButton.gameObject.SetActive(true);
        }
        else
        {
            // Show Next button on all other panels and hide Close button
            NextClickedNoButton.gameObject.SetActive(true);
            CloseClickedNoButton.gameObject.SetActive(false);
        }
    }

    void NextClickedNoPanel()
    {
        if (currentClickedNoPanelIndex < ClickedNoPanel.Length - 1)
        {
            currentClickedNoPanelIndex++;
            ShowClickedNoPanel(currentClickedNoPanelIndex);
        }
    }

    void PrevClickedNoPanels()
    {
        if (currentClickedNoPanelIndex > 0)
        {
            currentClickedNoPanelIndex--;
            ShowClickedNoPanel(currentClickedNoPanelIndex);
        }
    }

    void CloseClickedNoPanel()
    {
        // Hide all panels in ClickedNoPanel
        foreach (GameObject panel in ClickedNoPanel)
        {
            panel.SetActive(false);
        }

        // Hide the Lecturer object when ClickedYesPanel is closed
        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                lecturerTransform.gameObject.SetActive(false);  // Hide the Lecturer object
            }
            else
            {
                Debug.LogError("Lecturer object not found under LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogError("LabAntro(Clone) object not found in the scene.");
        }

        if (spawnToPrefabTwo != null)
        {
            Instantiate(spawnToPrefabTwo);  // Spawn the prefab at default position and rotation
        }
        else
        {
            Debug.LogError("SpawnToPrefabTwo prefab is not assigned.");
        }

        // Hide the Close button as we close the ClickedNoPanel
        CloseClickedNoButton.gameObject.SetActive(false);
        prevClickedNoButton.gameObject.SetActive(false);
    }


    void OnDownloadButton1Clicked()
    {
        // Hide the DownloadButton1
        DownloadButton1.gameObject.SetActive(false);

        // Show the Next button
        nextConversationBeforeSecondSimulationButton.gameObject.SetActive(true);

        ExportCSV();
    }

    void OnDownloadButton2Clicked()
    {
        // Hide the DownloadButton1
        DownloadButton2.gameObject.SetActive(false);

        // Show the Next button
        nextConversationAlmostFinishPanelButton.gameObject.SetActive(true);

        ExportCSV2();
    }


    void Update()
    {
        CheckAnimationStatus();

        // Check if `AnimasiModul 3 Ke 2(Clone)` has been spawned
        if (!hasAnimasiModul3Ke2CloneSpawned)
        {
            animasiModul3Ke2Clone = GameObject.Find("AnimasiModul 3 Ke 2(Clone)");

            // Set the flag to true if we find `AnimasiModul 3 Ke 2(Clone)` in the scene
            if (animasiModul3Ke2Clone != null)
            {
                hasAnimasiModul3Ke2CloneSpawned = true;
            }
        }

        // When `AnimasiModul 3 Ke 2(Clone)` is destroyed, open the conversation panels
        if (hasAnimasiModul3Ke2CloneSpawned && animasiModul3Ke2Clone == null)
        {
            OpenConversationBeforeSecondSimulationPanels();  // Trigger ConversationAfterSimulation2 instead
            hasAnimasiModul3Ke2CloneSpawned = false; // Reset the flag to allow checking again if the object is spawned later
        }

        if (hasAnimasiModul3Ke2CloneSpawned && animasiModul3Ke2Clone == null && !conversationAfterTheFirstSimulationTwo)
        {
            OpenConversationBeforeSecondSimulationPanels();  // Trigger ConversationAfterSimulation2 instead
        }

        if (!hasAnimasiModul3Ke3CloneSpawned)
        {
            animasiModul3Ke3Clone = GameObject.Find("AnimasiModul 3 Ke 3(Clone)");

            if (animasiModul3Ke3Clone != null)
            {
                hasAnimasiModul3Ke3CloneSpawned = true; // Flag it as spawned
            }
        }

        // If the object was spawned and is now destroyed
        if (hasAnimasiModul3Ke3CloneSpawned && animasiModul3Ke3Clone == null)
        {
            OpenAlmostFinishPanel(); // Open AlmostFinishPanel sequence
            hasAnimasiModul3Ke3CloneSpawned = false; // Prevent repeat triggers
        }

        bool isConversationBeforeSecondSimulationOpen = false;
        foreach (GameObject panel in ConversationBeforeSecondSimulationPanels)
        {
            if (panel.activeSelf)
            {
                isConversationBeforeSecondSimulationOpen = true;
                break;
            }
        }

        targetObject = GameObject.Find(targetObjectName);

        if (targetObject != null && !isConversationBeforeSecondSimulationOpen)
        {
            // If the target object appears and ConversationBeforeSecondSimulation is not open, activate the timer
            if (!panelTimer.activeSelf)
            {
                panelTimer.SetActive(true);
                isCountingDown = true;
                countdownTime = 60f; // Reset the timer
            }
        }
        else
        {
            // If the target object disappears or ConversationBeforeSecondSimulation is open, hide the panel
            if (panelTimer.activeSelf)
            {
                panelTimer.SetActive(false);
                isCountingDown = false;
            }
        }

        // Handle the countdown
        if (isCountingDown)
        {
            countdownTime -= Time.deltaTime;

            if (countdownTime <= 0)
            {
                countdownTime = 0;
                isCountingDown = false; // Stop the timer when it reaches zero
            }

            // Update the timer text
            timerText.text = FormatTime(countdownTime);
        }
    }

    private void ResetTimer()
    {
        countdownTime = 60f; // Reset the timer to 60 seconds
        isCountingDown = true; // Start the timer
        panelTimer.SetActive(true); // Ensure the panel is active
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OpenAlmostFinishPanel()
    {
        almostFinishPanelIndex = 0; // Reset to the first panel
        ShowAlmostFinishPanel(almostFinishPanelIndex);
    }

    void ShowAlmostFinishPanel(int index)
    {
        // Hide all panels in the AlmostFinishPanel array
        // Hide all panels in the AlmostFinishPanel array
        foreach (GameObject panel in ConversationAlmostFinishPanel)
        {
            panel.SetActive(false);
        }

        // Show the panel at the current index
        if (index < ConversationAlmostFinishPanel.Length)
        {
            ConversationAlmostFinishPanel[index].SetActive(true);
        }

        if (index == 1 && assumptionText2 != null)
        {
            assumptionText2.text = "Namun sebelum IE Lab berikan datanya, perlu disampaikan bahwa dalam peragaan pada simulasi praktikum IE Lab tetapkan bahwa:​\n" +
                              $"1. Waktu kerja operator dalam pengangkatan beban adalah {savedWaktu} menit.\n" +
                              $"2. Berat beban (m<size=12>Load</size>) yang diangkat operator adalah {savedBeratBeban} kg.\n" +
                              $"3. Untuk berat tubuh (m<size=12>Body</size>) operator pada simulasi praktikum, IE Lab tetapkan seberat 91 kg.";
        }

        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                lecturerTransform.gameObject.SetActive(true);  // Unhide the Lecturer object

                // Move the Lecturer based on the index
                if (index >= 5 && index <= 10) 
                {
                    Vector3 worldPosition = labAntroClone.transform.TransformPoint(new Vector3(0.33f, -0.36f, -4.880189f));
                    Vector3 worldRotation = labAntroClone.transform.TransformDirection(Quaternion.Euler(0f, 142.331f, 0f).eulerAngles);

                    lecturerTransform.position = worldPosition;
                    lecturerTransform.rotation = Quaternion.Euler(worldRotation);
                }
                else if (index >= 11 && index <= 12)
                {
                    Vector3 worldPosition = labAntroClone.transform.TransformPoint(new Vector3(-0.502f, -0.36f, -4.880189f));
                    Vector3 worldRotation = labAntroClone.transform.TransformDirection(Quaternion.Euler(0f, 142.331f, 0f).eulerAngles);

                    lecturerTransform.position = worldPosition;
                    lecturerTransform.rotation = Quaternion.Euler(worldRotation);
                }
            }
            else
            {
                Debug.LogError("Lecturer object not found under LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogError("LabAntro(Clone) object not found in the scene.");
        }

        if (index == 0)
        {
            prevConversationAlmostFinishPanelButton.gameObject.SetActive(true);  // show Prev button on first panel
        }
        else
        {
            prevConversationAlmostFinishPanelButton.gameObject.SetActive(true);   // Show Prev button on all other panels
        }

        // Manage visibility of Next and Close buttons based on the panel index
        if (index < ConversationAlmostFinishPanel.Length - 1)  // If not at the last panel, show Next button
        {
            nextConversationAlmostFinishPanelButton.gameObject.SetActive(true);
            closeConversationAlmostFinishPanelButton.gameObject.SetActive(false);
        }
        else  // If it's the last panel, show Close button
        {
            nextConversationAlmostFinishPanelButton.gameObject.SetActive(false);
            closeConversationAlmostFinishPanelButton.gameObject.SetActive(true);
        }

        if (index == 3) 
        {

            DownloadButton2.gameObject.SetActive(true);
            nextConversationAlmostFinishPanelButton.gameObject.SetActive(false);
        }
        else
        {
            DownloadButton2.gameObject.SetActive(false); 
        }
    }

    public void NextConversationAlmostFinishPanel()
    {
        if (almostFinishPanelIndex < ConversationAlmostFinishPanel.Length - 1)
        {
            almostFinishPanelIndex++;
            ShowAlmostFinishPanel(almostFinishPanelIndex);
        }
    }

    void PrevConversationAlmostFinishPanels()
    {
        if (almostFinishPanelIndex == 0) // On the first panel of second sequence
        {
            // Close current panels (almostFinishPanelIndex)
            foreach (GameObject panel in ConversationAlmostFinishPanel)
            {
                panel.SetActive(false);
            }

            // Show the first panel of the first sequence (ConversationAlmostFinishPanel)
            currentClickedNoPanelIndex = 0; // Reset to the first panel
            ShowClickedNoPanel(currentClickedNoPanelIndex);

            // Hide buttons for the second conversation sequence
            nextConversationAlmostFinishPanelButton.gameObject.SetActive(false); // Hide "Previous" button
            prevConversationAlmostFinishPanelButton.gameObject.SetActive(false); // Hide "Next" button
        }
        else
        {
            // For all other panels, navigate to the previous one in conversationAfterTheFirstSimulation2Panels
            if (almostFinishPanelIndex > 0)
            {
                almostFinishPanelIndex--;
                ShowAlmostFinishPanel(almostFinishPanelIndex);
            }
        }
    }

    public void CloseConversationAlmostFinishPanel()
    {
        // Hide all panels in the AlmostFinishPanel array
        foreach (GameObject panel in ConversationAlmostFinishPanel)
        {
            panel.SetActive(false);
        }

        closeConversationAlmostFinishPanelButton.gameObject.SetActive(false);
        prevConversationAlmostFinishPanelButton.gameObject.SetActive(false);
        ShowFinishPanel(currentFinishPanelIndex);
    }

    void OnStartModuleButtonClicked()
    {
        // Find the LabAntro(Clone) object
        GameObject labAntro = GameObject.Find("LabAntro(Clone)");

        if (labAntro != null)
        {
            // Find the Lecturer object inside LabAntro(Clone)
            Transform lecturerTransform = labAntro.transform.Find("Lecturer");

            if (lecturerTransform != null)
            {
                // Hide the Lecturer object by setting its active state to false
                lecturerTransform.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Lecturer object not found inside LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogError("LabAntro(Clone) object not found.");
        }
    }

    void OpenConversationBeforeSecondSimulationPanels()
    {
        conversationAfterTheFirstSimulation2PanelIndex = 0;
        ShowAfterSimulation2Panel(conversationAfterTheFirstSimulation2PanelIndex);
    }

    void CheckAnimationStatus()
    {
        // Ensure all animators are assigned and the Replay Panel is not already shown
        if ((targetAnimator != null && targetAnimator2 != null && targetAnimator3 != null) && !isReplayPanelShown)
        {
            // Get the current state of each animator
            AnimatorStateInfo animState1 = targetAnimator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo animState2 = targetAnimator2.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo animState3 = targetAnimator3.GetCurrentAnimatorStateInfo(0);

            // Check if all animations are finished
            if (animState1.normalizedTime >= 1 && !targetAnimator.IsInTransition(0) &&
                animState2.normalizedTime >= 1 && !targetAnimator2.IsInTransition(0) &&
                animState3.normalizedTime >= 1 && !targetAnimator3.IsInTransition(0))
            {
                // Increment the repitisi count after all animations are complete
                currentRepitisiCount++;

                if (currentRepitisiCount < savedRepitisi)
                {
                    // Restart all animations
                    targetAnimator.Play(animState1.fullPathHash, -1, 0);
                    targetAnimator2.Play(animState2.fullPathHash, -1, 0);
                    targetAnimator3.Play(animState3.fullPathHash, -1, 0);
                }
                else
                {
                    // Show the Replay Panel after all repetitions are done
                    StartCoroutine(ShowReplayPanelWithDelay(2.0f));
                    isReplayPanelShown = true;
                }
            }
        }
    }

    IEnumerator ShowReplayPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReplayPanel.SetActive(true);
    }

    // Called when the Yes button is clicked
    void OnYesClicked()
    {
        ReplayPanel.SetActive(false); // Close ReplayPanel
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab); // Destroy the current prefab
        }

        SpawnPrefab(); // Spawn a new one
        currentRepitisiCount = 0; // Reset the counter
    }

    // Called when the No button is clicked
    void OnNoClicked()
    {
        ReplayPanel.SetActive(false);  // Close ReplayPanel
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);  // Destroy the current prefab
        }

        currentconversationAfterTheFirstSimulationPanelIndex = 0;  // Reset to the first panel
        ShowConversationAfterTheFirstSimulationPanels(currentconversationAfterTheFirstSimulationPanelIndex);
    }
    void ValidateNumberInput(string input)
    {
        // Optionally add additional logic if you want to handle special cases
        // This function ensures that only numbers are entered.
    }
    void ShowPanel(int index)
    {
        foreach (GameObject panel in conversationAwalPanels)
        {
            panel.SetActive(false);
        }

        if (index < conversationAwalPanels.Length)
        {
            conversationAwalPanels[index].SetActive(true);
        }

        if (index == 0) // First panel
        {
            prevStartconversationAwalButton.gameObject.SetActive(false); // Hide the Previous button
        }
        else
        {
            prevStartconversationAwalButton.gameObject.SetActive(true); // Show the Previous button
        }

        if (index == conversationAwalPanels.Length - 1)
        {
            nextStartconversationAwalButton.gameObject.SetActive(false);
            closeStartconversationAwalButton.gameObject.SetActive(true);
        }
        else
        {
            nextStartconversationAwalButton.gameObject.SetActive(true);
            closeStartconversationAwalButton.gameObject.SetActive(false);
        }
    }

    void NextPanel()
    {
        if (currentPanelIndex < conversationAwalPanels.Length - 1)
        {
            currentPanelIndex++;
            ShowPanel(currentPanelIndex);
        }
    }

    void PrevPanel()
    {
        if (currentPanelIndex > 0) // Ensure we don't go below 0
        {
            currentPanelIndex--;
            ShowPanel(currentPanelIndex);
        }
    }

    void CloseConversation()
    {
        foreach (GameObject panel in conversationAwalPanels)
        {
            panel.SetActive(false);
        }

        closeStartconversationAwalButton.gameObject.SetActive(false);
        prevStartconversationAwalButton.gameObject.SetActive(false);

        inputPanel.SetActive(true);
    }

    void SubmitData()
    {
        bool isBeratBebanInvalid = false;
        bool isWaktuInvalid = false;

        // Validate Berat Beban
        if (!int.TryParse(beratBebanInput.text, out int beratBeban) || beratBeban < 1 || beratBeban > 35)
        {
            isBeratBebanInvalid = true;
            beratBebanInput.text = "";  // Clear the input field
        }

        // Validate Waktu
        if (!int.TryParse(waktuInput.text, out int waktu) || waktu < 1 || waktu > 480)
        {
            isWaktuInvalid = true;
            waktuInput.text = "";  // Clear the input field
        }

        // Show appropriate warning panel
        if (isBeratBebanInvalid && isWaktuInvalid)
        {
            ShowWarningPanel(BeratBebanWaktuWarning);
        }
        else if (isBeratBebanInvalid)
        {
            ShowWarningPanel(BeratBebanWarning);
        }
        else if (isWaktuInvalid)
        {
            ShowWarningPanel(WaktuWarning);
        }
        else
        {
            savedBeratBeban = beratBeban;
            savedWaktu = waktu;
            savedRepitisi = GetRepitisi(savedBeratBeban);
            savedFM = CalculateFM(beratBeban, waktu, V);

            // Send data to SoalModul3 script
            SoalModul3 soalModul3 = FindObjectOfType<SoalModul3>(); // Get reference to SoalModul3 script
            if (soalModul3 != null)
            {
                soalModul3.SetData(savedBeratBeban, savedWaktu, savedRepitisi, savedFM); // Pass the data
            }
            else
            {
                Debug.LogError("SoalModul3 script not found!");
            }

            // If inputs are valid, proceed with calculations
            int repitisi = GetRepitisi(beratBeban);

            FMResultText.text = $"Pada simulasi yang telah anda amati, total pengangkatan adalah sebanyak {repitisi} kali dalam 1 menit dan waktu kerja yang anda input adalah {waktu} menit. Dari informasi ini dapat kita kaitkan dengan tabel diatas dan dapat kita simpulkan bahwa nilai <i>Frequency Multiplier</i> (FM) adalah {savedFM}.";

            UpdateAssumptionText(waktu, beratBeban);

            inputPanel.SetActive(false);
            ShowAfterSelectingPanel(currentAfterSelectingPanelIndex);
        }
    }

    void ShowWarningPanel(GameObject warningPanel)
    {
        if (warningPanel != null)
        {
            warningPanel.SetActive(true);
            StartCoroutine(AutoCloseWarningPanel(warningPanel, 3f));
        }
    }

    IEnumerator AutoCloseWarningPanel(GameObject warningPanel, float delay)
    {
        yield return new WaitForSeconds(delay);
        warningPanel.SetActive(false);
    }

    // Function to calculate repitisi based on berat beban
    int GetRepitisi(int beratBeban)
    {
        return beratBeban switch
        {
            <= 5 => 30,
            <= 10 => 25,
            <= 15 => 20,
            <= 20 => 20,
            <= 25 => 15,
            <= 30 => 10,
            <= 35 => 5,
            _ => 0
        };
    }

    // Function to calculate Frequency Multiplier (FM)
    float CalculateFM(int beratBeban, int waktu, float V)
    {
        // Update the logic to check if V ≥ 30
        bool isVGreaterThanOrEqual30 = V >= 30;

        // Conditions based on berat beban and waktu
        if (beratBeban >= 1 && beratBeban <= 5)
        {
            return 0; // FM is 0 for all waktu values
        }
        else if (beratBeban >= 6 && beratBeban <= 10)
        {
            return 0; // FM is 0 for all waktu values
        }
        else if (beratBeban >= 11 && beratBeban <= 15)
        {
            return 0; // FM is 0 for all waktu values
        }
        else if (beratBeban >= 16 && beratBeban <= 20)
        {
            return 0; // FM is 0 for all waktu values
        }
        else if (beratBeban >= 21 && beratBeban <= 25)
        {
            if (waktu <= 60)
            {
                return 0.28f; // FM = 0.28 for Waktu ≤ 1 hour
            }
            return 0; // FM is 0 for other time ranges
        }
        else if (beratBeban >= 26 && beratBeban <= 30)
        {
            // FM values for Waktu ≤ 1 hour, > 1 hour but ≤ 2 hours, > 2 hours but ≤ 8 hours
            if (waktu <= 60)
            {
                return 0.45f; // FM = 0.45 for Waktu ≤ 1 hour
            }
            else if (waktu > 60 && waktu <= 120)
            {
                return 0.23f; // FM = 0.23 for Waktu > 1 hour but ≤ 2 hours
            }
            else if (waktu > 120 && waktu <= 480)
            {
                return 0.13f; // FM = 0.13 for Waktu > 2 hours but ≤ 8 hours
            }
            return 0; // FM is 0 for times greater than 8 hours
        }
        else if (beratBeban >= 31 && beratBeban <= 35)
        {
            // FM values for Waktu ≤ 1 hour, > 1 hour but ≤ 2 hours, > 2 hours but ≤ 8 hours
            if (waktu <= 60)
            {
                return 0.8f; // FM = 0.8 for Waktu ≤ 1 hour
            }
            else if (waktu > 60 && waktu <= 120)
            {
                return 0.6f; // FM = 0.6 for Waktu > 1 hour but ≤ 2 hours
            }
            else if (waktu > 120 && waktu <= 480)
            {
                return 0.35f; // FM = 0.35 for Waktu > 2 hours but ≤ 8 hours
            }
            return 0; // FM is 0 for times greater than 8 hours
        }

        return 0; // Default case: return 0 if beratBeban is not within valid ranges
    }


    void UpdateAssumptionText(int waktu, int beratBeban)
    {
        assumptionText.text = $"Namun sebelum IE Lab berikan datanya, perlu disampaikan bahwa dalam peragaan ini kita asumsikan bahwa terdapat seorang operator gudang. Dengan kondisi sebagai berikut:\n\n" +
                              $"1. Waktu kerja operator dalam pengangkatan beban adalah {waktu} menit.\n" +
                              $"2. Berat beban yang diangkat operator adalah {beratBeban} kg.\n" +
                              $"3. Operatornya berat tubuh operator IE Lab tetapkan seberat 91 kg.\n" +
                              $"4. Dikarenakan benda yang diangkat cukup besar, sulit dipegang dan memiliki sudut yang tajam, maka dapat kita simpulkan bahwa <i>Coupling Multiplier (CM)</i> berada pada kategori <i>poor</i>. Untuk detail tabel <i>Coupling Multiplier</i> dapat kamu lihat pada modul praktikum!";
    }


    IEnumerator FlashRed(TMP_InputField inputField, string fieldType)
    {

        Color originalColor = inputField.image.color;
        inputField.image.color = Color.red;  // Change the background color to red

        yield return new WaitForSeconds(0.5f);  // Wait for 0.5 seconds

        inputField.image.color = originalColor;  // Revert to the original color
    }

    void ShowAfterSelectingPanel(int index)
    {
        // Hide all panels in the ConversationAfterSelecting array
        foreach (GameObject panel in conversationAfterSelectingPanels)
        {
            panel.SetActive(false);
        }

        if (index < conversationAfterSelectingPanels.Length)
        {
            conversationAfterSelectingPanels[index].SetActive(true);
        }

        if (index == 0) // First panel
        {
            prevConversationAfterSelectingButton.gameObject.SetActive(false); // Hide "Previous" button
        }
        else
        {
            prevConversationAfterSelectingButton.gameObject.SetActive(true); // Show "Previous" button
        }

        // Update button visibility for next and close buttons
        if (index == conversationAfterSelectingPanels.Length - 1)
        {
            nextConversationAfterSelectingButton.gameObject.SetActive(false);
            closeConversationAfterSelectingButton.gameObject.SetActive(true);
        }
        else
        {
            nextConversationAfterSelectingButton.gameObject.SetActive(true);
            closeConversationAfterSelectingButton.gameObject.SetActive(false);
        }
    }

    void NextPanelAfterSelecting()
    {
        if (currentAfterSelectingPanelIndex < conversationAfterSelectingPanels.Length - 1)
        {
            currentAfterSelectingPanelIndex++;
            ShowAfterSelectingPanel(currentAfterSelectingPanelIndex);
        }
    }

    void PrevPanelAfterSelecting()
    {
        if (currentAfterSelectingPanelIndex > 0) // Ensure we don't go below 0
        {
            currentAfterSelectingPanelIndex--;
            ShowAfterSelectingPanel(currentAfterSelectingPanelIndex);
        }
    }

    void CloseConversationAfterSelecting()
    {
        foreach (GameObject panel in conversationAfterSelectingPanels)
        {
            panel.SetActive(false);
        }

        closeConversationAfterSelectingButton.gameObject.SetActive(false);
        prevConversationAfterSelectingButton.gameObject.SetActive(false);

        // Show the StartModule button after closing the sequence
        startModuleButton.gameObject.SetActive(true);
    }

    void ShowConversationAfterTheFirstSimulationPanels(int index)
    {
        // Hide all panels in the ConversationAfterSimulation array
        foreach (GameObject panel in conversationAfterTheFirstSimulationPanels)
        {
            panel.SetActive(false);
        }

        // Find the LabAntro(Clone) object and then find the Lecturer object under it
        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                lecturerTransform.gameObject.SetActive(true);  // Unhide the Lecturer object
            }
            else
            {
                Debug.LogError("Lecturer object not found under LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogError("LabAntro(Clone) object not found in the scene.");
        }

        if (index < conversationAfterTheFirstSimulationPanels.Length)
        {
            conversationAfterTheFirstSimulationPanels[index].SetActive(true);
        }

        if (index == 0) // First panel
        {
            prevConversationAfterTheFirstSimulationButton.gameObject.SetActive(false); // Hide "Previous" button
        }
        else
        {
            prevConversationAfterTheFirstSimulationButton.gameObject.SetActive(true); // Show "Previous" button
        }

        // Update button visibility for next and close buttons
        if (index == conversationAfterTheFirstSimulationPanels.Length - 1)
        {
            nextConversationAfterTheFirstSimulationButton.gameObject.SetActive(false);
            closeConversationAfterTheFirstSimulationButton.gameObject.SetActive(true);
        }
        else
        {
            nextConversationAfterTheFirstSimulationButton.gameObject.SetActive(true);
            closeConversationAfterTheFirstSimulationButton.gameObject.SetActive(false);
        }

        prevConversationAfterTheFirstSimulation2Button.gameObject.SetActive(false); 
        nextConversationAfterTheFirstSimulation2Button.gameObject.SetActive(false);
    }

    void NextConversationAfterTheFirstSimulationPanels()
    {
        if (currentconversationAfterTheFirstSimulationPanelIndex < conversationAfterTheFirstSimulationPanels.Length - 1)
        {
            currentconversationAfterTheFirstSimulationPanelIndex++;
            ShowConversationAfterTheFirstSimulationPanels(currentconversationAfterTheFirstSimulationPanelIndex);
        }
    }

    void PrevConversationAfterTheFirstSimulationPanels()
    {
        if (currentconversationAfterTheFirstSimulationPanelIndex > 0) // Ensure we don't go below 0
        {
            currentconversationAfterTheFirstSimulationPanelIndex--;
            ShowConversationAfterTheFirstSimulationPanels(currentconversationAfterTheFirstSimulationPanelIndex);
        }
    }

    void CloseConversationAfterTheFirstSimulationPanels()
    {
        // Hide all panels in the ConversationAfterSimulation array
        foreach (GameObject panel in conversationAfterTheFirstSimulationPanels)
        {
            panel.SetActive(false);
        }

        closeConversationAfterTheFirstSimulationButton.gameObject.SetActive(false);
        prevConversationAfterTheFirstSimulationButton.gameObject.SetActive(false);

        // Find the LabAntro(Clone) object and then find the Lecturer object under it
        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                lecturerTransform.gameObject.SetActive(false);  // Hide the Lecturer object
            }
            else
            {
                Debug.LogError("Lecturer object not found under LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogError("LabAntro(Clone) object not found in the scene.");
        }


        // Instantiate NoAnimationPrefab when conversationAfterSimulationPanels is closed
        if (NoAnimationPrefab != null && spawnedNoAnimationPrefab == null)
        {
            spawnedNoAnimationPrefab = Instantiate(NoAnimationPrefab);
        }
        else if (NoAnimationPrefab == null)
        {
            Debug.LogError("NoAnimationPrefab is not assigned.");
        }
    }

    void ShowAfterSimulation2Panel(int index)
    {
        // Hide all panels in the ConversationAfterSimulation2 array
        foreach (GameObject panel in conversationAfterTheFirstSimulation2Panels)
        {
            panel.SetActive(false);
        }

        if (index < conversationAfterTheFirstSimulation2Panels.Length)
        {
            conversationAfterTheFirstSimulation2Panels[index].SetActive(true);
        }

        // Find the LabAntro(Clone) object and then find the Lecturer object under it
        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                lecturerTransform.gameObject.SetActive(true);

                // If it's the 3rd panel (index 2), move the Lecturer to the specified position and rotation
                if (index == 2)
                {
                    // Convert local position and rotation to world space.
                    Vector3 worldPosition = labAntroClone.transform.TransformPoint(new Vector3(-0.03f, -0.36f, -4.880189f));
                    Vector3 worldRotation = labAntroClone.transform.TransformDirection(Quaternion.Euler(0f, 142.331f, 0f).eulerAngles);

                    lecturerTransform.position = worldPosition;
                    lecturerTransform.rotation = Quaternion.Euler(worldRotation);

                }
            }
            else
            {
                Debug.LogError("Lecturer object not found under LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogError("LabAntro(Clone) object not found in the scene.");
        }

        if (index == 0) // First panel of the second sequence
        {
            prevConversationAfterTheFirstSimulation2Button.gameObject.SetActive(true); // Show "Previous" button
        }
        else
        {
            prevConversationAfterTheFirstSimulation2Button.gameObject.SetActive(true); // Show "Previous" button
        }

        // Update button visibility for next and close buttons
        if (index == conversationAfterTheFirstSimulation2Panels.Length - 1)
        {
            nextConversationAfterTheFirstSimulation2Button.gameObject.SetActive(false);
            closeConversationAfterTheFirstSimulation2Button.gameObject.SetActive(true);
        }
        else
        {
            nextConversationAfterTheFirstSimulation2Button.gameObject.SetActive(true);
            closeConversationAfterTheFirstSimulation2Button.gameObject.SetActive(false);
        }
    }

    void NextConversationAfterTheFirstSimulation2Panels()
    {
        if (conversationAfterTheFirstSimulation2PanelIndex < conversationAfterTheFirstSimulation2Panels.Length - 1)
        {
            conversationAfterTheFirstSimulation2PanelIndex++;
            ShowAfterSimulation2Panel(conversationAfterTheFirstSimulation2PanelIndex);
        }
    }

    void PrevConversationAfterTheFirstSimulation2Panels()
    {
        if (conversationAfterTheFirstSimulation2PanelIndex == 0) // On the first panel of second sequence
        {
            // Close current panels (conversationAfterTheFirstSimulation2Panels)
            foreach (GameObject panel in conversationAfterTheFirstSimulation2Panels)
            {
                panel.SetActive(false);
            }

            // Show the first panel of the first sequence (conversationAfterTheFirstSimulationPanels)
            currentconversationAfterTheFirstSimulationPanelIndex = 0; // Reset to the first panel
            ShowConversationAfterTheFirstSimulationPanels(currentconversationAfterTheFirstSimulationPanelIndex);

            // Hide buttons for the second conversation sequence
            prevConversationAfterTheFirstSimulation2Button.gameObject.SetActive(false); // Hide "Previous" button
            nextConversationAfterTheFirstSimulation2Button.gameObject.SetActive(false); // Hide "Next" button
        }
        else
        {
            // For all other panels, navigate to the previous one in conversationAfterTheFirstSimulation2Panels
            if (conversationAfterTheFirstSimulation2PanelIndex > 0)
            {
                conversationAfterTheFirstSimulation2PanelIndex--;
                ShowAfterSimulation2Panel(conversationAfterTheFirstSimulation2PanelIndex);
            }
        }
    }

    void CloseConversationAfterTheFirstSimulation2Panels()
    {
        foreach (GameObject panel in conversationAfterTheFirstSimulation2Panels)
        {
            panel.SetActive(false);
        }

        closeConversationAfterTheFirstSimulation2Button.gameObject.SetActive(false);
        prevConversationAfterTheFirstSimulation2Button.gameObject.SetActive(false);

        // Find the LabAntro(Clone) object and then find the Lecturer object under it
        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                lecturerTransform.gameObject.SetActive(false);  // Hide the Lecturer object
            }
            else
            {
                Debug.LogError("Lecturer object not found under LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogError("LabAntro(Clone) object not found in the scene.");
        }

        // Start showing ConversationAlmostFinish panels
        ConversationBeforeSecondSimulationPanelsIndex = 0;
        ShowConversationBeforeSecondSimulationPanels(ConversationBeforeSecondSimulationPanelsIndex);
    }

    void ShowConversationBeforeSecondSimulationPanels(int index)
    {
        // Hide all panels in the ConversationAlmostFinish array
        foreach (GameObject panel in ConversationBeforeSecondSimulationPanels)
        {
            panel.SetActive(false);
        }

        // Show the corresponding panel
        if (index < ConversationBeforeSecondSimulationPanels.Length)
        {
            ConversationBeforeSecondSimulationPanels[index].SetActive(true);
        }

        GameObject redBox = FindRedBox();
        if (redBox != null)
        {
            if (index == 1)
            {
                redBox.SetActive(true); // Show Red Box at index 1
            }
            else
            {
                redBox.SetActive(false); // Hide Red Box for other indices
            }
        }

        GameObject conversationCanvas = GameObject.Find("ConversationCanvas");
        if (conversationCanvas == null)
        {
            Debug.LogError("ConversationCanvas not found in the scene.");
            return; // Exit if ConversationCanvas is missing
        }

        // Locate ImageConversationBeforeSecondSimulation
        Transform imageConversationTransform = conversationCanvas.transform.Find("ImageConversationBeforeSecondSimulation");
        if (imageConversationTransform == null)
        {
            Debug.LogError("ImageConversationBeforeSecondSimulation not found in ConversationCanvas.");
            return; // Exit if ImageConversationBeforeSecondSimulation is missing
        }

        // Ensure ImageConversationBeforeSecondSimulation is active
        GameObject imageConversation = imageConversationTransform.gameObject;
        if (!imageConversation.activeSelf)
        {
            Debug.LogWarning("ImageConversationBeforeSecondSimulation is not active. Activating it now.");
            imageConversation.SetActive(true); // Activate the parent object
        }

        // Move the Red Box if the FM is 0.8
        if (Mathf.Approximately(savedFM, 0.8f))
        {
            MoveRedBox();
        }
        else if (Mathf.Approximately(savedFM, 0f))
        {
            MoveRedBox();
        }
        else if (Mathf.Approximately(savedFM, 0.28f))
        {
            MoveRedBox();
        }
        else if (Mathf.Approximately(savedFM, 0.45f))
        {
            MoveRedBox();
        }
        else if (Mathf.Approximately(savedFM, 0.23f))
        {
            MoveRedBox();
        }
        else if (Mathf.Approximately(savedFM, 0.13f))
        {
            MoveRedBox();
        }
        else if (Mathf.Approximately(savedFM, 0.8f))
        {
            MoveRedBox();
        }
        else if (Mathf.Approximately(savedFM, 0.6f))
        {
            MoveRedBox();
        }
        else if (Mathf.Approximately(savedFM, 0.35f))
        {
            MoveRedBox();
        }

        // Show the image for the first and second panels only
        if (panelImageConversationBeforeSecondSimulation != null)
        {
            if (index == 0 || index == 1)  // Show the image for the first and second panels
            {
                panelImageConversationBeforeSecondSimulation.SetActive(true);
            }
            else  // Hide the image for the third and subsequent panels
            {
                panelImageConversationBeforeSecondSimulation.SetActive(false);
            }
        }

        // Spawn the prefab if it's not already spawned and this is the first panel
        if (index == 0 && prefabToSpawn != null && spawnedPrefab == null)
        {
            spawnedPrefab = Instantiate(prefabToSpawn);
            spawnedPrefab.transform.position = new Vector3(1.49f, 1.05f, 7.678f); // Move to specified location
        }

        // Locate the Lecturer and update position/rotation based on the index
        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                Vector3 newPosition = lecturerTransform.position;
                Vector3 newRotation = lecturerTransform.rotation.eulerAngles;

                if (index == 2)
                {
                    // Index 2
                    newPosition = new Vector3(-0.03f, newPosition.y, newPosition.z);
                    newRotation = new Vector3(newRotation.x, 142.331f, newRotation.z);
                }
                else if (index >= 3 && index <= 10)
                {
                    // Index 3 to 10
                    newPosition = new Vector3(-0.502f, newPosition.y, newPosition.z);
                    newRotation = new Vector3(newRotation.x, 142.331f, newRotation.z);
                }
                else if (index >= 11 && index <= 13)
                {
                    // Index 11 to 13
                    newPosition = new Vector3(2.3097f, newPosition.y, newPosition.z);
                    newRotation = new Vector3(newRotation.x, -180f, newRotation.z);
                }
                else if (index >= 14 && index <= 15)
                {
                    // Index 14 to 15
                    newPosition = new Vector3(-0.03f, newPosition.y, newPosition.z);
                    newRotation = new Vector3(newRotation.x, 142.331f, newRotation.z);
                }
                else if (index >= 16 && index <= 17)
                {
                    // Index 16 to 17
                    newPosition = new Vector3(-0.716f, newPosition.y, newPosition.z);
                    newRotation = new Vector3(newRotation.x, 142.331f, newRotation.z);
                }
                else if (index >= 18 && index <= 19)
                {
                    // Index 18 to 19
                    newPosition = new Vector3(2.3097f, newPosition.y, newPosition.z);
                    newRotation = new Vector3(newRotation.x, -180f, newRotation.z);
                }

                // Apply the updated position and rotation
                lecturerTransform.position = newPosition;
                lecturerTransform.rotation = Quaternion.Euler(newRotation);
            }
            else
            {
                Debug.LogError("Lecturer object not found under LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogError("LabAntro(Clone) object not found in the scene.");
        }

        // Hide the Lecturer object for 1st and 2nd panels
        if (index == 0 || index == 1)
        {
            if (labAntroClone != null)
            {
                Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
                if (lecturerTransform != null)
                {
                    lecturerTransform.gameObject.SetActive(false); // Hide the Lecturer object
                }
                else
                {
                    Debug.LogError("Lecturer object not found under LabAntro(Clone).");
                }
            }
            else
            {
                Debug.LogError("LabAntro(Clone) object not found in the scene.");
            }
        }

        // If it's the third panel (index 2), hide the prefab and image
        if (index == 2)
        {
            // Hide the prefab if it's spawned
            if (spawnedPrefab != null)
            {
                Destroy(spawnedPrefab);
                spawnedPrefab = null;  // Clear reference
            }

            // Hide the image
            if (panelImageConversationBeforeSecondSimulation != null)
            {
                panelImageConversationBeforeSecondSimulation.SetActive(false); // Hide the image
            }

            if (labAntroClone != null)
            {
                Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
                if (lecturerTransform != null)
                {
                    lecturerTransform.gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogError("Lecturer object not found under LabAntro(Clone).");
                }
            }
            else
            {
                Debug.LogError("LabAntro(Clone) object not found in the scene.");
            }
        }

        // Show or hide images based on panel index
        if (index == 3)  // 4th panel (index 3)
        {
            if (ImageConversationBeforeSecondSimulation4and5 != null)
            {
                ImageConversationBeforeSecondSimulation4and5.SetActive(true); // Show image for panel 4 only
            }
        }
        else  // Hide image for all other panels
        {
            if (ImageConversationBeforeSecondSimulation4and5 != null)
            {
                ImageConversationBeforeSecondSimulation4and5.SetActive(false); // Hide image for all other panels
            }
        }

        // Show or hide images based on panel index
        if (index == 3 || index == 4)  // 4th and 5th panel (index 3 and 4)
        {
            if (ImageConversationBeforeSecondSimulation4and5 != null)
            {
                ImageConversationBeforeSecondSimulation4and5.SetActive(true); // Show image for panel 4 & 5
            }

            if (labAntroClone != null)
            {
                Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
                if (lecturerTransform != null)
                {
                    // Convert local position and rotation to world space.
                    Vector3 worldPosition = labAntroClone.transform.TransformPoint(new Vector3(-0.502f, -0.36f, -4.880189f));
                    Vector3 worldRotation = labAntroClone.transform.TransformDirection(Quaternion.Euler(0f, 142.331f, 0f).eulerAngles);

                    lecturerTransform.position = worldPosition;
                    lecturerTransform.rotation = Quaternion.Euler(worldRotation);
                }
                else
                {
                    Debug.LogError("Lecturer object not found under LabAntro(Clone).");
                }
            }
        }

        // Show or hide images based on panel index
        if (index == 5 || index == 6 || index == 7 || index == 8 || index == 9 || index == 10)  // 6th to 11th panels (index 5-10)
        {
            if (ImageConversationBeforeSecondSimulation2 != null)
            {
                ImageConversationBeforeSecondSimulation2.SetActive(true); // Show image for panel 6 to 11
            }
        }
        else  // Hide image for all other panels
        {
            if (ImageConversationBeforeSecondSimulation2 != null)
            {
                ImageConversationBeforeSecondSimulation2.SetActive(false); // Hide image for all other panels
            }
        }

        if (index == 5)  // 6th panel (index 5)
        {
            if (ImageConversationBeforeSecondSimulation4and5 != null)
            {
                ImageConversationBeforeSecondSimulation4and5.SetActive(false); // Hide image for panel 4 & 5
            }
        }

        if (index == 5 || index == 6 || index == 7 || index == 8 || index == 9 || index == 10)  // 6th panel (index 5)
        {
            if (ImageConversationBeforeSecondSimulation2 != null)
            {
                ImageConversationBeforeSecondSimulation2.SetActive(true); // Show image for panel 6
            }
        }

        if (index == 11)  // 12th panel (index 11)
        {
            if (ImageConversationBeforeSecondSimulation2 != null)
            {
                ImageConversationBeforeSecondSimulation2.SetActive(false); // Hide image for panel 12
            }

            if (labAntroClone != null)
            {
                Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
                if (lecturerTransform != null)
                {
                    lecturerTransform.position = originalPosition;
                    lecturerTransform.rotation = originalRotation;
                }
                else
                {
                    Debug.LogError("Lecturer object not found under LabAntro(Clone).");
                }
            }
        }

        if (index == 14)  // 17th panel (index 16)
        {
            if (labAntroClone != null)
            {
                Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
                if (lecturerTransform != null)
                {
                    // Convert local position and rotation to world space.
                    Vector3 worldPosition = labAntroClone.transform.TransformPoint(new Vector3(-0.03f, -0.36f, -4.880189f));
                    Vector3 worldRotation = labAntroClone.transform.TransformDirection(Quaternion.Euler(0f, 142.331f, 0f).eulerAngles);

                    lecturerTransform.position = worldPosition;
                    lecturerTransform.rotation = Quaternion.Euler(worldRotation);
                }
                else
                {
                    Debug.LogError("Lecturer object not found under LabAntro(Clone).");
                }
            }
        }

        // Show or hide images based on panel index
        if (index == 16 || index == 17 || index == 18)  
        {
            if (ImageConversationBeforeSecondSimulation17and18 != null)
            {
                ImageConversationBeforeSecondSimulation17and18.SetActive(true); 
            }
        }
        else  // Hide image for all other panels
        {
            if (ImageConversationBeforeSecondSimulation17and18 != null)
            {
                ImageConversationBeforeSecondSimulation17and18.SetActive(false); 
            }
        }

        // Show or hide images based on panel index
        if (index == 16)  // 17th panel (index 16)
        {
            if (ImageConversationBeforeSecondSimulation17and18 != null)
            {
                ImageConversationBeforeSecondSimulation17and18.SetActive(true); // Show image for panel 17
            }

            if (labAntroClone != null)
            {
                Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
                if (lecturerTransform != null)
                {
                    // Convert local position and rotation to world space.
                    Vector3 worldPosition = labAntroClone.transform.TransformPoint(new Vector3(-0.716f, -0.36f, -4.880189f));
                    Vector3 worldRotation = labAntroClone.transform.TransformDirection(Quaternion.Euler(0f, 142.331f, 0f).eulerAngles);

                    lecturerTransform.position = worldPosition;
                    lecturerTransform.rotation = Quaternion.Euler(worldRotation);
                }
                else
                {
                    Debug.LogError("Lecturer object not found under LabAntro(Clone).");
                }
            }
        }

        if (index == 18)  // 19th panel (index 18)
        {
            if (ImageConversationBeforeSecondSimulation17and18 != null)
            {
                ImageConversationBeforeSecondSimulation17and18.SetActive(false); // Hide image for panel 19
            }

            if (labAntroClone != null)
            {
                Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
                if (lecturerTransform != null)
                {
                    lecturerTransform.position = originalPosition;
                    lecturerTransform.rotation = originalRotation;
                }
                else
                {
                    Debug.LogError("Lecturer object not found under LabAntro(Clone).");
                }
            }
        }

        if (index == 0)
        {
            prevConversationBeforeSecondSimulationButton.gameObject.SetActive(true);  // show Prev button on first panel
        }
        else
        {
            prevConversationBeforeSecondSimulationButton.gameObject.SetActive(true);   // Show Prev button on all other panels
        }

        if (index == ConversationBeforeSecondSimulationPanels.Length - 1)
        {
            nextConversationBeforeSecondSimulationButton.gameObject.SetActive(false);  // Hide the Next button
            closeConversationBeforeSecondSimulationButton.gameObject.SetActive(true); // Show the Close button
        }
        else
        {
            nextConversationBeforeSecondSimulationButton.gameObject.SetActive(true);  // Show the Next button
            closeConversationBeforeSecondSimulationButton.gameObject.SetActive(false); // Hide the Close button
        }

        // Reset the Download button visibility and Next button visibility
        if (index == 12) // 13th panel
        {
            DownloadButton1.gameObject.SetActive(true);
            nextConversationBeforeSecondSimulationButton.gameObject.SetActive(false);
        }
        else
        {
            DownloadButton1.gameObject.SetActive(false);  // Hide the Download button for all other panels
        }
    }

    void MoveRedBox()
    {
        GameObject conversationCanvas = GameObject.Find("ConversationCanvas");
        if (conversationCanvas == null)
        {
            Debug.LogError("ConversationCanvas not found in the scene.");
            return; // Exit if ConversationCanvas is missing
        }

        Transform imageConversationTransform = conversationCanvas.transform.Find("ImageConversationBeforeSecondSimulation");
        if (imageConversationTransform == null)
        {
            Debug.LogError("ImageConversationBeforeSecondSimulation not found in ConversationCanvas.");
            return; // Exit if ImageConversationBeforeSecondSimulation is missing
        }

        GameObject imageConversation = imageConversationTransform.gameObject;

        // Ensure the parent is active
        if (!imageConversation.activeSelf)
        {
            Debug.LogWarning("ImageConversationBeforeSecondSimulation is not active. Red Box will not be moved.");
            return; // Exit without moving Red Box
        }

        // Find the Red Box within the hierarchy
        Transform redBoxTransform = imageConversation.transform.Find("Red Box");
        if (redBoxTransform != null)
        {
            // Check FM and waktu conditions
            if (Mathf.Approximately(savedFM, 0) && savedWaktu <= 60)
            {
                redBoxTransform.localPosition = new Vector3(-63.2875f, -150.5f, redBoxTransform.localPosition.z);
            }
            else if (Mathf.Approximately(savedFM, 0) && savedWaktu > 60 && savedWaktu <= 120)
            {
                redBoxTransform.localPosition = new Vector3(58f, -150.5f, redBoxTransform.localPosition.z);
                redBoxTransform.localScale = new Vector3(1.2f, 1f, redBoxTransform.localScale.z);
            }
            else if (Mathf.Approximately(savedFM, 0) && savedWaktu > 120 && savedWaktu <= 480)
            {
                redBoxTransform.localPosition = new Vector3(185.5f, -150.5f, redBoxTransform.localPosition.z);
                redBoxTransform.localScale = new Vector3(1.2f, 1f, redBoxTransform.localScale.z);
            }
            else if (Mathf.Approximately(savedFM, 0.28f))
            {
                redBoxTransform.localPosition = new Vector3(-63.2f, -136.7f, redBoxTransform.localPosition.z);
            }
            else if (Mathf.Approximately(savedFM, 0) && savedBeratBeban >= 21 && savedBeratBeban <= 25)
            {
                if (savedWaktu > 60 && savedWaktu <= 120)
                {
                    redBoxTransform.localPosition = new Vector3(58f, -136.7f, redBoxTransform.localPosition.z);
                    redBoxTransform.localScale = new Vector3(1.2f, 1f, redBoxTransform.localScale.z);
                }
                else if (savedWaktu > 120 && savedWaktu <= 480)
                {
                    redBoxTransform.localPosition = new Vector3(185.5f, -136.7f, redBoxTransform.localPosition.z);
                    redBoxTransform.localScale = new Vector3(1.2f, 1f, redBoxTransform.localScale.z);
                }
            }
            else if (Mathf.Approximately(savedFM, 0.45f))
            {
                redBoxTransform.localPosition = new Vector3(-63.2875f, -64.2f, redBoxTransform.localPosition.z);
            }
            else if (Mathf.Approximately(savedFM, 0.23f))
            {
                redBoxTransform.localPosition = new Vector3(58f, -64.2f, redBoxTransform.localPosition.z);
                redBoxTransform.localScale = new Vector3(1.2f, 1f, redBoxTransform.localScale.z);
            }
            else if (Mathf.Approximately(savedFM, 0.13f))
            {
                redBoxTransform.localPosition = new Vector3(185.5f, -64.2f, redBoxTransform.localPosition.z);
                redBoxTransform.localScale = new Vector3(1.2f, 1f, redBoxTransform.localScale.z);
            }
            else if (Mathf.Approximately(savedFM, 0.8f))
            {
                redBoxTransform.localPosition = new Vector3(-63.2875f, 8.5f, redBoxTransform.localPosition.z);
            }
            else if (Mathf.Approximately(savedFM, 0.6f))
            {
                redBoxTransform.localPosition = new Vector3(58f, 8.5f, redBoxTransform.localPosition.z);
                redBoxTransform.localScale = new Vector3(1.2f, 1f, redBoxTransform.localScale.z);
            }
            else if (Mathf.Approximately(savedFM, 0.35f))
            {
                redBoxTransform.localPosition = new Vector3(185.5f, 8.5f, redBoxTransform.localPosition.z);
                redBoxTransform.localScale = new Vector3(1.2f, 1f, redBoxTransform.localScale.z);
            }
            else
            {
                redBoxTransform.localPosition = new Vector3(-63.2875f, 7.7248f, redBoxTransform.localPosition.z); // Default position
            }
        }
        else
        {
            Debug.LogError("Red Box not found under ImageConversationBeforeSecondSimulation.");
        }
    }


    void NextConversationBeforeSecondSimulationPanels()
    {
        if (ConversationBeforeSecondSimulationPanelsIndex < ConversationBeforeSecondSimulationPanels.Length - 1)
        {
            ConversationBeforeSecondSimulationPanelsIndex++;
            ShowConversationBeforeSecondSimulationPanels(ConversationBeforeSecondSimulationPanelsIndex);
        }
    }

    void PrevConversationBeforeSecondSimulationPanels()
    {
        if (ConversationBeforeSecondSimulationPanelsIndex == 0) // On the first panel of second sequence
        {
            // Close current panels (ConversationBeforeSecondSimulationPanels)
            foreach (GameObject panel in ConversationBeforeSecondSimulationPanels)
            {
                panel.SetActive(false);
            }

            // Hide the prefab if it's spawned
            if (spawnedPrefab != null)
            {
                spawnedPrefab.SetActive(false);  // Hide the prefab
            }

            // Hide the ImageConversationBeforeSecondSimulation if it's active
            if (panelImageConversationBeforeSecondSimulation != null)
            {
                panelImageConversationBeforeSecondSimulation.SetActive(false); // Hide the image
            }

            // Show the first panel of the first sequence (conversationAfterTheFirstSimulationPanels)
            conversationAfterTheFirstSimulation2PanelIndex = 0; // Reset to the first panel
            ShowAfterSimulation2Panel(conversationAfterTheFirstSimulation2PanelIndex);

            // Hide buttons for the second conversation sequence
            prevConversationBeforeSecondSimulationButton.gameObject.SetActive(false); // Hide "Previous" button
            nextConversationBeforeSecondSimulationButton.gameObject.SetActive(false); // Hide "Next" button
        }
        else
        {
            // For all other panels, navigate to the previous one in ConversationBeforeSecondSimulationPanels
            if (ConversationBeforeSecondSimulationPanelsIndex > 0)
            {
                ConversationBeforeSecondSimulationPanelsIndex--;
                ShowConversationBeforeSecondSimulationPanels(ConversationBeforeSecondSimulationPanelsIndex);
            }
        }
    }

    void CloseConversationBeforeSecondSimulationPanels()
    {
        foreach (GameObject panel in ConversationBeforeSecondSimulationPanels)
        {
            panel.SetActive(false);
        }

        closeConversationBeforeSecondSimulationButton.gameObject.SetActive(false);
        prevConversationBeforeSecondSimulationButton.gameObject.SetActive(false);

        // Hide the image when the panel is closed
        if (panelImageConversationBeforeSecondSimulation != null)
        {
            panelImageConversationBeforeSecondSimulation.SetActive(false); // Hide the image
        }

        if (ImageConversationBeforeSecondSimulation2 != null)
        {
            ImageConversationBeforeSecondSimulation2.SetActive(false);  // Hide image when closing the sequence
        }

        // Destroy the prefab when ConversationAlmostFinish closes
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);
            spawnedPrefab = null;  // Clear reference
        }

        ShowReplayPanel2();
    }

    void ShowReplayPanel2()
    {
        // Deactivate all other panels if needed (depending on your setup)
        // For example, if you have an array of other panels, you can hide them here.

        // Activate ReplayPanel2
        if (ReplayPanel2 != null)
        {
            ReplayPanel2.SetActive(true);
        }
        else
        {
            Debug.LogError("ReplayPanel2 not found.");
        }

        // Optionally, you can also manage buttons here for next steps, etc.
    }

    void ShowFinishPanel(int index)
    {
        // Hide all panels in the ConversationFinish array
        foreach (GameObject panel in ConversationFinish)
        {
            panel.SetActive(false);
        }

        if (index < ConversationFinish.Length)
        {
            ConversationFinish[index].SetActive(true);
        }

        if (index == 0) // First panel
        {
            prevConversationFinishButton.gameObject.SetActive(true); // Hide the Previous button
        }
        else
        {
            prevConversationFinishButton.gameObject.SetActive(true); // Show the Previous button
        }

        if (index == ConversationFinish.Length - 1)
        {
            nextConversationFinishButton.gameObject.SetActive(false);
            closeConversationFinishButton.gameObject.SetActive(true);
        }
        else
        {
            nextConversationFinishButton.gameObject.SetActive(true);
            closeConversationFinishButton.gameObject.SetActive(false);
        }

        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                lecturerTransform.position = originalPosition;
                lecturerTransform.rotation = originalRotation;
            }
            else
            {
                Debug.LogError("Lecturer object not found under LabAntro(Clone).");
            }
        }

        soalButton.gameObject.SetActive(false);
    }

    // Method to go to the next ConversationFinish panel
    void NextPanelFinish()
    {
        if (currentFinishPanelIndex < ConversationFinish.Length - 1)
        {
            currentFinishPanelIndex++;
            ShowFinishPanel(currentFinishPanelIndex);
        }
    }

    void PrevConversationFinishPanels()
    {
        if (currentFinishPanelIndex == 0) // On the first panel of second sequence
        {
            // Close current panels (ConversationFinish)
            foreach (GameObject panel in ConversationFinish)
            {
                panel.SetActive(false);
            }

            // Show the first panel of the first sequence (conversationAfterTheFirstSimulationPanels)
            almostFinishPanelIndex = 0; // Reset to the first panel
            ShowAlmostFinishPanel(almostFinishPanelIndex);

            // Hide buttons for the second conversation sequence
            prevConversationFinishButton.gameObject.SetActive(false); // Hide "Previous" button
            nextConversationFinishButton.gameObject.SetActive(false); // Hide "Next" button
        }
        else
        {
            // For all other panels, navigate to the previous one in ConversationFinish
            if (currentFinishPanelIndex > 0)
            {
                currentFinishPanelIndex--;
                ShowFinishPanel(currentFinishPanelIndex);
            }
        }
    }

    // Method to close the ConversationFinish sequence
    void CloseConversationFinish()
    {
        foreach (GameObject panel in ConversationFinish)
        {
            panel.SetActive(false);
        }

        closeConversationFinishButton.gameObject.SetActive(false);
        prevConversationFinishButton.gameObject.SetActive(false);

        soalButton.gameObject.SetActive(true);
    }

    void OnSoalButtonClicked()
    {
        // Hide the Soal button when clicked
        soalButton.gameObject.SetActive(false);

        // Find the LabAntro(Clone) object and then find the Lecturer object under it
        GameObject labAntroClone = GameObject.Find("LabAntro(Clone)");
        if (labAntroClone != null)
        {
            Transform lecturerTransform = labAntroClone.transform.Find("Lecturer");
            if (lecturerTransform != null)
            {
                lecturerTransform.gameObject.SetActive(false);  // Hide the Lecturer object
            }
            else
            {
                Debug.LogError("Lecturer object not found under LabAntro(Clone).");
            }
        }
        else
        {
            Debug.LogError("LabAntro(Clone) object not found in the scene.");
        }

    }

    void SpawnPrefab()
    {
        startModuleButton.gameObject.SetActive(false);

        if (prefabToSpawn != null)
        {
            // Reset flag to allow ReplayPanel to show again for the new object
            isReplayPanelShown = false;
            currentRepitisiCount = 0;

            spawnedPrefab = Instantiate(prefabToSpawn);  // Spawns the prefab

            Transform animasiModul3Object = spawnedPrefab.transform.Find("Karakter");
            if (animasiModul3Object != null)
            {
                targetAnimator = animasiModul3Object.GetComponent<Animator>();
                if (targetAnimator == null)
                {
                    Debug.LogError("Animator component not found on AnimasiModul3 in the spawned prefab.");
                }
                else
                {
                    targetAnimator.Play(0, -1, 0);
                }
            }
            else
            {
                Debug.LogError("GameObject 'AnimasiModul3' not found in the spawned prefab.");
            }


            Transform animasiModul3Object2 = spawnedPrefab.transform.Find("Karakter/Meja");
            if (animasiModul3Object2 != null)
            {
                targetAnimator2 = animasiModul3Object2.GetComponent<Animator>();
                if (targetAnimator2 == null)
                {
                    Debug.LogError("Animator component not found on AnimasiModul3 Karakter2 in the spawned prefab.");
                }
                else
                {
                    targetAnimator2.Play(0, -1, 0);
                }
            }
            else
            {
                Debug.LogError("GameObject 'AnimasiModul3' not found in the spawned prefab.");
            }

            Transform animasiModul3Object3 = spawnedPrefab.transform.Find("Karakter/BebanAngkat");
            if (animasiModul3Object3 != null)
            {
                targetAnimator3 = animasiModul3Object3.GetComponent<Animator>();
                if (targetAnimator3 == null)
                {
                    Debug.LogError("Animator component not found on AnimasiModul3 Karakter3 in the spawned prefab.");
                }
                else
                {
                    targetAnimator3.Play(0, -1, 0);
                }
            }

            SetAnimationSpeed(savedRepitisi);

            Transform meja = spawnedPrefab.transform.Find("Karakter/Meja");
            Transform paletKayu = spawnedPrefab.transform.Find("Karakter/PaletKayu");
            SetAnimationSpeed(savedRepitisi);
        }
        else
        {
            Debug.LogError("Prefab is not assigned.");
        }
    }

    void SetAnimationSpeed(int repitisi)
    {
        float speed = 1.0f; // Default speed

        if (repitisi == 5)
        {
            speed = 0.5f;
        }
        else if (repitisi == 10)
        {
            speed = 1.05f;
        }
        else if (repitisi == 15)
        {
            speed = 1.6f;
        }
        else if (repitisi == 20)
        {
            speed = 2.1f;
        }
        else if (repitisi == 25)
        {
            speed = 2.7f;
        }
        else if (repitisi == 30)
        {
            speed = 3f;
        }

        // Apply speed to all animators
        if (targetAnimator != null) targetAnimator.speed = speed;
        if (targetAnimator2 != null) targetAnimator2.speed = speed;
        if (targetAnimator3 != null) targetAnimator3.speed = speed;
    }

    public string GenerateCSVFile()
    {
        // Example data for CSV
        string[] headers = { "No.", "Keterangan", "Nilai" };
        string[][] data = {
            new string[] { "1", "Hawal", "24 cm" },
            new string[] { "2", "Hakhir", "34 cm" },
            new string[] { "3", "Vawal", "75 cm" },
            new string[] { "4", "Vakhir", "13 cm" },
            new string[] { "5", "Frequency", savedRepitisi.ToString() + " Kali" },
            new string[] { "6", "Weight", savedBeratBeban.ToString() + " kg" },
            new string[] { "7", "LC", "23 kg" },
            new string[] { "8", "Waktu", savedWaktu.ToString() + " menit" },
            new string[] { "9", "Aawal", "0 derajat" },
            new string[] { "10", "Aakhir", "45 derajat" },
            new string[] { "11", "FM", savedFM.ToString() }
        };

        // Build the CSV content with semicolons as the delimiter
        System.Text.StringBuilder csv = new System.Text.StringBuilder();

        // Add headers
        csv.AppendLine(string.Join(";", headers));

        // Add data
        foreach (var row in data)
        {
            csv.AppendLine(string.Join(";", row));
        }

        return csv.ToString();
    }

    public void ExportCSV()
    {
        try
        {
            string csv = GenerateCSVFile();

            IDownloadFileCSVWebGLService downloadFileCSVWebGL = ServiceLocator.GetService<IDownloadFileCSVWebGLService>();
            downloadFileCSVWebGL.DownloadFileCSV("Data NIOSH.csv", csv);
        }
        catch
        {
            Debug.LogError("Only run in webgl");
        }
    }

    public void ExportCSV2()
    {
        try
        {
            string csv = GenerateCSVFile2();

            IDownloadFileCSVWebGLService downloadFileCSVWebGL = ServiceLocator.GetService<IDownloadFileCSVWebGLService>();
            downloadFileCSVWebGL.DownloadFileCSV("Data Compressive dan Shear Force.csv", csv);
        }
        catch
        {
            Debug.LogError("Only run in webgl");
        }
    }

    public string GenerateCSVFile2()
    {
        // Example data for CSV
        string[] headers = { "No.", "Keterangan", "Nilai" };
        string[][] data = {
            new string[] { "1", "Lload", "0.24 m" },
            new string[] { "2", "Lbody", "0.34 m" },
            new string[] { "3", "Mload", savedBeratBeban.ToString() + " kg" },
            new string[] { "4", "Mbody", "91 Kg" },
            new string[] { "5", "θ", "31 derajat" },
            new string[] { "6", "d", "0.05 m" },
            new string[] { "7", "g", "10 m/s2 " }
        };

        // Build the CSV content with semicolons as the delimiter
        System.Text.StringBuilder csv = new System.Text.StringBuilder();

        // Add headers
        csv.AppendLine(string.Join(";", headers));

        // Add data
        foreach (var row in data)
        {
            csv.AppendLine(string.Join(";", row));
        }

        return csv.ToString();
    }
}