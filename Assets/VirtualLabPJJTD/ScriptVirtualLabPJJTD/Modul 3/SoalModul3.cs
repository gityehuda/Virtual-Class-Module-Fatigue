using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using Photon.Pun;

[System.Serializable]
public struct SoalPanel
{
    public GameObject panel;
    public List<Button> optionButtons;
    public int correctAnswerIndex;
}

public class SoalModul3 : MonoBehaviour
{
    // Array to hold the panels (as before)
    public SoalPanel[] ListSoal;

    // Index to track the current panel
    private int currentIndex = 0;
    public Button nextButton;
    public Button prevButton;

    // References to the images and buttons
    public GameObject NIOSHImage; 
    public GameObject CNSFImage; 
    public Button NIOSHTableButton; 
    public Button CNSFTableButton;  

    // Start button that will trigger the start of the process
    public Button startSoalButton;

    public Color activeColor = Color.gray;
    public Color inactiveColor = Color.white;

    [Header("Data")]
    public int savedBeratBeban;
    public int savedWaktu;
    public int savedRepitisi;
    public float savedFM;

    public TextMeshProUGUI beratBebanText;  
    public TextMeshProUGUI waktuText;       
    public TextMeshProUGUI repitisiText;
    public TextMeshProUGUI mLoadText;
    public TextMeshProUGUI fmText;

    [Header("Additional Variables")]
    public float LLoad = 24f;     // cm
    public float Lbody = 34f;     // cm
    public float Mload;          // Will be the savedBeratBeban
    public float Mbody = 91f;    // kg
    public float SINθ = 0.51503f;        // cm    sin 31^o= 0.51503
    public float COSθ = 0.8572f;         // cm    cos 31^o = 0.8572
    public float d = 0.05f;      // m
    public float g = 10f;        // m/s^2
    public float CM = 0.9f;

    public float Hawal = 24f;    // cm
    public float Hakhir = 34f;   // cm
    public float Vawal = 75f;    // cm
    public float Vakhir = 13f;   // cm
    public int Frequency;        // Will be the savedRepitisi
    public float Weight;         // Will be the savedBeratBeban
    public float Lc = 23f;       // kg
    public float Waktu;          // Will be the savedWaktu
    public float Aawal = 0f;     // degrees
    public float Aakhir = 45f;   // degrees

    public float Lw;

    [Header("Hasil RWL")]
    public float RWL_Awal;      
    public float RWL_Akhir;

    [Header("Hasil LI")]
    public float LIawal; 
    public float LIakhir;

    [Header("Hasil Muscle Force")]
    public float Mexternal;     
    public float Minternal;     
    public float Fmuscle;

    [Header("Hasil FCompressive")]
    public float Fcompression;

    [Header("Hasil Shear Force")]
    public float Fshear;

    private int currentSelectedAnswerIndex = -1;

    private float LIAwalOptionSalah1;
    private float LIAwalOptionSalah2;
    private float LIAwalOptionSalah3;
    private float LIAkhirOptionSalah1;
    private float LIAkhirOptionSalah2;
    private float LIAkhirOptionSalah3;
    private float Mexternal2;
    private float Minternal2;
    private float FmuscleOptionSalah1;
    private float FmuscleOptionSalah2;
    private float FmuscleOptionSalah3;

    bool[] questionsAnswered;
    public Button finishButton;
    public GameObject scorePanel;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private int totalQuestions;
    private int[] userAnswers;

    public GameObject[] EvaluasiPanel;
    public Button openEvaluasiButton;
    public Button nextEvaluasiButton;
    public Button prevEvaluasiButton;
    private int currentEvaluasiIndex = 0;

    public Button moveToSceneButton;

    [Header("Evaluasi Answer Texts")]
    public TextMeshProUGUI[] evaluasiAnswerQ1;
    public TextMeshProUGUI[] evaluasiAnswerQ2;
    public TextMeshProUGUI[] evaluasiAnswerQ3;
    public TextMeshProUGUI[] evaluasiAnswerQ4;
    public TextMeshProUGUI[] evaluasiAnswerQ5;
    public TextMeshProUGUI[] evaluasiAnswerQ6;

    [Header("Colors for Evaluation Feedback")]
    public Color correctColor = Color.green; // Highlight for correct answers
    public Color incorrectColor = Color.red; // Highlight for incorrect answers
    public Color defaultColor = Color.white; // Neutral color for unselected options



