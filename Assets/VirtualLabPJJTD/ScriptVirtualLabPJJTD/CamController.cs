using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class CamController : MonoBehaviour
{
    public CinemachineFreeLook cinemachineFreeLook;
    public Slider sensitivitySlider; 
    public TextMeshProUGUI sensitivityLabel;

    private float sensitivity = 5f; // Default sensitivity

    void Start()
    {
        // Check if a sensitivity value is saved in PlayerPrefs, and load it
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            sensitivity = PlayerPrefs.GetFloat("Sensitivity"); // Load the saved sensitivity value
        }
        else
        {
            sensitivity = 5f; // If no value is saved, use the default value
        }

        // Set the sensitivity slider value based on the loaded sensitivity
        sensitivitySlider.value = sensitivity;

        // Set the slider range to go from 0 to 10
        sensitivitySlider.minValue = 0f;
        sensitivitySlider.maxValue = 10f;

        // Link the slider value change to a function that updates the sensitivity
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);

        // Show the initial value of sensitivity on the UI
        sensitivityLabel.text = sensitivity.ToString("F2");

        // Apply the initial sensitivity to the camera
        ApplySensitivity();
    }

    void UpdateSensitivity(float value)
    {
        // Update the sensitivity value based on the slider's value
        sensitivity = value;

        // Update camera speed according to the new sensitivity
        ApplySensitivity();

        // Update the label on the UI with the new sensitivity
        if (sensitivityLabel != null)
        {
            sensitivityLabel.text = sensitivity.ToString("F2");
        }

        // Save the new sensitivity to PlayerPrefs
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        PlayerPrefs.Save(); // Make sure to save the value
    }

    void ApplySensitivity()
    {
        // Update camera speed according to the sensitivity
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = sensitivity * 50f;  // Adjust multiplier for desired sensitivity
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = sensitivity * 700f; // Adjust multiplier for desired sensitivity
    }

    void Update()
    {
        // Track if the right mouse button is held
        if (Input.GetMouseButton(1))  // Right-click is held
        {
            cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = false; // Disable recentering while right-click is held
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = sensitivity * 50f;  // Use updated sensitivity
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = sensitivity * 700f; // Use updated sensitivity
        }
        else
        {
            cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = true; // Enable recentering when right-click is not held
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0f;  // Stop vertical movement when not right-clicking
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0f;  // Stop horizontal movement when not right-clicking
        }

        // Camera movement logic for other controls
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                cinemachineFreeLook.m_YAxis.Value += 1f * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                cinemachineFreeLook.m_YAxis.Value -= 1f * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                cinemachineFreeLook.m_XAxis.Value += 360f * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                cinemachineFreeLook.m_XAxis.Value -= 360f * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.I))
        {
            cinemachineFreeLook.m_YAxis.Value += 1f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.K))
        {
            cinemachineFreeLook.m_YAxis.Value -= 1f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.L))
        {
            cinemachineFreeLook.m_XAxis.Value += 360f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.J))
        {
            cinemachineFreeLook.m_XAxis.Value -= 360f * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(1))  // Right-click press
        {
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = sensitivity * 50f;  // Use updated sensitivity
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = sensitivity * 700f; // Use updated sensitivity
        }

        if (Input.GetMouseButtonUp(1))  // Right-click release
        {
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0f;
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0f;
        }
    }
}
