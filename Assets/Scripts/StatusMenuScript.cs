using UnityEngine;
using UnityEngine.UI;

public class StatusMenuScript : MonoBehaviour
{
  private Text playerNumber;

  public void OnEnterPlayingBasicState()
  {
    int currentPlayer = StateManager.GetInstance().GetCurrentPlayer();
    playerNumber.text = currentPlayer.ToString();
  }

  public void EndTurn()
  {
    StateManager.GetInstance().SetState(GameState.READY);
  }

  // Start is called before the first frame update
  void Start()
  {
    playerNumber = transform.Find("Status Panel/Turn Number").GetComponent<Text>();

    StateManager.GetInstance().AddCallback(CallbackType.STATE_ENTER, GameState.PLAYING_BASIC, OnEnterPlayingBasicState);
  }
}
