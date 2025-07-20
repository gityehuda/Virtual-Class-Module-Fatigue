using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DialogueEditor;


public class Exit : MonoBehaviour
{
    public GameObject exitPanel;
    public Button yesButton;
    public Button noButton;
    public Button exitButton;
    public GameObject excelGameObject; // Make it public to assign in the Unity Editor

    private bool isPanelOpen = false;
    private ConversationManager conversationManager;

    private void Start()
    {
        exitPanel.SetActive(false); // Make sure the exit panel is initially deactivated
        yesButton.onClick.AddListener(OnYesButtonClick);
        noButton.onClick.AddListener(OnNoButtonClick);
        exitButton.onClick.AddListener(OpenExitPanel);

        // Find and store the ConversationManager component
        conversationManager = FindObjectOfType<ConversationManager>();
    }

    private void OpenExitPanel()
    {
        // Check if the Conversation Manager panel is active
        if (IsConversationPanelActive())
        {
            Debug.Log("Cannot open exit panel while Conversation Manager panel is active.");
            return;
        }

        // Check if the Excel GameObject is active
        if (excelGameObject != null && excelGameObject.activeSelf)
        {
            Debug.Log("Cannot open exit panel while Excel is active.");
            return;
        }

        exitPanel.SetActive(true);
        isPanelOpen = true;
        Time.timeScale = 0; // Pause the game

        // Disable interaction with all other UI elements
        DisableAllOtherUIElements();

        // Disable rotation
        var objectControl = FindObjectOfType<ObjectControl>();
        if (objectControl != null)
        {
            objectControl.DisableMovementAndRotation();
        }
    }

    private bool IsConversationPanelActive()
    {
        // Check if the Conversation Manager panel is active
        return conversationManager != null && conversationManager.isActiveAndEnabled;
    }

    private void OnYesButtonClick()
    {
        Time.timeScale = 1; // Resume the game before loading a new scene
        SceneManager.LoadScene("LabAwal"); // Load your next scene here
    }

    private void OnNoButtonClick()
    {
        exitPanel.SetActive(false); // Close the exit panel
        isPanelOpen = false;
        Time.timeScale = 1; // Resume the game

        // Enable interaction with all other UI elements
        EnableAllOtherUIElements();

        // Enable rotation
        var objectControl = FindObjectOfType<ObjectControl>();
        if (objectControl != null)
        {
            objectControl.EnableMovementAndRotation();
        }
    }

    private bool IsOtherPanelOpen()
    {
        // Check if any other panel is open
        return isPanelOpen; // Adjust this based on your actual UI structure
    }

    private void DisableAllOtherUIElements()
    {
        // Get all UI elements in the scene
        var uiElements = FindObjectsOfType<Button>();

        // Iterate through each UI element
        foreach (var uiElement in uiElements)
        {
            // Check if the UI element is not the exit panel or its buttons
            if (uiElement.gameObject != exitPanel && uiElement.gameObject != yesButton.gameObject && uiElement.gameObject != noButton.gameObject && uiElement.gameObject != exitButton.gameObject)
            {
                // Disable interaction with the UI element
                uiElement.interactable = false;
            }
        }
    }

    private void EnableAllOtherUIElements()
    {
        // Get all UI elements in the scene
        var uiElements = FindObjectsOfType<Button>();

        // Iterate through each UI element
        foreach (var uiElement in uiElements)
        {
            // Enable interaction with the UI element
            uiElement.interactable = true;
        }
    }
}