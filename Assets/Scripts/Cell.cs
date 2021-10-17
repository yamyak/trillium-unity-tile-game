using UnityEngine;

public class Cell : MonoBehaviour
{
  public void SetCellColor(MapColor colorIn)
  {
    Color color = Constants.EnumToCellColor(colorIn);

    this.GetComponent<SpriteRenderer>().color = color;
  }
}