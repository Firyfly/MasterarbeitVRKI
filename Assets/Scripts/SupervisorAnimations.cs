using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------
//Animations for the supervisor, controlling the animator
//-----------------------------------------

public class SupervisorAnimations : MonoBehaviour
{

    enum SupervisorGender
    {
        male,
        female,
    }

    [SerializeField]
    private SupervisorGender supervisorGender = SupervisorGender.male;

    [SerializeField]
    private Animator animator;

    private float idleSittingTimer = 10.0f;
    private float talkingTimer = 1.0f;

    private bool armRubbing = false;

    public bool isTalking = false;

    private bool isPointingFinger = false;
    private bool isDisapproval = false;
    private bool isThumbsUp = false;
    private bool isClapping = false;

    [SerializeField]
    private GameObject mouthMoverObject;

    // Update is called once per frame
    void Update()
    {
        //if nothing else is done, count down timer and play idle animation
        mouthMoverObject.GetComponent<MouthMover>().isTalking = isTalking;
        if (!armRubbing && !isTalking && !isPointingFinger && !isDisapproval && !isThumbsUp && !isClapping)  //Default Idle Animations when nothing else is happening
        {
            if (idleSittingTimer > 0.0f)
            {
                idleSittingTimer -= Time.deltaTime * 1;
            }
            else
            {
                RandomIdleAnimation();
                idleSittingTimer = 10.0f;
            }
        }
        else if (armRubbing)    //Rare arm Rubbing animation vor diversity  //Activated interally in the default idle animations
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                animator.SetBool("IdleSitting2", false);
                animator.SetBool("IdleArmRub", false);
                armRubbing = false;
            }
        }
        else if (isTalking)     //Animations for when ChatGpt is talking    //is Activated externally by setting the isTalking bool needs to be stopped manually with StopTalking function
        {
            
            if(talkingTimer > 0.0f)
            {
                talkingTimer -= Time.deltaTime * 1;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && talkingTimer < 0.0f)
            {
                StartTalking();
                talkingTimer = 1.0f;
            }
        }
        else if (isPointingFinger)  //is activated externally by calling the SetPointingFinger Function, Ends automatically
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                isPointingFinger = false;
                animator.SetBool("PointingFinger", false);
                RandomIdleAnimation();
            }
        }
        else if (isDisapproval)  //is activated externally by calling the SetDisapproval Function, Ends automatically
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                isDisapproval = false;
                animator.SetBool("Disapproval", false);
                RandomIdleAnimation();
            }
        }
        else if (isThumbsUp)  //is activated externally by calling the SetThumbsUp Function, Ends automatically
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                isThumbsUp = false;
                animator.SetBool("ThumbsUp", false);
                RandomIdleAnimation();
            }
        }
        else if (isClapping)  //is activated externally by calling the SetClapping Function, Ends automatically
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                isClapping = false;
                animator.SetBool("Clapping", false);
                RandomIdleAnimation();
            }
        }
        else    //"Catch", if no animation is playing anymore and no bool is set, then automatically go to the default idle animations
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                RandomIdleAnimation();
            }
        }

    }

    //get random idle animation
    private void RandomIdleAnimation()
    {
        int rand = Random.Range(1,11);

        switch (rand)
        {
            case 1:

                animator.SetBool("IdleSitting2", false);
                animator.SetBool("IdleArmRub", true);
                armRubbing = true;
                break;

            case > 1 and < 7:

                animator.SetBool("IdleSitting2", false);
                animator.SetBool("IdleArmRub", false);
                break;

            case > 6 and < 12:

                animator.SetBool("IdleSitting2", true);
                animator.SetBool("IdleArmRub", false);
                break;

            default:

                animator.SetBool("IdleSitting2", false);
                animator.SetBool("IdleArmRub", false);
                break;


        }

    }

    //get random talking animation
    private void StartTalking()
    {
        int rand = Random.Range(1, 10);

        switch (rand)
        {
            case >= 1 and < 6:

                animator.SetBool("Talking1", false);
                animator.SetBool("Talking2", true);
                break;

            case > 5 and <= 10:

                animator.SetBool("Talking1", true);
                animator.SetBool("Talking2", false);
                break;

            default:

                animator.SetBool("Talking1", false);
                animator.SetBool("Talking2", true);
                break;


        }
    }

    public void StopTalking()
    {
        animator.SetBool("Talking1", false);
        animator.SetBool("Talking2", false);
        RandomIdleAnimation();
    }

    public void SetPointingFinger()
    {
        animator.SetBool("PointingFinger", true);
        isPointingFinger = true;
    }

    public void SetDisapproval()
    {
        animator.SetBool("Disapproval", true);
        isDisapproval = true;
    }

    public void SetThumbsUp()
    {
        animator.SetBool("ThumbsUp", true);
        isThumbsUp = true;
    }

    public void SetClapping()
    {
        animator.SetBool("Clapping", true);
        isClapping = true;
    }


}
