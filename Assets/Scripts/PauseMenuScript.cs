using UnityEngine;
using UnityEngine.SceneManagement;

// pause menu script
public class PauseMenuScript : MonoBehaviour
{
  // pause menu object
  private GameObject pauseMenu;

  // called when "Resume Game" button selected
  public void Resume()
  {
    // reverts game state to previous state
    StateManager.GetInstance().RevertToLastState();
  }

  // called when "Main Menu" button selected
  public void LoadLevel(string levelName)
  {
    // switch game scenes (to main menu)
    SceneManager.LoadScene(levelName);
  }

  // called when "Quit Game" button selected
  public void QuitGame()
  {
    // closes the game
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #else
      Application.Quit();
    #endif
  }

  // callback function when pause state entered
  public void OnEnterPauseState()
  {
    // freezes time
    Time.timeScale = 0;
    // activates pause menu object
    pauseMenu.SetActive(true);
  }

  // callback function when pause state exited
  public void OnExitPauseState()
  {
    // restart time
    Time.timeScale = 1;
    // deactivates pause menu object
    pauseMenu.SetActive(false);
  }

  // Start is called before the first frame update
  void Start()
  {
    // retrieve pause menu object and deactivate
    pauseMenu = transform.Find("Pause Menu").gameObject;
    pauseMenu.SetActive(false);

    // register callbacks
    StateManager.GetInstance().AddCallback(CallbackType.STATE_ENTER, GameState.PAUSE, OnEnterPauseState);
    StateManager.GetInstance().AddCallback(CallbackType.STATE_EXIT, GameState.PAUSE, OnExitPauseState);
  }
}