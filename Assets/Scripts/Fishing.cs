using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializedField] GameObject hook;

    [SerializedField] Transform WaterSurface;
    [SerializedField] Transform BoatPos;


    private void HandleState()
    {
        //napisat funkcije za svaki od phaseova



    }

    private void HandleThrowing()
    {
        // ako je stisnut lijevi klik misa, instancirat gameobjekt hook i napraviti kosi hitac na nacin 
        // da landa na poziciju koja je odredjena x kordinatom trenutne pozicije misa.x i y kordinatom WaterSutface.position.y
        // kada landa na povrsinu mora, prebacuje se u stanje Falling

    }

    private void HandleFalling()
    {
        // hook pada jednolikom brzinom fallingSpeed sve dok se ponovno ne stisne lijevi klik misa, zatim vise ne pada i prebacuje se u stanje stayingAtLevel


    }

    private void HandleStayingAtLevel()
    {
        // hook se pomice za misem, lerpa brzinom stayingatlevelspeed sve dok se ne stisne lijevi klik misa, tada prelazi u stanje Caught
    }

    private void HandleCaught()
    {
        // kada je uhvacen, hook zapocinje brojati 5 sekundi te ako se u medjuvremenu nista ne dogodi vraca se u stanje handlestayingatLevel, medjutim,
        // ako igrac unutar tih 5 sekundi pritisne lijevi klik misa hook se krene pomicat prema boatu brzinom reeloutspeed te se vraca natrag u stanje throwing

    }
    private void Start()
    {
        currentPhase = FishingPhase.Throwing;
        Debug.Log("Press left click to throw");
    }


    private void Update()
    {





    }


}
