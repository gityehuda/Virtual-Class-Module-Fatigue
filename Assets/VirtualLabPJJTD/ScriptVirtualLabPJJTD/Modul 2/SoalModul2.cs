using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using System.Text;
using TMPro;
using Binus.WebGL.Service;

public class SoalModul2 : MonoBehaviour
{
    public Module2Manager module2Manager;  // Reference to the Module2Manager script
    public RecAllowance recAllowance;

    public GameObject[] panels;            // Array of main panels
    public GameObject[] SoalPanel;         // Array of Soal panels
    public GameObject[] EvaluasiPanel;     // Array for Evaluasi panels
    public GameObject downloadPanel;       // Panel for download action

    public Button nextButton;              // Button to go to the next panel
    public Button NextSoalButton;
    public Button nextEvaluasiButton;      // New Button to go to the next evaluasi panel
    public Button closeButton;             // Button to close the main panel if it's the last
    public Button StartSoalModul2;         // Button to start the first panel
    public Button downloadButton;          // Button for download action
    public Button ReturnHomeButton;        // Button to return to the home scene

    public GameObject ScorePanel;  // Reference to the Score Panel
    public TextMeshProUGUI ScoreText;         // Reference to the Text that shows the score
    public Button EvaluasiButton;  // Button to open the EvaluasiPanel

    // Arrays of Text fields for Waktu Kumulatif and Waktu Proses (10 for each)
    public GameObject tabelWaktuKumulatif; // The image object in Soal1
    public GameObject tabelAllowance;      // The image object in Soal4

    private Transform originalParentSoal1; // To store the original parent of tabelWaktuKumulatif (Soal1)
    private Vector3 originalPositionWaktuKumulatif; // To store the original local position of tabelWaktuKumulatif

    private Transform originalParentSoal4; // To store the original parent of tabelAllowance (Soal4)
    private Vector3 originalPositionAllowance;

    public TextMeshProUGUI[] waktuKumulatifTexts = new TextMeshProUGUI[10];  // Array of Text for Waktu Kumulatif
    public TextMeshProUGUI[] waktuProsesTexts = new TextMeshProUGUI[10];     // Array of Text for Waktu Proses

    private int currentPanelIndex = 0;     // Track the current main panel index
    private int currentSoalPanelIndex = 0; // Track the current soal panel index
    private int currentEvaluasiPanelIndex = 0; // Track the current evaluasi panel index

    // UI elements for the quiz answers (no question text)
    public Button[] answerButtons1;  // Buttons for the first question
    public Button[] answerButtons2;  // Buttons for the second question
    public Button[] answerButtons3;  // Buttons for the third question
    public Button[] answerButtons4;
    public Button[] answerButtons5;

    // Arrays of Text fields to show the answer options for each question in the EvaluasiPanel
    public TextMeshProUGUI[] evaluasiAnswerTextsQ1; // Texts for Question 1 options
    public TextMeshProUGUI[] evaluasiAnswerTextsQ2; // Texts for Question 2 options
    public TextMeshProUGUI[] evaluasiAnswerTextsQ3; // Texts for Question 3 options
    public TextMeshProUGUI[] evaluasiAnswerTextsQ4; // Texts for Question 4 options
    public TextMeshProUGUI[] evaluasiAnswerTextsQ5; // Texts for Question 5 options


    private bool[] isQuestionAnswered;
    private int correctAnswersCount = 0;  // Tracks how many questions were answered correctly

    // Define pressed and default colors
    public Color defaultColor = Color.white;
    public Color pressedColor = Color.gray;

    private float lastWaktuKumulatif;

    // Add this field to the SoalModul2 class
    private List<PerformanceData> storedPerformanceData = new List<PerformanceData>();

    private float correctAnswer2;
    private float correctAnswer3;
    private float correctAnswer4;

    public TextMeshProUGUI[] classTexts;   // Array to show Classes
    public TextMeshProUGUI[] symbolTexts;  // Array to show Symbols
    public TextMeshProUGUI[] ratingTexts;  // Array to show Ratings
    public TextMeshProUGUI totalScoreText; // Text to display Total Score

    // UI elements to display the allowances
    public TextMeshProUGUI constantAllowanceText;
    public TextMeshProUGUI variableAllowanceText;
    public TextMeshProUGUI totalAllowanceText;

    public int[] userAnswers = new int[5];

