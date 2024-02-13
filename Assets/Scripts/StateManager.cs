using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    public enum Gamestate
    {
        Empty,
        Preperation,
        ManagerTalking,
        ApplicantTalking,
        Ending
    }


    public Gamestate currentGamestate = Gamestate.Preperation;


    // Start is called before the first frame update
    void Start()
    {
        currentGamestate = Gamestate.Preperation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
