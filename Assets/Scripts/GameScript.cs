using UnityEngine;

public class GameScript : MonoBehaviour
{
  private GameObject map;
  public GameObject pauseMenu;

  StateManager stateManager;

  private int currentPlayer;

  Player[] players;

  public delegate void SetPauseStateCallback();

  void SetPauseState()
  {
    if (stateManager.GetState() == GameState.PAUSE)
    {
      stateManager.RevertToLastState();
      Time.timeScale = 1;
      pauseMenu.SetActive(false);
    }
    else if (stateManager.GetState() != GameState.GAME_OVER)
    {
      stateManager.SetState(GameState.PAUSE);
      Time.timeScale = 0;
      pauseMenu.SetActive(true);
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    stateManager = StateManager.GetInstance();
    map = transform.Find("Map").gameObject;

    pauseMenu.GetComponent<PauseMenuManager>().SetCallback(SetPauseState);
    pauseMenu.SetActive(false);

    currentPlayer = 0;

    players = new Player[2];
    players[0] = new Player(map, MainMenuScript.playerColor[0], MainMenuScript.playerType[0], 0, 0);
    players[1] = new Player(map, MainMenuScript.playerColor[1], MainMenuScript.playerType[1], (MainMenuScript.mapLength - 1), (MainMenuScript.mapLength - 1));
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyUp("escape"))
    {
      SetPauseState();
    }

    if(stateManager.GetState() == GameState.READY)
    {
      stateManager.SetState(GameState.PLAYING);
      players[currentPlayer].ProcessMove();
    }
  }
}