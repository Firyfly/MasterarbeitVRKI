using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------
//Script not in use, was originally designed to create a clothingline in the not in the Gameloop included and discontinued Home scene
//-----------------------------------------

public class ClothingLine : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> ClothingGameobjects;
    private List<GameObject> InstantiatedObjects = new List<GameObject>();
    int clothingLinePosArticle = 0;

    // Start is called before the first frame update
    void Start()
    {

        if (ClothingGameobjects.Count != 0)
        {
            for (int i = 0; i < ClothingGameobjects.Count; i++)
            {
                GameObject temp = (GameObject)Instantiate(ClothingGameobjects[i], this.gameObject.transform);
                temp.gameObject.transform.position = this.gameObject.transform.position;
                InstantiatedObjects.Add(temp);
                InstantiatedObjects[i].transform.position = new Vector3(InstantiatedObjects[i].transform.position.x, InstantiatedObjects[i].transform.position.y, InstantiatedObjects[i].transform.position.z + i*1);

                if(InstantiatedObjects[i].transform.position.z > 2)
                {
                    InstantiatedObjects[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void SwitchRight()
    {

        if (clothingLinePosArticle < InstantiatedObjects.Count)
        {
            float timer = 0;
            clothingLinePosArticle += 1;

            for (int i = 0; i < InstantiatedObjects.Count; i++)
            { 
                Vector3 oldPosition = InstantiatedObjects[i].transform.position;
                Vector3 newPosition = InstantiatedObjects[i].transform.position;
                newPosition.z -= 1;

                while (timer <= 1)
                {
                    timer += 0.01f*Time.deltaTime;
                    InstantiatedObjects[i].transform.position = Vector3.Lerp(oldPosition,newPosition,timer);
                }

                if(i == clothingLinePosArticle - 1)
                {
                    InstantiatedObjects[i].gameObject.SetActive(true);
                }
                else if (i == clothingLinePosArticle + 1)
                {
                    InstantiatedObjects[i].gameObject.SetActive(true);
                }
                else if (i == clothingLinePosArticle )
                {
                    InstantiatedObjects[i].gameObject.SetActive(true);
                }
                else
                {
                    InstantiatedObjects[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void SwitchLeft()
    {
        if (clothingLinePosArticle != 0)
        {
            float timer = 0;
            clothingLinePosArticle -= 1;

            for (int i = 0; i < InstantiatedObjects.Count; i++)
            {
                Vector3 oldPosition = InstantiatedObjects[i].transform.position;
                Vector3 newPosition = InstantiatedObjects[i].transform.position;
                newPosition.z += 1;

                while (timer <= 1)
                {
                    timer += 0.01f*Time.deltaTime;
                    InstantiatedObjects[i].transform.position = Vector3.Lerp(oldPosition, newPosition, timer);
                }

                if (i == clothingLinePosArticle - 1)
                {
                    InstantiatedObjects[i].gameObject.SetActive(true);
                }
                else if (i == clothingLinePosArticle + 1)
                {
                    InstantiatedObjects[i].gameObject.SetActive(true);
                }
                else if (i == clothingLinePosArticle)
                {
                    InstantiatedObjects[i].gameObject.SetActive(true);
                }
                else
                {
                    InstantiatedObjects[i].gameObject.SetActive(false);
                }
            }
        }
    }

}
