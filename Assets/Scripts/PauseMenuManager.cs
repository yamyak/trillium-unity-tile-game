using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
  private static GameScript.SetPauseStateCallback playCallback;

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

  public void SetCallback(GameScript.SetPauseStateCallback callback)
  {
    playCallback = callback;
  }
}