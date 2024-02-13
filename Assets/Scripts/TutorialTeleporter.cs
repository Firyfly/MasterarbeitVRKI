using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTeleporter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TeleportBackToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void TeleportToHandtrackingTutorial()
    {
        SceneManager.LoadScene(5, LoadSceneMode.Single);
    }
    public void TeleportToControllerTutorial()
    {
        SceneManager.LoadScene(6, LoadSceneMode.Single);
    }

}
