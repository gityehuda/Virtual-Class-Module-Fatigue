using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PanelsGroup
{
    public GameObject[] panels;
}

[System.Serializable]
public class TutorialConversation
{
    public GameObject PanelToRemove;           
    public GameObject[] ConversationTextPanels;  
    public GameObject RedBox;                  
    [HideInInspector] public int currentTextPanelIndex = 0; 
}

public class RecAllowance : MonoBehaviour
{
    // Array of panel groups for each step
    public PanelsGroup[] panelGroups;

    // Buttons for opening and navigating through panels
    public Button RAButton;
    public Button NextConversationButton;
    public Button CloseButton;
    public Button GoToSoalButton; 
    public Button CloseILOPanelButton; 
    public Button NextConversationTextPanelsButton;
    public Button NextConversationDataILOButton;

    // Panel that should appear when the last panel group is closed
    public GameObject ILOPanel;
    public GameObject DataILOPanel; 

    // Text fields in Data ILO Panel to display the allowances
    public TextMeshProUGUI constantAllowanceText;
    public TextMeshProUGUI variableAllowanceText;
    public TextMeshProUGUI totalAllowanceText;
    public TextMeshProUGUI totalAllowanceMessageText;

    // Array for tutorial panels, conversation text panels, and the RedBox panel
    public TutorialConversation[] TutorialConversationPanel;

    // New array for Conversation Data ILO panels
    public GameObject[] ConversationDataILO;

    // Internal tracking variables
    private int currentTutorialIndex = 0; 
    private int currentConversationDataILOIndex = 0; 

    // Toggles for different allowance categories (linked to UI toggles in the inspector)
    [Header("Abnormal Position Toggles")]
    public Toggle[] abnormalPositionToggles; // Abnormal position allowance toggles
    [Header("Bad Light Toggles")]
    public Toggle[] badLightToggles; // Bad light toggles
    [Header("Close Attention Toggles")]
    public Toggle[] closeAttentionToggles; // Close attention toggles
    [Header("Noise Level Toggles")]
    public Toggle[] noiseLevelToggles; // Noise level toggles
    [Header("Mental Strain Toggles")]
    public Toggle[] mentalStrainToggles; // Mental strain toggles
    [Header("Monotony Toggles")]
    public Toggle[] monotonyToggles; // Monotony toggles
    [Header("Tediousness Toggles")]
    public Toggle[] tediousnessToggles; // Tediousness toggles

    // Predefined values
    private float personalAllowance = 5.0f;
    private float basicFatigueAllowance = 4.0f;
    private float standingAllowance = 2.0f;
    private float atmosphericConditions = 5.83f;
    private float selectedUseOfForceValue = 0f;

    // Selected values for each category
    private float selectedAbnormalPositionValue = float.NaN;
    private float selectedBadLightValue = float.NaN;
    private float selectedCloseAttentionValue = float.NaN;
    private float selectedNoiseLevelValue = float.NaN;
    private float selectedMentalStrainValue = float.NaN;
    private float selectedMonotonyValue = float.NaN;
    private float selectedTediousnessValue = float.NaN;

    // Internal index to track the current group of panels
    private int currentPanelGroupIndex = 0;

    // Public properties to store calculated allowances
    public float ConstantAllowance { get; private set; }
    public float VariableAllowance { get; private set; }
    public float TotalAllowance { get; private set; }

    // Event to notify when calculation is done
    public delegate void AllowanceCalculated();
    public event AllowanceCalculated OnAllowanceCalculated;

    private Dictionary<Toggle[], List<Color>> toggleOriginalColors = new Dictionary<Toggle[], List<Color>>();

    public Transform contentParentNext2;
    public Transform contentParentNext4;
    private Transform originalParent; // To store the original parent of the button
    private Vector2 originalPosition; // To store the original position of the button


