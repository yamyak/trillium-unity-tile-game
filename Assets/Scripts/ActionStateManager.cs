using UnityEngine;

public sealed class ActionStateManager
{
  private static ActionStateManager instance;

  private ActionMenuScript menu;
  private GameObject currentPiece;

  private int currentFrame;
  private int totalFrames;
  private Option currentOption;

  private Constants.OptionActionCallback[] actionCallbacks;

  private ActionStateManager()
  {
    actionCallbacks = new Constants.OptionActionCallback[Constants.actionMenuNumButtons];
  }

  public static ActionStateManager GetInstance()
  {
    if (instance == null)
    {
      instance = new ActionStateManager();
    }

    return instance;
  }

  private void UpdateCurrentFrame()
  {
    totalFrames = (int)(currentOption.subOptions.Count / Constants.actionMenuNumButtons) + 1;
    currentFrame = 0;
  }

  private bool StepIntoChildOption(int index)
  {
    currentOption = currentOption.subOptions[currentFrame * Constants.actionMenuNumButtons + index];
    if (currentOption.subOptions != null && currentOption.subOptions.Count > 0)
    {
      UpdateCurrentFrame();
      UpdateButtons();

      return true;
    }

    currentOption = currentOption.parentOption;

    return false;
  }

  private void UpdateSlotButtons()
  {
    int buttonIndex = 0;
    int frameStart = Constants.actionMenuNumButtons * currentFrame;
    for (int i = frameStart; (buttonIndex < Constants.actionMenuNumButtons) && (i < currentOption.subOptions.Count); i++, buttonIndex++)
    {
      menu.ActivateButton((ActionButton)buttonIndex, true);
      menu.SetButtonText((ActionButton)buttonIndex, currentOption.subOptions[i].optionName);
      actionCallbacks[buttonIndex] = currentOption.subOptions[i].actionCallback;
    }

    while (buttonIndex < Constants.actionMenuNumButtons)
    {
      menu.ActivateButton((ActionButton)buttonIndex, false);
      buttonIndex++;
    }
  }

  private void UpdateUpDownButtons()
  {
    menu.ActivateButton(ActionButton.DOWN, currentFrame + 1 != totalFrames);
    menu.ActivateButton(ActionButton.UP, currentFrame != 0);
  }

  private void UpdateBackButton()
  {
    menu.ActivateButton(ActionButton.BACK, currentOption.parentOption != null);
  }

  private void UpdateButtons()
  {
    UpdateSlotButtons();
    UpdateUpDownButtons();
    UpdateBackButton();
  }

  private void ClickActionButton(ActionButton buttonType)
  {
    currentPiece.GetComponent<PieceScript>().ClearSelections();

    if (!StepIntoChildOption((int)buttonType))
    {
      actionCallbacks[(int)buttonType]();
    }
  }

  private void ClickUpButton()
  {
    currentPiece.GetComponent<PieceScript>().ClearSelections();

    currentFrame--;

    UpdateButtons();
  }

  private void ClickDownButton()
  {
    currentPiece.GetComponent<PieceScript>().ClearSelections();

    currentFrame++;

    UpdateButtons();
  }

  private void ClickBackButton()
  {
    currentPiece.GetComponent<PieceScript>().ClearSelections();

    currentOption = currentOption.parentOption;

    UpdateCurrentFrame();
    UpdateButtons();
  }

  private void ClickCancelButton()
  {
    CleanUp();
  }

  public void CleanUp()
  {
    currentPiece.GetComponent<PieceScript>().ClearSelections();
    StateManager.GetInstance().SetState(GameState.PLAYING_BASIC);
  }

  public void ClickButton(ActionButton button)
  {
    currentPiece.GetComponent<PieceScript>().ClearSelections();

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

  public void SetCurrentPiece(GameObject piece)
  {
    currentPiece = piece;
    currentOption = piece.GetComponent<BasePieceScript>().GetOption();
    menu.SetPieceName(currentPiece.GetComponent<BasePieceScript>().pieceName);

    UpdateCurrentFrame();
    UpdateButtons();
  }

  public GameObject GetCurrentPiece()
  {
    return currentPiece;
  }

  public void SetMenuScript(ActionMenuScript menu)
  {
    this.menu = menu;
  }
}