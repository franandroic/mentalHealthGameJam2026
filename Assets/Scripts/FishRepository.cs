using System.Collections.Generic;
using UnityEngine;

public readonly struct FishData
{
    public readonly string PrefabVariantName;
    public readonly int AngerScore;
    public readonly int HappinessScore;
    public readonly Vector3 Scale;
    public readonly float Depth;

    public FishData(string prefabVariantName, int angerScore, int happinessScore, Vector3 scale, float depth)
    {
        PrefabVariantName = prefabVariantName;
        AngerScore = angerScore;
        HappinessScore = happinessScore;
        Scale = scale;
        Depth = depth;
    }
}

//Hardcoded fish entries.
//Direct access:  FishDatabase.AnglerFish.AngerScore
//By variant name: FishDatabase.TryGetByVariantName("AnglerVariant", out FishData data)
public static class FishDatabase
{
    public static readonly FishData TestFish1 = new FishData(
        prefabVariantName: "TestFish1",
        angerScore: 8,
        happinessScore: 2,
        scale: new Vector3(1.5f, 1.5f, 1.5f),
        depth: 25f);

    public static readonly FishData TestFish2 = new FishData(
        prefabVariantName: "TestFish2",
        angerScore: 2,
        happinessScore: 9,
        scale: new Vector3(0.5f, 0.5f, 0.5f),
        depth: 5f);

    // Every entry, for looping/filtering. Add new fish here too.
    public static readonly IReadOnlyList<FishData> All = new[]
    {
        TestFish1,
        TestFish2
    };

    // Lookup table built once from All. Assumes one entry per variant name.
    private static readonly Dictionary<string, FishData> ByVariantName = BuildLookup();

    private static Dictionary<string, FishData> BuildLookup()
    {
        var lookup = new Dictionary<string, FishData>(All.Count);
        foreach (FishData fish in All)
            lookup.Add(fish.PrefabVariantName, fish);
        return lookup;
    }

    public static bool TryGetByVariantName(string variantName, out FishData data)
    {
        return ByVariantName.TryGetValue(variantName, out data);
    }
}