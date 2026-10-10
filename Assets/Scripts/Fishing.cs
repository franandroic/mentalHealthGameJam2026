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

    [Header("Free motion")]
    public float freeMoveSpeed = 8;
    public float freeMoveInertia = 0.3f;

    [Header("Fish")]
    public float caughtDuration = 5;
    public Vector2 caughtFishOffset;

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
    Vector3 freeMoveVelocity;
    float caughtTimer;

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
            case GameStateType.InFreeMotion:
                freeMoveVelocity = Vector3.zero;
                break;
            case GameStateType.FishCaught:
                caughtTimer = 0;
                HookFish(GameState.Instance.CaughtFish);
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
            case GameStateType.InFreeMotion:
                UpdateFreeMotion();
                break;
            case GameStateType.FishCaught:
                UpdateFishCaught();
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

    void HookFish(GameObject fish)
    {
        FishBehaviour behaviour = fish.GetComponent<FishBehaviour>();
        if (behaviour != null) behaviour.enabled = false;   // its Update was moving the fish every frame

        Vector3 hookPos = hookInstance.transform.position;
        fish.transform.position = new Vector3(hookPos.x + caughtFishOffset.x, hookPos.y + caughtFishOffset.y, fish.transform.position.z);
        fish.transform.SetParent(hookInstance.transform, true);
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

    void UpdateFreeMotion()
    {
        Vector3 target = InputController.Instance.MouseWorldPosition;
        target = cameraController.ClampToView(target);
        target.y = Mathf.Min(target.y, WaterSurface.position.y);
        target.z = hookInstance.transform.position.z;

        hookInstance.transform.position = Vector3.SmoothDamp(
            hookInstance.transform.position, target, ref freeMoveVelocity, freeMoveInertia, freeMoveSpeed
            );
    }

    void UpdateFishCaught()
    {
        caughtTimer += Time.deltaTime;
        if (caughtTimer < caughtDuration) return;

        Destroy(GameState.Instance.CaughtFish);
        GameState.Instance.ClearCaughtFish();
        GameState.Instance.SetState(GameStateType.InFreeMotion);
    }

    void UpdateReel()
    {
        hookInstance.transform.position = Vector3.MoveTowards(hookInstance.transform.position, throwStart, reelSpeed * Time.deltaTime);

        if (hookInstance.transform.position == throwStart)
            GameState.Instance.SetState(GameStateType.AfterReel);
    }

    void UpdateAfterReel()
    {
        cameraController.SetTarget(null);
        Destroy(hookInstance);
        hookInstance = null;
        GameState.Instance.ClearCaughtFish();
        GameState.Instance.SetState(GameStateType.BeforeCast);
    }
}
