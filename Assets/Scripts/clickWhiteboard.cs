using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickWhiteboard : MonoBehaviour
{
    public GameObject whiteboard;
    private void OnMouseDown()
    {
        whiteboard.SetActive(true);
    }
}
