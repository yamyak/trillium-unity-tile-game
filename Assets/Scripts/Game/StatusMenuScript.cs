using UnityEngine;
using UnityEngine.UI;

// script attached to the game status menu
public class StatusMenuScript : MonoBehaviour
{
  // player number text object
  private Text playerNumber;

  // callback function when entering basic play state
  public void OnEnterPlayingBasicState()
  {
    // update the player number text object
    int currentPlayer = StateManager.GetInstance().GetCurrentPlayer();
    playerNumber.text = currentPlayer.ToString();
  }

  // called when "End Turn button is selected
  public void EndTurn()
  {
    // cleans up action state manager
    ActionStateManager.GetInstance().CleanUp();
    // initiate transition to ready state
    StateManager.GetInstance().SetState(GameState.READY);
  }

  // Start is called before the first frame update
  void Start()
  {
    // retrieve player number text object
    playerNumber = transform.Find("Status Panel/Turn Number").GetComponent<Text>();

    // register the callback function for entering the basic play state
    StateManager.GetInstance().AddCallback(CallbackType.STATE_ENTER, GameState.PLAYING_BASIC, OnEnterPlayingBasicState);
  }
}
