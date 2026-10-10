using System;
using UnityEngine;

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

    //Stores the fish on the hook. Null if fish not caught.
    public GameObject CaughtFish { get; private set; }

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
        OnStateChanged?.Invoke(newState);
    }

    public void CatchFish(GameObject fish)
    {
        if (Current != GameStateType.InFreeMotion || fish == null) return;
        CaughtFish = fish;
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
