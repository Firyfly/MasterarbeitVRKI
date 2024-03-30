using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//-----------------------------------------
//Makes the Tutorial Object Disappear after 10 seconds
//-----------------------------------------

public class DisappearAfterFirstTalk : MonoBehaviour
{

    float timer = 10.0f;
    float interpolateRatio = 1.0f;

    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject header;
    [SerializeField]
    private GameObject top;
    [SerializeField]
    private GameObject outline;

    //Fade the Object
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            if(timer >= 0.0f)
            {
                timer -= Time.deltaTime * 1;
            }
            else
            {
                //Ending Fade is Slower
                if(interpolateRatio > 0.4)
                {
                    interpolateRatio -= Time.deltaTime * 0.2f;
                }
                //Beginning Fade is Quicker
                else
                {
                    interpolateRatio -= Time.deltaTime * 0.4f;
                }
                background.GetComponent<Image>().color = new Color(background.GetComponent<Image>().color.r, background.GetComponent<Image>().color.g, background.GetComponent<Image>().color.b, interpolateRatio);
                header.GetComponent<TMP_Text>().color = new Color(header.GetComponent<TMP_Text>().color.r, header.GetComponent<TMP_Text>().color.g, header.GetComponent<TMP_Text>().color.b, interpolateRatio);
                top.GetComponent<Image>().color = new Color(top.GetComponent<Image>().color.r, top.GetComponent<Image>().color.g, top.GetComponent<Image>().color.b, interpolateRatio);
                outline.GetComponent<Image>().color = new Color(outline.GetComponent<Image>().color.r, outline.GetComponent<Image>().color.g, outline.GetComponent<Image>().color.b, interpolateRatio);
            }
        }
    }
}
