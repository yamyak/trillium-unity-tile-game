using UnityEngine;

public class TileScript : MonoBehaviour
{
  // 2d matrix coordinates of tile
  private int x;
  private int y;

  // set tile location
  public void SetLocation(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  // set tile color
  public void SetCellColor(MapColor colorIn)
  {
    this.GetComponent<SpriteRenderer>().color = Constants.EnumToCellColor(colorIn);
  }
}