    private void Start()
    {
        originalParent = NextConversationTextPanelsButton.transform.parent;
        originalPosition = NextConversationTextPanelsButton.GetComponent<RectTransform>().anchoredPosition;

        // Initially hide all panels and buttons
        HideAllPanels();
        HideAllTutorialPanels();
        HideAllConversationDataILOPanels(); 
        DataILOPanel.SetActive(false); 
        CloseButton.gameObject.SetActive(false);
        ILOPanel.SetActive(false);
        NextConversationTextPanelsButton.gameObject.SetActive(false); 
        CloseILOPanelButton.gameObject.SetActive(false); 
        NextConversationDataILOButton.gameObject.SetActive(false); 

        // Add listeners to the buttons
        RAButton.onClick.AddListener(OpenFirstPanelGroup); 
        NextConversationButton.onClick.AddListener(OpenNextPanelGroup);
        CloseButton.onClick.AddListener(CloseLastPanel); 
        GoToSoalButton.gameObject.SetActive(false);
        GoToSoalButton.onClick.AddListener(HideGoToSoalButton); 
        CloseILOPanelButton.onClick.AddListener(CloseILOPanel);
        NextConversationDataILOButton.onClick.AddListener(ShowNextConversationDataILOPanel); 

        // Add listener to the NextConversationTextPanels button
        NextConversationTextPanelsButton.onClick.AddListener(ShowNextConversationTextPanel);

        // Setup toggle groups for each category
        SetupToggleGroup(abnormalPositionToggles, new float[] { 0f, 2f, 7f }, SetAbnormalPositionValue);
        SetupToggleGroup(badLightToggles, new float[] { 0f, 2f, 5f }, SetBadLightValue);
        SetupToggleGroup(closeAttentionToggles, new float[] { 0f, 2f, 5f }, SetCloseAttentionValue);
        SetupToggleGroup(noiseLevelToggles, new float[] { 0f, 2f, 5f, 5f }, SetNoiseLevelValue);
        SetupToggleGroup(mentalStrainToggles, new float[] { 1f, 4f, 8f }, SetMentalStrainValue);
        SetupToggleGroup(monotonyToggles, new float[] { 0f, 1f, 4f }, SetMonotonyValue);
        SetupToggleGroup(tediousnessToggles, new float[] { 0f, 2f, 5f }, SetTediousnessValue);
    }

    // Method to hide all panels in all panel groups
    private void HideAllPanels()
    {
        foreach (PanelsGroup group in panelGroups)
        {
            foreach (GameObject panel in group.panels)
            {
                panel.SetActive(false);
            }
        }
    }

    // Method to hide all tutorial panels
    private void HideAllTutorialPanels()
    {
        foreach (TutorialConversation tutorial in TutorialConversationPanel)
        {
            if (tutorial.PanelToRemove != null)
            {
                tutorial.PanelToRemove.SetActive(false);
            }

            if (tutorial.ConversationTextPanels != null)
            {
                foreach (GameObject textPanel in tutorial.ConversationTextPanels)
                {
                    textPanel.SetActive(false); // Hide each conversation text panel
                }
            }

            if (tutorial.RedBox != null)
            {
                tutorial.RedBox.SetActive(false); // Hide the RedBox
            }

            tutorial.currentTextPanelIndex = 0; // Reset the index for each tutorial conversation
        }
    }

    // Method to hide all Conversation Data ILO panels
    private void HideAllConversationDataILOPanels()
    {
        foreach (GameObject panel in ConversationDataILO)
        {
            if (panel != null)
            {
                panel.SetActive(false); // Hide each Conversation Data ILO panel
            }
        }
        currentConversationDataILOIndex = 0; // Reset index
    }

    // Method to open the first group of panels
    private void OpenFirstPanelGroup()
    {
        // Hide all panels initially
        HideAllPanels();

        // Reset the panel group index to 0
        currentPanelGroupIndex = 0;
        currentTutorialIndex = 0;

        // Open the first group of panels
        if (panelGroups.Length > 0)
        {
            OpenPanelGroup(currentPanelGroupIndex);
        }

        // Show the NextConversationButton to allow navigation to the next panel group
        NextConversationButton.gameObject.SetActive(true);
    }

    // Method to open the next group of panels in the array
    private void OpenNextPanelGroup()
    {
        // Hide the current group of panels
        HideCurrentPanelGroup();

        // Increment the panel group index to move to the next one
        currentPanelGroupIndex++;

        // Check if we've reached the last panel group
        if (currentPanelGroupIndex < panelGroups.Length)
        {
            // Open the next group of panels
            OpenPanelGroup(currentPanelGroupIndex);

            // If this is the last panel group in the array, swap the Next button with the Close button
            if (currentPanelGroupIndex == panelGroups.Length - 1)
            {
                // Hide the NextConversationButton and show the CloseButton
                NextConversationButton.gameObject.SetActive(false);
                CloseButton.gameObject.SetActive(true); // Show the close button for the last panel group
            }
        }
    }

    // Method to open a group of panels
    private void OpenPanelGroup(int index)
    {
        foreach (GameObject panel in panelGroups[index].panels)
        {
            panel.SetActive(true); // Show each panel in the group
        }
    }

