using UnityEngine;

public enum GameState
{
  READY,
  PLAYING,
  PAUSE,
  GAME_OVER
}

public class PlayManager : MonoBehaviour
{
  public GameObject pauseMenu;

  public GameObject basicPiece;

  private GameState currentState;
  private GameState savedState;
  private int currentPlayer;

  Player[] players;

  public delegate void SetPauseStateCallback(GameState state);

  void SetPauseState(GameState state)
  {
    if (currentState == GameState.PAUSE)
    {
      currentState = savedState;
      Time.timeScale = 1;
      pauseMenu.SetActive(false);
    }
    else if (currentState != GameState.GAME_OVER)
    {
      savedState = currentState;
      currentState = GameState.PAUSE;
      Time.timeScale = 0;
      pauseMenu.SetActive(true);
    }
  }

  public GameState GetState()
  {
    return currentState;
  }

  // Start is called before the first frame update
  void Start()
  {
    currentState = GameState.READY;
    currentPlayer = 0;

    players = new Player[2];
    for(int i = 0; i < 2; i++)
    {
      players[i] = new Player(MainMenuManager.playerColor[i], MainMenuManager.playerType[i]);
    }

    pauseMenu.GetComponent<PauseMenuManager>().SetCallback(SetPauseState);
    pauseMenu.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyUp("escape"))
    {
      SetPauseState(currentState);
    }

    if(currentState == GameState.READY)
    {
      currentState = GameState.PLAYING;
      players[currentPlayer].ProcessMove();
    }
  }
}