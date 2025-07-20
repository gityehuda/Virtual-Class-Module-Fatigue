using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public string name;           // Optional name for the dialogue
    public GameObject[] panels;   // Array of panels for this dialogue
}

public class ConversationPanel : MonoBehaviour
{
    public static ConversationPanel instance;

    public Dialogue[] dialogues;     // Array of dialogues
    public Dialogue[] conversationAfterTutorial; // Array of dialogues after DummyAntro is destroyed
    public Dialogue[] conversationWhenKursiGuruAppears; // Array of dialogues when KursiGuru appears
    public Button closeButton;       // Reference to the Close button
    public Button nextButton;        // Reference to the Next button
    [SerializeField] public Button prevButton;        // Reference to the Previous button
    public Button openNextDialogueButton; // Reference to the button that opens the next dialogue

    public int currentDialogueIndex = 0;
    public int currentPanelIndex = 0;
    private bool dummyAntroAppeared = false;
    private bool dummyAntroDestroyed = false;
    private bool kursiGuruAppeared = false;

    public bool showFirstPanelOnStart = true;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        openNextDialogueButton.gameObject.SetActive(false); // Hide the open next dialogue button initially
        if (showFirstPanelOnStart) ShowFirstPanel();
    }

    void Update()
    {
        // Check if the DummyAntro object appears
        if (!dummyAntroAppeared && GameObject.Find("DummyAntro(Clone)") != null)
        {
            dummyAntroAppeared = true;
        }

        // Check if the DummyAntro object is destroyed after it appeared
        if (dummyAntroAppeared && !dummyAntroDestroyed && GameObject.Find("DummyAntro(Clone)") == null)
        {
            dummyAntroDestroyed = true;
            StartConversationAfterTutorial();
        }

        // Check if the KursiGuru object appears
        if (!kursiGuruAppeared && GameObject.Find("KursiGuru(Clone)") != null)
        {
            kursiGuruAppeared = true;
            StartConversationWhenKursiGuruAppears();
        }
    }

    public void ShowFirstPanel()
    {
        ShowPanel(currentDialogueIndex, currentPanelIndex);
    }

    public void ShowNextPanel()
    {
        if (currentPanelIndex < dialogues[currentDialogueIndex].panels.Length - 1)
        {
            // Move to the next panel within the same dialogue
            dialogues[currentDialogueIndex].panels[currentPanelIndex].SetActive(false);
            currentPanelIndex++;
            ShowPanel(currentDialogueIndex, currentPanelIndex);
        }
        else
        {
            // End of current dialogue
            dialogues[currentDialogueIndex].panels[currentPanelIndex].SetActive(false);

            // Show the Close button at the end of the dialogue
            closeButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false); // Hide the next button
            openNextDialogueButton.gameObject.SetActive(false); // Hide the open next dialogue button
        }
    }

    public void ShowPrevPanel()
    {
        if (currentPanelIndex > 0)
        {
            // Move to the previous panel within the same dialogue
            dialogues[currentDialogueIndex].panels[currentPanelIndex].SetActive(false);
            currentPanelIndex--;
            ShowPanel(currentDialogueIndex, currentPanelIndex);
        }
    }

    public void CloseCurrentPanel()
    {
        dialogues[currentDialogueIndex].panels[currentPanelIndex].SetActive(false);
        closeButton.gameObject.SetActive(false); // Hide the Close button when closing
        prevButton.gameObject.SetActive(false); // Hide the prev button when closing

        // Hide the Lecturer object if the current dialogue is from conversationAfterTutorial
        if (dialogues == conversationAfterTutorial)
        {
            GameObject labAntro = GameObject.Find("LabAntro(Clone)");
            if (labAntro != null)
            {
                Transform lecturerTransform = labAntro.transform.Find("Lecturer");
                if (lecturerTransform != null)
                {
                    lecturerTransform.gameObject.SetActive(false); // Hide the Lecturer
                    Debug.Log("Lecturer object hidden.");
                }
                else
                {
                    Debug.LogWarning("Lecturer object not found in LabAntro(Clone).");
                }
            }
            else
            {
                Debug.LogWarning("LabAntro(Clone) not found.");
            }
        }

        if (currentDialogueIndex < dialogues.Length - 1)
        {
            openNextDialogueButton.gameObject.SetActive(true); // Show the button to open the next dialogue if there are more dialogues
        }
    }

    public void OpenDialogue(int dialogueIndex)
    {
        if (dialogueIndex >= 0 && dialogueIndex < dialogues.Length)
        {
            // Hide current panel
            if (currentPanelIndex < dialogues[currentDialogueIndex].panels.Length)
            {
                dialogues[currentDialogueIndex].panels[currentPanelIndex].SetActive(false);
            }

            // Switch to the specified dialogue
            currentDialogueIndex = dialogueIndex;
            currentPanelIndex = 0;
            ShowPanel(currentDialogueIndex, currentPanelIndex);

            // Hide the button for opening the next dialogue
            openNextDialogueButton.gameObject.SetActive(false);
        }
    }

    private void ShowPanel(int dialogueIndex, int panelIndex)
    {
        if (dialogueIndex >= 0 && dialogueIndex < dialogues.Length &&
            panelIndex >= 0 && panelIndex < dialogues[dialogueIndex].panels.Length)
        {
            dialogues[dialogueIndex].panels[panelIndex].SetActive(true);
            // Show or hide buttons based on the current panel index
            bool isLastPanelInDialogue = panelIndex == dialogues[dialogueIndex].panels.Length - 1;
            nextButton.gameObject.SetActive(!isLastPanelInDialogue);
            closeButton.gameObject.SetActive(isLastPanelInDialogue);
            openNextDialogueButton.gameObject.SetActive(false);

            bool isFirstPanelInDialogue = panelIndex == 0;
            if (prevButton != null) prevButton.gameObject.SetActive(!isFirstPanelInDialogue);

            // Display the dialogue for the current panel (if you have Text components in the panels)
            Text dialogueText = dialogues[dialogueIndex].panels[panelIndex].GetComponentInChildren<Text>();
            if (dialogueText != null)
            {
                dialogueText.text = dialogues[dialogueIndex].name + " - Panel " + (panelIndex + 1);
            }
        }
    }

    public void OpenNextDialogue()
    {
        int nextDialogueIndex = currentDialogueIndex + 1;
        if (nextDialogueIndex < dialogues.Length)
        {
            OpenDialogue(nextDialogueIndex);
        }
        else
        {
            Debug.Log("All dialogues have been opened.");
            // Optionally, handle the case where all dialogues have been opened
            nextButton.gameObject.SetActive(false);
        }
    }

    private void StartConversationAfterTutorial()
    {
        if (conversationAfterTutorial.Length > 0)
        {
            dialogues = conversationAfterTutorial; // Switch dialogues to conversationAfterTutorial
            currentDialogueIndex = 0;
            currentPanelIndex = 0;
            ShowPanel(currentDialogueIndex, currentPanelIndex);

            // Find the Lecturer object inside LabAntro(Clone) and make it appear
            GameObject labAntro = GameObject.Find("LabAntro(Clone)");
            if (labAntro != null)
            {
                Transform lecturerTransform = labAntro.transform.Find("Lecturer");
                if (lecturerTransform != null)
                {
                    lecturerTransform.gameObject.SetActive(true);
                    Debug.Log("Lecturer object appeared.");
                }
                else
                {
                    Debug.LogWarning("Lecturer object not found in LabAntro(Clone).");
                }
            }
            else
            {
                Debug.LogWarning("LabAntro(Clone) not found.");
            }
        }
    }

    private void StartConversationWhenKursiGuruAppears()
    {
        if (conversationWhenKursiGuruAppears.Length > 0)
        {
            dialogues = conversationWhenKursiGuruAppears; // Switch dialogues to conversationWhenKursiGuruAppears
            currentDialogueIndex = 0;
            currentPanelIndex = 0;
            ShowPanel(currentDialogueIndex, currentPanelIndex);
        }
    }
}
