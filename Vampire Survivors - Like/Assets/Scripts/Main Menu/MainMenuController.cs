using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private MainMenuAnimationsHandler animHandler;

    private void Start()
    {
        animHandler.FadeEnded += () => SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        Debug.Log("Start");
        animHandler.BeginStartRoutine();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
