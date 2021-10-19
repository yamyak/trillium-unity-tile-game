using UnityEngine;
using System.Collections.Generic;

public class PieceScript : MonoBehaviour
{
  private int x;
  private int y;
  private MapColor color;

  private GameObject map;

  public List<Location> moveLocations;
  public List<Location> attackLocations;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    
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
    this.GetComponent<BoxCollider>().enabled = flag;
    map.GetComponent<MapScript>().SetHighlightColor(x, y, color);
    map.GetComponent<MapScript>().ActivateHighlight(x, y, flag);
  }

  void OnMouseEnter()
  {
    map.GetComponent<MapScript>().SetCellColor(x, y, color);
  }

  private void OnMouseExit()
  {
    map.GetComponent<MapScript>().SetCellColor(x, y, MapColor.WHITE);
  }
}
