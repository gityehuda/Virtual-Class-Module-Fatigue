using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

[System.Serializable]
public struct PredefinedDistance
{
    public string label;
    public string distance;
    public GameObject panel;
}

public class AntroControl2 : MonoBehaviour
{
    public Material lineMaterial;
    public GameObject[] startPoints;
    public GameObject[] endPoints;
    public TextMeshProUGUI distanceText;
    public GameObject nextPrefab;
    public GameObject objectToDestroy;
    public float snapDistanceThreshold = 0.5f;
    public PredefinedDistance[] predefinedDistances;
    public ObjectControl objectController;
    public GameObject teksAntroPanel;
    public Button nextPointButton;
    public Button descriptionButton;
    public Button closeDescriptionButton;
    public Button[] buttonsToDisable;

    public float[] rotationAngles;

    public GameObject distancePanel;

    public AudioClip snapSound;

    private LineRenderer lineRenderer;
    private int currentIndex = 0;
    private GameObject currentStartPoint;
    private GameObject currentEndPoint;
    private bool drawingLine = false;
    private GameObject previousPrefab;
    private bool lineAtEndPoint = false;

    private GameObject activePanel;
    private AudioSource audioSource;

    private Vector3 originalPosition;
    private bool isObjectControlEnabled;

    // New variables to store line positions and distance
    private Vector3 startPointPosition;
    private Vector3 endPointPosition;
    private string storedDistanceText;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = false;

        // Add an AudioSource component if it's not already attached
        audioSource = gameObject.AddComponent<AudioSource>();

        SetPointsActive(false);

        nextPointButton.gameObject.SetActive(false);
        nextPointButton.onClick.AddListener(OnNextPointButtonClicked);

        descriptionButton.gameObject.SetActive(false);
        descriptionButton.onClick.AddListener(OnDescriptionButtonClicked);
        if (closeDescriptionButton != null)
        {
            closeDescriptionButton.onClick.AddListener(OnCloseDescriptionButtonClicked);
            closeDescriptionButton.gameObject.SetActive(false);
        }

        if (buttonsToDisable != null)
        {
            SetButtonsInteractable(true);
        }

        StartCoroutine(ShowPanelAndFirstPointAfterDelay(1.5f));
    }

    IEnumerator ShowPanelAndFirstPointAfterDelay(float delay)
    {
        if (teksAntroPanel != null)
        {
            teksAntroPanel.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            teksAntroPanel.SetActive(false);
        }
        yield return new WaitForSeconds(delay);
        ShowNextPoints();
    }

    void Update()
    {
        if (currentIndex >= startPoints.Length || IsPointerOverUIObject())
        {
            return;
        }

        if (Input.GetMouseButton(0) && drawingLine)
        {
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
                lineRenderer.enabled = false;
                distanceText.gameObject.SetActive(false);
                if (distancePanel != null)
                {
                    distancePanel.SetActive(false);
                }
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
            lineRenderer.SetPosition(0, currentStartPoint.transform.position);
            lineRenderer.SetPosition(1, currentEndPoint.transform.position);
            lineRenderer.enabled = true;
            DisplayStoredDistance();  // Ensure distance is always displayed
        }
    }

    void UpdateLine()
    {
        if (currentStartPoint == null || currentEndPoint == null)
            return;

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
        distance *= 100f;
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
        if (distancePanel != null)
        {
            distancePanel.SetActive(false);
        }
        if (activePanel != null)
        {
            activePanel.SetActive(false);
            closeDescriptionButton.gameObject.SetActive(false);
            SetButtonsInteractable(true);
            activePanel = null;
        }

        currentIndex++;
        if (currentIndex < startPoints.Length)
        {
            ShowNextPoints();
        }
        else
        {
            GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");
            foreach (GameObject bot in bots)
            {
                Destroy(bot);
            }

            if (previousPrefab != null)
            {
                Destroy(previousPrefab);
            }

            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy);
            }

            if (nextPrefab != null)
            {
                GameObject prefabInstance = Instantiate(nextPrefab);
                previousPrefab = prefabInstance;

                objectController.ResetRotationAndZoom();
            }
            else
            {
                Debug.LogError("Next prefab is not assigned!");
            }
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

                // Store the original position
                originalPosition = gameObject.transform.position;

                // Store the current positions of the line
                startPointPosition = lineRenderer.GetPosition(0);
                endPointPosition = lineRenderer.GetPosition(1);

                // Check if the object's name contains "Hand" or "TPose"
                float newXPosition;
                if (gameObject.name.Contains("Hand"))
                {
                    newXPosition = 2.6f;
                }
                else if (gameObject.name.Contains("TPose"))
                {
                    newXPosition = 0.8f;
                }
                else
                {
                    newXPosition = 0.9f;
                }

                // Move the object to the new X position
                Vector3 newPosition = gameObject.transform.position;
                newPosition.x = newXPosition;
                gameObject.transform.position = newPosition;

                // Disable the reset functionality
                Reset resetScript = gameObject.GetComponent<Reset>();
                if (resetScript != null)
                {
                    resetScript.SetCanReset(false);
                }

                // Enable only rotation in the ObjectControl script
                if (objectController != null)
                {
                    objectController.EnableRotationOnly(true);
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

            // Move the object back to its original position
            gameObject.transform.position = originalPosition;

            // Restore the line positions after the panel is closed
            RestoreLineRendererState();

            // Re-enable full control in the ObjectControl script
            if (objectController != null)
            {
                objectController.EnableRotationOnly(false);
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
        // Restore the line positions and ensure the line renderer is enabled
        if (currentStartPoint != null && currentEndPoint != null)
        {
            lineRenderer.SetPosition(0, startPointPosition);
            lineRenderer.SetPosition(1, endPointPosition);
            lineRenderer.enabled = true;

            // Display the stored distance text
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
