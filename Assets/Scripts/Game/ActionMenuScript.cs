using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// script attached to action menu
public class ActionMenuScript : MonoBehaviour
{
  private ActionStateManager actionManager;

  // action menu object
  private GameObject actionMenu;
  // piece name object
  private Text pieceName;

  // map of buttons on menu
  private Dictionary<ActionButton, GameObject> actionButtons;

  // activate or deactivate a button in map
  public void ActivateButton(ActionButton button, bool active)
  {
    actionButtons[button].SetActive(active);
  }

  // set piece name
  public void SetPieceName(string name)
  {
    // update piece name object text
    pieceName.text = name;
  }

  // set button text in map
  public void SetButtonText(ActionButton button, string text)
  {
    actionButtons[button].transform.Find("Button").GetComponentInChildren<Text>().text = text;
  }

  // callback with slot 1 button selected
  public void SelectSlot1()
  {
    // informs manager to perform action associated with slot 1 button
    actionManager.ClickButton(ActionButton.ACTION1);
  }

  // callback with slot 2 button selected
  public void SelectSlot2()
  {
    // informs manager to perform action associated with slot 2 button
    actionManager.ClickButton(ActionButton.ACTION2);
  }

  // callback with slot 3 button selected
  public void SelectSlot3()
  {
    // informs manager to perform action associated with slot 3 button
    actionManager.ClickButton(ActionButton.ACTION3);
  }

  // callback with slot 4 button selected
  public void SelectSlot4()
  {
    // informs manager to perform action associated with slot 4 button
    actionManager.ClickButton(ActionButton.ACTION4);
  }

  // callback with up button selected
  public void Up()
  {
    // informs manager to perform action associated with up button
    actionManager.ClickButton(ActionButton.UP);
  }

  // callback with down button selected
  public void Down()
  {
    // informs manager to perform action associated with down button
    actionManager.ClickButton(ActionButton.DOWN);
  }

  // callback with back button selected
  public void Back()
  {
    // informs manager to perform action associated with back button
    actionManager.ClickButton(ActionButton.BACK);
  }

  // callback with cancel button selected
  public void Cancel()
  {
    // informs manager to perform action associated with cancel button
    actionManager.ClickButton(ActionButton.CANCEL);
  }

  // callback function for entering play action state
  public void OnEnterPlayingActionState()
  {
    // activate action menu
    actionMenu.SetActive(true);
  }

  // callback function for exiting play action state
  public void OnExitPlayingActionState()
  {
    // deactivate action menu
    actionMenu.SetActive(false);
  }

  // Start is called before the first frame update
  void Start()
  {
    actionManager = ActionStateManager.GetInstance();
    actionManager.SetMenuScript(this);

    // reference action menu and piece name objects
    actionMenu = transform.Find("Action Menu").gameObject;
    pieceName = transform.Find("Action Menu/Piece Name").GetComponent<Text>();

    // reference and add all buttons to button map
    actionButtons = new Dictionary<ActionButton, GameObject>();
    actionButtons.Add(ActionButton.ACTION1, transform.Find("Action Menu/Action Panel/Slot 1 Button").gameObject);
    actionButtons.Add(ActionButton.ACTION2, transform.Find("Action Menu/Action Panel/Slot 2 Button").gameObject);
    actionButtons.Add(ActionButton.ACTION3, transform.Find("Action Menu/Action Panel/Slot 3 Button").gameObject);
    actionButtons.Add(ActionButton.ACTION4, transform.Find("Action Menu/Action Panel/Slot 4 Button").gameObject);
    actionButtons.Add(ActionButton.UP, transform.Find("Action Menu/Action Panel/Up Button").gameObject);
    actionButtons.Add(ActionButton.DOWN, transform.Find("Action Menu/Action Panel/Down Button").gameObject);
    actionButtons.Add(ActionButton.BACK, transform.Find("Action Menu/Action Panel/Back Button").gameObject);
    actionButtons.Add(ActionButton.CANCEL, transform.Find("Action Menu/Action Panel/Cancel Button").gameObject);

    // register callbacks
    StateManager.GetInstance().AddCallback(CallbackType.STATE_ENTER, GameState.PLAYING_ACTION, OnEnterPlayingActionState);
    StateManager.GetInstance().AddCallback(CallbackType.STATE_EXIT, GameState.PLAYING_ACTION, OnExitPlayingActionState);
  }
}
