using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{

    public enum SwitchSceneSelection
    {
        MainMenu,   //0
        Home,       //1
        Office,     //2
        Feedback,   //3
        Calibration //4
    }

    [SerializeField]
    private SwitchSceneSelection SwitchScenesSelected = SwitchSceneSelection.MainMenu;


    public void ChangeScene()
    {
        switch(SwitchScenesSelected)
        {
            case SwitchSceneSelection.MainMenu:
                SceneManager.LoadScene(0, LoadSceneMode.Single);
                break;

            case SwitchSceneSelection.Home:
                SceneManager.LoadScene(1, LoadSceneMode.Single);
                break;

            case SwitchSceneSelection.Office:
                SceneManager.LoadScene(2, LoadSceneMode.Single);
                break;

            case SwitchSceneSelection.Feedback:
                SceneManager.LoadScene(3, LoadSceneMode.Single);
                break;

            case SwitchSceneSelection.Calibration:
                SceneManager.LoadScene(4, LoadSceneMode.Single);
                break;
        }
    }
}
