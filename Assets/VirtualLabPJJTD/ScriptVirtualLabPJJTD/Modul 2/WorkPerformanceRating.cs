using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PanelGroup
{
    public GameObject[] panels; // Array to hold multiple panels for each step in the conversation
}

[System.Serializable]
public class WPRContentGroup
{
    public GameObject WPRContentPanel;  // The panel for WPR content
    public GameObject[] WPRTutorialPanels; // Array of tutorial panels
    public Vector3 originalPosition; // Store the original position
}

public class PerformanceData
{
    public string Class;
    public string Symbol;
    public string Rating;
}

public class WorkPerformanceRating : MonoBehaviour
{
    // Array of panel groups for each step
    public PanelGroup[] panelGroups; // Each element can contain multiple panels

    // Array of WPR content groups
    public WPRContentGroup[] wprContentGroups;

    // Array for conversation panels that will be shown after WPR data
    public GameObject[] conversationDataWpr;

    // Buttons for opening and navigating through panels
    public Button WPRButton;
    public Button NextConversationButton;
    public Button CloseButton; // Button to close the last panel
    public GameObject WPRPanel;
    public Button WPRDataButton; // Button to open WPR data panel
    public Button NextGameButton; // New Next Game Button
    public Button WPRNextTutorialButton; // Button to navigate between WPR tutorial panels
    public Button conversationDataWPRNext; // New button to navigate through conversation panels

    // New WPR Data Panel and its elements
    public GameObject WPRDataPanel;
    public TextMeshProUGUI skillClassText;
    public TextMeshProUGUI skillSymbolText;
    public TextMeshProUGUI skillRatingText;
    public TextMeshProUGUI effortClassText;
    public TextMeshProUGUI effortSymbolText;
    public TextMeshProUGUI effortRatingText;
    public TextMeshProUGUI conditionClassText;
    public TextMeshProUGUI conditionSymbolText;
    public TextMeshProUGUI conditionRatingText;
    public TextMeshProUGUI consistencyClassText;
    public TextMeshProUGUI consistencySymbolText;
    public TextMeshProUGUI consistencyRatingText;
    public TextMeshProUGUI totalScoreText;

    // References to the individual panels inside WPRPanel
    public GameObject SkillPanel;
    public GameObject EffortPanel;
    public GameObject ConditionPanel;
    public GameObject ConsistencyPanel;

    // Toggles for each category
    [Header("Skill Category Toggles")]
    public Toggle[] skillToggles;

    [Header("Effort Category Toggles")]
    public Toggle[] effortToggles;

    [Header("Condition Category Toggles")]
    public Toggle[] conditionToggles;

    [Header("Consistency Category Toggles")]
    public Toggle[] consistencyToggles;

    // Labels for each toggle
    private string[] skillLabels = { "A1", "A2", "B1", "B2", "C1", "C2", "D", "E1", "E2", "F1", "F2" };
    private string[] effortLabels = { "A1", "A2", "B1", "B2", "C1", "C2", "D", "E1", "E2", "F1", "F2" };
    private string[] conditionLabels = { "A", "B", "C", "D", "E", "F" };
    private string[] consistencyLabels = { "A", "B", "C", "D", "E", "F" };

    // Class names for each category
    private string[] skillClasses = { "Super Skill", "Super Skill", "Excellent", "Excellent", "Good", "Good", "Average", "Fair", "Fair", "Poor", "Poor" };
    private string[] effortClasses = { "Super Skill", "Super Skill", "Excellent", "Excellent", "Good", "Good", "Average", "Fair", "Fair", "Poor", "Poor" };
    private string[] conditionClasses = { "Ideal", "Excellent", "Good", "Average", "Fair", "Poor" };
    private string[] consistencyClasses = { "Ideal", "Excellent", "Good", "Average", "Fair", "Poor" };

    // To store the selected values
    private float[] skillValues;
    private float[] effortValues;
    private float[] conditionValues;
    private float[] consistencyValues;

    private int selectedSkillIndex;
    private int selectedEffortIndex;
    private int selectedConditionIndex;
    private int selectedConsistencyIndex;

    private float selectedSkillValue = float.NaN;
    private float selectedEffortValue = float.NaN;
    private float selectedConditionValue = float.NaN;
    private float selectedConsistencyValue = float.NaN;

    // Index to track the current WPR content group
    private int currentWPRContentGroupIndex = 0;
    private int currentConversationIndex = 0; // Track the current panel in conversationDataWpr

    // Boolean to track if Skill, Effort, Condition, or Consistency toggle is selected
    private bool isSkillSelected = false;
    private bool isEffortSelected = false;
    private bool isConditionSelected = false;
    private bool isConsistencySelected = false;

    // Reference to SoalModul2 script to pass data
    public SoalModul2 soalModul2;  // Drag the SoalModul2 script in the Inspector

    private int currentPanelGroupIndex = 0;
    private int currentTutorialPanelIndex = 0;

    public GameObject lecturerObject;

    public TextMeshProUGUI WPRDataText;
    public TextMeshProUGUI WPRDataText2;

