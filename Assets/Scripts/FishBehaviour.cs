using UnityEngine;

public class FishBehaviour : MonoBehaviour
{

    private Transform edgeRight;
    private Transform edgeLeft;

    private Transform edgeTop;
    private float distanceSideError = 0.2f;
    private float distanceTopError = 1f;
    public float distanceTargetError = 0.05f;

    // stanja: true od trenutka okretanja dok riba ne dodje do iduce target pozicije,
    // da se ne bi okretala svaki frame dok je jos uvijek blizu ruba
    private bool turnedFromEdge = false;
    private bool turnedFromTop = false;

    [SerializeField] GameObject Visuals;
    [SerializeField] GameObject CalmSwimPattern;
    [SerializeField] GameObject ChaoticSwimPattern;
    [SerializeField] GameObject CustomSwimPattern;
    [SerializeField] GameObject BottomSwimPattern;
    public enum SwimPatter
    {
        Calm,
        Chaotic,
        Custom
    }

    public SwimPatter swimPattern;
    public bool turnsAroundRandomly = false;
    public float speed = 3;

    private Vector3 destination;


    void Start()
    {
        edgeRight = GameObject.Find("EdgeRight").transform;
        edgeLeft = GameObject.Find("EdgeLeft").transform;
        edgeTop = GameObject.Find("WaterSurface").transform;
        Swim();
    }


    private void Swim()
    {
        // ne okrece se nasumicno ako se upravo okrenula od ruba, jer bi to ponistilo okret
        if (turnsAroundRandomly && !turnedFromEdge)
        {
            if (Random.Range(0, 2) == 0)
            {
                TurnAround();
            }
        }

        // na temelju zadanog swim patterna, odabire jedan od assignanih game objekata te nasumicno odabire jedno dijete od njega
        // i promatra njegovu poziciju u overvorldu i to mu je krajnja destinacija te se lerpa unutar updatea
        // u updateu provjerava da li je u bilo kojem trenutku udaljenost od bilo kojeg ruba <= distanceErrora, ako jest
        // prekida lerpanje u tom smjeru te se okrecepomocu turn around i bira novu destinaciju istog swim patterna

        Transform pattern = GetSwimPattern();
        if (pattern == null || pattern.childCount == 0)
        {
            Debug.LogWarning(name + ": swim pattern " + swimPattern + " nije postavljen ili nema djece");
            destination = transform.position;
            return;
        }

        int randomnum = Random.Range(0, pattern.childCount);
        Debug.Log(randomnum);
        Debug.Log(pattern.GetChild(randomnum).name);
        Vector2 pointPos = pattern.GetChild(randomnum).position;
        destination = pointPos;
    }

    private Transform GetSwimPattern()
    {
        GameObject pattern = null;
        switch (swimPattern)
        {
            case SwimPatter.Calm:
                pattern = CalmSwimPattern;
                break;
            case SwimPatter.Chaotic:
                pattern = ChaoticSwimPattern;
                break;
            case SwimPatter.Custom:
                pattern = CustomSwimPattern;
                break;
        }
        return pattern != null ? pattern.transform : null;
    }

    private bool IsNearEdge(Transform edge)
    {
        return Mathf.Abs(transform.position.x - edge.position.x) <= distanceSideError;
    }

    private bool IsNearTop()
    {
        // >= pokriva i slucaj kada je riba nekako izasla iznad povrsine
        return Mathf.Abs(transform.position.y - edgeTop.position.y) <= distanceTopError;
    }

    private void SwimToBottom()
    {
        Debug.Log("SwimToBottom");
        // riba je blizu povrsine -> nasumicno bira jednu od tocaka BottomSwimPatterna i pliva prema njoj
        if (BottomSwimPattern == null || BottomSwimPattern.transform.childCount == 0)
        {
            Debug.LogWarning(name + ": BottomSwimPattern nije postavljen ili nema djece");
            Swim();
            return;
        }

        Transform bottomPoints = BottomSwimPattern.transform;
        Vector2 pointPos = bottomPoints.GetChild(Random.Range(0, bottomPoints.childCount)).position;
        destination = pointPos;
    }

    void TurnAround()
    {
        Vector3 scale = Visuals.transform.localScale;
        scale.x *= -1;
        Visuals.transform.localScale = scale;
    }

    void Update()
    {
        bool movingRight = destination.x > transform.position.x;
        bool movingLeft = destination.x < transform.position.x;

        // riba je dosla blizu povrsine i jos uvijek pliva prema gore -> vraca se prema dnu
        if (!turnedFromTop && IsNearTop())
        {
            turnedFromTop = true;
            SwimToBottom();
            return;
        }

        // riba je dosla do ruba i jos uvijek pliva prema njemu -> okrece se i bira novu destinaciju
        if (!turnedFromEdge && ((movingRight && IsNearEdge(edgeRight)) || (movingLeft && IsNearEdge(edgeLeft))))
        {
            turnedFromEdge = true;
            TurnAround();

            // ako je riba na putu prema dnu, ostaje na tom putu (nova tocka dna), inace bi Swim() pregazio destinaciju
            if (turnedFromTop)
                SwimToBottom();
            else
                Swim();
            return;
        }

        // pomice se samo po x i y, z ostaje cijelo vrijeme isti
        Vector2 newPos = Vector2.Lerp(transform.position, destination, speed * Time.deltaTime);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

        // dosla je do target pozicije -> otkacuju se stanja i bira se nova destinacija
        if (Vector2.Distance(transform.position, destination) <= distanceTargetError)
        {

            turnedFromEdge = false;
            turnedFromTop = false;

            Swim();
        }
    }
}
