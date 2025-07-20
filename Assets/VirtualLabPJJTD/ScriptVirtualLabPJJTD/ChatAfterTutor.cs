using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChatAfterTutor : MonoBehaviour
{
    public GameObject[] panels; // Array of panels to navigate through
    public Button nextButton;   // Button to navigate to the next panel

    private int currentPanelIndex = 0; // Index of the current panel
    private GameObject dummyAntro;     // Reference to the DummyAntro object
    private bool dummyAntroWasSpawned = false; // Flag to check if DummyAntro was spawned

    void Start()
    {
        // Make sure all panels are inactive at the start
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        // Add listener to the next button
        nextButton.onClick.AddListener(ShowNextPanel);
    }

    void Update()
    {
        // Check if the object named "DummyAntro" appears in the scene
        dummyAntro = GameObject.Find("DummyAntro");

        if (dummyAntro != null && !dummyAntroWasSpawned)
        {
            dummyAntroWasSpawned = true; // Mark that DummyAntro was spawned
        }

        // Check if the DummyAntro object is destroyed after being spawned
        if (dummyAntro == null && dummyAntroWasSpawned)
        {
            dummyAntroWasSpawned = false; // Reset the flag

            // Open the first panel if it exists
            if (panels.Length > 0)
            {
                panels[0].SetActive(true);
            }
        }
    }

    void ShowNextPanel()
    {
        // Hide the current panel
        if (panels[currentPanelIndex] != null)
        {
            panels[currentPanelIndex].SetActive(false);
        }

        // Move to the next panel
        currentPanelIndex++;

        // If it is the last panel, close all panels
        if (currentPanelIndex >= panels.Length)
        {
            currentPanelIndex = 0; // Reset index (optional)
        }
        else
        {
            // Show the next panel
            if (panels[currentPanelIndex] != null)
            {
                panels[currentPanelIndex].SetActive(true);
            }
        }
    }
}
