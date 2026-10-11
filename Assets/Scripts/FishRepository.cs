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
    public readonly float SpawnWeight;

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
        float depthRange,
        float spawnWeight
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
        SpawnWeight = spawnWeight;
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
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 1,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 17.5f,
        depthRange: 17.5f,
        spawnWeight: 100
        );

    public static readonly FishData CloudFish = new FishData(
        prefabVariantName: "CloudFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 2,
        sadnessScore: 1,
        scale: new Vector3(1.5f, 1.5f, 1.5f),
        depth: 7.5f,
        depthRange: 5f,
        spawnWeight: 8
        );

    public static readonly FishData CoralFish = new FishData(
        prefabVariantName: "CoralFish",
        angerScore: 0,
        disgustScore: 1,
        fearScore: 1,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(2f, 2f, 2f),
        depth: 9f,
        depthRange: 4f,
        spawnWeight: 5
        );

    public static readonly FishData DemonFish = new FishData(
        prefabVariantName: "DemonFish",
        angerScore: 3,
        disgustScore: 4,
        fearScore: 2,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1.6f, 1.6f, 1.6f),
        depth: 27.5f,
        depthRange: 4f,
        spawnWeight: 3
        );

    public static readonly FishData ElectroFish = new FishData(
        prefabVariantName: "ElectroFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 3,
        happinessScore: 0,
        sadnessScore: 4,
        scale: new Vector3(1.5f, 1.5f, 1.5f),
        depth: 16f,
        depthRange: 3f,
        spawnWeight: 3
        );

    public static readonly FishData FroggyFish = new FishData(
        prefabVariantName: "FroggyFish",
        angerScore: 0,
        disgustScore: 1,
        fearScore: 0,
        happinessScore: 3,
        sadnessScore: 1,
        scale: new Vector3(1.3f, 1.3f, 1.3f),
        depth: 21f,
        depthRange: 5f,
        spawnWeight: 5
        );

    public static readonly FishData GreyFish = new FishData(
        prefabVariantName: "GreyFish",
        angerScore: 3,
        disgustScore: 3,
        fearScore: 2,
        happinessScore: 0,
        sadnessScore: 2,
        scale: new Vector3(2f, 2f, 2f),
        depth: 4f,
        depthRange: 3f,
        spawnWeight: 8
        );

    public static readonly FishData IceFish = new FishData(
        prefabVariantName: "IceFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 5,
        sadnessScore: 0,
        scale: new Vector3(1.6f, 1.6f, 1.6f),
        depth: 26f,
        depthRange: 3f,
        spawnWeight: 2
        );

    public static readonly FishData MoustacheFish = new FishData(
        prefabVariantName: "MoustacheFish",
        angerScore: 0,
        disgustScore: 1,
        fearScore: 0,
        happinessScore: 1,
        sadnessScore: 0,
        scale: new Vector3(1.1f, 1.1f, 1.1f),
        depth: 29f,
        depthRange: 6f,
        spawnWeight: 5
        );

    public static readonly FishData PufferFish = new FishData(
        prefabVariantName: "PufferFish",
        angerScore: 0,
        disgustScore: 1,
        fearScore: 3,
        happinessScore: 0,
        sadnessScore: 3,
        scale: new Vector3(2f, 2f, 2f),
        depth: 31f,
        depthRange: 3f,
        spawnWeight: 3
        );

    public static readonly FishData RedFish = new FishData(
        prefabVariantName: "RedFish",
        angerScore: 1,
        disgustScore: 0,
        fearScore: 3,
        happinessScore: 0,
        sadnessScore: 5,
        scale: new Vector3(2f, 2f, 2f),
        depth: 33.5f,
        depthRange: 1.5f,
        spawnWeight: 2
        );

    public static readonly FishData SharkFish = new FishData(
        prefabVariantName: "SharkFish",
        angerScore: 2,
        disgustScore: 3,
        fearScore: 1,
        happinessScore: 0,
        sadnessScore: 2,
        scale: new Vector3(1.4f, 1.4f, 1.4f),
        depth: 14f,
        depthRange: 4f,
        spawnWeight: 4
        );

    public static readonly FishData ShrimpFish = new FishData(
        prefabVariantName: "ShrimpFish",
        angerScore: 3,
        disgustScore: 4,
        fearScore: 5,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1.4f, 1.4f, 1.4f),
        depth: 17f,
        depthRange: 3f,
        spawnWeight: 2
        );

    public static readonly FishData SpikeFish = new FishData(
        prefabVariantName: "SpikeFish",
        angerScore: 5,
        disgustScore: 1,
        fearScore: 4,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1.2f, 1.2f, 1.2f),
        depth: 22.5f,
        depthRange: 5f,
        spawnWeight: 4
        );

    public static readonly FishData TinyFish = new FishData(
        prefabVariantName: "TinyFish",
        angerScore: 3,
        disgustScore: 2,
        fearScore: 1,
        happinessScore: 0,
        sadnessScore: 1,
        scale: new Vector3(1.1f, 1.1f, 1.1f),
        depth: 19f,
        depthRange: 6f,
        spawnWeight: 8
        );

    public static readonly FishData UglyFish = new FishData(
        prefabVariantName: "UglyFish",
        angerScore: 1,
        disgustScore: 5,
        fearScore: 4,
        happinessScore: 0,
        sadnessScore: 2,
        scale: new Vector3(2f, 2f, 2f),
        depth: 32f,
        depthRange: 2.5f,
        spawnWeight: 2
        );

    public static readonly FishData WhaleFish = new FishData(
        prefabVariantName: "WhaleFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 4,
        sadnessScore: 2,
        scale: new Vector3(5f, 5f, 5f),
        depth: 10f,
        depthRange: 7.5f,
        spawnWeight: 1
        );

    public static readonly FishData WhiteFish = new FishData(
        prefabVariantName: "WhiteFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 4,
        sadnessScore: 0,
        scale: new Vector3(1.5f, 1.5f, 1.5f),
        depth: 11f,
        depthRange: 6f,
        spawnWeight: 5
        );

    // Every entry, for looping/filtering. Add new fish here too.
    public static readonly IReadOnlyList<FishData> All = new[]
    {
        BasicFish,
        CloudFish,
        CoralFish,
        DemonFish,
        ElectroFish,
        FroggyFish,
        GreyFish,
        MoustacheFish,
        PufferFish,
        RedFish,
        SharkFish,
        ShrimpFish,
        SpikeFish,
        TinyFish,
        UglyFish,
        WhaleFish,
        WhiteFish
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