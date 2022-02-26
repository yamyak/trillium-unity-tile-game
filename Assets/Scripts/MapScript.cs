using System.Collections.Generic;
using UnityEngine;

// map object script
public class MapScript : MonoBehaviour
{
  // prototpye tile object
  public GameObject tile;

  // prototype base piece object
  public GameObject basePiece;
  // more pieces need to be added here

  // 2 layer map grid
  // layer 0: tiles
  // layer 1: pieces
  private GameObject[,,] grid;
  private StateManager stateManager;

  // flag to deactivate inputs during rotations
  private bool activeInput = true;
  // degrees map has rotated and which direction
  private float completedDegrees, direction;

  // move a piece on the map
  public void MovePieceOnMap(int xOld, int yOld, int xNew, int yNew)
  {
    // update second layer 2d grid with piece coordinates
    grid[xOld, yOld, 1].GetComponent<PieceScript>().SetLocation(xNew, yNew);
    grid[xNew, yNew, 1] = grid[xOld, yOld, 1];
    grid[xOld, yOld, 1] = null;

    // update piece location in piece transform
    grid[xNew, yNew, 1].transform.position = Constants.CalculateLocation(xNew, yNew, grid[xNew, yNew, 1].transform.position.z);
  }

  // verify that coordinates provided do not exceed bounds of map
  public bool ValidLocation(int x, int y)
  {
    if(x < 0 || x >= MainMenuScript.mapLength || y < 0 || y >= MainMenuScript.mapLength)
    {
      return false;
    }

    return true;
  }

  // add a new piece to the map
  public GameObject AddPieceToMap(string pieceName, int x, int y, MapColor color)
  {
    GameObject piece = null;

    // check which type of piece is being added
    // switch statement and enums are better here
    if(pieceName == "Base")
    {
      // create the requested piece
      piece = Instantiate(basePiece, Constants.CalculateLocation(x, y, -0.3f), Quaternion.identity);
    }
    // more pieces to be added here

    if(piece != null)
    {
      // initialize the piece
      piece.GetComponent<PieceScript>().Intialize(this.gameObject, color, x, y);

      // add the piece to the map
      grid[x, y, 1] = piece;
    }

    return piece;
  }

  // get tile associated with coordinates
  // probably not needed
  public GameObject GetCell(int x, int y)
  {
    return grid[x, y, 0];
  }

  // Start is called before the first frame update
  void Start()
  {
    stateManager = StateManager.GetInstance();
    int side = MainMenuScript.mapLength;

    // initialize the grid
    grid = new GameObject[side, side, 2];

    // calculate midpoint of map and move map so bottom left edge is at origin
    float midPoint = (((float)side * Constants.tileLength) + ((float)(side - 1) * Constants.tileBuffer)) / 2;
    this.transform.position = new Vector3(midPoint, midPoint, 0);

    // iterate through map coordinates
    for(int i = 0; i < side; i++)
    {
      for(int j = 0; j < side; j++)
      {
        // create tile object and add to map
        GameObject obj = Instantiate(tile, Constants.CalculateLocation(i, j, 0.02f), Quaternion.identity);
        obj.transform.parent = this.transform;
        obj.GetComponent<TileScript>().SetLocation(i, j);
        obj.GetComponent<TileScript>().SetCellColor(MapColor.WHITE);
        grid[i, j, 0] = obj;
        grid[i, j, 1] = null;
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
    // if game not paused or over
    if (stateManager.GetState() != GameState.PAUSE && stateManager.GetState() != GameState.GAME_OVER)
    {
      // if not currently turning
      if (activeInput)
      {
        // if rotate clockwise key selected
        if (Input.GetKeyUp(KeyCode.E))
        {
          // initialize degrees rotated and direction
          completedDegrees = Time.deltaTime * Constants.mapRotationSpeed;
          direction = -1;
          // deactivate input
          activeInput = false;

          // start rotation
          this.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * Constants.mapRotationSpeed * direction)), Space.World);
        }
        // if rotate counter-clockwise key selected
        if (Input.GetKeyUp(KeyCode.Q))
        {
          // initialize degrees rotated and direction
          completedDegrees = Time.deltaTime * Constants.mapRotationSpeed;
          direction = 1;
          // deactivate input
          activeInput = false;

          // start rotation
          this.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * Constants.mapRotationSpeed * direction)), Space.World);
        }
      }
      else
      {
        // if rotation has not been completed
        if (completedDegrees < 90)
        {
          // continue rotating and update degrees rotated
          completedDegrees += (Time.deltaTime * Constants.mapRotationSpeed);
          this.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * Constants.mapRotationSpeed * direction)), Space.World);
        }
        else
        {
          // rotation completed, activate input
          activeInput = true;
        }
      }
    }
  }
}