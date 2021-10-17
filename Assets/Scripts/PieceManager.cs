using UnityEngine;

public class PieceManager : MonoBehaviour
{
  private bool active;
  private int x;
  private int y;
  private MapColor color;

  private GameObject map;

  // Start is called before the first frame update
  void Start()
  {
    active = false;
  }

  // Update is called once per frame
  void Update()
  {
    
  }

  public void Intialize(bool active, GameObject map, MapColor color)
  {
    this.active = active;
    this.map = map;
    this.color = color;

    transform.Find("Highlight").GetComponent<SpriteRenderer>().color = Constants.EnumToHighlightColor(color);
  }

  public void SetLocation(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  public void SetActive(bool flag)
  {
    active = flag;
    transform.Find("Highlight").gameObject.SetActive(flag);
  }

  void OnMouseEnter()
  {
    if (active)
    {
      map.GetComponent<MapManager>().SetCellColor(x, y, color);
    }
  }

  private void OnMouseExit()
  {
    if (active)
    {
      map.GetComponent<MapManager>().SetCellColor(x, y, MapColor.WHITE);
    }
  }
}
