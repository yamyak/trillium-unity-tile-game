using UnityEngine;
using System.Collections.Generic;

public class PieceScript : MonoBehaviour
{
  private int x;
  private int y;
  private MapColor color;

  private bool active;

  protected StateManager stateManager;
  private GameObject map;

  public string pieceName;

  protected Option baseOption;

  // Start is called before the first frame update
  void Start()
  {
    stateManager = StateManager.GetInstance();

    baseOption = new Option();
    baseOption.parentOption = null;
  }

  public void Intialize(bool active, GameObject map, MapColor color)
  {
    this.map = map;
    this.color = color;
  }

  public void SetLocation(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  public void SetActive(bool flag)
  {
    active = flag;
    this.GetComponent<BoxCollider>().enabled = flag;
    map.GetComponent<MapScript>().SetHighlightColor(x, y, color);
    map.GetComponent<MapScript>().ActivateHighlight(x, y, flag);
  }

  void OnMouseEnter()
  {
    map.GetComponent<MapScript>().SetCellColor(x, y, color);
  }

  void OnMouseExit()
  {
    map.GetComponent<MapScript>().SetCellColor(x, y, MapColor.WHITE);
  }

  void OnMouseOver()
  {
    if(Input.GetMouseButtonDown(0) && stateManager.GetState() == GameState.PLAYING_BASIC)
    {
      stateManager.SetCurrentPiece(this.gameObject);
      stateManager.SetState(GameState.PLAYING_ACTION);
    }
  }

  public Option GetOption()
  {
    return baseOption;
  }
}
