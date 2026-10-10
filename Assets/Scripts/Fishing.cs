using UnityEngine;

//Spawns the hook and moves it according to the current GameState.
//Reports world events (hook hit the water, hook is back) to GameState.
public class Fishing : MonoBehaviour
{
    [Header("Cast")]
    public float throwDuration = 1;
    public float throwArcHeight = 2;

    [Header("Movement")]
    public float fallingSpeed = 1;
    public float reelSpeed = 5;

    [Header("References")]
    [SerializeField] GameObject hook;
    [SerializeField] Transform WaterSurface;
    [SerializeField] Transform BoatPos;
    [SerializeField] CameraController cameraController;
    [SerializeField] FishSpawner fishSpawner;

    GameObject hookInstance;
    Vector3 throwStart;
    Vector3 throwTarget;
    float throwTimer;
    float filledBottomY;

    void Start()
    {
        ResetFilledBottomY();
        GameState.Instance.OnStateChanged += OnStateChanged;
    }

    void OnDestroy()
    {
        if (GameState.Instance != null)
            GameState.Instance.OnStateChanged -= OnStateChanged;
    }

    // One-time setup when entering a state
    void OnStateChanged(GameStateType state)
    {
        switch (GameState.Instance.Current)
        {
            case GameStateType.BeforeCast:
                fishSpawner.ClearFish();
                ResetFilledBottomY();
                break;
            case GameStateType.InCast:
                StartCast();
                break;
        }
    }

    // Per-frame work while in a state
    void Update()
    {
        switch (GameState.Instance.Current)
        {
            case GameStateType.InCast:
                UpdateCast();
                break;
            case GameStateType.InPlummet:
                UpdatePlummet();
                break;
            case GameStateType.InReel:
                UpdateReel();
                break;
            case GameStateType.AfterReel:
                UpdateAfterReel();
                break;
        }
    }

    void StartCast()
    {
        throwStart = BoatPos.position;
        Vector3 mouse = InputController.Instance.MouseWorldPosition;
        throwTarget = new Vector3(mouse.x, WaterSurface.position.y, throwStart.z);
        throwTimer = 0;

        hookInstance = Instantiate(hook, throwStart, Quaternion.identity);
        cameraController.SetTarget(hookInstance.transform);
    }

    void ResetFilledBottomY()
    {
        filledBottomY = WaterSurface.position.y;
    }

    void UpdateCast()
    {
        throwTimer += Time.deltaTime;
        float t = Mathf.Clamp01(throwTimer / throwDuration);

        // linear in x + parabola in y = projectile arc that lands exactly on throwTarget
        Vector3 position = Vector3.Lerp(throwStart, throwTarget, t);
        position.y += throwArcHeight * 4 * t * (1 - t);
        hookInstance.transform.position = position;

        if (t >= 1)
            GameState.Instance.SetState(GameStateType.InPlummet);
    }

    void UpdatePlummet()
    {
        hookInstance.transform.position += Vector3.down * fallingSpeed * Time.deltaTime;
        EnsureFishAhead();
    }

    void EnsureFishAhead()
    {
        Vector2 view = cameraController.ViewSize;
        if (view.y <= 0) return;

        Vector3 camPos = cameraController.transform.position;
        float camBottom = camPos.y - view.y / 2f;

        while (filledBottomY > camBottom - view.y)
        {
            Vector3 center = new Vector3(camPos.x, filledBottomY - view.y / 2f, throwStart.z);
            fishSpawner.SpawnFish(center, view);
            filledBottomY -= view.y;
        }
    }

    void UpdateReel()
    {
        hookInstance.transform.position = Vector3.MoveTowards(hookInstance.transform.position, throwStart, reelSpeed * Time.deltaTime);

        if (hookInstance.transform.position == throwStart)
            GameState.Instance.SetState(GameStateType.AfterReel);
    }

    void UpdateAfterReel()
    {
        // Placeholder: clean up and allow another cast so the loop is testable.
        cameraController.SetTarget(null);
        Destroy(hookInstance);
        hookInstance = null;
        GameState.Instance.SetState(GameStateType.BeforeCast);
    }
}
