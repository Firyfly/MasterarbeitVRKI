using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthMover : MonoBehaviour
{

    private float timer = 0.3f;
    private bool back = false;

    [SerializeField]
    SupervisorAnimations supervisorAnimations;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (supervisorAnimations.isTalking)
        {


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
