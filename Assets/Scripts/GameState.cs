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

    // Placeholder: until free motion is implemented, a click while plummeting goes straight to InReel.
    [SerializeField] bool skipFreeMotion = true;

    public GameStateType Current { get; private set; } = GameStateType.BeforeCast;

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
        Debug.Log("State -> " + newState);
        OnStateChanged?.Invoke(newState);
    }

    void HandleClick()
    {
        switch (Current)
        {
            case GameStateType.BeforeCast:
                SetState(GameStateType.InCast);
                break;
            case GameStateType.InPlummet:
                SetState(skipFreeMotion ? GameStateType.InReel : GameStateType.InFreeMotion);
                break;
            case GameStateType.InFreeMotion:
                // TODO: FishCaught will go here once fish exist.
                SetState(GameStateType.InReel);
                break;
            // InCast, FishCaught, InReel, AfterReel: clicks are ignored.
        }
    }
}
