using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------
//Quits the application
//-----------------------------------------

public class QuitApplication : MonoBehaviour
{
 
    //Quits the application
    public void QuitApplicationFunction()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
