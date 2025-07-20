using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DialogueEditor;

public class QuizManagerModul2 : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public GameObject panel;
        public Text questionText;
        public Button[] answerButtons;
        public Button closePanelButton;
        public string question;
        public int correctButtonIndex;
    }

    public Button openQuestionButton;
    public GameObject questionSelectionPanel;
    public Button closeSelectionPanelButton;
    public Button[] questionButtons;
    public Question[] questions;
    public GameObject scorePanel; // Panel to display the score
    public Text scoreText; // UI Text to display the score
    public Button exitButton; // Reference to the exit button

    private int currentQuestionIndex = 0;
    private int score = 0;

    private ConversationManager conversationManager;

    void Start()
    {
        openQuestionButton.onClick.AddListener(OpenQuestionSelectionPanel);
        closeSelectionPanelButton.onClick.AddListener(CloseQuestionSelectionPanel);
        questionSelectionPanel.SetActive(false);
        scorePanel.SetActive(false); // Hide the score panel initially
        exitButton.interactable = true; // Make sure the exit button is interactable initially

        for (int i = 0; i < questionButtons.Length; i++)
        {
            int index = i;
            questionButtons[i].onClick.AddListener(() => OpenQuestionPanel(index));
        }

        foreach (var question in questions)
        {
            question.panel.SetActive(false);
            question.closePanelButton.onClick.AddListener(CloseCurrentQuestionPanel);

            for (int i = 0; i < question.answerButtons.Length; i++)
            {
                int answerIndex = i;
                question.answerButtons[i].onClick.AddListener(() => AnswerQuestion(answerIndex, question.correctButtonIndex));
            }
        }

        // Find and store the ConversationManager component
        conversationManager = FindObjectOfType<ConversationManager>();
    }

    void OpenQuestionSelectionPanel()
    {
        if (IsConversationPanelActive())
        {
            Debug.Log("Cannot open question selection panel while Conversation Manager panel is active.");
            return;
        }

        questionSelectionPanel.SetActive(true);
        openQuestionButton.gameObject.SetActive(false);
        UpdateExitButtonInteractable();
    }

    void CloseQuestionSelectionPanel()
    {
        questionSelectionPanel.SetActive(false);
        openQuestionButton.gameObject.SetActive(true);
        UpdateExitButtonInteractable();
    }

    void OpenQuestionPanel(int index)
    {
        if (IsConversationPanelActive())
        {
            Debug.Log("Cannot open question panel while Conversation Manager panel is active.");
            return;
        }

        currentQuestionIndex = index;
        questionSelectionPanel.SetActive(false);
        DisplayCurrentQuestion();
        UpdateExitButtonInteractable();
    }

    void CloseCurrentQuestionPanel()
    {
        questions[currentQuestionIndex].panel.SetActive(false);
        questionSelectionPanel.SetActive(true);
        UpdateExitButtonInteractable();
    }

    void DisplayCurrentQuestion()
    {
        questions[currentQuestionIndex].panel.SetActive(true);
        questions[currentQuestionIndex].questionText.text = questions[currentQuestionIndex].question;
        UpdateExitButtonInteractable();
    }

    void AnswerQuestion(int answerIndex, int correctIndex)
    {
        if (answerIndex == correctIndex)
        {
            score++;
        }
        questions[currentQuestionIndex].panel.SetActive(false);
        questionButtons[currentQuestionIndex].interactable = false;
        UpdateExitButtonInteractable();

        if (AllQuestionsAnswered())
        {
            ShowScore();
        }
        else
        {
            questionSelectionPanel.SetActive(true);
        }
    }

    bool AllQuestionsAnswered()
    {
        foreach (var button in questionButtons)
        {
            if (button.interactable)
            {
                return false;
            }
        }
        return true;
    }

    void ShowScore()
    {
        scorePanel.SetActive(true);
        float percentage = ((float)score / questions.Length) * 100;
        scoreText.text = percentage.ToString("F2") + "%";
        StartCoroutine(WaitAndLoadScene(5)); // Wait for 5 seconds before loading the next scene
        UpdateExitButtonInteractable();
    }

    IEnumerator WaitAndLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("LabAwal");
    }

    void UpdateExitButtonInteractable()
    {
        // Disable the exit button if any panel is active
        bool anyPanelActive = questionSelectionPanel.activeSelf || scorePanel.activeSelf;
        foreach (var question in questions)
        {
            if (question.panel.activeSelf)
            {
                anyPanelActive = true;
                break;
            }
        }
        exitButton.interactable = !anyPanelActive;
    }

    private bool IsConversationPanelActive()
    {
        // Check if the Conversation Manager panel is active
        return conversationManager != null && conversationManager.isActiveAndEnabled;
    }
}
