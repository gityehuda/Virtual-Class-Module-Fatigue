using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionPanel : MonoBehaviour
{
    public GameObject panel;
    public Button submitButton;
    public Button closeButton;
    public Button chooseButton;
    public Button buttonToDisable;
    public Dropdown genderDropdown;
    public Dropdown raceDropdown;

    // Prefabs for each combination of gender and race for spawning characters
    public GameObject maleAsianStandingPrefab;
    public GameObject maleAsianSitdownPrefab;
    public GameObject femaleAsianStandingPrefab;
    public GameObject femaleAsianSitdownPrefab;
    public GameObject maleEuropeStandingPrefab;
    public GameObject maleEuropeSitdownPrefab;
    public GameObject femaleEuropeStandingPrefab;
    public GameObject femaleEuropeSitdownPrefab;
    public GameObject maleAmericasStandingPrefab;
    public GameObject maleAmericasSitdownPrefab;
    public GameObject femaleAmericasStandingPrefab;
    public GameObject femaleAmericasSitdownPrefab;

    // Prefabs for previewing the characters
    public GameObject maleAsianPrefab;
    public GameObject femaleAsianPrefab;
    public GameObject maleAmericanPrefab;
    public GameObject femaleAmericanPrefab;
    public GameObject maleEuPrefab;
    public GameObject femaleEuPrefab;

    public GameObject excelGameObject; // Make it public to assign in the Unity Editor
    public RawImage previewImage; // RawImage for the 3D preview
    public Camera previewCamera; // Camera used for the preview

    private bool isPanelOpen = false;
    private string selectedGender = "";
    private string selectedRace = "";

    private GameObject previewObject = null; // Track the preview object

    private Vector3 initialRotation; // Track the initial rotation for resetting
    private float rotationSpeed = 3f; // Set the rotation speed to 3

    void Update()
    {
        if (isPanelOpen)
        {
            // Handle rotation of the preview object using the mouse
            RotatePreviewObjectWithMouse();

            // Handle rotation using J and L keys
            RotatePreviewObjectWithKeys();

            // Reset rotation when R is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetPreviewRotation();
            }
        }
    }

    void RotatePreviewObjectWithMouse()
    {
        if (previewObject != null && Input.GetMouseButton(1)) // Right mouse button held down
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

            // Rotate the preview object around its Y-axis (horizontal rotation)
            previewObject.transform.Rotate(Vector3.up, -mouseX, Space.World);
        }
    }

    void RotatePreviewObjectWithKeys()
    {
        if (previewObject != null)
        {
            // Rotate around the Y-axis (horizontal rotation) using J and L keys
            if (Input.GetKey(KeyCode.J))
            {
                previewObject.transform.Rotate(Vector3.up, -rotationSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.L))
            {
                previewObject.transform.Rotate(Vector3.up, rotationSpeed, Space.World);
            }
        }
    }

    void ResetPreviewRotation()
    {
        if (previewObject != null)
        {
            // Reset to initial rotation
            previewObject.transform.rotation = Quaternion.Euler(initialRotation);
        }
    }

    void Start()
    {
        // Initialize dropdowns
        InitializeDropdowns();

        genderDropdown.onValueChanged.AddListener(delegate { SelectGender(genderDropdown.options[genderDropdown.value].text); });
        raceDropdown.onValueChanged.AddListener(delegate { SelectRace(raceDropdown.options[raceDropdown.value].text); });

        submitButton.onClick.AddListener(SubmitSelections);
        closeButton.onClick.AddListener(ClosePanel);
        chooseButton.onClick.AddListener(OpenPanel);

        // Initially hide the panel
        panel.SetActive(false);

        // Set default dropdown values
        ResetSelections();
    }

    void InitializeDropdowns()
    {
        // Initialize gender dropdown options
        genderDropdown.options.Clear();
        genderDropdown.options.Add(new Dropdown.OptionData("Select Gender"));
        genderDropdown.options.Add(new Dropdown.OptionData("Male"));
        genderDropdown.options.Add(new Dropdown.OptionData("Female"));

        // Initialize race dropdown options
        raceDropdown.options.Clear();
        raceDropdown.options.Add(new Dropdown.OptionData("Select Race"));
        raceDropdown.options.Add(new Dropdown.OptionData("Asia"));
        raceDropdown.options.Add(new Dropdown.OptionData("Europe"));
        raceDropdown.options.Add(new Dropdown.OptionData("Americas"));
    }

    void SelectGender(string gender)
    {
        selectedGender = gender;
        UpdatePreview(); // Update the preview when gender changes
    }

    void SelectRace(string race)
    {
        selectedRace = race;
        UpdatePreview(); // Update the preview when race changes
    }

    void SubmitSelections()
    {
        // Check if the user has not made valid selections for both gender and race
        if (selectedGender == "Select Gender" || selectedRace == "Select Race" || string.IsNullOrEmpty(selectedGender) || string.IsNullOrEmpty(selectedRace))
        {
            Debug.Log("Please select both gender and race.");
            return; // Do nothing and keep the panel open
        }

        // Destroy the object named "Lecturer" if it exists
        DestroyLecturerObject();

        // Spawn character based on selections
        SpawnCharacter(selectedGender, selectedRace);

        // Hide the choose button since a selection has been made
        chooseButton.gameObject.SetActive(false);

        // Close the panel
        ClosePanel();
    }


    void DestroyLecturerObject()
    {
        // Find the object with the name "Lecturer"
        GameObject lecturerObject = GameObject.Find("Lecturer");
        if (lecturerObject != null)
        {
            // Destroy the object
            Destroy(lecturerObject);
        }
    }

    void SpawnCharacter(string gender, string race)
    {
        // Destroy existing character with the "Bot" tag if it exists
        GameObject existingCharacter = GameObject.FindGameObjectWithTag("Bot");
        if (existingCharacter != null)
        {
            Destroy(existingCharacter);
        }

        GameObject existingCharacter2 = GameObject.FindGameObjectWithTag("Bot2");
        if (existingCharacter2 != null)
        {
            Destroy(existingCharacter2); // Or existingCharacter2.SetActive(false);
        }

        // Determine the appropriate prefab based on the selections
        GameObject selectedPrefab = GetSelectedPrefab(gender, race);

        if (selectedPrefab != null)
        {
            // Use the prefab's original position, rotation, and scale
            Vector3 spawnPosition = selectedPrefab.transform.position;
            Quaternion spawnRotation = selectedPrefab.transform.rotation;
            Vector3 spawnScale = selectedPrefab.transform.localScale;

            // Spawn the selected character prefab with the prefab's original settings
            GameObject newCharacter = Instantiate(selectedPrefab, spawnPosition, spawnRotation);
            newCharacter.transform.localScale = spawnScale;
            newCharacter.tag = "Bot";
        }
        else
        {
            Debug.LogError("Prefab for character with gender: " + gender + " and race: " + race + " not found!");
        }
    }

    GameObject GetSelectedPrefab(string gender, string race)
    {
        // Determine the appropriate prefab based on the selections
        switch (gender)
        {
            case "Male":
                return race == "Asia" ? maleAsianStandingPrefab :
                    race == "Europe" ? maleEuropeStandingPrefab : maleAmericasStandingPrefab;
            case "Female":
                return race == "Asia" ? femaleAsianStandingPrefab :
                    race == "Europe" ? femaleEuropeStandingPrefab : femaleAmericasStandingPrefab;
            default:
                return null;
        }
    }

    void UpdatePreview()
    {
        // Ensure both gender and race are selected
        if (selectedGender == "Select Gender" || selectedRace == "Select Race" || string.IsNullOrEmpty(selectedGender) || string.IsNullOrEmpty(selectedRace))
        {
            if (previewObject != null)
            {
                Destroy(previewObject); // Remove the preview if selections are incomplete
            }
            return;
        }

        // Destroy any existing preview object
        if (previewObject != null)
        {
            Destroy(previewObject);
        }

        // Get the preview prefab based on the selected gender and race
        GameObject previewPrefab = GetPreviewPrefab(selectedGender, selectedRace);

        if (previewPrefab != null)
        {
            // Instantiate the preview object in front of the preview camera
            previewObject = Instantiate(previewPrefab);

            // Set the position with the Y value fixed at 1.789686
            Vector3 spawnPosition = previewCamera.transform.position + previewCamera.transform.forward * 2.0f;
            spawnPosition.y = 1.789686f; // Set the Y position to 1.789686

            previewObject.transform.position = spawnPosition;

            // Make the preview object face Y: 215 degrees
            previewObject.transform.rotation = Quaternion.Euler(0, 215f, 0);

            previewObject.transform.localScale = Vector3.one; // Adjust scale for preview if needed

            // Save the initial rotation for resetting
            initialRotation = previewObject.transform.rotation.eulerAngles;

            // Assign the preview object to a specific layer so the preview camera can render it
            previewObject.layer = LayerMask.NameToLayer("Preview");

            // Ensure the preview camera is set to render only the "Preview" layer
            previewCamera.cullingMask = LayerMask.GetMask("Preview");
        }
    }

    GameObject GetPreviewPrefab(string gender, string race)
    {
        // Determine the appropriate prefab for the preview based on the selections
        switch (gender)
        {
            case "Male":
                return race == "Asia" ? maleAsianPrefab :
                       race == "Europe" ? maleEuPrefab :
                       race == "Americas" ? maleAmericanPrefab : null;
            case "Female":
                return race == "Asia" ? femaleAsianPrefab :
                       race == "Europe" ? femaleEuPrefab :
                       race == "Americas" ? femaleAmericanPrefab : null;
            default:
                return null;
        }
    }

    void OpenPanel()
    {
        // Check if the Excel GameObject is active
        if (excelGameObject != null && excelGameObject.activeSelf)
        {
            Debug.Log("Cannot open panel while Excel is active.");
            return;
        }

        // If the panel is already open, return without doing anything
        if (isPanelOpen)
        {
            return;
        }

        // Ensure the choose button is visible when the panel opens
        chooseButton.gameObject.SetActive(true);

        // Disable interaction with the specific button
        if (buttonToDisable != null)
        {
            Debug.Log("Disabling button: " + buttonToDisable.name);
            buttonToDisable.interactable = false;
        }
        else
        {
            Debug.LogWarning("buttonToDisable is not assigned!");
        }

        // Disable interaction with all other UI elements
        DisableAllOtherUIElements();

        // Show the panel
        panel.SetActive(true);
        isPanelOpen = true;

        // Reset selections
        ResetSelections();
    }

    void DisableAllOtherUIElements()
    {
        // Get all UI elements in the scene
        var uiElements = FindObjectsOfType<Selectable>();

        // Iterate through each UI element
        foreach (var uiElement in uiElements)
        {
            // Check if the UI element is not the character selection panel
            if (uiElement.gameObject != panel)
            {
                // Disable interaction with the UI element
                uiElement.interactable = false;
            }
        }
    }

    void ResetSelections()
    {
        // Reset dropdowns to their default state
        genderDropdown.value = 0; // Reset to "Select Gender"
        raceDropdown.value = 0;   // Reset to "Select Race"

        // Reset selection variables
        selectedGender = "";
        selectedRace = "";

        // Destroy any existing preview object
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
    }

    void ClosePanel()
    {
        // Enable interaction with the specific button
        if (buttonToDisable != null)
        {
            Debug.Log("Enabling button: " + buttonToDisable.name);
            buttonToDisable.interactable = true;
        }
        else
        {
            Debug.LogWarning("buttonToDisable is not assigned!");
        }

        // Enable interaction with all other UI elements
        EnableAllOtherUIElements();

        // Disable rotation
        var objectControl = FindObjectOfType<ObjectControl>();
        if (objectControl != null)
        {
            objectControl.EnableMovementAndRotation(); // Enable movement and rotation when the panel is closed
        }

        panel.SetActive(false);
        isPanelOpen = false;

        // Destroy any existing preview object
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
    }

    void EnableAllOtherUIElements()
    {
        // Get all UI elements in the scene
        var uiElements = FindObjectsOfType<Selectable>();

        // Iterate through each UI element
        foreach (var uiElement in uiElements)
        {
            // Enable interaction with the UI element
            uiElement.interactable = true;
        }
    }
}
