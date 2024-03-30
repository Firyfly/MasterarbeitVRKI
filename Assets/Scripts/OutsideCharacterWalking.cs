using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------
//Movement for the Avatars outside the office
//-----------------------------------------

public class OutsideCharacterWalking : MonoBehaviour
{

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
        //Sets the start position
        if (walkingDirection == Direction.x)
        {
            startingPos = this.gameObject.transform.position.x;
        }
        else if (walkingDirection == Direction.z)
        {
            startingPos = this.gameObject.transform.position.z;
        }
    }

    //Walks depending on the direction
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

    //Walking in the z direction
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

    //Walking in the x direction
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
