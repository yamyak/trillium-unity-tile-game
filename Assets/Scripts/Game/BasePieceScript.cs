using System.Collections.Generic;
using UnityEngine;

// script attached to base piece
public class BasePieceScript : PieceScript
{
  // locations piece can attack
  Location[] attackCells;

  // Start is called before the first frame update
  void Start()
  {
    // get managers
    stateManager = StateManager.GetInstance();
    actionManager = ActionStateManager.GetInstance();

    // base option associated with action menu for this piece
    baseOption = new Option();
    baseOption.parentOption = null;

    // some dummy options
    Option attack = new Option { optionName = "Attack", parentOption = baseOption, actionCallback = AttackMenuCallback };
    Option move = new Option { optionName = "Move", parentOption = baseOption, actionCallback = MoveCallback };
    Option special = new Option { optionName = "Special", parentOption = baseOption, actionCallback = SpecialCallback };
    Option item = new Option { optionName = "Item", parentOption = baseOption, actionCallback = ItemCallback };
    Option heal = new Option { optionName = "Heal", parentOption = baseOption, actionCallback = HealCallback };

    // some dummy sub options
    Option suboption1 = new Option { optionName = "Test 1", parentOption = item, actionCallback = MoveCallback };
    Option suboption2 = new Option { optionName = "Test 2", parentOption = item, actionCallback = MoveCallback };
    Option suboption3 = new Option { optionName = "Test 3", parentOption = item, actionCallback = MoveCallback };

    // add sub options to an option
    item.subOptions = new List<Option>();
    item.subOptions.Add(suboption1);
    item.subOptions.Add(suboption2);
    item.subOptions.Add(suboption3);
    
    // add options to base option
    baseOption.subOptions = new List<Option>();
    baseOption.subOptions.Add(attack);
    baseOption.subOptions.Add(move);
    baseOption.subOptions.Add(special);
    baseOption.subOptions.Add(item);
    baseOption.subOptions.Add(heal);

    // add target coordinates that base piece can attack
    // relative coordinates based on current piece location
    attackCells = new Location[8];
    attackCells[0] = new Location { x = 0, y = 1 };
    attackCells[1] = new Location { x = 0, y = -1 };
    attackCells[2] = new Location { x = 1, y = 0 };
    attackCells[3] = new Location { x = -1, y = 0 };
    attackCells[4] = new Location { x = -1, y = -1 };
    attackCells[5] = new Location { x = 1, y = 1 };
    attackCells[6] = new Location { x = -1, y = 1 };
    attackCells[7] = new Location { x = 1, y = -1 };
  }

  // callback for attack option
  public void AttackMenuCallback()
  {
    Debug.Log("Attack Callback");
    // populate selection tiles in location where piece can attack with callback used when selection tile selected
    AddSelections(attackCells, AttackTileCallback);
  }

  // callback used when selection tile selected
  public void AttackTileCallback(int x, int y)
  {
    // clean up all selection tiles in manager
    actionManager.CleanUp();
    // player turn over, switch to ready state
    StateManager.GetInstance().SetState(GameState.READY);
  }

  // dummy move callback
  public void MoveCallback()
  {
    Debug.Log("Move Callback");
  }

  // dummy special move callback
  public void SpecialCallback()
  {
    Debug.Log("Special Callback");
  }

  // dummy item callback
  public void ItemCallback()
  {
    Debug.Log("Item Callback");
  }

  // dummy heal callback
  public void HealCallback()
  {
    Debug.Log("Heal Callback");
  }
}
