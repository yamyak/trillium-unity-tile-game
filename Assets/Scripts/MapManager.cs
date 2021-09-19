using UnityEngine;

public class MapManager : MonoBehaviour
{
  public GameObject tile;
  public int side = 100;
  public float tileLength = 1.0f;
  public float tileBuffer = 0.25f;

  private GameObject[,] grid;
  public GameObject highlight;

  public float speed = 10f;
  private bool activeInput = true;
  private float completedDegrees, direction;

  // Start is called before the first frame update
  void Start()
  {
    grid = new GameObject[side, side];

    highlight.GetComponent<SpriteRenderer>().color = Color.red;
    highlight.SetActive(false);

    float midPoint = (((float)side * tileLength) + ((float)(side - 1) * tileBuffer)) / 2;
    this.transform.position = new Vector3(midPoint, midPoint, 0);

    for(int i = 0; i < side; i++)
    {
      for(int j = 0; j < side; j++)
      {
        GameObject obj = Instantiate(tile, new Vector3((tileLength / 2) + (tileLength + tileBuffer) * i, (tileLength / 2) + (tileLength + tileBuffer) * j, 0), Quaternion.identity);
        obj.transform.parent = this.transform;
        grid[i, j] = obj;
      }
    }

    for (int i = 0; i < side; i++)
    {
      for (int j = 0; j < side; j++)
      {
        grid[i, j].GetComponent<Cell>().SetCellStatus(CellType.HIDDEN);
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (activeInput)
    {
      if (Input.GetKeyUp(KeyCode.E))
      {
        completedDegrees = Time.deltaTime * speed;
        direction = -1;
        activeInput = false;

        this.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * speed * direction)), Space.World);
      }
      if (Input.GetKeyUp(KeyCode.Q))
      {
        completedDegrees = Time.deltaTime * speed;
        direction = 1;
        activeInput = false;

        this.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * speed * direction)), Space.World);
      }
    }
    else
    {
      if(completedDegrees < 90)
      {
        completedDegrees += (Time.deltaTime * speed);
        this.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * speed * direction)), Space.World);
      }
      else
      {
        activeInput = true;
      }
    }
  }
  
  public ref GameObject GetHighlight()
  {
    return ref highlight;
  }
}
