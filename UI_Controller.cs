using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void Play()
    {
        SceneController.LoadScene(1);
    }

    public void Restart()
    {
        SceneController.Restart();
    }

    public void NextLevel()
    {
        SceneController.NextLevel();
    }

    public void Exit()
    {
        Debug.Log("exit?");
        Application.Quit();
    }

    public void Menu()
    {
        Time.timeScale = 1.0f;
        SceneController.LoadScene(0);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    public void SceneLoad(int sceneIndex)
    {
        SceneController.LoadScene(sceneIndex);
    }

    
}
