using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


[System.Serializable]
public class PointPair
{
    public GameObject startPoint;
    public GameObject endPoint;
    public float predefinedDistance;
    public GameObject panel; // Panel to display when reaching the endpoint
    public TextMeshProUGUI distanceText; // Distance text specific to this point pair
    public Button nextButton; // Next button specific to this point pair

    public GameObject customStartPoint;
}

public class Module3DrawLine : MonoBehaviour
{
    public Material lineMaterial;
    public PointPair[] pointPairs;
    public float snapDistanceThreshold = 0.1f;
    public Camera mainCamera; // Reference to the main camera

    private LineRenderer lineRenderer;
    private int currentIndex = 0;
    private GameObject currentStartPoint;
    private GameObject currentEndPoint;
    private GameObject currentPanel;
    private TextMeshProUGUI currentDistanceText;
    private Button currentNextButton; // Reference to the current Next button
    private bool drawingLine = false;
    private bool lineAtEndPoint = false; // Track if line has reached endpoint

    // Variables to store the camera's original position and rotation
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    public GameObject nextPrefabToSpawn;

    private Vector3 maxCameraPosition = new Vector3(2.55f, 3f, 8.02f);
    private Quaternion maxCameraRotation = Quaternion.Euler(90f, 0, 0);
    private Vector3 minCameraPosition = new Vector3(2.55f, 1.627f, 4.705f);
    private Quaternion minCameraRotation = Quaternion.Euler(0, 0, 0);

    private bool isLineRendererAnimationEffect = false;

