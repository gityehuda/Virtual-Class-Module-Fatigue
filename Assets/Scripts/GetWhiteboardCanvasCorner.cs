using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWhiteboardCanvasCorner : MonoBehaviour
{
    [SerializeField] RectTransform whiteboardReal;

    public void GetWorldCorners()
    {
        var rt = GetComponent<RectTransform>();
        var whiteboardRealRectTransform = whiteboardReal.GetComponent<RectTransform>();
        rt.anchorMin = whiteboardRealRectTransform.anchorMin;
        rt.anchorMax = whiteboardRealRectTransform.anchorMax;
        rt.anchoredPosition = whiteboardRealRectTransform.anchoredPosition;
        rt.sizeDelta = whiteboardRealRectTransform.sizeDelta;

        if (rt != null)
        {
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);

            WhiteboardManager.instance.bottomLeft = corners[0];
            WhiteboardManager.instance.topRight = corners[2];
        }
    }
}
