using Unity.VisualScripting;
using UnityEngine;

public class HookBehaviour : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("COLLIDED");

        if (GameState.Instance.Current != GameStateType.InFreeMotion) return;

        FishBehaviour fish = other.GetComponentInParent<FishBehaviour>();
        if (fish == null) return;

        GameState.Instance.CatchFish(fish.gameObject);
    }
}
