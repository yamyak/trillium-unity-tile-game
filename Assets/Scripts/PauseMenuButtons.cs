using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MainMenuButtons
{
  public static bool isPaused;
  public GameObject pauseMenu;

  public void Start()
  {
    isPaused = false;
    pauseMenu.SetActive(false);
  }

  public void Update()
  {
    if(Input.GetKeyUp("escape"))
    {
      isPaused = !isPaused;
      Time.timeScale = isPaused ? 0 : 1;
      pauseMenu.SetActive(isPaused);
    }
  }

  public void Resume()
  {
    isPaused = false;
    pauseMenu.SetActive(false);
    Time.timeScale = 1;
  }
}
