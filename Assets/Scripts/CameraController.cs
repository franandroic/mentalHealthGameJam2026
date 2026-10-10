using UnityEngine;

//Owns all camera logic.
//While plummeting or reeling the camera is attached to the hook (keeps the hook at the same
//screen position, never rises above its starting height). In every other state it stays put.
public class CameraController : MonoBehaviour
{
    Transform target;
    bool following;
    float offsetY;
    float startY;

    void Awake()
    {
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

    //Called by whoever spawns/destroys the hook.
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void OnStateChanged(GameStateType state)
    {
        following = state == GameStateType.InPlummet || state == GameStateType.InReel;

        // Remember where the hook sits relative to the camera at the moment we attach.
        if (following && target != null)
            offsetY = transform.position.y - target.position.y;
    }

    void LateUpdate()
    {
        // LateUpdate so the hook has already moved this frame.
        if (!following || target == null) return;

        Vector3 pos = transform.position;
        pos.y = Mathf.Min(startY, target.position.y + offsetY);
        transform.position = pos;
    }
}