    // Start is no longer responsible for the initialization
    void Start()
    {
        // Initialize buttons with listeners
        NIOSHTableButton.onClick.AddListener(ShowNIOSHImage);
        CNSFTableButton.onClick.AddListener(ShowCNSFImage);
        nextButton.onClick.AddListener(ShowNextPanel);
        prevButton.onClick.AddListener(ShowPrevPanel);

        // Set up the start button to begin the process
        startSoalButton.onClick.AddListener(StartGame);

        // Initially hide navigation buttons and images
        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);
        NIOSHImage.SetActive(false);
        CNSFImage.SetActive(false);
        NIOSHTableButton.gameObject.SetActive(false);
        CNSFTableButton.gameObject.SetActive(false);

        questionsAnswered = new bool[ListSoal.Length];

        // Ensure the Finish button is initially hidden
        finishButton.gameObject.SetActive(false);
        finishButton.onClick.AddListener(OnFinishButtonClicked);
        scorePanel.SetActive(false);
        totalQuestions = ListSoal.Length;

        userAnswers = new int[ListSoal.Length];
        for (int i = 0; i < userAnswers.Length; i++)
        {
            userAnswers[i] = -1; // -1 means no answer selected yet
        }

        if (EvaluasiPanel.Length > 0)
        {
            foreach (GameObject panel in EvaluasiPanel)
            {
                panel.SetActive(false);
            }
        }

        // Initialize buttons
        openEvaluasiButton.onClick.AddListener(OpenEvaluasiPanels);
        nextEvaluasiButton.onClick.AddListener(ShowNextEvaluasiPanel);
        prevEvaluasiButton.onClick.AddListener(ShowPrevEvaluasiPanel); // Add listener for prev button

        // Hide the next button initially
        nextEvaluasiButton.gameObject.SetActive(false);
        // Ensure prevEvaluasiButton is initially hidden
        prevEvaluasiButton.gameObject.SetActive(false);

