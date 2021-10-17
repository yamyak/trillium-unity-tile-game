using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
  public GameObject tile;

  public GameObject basePiece;

  private GameObject[,,] grid;
  StateManager stateManager;

  private bool activeInput = true;
  private float completedDegrees, direction;

  private Vector3 CalculateLocation(int xCell, int yCell, float zLoc)
  {
    return new Vector3((Constants.tileLength / 2) + (Constants.tileLength + Constants.tileBuffer) * xCell,
        (Constants.tileLength / 2) + (Constants.tileLength + Constants.tileBuffer) * yCell, zLoc);
  }

  public void MovePieceOnMap(int xOld, int yOld, int xNew, int yNew)
  {
    grid[xOld, yOld, 1].GetComponent<PieceManager>().SetLocation(xNew, yNew);
    grid[xNew, yNew, 1] = grid[xOld, yOld, 1];
    grid[xOld, yOld, 1] = null;

    grid[xNew, yNew, 1].transform.position = CalculateLocation(xNew, yNew, grid[xNew, yNew, 1].transform.position.z);
  }

  public GameObject AddPieceToMap(string pieceName, int x, int y, MapColor color, bool active)
  {
    GameObject piece = null;

    if(pieceName == "Base")
    {
      piece = Instantiate(basePiece, CalculateLocation(x, y, -0.3f), Quaternion.identity);
    }

    if(piece != null)
    {
      piece.GetComponent<PieceManager>().Intialize(active, this.gameObject, color);

      grid[x, y, 1] = piece;
      grid[x, y, 1].GetComponent<PieceManager>().SetLocation(x, y);
    }

    return piece;
  }

  public void SetCellColor(int x, int y, MapColor color)
  {
    grid[x, y, 0].GetComponent<Cell>().SetCellColor(color);
  }

  // Start is called before the first frame update
  void Start()
  {
    stateManager = StateManager.GetInstance();
    int side = MainMenuManager.mapLength;

    grid = new GameObject[side, side, 2];

    float midPoint = (((float)side * Constants.tileLength) + ((float)(side - 1) * Constants.tileBuffer)) / 2;
    this.transform.position = new Vector3(midPoint, midPoint, 0);

    for(int i = 0; i < side; i++)
    {
      for(int j = 0; j < side; j++)
      {
        GameObject obj = Instantiate(tile, CalculateLocation(i, j, 0), Quaternion.identity);
        obj.transform.parent = this.transform;
        obj.GetComponent<Cell>().SetCellColor(MapColor.WHITE);
        grid[i, j, 0] = obj;
        grid[i, j, 1] = null;
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (stateManager.GetState() != GameState.PAUSE && stateManager.GetState() != GameState.GAME_OVER)
    {
      if (activeInput)
      {
        if (Input.GetKeyUp(KeyCode.E))
        {
          completedDegrees = Time.deltaTime * Constants.mapRotationSpeed;
          direction = -1;
          activeInput = false;

          this.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * Constants.mapRotationSpeed * direction)), Space.World);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
          completedDegrees = Time.deltaTime * Constants.mapRotationSpeed;
          direction = 1;
          activeInput = false;

          this.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * Constants.mapRotationSpeed * direction)), Space.World);
        }
      }
      else
      {
        if (completedDegrees < 90)
        {
          completedDegrees += (Time.deltaTime * Constants.mapRotationSpeed);
          this.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * Constants.mapRotationSpeed * direction)), Space.World);
        }
        else
        {
          activeInput = true;
        }
      }
    }
  }
}