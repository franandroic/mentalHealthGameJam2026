using UnityEngine;

//Spawns a random number of fish, each using a randomly chosen prefab variant.
public class FishSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject[] fishVariants;

    [Header("Amount")]
    [SerializeField] private int minFish = 5;
    [SerializeField] private int maxFish = 15;

    [Header("Spawn Area (box centered on this object)")]
    [SerializeField] private Vector3 areaSize = new Vector3(20f, 10f, 20f);

    public int SpawnFish(Vector3 areaCenter)
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
                Random.Range(-areaSize.z / 2f, areaSize.z / 2f)
                );

            Quaternion rotation = Quaternion.identity;

            Instantiate(prefab, position, rotation);
        }

        return count;
    }
}