        if (moveToSceneButton != null)
        {
            moveToSceneButton.onClick.AddListener(MoveToScene);
        }
    }

    void MoveToScene()
    {
        SceneManager.LoadScene("LabAwal"); // Replace "TargetSceneName" with your scene's name
    }

    void OpenEvaluasiPanels()
    {
        if (EvaluasiPanel.Length > 0)
        {
            // Show the first panel
            EvaluasiPanel[0].SetActive(true);

            // Show the next button if there are multiple panels
            if (EvaluasiPanel.Length > 1)
            {
                nextEvaluasiButton.gameObject.SetActive(true);
            }

            // Hide the open button after starting the evaluation
            openEvaluasiButton.gameObject.SetActive(false);
        }
    }

    // Method to show the next panel in the EvaluasiPanel array
    void ShowNextEvaluasiPanel()
    {
        // Hide the current panel
        EvaluasiPanel[currentEvaluasiIndex].SetActive(false);

        // Move to the next panel
        currentEvaluasiIndex++;

        // Show the next panel
        if (currentEvaluasiIndex < EvaluasiPanel.Length)
        {
            EvaluasiPanel[currentEvaluasiIndex].SetActive(true);
        }

        // Hide the next button if the last panel is reached
        if (currentEvaluasiIndex >= EvaluasiPanel.Length - 1)
        {
            nextEvaluasiButton.gameObject.SetActive(false);
        }

        prevEvaluasiButton.gameObject.SetActive(true);
    }

    void ShowPrevEvaluasiPanel()
    {
        // Hide the current panel
        EvaluasiPanel[currentEvaluasiIndex].SetActive(false);

        // Move to the previous panel
        currentEvaluasiIndex--;

        // Show the previous panel
        if (currentEvaluasiIndex >= 0)
        {
            EvaluasiPanel[currentEvaluasiIndex].SetActive(true);
        }

        // Hide the prev button if it's the first panel
        if (currentEvaluasiIndex <= 0)
        {
            prevEvaluasiButton.gameObject.SetActive(false);
        }

        // Ensure the next button is visible when not at the last panel
        nextEvaluasiButton.gameObject.SetActive(true);
    }


    public void SetData(int beratBeban, int waktu, int repitisi, float FM)
    {
        savedBeratBeban = beratBeban;
        savedWaktu = waktu;
        savedRepitisi = repitisi;
        savedFM = FM;

        // Assign the values to the additional variables
        Mload = savedBeratBeban;      // kg
        Frequency = savedRepitisi;    // from savedRepitisi
        Weight = savedBeratBeban;     // kg
        Waktu = savedWaktu;           // minutes

        for (int i = 0; i < ListSoal.Length; i++)
        {
            // Example: Update options based on the panel (customize as needed)
            foreach (Button button in ListSoal[i].optionButtons)
            {
                button.onClick.AddListener(() => OnOptionSelected(i));  // Handle option selection
            }
        }

        RWL_Awal = CalculateRWL_Awal();
        RWL_Akhir = CalculateRWL_Akhir();

        LIawal = CalculateLIawal(savedBeratBeban, RWL_Awal);
        LIakhir = CalculateLIakhir(savedBeratBeban, RWL_Akhir);

        UpdateMuscleForceCalculation();
        UpdateMuscleForceCalculationOptionSalah1();
        UpdateMuscleForceCalculationOptionSalah2();
        UpdateMuscleForceCalculationOptionSalah3();
        CalculateCompressiveForce();
        CalculateShearForce();

        CalculateLIAwalOptionSalah1();
        CalculateLIAwalOptionSalah2();
        CalculateLIAwalOptionSalah3();
        CalculateLIAkhirOptionSalah1();
        CalculateLIAkhirOptionSalah2();
        CalculateLIAkhirOptionSalah3();

        UpdateUI();

        SetupQuestion1Options();
        SetupQuestion2Options();
        SetupQuestion3Options();
        SetupQuestion4Options();
        SetupQuestion5Options();
        SetupQuestion6Options();
    }

    private void OnOptionSelected(int i)
    {
        throw new NotImplementedException();
    }

    // Start the game when the Start button is clicked
    void StartGame()
    {
        // Show the NIOSH image by default when starting the game
        ShowNIOSHImage();

        // Show the navigation buttons and panels
        nextButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(true);
        NIOSHTableButton.gameObject.SetActive(true);
        CNSFTableButton.gameObject.SetActive(true);

        // Optionally, update panel visibility
        UpdatePanelVisibility();
    }

    // Method to show the NIOSH image
    void ShowNIOSHImage()
    {
        // Hide the CNSF image and show the NIOSH image
        NIOSHImage.SetActive(true);
        CNSFImage.SetActive(false);

        // Update button appearance
        SetButtonAppearance(NIOSHTableButton, false);
        SetButtonAppearance(CNSFTableButton, true);
    }

    // Method to show the CNSF image
    void ShowCNSFImage()
    {
        // Hide the NIOSH image and show the CNSF image
        NIOSHImage.SetActive(false);
        CNSFImage.SetActive(true);

        // Update button appearance
        SetButtonAppearance(NIOSHTableButton, true);
        SetButtonAppearance(CNSFTableButton, false);
    }

    // Method to set button appearance (color change for active and inactive state)
    void SetButtonAppearance(Button button, bool isActive)
    {
        // Adjust the button's color based on its active state
        Image buttonImage = button.GetComponent<Image>();

        if (isActive)
        {
            // Darken the active button and disable interaction
            buttonImage.color = activeColor;
            button.enabled = true; // Enable interaction for active button
        }
        else
        {
            // Lighten the inactive button and allow interaction
            buttonImage.color = inactiveColor;
            button.enabled = false; // Disable interaction for inactive button
        }
    }

    // Method to show the next panel (for ListSoal array)
    void ShowNextPanel()
    {
        if (currentIndex < ListSoal.Length - 1)
        {
            currentIndex++;
            UpdatePanelVisibility();

            if (currentIndex == 1) // When it's the second question
            {
                SetupQuestion2Options();
            }
            else if (currentIndex == 2) // When it's the third question
            {
                SetupQuestion3Options();
            }
            else if (currentIndex == 3) // When it's the fourth question
            {
                SetupQuestion4Options();
            }
            else if (currentIndex == 4) // When it's the fourth question
            {
                SetupQuestion5Options();
            }
            else if (currentIndex == 5) // When it's the fourth question
            {
                SetupQuestion6Options();
            }
        }
    }

    // Method to show the previous panel (for ListSoal array)
    void ShowPrevPanel()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdatePanelVisibility();
        }
    }

    // Method to update the visibility of panels (for ListSoal array)
    void UpdatePanelVisibility()
    {
        // Hide all panels first
        foreach (SoalPanel soalPanel in ListSoal)
        {
            soalPanel.panel.SetActive(false);
            foreach (Button optionButton in soalPanel.optionButtons)
            {
                optionButton.gameObject.SetActive(false);  // Hide all option buttons
            }
        }

        // Show the current panel
        if (ListSoal.Length > 0)
        {
            ListSoal[currentIndex].panel.SetActive(true);

            // Show the corresponding option buttons
            foreach (Button optionButton in ListSoal[currentIndex].optionButtons)
            {
                optionButton.gameObject.SetActive(true);

                // Add listener for each option button
                optionButton.onClick.RemoveAllListeners();  // Ensure no old listeners exist
                optionButton.onClick.AddListener(() => OnOptionSelected(optionButton));
            }
        }

        // Hide the Prev button if on the first panel
        if (currentIndex == 0)
        {
            prevButton.gameObject.SetActive(false);
        }
        else
        {
            prevButton.gameObject.SetActive(true);
        }

        // Hide the Next button if on the last panel
        if (currentIndex == ListSoal.Length - 1)
        {
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            nextButton.gameObject.SetActive(true);
        }
    }

    void OnFinishButtonClicked()
    {
        HideAllPanels();

        // Optionally, hide images and buttons
        NIOSHImage.SetActive(false);
        CNSFImage.SetActive(false);
        NIOSHTableButton.gameObject.SetActive(false);
        CNSFTableButton.gameObject.SetActive(false);
        finishButton.gameObject.SetActive(false);

        // If you want to hide all other UI elements, you can deactivate them similarly.
        // For example:
        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);

        score = 0;
        for (int i = 0; i < userAnswers.Length; i++)
        {
            if (userAnswers[i] == ListSoal[i].correctAnswerIndex)
            {
                score++;
            }
            DisplayEvaluasiAnswers(i);
        }

        // Show the score panel
        ShowScorePanel();
    }

    void DisplayEvaluasiAnswers(int questionIndex)
    {
        TextMeshProUGUI[] evaluasiTexts = null;

        // Determine the corresponding evaluasi text array
        switch (questionIndex)
        {
            case 0:
                evaluasiTexts = evaluasiAnswerQ1;
                break;
            case 1:
                evaluasiTexts = evaluasiAnswerQ2;
                break;
            case 2:
                evaluasiTexts = evaluasiAnswerQ3;
                break;
            case 3:
                evaluasiTexts = evaluasiAnswerQ4;
                break;
            case 4:
                evaluasiTexts = evaluasiAnswerQ5;
                break;
            case 5:
                evaluasiTexts = evaluasiAnswerQ6;
                break;
        }

        if (evaluasiTexts != null)
        {
            SoalPanel question = ListSoal[questionIndex];

            for (int j = 0; j < question.optionButtons.Count; j++)
            {
                // Set the default color first
                evaluasiTexts[j].color = defaultColor;

                // Update evaluasi text with the option text
                evaluasiTexts[j].text = question.optionButtons[j].GetComponentInChildren<TextMeshProUGUI>().text;

                // Highlight the correct answer
                if (j == question.correctAnswerIndex)
                {
                    evaluasiTexts[j].color = correctColor; // Correct answer
                }

                // Highlight the user's incorrect selection
                if (userAnswers[questionIndex] == j && j != question.correctAnswerIndex)
                {
                    evaluasiTexts[j].color = incorrectColor; // Incorrect user-selected answer
                }
            }
        }
    }


    void ShowScorePanel()
    {
        // Activate the score panel
        scorePanel.SetActive(true);

        // Calculate the percentage score
        float percentage = (float)score / totalQuestions * 100;

        // Display the score as a percentage on the TextMeshProUGUI component
        if (scoreText != null)
        {
            scoreText.text = $"{percentage:F1}%";  // F1 to show one decimal point
        }
    }   

    void HideAllPanels()
    {
        foreach (var soalPanel in ListSoal)
        {
            if (soalPanel.panel != null)
            {
                soalPanel.panel.SetActive(false); // Hide the panel GameObject
            }
        }
    }

    void OnOptionSelected(Button selectedButton)
    {
        // Get the current panel's correct answer index
        int selectedIndex = ListSoal[currentIndex].optionButtons.IndexOf(selectedButton);

        userAnswers[currentIndex] = selectedIndex;

        // Log debug information whether the answer is correct or incorrect
        if (selectedIndex == ListSoal[currentIndex].correctAnswerIndex) score++;

        // Update the current selected answer index
        currentSelectedAnswerIndex = selectedIndex;

        questionsAnswered[currentIndex] = true;

        // Check if all questions are answered
        CheckIfAllQuestionsAnswered();

        // Allow the user to change their answer
        foreach (Button optionButton in ListSoal[currentIndex].optionButtons)
        {
            optionButton.interactable = true;  // Make sure all options are interactable again
        }

        // Disable the option buttons after an answer is selected
        foreach (Button optionButton in ListSoal[currentIndex].optionButtons)
        {
            if (ListSoal[currentIndex].optionButtons.IndexOf(optionButton) == selectedIndex)
            {
                optionButton.interactable = false;  // Disable the selected option
            }
        }
    }

    void CheckIfAllQuestionsAnswered()
    {
        // Check if all questions have been answered
        bool allAnswered = true;
        foreach (bool answered in questionsAnswered)
        {
            if (!answered)
            {
                allAnswered = false;
                break;
            }
        }

        // Show the Finish button if all questions are answered
        if (allAnswered)
        {
            ShowFinishButton();
        }
    }

    void ShowFinishButton()
    {
        // Assuming you have a reference to the Finish button
        finishButton.gameObject.SetActive(true);
    }

    void UpdateUI()
    {
        // Update the text fields to display the saved values
        if (beratBebanText != null) beratBebanText.text = savedBeratBeban.ToString() + " Kg";
        if (waktuText != null) waktuText.text = savedWaktu.ToString() + " Menit";
        if (repitisiText != null) repitisiText.text = savedRepitisi.ToString() + " Kali";
        if (mLoadText != null) mLoadText.text = savedBeratBeban.ToString() + " Kg";
        if (fmText != null) fmText.text = savedFM.ToString();
    }

    public float CalculateRWL_Awal()
    {
        // Calculate each component of the formula
        float HM = 25 / Hawal;
        float VMawal = 1 - (0.003f * Mathf.Abs(Vawal - 75));
        float D = Mathf.Abs(Vawal - Vakhir);
        float DM = 0.82f + (4.5f / D);
        float AMawal = 1 - (0.0032f * Aawal);

        // Calculate RWL Awal
        float RWL_Awal = Lc * HM * VMawal * DM * AMawal * savedFM * CM;

        float RWL_AwalOptionSalah1 = Lc * HM * VMawal * DM * AMawal * 0.31f * CM;
        float RWL_AwalOptionSalah2 = Lc * HM * VMawal * DM * AMawal * 0.28f * CM;
        float RWL_AwalOptionSalah3 = Lc * HM * VMawal * DM * AMawal * 0f * CM;

        return RWL_Awal;
    }

    public float CalculateRWL_Akhir()
    {
        // Calculate each component of the formula
        float HM = 25 / Hakhir;
        float VMakhir = 1 - (0.003f * Mathf.Abs(Vakhir - 75));
        float D = Mathf.Abs(Vawal - Vakhir);
        float DM = 0.82f + (4.5f / D);
        float AMakhir = 1 - (0.0032f * Aakhir);

        // Calculate RWL Awal
        float RWL_Akhir = Lc * HM * VMakhir * DM * AMakhir * savedFM * CM;

        float RWL_AkhirOptionSalah1 = Lc * HM * VMakhir * DM * AMakhir * 0.31f * CM;
        float RWL_AkhirOptionSalah2 = Lc * HM * VMakhir * DM * AMakhir * 0.28f * CM;
        float RWL_AkhirOptionSalah3 = Lc * HM * VMakhir * DM * AMakhir * 0.28f * CM;

        return RWL_Akhir;
    }

    // Method to calculate LIawal
    public float CalculateLIawal(float Lw, float RWL_Awal)
    {
        if (RWL_Awal == 0)
        {
            Debug.LogWarning("RWL_Awal is zero, result will be Infinity.");
        }
        return Lw / RWL_Awal;
    }

    public void CalculateLIAwalOptionSalah1()
    {
        // Ensure RWL_AwalOptionSalah1 is calculated first
        float RWL_AwalOptionSalah1 = Lc * (25 / Hawal) * (1 - (0.003f * Mathf.Abs(Vawal - 75))) * (0.82f + (4.5f / Mathf.Abs(Vawal - Vakhir))) * (1 - (0.0032f * Aawal)) * 0.31f * CM;

        // Calculate LIAwalOptionSalah1
        LIAwalOptionSalah1 = savedBeratBeban / RWL_AwalOptionSalah1;
    }

    public void CalculateLIAwalOptionSalah2()
    {
        // Ensure RWL_AwalOptionSalah2 is calculated first
        float RWL_AwalOptionSalah2 = Lc * (25 / Hawal) * (1 - (0.003f * Mathf.Abs(Vawal - 75))) * (0.82f + (4.5f / Mathf.Abs(Vawal - Vakhir))) * (1 - (0.0032f * Aawal)) * 0.28f * CM;

        // Calculate LIAwalOptionSalah2
        LIAwalOptionSalah2 = savedBeratBeban / RWL_AwalOptionSalah2;
    }

    public void CalculateLIAwalOptionSalah3()
    {
        // Ensure RWL_AwalOptionSalah3 is calculated first
        float RWL_AwalOptionSalah3 = Lc * (25 / Hawal) * (1 - (0.003f * Mathf.Abs(Vawal - 75))) * (0.82f + (4.5f / Mathf.Abs(Vawal - Vakhir))) * (1 - (0.0032f * Aawal)) * 0.34f * CM;

        // Calculate LIAwalOptionSalah3
        LIAwalOptionSalah3 = savedBeratBeban / RWL_AwalOptionSalah3;
    }

    // Method to calculate LIakhir
    public float CalculateLIakhir(float Lw, float RWL_Akhir)
    {
        if (RWL_Akhir == 0)
        {
            Debug.LogWarning("RWL_Akhir is zero, result will be Infinity.");
        }
        return Lw / RWL_Akhir;
    }

    public void CalculateLIAkhirOptionSalah1()
    {
        // Ensure RWL_AkhirOptionSalah1 is calculated first
        float RWL_AkhirOptionSalah1 = Lc * (25 / Hawal) * (1 - (0.003f * Mathf.Abs(Vawal - 75))) * (0.82f + (4.5f / Mathf.Abs(Vawal - Vakhir))) * (1 - (0.0032f * Aawal)) * 0.31f * CM;

        // Calculate LIAkhirOptionSalah1
        LIAkhirOptionSalah3 = savedBeratBeban / RWL_AkhirOptionSalah1;
    }

    public void CalculateLIAkhirOptionSalah2()
    {
        // Ensure RWL_AkhirOptionSalah2 is calculated first
        float RWL_AkhirOptionSalah2 = Lc * (25 / Hawal) * (1 - (0.003f * Mathf.Abs(Vawal - 75))) * (0.82f + (4.5f / Mathf.Abs(Vawal - Vakhir))) * (1 - (0.0032f * Aawal)) * 0.28f * CM;

        // Calculate LIAkhirOptionSalah2
        LIAkhirOptionSalah2 = savedBeratBeban / RWL_AkhirOptionSalah2;
    }

    public void CalculateLIAkhirOptionSalah3()
    {
        // Ensure RWL_AkhirOptionSalah3 is calculated first
        float RWL_AkhirOptionSalah3 = Lc * (25 / Hawal) * (1 - (0.003f * Mathf.Abs(Vawal - 75))) * (0.82f + (4.5f / Mathf.Abs(Vawal - Vakhir))) * (1 - (0.0032f * Aawal)) * 0.34f * CM;

        // Calculate LIAkhirOptionSalah3
        LIAkhirOptionSalah3 = savedBeratBeban / RWL_AkhirOptionSalah3;
    }

    // Method to categorize risk levels
    public string CategorizeRisk(float LI)
    {
        if (LI <= 1)
        {
            return "Very Low Risk";
        }
        else if (LI > 1 && LI <= 3)
        {
            return "Moderate Risk";
        }
        else // LI > 3
        {
            return "Very High Risk";
        }
    }

    // Example to display results for all LIs
    public void DisplayRiskLevels()
    {
        float LIawal = CalculateLIawal(Lw, RWL_Awal);

        float LIakhir = CalculateLIakhir(Lw, RWL_Akhir);

        CalculateLIAwalOptionSalah1();

        CalculateLIAwalOptionSalah2();

        CalculateLIAwalOptionSalah3();

        CalculateLIAkhirOptionSalah1();

        CalculateLIAkhirOptionSalah2();

        CalculateLIAkhirOptionSalah3();
    }

    // Function to calculate Mexternal
    public float CalculateMexternal()
    {
        // Mexternal formula:
        // Mexternal = [-(68.8% * Mbody * g) * Lbody] + [-(Mload * g) * Lload]
        float term1 = -((0.688f * Mbody * g) * Lbody);
        float term2 = -((Mload * g) * LLoad);

        // Sum the terms for Mexternal
        return term1 + term2;
    }

    public float CalculateMexternal2()
    {
        // Mexternal formula:
        // Mexternal = [-(68.8% * Mbody * g) * Lbody] - [-(Mload * g) * Lload]
        float term3 = -((0.688f * Mbody * g) * Lbody);
        float term4 = -((Mload * g) * LLoad);

        // Sum the terms for Mexternal
        return term3 - term4;
    }

    public void UpdateMuscleForceCalculation()
    {
        // Calculate Mexternal
        Mexternal = CalculateMexternal();

        // Calculate Minternal (which is the opposite of Mexternal)
        Minternal = -Mexternal;

        // Calculate Muscle Force (Fmuscle)
        Fmuscle = Minternal / d;
    }

    public void UpdateMuscleForceCalculationOptionSalah1()
    {
        // Calculate Mexternal
        Mexternal = CalculateMexternal2();

        // Calculate Minternal (which is the opposite of Mexternal)
        Minternal = -Mexternal;

        // Calculate Muscle Force (Fmuscle)
        FmuscleOptionSalah1 = Minternal / d;
    }

    public void UpdateMuscleForceCalculationOptionSalah2()
    {
        // Calculate Mexternal
        Mexternal = CalculateMexternal();

        // Calculate Minternal (which is the opposite of Mexternal)
        Minternal = -Mexternal;

        // Calculate Muscle Force (Fmuscle)
        FmuscleOptionSalah2  = Minternal / ((d/2) + 0.05f);
    }

    public void UpdateMuscleForceCalculationOptionSalah3()
    {
        // Calculate Mexternal
        Mexternal = CalculateMexternal();

        // Calculate Minternal (which is the opposite of Mexternal)
        Minternal = -Mexternal;

        // Calculate Muscle Force (Fmuscle)
        FmuscleOptionSalah3 = Minternal / ((d / 2) + 0.02f);
    }

    public void CalculateCompressiveForce()
    {

        // Calculate the compressive force
        Fcompression = (Mbody * g * SINθ) + (Mload * g * SINθ) + Fmuscle;
    }

    public void CalculateShearForce()
    {
        // Calculate the shear force using the formula
        Fshear = (Mbody * g * COSθ) + (Mload * g * COSθ);

        float FshearOptionSalah1 = (Mbody * g * SINθ) + (Mload * g * SINθ);
        float FshearOptionSalah2 = (Mbody * g * COSθ) - (Mload * g * COSθ);
        float FshearOptionSalah3 = (Mbody * g * 0.86602f) - (Mload * g * 0.86602f);
    }

    void SetupQuestion1Options()
    {
        if (ListSoal.Length > 0)
        {
            // Setup the options for Question 1
            SoalPanel question1 = ListSoal[0]; // The first question is at index 0

            // Option A:
            float RWL_AwalOptionSalah1 = Lc * (25 / Hawal) * (1 - (0.003f * Mathf.Abs(Vawal - 75))) * (0.82f + (4.5f / Mathf.Abs(Vawal - Vakhir))) * (1 - (0.0032f * Aawal)) * 0.31f * CM;
            float RWL_AkhirOptionSalah1 = Lc * (25 / Hakhir) * (1 - (0.003f * Mathf.Abs(Vakhir - 75))) * (0.82f + (4.5f / Mathf.Abs(Vawal - Vakhir))) * (1 - (0.0032f * Aakhir)) * 0.31f * CM;
            question1.optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = $"A. {RWL_AwalOptionSalah1:F2} dan {RWL_AkhirOptionSalah1:F2}";

            // Option B: 
            float RWL_AwalOptionSalah2 = Lc * (25 / Hawal) * (1 - (0.003f * Mathf.Abs(Vawal - 75))) * (0.82f + (4.5f / Mathf.Abs(Vawal - Vakhir))) * (1 - (0.0032f * Aawal)) * 0.28f * CM;
            float RWL_AkhirOptionSalah2 = Lc * (25 / Hakhir) * (1 - (0.003f * Mathf.Abs(Vakhir - 75))) * (0.82f + (4.5f / Mathf.Abs(Vawal - Vakhir))) * (1 - (0.0032f * Aakhir)) * 0.28f * CM;
            question1.optionButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = $"B. {RWL_AwalOptionSalah2:F2} dan {RWL_AkhirOptionSalah2:F2}";

            // Option C: 
            float rwlAwal = CalculateRWL_Awal(); 
            float rwlAkhir = CalculateRWL_Akhir();
            question1.optionButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = $"C. {rwlAwal:F2} dan {rwlAkhir:F2}";

            // Option D: 
            float RWL_AwalOptionSalah3 = Lc * (25 / Hawal) * (1 - (0.003f * Mathf.Abs(Vawal - 75))) * (0.82f + (4.5f / Mathf.Abs(Vawal - Vakhir))) * (1 - (0.0032f * Aawal)) * 0f * CM;
            float RWL_AkhirOptionSalah3 = Lc * (25 / Hakhir) * (1 - (0.003f * Mathf.Abs(Vakhir - 75))) * (0.82f + (4.5f / Mathf.Abs(Vawal - Vakhir))) * (1 - (0.0032f * Aakhir)) * 0.28f * CM;
            question1.optionButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = $"D. {RWL_AwalOptionSalah3:F2} dan {RWL_AkhirOptionSalah3:F2}";
        }
    }

    void SetupQuestion2Options()
    {
        if (ListSoal.Length > 1)
        {
            // Setup the options for Question 1
            SoalPanel question2 = ListSoal[1]; // The first question is at index 0

            // Option A:
            question2.optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = $"A. {LIAwalOptionSalah1:F2} dan {LIAkhirOptionSalah1:F2}";

            // Option B: 
            question2.optionButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = $"B. {LIAwalOptionSalah2:F2} dan {LIAkhirOptionSalah2:F2}";

            // Option C: 
            question2.optionButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = $"C. {LIAwalOptionSalah3:F2} dan {LIAkhirOptionSalah3:F2}";

            // Option D: 
            question2.optionButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = $"D. {LIawal:F2} dan {LIakhir:F2}";
        }
    }

    void SetupQuestion3Options()
    {
        if (ListSoal.Length > 2)
        {
            // Setup the options for Question 1
            SoalPanel question3 = ListSoal[2]; // The first question is at index 0

            // Option A:
            question3.optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = $"A. LI<size=14>awal</size> {CategorizeRisk(LIAwalOptionSalah1)} and LI<size=14>akhir</size> {CategorizeRisk(LIAkhirOptionSalah1)}";

            // Option B: 
            question3.optionButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = $"B. LI<size=14>awal</size> {CategorizeRisk(LIAwalOptionSalah2)} and LI<size=14>akhir</size> {CategorizeRisk(LIAkhirOptionSalah2)}";

            // Option C: 
            question3.optionButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = $"C. LI<size=14>awal</size> {CategorizeRisk(LIAwalOptionSalah3)} and LI<size=14>akhir</size> {CategorizeRisk(LIAkhirOptionSalah3)}";

            // Option D: 
            question3.optionButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = $"D. LI<size=14>awal</size> {CategorizeRisk(LIawal)} and LI<size=14>akhir</size> {CategorizeRisk(LIakhir)}";
        }
    }

    void SetupQuestion4Options()
    {
        if (ListSoal.Length > 3)
        {
            // Setup the options for Question 1
            SoalPanel question4 = ListSoal[3]; // The first question is at index 0

            // Option A:
            question4.optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = $"A. {FmuscleOptionSalah1} N";

            // Option B: 
            question4.optionButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = $"B. {Fmuscle} N";

            // Option C: 
            question4.optionButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = $"C. {FmuscleOptionSalah2} N";
           
            // Option D: 
            question4.optionButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = $"D. {FmuscleOptionSalah3} N";
        }
    }

    void SetupQuestion5Options()
    {
        if (ListSoal.Length > 4)
        {
            // Setup the options for Question 1
            SoalPanel question5 = ListSoal[4]; // The first question is at index 0
            // Option A:
            float FcompressionOptionSalah1 = (Mbody * g * SINθ) - (Mload * g * SINθ) + (Fmuscle * 0.9f);
            question5.optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = $"A. {FcompressionOptionSalah1} N";

            // Option B: 
            question5.optionButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = $"B. {Fcompression} N";

            // Option C: 
            float FcompressionOptionSalah2 = (Mbody * g * COSθ) + (Mload * g * COSθ) + (Fmuscle * 1.1f);
            question5.optionButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = $"C. {FcompressionOptionSalah2} N";

            // Option D:
            float FcompressionOptionSalah3 = (Mbody * g * 0.86602f) - (Mload * g * 0.86602f) + (Fmuscle * 1.2f);
            question5.optionButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = $"D. {FcompressionOptionSalah3} N";
        }
    }

    void SetupQuestion6Options()
    {
        if (ListSoal.Length > 5)
        {
            // Setup the options for Question 1
            SoalPanel question6 = ListSoal[5]; // The first question is at index 0

            // Option A:
            float FshearOptionSalah1 = (Mbody * g * SINθ) + (Mload * g * SINθ);
            question6.optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = $"A. {FshearOptionSalah1} N";

            // Option B: 
            float FshearOptionSalah2 = (Mbody * g * COSθ) - (Mload * g * COSθ);
            question6.optionButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = $"B. {FshearOptionSalah2} N";

            // Option C: 
            question6.optionButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = $"C. {Fshear} N";

            // Option D:
            float FshearOptionSalah3 = (Mbody * g * 0.86602f) - (Mload * g * 0.86602f);
            question6.optionButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = $"D. {FshearOptionSalah3} N";
        }
    }
}