    // Method to hide the current group of panels
    private void HideCurrentPanelGroup()
    {
        foreach (GameObject panel in panelGroups[currentPanelGroupIndex].panels)
        {
            panel.SetActive(false); // Hide each panel in the current group
        }
    }

    // Method to close the last panel group when the CloseButton is pressed
    private void CloseLastPanel()
    {
        // Hide the current (last) panel group and the CloseButton
        HideCurrentPanelGroup();
        CloseButton.gameObject.SetActive(false);

        // Activate the ILOPanel and open the tutorial panels
        if (ILOPanel != null)
        {
            ILOPanel.SetActive(true); // Show the ILOPanel
            OpenTutorialPanels(); // Open all tutorial conversation panels
        }
    }

    // Method to open tutorial panels, display conversation text panels, and the RedBox panel
    private void OpenTutorialPanels()
    {
        if (currentTutorialIndex < TutorialConversationPanel.Length)
        {
            TutorialConversation tutorial = TutorialConversationPanel[currentTutorialIndex];

            // Show the main panel if it exists
            if (tutorial.PanelToRemove != null)
            {
                tutorial.PanelToRemove.SetActive(true); // Show the panel when the tutorial starts
            }

            // Show the first conversation text panel and the RedBox
            if (tutorial.ConversationTextPanels != null && tutorial.ConversationTextPanels.Length > 0)
            {
                tutorial.ConversationTextPanels[tutorial.currentTextPanelIndex].SetActive(true);

                // Show the RedBox if available
                if (tutorial.RedBox != null)
                {
                    tutorial.RedBox.SetActive(true); // Show the RedBox alongside the conversation text panel
                }

                // Check if this is the second element of the TutorialConversationPanel (index 1)
                if (currentTutorialIndex == 1)
                {
                    // Move NextConversationTextPanelsButton to become a child of "ILO Next 2 -> Viewport -> Content"
                    NextConversationTextPanelsButton.transform.SetParent(contentParentNext2, false); // Set new parent

                    // After reparenting, set the new position (move pos y to -328)
                    RectTransform buttonRect = NextConversationTextPanelsButton.GetComponent<RectTransform>();
                    buttonRect.anchoredPosition = new Vector2(buttonRect.anchoredPosition.x, -328); // Set y to -328
                }
                // Reset the button when the third panel (Element 2) is opened
                else if (currentTutorialIndex == 2)
                {
                    // Move the button back to its original parent and position
                    NextConversationTextPanelsButton.transform.SetParent(originalParent, false); // Set back to original parent

                    // Reset the button's position to its original position
                    RectTransform buttonRect = NextConversationTextPanelsButton.GetComponent<RectTransform>();
                    buttonRect.anchoredPosition = originalPosition; // Reset to original position
                }
                // Move the button to ILO Next 4 -> Viewport -> Content for Element 3 (index 3)
                else if (currentTutorialIndex == 3)
                {
                    // Move the button to "ILO Next 4 -> Viewport -> Content"
                    NextConversationTextPanelsButton.transform.SetParent(contentParentNext4, false); // Set new parent

                    // After reparenting, set the new position (move pos y to -328)
                    RectTransform buttonRect = NextConversationTextPanelsButton.GetComponent<RectTransform>();
                    buttonRect.anchoredPosition = new Vector2(buttonRect.anchoredPosition.x, -283); 
                }
                // Reset the button when the fourth panel (Element 4) is opened
                else if (currentTutorialIndex == 4)
                {
                    // Move the button back to its original parent and position
                    NextConversationTextPanelsButton.transform.SetParent(originalParent, false); // Set back to original parent

                    // Reset the button's position to its original position
                    RectTransform buttonRect = NextConversationTextPanelsButton.GetComponent<RectTransform>();
                    buttonRect.anchoredPosition = originalPosition; // Reset to original position
                }

                // Ensure the button is only active when necessary
                NextConversationTextPanelsButton.gameObject.SetActive(true); // Enable the button to move to the next text panel
            }
        }
    }

