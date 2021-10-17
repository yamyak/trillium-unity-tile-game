using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
  private static PlayManager.SetPauseStateCallback playCallback;

  public void Resume()
  {
    playCallback();
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

  public void SetCallback(PlayManager.SetPauseStateCallback callback)
  {
    playCallback = callback;
  }
}