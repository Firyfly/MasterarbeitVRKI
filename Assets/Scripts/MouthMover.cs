using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------
//Moves the mouth of the avatar when the ai is talking
//-----------------------------------------

public class MouthMover : MonoBehaviour
{

    private float timer = 0.3f;
    private bool back = false;
    public bool isTalking = false;

    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            //Moves the gameobject (used in the animation rigging, connected to the jaw) up and down
            if (timer >= 0.0f)
            {
                timer -= 1.0f * Time.deltaTime;
                if (back)
                {
                    this.gameObject.transform.Translate(new Vector3(0, -0.1f * Time.deltaTime, 0));
                }
                else
                {
                    this.gameObject.transform.Translate(new Vector3(0, 0.1f * Time.deltaTime, 0));
                }
            }
            else
            {
                if (back)
                {
                    back = false;
                }
                else
                {
                    back = true;
                }
                timer = 0.3f;
            }
        }
    }

}