    // Method to handle showing the next conversation text panel and managing the RedBox
    private void ShowNextConversationTextPanel()
    {
        if (currentTutorialIndex < TutorialConversationPanel.Length)
        {
            TutorialConversation tutorial = TutorialConversationPanel[currentTutorialIndex];

            // Check if the current text panel is the last one in the array
            bool isLastTextPanel = tutorial.currentTextPanelIndex >= tutorial.ConversationTextPanels.Length - 1;

            // Check if the required toggles are selected before allowing progression
            if (isLastTextPanel && !IsRequiredToggleSelectedForCurrentTutorial())
            {
                // If the required toggle for this tutorial conversation is not selected, flash the toggle's background image
                HighlightMissingToggleForCurrentTutorial();
                return; // Exit without progressing to the next panel
            }

            // Hide the current text panel
            if (tutorial.ConversationTextPanels != null && tutorial.currentTextPanelIndex < tutorial.ConversationTextPanels.Length)
            {
                tutorial.ConversationTextPanels[tutorial.currentTextPanelIndex].SetActive(false);

                // Move to the next text panel
                tutorial.currentTextPanelIndex++;

                if (tutorial.currentTextPanelIndex < tutorial.ConversationTextPanels.Length)
                {
                    // Show the next conversation text panel
                    tutorial.ConversationTextPanels[tutorial.currentTextPanelIndex].SetActive(true);

                    // Ensure the RedBox is still shown if available
                    if (tutorial.RedBox != null)
                    {
                        tutorial.RedBox.SetActive(true); // Keep RedBox active while conversation text panels are being shown
                    }
                }
                else
                {
                    // If it's the last text panel, remove the panel and move to the next tutorial conversation
                    tutorial.currentTextPanelIndex = 0; // Reset for the next time this conversation is visited

                    // Hide the PanelToRemove if it exists
                    if (tutorial.PanelToRemove != null)
                    {
                        tutorial.PanelToRemove.SetActive(false); // Hide the panel after the conversation finishes
                    }

                    // Hide the RedBox if it exists
                    if (tutorial.RedBox != null)
                    {
                        tutorial.RedBox.SetActive(false); // Hide the RedBox when the conversation finishes
                    }

                    NextConversationTextPanelsButton.gameObject.SetActive(false); // Hide the button

                    // Move to the next TutorialConversationPanel
                    currentTutorialIndex++;
                    if (currentTutorialIndex < TutorialConversationPanel.Length)
                    {
                        OpenTutorialPanels(); // Open the next tutorial panel
                    }
                    else
                    {
                        // Close everything if it's the last tutorial conversation panel
                        CloseLastPanel();
                    }
                }
            }
        }
    }

    // Method to check if the required toggle is selected for the current tutorial conversation
    private bool IsRequiredToggleSelectedForCurrentTutorial()
    {
        switch (currentTutorialIndex)
        {
            case 1:
                return !float.IsNaN(selectedAbnormalPositionValue); // Ensure Abnormal Position toggle is selected
            case 3:
                return !float.IsNaN(selectedBadLightValue); // Ensure Bad Light toggle is selected
            case 4:
                return !float.IsNaN(selectedCloseAttentionValue); // Ensure Close Attention toggle is selected
            case 5:
                return !float.IsNaN(selectedNoiseLevelValue); // Ensure Noise Level toggle is selected
            case 6:
                return !float.IsNaN(selectedMentalStrainValue); // Ensure Mental Strain toggle is selected
            case 7:
                return !float.IsNaN(selectedMonotonyValue); // Ensure Monotony toggle is selected
            case 8:
                return !float.IsNaN(selectedTediousnessValue); // Ensure Tediousness toggle is selected
            default:
                return true; // For other indices, no specific toggle selection is required
        }
    }

    private void HighlightMissingToggleForCurrentTutorial()
    {
        Toggle[] togglesToHighlight = null;

        // Determine which category of toggles to highlight based on the current tutorial index
        switch (currentTutorialIndex)
        {
            case 1:
                togglesToHighlight = abnormalPositionToggles;
                break;
            case 3:
                togglesToHighlight = badLightToggles;
                break;
            case 4:
                togglesToHighlight = closeAttentionToggles;
                break;
            case 5:
                togglesToHighlight = noiseLevelToggles;
                break;
            case 6:
                togglesToHighlight = mentalStrainToggles;
                break;
            case 7:
                togglesToHighlight = monotonyToggles;
                break;
            case 8:
                togglesToHighlight = tediousnessToggles;
                break;
        }

        // If a category of toggles was found, start the coroutine to flash their backgrounds
        if (togglesToHighlight != null)
        {
            StartCoroutine(FlashToggleBackgrounds(togglesToHighlight));
        }
    }

