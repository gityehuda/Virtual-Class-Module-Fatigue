using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShareScreenRoom : MonoBehaviour
{
    public static ShareScreenRoom instance;
    public RawImage shareScreenImage;

    private void Awake()
    {
        instance = this;
        shareScreenImage = GetComponentInChildren<RawImage>();
    }

    public void StartShareScreen()
    {
        gameObject.SetActive(true);
    }

    public void StopShareScreen()
    {
        gameObject.SetActive(false);
    }

    public static bool IsPointerOverNonWorldSpaceUI(int pointerId)
    {
        if (EventSystem.current == null) return false;

        var pointerData = new PointerEventData(EventSystem.current) { pointerId = pointerId };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            var canvas = result.gameObject.GetComponentInChildren<Canvas>();
            if (canvas != null && canvas.renderMode != RenderMode.WorldSpace)
                return true;
        }

        return false;
    }

    private void Update()
    {
        // === PC Mouse Input ===
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && IsPointerOverNonWorldSpaceUI(-1))
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
        if (!GameplayManager.instance.CheckSomeWindowActive())
        {
            ShareScreenManager.instance.parentShareScreenTemplate.SetActive(true);
            ShareScreenManager.instance.isShareUI = true;
            GameplayManager.instance.ActivateWindow(WindowList.BreakoutRoom);
        }
    }
}