    void Start()
    {

        ScorePanel.SetActive(false);
        EvaluasiButton.gameObject.SetActive(false);
        nextEvaluasiButton.gameObject.SetActive(false);

        // Assign the Module2Manager via FindObjectOfType (optional, can also be assigned via the inspector)
        if (module2Manager == null)
        {
            module2Manager = FindObjectOfType<Module2Manager>();  // Automatically find the Module2Manager if not assigned
        }

        if (recAllowance == null)
        {
            recAllowance = FindObjectOfType<RecAllowance>(); // Find RecAllowance script in the scene if not manually assigned
        }

        // Hide the NextSoalButton initially
        NextSoalButton.gameObject.SetActive(false);

        // Add listeners for answer buttons for each question
        AddAnswerButtonListeners();

        // Initialize the boolean array based on the number of SoalPanels
        isQuestionAnswered = new bool[SoalPanel.Length];

        // Set all questions to false initially (none answered)
        for (int i = 0; i < isQuestionAnswered.Length; i++)
        {
            isQuestionAnswered[i] = false; // Ensure all questions are not answered initially
        }

        // Subscribe to the event to display the allowance data when it's calculated
        recAllowance.OnAllowanceCalculated += DisplayAllowanceData;

        // Initialize all panels and buttons
        InitializePanels();
        InitializeSoalPanels();
        InitializeEvaluasiPanels();
        downloadPanel.SetActive(false); // Hide the download panel initially

        // Set the buttons' click events
        StartSoalModul2.onClick.AddListener(OpenFirstPanel);
        nextButton.onClick.AddListener(NextPanel);
        nextEvaluasiButton.onClick.AddListener(NextEvaluasiPanel); // Add NextEvaluasi button click event
        closeButton.onClick.AddListener(ClosePanel);
        downloadButton.onClick.AddListener(DownloadAction);
        ReturnHomeButton.onClick.AddListener(ReturnHome); // Add ReturnHomeButton click event

        // Hide the Close button initially
        closeButton.gameObject.SetActive(false);
        nextEvaluasiButton.gameObject.SetActive(false);  // Hide the NextEvaluasi button initially

        // Update the waktu fields
        UpdateWaktuTexts();

        GenerateQuiz();

        // Create a list of all the answer button arrays
        List<Button[]> allAnswerButtons = new List<Button[]>
        {
            answerButtons1, answerButtons2, answerButtons3, answerButtons4, answerButtons5
        };

        // Loop through each answer button array
        foreach (Button[] answerButtons in allAnswerButtons)
        {
            // For each button in the current array, assign the click event
            foreach (Button button in answerButtons)
            {
                // Capture the button for the listener
                Button capturedButton = button;
                button.onClick.AddListener(() => SetButtonPressed(answerButtons, capturedButton));
            }
        }

        // Store the original parent and local position of tabelWaktuKumulatif in Soal1
        originalParentSoal1 = tabelWaktuKumulatif.transform.parent;
        originalPositionWaktuKumulatif = tabelWaktuKumulatif.transform.localPosition;

        // Store the original parent and local position of tabelAllowance in Soal4
        originalParentSoal4 = tabelAllowance.transform.parent;
        originalPositionAllowance = tabelAllowance.transform.localPosition;
    }

    // Method to move the image to a target SoalPanel while keeping the same position
    public void MoveImageToSoal(GameObject imageObject, GameObject targetSoalPanel, bool moveToOriginal)
    {
        if (moveToOriginal)
        {
            // Move the image back to its original parent
            if (imageObject == tabelWaktuKumulatif)
            {
                tabelWaktuKumulatif.transform.SetParent(originalParentSoal1, true); // Keep world position
            }
            else if (imageObject == tabelAllowance)
            {
                tabelAllowance.transform.SetParent(originalParentSoal4, true); // Keep world position
            }
        }
        else
        {
            // Move the image to the new panel
            imageObject.transform.SetParent(targetSoalPanel.transform, true); // Keep world position
        }
    }

    void AddAnswerButtonListeners()
    {
        // Create a list of all the answer button arrays
        List<Button[]> allAnswerButtons = new List<Button[]>
    {
        answerButtons1, answerButtons2, answerButtons3, answerButtons4, answerButtons5
    };

        // Loop through each answer button array and add listener to each button
        for (int questionIndex = 0; questionIndex < allAnswerButtons.Count; questionIndex++)
        {
            Button[] answerButtons = allAnswerButtons[questionIndex];
            int currentQuestionIndex = questionIndex; // Capture the question index

            foreach (Button button in answerButtons)
            {
                button.onClick.AddListener(() => OnAnswerSelected(currentQuestionIndex, button));
            }
        }
    }

    void OnAnswerSelected(int questionIndex, Button selectedButton)
    {
        // Find the selected button index in the answer button array for the current question
        Button[] currentAnswerButtons;
        switch (questionIndex)
        {
            case 0: currentAnswerButtons = answerButtons1; break;
            case 1: currentAnswerButtons = answerButtons2; break;
            case 2: currentAnswerButtons = answerButtons3; break;
            case 3: currentAnswerButtons = answerButtons4; break;
            case 4: currentAnswerButtons = answerButtons5; break;
            default: return; // Exit if the question index is invalid
        }

        // Find the selected button's index within the current answer buttons array
        for (int i = 0; i < currentAnswerButtons.Length; i++)
        {
            if (currentAnswerButtons[i] == selectedButton)
            {
                userAnswers[questionIndex] = i; // Store the selected answer's index
                break;
            }
        }

        // Check if the selected button is the correct one and increment score if correct
        if ((questionIndex == 0 && selectedButton == answerButtons1[0]) ||
            (questionIndex == 1 && selectedButton == answerButtons2[4]) ||
            (questionIndex == 2 && selectedButton == answerButtons3[0]) ||
            (questionIndex == 3 && selectedButton == answerButtons4[2]) ||
            (questionIndex == 4 && selectedButton == answerButtons5[4]))
        {
            correctAnswersCount++;  // Increment the correct answer count
        }

        // Set that the question is answered
        isQuestionAnswered[questionIndex] = true;

        // Change the button color to indicate it was selected
        SetButtonPressed(currentAnswerButtons, selectedButton);

        // Display the NextSoalButton if the question is answered
        NextSoalButton.gameObject.SetActive(true);
        NextSoalButton.onClick.RemoveAllListeners(); // Ensure previous listeners are cleared
        NextSoalButton.onClick.AddListener(NextSoalPanel);
    }


    public void ResetButtonColors(Button[] buttons)
    {
        foreach (Button button in buttons)
        {
            button.GetComponent<Image>().color = defaultColor;
        }
    }

