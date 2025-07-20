using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPanelOnClick : MonoBehaviour
{
    public GameObject panel; // Assign your panel in the inspector
    public Color answeredColor = Color.green; // Color for answered objects
    private bool isAnswered = false; // Flag to track if the question is answered

    public delegate void PanelStateChanged(bool isOpen);
    public static event PanelStateChanged OnPanelStateChanged;

    private GameObject teksLanjutan6;
    private GameObject teksLanjutan7;
    private GameObject teksLanjutan8;

    void Start()
    {
        // Ensure the panel is initially closed
        panel.SetActive(false);

        GameObject conversationCanvas = GameObject.Find("ConversationCanvas");
        if (conversationCanvas != null)
        {
            teksLanjutan6 = conversationCanvas.transform.Find("Teks Lanjutan 6")?.gameObject;
            teksLanjutan7 = conversationCanvas.transform.Find("Teks Lanjutan 7")?.gameObject;
            teksLanjutan8 = conversationCanvas.transform.Find("Teks Lanjutan 8")?.gameObject;
        }
    }

    void OnMouseDown()
    {
        // If the question is already answered or another panel is open, return without further action
        if (isAnswered || IsAnyPanelOpen())
            return;

        // Toggle the panel's active state
        bool isActive = !panel.activeSelf;
        panel.SetActive(isActive);

        // Notify about the panel state change
        OnPanelStateChanged?.Invoke(isActive);
    }

    // Method to mark the object as answered and change its color
    public void MarkAsAnswered()
    {
        isAnswered = true;
        GetComponent<Renderer>().material.color = answeredColor;
        // Close the panel when marked as answered
        panel.SetActive(false);
        OnPanelStateChanged?.Invoke(false);
    }

    private bool IsAnyPanelOpen()
    {
        // Check if specific panels within ConversationCanvas are open
        if ((teksLanjutan6 != null && teksLanjutan6.activeSelf) ||
            (teksLanjutan7 != null && teksLanjutan7.activeSelf) ||
            (teksLanjutan8 != null && teksLanjutan8.activeSelf))
        {
            return true;
        }

        OpenPanelOnClick[] allPanels = FindObjectsOfType<OpenPanelOnClick>();
        foreach (OpenPanelOnClick panelScript in allPanels)
        {
            if (panelScript.panel.activeSelf)
                return true;
        }
        return false;
    }
}