    private void Start()
    {
        // Auto-input values for each category
        skillValues = new float[] { 0.15f, 0.13f, 0.11f, 0.08f, 0.06f, 0.03f, 0.00f, -0.05f, -0.10f, -0.16f, -0.22f };
        effortValues = new float[] { 0.13f, 0.12f, 0.10f, 0.08f, 0.05f, 0.02f, 0.00f, -0.04f, -0.08f, -0.12f, -0.17f };
        conditionValues = new float[] { 0.06f, 0.04f, 0.02f, 0.00f, -0.03f, -0.07f };
        consistencyValues = new float[] { 0.04f, 0.03f, 0.01f, 0.00f, -0.02f, -0.04f };

        // Initially hide all panels and buttons
        HideAllPanels();
        CloseButton.gameObject.SetActive(false);
        WPRDataButton.gameObject.SetActive(false); // Start hidden
        WPRDataPanel.SetActive(false); // Hide the WPR Data Panel
        conversationDataWPRNext.gameObject.SetActive(false); // Hide the conversationDataWPRNext button
        NextGameButton.gameObject.SetActive(false);

        // Add listeners to the buttons
        WPRButton.onClick.AddListener(OpenFirstPanelGroup);
        NextConversationButton.onClick.AddListener(OpenNextPanelGroup);
        CloseButton.onClick.AddListener(CloseLastPanel);

        NextGameButton.onClick.AddListener(() => {
            NextGameButton.gameObject.SetActive(false); // Hides the button after it's clicked
        });

        // Add functionality for the WPRDataButton (to display data and open the WPR Data panel)
        WPRDataButton.onClick.AddListener(() =>
        {
            if (WPRPanel != null && WPRPanel.activeSelf)
            {
                WPRPanel.SetActive(false); // Hides the WPRPanel when WPRDataButton is clicked
            }

            // Populate the WPR Data Panel with the performance data
            PopulateWPRData();

            // Show the WPR Data Panel
            WPRDataPanel.SetActive(true);

            WPRDataButton.gameObject.SetActive(false); // Hides the WPRDataButton itself after it's clicked

            // Open the first conversation data panel and show the Next button
            if (conversationDataWpr.Length > 0)
            {
                currentConversationIndex = 0;
                conversationDataWpr[currentConversationIndex].SetActive(true);
                conversationDataWPRNext.gameObject.SetActive(true); // Show the next button
            }
        });

        // Add listener to the conversationDataWPRNext button
        conversationDataWPRNext.onClick.AddListener(() =>
        {
            ShowNextConversationPanel();
        });

        // Add listeners to the skill toggles
        for (int i = 0; i < skillToggles.Length; i++)
        {
            int index = i;
            skillToggles[i].onValueChanged.AddListener((isSelected) =>
            {
                if (isSelected)
                {
                    SelectSkill(index);
                }
                else
                {
                    selectedSkillValue = float.NaN; // Reset the value if deselected
                }
                CheckAndCalculate(); // Always check after any toggle change
            });
        }

        // Add listeners to the effort toggles
        for (int i = 0; i < effortToggles.Length; i++)
        {
            int index = i;
            effortToggles[i].onValueChanged.AddListener((isSelected) =>
            {
                if (isSelected)
                {
                    SelectEffort(index);
                }
                else
                {
                    selectedEffortValue = float.NaN; // Reset the value if deselected
                }
                CheckAndCalculate(); // Always check after any toggle change
            });
        }

        // Add listeners to the condition toggles
        for (int i = 0; i < conditionToggles.Length; i++)
        {
            int index = i;
            conditionToggles[i].onValueChanged.AddListener((isSelected) =>
            {
                if (isSelected)
                {
                    SelectCondition(index);
                }
                else
                {
                    selectedConditionValue = float.NaN; // Reset the value if deselected
                }
                CheckAndCalculate(); // Always check after any toggle change
            });
        }

        // Add listeners to the consistency toggles
        for (int i = 0; i < consistencyToggles.Length; i++)
        {
            int index = i;
            consistencyToggles[i].onValueChanged.AddListener((isSelected) =>
            {
                if (isSelected)
                {
                    SelectConsistency(index);
                }
                else
                {
                    selectedConsistencyValue = float.NaN; // Reset the value if deselected
                }
                CheckAndCalculate(); // Always check after any toggle change
            });
        }
    }

    // Method to show the next conversation panel
    private void ShowNextConversationPanel()
    {
        // Hide the current conversation panel
        conversationDataWpr[currentConversationIndex].SetActive(false);

        // Increment to the next panel
        currentConversationIndex++;

        // Check if there are more panels in the array
        if (currentConversationIndex < conversationDataWpr.Length)
        {
            // Show the next conversation panel
            conversationDataWpr[currentConversationIndex].SetActive(true);
        }
        else
        {
            // Close the WPRDataPanel and hide the conversationDataWPRNext button
            WPRDataPanel.SetActive(false);
            conversationDataWPRNext.gameObject.SetActive(false);

            // Show the NextGameButton
            NextGameButton.gameObject.SetActive(true);
        }
    }