    public void SetButtonPressed(Button[] buttons, Button pressedButton)
    {
        // Reset all buttons' colors to the default
        foreach (Button button in buttons)
        {
            button.GetComponent<Image>().color = defaultColor;
        }

        // Set the clicked button to the pressed color
        pressedButton.GetComponent<Image>().color = pressedColor;
    }

    public void SetButtonPressed(Button button)
    {
        // Get the button's Image component and change the color to pressedColor
        button.GetComponent<Image>().color = pressedColor;
    }

    // Method to display Constant, Variable, and Total Allowances on the UI
    public void DisplayAllowanceData()
    {
        // Fetch the data from RecAllowance
        float constantAllowance = recAllowance.ConstantAllowance;
        float variableAllowance = recAllowance.VariableAllowance;
        float totalAllowance = recAllowance.TotalAllowance;

        // Display the data on the UI (ensure UI Text components are linked in the Inspector)
        constantAllowanceText.text = constantAllowance.ToString("F2") + "%";
        variableAllowanceText.text = variableAllowance.ToString("F2") + "%";
        totalAllowanceText.text = totalAllowance.ToString("F2") + "%";

    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (recAllowance != null)
        {
            recAllowance.OnAllowanceCalculated -= DisplayAllowanceData;
        }
    }

    // Method to display performance data
    public void DisplayPerformanceData(List<PerformanceData> data)
    {
        // Store the performance data for later use in the CSV export
        storedPerformanceData = data;

        // Log the received data for debugging
        Debug.Log("Received performance data in SoalModul2:");
        foreach (var item in storedPerformanceData)
        {
            Debug.Log($"Class: {item.Class}, Symbol: {item.Symbol}, Rating: {item.Rating}");
        }

        // Display the data on the UI (assuming the UI elements are already set up)
        for (int i = 0; i < data.Count; i++)
        {
            classTexts[i].text = data[i].Class;
            symbolTexts[i].text = data[i].Symbol;
            ratingTexts[i].text = FormatRating(float.Parse(data[i].Rating));  // Format rating with +/- for better readability
        }

        // Calculate and display the total score
        float totalScore = CalculateTotalScore(data);
        totalScoreText.text = FormatRating(totalScore);

        // Log the total score for debugging
        Debug.Log($"Total Score: {totalScore}");
    }

    // Helper method to format rating with + or - sign
    private string FormatRating(float rating)
    {
        // Add a + sign for positive values
        return rating >= 0 ? "+" + rating.ToString("F2") : rating.ToString("F2");
    }

    private float CalculateTotalScore(List<PerformanceData> data)
    {
        float total = 0;
        foreach (var item in data)
        {
            total += float.Parse(item.Rating);  // Sum up the rating values
        }
        return total;
    }

    // Method to generate the quiz based on the last Waktu Kumulatif
    public void GenerateQuiz()
    {
        // Fetch the last Waktu Kumulatif value
        List<float> assemblyTimes = module2Manager.GetAssemblyTimes();  // Assuming you get the list from here

        // Check if assemblyTimes is null or empty
        if (assemblyTimes == null)
        {
            Debug.LogError("assemblyTimes is null. Ensure module2Manager is initialized properly.");
            return;
        }

        if (assemblyTimes.Count == 0)
        {
            Debug.LogError("assemblyTimes is empty. No data available.");
            return;
        }

        // Print all values of assemblyTimes to confirm correct data
        for (int i = 0; i < assemblyTimes.Count; i++)
        {
            Debug.Log($"assemblyTimes[{i}] = {assemblyTimes[i]}");
        }

        // Get the last Waktu Kumulatif
        lastWaktuKumulatif = assemblyTimes[assemblyTimes.Count - 1];  // Get the last Waktu Kumulatif

        // Check if lastWaktuKumulatif is valid
        if (lastWaktuKumulatif <= 0)
        {
            Debug.LogError("Invalid lastWaktuKumulatif value: " + lastWaktuKumulatif);
            return;
        }

        Debug.Log("Last Waktu Kumulatif: " + lastWaktuKumulatif);

        // Set the answers for the first, second, and third quizzes
        SetFirstQuestionAnswers();
        SetSecondQuestionAnswers();
        SetThirdQuestionAnswers();
        SetFourthQuestionAnswers();
        SetFifthQuestionAnswers();
    }

    private string FormatTime(float timeValue)
    {
        // If the time value is a whole number, show without decimal places
        if (timeValue % 1 == 0)
        {
            return Mathf.RoundToInt(timeValue).ToString() + " detik";  // No decimals for whole numbers
        }
        else
        {
            return timeValue.ToString("F2") + " detik";  // Show two decimals for non-whole numbers
        }
    }

    // Method for setting the first question's answers (for last Waktu Kumulatif)
    public void SetFirstQuestionAnswers()
    {
        // Ensure lastWaktuKumulatif is valid
        if (lastWaktuKumulatif <= 0)
        {
            Debug.LogError("Invalid lastWaktuKumulatif value.");
            return;
        }

        // Set the correct answer and the options
        float correctAnswer1 = lastWaktuKumulatif;
        answerButtons1[0].GetComponentInChildren<Text>().text = FormatTime(correctAnswer1);  // Correct answer (last waktu kumulatif)
        answerButtons1[1].GetComponentInChildren<Text>().text = FormatTime(correctAnswer1 + correctAnswer1 * 0.1f);  // +10%
        answerButtons1[2].GetComponentInChildren<Text>().text = FormatTime(correctAnswer1 + correctAnswer1 * 0.2f);  // +20%
        answerButtons1[3].GetComponentInChildren<Text>().text = FormatTime(correctAnswer1 + correctAnswer1 * 0.3f);  // +30%
        answerButtons1[4].GetComponentInChildren<Text>().text = FormatTime(correctAnswer1 + correctAnswer1 * 0.4f);  // +40%

        // Add listeners to buttons to handle the answer
        for (int i = 0; i < answerButtons1.Length; i++)
        {
            int index = i;  // Capture the index for the listener
            answerButtons1[i].onClick.AddListener(() => CheckFirstQuestionAnswer(index));
        }
    }

