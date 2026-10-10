using System.Collections.Generic;
using UnityEngine;

//Spawns a random number of fish, each using a randomly chosen prefab variant.
public class FishSpawner : MonoBehaviour
{
    private List<GameObject> spawnedFish = new List<GameObject>();

    [Header("Prefabs")]
    [SerializeField] private GameObject[] fishVariants;

    [SerializeField] private int minFish = 5;
    [SerializeField] private int maxFish = 15;
    [SerializeField] private float depth = 0.0f;

    public int SpawnFish(Vector3 areaCenter, Vector2 areaSize)
    {
        if (fishVariants == null || fishVariants.Length == 0)
        {
            Debug.LogWarning("FishSpawner: no fish variants assigned.", this);
            return 0;
        }

        int count = Random.Range(minFish, maxFish + 1);

        for (int i = 0; i < count; i++)
        {
            GameObject prefab = fishVariants[Random.Range(0, fishVariants.Length)];
            if (prefab == null) continue;

            Vector3 position = areaCenter + new Vector3(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                Random.Range(-areaSize.y / 2f, areaSize.y / 2f),
                Random.Range(-depth / 2f, depth / 2f)
                );

            Quaternion rotation = Quaternion.identity;

            spawnedFish.Add(Instantiate(prefab, position, rotation));
        }

        return count;
    }

    public void ClearFish()
    {
        foreach (GameObject fish in spawnedFish)
        {
            if (fish != null) Destroy(fish);
        }
        spawnedFish.Clear();
    }
}