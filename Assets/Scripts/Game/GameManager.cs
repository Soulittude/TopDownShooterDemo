using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    private float timeToWaitBeforeExit;

    [SerializeField]
    private SceneController sceneController;
    
    public void OnPlayerDied()
    {
        Invoke(nameof(EndGame), timeToWaitBeforeExit);
    }

    private void EndGame()
    {
        sceneController.LoadScene("MainMenu");
    }
}