    void Start()
    {
        float delayTime = 1.5f;

        // Auto-assign the main camera if it's not assigned in the Inspector
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("No Main Camera found. Please assign a camera manually.");
            }
        }

        if (gameObject.name == "NoAnimationModule 3(Clone)")
        {
            AutoFillPointPairs(new string[] { "Panel 1", "Panel 2", "Panel 3" });
        }
        else if (gameObject.name == "AnimasiModul 3 Ke 2(Clone)")
        {
            AutoFillPointPairs(new string[] { "Panel 4", "Panel 5", "Panel 6" });
            delayTime = 4f;
        }
        else if (gameObject.name == "AnimasiModul 3 Ke 3(Clone)")
        {
            AutoFillPointPairs(new string[] { "Panel 7", "Panel 8", "Panel 9" });
            delayTime = 4f; // Set a different delay time for this module if needed
        }

        // Store the camera’s original position and rotation
        if (mainCamera != null)
        {
            originalCameraPosition = mainCamera.transform.position;
            originalCameraRotation = mainCamera.transform.rotation;
        }

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.useWorldSpace = false; // Set to false to make the line follow the object's rotation
        lineRenderer.enabled = false;

        SetPointsAndPanelsActive(false);
        StartCoroutine(ShowFirstPointAfterDelay(delayTime));
    }

    private void AutoFillPointPairs(string[] panelNames)
    {
        // Find the "Module 3" object in the scene
        GameObject module3 = GameObject.Find("Module3Panels");
        if (module3 == null)
        {
            Debug.LogError("Module3Panels object not found. Please ensure it exists in the scene.");
            return;
        }

        // Find each specified panel within Module 3
        GameObject panel1 = module3.transform.Find(panelNames[0])?.gameObject;
        GameObject panel2 = module3.transform.Find(panelNames[1])?.gameObject;
        GameObject panel3 = module3.transform.Find(panelNames[2])?.gameObject;

        if (panel1 != null && panel2 != null && panel3 != null)
        {
            // Assign Panel, Distance Text, and Next Button for each point pair
            pointPairs[0].panel = panel1;
            pointPairs[0].distanceText = panel1.transform.Find("Distance")?.GetComponent<TextMeshProUGUI>();
            pointPairs[0].nextButton = panel1.transform.Find("Next")?.GetComponent<Button>();

            pointPairs[1].panel = panel2;
            pointPairs[1].distanceText = panel2.transform.Find("Distance")?.GetComponent<TextMeshProUGUI>();
            pointPairs[1].nextButton = panel2.transform.Find("Next")?.GetComponent<Button>();

            pointPairs[2].panel = panel3;
            pointPairs[2].distanceText = panel3.transform.Find("Distance")?.GetComponent<TextMeshProUGUI>();
            pointPairs[2].nextButton = panel3.transform.Find("Next")?.GetComponent<Button>();

            if (gameObject.name == "AnimasiModul 3 Ke 2(Clone)" || gameObject.name == "AnimasiModul 3 Ke 3(Clone)")
            {
                GameObject customPoint = gameObject.transform.Find("Point")?.gameObject;
                if (customPoint != null)
                {
                    pointPairs[2].customStartPoint = customPoint;
                }
                else
                {
                    Debug.LogError($"Custom start point 'Point' not found in {gameObject.name}.");
                }
            }
        }
        else
        {
            Debug.LogError("One or more specified panels are missing within Module 3. Please check that the panels exist under Module 3.");
        }
    }

    IEnumerator ShowFirstPointAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowNextPoints();
    }

    void Update()
    {
        if (currentIndex >= pointPairs.Length || IsPointerOverUIObject())
            return;

        if (Input.GetMouseButton(0) && drawingLine)
        {
            if (!lineRenderer.enabled)
                lineRenderer.enabled = true;

            UpdateLine();
        }

        if (Input.GetMouseButtonUp(0) && !lineAtEndPoint && !isLineRendererAnimationEffect)
        {
            // If the line is not at the endpoint, disable the line and hide the distance text
            lineRenderer.enabled = false;
            currentDistanceText?.gameObject.SetActive(false);
        }

        // Allow camera movement using scroll wheel when at index 2
        if (currentIndex == 2 && mainCamera != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0) // Scroll up
            {
                mainCamera.transform.position = maxCameraPosition;
                mainCamera.transform.rotation = maxCameraRotation;
            }
            else if (scroll < 0) // Scroll down
            {
                mainCamera.transform.position = minCameraPosition;
                mainCamera.transform.rotation = minCameraRotation;
            }
        }
    }

    void UpdateLine()
    {
        if (currentStartPoint == null || currentEndPoint == null)
            return;

        // Determine the actual starting point (custom or default)
        GameObject lineStartPoint = currentIndex == 2 && (gameObject.name == "AnimasiModul 3 Ke 2(Clone)" || gameObject.name == "AnimasiModul 3 Ke 3(Clone)") && pointPairs[currentIndex].customStartPoint != null ? pointPairs[currentIndex].customStartPoint : currentStartPoint;

        // Use local positions for the LineRenderer to follow rotation
        lineRenderer.SetPosition(0, lineStartPoint.transform.localPosition);

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
                Vector3 newPosition = transform.InverseTransformPoint(hit.point);
                lineRenderer.SetPosition(1, newPosition);

                float distance = Vector3.Distance(lineStartPoint.transform.position, newPosition);
                DisplayDistance(distance);

                lineRenderer.material.color = lineMaterial.color;
            }
            else
            {
                lineRenderer.SetPosition(1, lineStartPoint.transform.localPosition);
            }
        }
    }

    void SnapToEndPoint()
    {
        if(currentIndex == 2 && gameObject.name == "AnimasiModul 3 Ke 3(Clone)")
        {
            isLineRendererAnimationEffect = true;
            StartCoroutine(MoveLineRendererBasedOnTimes(currentStartPoint.transform.localPosition, currentEndPoint.transform.localPosition, 1));
        }
        else
        {
            // Set local positions to snap to endpoint within the object’s local space
            lineRenderer.SetPosition(1, currentEndPoint.transform.localPosition);
            AfterSnapped();
        }
    }

    IEnumerator MoveLineRendererBasedOnTimes(Vector3 start,Vector3 target, float time)
    {
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time; // Normalize time (0 to 1)
            lineRenderer.SetPosition(1, Vector3.Lerp(start, target, t));
            yield return null; // Wait for the next frame
        }

        lineRenderer.SetPosition(1, target); // Ensure it reaches exactly the target position

        AfterSnapped();
    }

    private void AfterSnapped()
    {
        isLineRendererAnimationEffect = false;

        DisplayPredefinedDistance(currentIndex);

        lineRenderer.material.color = Color.green;
        drawingLine = false;
        lineAtEndPoint = true; // Mark that the line has reached the endpoint

        OpenPanel(); // Open the panel only after snapping to endpoint
    }

    void ShowNextPoints()
    {
        if (currentIndex < pointPairs.Length)
        {
            currentStartPoint = pointPairs[currentIndex].startPoint;
            currentEndPoint = pointPairs[currentIndex].endPoint;
            currentPanel = pointPairs[currentIndex].panel;
            currentDistanceText = pointPairs[currentIndex].distanceText; // Assign specific distance text
            currentNextButton = pointPairs[currentIndex].nextButton; // Assign specific Next button

            SetPointsAndPanelsActive(false);
            currentStartPoint.SetActive(true);
            currentEndPoint.SetActive(true);
            currentNextButton.gameObject.SetActive(false); // Hide Next button initially

            if (currentIndex == 2 && pointPairs[currentIndex].customStartPoint != null)
            {
                pointPairs[currentIndex].customStartPoint.SetActive(true); // Make the custom point visible
            }

            drawingLine = true;
            lineAtEndPoint = false; // Reset the endpoint flag

            if (currentIndex == 2 && mainCamera != null && gameObject.name != "AnimasiModul 3 Ke 3(Clone)")
            {
                mainCamera.transform.position = new Vector3(2.55f, 3f, 8.02f); // Adjust position as needed
                mainCamera.transform.rotation = Quaternion.Euler(90f, mainCamera.transform.rotation.eulerAngles.y, mainCamera.transform.rotation.eulerAngles.z);
            }

            // Rotate object based on index when prefab is "AnimasiModul 3 Ke 2(Clone)"
            if (gameObject.name == "AnimasiModul 3 Ke 2(Clone)")
            {
                if (currentIndex == 0)
                {
                    transform.rotation = Quaternion.Euler(0, -180, 0);
                }
                else if (currentIndex == 1 || currentIndex == 2)
                {
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                }
            }
        }
    }

    void DisplayPredefinedDistance(int index)
    {
        if (index < pointPairs.Length && currentDistanceText != null)
        {
            int distance = Mathf.RoundToInt(pointPairs[index].predefinedDistance);

            // For the third point in AnimasiModul 3 Ke 3(Clone), display distance instead of angle
            if (index == 2 && gameObject.name == "AnimasiModul 3 Ke 3(Clone)")
            {
                currentDistanceText.text = $"Distance: {distance} cm"; // Use distance for the third point
            }
            else if (index == 2)
            {
                currentDistanceText.text = $"Angle: {distance}°"; // Original behavior for other modules
            }
            else
            {
                currentDistanceText.text = $"Distance: {distance} cm";
            }
            currentDistanceText.gameObject.SetActive(true);
        }
    }

    void DisplayDistance(float distance)
    {
        if (currentDistanceText != null)
        {
            // Convert to degrees if it’s the third point
            if (currentIndex == 2)
            {
                int angle = Mathf.RoundToInt(distance);
                currentDistanceText.text = $"Angle: {angle}°";
            }
            else
            {
                int distanceInCm = Mathf.RoundToInt(distance * 100f);
                currentDistanceText.text = $"Distance: {distanceInCm} cm";
            }
            currentDistanceText.gameObject.SetActive(true);
        }
    }

    void OpenPanel()
    {
        // Display the distance on the panel when it opens
        if (currentPanel != null)
        {
            currentPanel.SetActive(true);
            currentDistanceText.transform.SetParent(currentPanel.transform, false);
            currentNextButton.gameObject.SetActive(true); // Show Next button when the panel opens

            // Add listener for the next button
            currentNextButton.onClick.RemoveAllListeners(); // Clear previous listeners
            currentNextButton.onClick.AddListener(OnNextButtonClicked); // Add new listener
        }
    }

    void OnNextButtonClicked()
    {
        // Hide current start and end points and line renderer before moving to the next point pair
        currentStartPoint.SetActive(false);
        currentEndPoint.SetActive(false);
        lineRenderer.enabled = false; // Hide the drawn line
        currentPanel.SetActive(false); // Close the current panel

        currentIndex++; // Move to the next point pair
        if (currentIndex < pointPairs.Length)
        {
            ShowNextPoints(); // Show the next point pair
        }
        else
        {
            // Check if there is a next prefab to spawn
            if (nextPrefabToSpawn != null)
            {
                Instantiate(nextPrefabToSpawn, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }

        // If the third PointPair was just completed, reset camera and destroy "Bot" objects
        if (currentIndex == 3 && mainCamera != null)
        {
            mainCamera.transform.position = originalCameraPosition;
            mainCamera.transform.rotation = originalCameraRotation;
        }
    }

    void SetPointsAndPanelsActive(bool active)
    {
        foreach (PointPair pointPair in pointPairs)
        {
            pointPair.startPoint.SetActive(active);
            pointPair.endPoint.SetActive(active);
            pointPair.panel?.SetActive(false); // Hide all panels initially
            pointPair.distanceText?.gameObject.SetActive(false); // Hide all distance texts initially
            pointPair.nextButton?.gameObject.SetActive(false); // Hide all Next buttons initially
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
