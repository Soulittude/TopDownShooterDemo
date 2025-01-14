using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private SceneController sceneController;
    public void Play()
    {
        sceneController.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
