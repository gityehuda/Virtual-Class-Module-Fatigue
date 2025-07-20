using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject settingsPanel; 
    public Button openSettingsButton;
    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the panel is hidden initially
        settingsPanel.SetActive(false);

        // Add listener to the settings button to toggle the panel visibility
        openSettingsButton.onClick.AddListener(ToggleSettingsPanel);
        exitButton.onClick.AddListener(CloseSettingsPanel);
    }

    // Method to toggle the visibility of the settings panel
    void ToggleSettingsPanel()
    {
        // Toggle the panel's active state (show/hide)
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
}
