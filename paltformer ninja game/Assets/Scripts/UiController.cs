using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public void QuitGameButtonPrees() => Application.Quit();

    public void LoadScene(string levelName) => SceneManager.LoadScene(levelName);

    public void DeActivatePauseMenu(GameObject pauseMenu)
    {
        PlayerController.instance.isPause = false;
        Time.timeScale = 1;
    }
    
}
