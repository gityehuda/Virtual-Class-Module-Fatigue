using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public TMP_Text[] questionTexts;
    public Button[] choiceButtons;
    public GameObject[] questionPanels;
    public OpenPanelOnClick[] objectsWithOpenPanelOnClick; // Reference to objects with OpenPanelOnClick script
    public ScorePopUp scorePopup; // Reference to the ScorePopup script

    private int score = 0;
    private Question[] questions;
    private bool[] questionsAnswered;

    // Lists to store the correct answers and selected answers for each question
    private List<int> correctAnswers = new List<int>();
    private List<int> selectedAnswers = new List<int>();

    void Start()
    {
        questions = LoadQuestions(); // Load questions from a data source
        questionsAnswered = new bool[questions.Length]; // Initialize array to track answered questions

        // Initialize correct answers list based on questions
        foreach (var question in questions)
        {
            correctAnswers.Add(question.correctAnswerIndex);
        }

        // Initialize selectedAnswers with default values (e.g., -1) for each question
        for (int i = 0; i < questions.Length; i++)
        {
            selectedAnswers.Add(-1); // Default value indicating no answer selected
        }
    }

    // Show the question with the specified index
    void ShowQuestion(int questionIndex)
    {
        // If the question has already been answered, return without further action
        if (questionsAnswered[questionIndex])
            return;

        // Activate the corresponding question panel
        for (int i = 0; i < questionPanels.Length; i++)
        {
            questionPanels[i].SetActive(i == questionIndex);
        }

        // Set question text
        questionTexts[questionIndex].text = questions[questionIndex].questionText;

        // Enable choice buttons for the current question
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].interactable = !questionsAnswered[questionIndex];
        }
    }

    // Check the answer to the current question
    public void CheckAnswer(int choiceIndex)
    {
        int currentQuestionIndex = GetCurrentQuestionIndex();

        // If the question has already been answered, return without further action
        if (questionsAnswered[currentQuestionIndex])
            return;

        questionsAnswered[currentQuestionIndex] = true; // Mark the question as answered

        // Record the user's selected answer in the correct position
        selectedAnswers[currentQuestionIndex] = choiceIndex;

        // Check if the answer is correct
        if (choiceIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            score++;
        }

        // Update UI to reflect answer state
        ShowQuestion(currentQuestionIndex);

        // Close the current question panel
        CloseQuestionPanel(currentQuestionIndex);

        // Mark the object as answered
        objectsWithOpenPanelOnClick[currentQuestionIndex].MarkAsAnswered();

        // Check if all questions have been answered
        if (AllQuestionsAnswered())
        {
            // Show the final score with detailed answer feedback
            float percentage = ((float)score / questions.Length) * 100;
            scorePopup.ShowScore(percentage, correctAnswers, selectedAnswers);
        }
    }

    // Close the specified question panel
    void CloseQuestionPanel(int panelIndex)
    {
        questionPanels[panelIndex].SetActive(false);
    }

    // Get the index of the currently displayed question
    int GetCurrentQuestionIndex()
    {
        for (int i = 0; i < questionPanels.Length; i++)
        {
            if (questionPanels[i].activeSelf)
            {
                return i;
            }
        }
        return -1;
    }

    // Load questions from a data source
    private Question[] LoadQuestions()
    {
        // Define questions and their correct answers
        return new Question[]
        {
            new Question { questionText = "Body measurement apa yang digunakan untuk menentukan dimensi lebar alas dudukan pada kursi", correctAnswerIndex = 0 },
            new Question { questionText = "Body measurement apa yang digunakan untuk menentukan dimensi Tinggi sandaran tangan pada kursi", correctAnswerIndex = 1 },
            new Question { questionText = "Body measurement apa yang digunakan untuk menentukan dimensi Tinggi alas duduk kursi dari lantai", correctAnswerIndex = 2 },
            new Question { questionText = "Body measurement apa yang digunakan untuk menentukan dimensi Panjang alas duduk pada kursi", correctAnswerIndex = 0 },
            new Question { questionText = "Body measurement apa yang digunakan untuk menentukan dimensi Lebar sisi bahu pada kursi", correctAnswerIndex = 3 },
            new Question { questionText = "Body measurement apa yang digunakan untuk menentukan dimensi Tinggi sandaran badan", correctAnswerIndex = 2 },
            new Question { questionText = "Body measurement apa yang digunakan untuk menentukan dimensi Tinggi sandaran kepala", correctAnswerIndex = 3 },
        };
    }

    // Check if all questions have been answered
    bool AllQuestionsAnswered()
    {
        foreach (bool answered in questionsAnswered)
        {
            if (!answered)
            {
                return false;
            }
        }
        return true;
    }

    // Show the score
    void ShowScore()
    {
        int totalQuestions = questions.Length;
        float percentage = ((float)score / totalQuestions) * 100;
        scorePopup.ShowScore(percentage, correctAnswers, selectedAnswers);

    }

    // Close the current question panel without triggering answer checking
    public void CloseQuestionPanelOnly()
    {
        int currentQuestionIndex = GetCurrentQuestionIndex();

        // Close the current question panel
        CloseQuestionPanel(currentQuestionIndex);
    }
}

[System.Serializable]
public class Question
{
    public string questionText;
    public int correctAnswerIndex;
}