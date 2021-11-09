using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
  private GameObject pauseMenu;

  public void Resume()
  {
    StateManager.GetInstance().RevertToLastState();
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

  public void OnEnterPauseState()
  {
    Time.timeScale = 0;
    pauseMenu.SetActive(true);
  }

  public void OnExitPauseState()
  {
    Time.timeScale = 1;
    pauseMenu.SetActive(false);
  }

  // Start is called before the first frame update
  void Start()
  {
    pauseMenu = transform.Find("Pause Menu").gameObject;
    pauseMenu.SetActive(false);

    StateManager.GetInstance().AddCallback(CallbackType.STATE_ENTER, GameState.PAUSE, OnEnterPauseState);
    StateManager.GetInstance().AddCallback(CallbackType.STATE_EXIT, GameState.PAUSE, OnExitPauseState);
  }
}