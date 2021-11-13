using System.Collections.Generic;
using UnityEngine;

public class BasePieceScript : PieceScript
{
  // Start is called before the first frame update
  void Start()
  {
    stateManager = StateManager.GetInstance();

    baseOption = new Option();
    baseOption.parentOption = null;


    Option attack = new Option { optionName = "Attack", parentOption = baseOption, callback = AttackCallback };
    Option move = new Option { optionName = "Move", parentOption = baseOption, callback = MoveCallback };
    Option special = new Option { optionName = "Special", parentOption = baseOption, callback = SpecialCallback };
    Option item = new Option { optionName = "Item", parentOption = baseOption, callback = ItemCallback };
    Option heal = new Option { optionName = "Heal", parentOption = baseOption, callback = HealCallback };

    Option suboption1 = new Option { optionName = "Test 1", parentOption = item, callback = AttackCallback };
    Option suboption2 = new Option { optionName = "Test 2", parentOption = item, callback = AttackCallback };
    Option suboption3 = new Option { optionName = "Test 3", parentOption = item, callback = AttackCallback };

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
  }

  public void AttackCallback()
  {
    Debug.Log("Attack Callback");
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