    // Method to populate WPR Data Panel when the WPRDataButton is clicked
    private void PopulateWPRData()
    {
        // Set the text fields in the WPR Data Panel with the relevant data
        skillClassText.text = skillClasses[selectedSkillIndex];
        skillSymbolText.text = skillLabels[selectedSkillIndex];
        skillRatingText.text = selectedSkillValue.ToString("F2");

        effortClassText.text = effortClasses[selectedEffortIndex];
        effortSymbolText.text = effortLabels[selectedEffortIndex];
        effortRatingText.text = selectedEffortValue.ToString("F2");

        conditionClassText.text = conditionClasses[selectedConditionIndex];
        conditionSymbolText.text = conditionLabels[selectedConditionIndex];
        conditionRatingText.text = selectedConditionValue.ToString("F2");

        consistencyClassText.text = consistencyClasses[selectedConsistencyIndex];
        consistencySymbolText.text = consistencyLabels[selectedConsistencyIndex];
        consistencyRatingText.text = selectedConsistencyValue.ToString("F2");

        // Calculate the total score and display it
        float totalScore = selectedSkillValue + selectedEffortValue + selectedConditionValue + selectedConsistencyValue;
        totalScoreText.text = totalScore.ToString("F2");

        // Initialize the combined message
        string combinedMessage = "Nilai yang sudah anda isi maka akan otomatis terekap dan menghasilkan Tabel Akhir <i>Work Performance Rating</i>.";

        // Skill
        if (skillClasses[selectedSkillIndex] == "Super Skill")
        {
            combinedMessage += " Pada faktor <i>skill</i>, anda mendapat <i>rating</i> sebesar <color=red>" + selectedSkillValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>super skill</color></i> dan menggambarkan <color=red>bahwa anda bekerja dengan sempurna, otomatis, tanpa perlu berpikir panjang, dengan kecepatan dan kelancaran yang sulit ditandingi. anda tidak pernah membuat kesalahan dan bekerja seperti mesin yang sangat terlatih</color>.";
            Debug.Log("Skill Super Skill message added.");
        }
        if (skillClasses[selectedSkillIndex] == "Excellent")
        {
            combinedMessage += " Pada faktor <i>skill</i>, anda mendapat <i>rating</i> sebesar <color=red>" + selectedSkillValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>excellent</color></i> dan menggambarkan <color=red>bahwa anda sangat teliti, percaya diri, menggunakan alat dengan baik, dan bekerja cepat namun tetap menjaga mutu tanpa perlu pengukuran atau pemeriksaan ulang</color>.";
            Debug.Log("Skill Excellent message added.");
        }
        if (skillClasses[selectedSkillIndex] == "Good")
        {
            combinedMessage += " Pada faktor <i>skill</i>, anda mendapat <i>rating</i> sebesar <color=red>" + selectedSkillValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>good</color></i> dan menggambarkan <color=red>bahwa kualitas kerja anda baik dan lebih unggul dari kebanyakan pekerja lainnya, meskipun belum seotomatis dan secepat kategori lebih tinggi</color>.";
            Debug.Log("Skill Good message added.");
        }
        if (skillClasses[selectedSkillIndex] == "Average")
        {
            combinedMessage += " Pada faktor <i>skill</i>, anda mendapat <i>rating</i> sebesar <color=red>" + selectedSkillValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>average</color></i> dan menggambarkan <color=red>bahwa anda cukup terlatih dan cakap, dengan perencanaan yang terlihat dan koordinasi yang cukup baik, meskipun belum bekerja dengan cepat atau otomatis</color>.";
            Debug.Log("Skill Average message added.");
        }
        if (skillClasses[selectedSkillIndex] == "Fair")
        {
            combinedMessage += " Pada faktor <i>skill</i>, anda mendapat <i>rating</i> sebesar <color=red>" + selectedSkillValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>fair</color></i> dan menggambarkan <color=red>bahwa anda terlatih tetapi tidak sepenuhnya percaya diri, sering ragu, dan kerap membuat kesalahan yang memengaruhi produktivitas</color>.";
            Debug.Log("Skill Fair message added.");
        }
        if (skillClasses[selectedSkillIndex] == "Poor")
        {
            combinedMessage += " Pada faktor <i>skill</i>, anda mendapat <i>rating</i> sebesar <color=red>" + selectedSkillValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>poor</color></i> dan menggambarkan <color=red>bahwa anda sangat tidak terampil, ragu-ragu, sering membuat kesalahan, tidak memiliki inisiatif, dan tidak cocok dengan pekerjaannya</color>.";
            Debug.Log("Skill Poor message added.");
        }

        // Effort
        if (effortClasses[selectedEffortIndex] == "Super Skill")
        {
            combinedMessage += " Pada faktor <i>effort</i>, anda mendapatkan rating sebesar <color=red>" + selectedEffortValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>super skill</color></i> dan menggambarkan bahwa <color=red>anda memiliki kecepatan kerja sangat tinggi hingga membahayakan kesehatan, tidak dapat dipertahankan sepanjang hari kerja</color>.";
            Debug.Log("Effort Super Skill message added.");
        }
        if (effortClasses[selectedEffortIndex] == "Excellent")
        {
            combinedMessage += " Pada faktor <i>effort</i>, anda mendapatkan rating sebesar <color=red>" + selectedEffortValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>excellent</color></i> dan menggambarkan bahwa <color=red>anda memiliki kecepatan kerja tinggi dan ekonomis, bekerja dengan penuh perhatian, jarang membuat kesalahan, sistematis, namun tidak dapat bertahan lama</color>.";
            Debug.Log("Effort Excellent message added.");
        }
        if (effortClasses[selectedEffortIndex] == "Good")
        {
            combinedMessage += " Pada faktor <i>effort</i>, anda mendapatkan rating sebesar <color=red>" + selectedEffortValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>good</color></i> dan menggambarkan bahwa <color=red>anda bekerja berirama dan penuh perhatian, kecepatan baik dan dapat dipertahankan sepanjang hari, menerima saran, tempat kerja rapi, dan peralatan terjaga</color>.";
            Debug.Log("Effort Good message added.");
        }
        if (effortClasses[selectedEffortIndex] == "Average")
        {
            combinedMessage += " Pada faktor <i>effort</i>, anda mendapatkan rating sebesar <color=red>" + selectedEffortValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>average</color></i> dan menggambarkan bahwa <color=red>anda stabil dan setara dengan rata-rata, menerima saran namun tidak melaksanakannya</color>.";
            Debug.Log("Effort Average message added.");
        }
        if (effortClasses[selectedEffortIndex] == "Fair")
        {
            combinedMessage += " Pada faktor <i>effort</i>, anda mendapatkan rating sebesar <color=red>" + selectedEffortValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>fair</color></i> dan menggambarkan bahwa <color=red>anda kurang perhatian, menggunakan alat tidak selalu yang terbaik, sistematika kerja biasa, cenderung kurang efisien</color>.";
            Debug.Log("Effort Fair message added.");
        }
        if (effortClasses[selectedEffortIndex] == "Poor")
        {
            combinedMessage += " Pada faktor <i>effort</i>, anda mendapatkan rating sebesar <color=red>" + selectedEffortValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>poor</color></i> dan menggambarkan bahwa <color=red>anda banyak membuang waktu, tidak menunjukkan minat kerja, menolak saran, malas, dan tempat kerja tidak rapi</color>.";
            Debug.Log("Effort Poor message added.");
        }

        // Display the combined message in the WPRDataText component
        WPRDataText.text = combinedMessage;
        WPRDataText.gameObject.SetActive(true); // Show the text component
        Debug.Log("Combined message displayed: " + combinedMessage);

        string combinedMessage2 = "";

        // Condition
        if (conditionClasses[selectedConditionIndex] == "Ideal")
        {
            combinedMessage2 += "Pada faktor <i>consistency</i> anda mendapatkan <i>rating</i> sebesar <color=red>" + selectedConsistencyValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>ideal</color></i> dan meggambarkan <color=red>kondisi kerja sangat optimal, termasuk suhu, ventilasi, pencahayaan, dan kebisingan yang sempurna, memungkinkan kinerja maksimal</color>.";
            Debug.Log("Skill Ideal message added.");
        }
        if (conditionClasses[selectedConditionIndex] == "Excellent")
        {
            combinedMessage2 += "Pada faktor <i>consistency</i> anda mendapatkan <i>rating</i> sebesar <color=red>" + selectedConsistencyValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>excellent</color></i> dan meggambarkan <color=red>kondisi kerja sangat baik, sedikit di bawah ideal, tetap mendukung produktivitas tinggi</color>.";
            Debug.Log("Skill Excellent message added.");
        }
        if (conditionClasses[selectedConditionIndex] == "Good")
        {
            combinedMessage2 += "Pada faktor <i>condition</i>, anda mendapatkan <i>rating</i> sebesar <color=red>" + selectedConditionValue.ToString("F2") + "</color> yang termasuk kategori <i><color=red>good</color></i> dan menggambarkan bahwa <color=red>kondisi kerja baik, cukup nyaman untuk menjaga produktivitas namun mungkin ada sedikit kekurangan kecil</color>.";
            Debug.Log("Skill Good message added.");
        }
        if (conditionClasses[selectedConditionIndex] == "Average")
        {
            combinedMessage2 += "Pada faktor <i>condition</i>, anda mendapatkan <i>rating</i> sebesar <color=red>" + selectedConditionValue.ToString("F2") + "</color> yang termasuk kategori <i><color=red>average</color></i> dan menggambarkan bahwa <color=red>kerja standar, tidak memberikan keuntungan atau hambatan signifikan bagi pekerja</color>.";
            Debug.Log("Skill Average message added.");
        }
        if (conditionClasses[selectedConditionIndex] == "Fair")
        {
            combinedMessage2 += "Pada faktor <i>condition</i>, anda mendapatkan <i>rating</i> sebesar <color=red>" + selectedConditionValue.ToString("F2") + "</color> yang termasuk kategori <i><color=red>fair</color></i> dan menggambarkan bahwa <color=red>kondisi kurang mendukung, seperti ventilasi atau pencahayaan yang tidak memadai, menyebabkan penurunan kenyamanan dan kinerja</color>.";
            Debug.Log("Skill Fair message added.");
        }
        if (conditionClasses[selectedConditionIndex] == "Poor")
        {
            combinedMessage2 += "Pada faktor <i>condition</i>, anda mendapatkan <i>rating</i> sebesar <color=red>" + selectedConditionValue.ToString("F2") + "</color> yang termasuk kategori <i><color=red>poor</color></i> dan menggambarkan bahwa <color=red>kondisi kerja buruk, lingkungan yang tidak nyaman dengan suhu, ventilasi, atau kebisingan yang tidak ideal, menghambat produktivitas secara signifikan</color>.";
            Debug.Log("Skill Poor message added.");
        }

        // Consistency
        if (consistencyClasses[selectedConsistencyIndex] == "Ideal")
        {
            combinedMessage2 += " Pada faktor <i>consistency</i> anda mendapatkan <i>rating</i> sebesar <color=red>" + selectedConsistencyValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>ideal</color></i> dan meggambarkan <color=red>konsistensi kerja sangat optimal, anda hampir tidak pernah melakukan variasi atau kesalahan, menghasilkan hasil yang stabil</color>.";
            Debug.Log("Skill Ideal message added.");
        }
        if (consistencyClasses[selectedConsistencyIndex] == "Excellent")
        {
            combinedMessage2 += " Pada faktor <i>consistency</i> anda mendapatkan <i>rating</i> sebesar <color=red>" + selectedConsistencyValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>excellent</color></i> dan meggambarkan <color=red>konsistensi sangat baik, ada sedikit variasi namun tidak signifikan, tetap menghasilkan performa tinggi</color>.";
            Debug.Log("Skill Excellent message added.");
        }
        if (consistencyClasses[selectedConsistencyIndex] == "Good")
        {
            combinedMessage2 += " Pada faktor <i>consistency</i> anda mendapatkan <i>rating</i> sebesar <color=red>" + selectedConsistencyValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>good</color></i> dan meggambarkan <color=red>konsistensi baik, ada sedikit variasi dalam pekerjaan, tetapi tidak berdampak besar pada hasil akhir</color>.";
            Debug.Log("Skill Good message added.");
        }
        if (consistencyClasses[selectedConsistencyIndex] == "Average")
        {
            combinedMessage2 += " Pada faktor <i>consistency</i> anda mendapatkan <i>rating</i> sebesar <color=red>" + selectedConsistencyValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>average</color></i> dan meggambarkan <color=red>konsistensi standar, variasi dalam performa berada dalam batas normal, tidak memberi keuntungan atau kerugian</color>.";
            Debug.Log("Skill Average message added.");
        }
        if (consistencyClasses[selectedConsistencyIndex] == "Fair")
        {
            combinedMessage2 += " Pada faktor <i>consistency</i> anda mendapatkan <i>rating</i> sebesar <color=red>" + selectedConsistencyValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>fair</color></i> dan meggambarkan <color=red>konsistensi kurang baik, terdapat variasi yang lebih sering terjadi, menyebabkan penurunan keandalan hasil</color>.";
            Debug.Log("Skill Fair message added.");
        }
        if (consistencyClasses[selectedConsistencyIndex] == "Poor")
        {
            combinedMessage2 += " Pada faktor <i>consistency</i> anda mendapatkan <i>rating</i> sebesar <color=red>" + selectedConsistencyValue.ToString("F2") + "</color> yang termasuk kategori kelas <i><color=red>poor</color></i> dan meggambarkan <color=red>konsistensi rendah, variasi sering terjadi, mempengaruhi kualitas dan efisiensi pekerjaan secara signifikan</color>.";
            Debug.Log("Skill Poor message added.");
        }

        combinedMessage2 += " Nilai Total <i>Work Performance Rating</i> sebesar <color=red>" + totalScore.ToString("F2") + "</color> menunjukkan penilaian terhadap seberapa besar efektivitas dan efisiensi kinerja anda secara keseluruhan ketika melakukan perakitan LEGO Buldozer sebanyak 10 kali. Nilai tersebut juga akan digunakan pada perhitungan <i>normal time</i>.";

        // Display the combined message in the WPRDataText component
        WPRDataText2.text = combinedMessage2;
        WPRDataText2.gameObject.SetActive(true); // Show the text component
        Debug.Log("Combined message displayed: " + combinedMessage2);
    }

