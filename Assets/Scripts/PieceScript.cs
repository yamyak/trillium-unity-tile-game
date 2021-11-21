using UnityEngine;
using System.Collections.Generic;

public class PieceScript : MonoBehaviour
{
  protected int x;
  protected int y;
  protected MapColor color;

  protected StateManager stateManager;
  protected ActionStateManager actionManager;
  protected GameObject map;

  public string pieceName;

  protected Option baseOption;

  public GameObject selectionTile;
  protected List<GameObject> selections;

  // Start is called before the first frame update
  void Start()
  {
    stateManager = StateManager.GetInstance();
    actionManager = ActionStateManager.GetInstance();

    baseOption = new Option();
    baseOption.parentOption = null;
  }

  public void Intialize(GameObject map, MapColor color, int x, int y)
  {
    this.map = map;
    this.color = color;

    selections = new List<GameObject>();

    SetLocation(x, y);
    transform.Find("PieceTile/PieceHighlight").GetComponent<SpriteRenderer>().color = Constants.EnumToHighlightColor(color);
  }

  public void AddSelections(Location[] locs, Constants.OptionTileCallback callback)
  {
    foreach (Location loc in locs)
    {
      if (map.GetComponent<MapScript>().ValidLocation(x + loc.x, y + loc.y))
      {
        GameObject obj = Instantiate(selectionTile, Constants.CalculateLocation(x + loc.x, y + loc.y, 0.0f), Quaternion.identity);
        obj.transform.Find("Highlight").GetComponent<SpriteRenderer>().color = Constants.EnumToHighlightColor(color);
        obj.GetComponent<SelectionScript>().Initialize(color, x + loc.x, y + loc.y, callback);
        selections.Add(obj);
      }
    }
  }

  public void ClearSelections()
  {
    foreach(GameObject obj in selections)
    {
      Destroy(obj);
    }
    selections.Clear();
  }

  public void SetLocation(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  public void SetActive(bool flag)
  {
    this.GetComponent<BoxCollider>().enabled = flag;
    transform.Find("PieceTile").gameObject.SetActive(flag);
  }

  void OnMouseEnter()
  {
    transform.Find("PieceTile").GetComponent<SpriteRenderer>().color = Constants.EnumToCellColor(color);
  }

  void OnMouseExit()
  {
    transform.Find("PieceTile").GetComponent<SpriteRenderer>().color = Constants.EnumToCellColor(MapColor.WHITE);
  }

  void OnMouseOver()
  {
    if(Input.GetMouseButtonDown(0) && stateManager.GetState() == GameState.PLAYING_BASIC)
    {
      actionManager.SetCurrentPiece(this.gameObject);
      stateManager.SetState(GameState.PLAYING_ACTION);
    }
  }

  public Option GetOption()
  {
    return baseOption;
  }
}
