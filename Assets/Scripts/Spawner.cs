using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    // Reference to the prefab to spawn
    [SerializeField] GameObject[] obstacleToSpawn;

    // Delay between spawns in seconds
    [SerializeField] float spawnDelay = 2f;

    // OPTIONAL: Forceful way to select the layer for the object
    // public string obstacleLayerName = "Snowball";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPrefabsWithDelay());
    }

    // Coroutine to spawn prefabs with delayd
    IEnumerator SpawnPrefabsWithDelay()
    {
        while (true) // Infinite loop
        {
            SpawnObstacle();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Method to spawn the prefab
    void SpawnObstacle()
    {
        // Select a random prefab from the array
        int randomIndex = Random.Range(0, obstacleToSpawn.Length);
        Debug.Log(obstacleToSpawn.Length);
        GameObject selectedPrefab = obstacleToSpawn[randomIndex];

        // Instantiate the selected prefab at the current position and rotation
        GameObject spawnedPrefab = Instantiate(selectedPrefab, transform.position, selectedPrefab.transform.rotation);

        // OPTIONAL: Assign the layer to the spawned prefab
        // spawnedPrefab.layer = LayerMask.NameToLayer(obstacleLayerName);
    }
}