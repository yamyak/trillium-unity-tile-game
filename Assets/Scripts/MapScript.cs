using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
  public GameObject tile;

  public GameObject basePiece;

  private GameObject[,,] grid;
  private StateManager stateManager;

  private bool activeInput = true;
  private float completedDegrees, direction;

  public void MovePieceOnMap(int xOld, int yOld, int xNew, int yNew)
  {
    grid[xOld, yOld, 1].GetComponent<PieceScript>().SetLocation(xNew, yNew);
    grid[xNew, yNew, 1] = grid[xOld, yOld, 1];
    grid[xOld, yOld, 1] = null;

    grid[xNew, yNew, 1].transform.position = Constants.CalculateLocation(xNew, yNew, grid[xNew, yNew, 1].transform.position.z);
  }

  public bool ValidLocation(int x, int y)
  {
    if(x < 0 || x >= MainMenuScript.mapLength || y < 0 || y >= MainMenuScript.mapLength)
    {
      return false;
    }

    return true;
  }

  public GameObject AddPieceToMap(string pieceName, int x, int y, MapColor color)
  {
    GameObject piece = null;

    if(pieceName == "Base")
    {
      piece = Instantiate(basePiece, Constants.CalculateLocation(x, y, -0.3f), Quaternion.identity);
    }

    if(piece != null)
    {
      piece.GetComponent<PieceScript>().Intialize(this.gameObject, color, x, y);

      grid[x, y, 1] = piece;
    }

    return piece;
  }

  public GameObject GetCell(int x, int y)
  {
    return grid[x, y, 0];
  }

  // Start is called before the first frame update
  void Start()
  {
    stateManager = StateManager.GetInstance();
    int side = MainMenuScript.mapLength;

    grid = new GameObject[side, side, 2];

    float midPoint = (((float)side * Constants.tileLength) + ((float)(side - 1) * Constants.tileBuffer)) / 2;
    this.transform.position = new Vector3(midPoint, midPoint, 0);

    for(int i = 0; i < side; i++)
    {
      for(int j = 0; j < side; j++)
      {
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