    // Method to hide all panels in all panel groups
    private void HideAllPanels()
    {
        foreach (PanelGroup group in panelGroups)
        {
            foreach (GameObject panel in group.panels)
            {
                panel.SetActive(false);
            }
        }
    }

    private void OpenFirstPanelGroup()
    {
        HideAllPanels();
        currentPanelGroupIndex = 0;
        if (panelGroups.Length > 0)
        {
            OpenPanelGroup(currentPanelGroupIndex);
        }
        NextConversationButton.gameObject.SetActive(true);
    }

    private void OpenNextPanelGroup()
    {
        HideCurrentPanelGroup();
        currentPanelGroupIndex++;
        if (currentPanelGroupIndex < panelGroups.Length)
        {
            OpenPanelGroup(currentPanelGroupIndex);
            if (currentPanelGroupIndex == panelGroups.Length - 1)
            {
                NextConversationButton.gameObject.SetActive(false);
                CloseButton.gameObject.SetActive(true);
            }
        }
    }

    private void OpenPanelGroup(int index)
    {
        // Move the camera to the specified position and set the rotation
        Camera.main.transform.position = new Vector3(2.32f, 1.64f, 4.358f);
        Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Set rotation to (0, 0, 0)

        // Make the Lecturer object visible by setting it to active
        if (lecturerObject != null)
        {
            lecturerObject.SetActive(true); // Show the Lecturer object
        }

        foreach (GameObject panel in panelGroups[index].panels) // Iterate through all panels in the specified panel group
        {
            panel.SetActive(true); // Set each panel active to make it visible
        }
    }