    // Method to check the answer for the first question
    public void CheckFirstQuestionAnswer(int index)
    {
        if (index == 0)
        {
            Debug.Log("Correct answer for the first question!");
        }
        else
        {
            Debug.Log("Wrong answer for the first question.");
        }
    }

    public void SetSecondQuestionAnswers()
    {
        // Ensure lastWaktuKumulatif is valid
        if (lastWaktuKumulatif <= 0)
        {
            Debug.LogError("Invalid lastWaktuKumulatif value.");
            return;
        }

        // Calculate the correct answer for the second question
        correctAnswer2 = lastWaktuKumulatif / 10f;

        // Log correctAnswer2 for debugging
        Debug.Log("CorrectAnswer2: " + correctAnswer2);

        // Set the correct answer and the options
        answerButtons2[0].GetComponentInChildren<Text>().text = FormatTime(correctAnswer2 + correctAnswer2 * 0.1f);  // +10%
        answerButtons2[1].GetComponentInChildren<Text>().text = FormatTime(correctAnswer2 + correctAnswer2 * 0.2f);  // +20%
        answerButtons2[2].GetComponentInChildren<Text>().text = FormatTime(correctAnswer2 + correctAnswer2 * 0.3f);  // +30%
        answerButtons2[3].GetComponentInChildren<Text>().text = FormatTime(correctAnswer2 + correctAnswer2 * 0.4f);  // +40%
        answerButtons2[4].GetComponentInChildren<Text>().text = FormatTime(correctAnswer2);  // Correct answer (last waktu kumulatif / 10)

        // Add listeners to buttons to handle the answer
        for (int i = 0; i < answerButtons2.Length; i++)
        {
            int index = i;  // Capture the index for the listener
            answerButtons2[i].onClick.AddListener(() => CheckSecondQuestionAnswer(index));
        }
    }

    // Method to check the answer for the second question
    public void CheckSecondQuestionAnswer(int index)
    {
        if (index == 4)
        {
            Debug.Log("Correct answer for the second question!");
        }
        else
        {
            Debug.Log("Wrong answer for the second question.");
        }
    }

    public void SetThirdQuestionAnswers()
    {
        // Ensure totalScoreText is not empty or invalid
        if (string.IsNullOrEmpty(totalScoreText.text))
        {
            Debug.LogError("TotalScoreText is empty or invalid.");
            return;
        }

        // Parse the total score from the totalScoreText
        float totalScore;
        if (!float.TryParse(totalScoreText.text, out totalScore))
        {
            Debug.LogError("Failed to parse totalScoreText: " + totalScoreText.text);
            return;
        }

        // Calculate the correct answer using totalScore from totalScoreText
        correctAnswer3 = (lastWaktuKumulatif / 10) * (1 + totalScore);

        // Debug log to confirm correct calculation
        Debug.Log("CorrectAnswer3: " + correctAnswer3);

        // Set the correct answer and the options
        answerButtons3[0].GetComponentInChildren<Text>().text = correctAnswer3 + " detik";  // Correct answer
        answerButtons3[1].GetComponentInChildren<Text>().text = (correctAnswer3 + correctAnswer3 * 0.1f) + " detik";  // +10%
        answerButtons3[2].GetComponentInChildren<Text>().text = (correctAnswer3 + correctAnswer3 * 0.2f) + " detik";  // +20%
        answerButtons3[3].GetComponentInChildren<Text>().text = (correctAnswer3 + correctAnswer3 * 0.3f) + " detik";  // +30%
        answerButtons3[4].GetComponentInChildren<Text>().text = (correctAnswer3 + correctAnswer3 * 0.4f) + " detik";  // +40%

        // Add listeners to buttons to handle the answer
        for (int i = 0; i < answerButtons3.Length; i++)
        {
            int index = i;  // Capture the index for the listener
            answerButtons3[i].onClick.AddListener(() => CheckThirdQuestionAnswer(index));
        }
    }

    public void CheckThirdQuestionAnswer(int index)
    {
        if (index == 0)
        {
            Debug.Log("Correct answer for the third question!");
        }
        else
        {
            Debug.Log("Wrong answer for the third question.");
        }
    }

