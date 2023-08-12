using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
