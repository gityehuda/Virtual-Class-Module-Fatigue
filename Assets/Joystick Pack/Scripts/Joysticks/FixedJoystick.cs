using Binus.WebGL.Service;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick, IFixedJoystick
{
    public bool isJoystickUsed = false;
    public int joystickFingerId { get; set; }

    protected override void Start()
    {
#if UNITY_WEBGL
        Destroy(this.gameObject);
#endif
        ServiceLocator.RegisterService<IFixedJoystick>(this);
        base.Start();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        IGameplayManager gameplayManager = ServiceLocator.GetService<IGameplayManager>();
        bool someWindowActive = gameplayManager.CheckSomeWindowActive();

        if (someWindowActive)
        {
            return;
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                Debug.Log("Newest fingerId just touched: " + t.fingerId);
                joystickFingerId = t.fingerId;
                break; // stop after first new finger found
            }
        }

        isJoystickUsed = true;
        base.OnPointerDown(eventData);
        gameplayManager.ActivateWindow(WindowList.Joystick);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        IGameplayManager gameplayManager = ServiceLocator.GetService<IGameplayManager>();

        isJoystickUsed = false;
        base.OnPointerUp(eventData);
        gameplayManager.DeActivateWindow();
    }

    public bool IsJoystickUsed()
    {
        return isJoystickUsed;
    }
}