    // Coroutine to change the background of all toggles in a category to red and revert after 0.5 seconds
    private IEnumerator FlashToggleBackgrounds(Toggle[] toggles)
    {
        // Ensure original colors are stored if they haven't been already
        if (!toggleOriginalColors.ContainsKey(toggles))
        {
            List<Color> originalColors = new List<Color>();
            foreach (Toggle toggle in toggles)
            {
                if (toggle.targetGraphic != null)
                {
                    originalColors.Add(toggle.targetGraphic.color);
                }
            }
            toggleOriginalColors[toggles] = originalColors;
        }

        // Reset all toggles to their original colors before applying the red color
        ResetToggleColors(toggles);

        // Change the backgrounds to red
        foreach (Toggle toggle in toggles)
        {
            if (toggle.targetGraphic != null)
            {
                toggle.targetGraphic.color = Color.red;
            }
        }

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Revert the background colors to their original state
        ResetToggleColors(toggles);
    }

    // Method to reset toggles to their original colors
    private void ResetToggleColors(Toggle[] toggles)
    {
        if (toggleOriginalColors.ContainsKey(toggles))
        {
            List<Color> originalColors = toggleOriginalColors[toggles];
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].targetGraphic != null && i < originalColors.Count)
                {
                    toggles[i].targetGraphic.color = originalColors[i];
                }
            }
        }
    }

    // Method to get the first active toggle from a list
    private Toggle GetFirstActiveToggle(Toggle[] toggles)
    {
        foreach (Toggle toggle in toggles)
        {
            if (toggle != null)
            {
                return toggle; // Return the first toggle (to highlight its background)
            }
        }
        return null;
    }

    // Coroutine to change the toggle's background to red and revert after 0.5 seconds
    private IEnumerator FlashToggleBackground(Toggle toggle)
    {
        if (toggle.targetGraphic != null)
        {
            Color originalColor = toggle.targetGraphic.color; // Store the original color

            // Change the background color to red
            toggle.targetGraphic.color = Color.red;

            // Wait for 0.5 seconds
            yield return new WaitForSeconds(0.5f);

            // Revert to the original color
            toggle.targetGraphic.color = originalColor;
        }
    }

    // Method to handle closing the ILOPanel using the new CloseILOPanelButton (appears only when all categories are selected)
    private void CloseILOPanel()
    {
        if (ILOPanel != null && ILOPanel.activeSelf)
        {
            ILOPanel.SetActive(false); // Close the ILOPanel
            Debug.Log("ILOPanel closed");

            // Show Conversation Data ILO panels
            if (ConversationDataILO.Length > 0)
            {
                // Open the Data ILO Panel when ConversationDataILO opens
                DataILOPanel.SetActive(true);

                // Update and display allowance values in Data ILO Panel
                UpdateDataILOPanelText();

                ShowNextConversationDataILOPanel(); // Open the first Conversation Data ILO panel
                NextConversationDataILOButton.gameObject.SetActive(true); // Enable the Next button
            }
        }

        CloseILOPanelButton.gameObject.SetActive(false); // Hide the CloseILOPanel button after it is clicked
    }

    // Method to show the next Conversation Data ILO panel
    private void ShowNextConversationDataILOPanel()
    {
        // Hide the current panel
        if (currentConversationDataILOIndex < ConversationDataILO.Length)
        {
            if (ConversationDataILO[currentConversationDataILOIndex] != null)
            {
                ConversationDataILO[currentConversationDataILOIndex].SetActive(false);
            }

            // Move to the next panel
            currentConversationDataILOIndex++;

            if (currentConversationDataILOIndex < ConversationDataILO.Length)
            {
                // Show the next panel
                if (ConversationDataILO[currentConversationDataILOIndex] != null)
                {
                    ConversationDataILO[currentConversationDataILOIndex].SetActive(true);
                }
            }
            else
            {
                // If it's the last panel, hide everything and show GoToSoalButton
                NextConversationDataILOButton.gameObject.SetActive(false); // Hide the Next button

                // Hide Data ILO Panel when ConversationDataILO is finished
                DataILOPanel.SetActive(false);

                // Show GoToSoalButton after the last panel
                GoToSoalButton.gameObject.SetActive(true);
            }
        }
    }

    // Method to hide GoToSoalButton when clicked
    private void HideGoToSoalButton()
    {
        GoToSoalButton.gameObject.SetActive(false); // Hide the GoToSoalButton when clicked
        Debug.Log("GoToSoalButton hidden");
    }

    // Method to update and display the allowance values in Data ILO Panel
    private void UpdateDataILOPanelText()
    {
        constantAllowanceText.text = ConstantAllowance.ToString("F2") + "%";
        variableAllowanceText.text = VariableAllowance.ToString("F2") + "%";
        totalAllowanceText.text = TotalAllowance.ToString("F2") + "%";
        totalAllowanceMessageText.text = $"Berikut hasil <i>percentage allowance</i>. Nilai <i>total percentage allowances</i> sebesar <color=red>{TotalAllowance:F2}%</color> menunjukkan tambahan kelonggaran yang diberikan kepada anda ketika merakit LEGO Bulldozer sebanyak 10 kali agar mencerminkan kondisi kerja yang sesungguhnya, termasuk faktor-faktor yang dapat mempengaruhi produktivitas dan kenyamanan pekerja, seperti posisi postur kerja, kondisi tingkat pencahayaan, atmosfer, dan kebisingan, serta jenis dan kompleksitas pekerjaan. Nilai tersebut juga akan digunakan untuk perhitungan <i>allowance correction</i> dan <i>standard time</i>. Tabel ini bisa anda <i>download</i> bersama data yang lain setelah semua simulasi selesai.";
    }

    // Helper method to setup toggle group logic (only one toggle active per group)
    private void SetupToggleGroup(Toggle[] toggles, float[] values, System.Action<float> setValue)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            int index = i; // Capture the current index to avoid closure issues
            toggles[i].onValueChanged.AddListener((isSelected) =>
            {
                if (isSelected)
                {
                    // Set the selected value for the group
                    setValue(values[index]);
                    // Ensure all other toggles in the same category are deselected
                    DeselectOthers(toggles, index);
                }
                else
                {
                    if (!AnyToggleSelected(toggles))
                    {
                        setValue(float.NaN);
                    }
                }

                // Check all categories and update button visibility accordingly
                CheckAndCalculate();
            });
        }
    }

    // Check if any toggle in the group is selected
    private bool AnyToggleSelected(Toggle[] toggles)
    {
        foreach (var toggle in toggles)
        {
            if (toggle.isOn)
            {
                return true;
            }
        }
        return false;
    }


    // Deselect all other toggles in the same category
    private void DeselectOthers(Toggle[] toggles, int selectedIndex)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (i != selectedIndex)
            {
                toggles[i].isOn = false; // Deselect the other toggles
            }
        }
    }

    // Check if all categories have been selected and calculate the total allowance
    private void CheckAndCalculate()
    {
        // Check if all categories have been selected
        bool allCategoriesSelected = !float.IsNaN(selectedAbnormalPositionValue) &&
                                     !float.IsNaN(selectedUseOfForceValue) &&
                                     !float.IsNaN(selectedBadLightValue) &&
                                     !float.IsNaN(selectedCloseAttentionValue) &&
                                     !float.IsNaN(selectedNoiseLevelValue) &&
                                     !float.IsNaN(selectedMentalStrainValue) &&
                                     !float.IsNaN(selectedMonotonyValue) &&
                                     !float.IsNaN(selectedTediousnessValue);

        if (allCategoriesSelected)
        {
            // Calculate allowances
            ConstantAllowance = personalAllowance + basicFatigueAllowance;
            VariableAllowance = standingAllowance + atmosphericConditions + selectedAbnormalPositionValue +
                                selectedUseOfForceValue + selectedBadLightValue +
                                selectedCloseAttentionValue + selectedNoiseLevelValue +
                                selectedMentalStrainValue + selectedMonotonyValue + selectedTediousnessValue;
            TotalAllowance = ConstantAllowance + VariableAllowance;

            // Show the CloseILOPanelButton if all are selected
            CloseILOPanelButton.gameObject.SetActive(true);

            // Trigger the allowance calculation event
            OnAllowanceCalculated?.Invoke();
        }
        else
        {
            // Hide the CloseILOPanelButton if not all are selected
            CloseILOPanelButton.gameObject.SetActive(false);
        }
    }

    // Methods to set selected values for each category
    private void SetAbnormalPositionValue(float value) => selectedAbnormalPositionValue = value;
    private void SetUseOfForceValue(float value) => selectedUseOfForceValue = value;
    private void SetBadLightValue(float value) => selectedBadLightValue = value;
    private void SetCloseAttentionValue(float value) => selectedCloseAttentionValue = value;
    private void SetNoiseLevelValue(float value) => selectedNoiseLevelValue = value;
    private void SetMentalStrainValue(float value) => selectedMentalStrainValue = value;
    private void SetMonotonyValue(float value) => selectedMonotonyValue = value;
    private void SetTediousnessValue(float value) => selectedTediousnessValue = value;
}