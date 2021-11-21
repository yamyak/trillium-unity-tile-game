using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
  private StateManager stateManager;
  private GameObject map;

  private Player[] players;

  public void OnEnterPlayingBasicState()
  {
    int currentPlayer = stateManager.GetCurrentPlayer();
    players[currentPlayer - 1].ProcessMove();
  }

  public void OnEnterReadyState()
  {
    stateManager.SwitchToNextPlayer();
    stateManager.SetState(GameState.PLAYING_BASIC);
  }

  // Start is called before the first frame update
  void Start()
  {
    stateManager = StateManager.GetInstance();
    ActionStateManager.GetInstance();
    map = transform.Find("Map").gameObject;

    players = new Player[2];
    players[0] = new Player(map, MainMenuScript.playerColor[0], MainMenuScript.playerType[0], 0, 0);
    players[1] = new Player(map, MainMenuScript.playerColor[1], MainMenuScript.playerType[1], (MainMenuScript.mapLength - 1), (MainMenuScript.mapLength - 1));

    
    stateManager.AddCallback(CallbackType.STATE_ENTER, GameState.PLAYING_BASIC, OnEnterPlayingBasicState);
    stateManager.AddCallback(CallbackType.STATE_ENTER, GameState.READY, OnEnterReadyState);

    stateManager.SetState(GameState.PLAYING_BASIC);
  }

  // Update is called once per frame
  void Update()
  {
    GameState currentState = stateManager.GetState();

    if (Input.GetKeyUp("escape"))
    {
      if(currentState != GameState.PAUSE)
      {
        stateManager.SetState(GameState.PAUSE);
      }
      else
      {
        stateManager.RevertToLastState();
      }
    }
  }
}