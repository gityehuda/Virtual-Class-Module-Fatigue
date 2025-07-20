using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    public NPCConversation myConversation;
    public GameObject objectToToggle; // Reference to the GameObject you want to toggle
    private bool hasStartedConversation = false;

    private void OnTriggerStay(Collider other)
    {
        if (other != null && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bot2")) && !hasStartedConversation)
        {
            if (ConversationManager.Instance != null && myConversation != null)
            {
                ConversationManager.Instance.StartConversation(myConversation);
                hasStartedConversation = true;
            }
            else
            {
                Debug.LogWarning("ConversationManager.Instance or myConversation is null.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bot2"))
        {
            hasStartedConversation = false;
        }
    }

    private void Update()
    {
        // Check if the conversation has started and if the objectToToggle is not null
        if (hasStartedConversation && objectToToggle != null)
        {
            // Check if the GameObject is inactive before attempting to activate it
            if (!objectToToggle.activeSelf)
            {
                objectToToggle.SetActive(true);
            }
        }
    }
}