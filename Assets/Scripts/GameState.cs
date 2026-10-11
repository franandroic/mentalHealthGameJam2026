using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameStateType
{
    BeforeCast,
    InCast,
    InPlummet,
    InFreeMotion,
    FishCaught,
    InReel,
    AfterReel
}

//Singleton that owns the current game state and the click-driven transitions.
//Transitions that depend on world events (hook hits water, hook reaches boat)
//are triggered by whoever detects them (Fishing) via SetState.
public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    public GameStateType Current { get; private set; } = GameStateType.BeforeCast;
    [SerializeField] TMP_Text clueText;
    [SerializeField] Image caughtFishImage1;
    [SerializeField] Image caughtFishImage2;

    //Stores the fish on the hook. Null if fish not caught.
    public GameObject CaughtFish { get; private set; }

    //The emotion of the latest clue. Null until the first clue has been shown (first AfterReel)
    public Emotion? CurrentClue { get; private set; }

    //Fired with the new state after every change.
    public event Action<GameStateType> OnStateChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        InputController.Instance.OnClick += HandleClick;
    }

    void OnDestroy()
    {
        if (InputController.Instance != null)
            InputController.Instance.OnClick -= HandleClick;
        if (Instance == this) Instance = null;
    }

    public void SetState(GameStateType newState)
    {
        if (newState == Current) return;
        Current = newState;

        if (newState == GameStateType.AfterReel) ShowRandomClue();

        OnStateChanged?.Invoke(newState);
    }

    void ShowRandomClue()
    {
        Emotion[] emotions = (Emotion[])Enum.GetValues(typeof(Emotion));
        Emotion picked = emotions[UnityEngine.Random.Range(0, emotions.Length)];

        CurrentClue = picked;
        if (clueText == null)
        {
            Debug.LogWarning("GameState: clueText is not assigned.", this);
            return;
        }
        clueText.text = TextRepository.Clues.TextFor(picked);
    }

    void ShowCaughtFishImage(GameObject fish)
    {
        if (caughtFishImage1 == null || caughtFishImage2 == null)
        {
            Debug.LogWarning("GameState: caughtFishImage is not assigned.", this);
            return;
        }

        SpriteRenderer fishRenderer = fish.GetComponentInChildren<SpriteRenderer>();
        if (fishRenderer == null)
        {
            Debug.LogWarning("GameState: caught fish has no SpriteRenderer.", fish);
            return;
        }

        caughtFishImage1.sprite = fishRenderer.sprite;
        caughtFishImage2.sprite = fishRenderer.sprite;
    }

    // Looks up the caught fish's score for the current clue and logs it.
    void LogCaughtFishScore(GameObject fish)
    {
        FishVariant variant = fish.GetComponentInParent<FishVariant>();
        if (variant == null)
        {
            Debug.LogWarning("GameState: caught fish has no FishVariant, can't score it.", fish);
            return;
        }

        if (!FishDatabase.TryGetByVariantName(variant.VariantName, out FishData data))
        {
            Debug.LogWarning("GameState: no FishDatabase entry for '" + variant.VariantName + "'.", fish);
            return;
        }

        if (CurrentClue == null)
        {
            Debug.Log("Caught " + variant.VariantName + ", but there is no clue yet so it isn't scored.");
            return;
        }

        Emotion clue = CurrentClue.Value;
        Debug.Log("Caught " + variant.VariantName + ": " + clue + " score = " + data.GetScore(clue));
    }

    public void CatchFish(GameObject fish)
    {
        if (Current != GameStateType.InFreeMotion || fish == null) return;
        CaughtFish = fish;
        LogCaughtFishScore(fish);
        ShowCaughtFishImage(fish);
        SetState(GameStateType.FishCaught);
    }

    public void ClearCaughtFish()
    {
        CaughtFish = null;
    }

    void HandleClick()
    {
        switch (Current)
        {
            case GameStateType.BeforeCast:
                SetState(GameStateType.InCast);
                break;
            case GameStateType.InPlummet:
                SetState(GameStateType.InFreeMotion);
                break;
            case GameStateType.InFreeMotion:
                //Fallthrough with caught fish at null
            case GameStateType.FishCaught:
                SetState(GameStateType.InReel);
                break;
            // InCast, InReel, AfterReel: clicks are ignored.
        }
    }
}
