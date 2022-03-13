using UnityEngine;
using System.Collections.Generic;

// base script for pieces
public class PieceScript : MonoBehaviour
{
  // piece location and color
  protected int x;
  protected int y;
  protected MapColor color;

  // managers and map
  protected StateManager stateManager;
  protected ActionStateManager actionManager;
  protected GameObject map;

  // piece name
  public string pieceName;

  // base option associated with action menu
  protected Option baseOption;

  // selection tile prototype object
  public GameObject selectionTile;
  // list of selection tiles
  protected List<GameObject> selections;

  // Start is called before the first frame update
  void Start()
  {
    // get references to managers
    stateManager = StateManager.GetInstance();
    actionManager = ActionStateManager.GetInstance();

    // initialize base option
    baseOption = new Option();
    baseOption.parentOption = null;
  }

  public void Intialize(GameObject map, MapColor color, int x, int y)
  {
    // set the map and piece color
    this.map = map;
    this.color = color;

    // initialize selection list
    selections = new List<GameObject>();

    // set piece location
    SetLocation(x, y);
    // set piece highlight color
    transform.Find("PieceTile/PieceHighlight").GetComponent<SpriteRenderer>().color = Constants.EnumToHighlightColor(color);
  }

  // add selection tiles to map
  public void AddSelections(Location[] locs, Constants.OptionTileCallback callback)
  {
    // iterate through all relative coordinates
    foreach (Location loc in locs)
    {
      // check if coordinates are on map
      if (map.GetComponent<MapScript>().ValidLocation(x + loc.x, y + loc.y))
      {
        // create a selection tile and add to list
        GameObject obj = Instantiate(selectionTile, Constants.CalculateLocation(x + loc.x, y + loc.y, 0.0f), Quaternion.identity);
        obj.transform.Find("Highlight").GetComponent<SpriteRenderer>().color = Constants.EnumToHighlightColor(color);
        obj.GetComponent<SelectionScript>().Initialize(color, x + loc.x, y + loc.y, callback);
        selections.Add(obj);
      }
    }
  }

  // delete all selection tiles
  public void ClearSelections()
  {
    // iterate through selection list and destroy objects
    foreach(GameObject obj in selections)
    {
      Destroy(obj);
    }
    selections.Clear();
  }

  // set piece location
  public void SetLocation(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  // set piece active/inactive for piece selection/highlighting
  public void SetActive(bool flag)
  {
    // deactivate or activate piece collider
    this.GetComponent<BoxCollider>().enabled = flag;
    // deactivate or activate piece tile
    transform.Find("PieceTile").gameObject.SetActive(flag);
  }

  // called when mouse enters piece
  void OnMouseEnter()
  {
    // change piece tile color to player color
    transform.Find("PieceTile").GetComponent<SpriteRenderer>().color = Constants.EnumToCellColor(color);
  }

  // called when mouse exits piece
  void OnMouseExit()
  {
    // change piece tile color to white
    transform.Find("PieceTile").GetComponent<SpriteRenderer>().color = Constants.EnumToCellColor(MapColor.WHITE);
  }

  // called when mouse hovering over piece
  void OnMouseOver()
  {
    // if piece selected and currently in basic play state
    if(Input.GetMouseButtonDown(0) && stateManager.GetState() == GameState.PLAYING_BASIC)
    {
      // set the current piece in the action manager
      actionManager.SetCurrentPiece(this.gameObject);
      // initiate transition to play action state
      stateManager.SetState(GameState.PLAYING_ACTION);
    }
  }

  // get the base option associated with this piece
  public Option GetOption()
  {
    return baseOption;
  }
}
