using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

[System.Serializable]
public struct PredefinedDistances
{
    public string label;
    public string distance;
}

public class AntroControl3 : MonoBehaviour
{
    public Material lineMaterial;
    public GameObject[] startPoints;
    public GameObject[] endPoints;
    public TextMeshProUGUI distanceText;
    public GameObject distancePanel; 
    public GameObject objectToDestroy;
    public GameObject[] panelLine;
    public GameObject[] panelScore;
    public float snapDistanceThreshold = 0.5f;
    public PredefinedDistances[] predefinedDistances;
    public ObjectControl objectController;
    public Button panelLineNextButton;
    public Button panelScoreNextButton;
    public Button chooseButton;

    private LineRenderer lineRenderer;
    private int currentIndex = 0;
    private int panelLineIndex = 0;
    private int panelScoreIndex = 0;
    private GameObject currentStartPoint;
    private GameObject currentEndPoint;
    private bool drawingLine = false;
    private bool panelLineClosed = false;
    private bool panelScoreVisible = false;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = false;

        SetPointsActive(false);

        // Hide the navigation buttons initially
        if (panelLineNextButton != null)
        {
            panelLineNextButton.gameObject.SetActive(false);
            panelLineNextButton.onClick.AddListener(OnPanelLineNextButtonClick);
        }

        if (panelScoreNextButton != null)
        {
            panelScoreNextButton.gameObject.SetActive(false);
            panelScoreNextButton.onClick.AddListener(OnPanelScoreNextButtonClick);
        }

        // Hide the choose button initially
        if (chooseButton != null)
        {
            chooseButton.gameObject.SetActive(false);
        }

        // Disable object control initially
        if (objectController != null)
        {
            objectController.enabled = false;
        }

