using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class AntroControl : MonoBehaviour
{
    public Material lineMaterial;
    public GameObject[] startPoints;
    public GameObject[] endPoints;
    public TextMeshProUGUI distanceText;
    public GameObject panel;
    public Button openExcelButton;
    public Button nextPointButton;
    public GameObject distancePanel;
    public Button descriptionButton;
    public Button closeDescriptionButton;
    public Button[] buttonsToDisable;
    public float snapDistanceThreshold = 0.5f;
    public PredefinedDistance[] predefinedDistances;
    public string excelLink = "https://docs.google.com/spreadsheets/d/1zIBtANrtdH-_oZEWLCxD0gV5g51rDSLl/edit?usp=sharing&ouid=103801528146517004785&rtpof=true&sd=true"; // Default Excel link
    public float[] rotationAngles;
    public AudioClip snapSound;

    private LineRenderer lineRenderer;
    private int currentIndex = 0;
    private GameObject currentStartPoint;
    private GameObject currentEndPoint;
    private bool drawingLine = false;
    private bool lineAtEndPoint = false;
    private GameObject activePanel;
    private AudioSource audioSource;
    private bool canMove = true; // Movement control flag

    public ObjectControl objectControl;

    private Vector3 originalPosition; // Store the original position
    private Vector3 startPointPosition;
    private Vector3 endPointPosition;
    private string storedDistanceText;

    void Start()
    {
        if (startPoints.Length == 0 || endPoints.Length == 0 || distanceText == null || panel == null || openExcelButton == null || objectControl == null || nextPointButton == null || distancePanel == null || descriptionButton == null || closeDescriptionButton == null)
        {
            Debug.LogError("Start Points, End Points, Distance Text, Panel, Open Excel Button, Next Point Button, Distance Panel, Description Button, Close Button, or Object Control not assigned!");
            return;
        }

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;

        // Add an AudioSource component if it's not already attached
        audioSource = gameObject.AddComponent<AudioSource>();

        // Add listener for the buttons
        openExcelButton.onClick.AddListener(OnOpenExcelClicked);
        nextPointButton.onClick.AddListener(OnNextPointButtonClicked);
        descriptionButton.onClick.AddListener(OnDescriptionButtonClicked);
        closeDescriptionButton.onClick.AddListener(OnCloseDescriptionButtonClicked);

        // Set initial line position to match start point's position
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        // Hide the line initially
        lineRenderer.enabled = false;

        SetPointsActive(false);
        ShowNextPoints();

        // Hide the panel, Next button, and Description button initially
        panel.SetActive(false);
        nextPointButton.gameObject.SetActive(false);
        descriptionButton.gameObject.SetActive(false);
        distancePanel.SetActive(false);
        closeDescriptionButton.gameObject.SetActive(false);

        // Store the original position of the object at the start
        originalPosition = transform.position;
    }

    void Update()
    {
        // Disable other interactions when the panel is active or all points are processed
        if (panel.activeSelf || currentIndex >= startPoints.Length || IsPointerOverUIObject())
        {
            return;
        }

        // Allow object movement if the movement is enabled
        if (canMove)
        {
            HandleObjectMovement();
        }

        // Handle line drawing separately
        if (Input.GetMouseButton(0) && drawingLine)
        {
            // Ensure the line renderer is enabled when the user starts drawing
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }
            UpdateLine();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!IsLineAtEndPoint())
            {
                // If the line is not at the end point when the mouse button is released, hide the line
                lineRenderer.enabled = false;
                distanceText.gameObject.SetActive(false);
                distancePanel.SetActive(false);
                if (activePanel != null)
                {
                    activePanel.SetActive(false);
                    closeDescriptionButton.gameObject.SetActive(false);
                    SetButtonsInteractable(true);
                }
            }
        }

        if (lineAtEndPoint)
        {
            // Update line positions to match the start and end points if the line reached the end point
            lineRenderer.SetPosition(0, currentStartPoint.transform.position);
            lineRenderer.SetPosition(1, currentEndPoint.transform.position);
            DisplayStoredDistance();  // Ensure distance is always displayed
        }
    }

    void HandleObjectMovement()
    {
        // Example: Moving object with arrow keys
        float moveSpeed = 5f;
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection += Vector3.back;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += Vector3.right;
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void UpdateLine()
    {
        lineRenderer.SetPosition(0, currentStartPoint.transform.position);

        Vector3 mousePosition = Input.mousePosition;
        Vector3 endPointScreenPos = Camera.main.WorldToScreenPoint(currentEndPoint.transform.position);

        if (Vector2.Distance(mousePosition, endPointScreenPos) < snapDistanceThreshold * Screen.dpi)
        {
            SnapToEndPoint();
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 newPosition = hit.point;
                lineRenderer.SetPosition(1, newPosition);
                DisplayDistance(Vector3.Distance(currentStartPoint.transform.position, newPosition));

                lineRenderer.material.color = lineMaterial.color;
            }
            else
            {
                lineRenderer.SetPosition(1, currentStartPoint.transform.position);
            }
        }
    }

    void SnapToEndPoint()
    {
        lineRenderer.SetPosition(1, currentEndPoint.transform.position);
        DisplayPredefinedDistance(currentIndex);

        lineRenderer.material.color = Color.green;

        // Play the snap sound
        if (snapSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(snapSound);
        }

        if (drawingLine)
        {
            drawingLine = false;
            lineAtEndPoint = true;
            nextPointButton.gameObject.SetActive(true);
            descriptionButton.gameObject.SetActive(true);
        }
    }

    void ShowNextPoints()
    {
        if (currentIndex < startPoints.Length)
        {
            currentStartPoint = startPoints[currentIndex];
            currentEndPoint = endPoints[currentIndex];

            SetPointsActive(false);

            currentStartPoint.SetActive(true);
            currentEndPoint.SetActive(true);

            Renderer startRenderer = currentStartPoint.GetComponent<Renderer>();
            if (startRenderer != null)
            {
                startRenderer.material.color = Color.blue;
            }

            Renderer endRenderer = currentEndPoint.GetComponent<Renderer>();
            if (endRenderer != null)
            {
                endRenderer.material.color = Color.red;
            }

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, currentStartPoint.transform.position);
            lineRenderer.SetPosition(1, currentStartPoint.transform.position);

            drawingLine = true;
            lineAtEndPoint = false;

            if (rotationAngles.Length > currentIndex)
            {
                transform.rotation = Quaternion.Euler(0, rotationAngles[currentIndex], 0);
            }
        }
        else if (currentIndex == startPoints.Length)
        {
            // If all points are finished, show the panel
            panel.SetActive(true);
            openExcelButton.gameObject.SetActive(true);
            SetButtonsInteractable(false);
        }
    }

    void DisplayPredefinedDistance(int index)
    {
        if (index < predefinedDistances.Length)
        {
            string label = predefinedDistances[index].label;
            string distance = predefinedDistances[index].distance;

            storedDistanceText = $"{label}: {distance} cm";
            distanceText.text = storedDistanceText;
            distanceText.gameObject.SetActive(true);
        }
        else
        {
            storedDistanceText = "Distance: N/A";
            distanceText.text = storedDistanceText;
            distanceText.gameObject.SetActive(true);
        }

        if (distancePanel != null)
        {
            distancePanel.SetActive(true);
        }
    }

    void DisplayDistance(float distance)
    {
        distance *= 100f; // Convert to centimeters
        string label = currentIndex < predefinedDistances.Length ? predefinedDistances[currentIndex].label : "N/A";

        storedDistanceText = $"{label}: {distance.ToString("F2")} cm";
        distanceText.text = storedDistanceText;
        distanceText.gameObject.SetActive(true);

        if (distancePanel != null)
        {
            distancePanel.SetActive(true);
        }
    }

    void OnNextPointButtonClicked()
    {
        nextPointButton.gameObject.SetActive(false);
        descriptionButton.gameObject.SetActive(false);
        lineRenderer.enabled = false;

        SetPointsActive(false);
        distanceText.gameObject.SetActive(false);
        distancePanel.SetActive(false);

        currentIndex++;
        if (currentIndex < startPoints.Length)
        {
            ShowNextPoints();
            objectControl.ResetRotationAndZoom();
        }
        else
        {
            // If all points are finished, show the panel
            panel.SetActive(true);
            openExcelButton.gameObject.SetActive(true);
            SetButtonsInteractable(false);
        }
    }

    void OnDescriptionButtonClicked()
    {
        if (currentIndex < predefinedDistances.Length)
        {
            GameObject panel = predefinedDistances[currentIndex].panel;

            if (panel != null)
            {
                if (activePanel != null)
                {
                    activePanel.SetActive(false);
                }

                // Disable movement when the panel is opened
                canMove = false;

                // Store the original position before moving
                originalPosition = transform.position;

                // Store the current positions of the line
                startPointPosition = lineRenderer.GetPosition(0);
                endPointPosition = lineRenderer.GetPosition(1);

                // Move the object to the new X position based on its name
                float newXPosition;
                {
                    newXPosition = 1.2f;
                }

                Vector3 newPosition = transform.position;
                newPosition.x = newXPosition;
                transform.position = newPosition;

                // Disable the reset functionality
                Reset resetScript = gameObject.GetComponent<Reset>();
                if (resetScript != null)
                {
                    resetScript.SetCanReset(false);
                }

                // Enable only rotation in the ObjectControl script
                if (objectControl != null)
                {
                    objectControl.EnableRotationOnly(true);
                }

                panel.SetActive(true);
                closeDescriptionButton.gameObject.SetActive(true);
                SetButtonsInteractable(false);
                activePanel = panel;

                // Ensure the line renderer is enabled and the line is drawn
                RestoreLineRendererState();
            }
        }
    }

    void OnCloseDescriptionButtonClicked()
    {
        if (activePanel != null)
        {
            activePanel.SetActive(false);
            closeDescriptionButton.gameObject.SetActive(false);
            SetButtonsInteractable(true);

            // Re-enable movement when the panel is closed
            canMove = true;

            // Move the object back to its original position
            transform.position = originalPosition;

            // Restore the line positions after the panel is closed
            RestoreLineRendererState();

            // Re-enable full control in the ObjectControl script
            if (objectControl != null)
            {
                objectControl.EnableRotationOnly(false);
            }

            // Re-enable the reset functionality
            Reset resetScript = gameObject.GetComponent<Reset>();
            if (resetScript != null)
            {
                resetScript.SetCanReset(true);
            }

            activePanel = null;
        }
    }

    void RestoreLineRendererState()
    {
        if (currentStartPoint != null && currentEndPoint != null)
        {
            lineRenderer.SetPosition(0, startPointPosition);
            lineRenderer.SetPosition(1, endPointPosition);
            lineRenderer.enabled = true;

            DisplayStoredDistance();
        }
    }

    void DisplayStoredDistance()
    {
        if (!string.IsNullOrEmpty(storedDistanceText))
        {
            distanceText.text = storedDistanceText;
            distanceText.gameObject.SetActive(true);

            if (distancePanel != null)
            {
                distancePanel.SetActive(true);
            }
        }
    }

    void SetButtonsInteractable(bool interactable)
    {
        if (buttonsToDisable != null)
        {
            foreach (Button button in buttonsToDisable)
            {
                if (button != null)
                {
                    button.interactable = interactable;
                }
            }
        }
    }

    void SetPointsActive(bool active)
    {
        foreach (GameObject point in startPoints)
        {
            point.SetActive(active);
        }
        foreach (GameObject point in endPoints)
        {
            point.SetActive(active);
        }
    }

    public void OpenLink()
    {
        Application.OpenURL(excelLink);

        panel.SetActive(false);

        openExcelButton.gameObject.SetActive(true);

        Destroy(gameObject);
    }

    void OnOpenExcelClicked()
    {
        OpenLink();
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    bool IsLineAtEndPoint()
    {
        if (currentEndPoint == null)
            return false;

        float distanceToEndPoint = Vector3.Distance(lineRenderer.GetPosition(1), currentEndPoint.transform.position);
        return distanceToEndPoint < snapDistanceThreshold;
    }
}
