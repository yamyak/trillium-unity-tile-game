using System.Collections.Generic;
using UnityEngine;

public class BasePieceScript : PieceScript
{
  // Start is called before the first frame update
  void Start()
  {
    stateManager = StateManager.GetInstance();

    options = new List<Option>();

    Option attack = new Option { optionName = "Attack", parentOption = null, callback = AttackCallback };
    Option move = new Option { optionName = "Move", parentOption = null, callback = MoveCallback };
    Option special = new Option { optionName = "Special", parentOption = null, callback = SpecialCallback };
    Option item = new Option { optionName = "Item", parentOption = null, callback = ItemCallback };
    Option heal = new Option { optionName = "Heal", parentOption = null, callback = HealCallback };

    options.Add(attack);
    options.Add(move);
    options.Add(special);
    options.Add(item);
    options.Add(heal);
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
