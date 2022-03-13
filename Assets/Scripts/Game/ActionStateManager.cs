using UnityEngine;

// action state manager singleton
// handle buttons and their associated callbacks in the action menu
public sealed class ActionStateManager
{
  // singleton instance
  private static ActionStateManager instance;

  // reference to action menu
  private ActionMenuScript menu;
  // reference to currently selected piece
  private GameObject currentPiece;

  // action menu has a 4 button capacity
  // options are split in 4 option frames
  // current frame is the current set of 4 options
  private int currentFrame;
  // total number of frames of options available currently
  private int totalFrames;
  // current parent option to action menu options
  private Option currentOption;

  // list of callbacks associated with option buttons
  private Constants.OptionActionCallback[] actionCallbacks;

  // constructor
  private ActionStateManager()
  {
    // initiate callback list
    actionCallbacks = new Constants.OptionActionCallback[Constants.actionMenuNumButtons];
  }

  // singleton accessor function
  public static ActionStateManager GetInstance()
  {
    if (instance == null)
    {
      instance = new ActionStateManager();
    }

    return instance;
  }

  
  // called when current option has changed
  // updates the total number of frame based on current option
  private void UpdateCurrentFrame()
  {
    // current option has changed so update the total number of frames availabe for current option
    totalFrames = (int)(currentOption.subOptions.Count / Constants.actionMenuNumButtons) + 1;
    // reset current frame to 0
    currentFrame = 0;
  }

  // option selected so update action menu based on that option
  private bool StepIntoChildOption(int index)
  {
    // set the new current option
    currentOption = currentOption.subOptions[currentFrame * Constants.actionMenuNumButtons + index];
    // if current option has suboptions
    if (currentOption.subOptions != null && currentOption.subOptions.Count > 0)
    {
      // update the frame counts
      UpdateCurrentFrame();
      // update the buttons in menu
      UpdateButtons();

      return true;
    }

    // no suboptions so jump back to parent option
    currentOption = currentOption.parentOption;

    return false;
  }

  // update all 4 slot button names and callbacks
  private void UpdateSlotButtons()
  {
    // iterate through current frame of options
    int buttonIndex = 0;
    int frameStart = Constants.actionMenuNumButtons * currentFrame;
    for (int i = frameStart; (buttonIndex < Constants.actionMenuNumButtons) && (i < currentOption.subOptions.Count); i++, buttonIndex++)
    {
      // activate the button and set text and callbacks
      menu.ActivateButton((ActionButton)buttonIndex, true);
      menu.SetButtonText((ActionButton)buttonIndex, currentOption.subOptions[i].optionName);
      actionCallbacks[buttonIndex] = currentOption.subOptions[i].actionCallback;
    }

    // if less than 4 options in frame, deactivate rest of buttons
    while (buttonIndex < Constants.actionMenuNumButtons)
    {
      menu.ActivateButton((ActionButton)buttonIndex, false);
      buttonIndex++;
    }
  }

  // activate/deactivate the up and down buttons based on current and total frames
  private void UpdateUpDownButtons()
  {
    // if not at end of total frames, activate down button
    menu.ActivateButton(ActionButton.DOWN, currentFrame + 1 != totalFrames);
    // if not first frame, activate up button
    menu.ActivateButton(ActionButton.UP, currentFrame != 0);
  }

  // activate/deactivate back button
  private void UpdateBackButton()
  {
    // activate back button if current option has a parent
    menu.ActivateButton(ActionButton.BACK, currentOption.parentOption != null);
  }

  // update all buttons
  private void UpdateButtons()
  {
    UpdateSlotButtons();
    UpdateUpDownButtons();
    UpdateBackButton();
  }

  // when 1 of 4 action buttons are selected
  // should not be called with up, down, back, or cancel
  private void ClickActionButton(ActionButton buttonType)
  {
    // clear out any selections on map from previous options
    currentPiece.GetComponent<PieceScript>().ClearSelections();

    // check if option has sub options
    // if so, handled in SteIntoChildOption function
    if (!StepIntoChildOption((int)buttonType))
    {
      // if not, call option callback associated with button
      actionCallbacks[(int)buttonType]();
    }
  }

  // when up button selected
  private void ClickUpButton()
  {
    // clear out any selections on map from previous options
    currentPiece.GetComponent<PieceScript>().ClearSelections();

    // go to previous frame
    currentFrame--;

    // update buttons for new frame
    UpdateButtons();
  }

  // when down button selected
  private void ClickDownButton()
  {
    // clear out any selections on map from previous options
    currentPiece.GetComponent<PieceScript>().ClearSelections();

    // go to next frame
    currentFrame++;

    // update buttons for new frame
    UpdateButtons();
  }

  // when back button selected
  private void ClickBackButton()
  {
    // clear out any selections on map from previous options
    currentPiece.GetComponent<PieceScript>().ClearSelections();

    // go to parent option
    currentOption = currentOption.parentOption;

    // update frame and button for new parent option
    UpdateCurrentFrame();
    UpdateButtons();
  }

  // when cancel button selected
  private void ClickCancelButton()
  {
    // clean up and close action menu
    CleanUp();
  }

  // cleans up action menu
  public void CleanUp()
  {
    // clear out any selections on map from previous options
    currentPiece.GetComponent<PieceScript>().ClearSelections();
    // initiate transition to basic play state
    StateManager.GetInstance().SetState(GameState.PLAYING_BASIC);
  }

  // called when any button on action menu selected
  public void ClickButton(ActionButton button)
  {
    // clear out any selections on map from previous options
    currentPiece.GetComponent<PieceScript>().ClearSelections();

    // perform appropriate action based on button selected
    switch (button)
    {
      case ActionButton.ACTION1:
      case ActionButton.ACTION2:
      case ActionButton.ACTION3:
      case ActionButton.ACTION4:
        ClickActionButton(button);
        break;
      case ActionButton.UP:
        ClickUpButton();
        break;
      case ActionButton.DOWN:
        ClickDownButton();
        break;
      case ActionButton.BACK:
        ClickBackButton();
        break;
      case ActionButton.CANCEL:
        ClickCancelButton();
        break;
    }
  }

  // set the currently active piece
  public void SetCurrentPiece(GameObject piece)
  {
    // set piece
    currentPiece = piece;
    // set current options
    currentOption = piece.GetComponent<BasePieceScript>().GetOption();
    // set piece name in menu
    menu.SetPieceName(currentPiece.GetComponent<BasePieceScript>().pieceName);

    // update frame values and buttons
    UpdateCurrentFrame();
    UpdateButtons();
  }

  // get the currently active piece
  public GameObject GetCurrentPiece()
  {
    return currentPiece;
  }

  // set the action menu object in the manager
  public void SetMenuScript(ActionMenuScript menu)
  {
    this.menu = menu;
  }
}