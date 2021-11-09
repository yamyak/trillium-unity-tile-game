using UnityEngine;
using UnityEngine.UI;

public class ActionMenuScript : MonoBehaviour
{
  private GameObject actionMenu;
  private Text pieceName;

  public void SelectSlot1()
  {

  }

  public void SelectSlot2()
  {

  }

  public void SelectSlot3()
  {

  }

  public void SelectSlot4()
  {

  }

  public void Up()
  {
    
  }

  public void Down()
  {

  }

  public void Back()
  {

  }

  public void Cancel()
  {
    StateManager.GetInstance().SetState(GameState.PLAYING_BASIC);
  }

  public void OnEnterPlayingActionState()
  {
    actionMenu.SetActive(true);
    pieceName.text = StateManager.GetInstance().GetCurrentPiece().GetComponent<PieceScript>().GetPieceName();
  }

  public void OnExitPlayingActionState()
  {
    actionMenu.SetActive(false);
  }

  // Start is called before the first frame update
  void Start()
  {
    actionMenu = transform.Find("Action Menu").gameObject;
    pieceName = transform.Find("Action Menu/Piece Name").GetComponent<Text>();

    StateManager.GetInstance().AddCallback(CallbackType.STATE_ENTER, GameState.PLAYING_ACTION, OnEnterPlayingActionState);
    StateManager.GetInstance().AddCallback(CallbackType.STATE_EXIT, GameState.PLAYING_ACTION, OnExitPlayingActionState);
  }
}
