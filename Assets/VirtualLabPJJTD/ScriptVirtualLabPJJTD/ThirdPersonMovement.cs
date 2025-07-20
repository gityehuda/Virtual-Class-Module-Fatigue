using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    CharacterController controller;
    public Transform cam;
    public GameObject confirmationPanel; // Reference to the confirmation panel
    public GameObject conversationCanvas; // Reference to the conversation canvas

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Animator animator;
    Vector3 velocity;
    bool hasOpenedConversationCanvas = false; // Flag to check if the conversation canvas has been opened

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        controller.center = new Vector3(0f, 0.64f, -0.06f);
        controller.height = 1.17f;
    }

    void Update()
    {
        // Check if the confirmation panel is active or if the conversation canvas is active and hasn't been opened before
        if ((confirmationPanel != null && confirmationPanel.activeInHierarchy) || 
            (conversationCanvas != null && IsAnyPanelActive(conversationCanvas) && !hasOpenedConversationCanvas))
        {
            // If either is active, do not allow movement
            return;
        }

        // If the conversation canvas is active, mark it as opened
        if (conversationCanvas != null && IsAnyPanelActive(conversationCanvas))
        {
            hasOpenedConversationCanvas = true;
        }

        // Movement Input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Movement Calculation
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

            // Animation control based on movement
            if (direction == Vector3.zero)
            {
                animator.SetInteger("Animation", 0);
            }
            else
            {
                animator.SetInteger("Animation", 1);
            }
        }
        else
        {
            // If not moving, set animation to idle
            animator.SetInteger("Animation", 0);
        }
    }

    bool IsAnyPanelActive(GameObject canvas)
    {
        foreach (Transform child in canvas.transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
}