    // Method to set answers for Question 4
    public void SetFourthQuestionAnswers()
    {
        // Fetch the total allowance from the text
        float totalAllowance;
        if (!float.TryParse(totalAllowanceText.text.Replace("%", ""), out totalAllowance))
        {
            Debug.LogError("Failed to parse Total Allowance: " + totalAllowanceText.text);
            return;
        }

        // Calculate the correct answer using the formula (100% / (100% - Total Allowance))
        correctAnswer4 = 100f / (100f - totalAllowance);

        // Debug log to confirm correct calculation
        Debug.Log("CorrectAnswer4: " + correctAnswer4);

        // Set the correct answer and the options
        answerButtons4[0].GetComponentInChildren<Text>().text = (correctAnswer4 + correctAnswer4 * 0.1f).ToString("F2");  // +10%
        answerButtons4[1].GetComponentInChildren<Text>().text = (correctAnswer4 + correctAnswer4 * 0.2f).ToString("F2");  // +20%
        answerButtons4[2].GetComponentInChildren<Text>().text = correctAnswer4.ToString("F2");  // Correct answer
        answerButtons4[3].GetComponentInChildren<Text>().text = (correctAnswer4 + correctAnswer4 * 0.3f).ToString("F2");  // +30%
        answerButtons4[4].GetComponentInChildren<Text>().text = (correctAnswer4 + correctAnswer4 * 0.4f).ToString("F2");  // +40%

        // Add listeners to buttons to handle the answer
        for (int i = 0; i < answerButtons4.Length; i++)
        {
            int index = i;  // Capture the index for the listener
            answerButtons4[i].onClick.AddListener(() => CheckFourthQuestionAnswer(index, correctAnswer4));
        }
    }

    // Method to check the answer for the fourth question
    public void CheckFourthQuestionAnswer(int index, float correctAnswer)
    {
        if (index == 2)
        {
            Debug.Log("Correct answer for the fourth question!");
        }
        else
        {
            Debug.Log("Wrong answer for the fourth question.");
        }
    }

    public void SetFifthQuestionAnswers()
    {
        // Ensure correctAnswer3 and correctAnswer4 are valid
        if (correctAnswer3 <= 0 || correctAnswer4 <= 0)
        {
            Debug.LogError("correctAnswer3 or correctAnswer4 is invalid.");
            return;
        }

        // Calculate the correct answer for Question 5
        float correctAnswer5 = correctAnswer3 * correctAnswer4;

        // Log the correct answer for debugging
        Debug.Log("CorrectAnswer5: " + correctAnswer5);

        // Set the correct answer and the options
        answerButtons5[0].GetComponentInChildren<Text>().text = (correctAnswer5 + correctAnswer5 * 0.1f).ToString("F2");  // +10%
        answerButtons5[1].GetComponentInChildren<Text>().text = (correctAnswer5 + correctAnswer5 * 0.2f).ToString("F2");  // +20%
        answerButtons5[2].GetComponentInChildren<Text>().text = (correctAnswer5 + correctAnswer5 * 0.3f).ToString("F2");  // +30%
        answerButtons5[3].GetComponentInChildren<Text>().text = (correctAnswer5 + correctAnswer5 * 0.4f).ToString("F2");  // +40%
        answerButtons5[4].GetComponentInChildren<Text>().text = correctAnswer5.ToString("F2");  // Correct answer

        // Add listeners to buttons to handle the answer
        for (int i = 0; i < answerButtons5.Length; i++)
        {
            int index = i;  // Capture the index for the listener
            answerButtons5[i].onClick.AddListener(() => CheckFifthQuestionAnswer(index, correctAnswer5));
        }
    }

    public void CheckFifthQuestionAnswer(int index, float correctAnswer)
    {
        if (index == 4)
        {
            Debug.Log("Correct answer for the fifth question!");
        }
        else
        {
            Debug.Log("Wrong answer for the fifth question.");
        }
    }



