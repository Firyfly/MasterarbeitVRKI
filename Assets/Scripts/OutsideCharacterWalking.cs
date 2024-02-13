using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideCharacterWalking : MonoBehaviour
{

    //[SerializeField]
    //private Animator animator;

    enum Direction{
        x,
        z,
    };

    [SerializeField]
    private Direction walkingDirection = Direction.x;

    [SerializeField]
    private int maxDistance = 5;
    [SerializeField]
    private float speed = 1;

    private float startingPos = 0;
    private bool walkingBack = false;

    // Start is called before the first frame update
    void Start()
    {
        //animator.SetBool("isWalking", true);

        if (walkingDirection == Direction.x)
        {
            startingPos = this.gameObject.transform.position.x;
        }
        else if (walkingDirection == Direction.z)
        {
            startingPos = this.gameObject.transform.position.z;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (walkingDirection == Direction.x)
        {
            WalkingX();
        }
        else if (walkingDirection == Direction.z)
        {
            WalkingZ();
        }
    }
    private void WalkingZ()
    {
        if (!walkingBack)
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, speed* Time.deltaTime));
            if(this.transform.position.z - startingPos >= maxDistance)
            {
                walkingBack = true;
                Quaternion newRotation = new Quaternion(0, 0, 0, 0);
                newRotation.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + 180, this.transform.rotation.eulerAngles.z);
                this.transform.rotation = newRotation;
            }
        }
        else
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            if (this.transform.position.z <= startingPos)
            {
                walkingBack = false;
                Quaternion newRotation = new Quaternion(0,0,0,0);
                newRotation.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y - 180, this.transform.rotation.eulerAngles.z);
                this.transform.rotation = newRotation;
            }
        }
    }

    private void WalkingX()
    {
        if (!walkingBack)
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            if (this.transform.position.x - startingPos >= maxDistance)
            {
                walkingBack = true;
                Quaternion newRotation = new Quaternion(0, 0, 0, 0);
                newRotation.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + 180, this.transform.rotation.eulerAngles.z);
                this.transform.rotation = newRotation;
            }
        }
        else
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            if (this.transform.position.x <= startingPos)
            {
                walkingBack = false;
                Quaternion newRotation = new Quaternion(0, 0, 0, 0);
                newRotation.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y - 180, this.transform.rotation.eulerAngles.z);
                this.transform.rotation = newRotation;
            }
        }
    }
}
