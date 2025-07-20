using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryReducers : MonoBehaviour
{
    private float unloadInterval = 30f; // Time in seconds between unloads
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= unloadInterval)
        {
            Resources.UnloadUnusedAssets();
            System.GC.Collect(); // Force garbage collection
            timer = 0f;
        }
    }
}
