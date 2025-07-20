using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Testing : MonoBehaviour
{
    public GameObject Module1; // Reference to the object with the collider for Module 1
    public string Module1SceneToLoad; // The scene to load when the player enters Module 1
    public GameObject confirmationPanel; // Reference to the confirmation panel UI
    public Button yesButton; // Reference to the Yes button
    public Button noButton; // Reference to the No button
    public Animator animator; // Reference to the Animator component

    private string sceneToLoad; // Store the scene to load after confirmation

    void Start()
    {
        // Ensure the modules are set
        if (Module1 == null || confirmationPanel == null || yesButton == null || noButton == null)
        {
            Debug.LogError("Required objects not assigned!");
            return;
        }

        // Find the object with the name "mahasiswa_female(Clone)" and get its Animator component
        GameObject mahasiswaFemale = GameObject.Find("mahasiswa_female(Clone)");
        if (mahasiswaFemale != null)
        {
            animator = mahasiswaFemale.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on mahasiswa_female(Clone)!");
            }
        }
        else
        {
            Debug.LogError("mahasiswa_female(Clone) object not found in the scene!");
        }

        // Initially hide the confirmation panel
        confirmationPanel.SetActive(false);

        // Attach the trigger detection method to the module's collider
        AttachTriggerHandler(Module1, Module1SceneToLoad);

        // Add listeners to the buttons
        yesButton.onClick.AddListener(OnConfirmYes);
        noButton.onClick.AddListener(OnConfirmNo);
    }

    void AttachTriggerHandler(GameObject module, string sceneToLoad)
    {
        Collider collider = module.GetComponent<Collider>();
        if (collider != null && collider.isTrigger)
        {
            var triggerHandler = module.AddComponent<TriggerHandler>();
            triggerHandler.Initialize(this, sceneToLoad);
        }
        else
        {
            Debug.LogError($"{module.name} does not have a trigger collider!");
        }
    }

    public void OnPlayerTriggerEnter(string sceneToLoad)
    {
        // Store the scene to load after confirmation
        this.sceneToLoad = sceneToLoad;

        // Stop the animation by disabling the Animator component
        if (animator != null)
        {
            animator.enabled = false;
        }

        // Show the confirmation panel
        confirmationPanel.SetActive(true);
    }

    private void OnConfirmYes()
    {
        // Hide the confirmation panel and load the specified scene
        confirmationPanel.SetActive(false);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnConfirmNo()
    {
        // Hide the confirmation panel
        confirmationPanel.SetActive(false);

        // Re-enable the animation by enabling the Animator component
        if (animator != null)
        {
            animator.enabled = true;
        }
    }
}

// Helper class to handle the trigger event
public class TriggerHandler : MonoBehaviour
{
    private Testing panelOpener;
    private string sceneToLoad;

    public void Initialize(Testing opener, string sceneName)
    {
        panelOpener = opener;
        sceneToLoad = sceneName;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has the "Player" tag
        if (other.gameObject.CompareTag("Player"))
        {
            panelOpener.OnPlayerTriggerEnter(sceneToLoad);
        }
    }
}