    private void HideCurrentPanelGroup()
    {
        foreach (GameObject panel in panelGroups[currentPanelGroupIndex].panels)
        {
            panel.SetActive(false);
        }
    }

    private void CloseLastPanel()
    {
        HideCurrentPanelGroup();
        CloseButton.gameObject.SetActive(false);

        // Check if WPRContentGroups exist and show the panels
        if (wprContentGroups != null && wprContentGroups.Length > 0)
        {
            OpenWPRContentGroup(0);  // Open the first WPR Content Group
        }
    }

    private void OpenWPRContentGroup(int index)
    {
        // Ensure the index is valid and within bounds
        if (index < wprContentGroups.Length)
        {
            WPRContentGroup nextContentGroup = wprContentGroups[index];

            // Store the original position if it's the first time opening
            if (nextContentGroup.WPRContentPanel != null && nextContentGroup.originalPosition == Vector3.zero)
            {
                nextContentGroup.originalPosition = nextContentGroup.WPRContentPanel.transform.localPosition;
            }

            // Determine the new position based on the panel's name
            if (nextContentGroup.WPRContentPanel.name == "Skill" || nextContentGroup.WPRContentPanel.name == "Effort")
            {
                // Move to position x: -780, y: -708
                nextContentGroup.WPRContentPanel.transform.localPosition = new Vector3(-780f, -708f, 0f);
            }
            else if (nextContentGroup.WPRContentPanel.name == "Condition")
            {
                // Move to position x: -743, y: -708
                nextContentGroup.WPRContentPanel.transform.localPosition = new Vector3(-743f, -708f, 0f);
            }
            else if (nextContentGroup.WPRContentPanel.name == "Consistency")
            {
                // Move to position x: -743, y: -708
                nextContentGroup.WPRContentPanel.transform.localPosition = new Vector3(-761f, -708f, 0f);
            }


            // Show the content panel of the next group
            if (nextContentGroup.WPRContentPanel != null)
            {
                nextContentGroup.WPRContentPanel.SetActive(true);
                Debug.Log("WPRContentPanel for group " + index + " is now active.");
            }

            // Show the first tutorial panel of the next group if it exists
            if (nextContentGroup.WPRTutorialPanels.Length > 0)
            {
                nextContentGroup.WPRTutorialPanels[0].SetActive(true);
                currentTutorialPanelIndex = 0;
                Debug.Log("First tutorial panel for group " + index + " is now active.");

                // If more than one tutorial panel, show the "Next" button
                if (nextContentGroup.WPRTutorialPanels.Length > 1)
                {
                    WPRNextTutorialButton.gameObject.SetActive(true);
                    WPRNextTutorialButton.onClick.RemoveAllListeners(); // Remove previous listeners
                    WPRNextTutorialButton.onClick.AddListener(() => ShowNextTutorialPanel(nextContentGroup));
                }
                else
                {
                    WPRNextTutorialButton.gameObject.SetActive(false);
                }
            }
        }
    }

