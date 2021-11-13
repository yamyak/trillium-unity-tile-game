using UnityEngine;
using UnityEngine.UI;

public class ActionMenuScript : MonoBehaviour
{
  private GameObject actionMenu;
  
  private GameObject currentPiece;
  private Text pieceName;

  private GameObject[] slotButtons;
  private GameObject upButton;
  private GameObject downButton;
  private GameObject backButton;

  private Constants.OptionActionCallback[] slotCallbacks;

  private int currentFrame;
  private int totalFrames;
  private Option currentOption;

  public void SelectSlot1()
  {
    if(!StepIntoChildOption(0))
    {
      slotCallbacks[0]();
    }
  }

  public void SelectSlot2()
  {

    if (!StepIntoChildOption(1))
    {
      slotCallbacks[1]();
    }
  }

  public void SelectSlot3()
  {
    if (!StepIntoChildOption(2))
    {
      slotCallbacks[2]();
    }
  }

  public void SelectSlot4()
  {
    if (!StepIntoChildOption(3))
    {
      slotCallbacks[3]();
    }
  }

  private void UpdateUpDownButtons()
  {
    if (currentFrame + 1 == totalFrames)
    {
      downButton.SetActive(false);
    }
    else
    {
      downButton.SetActive(true);
    }

    if (currentFrame == 0)
    {
      upButton.SetActive(false);
    }
    else
    {
      upButton.SetActive(true);
    }
  }

  private void UpdateSlotButtons()
  {
    int buttonIndex = 0;
    int frameStart = Constants.actionMenuNumButtons * currentFrame;
    for (int i = frameStart; (buttonIndex < Constants.actionMenuNumButtons) && (i < currentOption.subOptions.Count); i++, buttonIndex++)
    {
      slotButtons[buttonIndex].SetActive(true);
      slotButtons[buttonIndex].transform.Find("Button").GetComponentInChildren<Text>().text = currentOption.subOptions[i].optionName;
      slotCallbacks[buttonIndex] = currentOption.subOptions[i].callback;
    }

    while(buttonIndex < Constants.actionMenuNumButtons)
    {
      slotButtons[buttonIndex].SetActive(false);
      buttonIndex++;
    }
  }

  private void UpdateBackButton()
  {
    if(currentOption.parentOption == null)
    {
      backButton.SetActive(false);
    }
    else
    {
      backButton.SetActive(true);
    }
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
      UpdateUpDownButtons();
      UpdateSlotButtons();
      UpdateBackButton();

      return true;
    }

    currentOption = currentOption.parentOption;

    return false;
  }

  public void Up()
  {
    currentFrame--;
    UpdateUpDownButtons();
    UpdateSlotButtons();
  }

  public void Down()
  {
    currentFrame++;
    UpdateUpDownButtons();
    UpdateSlotButtons();
  }

  public void Back()
  {
    currentOption = currentOption.parentOption;

    UpdateCurrentFrame();
    UpdateUpDownButtons();
    UpdateSlotButtons();
    UpdateBackButton();
  }

  public void Cancel()
  {
    StateManager.GetInstance().SetState(GameState.PLAYING_BASIC);
  }

  public void OnEnterPlayingActionState()
  {
    actionMenu.SetActive(true);

    currentPiece = StateManager.GetInstance().GetCurrentPiece();
    pieceName.text = currentPiece.GetComponent<BasePieceScript>().pieceName;

    currentOption = currentPiece.GetComponent<BasePieceScript>().GetOption();

    UpdateCurrentFrame();
    UpdateUpDownButtons();
    UpdateSlotButtons();
    UpdateBackButton();
  }

  public void OnExitPlayingActionState()
  {
    pieceName.text = currentPiece.GetComponent<BasePieceScript>().pieceName;
    actionMenu.SetActive(false);
    currentPiece = null;
  }

  // Start is called before the first frame update
  void Start()
  {
    actionMenu = transform.Find("Action Menu").gameObject;
    pieceName = transform.Find("Action Menu/Piece Name").GetComponent<Text>();

    slotCallbacks = new Constants.OptionActionCallback[4];
    slotButtons = new GameObject[4];
    slotButtons[0] = transform.Find("Action Menu/Action Panel/Slot 1 Button").gameObject;
    slotButtons[1] = transform.Find("Action Menu/Action Panel/Slot 2 Button").gameObject;
    slotButtons[2] = transform.Find("Action Menu/Action Panel/Slot 3 Button").gameObject;
    slotButtons[3] = transform.Find("Action Menu/Action Panel/Slot 4 Button").gameObject;
    upButton = transform.Find("Action Menu/Action Panel/Up Button").gameObject;
    downButton = transform.Find("Action Menu/Action Panel/Down Button").gameObject;
    backButton = transform.Find("Action Menu/Action Panel/Back Button").gameObject;

    StateManager.GetInstance().AddCallback(CallbackType.STATE_ENTER, GameState.PLAYING_ACTION, OnEnterPlayingActionState);
    StateManager.GetInstance().AddCallback(CallbackType.STATE_EXIT, GameState.PLAYING_ACTION, OnExitPlayingActionState);

    currentOption = null;
  }
}
