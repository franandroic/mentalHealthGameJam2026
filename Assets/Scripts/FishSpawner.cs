using System.Collections.Generic;
using UnityEngine;

//Spawns a random number of fish, each using a randomly chosen prefab variant.
//Each variant only spawns inside its depth band from FishDatabase and gets that entry's scale.
public class FishSpawner : MonoBehaviour
{
    private struct Variant
    {
        public GameObject Prefab;
        public FishData Data;
    }

    [Header("Prefabs (asset name must match a FishDatabase entry)")]
    [SerializeField] private GameObject[] fishVariants;

    [Header("Amount (per call, i.e. per area)")]
    [SerializeField] private int minFish = 5;
    [SerializeField] private int maxFish = 15;

    [Header("Fish Z")]
    [SerializeField] private float fishZ = 0f;

    [Header("References")]
    [SerializeField] private Transform waterSurface;

    private readonly List<GameObject> spawnedFish = new List<GameObject>();
    private readonly List<Variant> variants = new List<Variant>();
    private readonly List<Variant> candidates = new List<Variant>();

    private void Awake()
    {
        // Pair every prefab with its FishDatabase entry once, so spawning doesn't do lookups.
        foreach (GameObject prefab in fishVariants)
        {
            if (prefab == null) continue;

            if (FishDatabase.TryGetByVariantName(prefab.name, out FishData data))
                variants.Add(new Variant { Prefab = prefab, Data = data });
            else
                Debug.LogWarning("FishSpawner: no FishDatabase entry named '" + prefab.name + "', it will not be spawned.", this);
        }
    }

    // Fills a box centered on areaCenter. areaSize is the width/height in world units (e.g. the screen).
    public int SpawnFish(Vector3 areaCenter, Vector2 areaSize)
    {
        if (variants.Count == 0)
        {
            Debug.LogWarning("FishSpawner: no usable fish variants assigned.", this);
            return 0;
        }
        if (waterSurface == null)
        {
            Debug.LogWarning("FishSpawner: water surface not assigned.", this);
            return 0;
        }

        // Depth range (below the surface, positive down) that this area covers.
        float surfaceY = waterSurface.position.y;
        float areaDepthFrom = surfaceY - (areaCenter.y + areaSize.y / 2f);
        float areaDepthTo = surfaceY - (areaCenter.y - areaSize.y / 2f);

        // Only variants whose depth band overlaps this area can spawn here.
        candidates.Clear();
        foreach (Variant v in variants)
        {
            if (v.Data.MaxDepth >= areaDepthFrom && v.Data.MinDepth <= areaDepthTo)
                candidates.Add(v);
        }
        if (candidates.Count == 0) return 0;

        int count = Random.Range(minFish, maxFish + 1);

        for (int i = 0; i < count; i++)
        {
            Variant v = candidates[Random.Range(0, candidates.Count)];

            // Pick the fish's depth inside the part of its band that falls in this area.
            float from = Mathf.Max(areaDepthFrom, v.Data.MinDepth);
            float to = Mathf.Min(areaDepthTo, v.Data.MaxDepth);
            float fishDepth = Random.Range(from, to);

            Vector3 position = new Vector3(
                areaCenter.x + Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                surfaceY - fishDepth,
                areaCenter.z + Random.Range(-fishZ / 2f, fishZ / 2f)
                );

            GameObject fish = Instantiate(v.Prefab, position, Quaternion.identity);
            fish.transform.localScale = v.Data.Scale;

            FishVariant tag = fish.GetComponent<FishVariant>();
            if (tag == null) tag = fish.AddComponent<FishVariant>();
            tag.VariantName = v.Data.PrefabVariantName;

            spawnedFish.Add(fish);
        }

        return count;
    }

    // Destroys every fish spawned so far (skips ones that were already destroyed, e.g. caught).
    public void ClearFish()
    {
        foreach (GameObject fish in spawnedFish)
        {
            if (fish != null) Destroy(fish);
        }
        spawnedFish.Clear();
    }
}