    private void CloseWPRContentGroup(WPRContentGroup contentGroup)
    {
        // Hide the WPR content panel
        if (contentGroup.WPRContentPanel != null)
        {
            contentGroup.WPRContentPanel.SetActive(false);
            Debug.Log("WPRContentPanel is now hidden.");

            // Move the panel back to its original position
            contentGroup.WPRContentPanel.transform.localPosition = contentGroup.originalPosition;
            Debug.Log("WPRContentPanel has returned to its original position.");
        }

        // Hide all tutorial panels
        foreach (var tutorialPanel in contentGroup.WPRTutorialPanels)
        {
            if (tutorialPanel != null)
            {
                tutorialPanel.SetActive(false);
            }
        }

        // Hide the "Next" button after closing the last panel
        WPRNextTutorialButton.gameObject.SetActive(false);
    }

    // Coroutine to change the background of all toggles in a category to red and revert after 0.5 seconds
    private IEnumerator FlashToggleBackgrounds(Toggle[] toggles)
    {
        // Store the color you want to revert to (e.g., white)
        Color originalColor = Color.white;

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

        // Revert the background colors to their original state (or fixed color)
        foreach (Toggle toggle in toggles)
        {
            if (toggle.targetGraphic != null)
            {
                toggle.targetGraphic.color = originalColor;
            }
        }
    }

