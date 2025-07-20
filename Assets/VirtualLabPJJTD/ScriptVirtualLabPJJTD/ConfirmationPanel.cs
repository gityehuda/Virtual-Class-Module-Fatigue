using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class ConfirmationPanel : MonoBehaviour
{
    public GameObject panel;
    public Button yesButton;
    public Button noButton;
    public CinemachineFreeLook cinemachineFreeLookCamera; 
    public string nextSceneName; 

    // Add references to your item colliders
    public Collider item1Collider;
    public Collider item2Collider;
    public Collider item3Collider;
    public Collider item4Collider;
    public Collider item5Collider;

    private bool clickEnabled = false;
    private bool isPanelOpen = false;

    private void OnEnable()
    {
        // Initially hide the panel
        panel.SetActive(false);

        // Add listeners to the buttons
        yesButton.onClick.AddListener(Confirm);
        noButton.onClick.AddListener(Cancel);

        // Start the coroutine to enable click after a delay
        StartCoroutine(EnableClickAfterDelay());
    }

    IEnumerator EnableClickAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        clickEnabled = true;

        // Add click listeners to the item colliders
        item1Collider.gameObject.AddComponent<ClickHandler>().OnClick += SelectItem1;
        item2Collider.gameObject.AddComponent<ClickHandler>().OnClick += SelectItem2;
        item3Collider.gameObject.AddComponent<ClickHandler>().OnClick += SelectItem3;
        item4Collider.gameObject.AddComponent<ClickHandler>().OnClick += SelectItem4;
        //item5Collider.gameObject.AddComponent<ClickHandler>().OnClick += SelectItem5;
    }

    public void ShowPanel()
    {
        // Check if the panel is already open
        if (isPanelOpen) return;

        // Show the panel and disable character movement and rotation
        panel.SetActive(true);
        isPanelOpen = true;  // Set the flag to true
        StartCoroutine(DisableButtonsTemporarily());

        if (cinemachineFreeLookCamera != null)
        {
            cinemachineFreeLookCamera.enabled = false;
        }
    }

    IEnumerator DisableButtonsTemporarily()
    {
        // Disable button click listeners temporarily
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Re-enable button click listeners
        yesButton.onClick.AddListener(Confirm);
        noButton.onClick.AddListener(Cancel);
    }

    private void Confirm()
    {
        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }

    private void Cancel()
    {
        // Hide the panel and enable character movement and rotation
        panel.SetActive(false);
        isPanelOpen = false;  // Reset the flag when panel is closed
        if (cinemachineFreeLookCamera != null)
        {
            cinemachineFreeLookCamera.enabled = true;
        }
    }

    // Methods to handle item selection
    private void SelectItem1()
    {
        if (clickEnabled)
        {
            nextSceneName = "Antropometri"; 
            ShowPanel();
        }
    }

    private void SelectItem2()
    {
        if (clickEnabled)
        {
            nextSceneName = "TimeStudy"; 
            ShowPanel(); 
        }
    }

    private void SelectItem3()
    {
        if (clickEnabled)
        {
            nextSceneName = "Biomechanic"; 
            ShowPanel(); 
        }
    }

    private void SelectItem4()
    {
        if (clickEnabled)
        {
            nextSceneName = "Fatigue";
            ShowPanel();
        }
    }

    //private void SelectItem5()
    //{
    //    if (clickEnabled)
    //    {
    //        nextSceneName = "Modul5"; 
    //        ShowPanel(); 
    //    }
    //}
}

// ClickHandler class to handle clicks on colliders
public class ClickHandler : MonoBehaviour
{
    public delegate void ClickAction();
    public event ClickAction OnClick;

    private void OnMouseDown()
    {
        OnClick?.Invoke();
    }
}
