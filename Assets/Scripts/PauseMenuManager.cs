using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
  private bool isPaused;
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

  public void LoadLevel(string levelName)
  {
    SceneManager.LoadScene(levelName);
  }

  public void QuitGame()
  {
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #else
      Application.Quit();
    #endif
  }
}