    // Method to highlight the background of all toggles in a required category if not selected
    private void HighlightMissingToggleForCurrentCategory()
    {
        Toggle[] togglesToHighlight = null;

        // Determine which category of toggles should be highlighted
        switch (currentWPRContentGroupIndex)
        {
            case 0: // Skill
                togglesToHighlight = skillToggles;
                break;
            case 1: // Effort
                togglesToHighlight = effortToggles;
                break;
            case 2: // Condition
                togglesToHighlight = conditionToggles;
                break;
            case 3: // Consistency
                togglesToHighlight = consistencyToggles;
                break;
        }

        // If toggles were found, start the coroutine to flash their backgrounds
        if (togglesToHighlight != null)
        {
            StartCoroutine(FlashToggleBackgrounds(togglesToHighlight));
        }
    }

    private void ShowNextTutorialPanel(WPRContentGroup contentGroup)
    {
        bool correctToggleSelected = (currentWPRContentGroupIndex == 0 && isSkillSelected) ||
                                     (currentWPRContentGroupIndex == 1 && isEffortSelected) ||
                                     (currentWPRContentGroupIndex == 2 && isConditionSelected) ||
                                     (currentWPRContentGroupIndex == 3 && isConsistencySelected);

        if (currentTutorialPanelIndex == contentGroup.WPRTutorialPanels.Length - 1 && !correctToggleSelected)
        {
            // If the required toggle is not selected, highlight the category
            HighlightMissingToggleForCurrentCategory();
            return; // Do not proceed until the correct toggle is selected
        }

        contentGroup.WPRTutorialPanels[currentTutorialPanelIndex].SetActive(false);
        currentTutorialPanelIndex++;

        if (currentTutorialPanelIndex >= contentGroup.WPRTutorialPanels.Length)
        {
            CloseWPRContentGroup(contentGroup);

            currentWPRContentGroupIndex++;
            if (currentWPRContentGroupIndex < wprContentGroups.Length)
            {
                OpenWPRContentGroup(currentWPRContentGroupIndex);
            }
            else
            {
                Debug.Log("All WPR content groups completed.");
                ResetAllWPRContentPanels(); // Reset all panels to original positions and show Skill, Effort, Condition, and Consistency Panels
            }

            currentTutorialPanelIndex = 0;
        }
        else
        {
            contentGroup.WPRTutorialPanels[currentTutorialPanelIndex].SetActive(true);
        }
    }

    private void ResetAllWPRContentPanels()
    {
        foreach (var contentGroup in wprContentGroups)
        {
            if (contentGroup.WPRContentPanel != null)
            {
                // Move the panel back to its original position
                contentGroup.WPRContentPanel.transform.localPosition = contentGroup.originalPosition;
                contentGroup.WPRContentPanel.SetActive(false); // Optionally hide the panel
                Debug.Log("WPRContentPanel has returned to its original position.");
            }
        }

        // Now make the specific panels visible inside the WPRPanel
        if (WPRPanel != null)
        {
            // Ensure WPRPanel is active
            WPRPanel.SetActive(true);

            // Activate the specific panels
            if (SkillPanel != null)
            {
                SkillPanel.SetActive(true);
            }
            if (EffortPanel != null)
            {
                EffortPanel.SetActive(true);
            }
            if (ConditionPanel != null)
            {
                ConditionPanel.SetActive(true);
            }
            if (ConsistencyPanel != null)
            {
                ConsistencyPanel.SetActive(true);
            }

            Debug.Log("Skill, Effort, Condition, and Consistency Panels are now visible inside WPRPanel.");

            // Check if all panels are open and display WPRDataButton
            ShowWPRDataButtonIfPanelsOpen();
        }
    }

    // Method to select skill
    private void SelectSkill(int index)
    {
        DeselectOthers(skillToggles, index);
        selectedSkillIndex = index;
        selectedSkillValue = skillValues[index];
        isSkillSelected = true;
        CheckAndCalculate(); // Check if all conditions are met after selection
    }

    // Method to select effort
    private void SelectEffort(int index)
    {
        DeselectOthers(effortToggles, index);
        selectedEffortIndex = index;
        selectedEffortValue = effortValues[index];
        isEffortSelected = true;
        CheckAndCalculate(); // Check if all conditions are met after selection
    }

    // Method to select condition
    private void SelectCondition(int index)
    {
        DeselectOthers(conditionToggles, index);
        selectedConditionIndex = index;
        selectedConditionValue = conditionValues[index];
        isConditionSelected = true;
        CheckAndCalculate(); // Check if all conditions are met after selection
    }

