using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjectModul2 : MonoBehaviour
{
    public GameObject SpawnObjectPrefab;
    public Button spawnButton;

    private bool hasSpawned = false;

    private void Start()
    {
        // Add listener to spawn the object when the button is clicked
        spawnButton.onClick.AddListener(SpawnAndDestroyBot);
    }

    public void SpawnAndDestroyBot()
    {
        // If object has already been spawned, do nothing
        if (hasSpawned)
        {
            return;
        }

        // Find all objects with the tag "Bot" and destroy them
        GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");
        foreach (GameObject bot in bots)
        {
            Destroy(bot);
        }

        // Set the desired spawn location, rotation, and scale
        Vector3 spawnLocation = new Vector3(2.57f, 0.53f, 6.2246f);
        Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);
        Vector3 spawnScale = new Vector3(0.2f, 0.2f, 0.2f);

        // Spawn the new object with the specified location, rotation, and scale
        GameObject newObject = Instantiate(SpawnObjectPrefab, spawnLocation, spawnRotation) as GameObject;
        newObject.transform.localScale = spawnScale;

        // Set hasSpawned to true to indicate object has been spawned
        hasSpawned = true;

        // Deactivate the spawn button
        spawnButton.gameObject.SetActive(false);
    }
}
