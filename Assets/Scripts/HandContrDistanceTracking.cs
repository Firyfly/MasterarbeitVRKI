using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------
//Tracks the distance which the hands are moving
//-----------------------------------------

public class HandContrDistanceTracking : MonoBehaviour
{

    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private GameObject leftHand;
    Vector3 rightHandOldPosition;
    Vector3 leftHandOldPosition;

    [SerializeField]
    private GameObject StateManagerObject;
    public StateManager stateManager;
    private StateManager.Gamestate oldGamestate;

    List<float> RightPhaseInSecCollection = new List<float>();
    List<float> LeftPhaseInSecCollection = new List<float>();

    List<float> RightPhaseSecCollection = new List<float>();
    List<float> LeftPhaseSecCollection = new List<float>();

    public List<float> RightGeneralAverageCollection = new List<float>();
    public List<float> LeftGeneralAverageCollection = new List<float>();

    bool hasStarted = false;

    float timerforSec = 1.0f;

    private float calibrationDistance;

    // Start is called before the first frame update
    void Start()
    {
        stateManager = StateManagerObject.GetComponent<StateManager>();
        oldGamestate = stateManager.currentGamestate;
        rightHandOldPosition = rightHand.transform.position;
        leftHandOldPosition = leftHand.transform.position;

        calibrationDistance = PlayerPrefs.GetFloat("CalibrationDistance");
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            //Check if gamestate changed, if yes, start tracking
            if (stateManager.currentGamestate != oldGamestate)
            {
                //Should Always be the case
                hasStarted = true;
                oldGamestate = stateManager.currentGamestate;
            }
        }
        else if (hasStarted)
        {
            //each change of the state, safe the data in the big list of the generalaverage collection
            if (stateManager.currentGamestate != oldGamestate)
            {
                //Clear the sec list  and write in the general avrg
                timerforSec = 1.0f;
                if (RightPhaseInSecCollection.Count != 0) 
                {
                    RightPhaseInSecCollection.Clear();
                }

                if (RightPhaseInSecCollection.Count != 0)
                {
                    LeftPhaseInSecCollection.Clear();
                }

                float sumRight = 0.0f;
                for(int i = 0; i < RightPhaseSecCollection.Count; i++ ){
                    sumRight += RightPhaseSecCollection[i];
                }
                RightGeneralAverageCollection.Add(sumRight / RightPhaseSecCollection.Count);
                RightPhaseSecCollection.Clear();

                float sumLeft = 0.0f;
                for (int i = 0; i < LeftPhaseSecCollection.Count; i++)
                {
                    sumLeft += LeftPhaseSecCollection[i];
                }
                LeftGeneralAverageCollection.Add(sumLeft / LeftPhaseSecCollection.Count);
                LeftPhaseSecCollection.Clear();

                oldGamestate = stateManager.currentGamestate;
            }

            //Get the movement data in each second
            if(timerforSec >= 0.0f)
            {
                timerforSec -= Time.deltaTime * 1;
                float distanceRight = Vector3.Distance(rightHand.transform.position, rightHandOldPosition);
                rightHandOldPosition = rightHand.transform.position;
                RightPhaseInSecCollection.Add(distanceRight*100);

                float distanceLeft = Vector3.Distance(leftHand.transform.position, leftHandOldPosition);
                leftHandOldPosition = leftHand.transform.position;
                LeftPhaseInSecCollection.Add(distanceLeft*100);
            }
            //Get the Data after each second and compile into 1 value
            else
            {
                timerforSec = 1.0f;

                float sumRight = 0.0f;
                for(int i = 0; i < RightPhaseInSecCollection.Count; i++)
                {
                    sumRight += RightPhaseInSecCollection[i];
                }
                RightPhaseSecCollection.Add(sumRight / RightPhaseInSecCollection.Count);
                
                RightPhaseInSecCollection.Clear();

                float sumLeft = 0.0f;
                for (int i = 0; i < LeftPhaseInSecCollection.Count; i++)
                {
                    sumLeft += LeftPhaseInSecCollection[i];
                }
                LeftPhaseSecCollection.Add(sumLeft / LeftPhaseInSecCollection.Count);
               
                LeftPhaseInSecCollection.Clear();

            }
        }
    }

}
