using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScorePopUp : MonoBehaviour
{
    public TMP_Text scoreText; // Reference to the TMP text element to display the score
    public string nextSceneName; // The name of the scene to load after showing the score popup
    public Button openPanelsButton; // Reference to the button that opens the panels
    public Button nextButton; // Reference to the next button
    public Button closeButton; // Reference to the close button
    public List<GameObject> Evaluasipanels; // List of panels to display
    private int currentPanelIndex = 0; // Index of the currently displayed panel

    // Arrays to hold TMP_Text fields for each question evaluation panel
    public TMP_Text[] Question1Evaluasi;
    public TMP_Text[] Question2Evaluasi;
    public TMP_Text[] Question3Evaluasi;
    public TMP_Text[] Question4Evaluasi;
    public TMP_Text[] Question5Evaluasi;
    public TMP_Text[] Question6Evaluasi;
    public TMP_Text[] Question7Evaluasi;

    // Lists to store correct answers and selected answers
    private List<int> correctAnswers = new List<int>();
    private List<int> selectedAnswers = new List<int>();

    public void ShowScore(float percentage, List<int> correctAnswers, List<int> selectedAnswers)
    {
        // Set the score text
        scoreText.text = percentage.ToString("0.00") + "%";
        gameObject.SetActive(true); // Make the score popup visible

        // Store the correct and selected answers
        this.correctAnswers = correctAnswers;
        this.selectedAnswers = selectedAnswers;

        // Configure and show the button to open the panels
        ConfigureOpenPanelsButton();

        // Optional: Display detailed feedback using the correct and selected answers
        DisplayDetailedFeedback();
    }

    private void DisplayDetailedFeedback()
    {
        // Arrays to hold all evaluation panels
        TMP_Text[][] evaluasiTexts = {
            Question1Evaluasi, Question2Evaluasi, Question3Evaluasi,
            Question4Evaluasi, Question5Evaluasi, Question6Evaluasi, Question7Evaluasi
        };

        // Loop through each question and set feedback text colors
        for (int i = 0; i < correctAnswers.Count; i++)
        {
            int correctIndex = correctAnswers[i];
            int selectedIndex = selectedAnswers[i];

            // Check if the panel and text array for this question are set up
            if (i < evaluasiTexts.Length && evaluasiTexts[i] != null)
            {
                TMP_Text[] questionEvaluasi = evaluasiTexts[i];

                // Set all options to default color first
                foreach (TMP_Text text in questionEvaluasi)
                {
                    text.color = Color.white;
                }

                // Highlight the correct answer in green
                questionEvaluasi[correctIndex].color = Color.green;

                // Highlight the selected answer in green if correct, or red if incorrect
                if (selectedIndex == correctIndex)
                {
                    questionEvaluasi[selectedIndex].color = Color.green; // Correct selection
                }
                else
                {
                    questionEvaluasi[selectedIndex].color = Color.red; // Incorrect selection
                }
            }
        }
    }

    // Method to configure the button that opens the panels
    private void ConfigureOpenPanelsButton()
    {
        openPanelsButton.gameObject.SetActive(true);
        openPanelsButton.onClick.RemoveAllListeners();
        openPanelsButton.onClick.AddListener(OpenPanels);
        nextButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }

    // Method to open the panels
    private void OpenPanels()
    {
        gameObject.SetActive(false); // Close the score panel

        // Show the first panel
        if (Evaluasipanels.Count > 0)
        {
            Evaluasipanels[0].SetActive(true);
        }

        // Configure the next button for the first panel
        ConfigureNextButton();
    }

    // Method to configure the next button
    private void ConfigureNextButton()
    {
        nextButton.gameObject.SetActive(true);
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(ShowNextPanel);
        closeButton.gameObject.SetActive(false);

        // Set the button position for the first panel
        RectTransform nextButtonRect = nextButton.GetComponent<RectTransform>();
        //if (currentPanelIndex == 0)
        //{
        //    nextButtonRect.anchoredPosition = new Vector2(0.3f, -47);
        //}
    }

    // Method to show the next panel
    private void ShowNextPanel()
    {
        // Hide the current panel
        if (currentPanelIndex < Evaluasipanels.Count)
        {
            Evaluasipanels[currentPanelIndex].SetActive(false);
        }

        // Move to the next panel
        currentPanelIndex++;

        // Show the next panel or configure the close button if it is the last panel
        if (currentPanelIndex < Evaluasipanels.Count)
        {
            Evaluasipanels[currentPanelIndex].SetActive(true);

            // Move the next button to the new position for the second panel
            if (currentPanelIndex == 1)
            {
                RectTransform nextButtonRect = nextButton.GetComponent<RectTransform>();
                //nextButtonRect.anchoredPosition = new Vector2(343, -196);
            }

            // If it is the last panel, show the close button instead of the next button
            if (currentPanelIndex == Evaluasipanels.Count - 1)
            {
                ConfigureCloseButton();
            }
            else
            {
                ConfigureNextButton();
            }
        }
    }

    // Method to configure the close button
    private void ConfigureCloseButton()
    {
        closeButton.gameObject.SetActive(true);
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(CloseAndLoadNextScene);
        nextButton.gameObject.SetActive(false);
    }

    // Method to close the panel and load the next scene
    private void CloseAndLoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
