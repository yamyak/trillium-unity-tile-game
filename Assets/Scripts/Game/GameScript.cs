using System;
using UnityEngine;
using UnityEngine.UI;

// overall game script
public class GameScript : MonoBehaviour
{
  private StateManager stateManager;
  private GameObject map;

  // list of players
  private Player[] players;

  // callback function used when entering basic play state
  public void OnEnterPlayingBasicState()
  {
    // get the current player and activate them
    int currentPlayer = stateManager.GetCurrentPlayer();
    players[currentPlayer - 1].ProcessMove();
  }

  // callback function used when entering game ready state
  public void OnEnterReadyState()
  {
    // switch to next player
    stateManager.SwitchToNextPlayer();
    // initiate switch to basic play state
    stateManager.SetState(GameState.PLAYING_BASIC);
  }

  // Start is called before the first frame update
  void Start()
  {
    // initialize the managers
    stateManager = StateManager.GetInstance();
    ActionStateManager.GetInstance();
    // get the game map
    map = transform.Find("Map").gameObject;

    

    // create the players
    players = new Player[2];
    MapColor color1 = (MapColor)Enum.Parse(typeof(MapColor), PlayerPrefs.GetString("color1"), true);
    players[0] = new Player(map, color1, PlayerPrefs.GetString("player1"), 0, 0);
    MapColor color2 = (MapColor)Enum.Parse(typeof(MapColor), PlayerPrefs.GetString("color2"), true);
    players[1] = new Player(map, color2, PlayerPrefs.GetString("player2"), (PlayerPrefs.GetInt("size") - 1), (PlayerPrefs.GetInt("size") - 1));

    // register the callbacks
    stateManager.AddCallback(CallbackType.STATE_ENTER, GameState.PLAYING_BASIC, OnEnterPlayingBasicState);
    stateManager.AddCallback(CallbackType.STATE_ENTER, GameState.READY, OnEnterReadyState);

    // initiate switch to basic play state
    stateManager.SetState(GameState.PLAYING_BASIC);
  }

  // Update is called once per frame
  void Update()
  {
    // if escape key selected
    if (Input.GetKeyUp("escape"))
    {
      if(stateManager.GetState() != GameState.PAUSE)
      {
        // if not currently paused, set game state to paused
        stateManager.SetState(GameState.PAUSE);
      }
      else
      {
        // if currently paused, revert to previous state
        stateManager.RevertToLastState();
      }
    }
  }
}