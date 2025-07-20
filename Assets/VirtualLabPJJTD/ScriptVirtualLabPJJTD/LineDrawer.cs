using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Line : MonoBehaviour
{
    public Material lineMaterial; // Material for the line
    public float lineWidth = 0.05f; // Width of the line
    private LineRenderer lineRenderer; // LineRenderer component
    private GameObject startObject; // Start object of the line
    private GameObject endObject; // End object of the line
    private bool isDrawing; // Flag to check if the line is being drawn

    void Start()
    {
        // Create a new GameObject with a LineRenderer component
        GameObject lineObject = new GameObject("Line");
        lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 2; // We'll have two points
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button down
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (startObject == null)
                {
                    startObject = hit.transform.gameObject;
                }
                else if (endObject == null)
                {
                    endObject = hit.transform.gameObject;
                    DrawLine();
                    CalculateDistance();
                }
            }
        }
    }

    void DrawLine()
    {
        if (startObject != null && endObject != null)
        {
            lineRenderer.SetPosition(0, startObject.transform.position);
            lineRenderer.SetPosition(1, endObject.transform.position);
        }
    }

    void CalculateDistance()
    {
        if (startObject != null && endObject != null)
        {
            float distance = Vector3.Distance(startObject.transform.position, endObject.transform.position);
            Debug.Log("Distance: " + distance);
        }
    }
}
