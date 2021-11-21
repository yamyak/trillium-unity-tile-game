using UnityEngine;

public class TileScript : MonoBehaviour
{
  private int x;
  private int y;

  public void SetLocation(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  public void SetCellColor(MapColor colorIn)
  {
    this.GetComponent<SpriteRenderer>().color = Constants.EnumToCellColor(colorIn);
  }
}