    // Method to select consistency
    private void SelectConsistency(int index)
    {
        DeselectOthers(consistencyToggles, index);
        selectedConsistencyIndex = index;
        selectedConsistencyValue = consistencyValues[index];
        isConsistencySelected = true;
        CheckAndCalculate(); // Check if all conditions are met after selection
    }

    private void DeselectOthers(Toggle[] toggles, int selectedIndex)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (i != selectedIndex)
            {
                toggles[i].isOn = false;
            }
        }
    }

    private void CheckAndCalculate()
    {
        if (!float.IsNaN(selectedSkillValue) && !float.IsNaN(selectedEffortValue) &&
        !float.IsNaN(selectedConditionValue) && !float.IsNaN(selectedConsistencyValue))
        {
            float totalScore = selectedSkillValue + selectedEffortValue + selectedConditionValue + selectedConsistencyValue;
            totalScoreText.text = totalScore.ToString("F2");
            WPRDataButton.gameObject.SetActive(true); // Show the button if all selections are made
        }
        else
        {
            WPRDataButton.gameObject.SetActive(false); // Hide the button if any selection is missing
        }

        // Check if all panels (Skill, Effort, Condition, Consistency) are visible
        if (SkillPanel.activeSelf && EffortPanel.activeSelf && ConditionPanel.activeSelf && ConsistencyPanel.activeSelf)
        {
            // Check if all categories are selected (not NaN)
            if (!float.IsNaN(selectedSkillValue) && !float.IsNaN(selectedEffortValue) &&
                !float.IsNaN(selectedConditionValue) && !float.IsNaN(selectedConsistencyValue))
            {
                // All conditions are met: Panels are visible and all categories are selected

                // Calculate the total score
                float totalScore = selectedSkillValue + selectedEffortValue + selectedConditionValue + selectedConsistencyValue;

                // Display debug information
                Debug.Log("Skill: " + FormatScore(selectedSkillValue));
                Debug.Log("Effort: " + FormatScore(selectedEffortValue));
                Debug.Log("Condition: " + FormatScore(selectedConditionValue));
                Debug.Log("Consistency: " + FormatScore(selectedConsistencyValue));
                Debug.Log("Total Score: " + FormatScore(totalScore));

                // Display performance data
                soalModul2.DisplayPerformanceData(GetPerformanceData());

                // Show the WPRDataButton
                WPRDataButton.gameObject.SetActive(true);
            }
            else
            {
                // If any category is unselected, hide the WPRDataButton
                WPRDataButton.gameObject.SetActive(false);
            }
        }
        else
        {
            // If any of the panels are not visible, hide the WPRDataButton
            WPRDataButton.gameObject.SetActive(false);
        }
    }

    private string FormatScore(float score)
    {
        return score >= 0 ? "+" + score.ToString("F2") : score.ToString("F2");
    }

    public List<PerformanceData> GetPerformanceData()
    {
        List<PerformanceData> performanceDataList = new List<PerformanceData>();

        performanceDataList.Add(new PerformanceData
        {
            Class = skillClasses[selectedSkillIndex],
            Symbol = skillLabels[selectedSkillIndex],
            Rating = selectedSkillValue.ToString("F2")
        });

        performanceDataList.Add(new PerformanceData
        {
            Class = effortClasses[selectedEffortIndex],
            Symbol = effortLabels[selectedEffortIndex],
            Rating = selectedEffortValue.ToString("F2")
        });

        performanceDataList.Add(new PerformanceData
        {
            Class = conditionClasses[selectedConditionIndex],
            Symbol = conditionLabels[selectedConditionIndex],
            Rating = selectedConditionValue.ToString("F2")
        });

        performanceDataList.Add(new PerformanceData
        {
            Class = consistencyClasses[selectedConsistencyIndex],
            Symbol = consistencyLabels[selectedConsistencyIndex],
            Rating = selectedConsistencyValue.ToString("F2")
        });

        return performanceDataList;
    }

    // Method to check if all panels are open and show the WPRDataButton if appropriate
    private void ShowWPRDataButtonIfPanelsOpen()
    {
        // Check if all panels (Skill, Effort, Condition, Consistency) are visible
        if (SkillPanel.activeSelf && EffortPanel.activeSelf && ConditionPanel.activeSelf && ConsistencyPanel.activeSelf)
        {
            // All panels are visible, call CheckAndCalculate to determine if the WPRDataButton should be shown
            CheckAndCalculate();
        }
    }

    private void SendPerformanceDataToSoalModul2()
    {
        List<PerformanceData> performanceData = GetPerformanceData(); // Generate the data

        // Log the data being sent for debugging
        Debug.Log("Sending performance data to SoalModul2:");
        foreach (var data in performanceData)
        {
            Debug.Log($"Class: {data.Class}, Symbol: {data.Symbol}, Rating: {data.Rating}");
        }

        soalModul2.DisplayPerformanceData(performanceData);
    }

}
