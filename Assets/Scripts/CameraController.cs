using UnityEngine;
using Cinemachine;
using Binus.WebGL.Service;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public CinemachineFreeLook cinemachineFreeLook;
    public bool isCameraMoving = true;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCameraMoving) return;

#if UNITY_WEBGL
        WebGLCamera();
#elif UNITY_ANDROID || UNITY_IOS
        MobileCamera();
#endif
    }

    private void WebGLCamera()
    {
        //Moving camera using ctrl + arrow
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

        //Moving camera using IJKL key
        if (Input.GetKey(KeyCode.I))
        {
            cinemachineFreeLook.m_YAxis.Value -= 1f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.K))
        {
            cinemachineFreeLook.m_YAxis.Value += 1f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.L))
        {
            cinemachineFreeLook.m_XAxis.Value += 360f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.J))
        {
            cinemachineFreeLook.m_XAxis.Value -= 360f * Time.deltaTime;
        }

        //Moving camera using Mouse
        if (Input.GetMouseButtonDown(1))
        {
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = 5f;
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = 700f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0f;
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0f;
        }
    }

    private void MobileCamera()
    {
        bool isMobileCameraMoving = false;
        Touch touch = new Touch();

        IFixedJoystick fixedJoystick = ServiceLocator.GetService<IFixedJoystick>();
        bool isFixedJoystickUsed = fixedJoystick.IsJoystickUsed();

        if (isFixedJoystickUsed)
        {
            if (Input.touchCount > 1)
            {
                touch = Input.GetTouch((fixedJoystick.joystickFingerId == 1 ? 0 : 1));
                isMobileCameraMoving = (touch.phase == TouchPhase.Moved);
            }
        }
        else
        {
            if (Input.touchCount == 1)
            {
                touch = Input.GetTouch(0);
                isMobileCameraMoving = (touch.phase == TouchPhase.Moved);
            }
        }

        if (isMobileCameraMoving)
        {
            float deltaX = touch.deltaPosition.x * 0.1f;
            float deltaY = touch.deltaPosition.y * 0.1f;

            cinemachineFreeLook.m_XAxis.Value = Mathf.Lerp(cinemachineFreeLook.m_XAxis.Value, cinemachineFreeLook.m_XAxis.Value + deltaX, Time.deltaTime * 50f);
            cinemachineFreeLook.m_YAxis.Value = Mathf.Lerp(cinemachineFreeLook.m_YAxis.Value, cinemachineFreeLook.m_YAxis.Value - deltaY, Time.deltaTime * 1f);
        }
    }

    public void ActiveCameraMoving()
    {
        isCameraMoving = true;
    }

    public void DeActiveCameraMoving()
    {
        isCameraMoving = false;
    }
}
