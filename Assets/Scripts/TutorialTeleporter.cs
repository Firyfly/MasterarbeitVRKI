using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//-----------------------------------------
//Teleports to the tutorials and back
//-----------------------------------------

public class TutorialTeleporter : MonoBehaviour
{
  
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
