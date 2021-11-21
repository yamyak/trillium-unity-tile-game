using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour
{
  private int x;
  private int y;
  private MapColor color;

  private StateManager stateManager;
  private ActionStateManager actionManager;

  private Constants.OptionTileCallback callback;

  // Start is called before the first frame update
  void Start()
  {
    stateManager = StateManager.GetInstance();
    actionManager = ActionStateManager.GetInstance();
  }

  public void Initialize(MapColor color, int x, int y, Constants.OptionTileCallback callback)
  {
    this.x = x;
    this.y = y;
    this.color = color;
    this.callback = callback;
  }

  void OnMouseEnter()
  {
    transform.GetComponent<SpriteRenderer>().color = Constants.EnumToCellColor(color);
  }

  void OnMouseExit()
  {
    transform.GetComponent<SpriteRenderer>().color = Constants.EnumToCellColor(MapColor.WHITE);
  }

  void OnMouseOver()
  {
    if (Input.GetMouseButtonDown(0) && stateManager.GetState() == GameState.PLAYING_ACTION)
    {
      callback(x, y);
    }
  }
}