        StartCoroutine(ShowFirstPointAfterDelay(1.5f));
    }

    IEnumerator ShowFirstPointAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowNextPoints();
    }

    void Update()
    {
        if (currentIndex >= startPoints.Length || IsPointerOverUIObject())
        {
            return;
        }

        if (Input.GetMouseButton(0) && drawingLine && panelLineClosed) 
        {
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }
            UpdateLine();
        }

        if (Input.GetMouseButtonUp(0) && panelLineClosed) 
        {
            if (IsLineAtEndPoint())
            {
                SnapToEndPoint();
                if (!panelScoreVisible)
                {
                    ShowPanel(panelScore); 
                    panelScoreVisible = true;
                }
            }
            else
            {
                lineRenderer.enabled = false;
                distanceText.gameObject.SetActive(false);
                if (distancePanel != null) 
                {
                    distancePanel.SetActive(false);
                }
            }
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

        if (drawingLine)
        {
            drawingLine = false;
        }

        // Disable movement and rotation for all objects with the tag 'Bot'
        DisableBotMovementAndRotation();
    }

    void ShowNextPoints()
    {
        if (currentIndex < startPoints.Length)
        {
            currentStartPoint = startPoints[currentIndex];
            currentEndPoint = endPoints[currentIndex];

            SetPointsActive(false);

            // Activate start and end points
            currentStartPoint.SetActive(true);
            currentEndPoint.SetActive(true);

            // Change start point color to blue
            Renderer startRenderer = currentStartPoint.GetComponent<Renderer>();
            if (startRenderer != null)
            {
                startRenderer.material.color = Color.blue;
            }

            // Change end point color to red
            Renderer endRenderer = currentEndPoint.GetComponent<Renderer>();
            if (endRenderer != null)
            {
                endRenderer.material.color = Color.red;
            }

            // Enable line renderer and set its initial position
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, currentStartPoint.transform.position);
            lineRenderer.SetPosition(1, currentStartPoint.transform.position);

            drawingLine = true;

            // Show the first panelLine when starting the new set of points
            ShowPanel(panelLine);
        }
    }

    void DisplayPredefinedDistance(int index)
    {
        if (index < predefinedDistances.Length)
        {
            string label = predefinedDistances[index].label;
            string distance = predefinedDistances[index].distance;

            distanceText.text = "" + label + ": " + distance + " cm";
            distanceText.gameObject.SetActive(true);
        }
        else
        {
            distanceText.text = "Distance: N/A";
            distanceText.gameObject.SetActive(true);
        }

        // Show the panel
        if (distancePanel != null)
        {
            distancePanel.SetActive(true);
        }
    }

    void DisplayDistance(float distance)
    {
        distance *= 100f; // Convert to centimeters
        string label = currentIndex < predefinedDistances.Length ? predefinedDistances[currentIndex].label : "N/A";

        distanceText.text = "" + label + ": " + distance.ToString("F2") + " cm";
        distanceText.gameObject.SetActive(true);

        // Show the panel
        if (distancePanel != null)
        {
            distancePanel.SetActive(true);
        }
    }

    void OnPanelLineNextButtonClick()
    {
        if (panelLine != null && panelLineIndex < panelLine.Length)
        {
            // Handle the panelLine navigation
            if (panelLineIndex < panelLine.Length - 1)
            {
                // Hide the current panelLine
                if (panelLine[panelLineIndex] != null)
                {
                    Debug.Log("Hiding panelLine: " + panelLine[panelLineIndex].name);
                    panelLine[panelLineIndex].SetActive(false);
                }
                panelLineIndex++;
                ShowPanel(panelLine);
            }
            else if (panelLineIndex == panelLine.Length - 1)
            {
                // This is the last panelLine, just close it and do nothing
                if (panelLine[panelLineIndex] != null)
                {
                    Debug.Log("Hiding last panelLine: " + panelLine[panelLineIndex].name);
                    panelLine[panelLineIndex].SetActive(false);
                }

                // Set the flag to indicate panelLine is closed
                panelLineClosed = true;

                // Hide the next button
                if (panelLineNextButton != null)
                {
                    panelLineNextButton.gameObject.SetActive(false);
                }

                // Enable object control when panelLine is closed
                if (objectController != null)
                {
                    objectController.enabled = true;
                }
            }
        }
    }

    void OnPanelScoreNextButtonClick()
    {
        if (panelScore != null && panelScoreIndex < panelScore.Length)
        {
            if (panelScoreIndex < panelScore.Length)
            {
                // Hide the current panelScore
                if (panelScore[panelScoreIndex] != null)
                {
                    Debug.Log("Hiding panelScore: " + panelScore[panelScoreIndex].name);
                    panelScore[panelScoreIndex].SetActive(false);
                }
                panelScoreIndex++;
            }

            // Show the next panelScore if available
            if (panelScoreIndex < panelScore.Length)
            {
                ShowPanel(panelScore);
            }
            else
            {
                // All panelScore panels have been shown, hide the button
                if (panelScoreNextButton != null)
                {
                    panelScoreNextButton.gameObject.SetActive(false);
                }

                // Destroy the object and proceed to the next points
                if (objectToDestroy != null)
                {
                    Debug.Log("Destroying objectToDestroy: " + objectToDestroy.name);
                    Destroy(objectToDestroy);

                    // Hide the distance text when the object is destroyed
                    if (distanceText != null)
                    {
                        distanceText.gameObject.SetActive(false);
                        if (distancePanel != null)
                        {
                            distancePanel.SetActive(false); 
                        }
                    }

                    // Show the choose button when the object is destroyed
                    if (chooseButton != null)
                    {
                        chooseButton.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    void ShowPanel(GameObject[] panels)
    {
        if (panels != null && panels.Length > 0)
        {
            // Deactivate all panels first
            foreach (GameObject panel in panels)
            {
                if (panel != null)
                {
                    panel.SetActive(false);
                }
            }

            // Activate the panel corresponding to the current index, if valid
            GameObject currentPanel = null;
            if (panels == panelLine)
            {
                currentPanel = panelLine[panelLineIndex];
            }
            else if (panels == panelScore)
            {
                currentPanel = panelScore[panelScoreIndex];
            }

            if (currentPanel != null)
            {
                Debug.Log("Showing panel: " + currentPanel.name);
                currentPanel.SetActive(true);

                // Show the panelLineNextButton when showing panelLine
                if (panels == panelLine && panelLineNextButton != null)
                {
                    panelLineNextButton.gameObject.SetActive(true);
                }
                else if (panelLineNextButton != null)
                {
                    panelLineNextButton.gameObject.SetActive(false);
                }

                // Show the panelScoreNextButton when showing panelScore
                if (panels == panelScore && panelScoreNextButton != null)
                {
                    panelScoreNextButton.gameObject.SetActive(true);
                }
                else if (panelScoreNextButton != null)
                {
                    panelScoreNextButton.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.LogWarning("No panel assigned for index.");
            }
        }
        else
        {
            Debug.LogError("Panels array is not assigned or empty!");
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

    void DisableBotMovementAndRotation()
    {
        // Find all objects with the tag 'Bot'
        GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");

        foreach (GameObject bot in bots)
        {
            // Optionally, ensure the bot has a Rigidbody component
            Rigidbody rb = bot.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // Disable physics-based movement
            }

            // Disable movement and rotation scripts if they exist
            ObjectControl objectControlScript = bot.GetComponent<ObjectControl>();
            if (objectControlScript != null)
            {
                objectControlScript.enabled = false;
            }
        }
    }
}
