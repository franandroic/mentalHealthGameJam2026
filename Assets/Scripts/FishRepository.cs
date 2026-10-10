using System.Collections.Generic;
using UnityEngine;

public readonly struct FishData
{
    public readonly string PrefabVariantName;
    public readonly int AngerScore;
    public readonly int DisgustScore;
    public readonly int FearScore;
    public readonly int HappinessScore;
    public readonly int SadnessScore;
    public readonly Vector3 Scale;
    public readonly float Depth;
    public readonly float DepthRange;

    public float MinDepth => Depth - DepthRange;
    public float MaxDepth => Depth + DepthRange;

    public FishData(
        string prefabVariantName,
        int angerScore,
        int disgustScore,
        int fearScore,
        int happinessScore,
        int sadnessScore,
        Vector3 scale,
        float depth,
        float depthRange
        )
    {
        PrefabVariantName = prefabVariantName;
        AngerScore = angerScore;
        DisgustScore = disgustScore;
        FearScore = fearScore;
        HappinessScore = happinessScore;
        SadnessScore = sadnessScore;
        Scale = scale;
        Depth = depth;
        DepthRange = depthRange;
    }

    public int GetScore(Emotion emotion)
    {
        switch (emotion)
        {
            case Emotion.Anger: return AngerScore;
            case Emotion.Disgust: return DisgustScore;
            case Emotion.Fear: return FearScore;
            case Emotion.Happiness: return HappinessScore;
            case Emotion.Sadness: return SadnessScore;
            default: return 0;
        }
    }
}

//Hardcoded fish entries.
//Direct access:  FishDatabase.AnglerFish.AngerScore
//By variant name: FishDatabase.TryGetByVariantName("AnglerVariant", out FishData data)
public static class FishDatabase
{
    public static readonly FishData BasicFish = new FishData(
        prefabVariantName: "BasicFish",
        angerScore: 8,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 2,
        sadnessScore: 0,
        scale: new Vector3(1.5f, 1.5f, 1.5f),
        depth: 25f,
        depthRange: 5f
        );

    public static readonly FishData GreyFish = new FishData(
        prefabVariantName: "GreyFish",
        angerScore: 2,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 9,
        sadnessScore: 0,
        scale: new Vector3(0.5f, 0.5f, 0.5f),
        depth: 5f,
        depthRange: 5f
        );

    // Every entry, for looping/filtering. Add new fish here too.
    public static readonly IReadOnlyList<FishData> All = new[]
    {
        BasicFish,
        GreyFish
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