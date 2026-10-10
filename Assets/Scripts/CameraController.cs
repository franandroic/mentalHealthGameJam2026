using UnityEngine;

//Owns all camera logic.
//InFreeMotion: Hold (camera stays where it is, the hook moves around the screen).
//Every other state: Follow (camera keeps its offset to the hook, never above its starting height).
public class CameraController : MonoBehaviour
{
    enum Mode { Hold, Follow }

    Transform target;
    Mode mode = Mode.Follow;
    float offsetY;
    float startY;
    Camera cam;

    public Vector2 ViewSize
    {
        get
        {
            float height = cam.orthographicSize * 2f;
            return new Vector2(height * cam.aspect, height);
        }
    }

    void Awake()
    {
        cam = GetComponent<Camera>();
        startY = transform.position.y;
    }

    void Start()
    {
        GameState.Instance.OnStateChanged += OnStateChanged;
    }

    void OnDestroy()
    {
        if (GameState.Instance != null)
            GameState.Instance.OnStateChanged -= OnStateChanged;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        CaptureOffset();
    }

    public Vector3 ClampToView(Vector3 point)
    {
        Vector2 half = ViewSize / 2f;
        Vector3 center = transform.position;
        point.x = Mathf.Clamp(point.x, center.x - half.x, center.x + half.x);
        point.y = Mathf.Clamp(point.y, center.y - half.y, center.y + half.y);
        return point;
    }

    void OnStateChanged(GameStateType state)
    {
        mode = state == GameStateType.InFreeMotion ? Mode.Hold : Mode.Follow;
        if (mode == Mode.Follow) CaptureOffset();
    }

    // Remember where the hook sits relative to the camera, so following doesn't make the view jump.
    void CaptureOffset()
    {
        if (target != null)
            offsetY = transform.position.y - target.position.y;
    }

    void LateUpdate()
    {
        // LateUpdate so the hook has already moved this frame.
        if (mode != Mode.Follow || target == null) return;

        Vector3 pos = transform.position;
        pos.y = Mathf.Min(startY, target.position.y + offsetY);
        transform.position = pos;
    }
}