    // Set all main panels inactive at the start
    void InitializePanels()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);  // Initially hide all main panels
        }

        // Ensure StartSoalModul2 button is visible and other buttons are hidden initially
        StartSoalModul2.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);    // Hide Next button initially
        closeButton.gameObject.SetActive(false);   // Hide Close button
    }

    // Set all soal panels inactive at the start
    void InitializeSoalPanels()
    {
        for (int i = 0; i < SoalPanel.Length; i++)
        {
            SoalPanel[i].SetActive(false);  // Initially hide all soal panels
        }
    }

    // Set all evaluasi panels inactive at the start
    void InitializeEvaluasiPanels()
    {
        for (int i = 0; i < EvaluasiPanel.Length; i++)
        {
            EvaluasiPanel[i].SetActive(false);  // Initially hide all evaluasi panels
        }
    }

    // Open the first panel and hide the StartSoalModul2 button
    public void OpenFirstPanel()
    {
        panels[currentPanelIndex].SetActive(true);   // Show the first panel
        StartSoalModul2.gameObject.SetActive(false); // Hide the Start button
        nextButton.gameObject.SetActive(true);       // Show the Next button for general navigation
    }

    // Open the next panel in the array
    public void NextPanel()
    {
        if (currentPanelIndex < panels.Length - 1)
        {
            panels[currentPanelIndex].SetActive(false);  // Hide the current panel
            currentPanelIndex++;  // Move to the next panel
            panels[currentPanelIndex].SetActive(true);   // Show the next panel
        }

        // If this is the special panel (2nd panel), hide Next button and show download panel
        if (currentPanelIndex == 1) // Assuming the download button is in the second panel
        {
            nextButton.gameObject.SetActive(false);  // Hide Next button
            downloadPanel.SetActive(true);           // Show download panel

            // Update Waktu Kumulatif and Waktu Proses Text
            UpdateWaktuTexts();
        }

        // If this is the last panel, close the main panels and open the first SoalPanel
        if (currentPanelIndex == panels.Length - 1)
        {
            CloseMainPanelsAndOpenSoal();
        }
    }

    // Method to update the Waktu Kumulatif and Waktu Proses for each row in the table
    public void UpdateWaktuTexts()
    {
        // Check if module2Manager is assigned
        if (module2Manager == null)
        {
            Debug.LogError("Module2Manager is not assigned.");
            return;
        }

        // Fetch assembly times from Module2Manager (Waktu Kumulatif)
        List<float> assemblyTimes = module2Manager.GetAssemblyTimes(); // Get Waktu Kumulatif

        // Loop through each round (max 10 rounds)
        for (int i = 0; i < 10; i++)
        {
            if (i < assemblyTimes.Count)
            {
                // Get Waktu Kumulatif and Waktu Proses for this round
                float waktuKumulatif = assemblyTimes[i];
                float waktuProses = i == 0 ? waktuKumulatif : assemblyTimes[i] - assemblyTimes[i - 1];

                // Convert to integer to remove decimal places
                int waktuKumulatifInt = Mathf.RoundToInt(waktuKumulatif);
                int waktuProsesInt = Mathf.RoundToInt(waktuProses);

                // Update the corresponding Text fields
                waktuKumulatifTexts[i].text = waktuKumulatifInt.ToString() + " detik";  // Update Waktu Kumulatif
                waktuProsesTexts[i].text = waktuProsesInt.ToString() + " detik";        // Update Waktu Proses
            }
            else
            {
                // If there is no data for this row, leave it empty
                waktuKumulatifTexts[i].text = "";
                waktuProsesTexts[i].text = "";
            }
        }

        // Once the texts are updated, generate the quiz with the latest data
        GenerateQuiz();
    }


    // Close the main panels and open the first SoalPanel
    public void CloseMainPanelsAndOpenSoal()
    {
        // Hide the current panel and buttons
        panels[currentPanelIndex].SetActive(false);
        nextButton.gameObject.SetActive(false);

        // Reset SoalPanel index and show the first soal panel
        currentSoalPanelIndex = 0;
        SoalPanel[currentSoalPanelIndex].SetActive(true);  // Show the first SoalPanel

    }

    void NextSoalPanel()
    {
        // Check if the current SoalPanel is not the last one
        if (currentSoalPanelIndex < SoalPanel.Length - 1)
        {
            // Hide the current SoalPanel
            SoalPanel[currentSoalPanelIndex].SetActive(false);

            // Move to the next panel
            currentSoalPanelIndex++;

            // Show the next SoalPanel
            SoalPanel[currentSoalPanelIndex].SetActive(true);

            // Hide the NextSoalButton initially
            NextSoalButton.gameObject.SetActive(false);

            // Move the image to Soal2 when it's opened (moving tabelWaktuKumulatif)
            if (currentSoalPanelIndex == 1)
            {
                MoveImageToSoal(tabelWaktuKumulatif, SoalPanel[1], false); // Automatically move image to Soal2 when opened
            }

            // Move the image to Soal5 when Soal5 is opened (moving tabelAllowance from Soal4 to Soal5)
            if (currentSoalPanelIndex == 4)
            {
                MoveImageToSoal(tabelAllowance, SoalPanel[4], false); // Automatically move image to Soal5 when opened
            }
        }
        else
        {
            // If it's the last SoalPanel, hide the SoalPanel and show the ScorePanel
            SoalPanel[currentSoalPanelIndex].SetActive(false);
            ShowScorePanel();
        }
    }


    void ShowScorePanel()
    {
        // Total number of questions
        int totalQuestions = SoalPanel.Length;

        // Calculate the score as a percentage
        float scorePercentage = (totalQuestions > 0) ? ((float)correctAnswersCount / totalQuestions) * 100 : 0;

        // Display the score in the ScoreText
        ScoreText.text = scorePercentage.ToString("F2") + "%";

        // Show the ScorePanel
        ScorePanel.SetActive(true);

        // Hide the NextSoalButton (as we don't need it anymore)
        NextSoalButton.gameObject.SetActive(false);

        // Show the EvaluasiButton
        EvaluasiButton.gameObject.SetActive(true);
        EvaluasiButton.onClick.RemoveAllListeners(); // Ensure previous listeners are cleared
        EvaluasiButton.onClick.AddListener(OpenEvaluasiPanel);
    }


    // Open the first evaluasi panel and close all SoalPanels
    public void OpenEvaluasiPanel()
    {
        // Hide the ScorePanel
        ScorePanel.SetActive(false);

        // Show the first EvaluasiPanel
        currentEvaluasiPanelIndex = 0;
        EvaluasiPanel[currentEvaluasiPanelIndex].SetActive(true);

        // Show the NextEvaluasiButton
        nextEvaluasiButton.gameObject.SetActive(true);
        nextEvaluasiButton.onClick.RemoveAllListeners();  // Remove any previous listeners
        nextEvaluasiButton.onClick.AddListener(NextEvaluasiPanel);

        // Display the answer options for each question and highlight the correct ones
        DisplayEvaluasiAnswers();
    }

    // Method to display the answer options and highlight the correct ones
    public void DisplayEvaluasiAnswers()
    {
        string[] optionLabels = { "A. ", "B. ", "C. ", "D. ", "E. " }; // Labels for options
        Color correctColor = Color.green;
        Color incorrectColor = Color.red;
        Color defaultColor = Color.white;

        // Display and highlight answers for each question
        for (int i = 0; i < evaluasiAnswerTextsQ1.Length; i++)
        {
            // Question 1
            evaluasiAnswerTextsQ1[i].text = optionLabels[i] + answerButtons1[i].GetComponentInChildren<Text>().text;
            evaluasiAnswerTextsQ1[i].color = (i == 0) ? correctColor : defaultColor; // Highlight correct answer in green
            if (userAnswers[0] == i && i != 0) // If user selected answer is incorrect, highlight in red
            {
                evaluasiAnswerTextsQ1[i].color = incorrectColor;
            }

            // Question 2
            evaluasiAnswerTextsQ2[i].text = optionLabels[i] + answerButtons2[i].GetComponentInChildren<Text>().text;
            evaluasiAnswerTextsQ2[i].color = (i == 4) ? correctColor : defaultColor; // Highlight correct answer in green
            if (userAnswers[1] == i && i != 4) // If user selected answer is incorrect, highlight in red
            {
                evaluasiAnswerTextsQ2[i].color = incorrectColor;
            }

            // Question 3
            evaluasiAnswerTextsQ3[i].text = optionLabels[i] + answerButtons3[i].GetComponentInChildren<Text>().text;
            evaluasiAnswerTextsQ3[i].color = (i == 0) ? correctColor : defaultColor; // Highlight correct answer in green
            if (userAnswers[2] == i && i != 0) // If user selected answer is incorrect, highlight in red
            {
                evaluasiAnswerTextsQ3[i].color = incorrectColor;
            }

            // Question 4
            evaluasiAnswerTextsQ4[i].text = optionLabels[i] + answerButtons4[i].GetComponentInChildren<Text>().text;
            evaluasiAnswerTextsQ4[i].color = (i == 2) ? correctColor : defaultColor; // Highlight correct answer in green
            if (userAnswers[3] == i && i != 2) // If user selected answer is incorrect, highlight in red
            {
                evaluasiAnswerTextsQ4[i].color = incorrectColor;
            }

            // Question 5
            evaluasiAnswerTextsQ5[i].text = optionLabels[i] + answerButtons5[i].GetComponentInChildren<Text>().text;
            evaluasiAnswerTextsQ5[i].color = (i == 4) ? correctColor : defaultColor; // Highlight correct answer in green
            if (userAnswers[4] == i && i != 4) // If user selected answer is incorrect, highlight in red
            {
                evaluasiAnswerTextsQ5[i].color = incorrectColor;
            }
        }
    }

    // Open the next EvaluasiPanel in the array
    public void NextEvaluasiPanel()
    {
        // Check if we are not on the last evaluasi panel
        if (currentEvaluasiPanelIndex < EvaluasiPanel.Length - 1)
        {
            EvaluasiPanel[currentEvaluasiPanelIndex].SetActive(false);  // Hide the current evaluasi panel
            currentEvaluasiPanelIndex++;  // Move to the next evaluasi panel
            EvaluasiPanel[currentEvaluasiPanelIndex].SetActive(true);   // Show the next evaluasi panel
        }

        // If it's the last evaluasi panel, hide the NextEvaluasiButton
        if (currentEvaluasiPanelIndex == EvaluasiPanel.Length - 1)
        {
            nextEvaluasiButton.gameObject.SetActive(false);  // Hide the button when all evaluasi panels are shown
        }
    }

    // Method to reopen Soal1 and move the image back to Soal1
    public void OpenSoalPanel1()
    {
        OpenSpecificSoalPanel(0); // Open the first SoalPanel

        // Move the image back to Soal1
        MoveImageToSoal(tabelWaktuKumulatif, SoalPanel[0], true); // Move image (tabelWaktuKumulatif) back to Soal1
    }

    public void OpenSoalPanel2()
    {
        OpenSpecificSoalPanel(1); // Open the second SoalPanel
    }

    public void OpenSoalPanel3()
    {
        OpenSpecificSoalPanel(2); // Open the third SoalPanel
    }

    public void OpenSoalPanel4()
    {
        OpenSpecificSoalPanel(3); // Open the fourth SoalPanel

        // Move the image back to Soal4
        MoveImageToSoal(tabelAllowance, SoalPanel[3], true); // Move image (tabelAllowance) back to Soal4
    }

    public void OpenSoalPanel5()
    {
        OpenSpecificSoalPanel(4); // Open the fifth SoalPanel
    }

    // Helper method to open a specific SoalPanel and hide the SoalList
    public void OpenSpecificSoalPanel(int panelIndex)
    {

        // Close all SoalPanels first before opening the selected one
        foreach (GameObject panel in SoalPanel)
        {
            panel.SetActive(false);  // Ensure all SoalPanels are hidden
        }

        // Show the selected SoalPanel if the index is valid
        if (panelIndex >= 0 && panelIndex < SoalPanel.Length)
        {
            SoalPanel[panelIndex].SetActive(true);  // Open the specific SoalPanel
            currentSoalPanelIndex = panelIndex;  // Update the current panel index to the newly opened panel

            // Move the image back to Soal1 when reopening it
            if (panelIndex == 0)
            {
                MoveImageToSoal(tabelWaktuKumulatif, SoalPanel[0], true); // Move image back to Soal1
            }

            // Move the image back to Soal4 when reopening it
            if (panelIndex == 3)
            {
                MoveImageToSoal(tabelAllowance, SoalPanel[3], true); // Move image back to Soal4
            }
        }
    }


    // Action for the download button
    public void DownloadAction()
    {
        // Trigger CSV download
        ExportCSV();

        // Simulate download action (you can implement actual functionality if needed)
        downloadPanel.SetActive(false);             // Hide Download panel
        nextButton.gameObject.SetActive(true);      // Show Next button after download is clicked
    }

    // Generate CSV content
    public string GenerateCSVFile(List<PerformanceData> performanceData)
    {
        // Check if module2Manager is assigned
        if (module2Manager == null)
        {
            Debug.LogError("Module2Manager is not assigned.");
            return "";
        }

        // CSV headers for the first section (Waktu Kumulatif and Waktu Proses)
        string[] headers = { "No", "Waktu Kumulatif", "Waktu Proses" };
        string[] wprHeaders = { "No", "Factor", "Class", "Symbol", "Rating" };  // Headers for WPR section
        string[] allowanceHeaders = { "Type of Allowance", "Nilai" }; // Headers for Allowance section

        // Build the CSV content
        StringBuilder csv = new StringBuilder();

        // --- Output Assembly Time Table ---
        csv.AppendLine("Output Assembly Time");  // Header for Assembly Time Table

        // Add headers for Waktu Kumulatif section
        csv.AppendLine(string.Join(";", headers));

        // Fetch assembly times from Module2Manager
        List<float> assemblyTimes = module2Manager.GetAssemblyTimes();
        List<float> waktuProsesList = new List<float>();

        // Calculate Waktu Proses from Waktu Kumulatif
        for (int i = 0; i < assemblyTimes.Count; i++)
        {
            float waktuKumulatif = assemblyTimes[i];
            float waktuProses = i == 0 ? waktuKumulatif : assemblyTimes[i] - assemblyTimes[i - 1];
            waktuProsesList.Add(waktuProses);
        }

        // Add data for Waktu Kumulatif and Waktu Proses
        for (int i = 0; i < assemblyTimes.Count; i++)
        {
            string no = (i + 1).ToString();   // No. starts from 1
            string waktuKumulatif = assemblyTimes[i].ToString("F1");  // Format to 1 decimal point
            string waktuProses = waktuProsesList[i].ToString("F1");

            // Add row for Waktu Kumulatif section
            csv.AppendLine(string.Join(";", no, waktuKumulatif, waktuProses));
        }

        // Add a blank line to separate sections
        csv.AppendLine();

        // --- Output Work Performance Table ---
        csv.AppendLine("Output Work Performance");  // Header for Work Performance Table

        // Add headers for WPR section
        csv.AppendLine(string.Join(";", wprHeaders));

        // Add the performance data from WPR if available
        string[] factors = { "Skill", "Effort", "Condition", "Consistency" };
        for (int i = 0; i < performanceData.Count; i++)
        {
            string no = (i + 1).ToString();
            string factor = factors[i % factors.Length]; // Loop through factors if more than available
            string classText = performanceData[i].Class;
            string symbol = performanceData[i].Symbol;
            string rating = performanceData[i].Rating;

            // Add row for WPR data
            csv.AppendLine(string.Join(";", no, factor, classText, symbol, rating));
        }

        // Calculate the total rating and add to the final row if performance data is available
        if (performanceData.Count > 0)
        {
            float totalScore = CalculateTotalScore(performanceData);
            csv.AppendLine(string.Join(";", "", "Total", "", "", totalScore.ToString("F2")));  // Leave first column empty for "Total" row
        }

        // Add a blank line to separate sections
        csv.AppendLine();

        // --- Output Recommended Allowance Table ---
        csv.AppendLine("Output Recommended Allowance");  // Header for Allowance Table

        // Add headers for Allowance section
        csv.AppendLine(string.Join(";", allowanceHeaders));

        // Add the Constant, Variable, and Total Allowances
        float constantAllowance = recAllowance.ConstantAllowance;
        float variableAllowance = recAllowance.VariableAllowance;
        float totalAllowance = recAllowance.TotalAllowance;

        csv.AppendLine(string.Join(";", "Constant allowances %", constantAllowance.ToString("F2")));
        csv.AppendLine(string.Join(";", "Variable allowances %", variableAllowance.ToString("F2")));
        csv.AppendLine(string.Join(";", "Total allowances %", totalAllowance.ToString("F2")));

        return csv.ToString();
    }


    // Export the CSV file (only works in a WebGL build)
    public void ExportCSV()
    {
        // Ensure that assembly times and allowances data are available
        List<float> assemblyTimes = module2Manager.GetAssemblyTimes();
        if (assemblyTimes == null || assemblyTimes.Count == 0)
        {
            Debug.LogError("No assembly times available for export.");
            return;
        }

        // Ensure that recAllowance is properly assigned
        if (recAllowance == null)
        {
            Debug.LogError("RecAllowance is not assigned.");
            return;
        }

        // Build the CSV content with available data
        string csv = GenerateCSVFile(storedPerformanceData);

        IDownloadFileCSVWebGLService downloadFileCSVWebGL = ServiceLocator.GetService<IDownloadFileCSVWebGLService>();
        downloadFileCSVWebGL.DownloadFileCSV("Data Module Time Study.csv", csv);

        Debug.Log("CSV file has been generated and downloaded.");
    }

    // Close the current panel if it's the last one and end the scene
    public void ClosePanel()
    {
        if (currentPanelIndex == panels.Length - 1)
        {
            panels[currentPanelIndex].SetActive(false);  // Hide the current panel
            closeButton.gameObject.SetActive(false);     // Hide the Close button after closing the last panel

            // Option 1: Quit the application (for standalone build)
            Application.Quit();

            // Option 2: Load a new scene (uncomment and use if you want to switch scenes)
            // SceneManager.LoadScene("YourNextSceneName");

            // Note: Application.Quit() only works in builds, not in the editor
        }
    }

    // Method for returning home (load a new scene)
    public void ReturnHome()
    {
        SceneManager.LoadScene("LabAwal");  // Replace with the name of your home scene
    }
}