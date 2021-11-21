using System.Collections.Generic;
using UnityEngine;

public class BasePieceScript : PieceScript
{
  Location[] attackCells;

  // Start is called before the first frame update
  void Start()
  {
    stateManager = StateManager.GetInstance();
    actionManager = ActionStateManager.GetInstance();

    baseOption = new Option();
    baseOption.parentOption = null;

    Option attack = new Option { optionName = "Attack", parentOption = baseOption, actionCallback = AttackMenuCallback };
    Option move = new Option { optionName = "Move", parentOption = baseOption, actionCallback = MoveCallback };
    Option special = new Option { optionName = "Special", parentOption = baseOption, actionCallback = SpecialCallback };
    Option item = new Option { optionName = "Item", parentOption = baseOption, actionCallback = ItemCallback };
    Option heal = new Option { optionName = "Heal", parentOption = baseOption, actionCallback = HealCallback };

    Option suboption1 = new Option { optionName = "Test 1", parentOption = item, actionCallback = MoveCallback };
    Option suboption2 = new Option { optionName = "Test 2", parentOption = item, actionCallback = MoveCallback };
    Option suboption3 = new Option { optionName = "Test 3", parentOption = item, actionCallback = MoveCallback };

    item.subOptions = new List<Option>();
    item.subOptions.Add(suboption1);
    item.subOptions.Add(suboption2);
    item.subOptions.Add(suboption3);

    baseOption.subOptions = new List<Option>();
    baseOption.subOptions.Add(attack);
    baseOption.subOptions.Add(move);
    baseOption.subOptions.Add(special);
    baseOption.subOptions.Add(item);
    baseOption.subOptions.Add(heal);

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

  public void AttackMenuCallback()
  {
    Debug.Log("Attack Callback");
    AddSelections(attackCells, AttackTileCallback);
  }

  public void AttackTileCallback(int x, int y)
  {
    actionManager.CleanUp();
    StateManager.GetInstance().SetState(GameState.READY);
  }

  public void MoveCallback()
  {
    Debug.Log("Move Callback");
  }

  public void SpecialCallback()
  {
    Debug.Log("Special Callback");
  }

  public void ItemCallback()
  {
    Debug.Log("Item Callback");
  }

  public void HealCallback()
  {
    Debug.Log("Heal Callback");
  }
}
