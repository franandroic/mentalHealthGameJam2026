using UnityEngine;
using UnityEngine.InputSystem;

public class Fishing : MonoBehaviour
{


    public enum FishingPhase
    {
        Throwing,
        Falling,
        StayingAtLevel,
        Caught,
        GoingBack
    }

    FishingPhase currentPhase;
    public float fallingSpeed = 1;
    public float stayingAtLevelSpeed = 3;
    public float caughtDuration = 5;
    public float reeloutSpeed = 5;
    public float throwDuration = 1;
    public float throwArcHeight = 2;
    [SerializeField] GameObject hook;

    [SerializeField] Transform WaterSurface;
    [SerializeField] Transform BoatPos;

    GameObject hookInstance;
    Camera cam;

    // kosi hitac
    bool isThrown;
    Vector3 throwStart;
    Vector3 throwTarget;
    float throwTimer;

    float caughtTimer;


    private void HandleState()
    {
        switch (currentPhase)
        {
            case FishingPhase.Throwing:
                HandleThrowing();
                break;
            case FishingPhase.Falling:
                HandleFalling();
                break;
            case FishingPhase.StayingAtLevel:
                HandleStayingAtLevel();
                break;
            case FishingPhase.Caught:
                HandleCaught();
                break;
            case FishingPhase.GoingBack:
                HandleGoingBack();
                break;
        }
    }

    private void HandleThrowing()
    {
        // ako je stisnut lijevi klik misa, instancirat gameobjekt hook i napraviti kosi hitac na nacin
        // da landa na poziciju koja je odredjena x kordinatom trenutne pozicije misa.x i y kordinatom WaterSutface.position.y
        // kada landa na povrsinu mora, prebacuje se u stanje Falling

        if (!isThrown)
        {
            if (!LeftClickPressed()) return;

            throwStart = BoatPos.position;
            throwTarget = new Vector3(MouseWorldPosition().x, WaterSurface.position.y, throwStart.z);
            throwTimer = 0;
            hookInstance = Instantiate(hook, throwStart, Quaternion.identity);
            isThrown = true;
            return;
        }

        throwTimer += Time.deltaTime;
        float t = Mathf.Clamp01(throwTimer / throwDuration);

        // jednolika brzina po x + parabola po y = kosi hitac koji tocno landa na throwTarget
        Vector3 position = Vector3.Lerp(throwStart, throwTarget, t);
        position.y += throwArcHeight * 4 * t * (1 - t);
        hookInstance.transform.position = position;

        if (t >= 1)
        {
            isThrown = false;
            currentPhase = FishingPhase.Falling;
            Debug.Log("Falling - press left click to stop");
        }
    }

    private void HandleFalling()
    {
        // hook pada jednolikom brzinom fallingSpeed sve dok se ponovno ne stisne lijevi klik misa, zatim vise ne pada i prebacuje se u stanje stayingAtLevel

        if (LeftClickPressed())
        {
            currentPhase = FishingPhase.StayingAtLevel;
            Debug.Log("Staying at level - press left click to catch");
            return;
        }

        hookInstance.transform.position += Vector3.down * fallingSpeed * Time.deltaTime;
    }

    private void HandleStayingAtLevel()
    {
        // hook se pomice za misem, lerpa brzinom stayingatlevelspeed sve dok se ne stisne lijevi klik misa, tada prelazi u stanje Caught

        if (LeftClickPressed())
        {
            caughtTimer = 0;
            currentPhase = FishingPhase.Caught;
            Debug.Log("Caught - press left click within " + caughtDuration + "s to reel out");
            return;
        }

        Vector3 target = MouseWorldPosition();
        target.y = Mathf.Min(target.y, WaterSurface.position.y); // hook ostaje ispod povrsine mora
        target.z = hookInstance.transform.position.z;

        hookInstance.transform.position = Vector3.Lerp(hookInstance.transform.position, target, stayingAtLevelSpeed * Time.deltaTime);
    }

    private void HandleCaught()
    {
        // kada je uhvacen, hook zapocinje brojati 5 sekundi te ako se u medjuvremenu nista ne dogodi vraca se u stanje handlestayingatLevel, medjutim,
        // ako igrac unutar tih 5 sekundi pritisne lijevi klik misa hook se krene pomicat prema boatu brzinom reeloutspeed te se vraca natrag u stanje throwing

        if (LeftClickPressed())
        {
            currentPhase = FishingPhase.GoingBack;
            Debug.Log("Reeling out");
            return;
        }

        caughtTimer += Time.deltaTime;
        if (caughtTimer >= caughtDuration)
        {
            currentPhase = FishingPhase.StayingAtLevel;
            Debug.Log("Got away - staying at level");
        }
    }

    private void HandleGoingBack()
    {
        // hook se pomice prema boatu brzinom reeloutSpeed, kada stigne unistava se i vraca se u stanje Throwing

        hookInstance.transform.position = Vector3.MoveTowards(hookInstance.transform.position, BoatPos.position, reeloutSpeed * Time.deltaTime);

        if (hookInstance.transform.position == BoatPos.position)
        {
            Destroy(hookInstance);
            hookInstance = null;
            currentPhase = FishingPhase.Throwing;
            Debug.Log("Press left click to throw");
        }
    }

    private bool LeftClickPressed()
    {
        return Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 screenPos = Mouse.current.position.ReadValue();
        screenPos.z = -cam.transform.position.z;
        return cam.ScreenToWorldPoint(screenPos);
    }

    private void Start()
    {
        cam = Camera.main;
        currentPhase = FishingPhase.Throwing;
        Debug.Log("Press left click to throw");
    }


    private void Update()
    {
        HandleState();
    }


}
