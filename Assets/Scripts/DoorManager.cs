using UnityEngine;
using UnityEngine.EventSystems;

public class DoorManager : MonoBehaviour
{
    private void Update()
    {
        // === PC Mouse Input ===
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                // Clicked UI — skip
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastToSelf(ray);
        }

        // === Mobile Touch Input ===
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                // Touched UI — skip
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastToSelf(ray);
        }
    }

    private void RaycastToSelf(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                OnClicked();
            }
        }
    }

    private void OnClicked()
    {
        Debug.Log("3D Object clicked: " + gameObject.name);
        // Your real logic here
        try
        {
            if (GameplayManager.instance.CheckSomeWindowActive()) return;

            if (MainData.instance.breakoutRoomId != "")
            {
                MainData.instance.roomName = "CreativeClass";
                StartCoroutine(InBreakoutRoomManager.instance.LeaveBreakoutRoom());
                return;
            }
            BreakoutRoomManager.instance.OpenBreakoutRoomMember();
        }
        catch
        {

        }
    }

}
