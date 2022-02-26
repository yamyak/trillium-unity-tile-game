using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script attached to selection tile 
public class SelectionScript : MonoBehaviour
{
  // tile location and color
  private int x;
  private int y;
  private MapColor color;

  private StateManager stateManager;

  private Constants.OptionTileCallback callback;

  // Start is called before the first frame update
  void Start()
  {
    // get reference to state manager
    stateManager = StateManager.GetInstance();
  }

  // initialize the selection tile color and callback
  public void Initialize(MapColor color, int x, int y, Constants.OptionTileCallback callback)
  {
    this.x = x;
    this.y = y;
    this.color = color;
    this.callback = callback;
  }

  // called when mouse enters selection tile
  void OnMouseEnter()
  {
    // changed tile color to player color
    transform.GetComponent<SpriteRenderer>().color = Constants.EnumToCellColor(color);
  }

  // called when mouse exits selection tile
  void OnMouseExit()
  {
    // changed tile color to white
    transform.GetComponent<SpriteRenderer>().color = Constants.EnumToCellColor(MapColor.WHITE);
  }

  // called while mouse hovers over selection tile
  void OnMouseOver()
  {
    // if selection tile selected and currently in play action state
    if (Input.GetMouseButtonDown(0) && stateManager.GetState() == GameState.PLAYING_ACTION)
    {
      // call selection callback
      callback(x, y);
    }
  }
}
