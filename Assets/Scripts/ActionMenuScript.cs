using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActionMenuScript : MonoBehaviour
{
  private ActionStateManager actionManager;

  private GameObject actionMenu;
  private Text pieceName;

  private Dictionary<ActionButton, GameObject> actionButtons;

  public void ActivateButton(ActionButton button, bool active)
  {
    actionButtons[button].SetActive(active);
  }

  public void SetPieceName(string name)
  {
    pieceName.text = name;
  }

  public void SetButtonText(ActionButton button, string text)
  {
    actionButtons[button].transform.Find("Button").GetComponentInChildren<Text>().text = text;
  }

  public void SelectSlot1()
  {
    actionManager.ClickButton(ActionButton.ACTION1);
  }

  public void SelectSlot2()
  {
    actionManager.ClickButton(ActionButton.ACTION2);
  }

  public void SelectSlot3()
  {
    actionManager.ClickButton(ActionButton.ACTION3);
  }

  public void SelectSlot4()
  {
    actionManager.ClickButton(ActionButton.ACTION4);
  }

  public void Up()
  {
    actionManager.ClickButton(ActionButton.UP);
  }

  public void Down()
  {
    actionManager.ClickButton(ActionButton.DOWN);
  }

  public void Back()
  {
    actionManager.ClickButton(ActionButton.BACK);
  }

  public void Cancel()
  {
    actionManager.ClickButton(ActionButton.CANCEL);
  }

  public void OnEnterPlayingActionState()
  {
    actionMenu.SetActive(true);
  }

  public void OnExitPlayingActionState()
  {
    actionMenu.SetActive(false);
  }

  // Start is called before the first frame update
  void Start()
  {
    actionManager = ActionStateManager.GetInstance();
    actionManager.SetMenuScript(this);

    actionMenu = transform.Find("Action Menu").gameObject;
    pieceName = transform.Find("Action Menu/Piece Name").GetComponent<Text>();

    actionButtons = new Dictionary<ActionButton, GameObject>();
    actionButtons.Add(ActionButton.ACTION1, transform.Find("Action Menu/Action Panel/Slot 1 Button").gameObject);
    actionButtons.Add(ActionButton.ACTION2, transform.Find("Action Menu/Action Panel/Slot 2 Button").gameObject);
    actionButtons.Add(ActionButton.ACTION3, transform.Find("Action Menu/Action Panel/Slot 3 Button").gameObject);
    actionButtons.Add(ActionButton.ACTION4, transform.Find("Action Menu/Action Panel/Slot 4 Button").gameObject);
    actionButtons.Add(ActionButton.UP, transform.Find("Action Menu/Action Panel/Up Button").gameObject);
    actionButtons.Add(ActionButton.DOWN, transform.Find("Action Menu/Action Panel/Down Button").gameObject);
    actionButtons.Add(ActionButton.BACK, transform.Find("Action Menu/Action Panel/Back Button").gameObject);
    actionButtons.Add(ActionButton.CANCEL, transform.Find("Action Menu/Action Panel/Cancel Button").gameObject);

    StateManager.GetInstance().AddCallback(CallbackType.STATE_ENTER, GameState.PLAYING_ACTION, OnEnterPlayingActionState);
    StateManager.GetInstance().AddCallback(CallbackType.STATE_EXIT, GameState.PLAYING_ACTION, OnExitPlayingActionState);
  }
}
