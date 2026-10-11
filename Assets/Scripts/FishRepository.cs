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
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 1,
        sadnessScore: 0,
        scale: new Vector3(1.5f, 1.5f, 1.5f),
        depth: 25f,
        depthRange: 5f
        );

    public static readonly FishData CloudFish = new FishData(
        prefabVariantName: "CloudFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData CoralFish = new FishData(
        prefabVariantName: "CoralFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData DemonFish = new FishData(
        prefabVariantName: "DemonFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData ElectroFish = new FishData(
        prefabVariantName: "ElectroFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData FroggyFish = new FishData(
        prefabVariantName: "FroggyFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData GreyFish = new FishData(
        prefabVariantName: "GreyFish",
        angerScore: 2,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(0.5f, 0.5f, 0.5f),
        depth: 5f,
        depthRange: 5f
        );

    public static readonly FishData IceFish = new FishData(
        prefabVariantName: "IceFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData MoustacheFish = new FishData(
        prefabVariantName: "MoustacheFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData PufferFish = new FishData(
        prefabVariantName: "PufferFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData RedFish = new FishData(
        prefabVariantName: "RedFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData SharkFish = new FishData(
        prefabVariantName: "SharkFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData ShrimpFish = new FishData(
        prefabVariantName: "ShrimpFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData SpikeFish = new FishData(
        prefabVariantName: "SpikeFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData TinyFish = new FishData(
        prefabVariantName: "TinyFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData UglyFish = new FishData(
        prefabVariantName: "UglyFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData WhaleFish = new FishData(
        prefabVariantName: "WhaleFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
        );

    public static readonly FishData WhiteFish = new FishData(
        prefabVariantName: "WhiteFish",
        angerScore: 0,
        disgustScore: 0,
        fearScore: 0,
        happinessScore: 0,
        sadnessScore: 0,
        scale: new Vector3(1f, 1f, 1f),
        depth: 5f,
        depthRange: 10f
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