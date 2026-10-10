using System;
using UnityEngine;
using UnityEngine.InputSystem;

//Singleton that listens for input and exposes it.
public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    public event Action OnClick;

    Camera cam;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        cam = Camera.main;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            OnClick?.Invoke();
    }

    public Vector3 MouseWorldPosition
    {
        get
        {
            Vector3 screenPos = Mouse.current.position.ReadValue();
            screenPos.z = -cam.transform.position.z;
            return cam.ScreenToWorldPoint(screenPos);
        }
    }
}
