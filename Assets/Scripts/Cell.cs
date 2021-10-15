using UnityEngine;

public enum CellType
{
  EMPTY = 0,
  VOID = 1,
  HIDDEN = 2,
  OCCUPIED = 3
}

public class Cell : MonoBehaviour
{
  public void SetCellStatus(CellType type)
  {
    Color color = Color.white;

    switch(type)
    {
      case CellType.EMPTY:
        color = Color.white;
        break;
      case CellType.VOID:
        color = Color.black;
        break;
      case CellType.HIDDEN:
        color = Color.gray;
        break;
      case CellType.OCCUPIED:
        color = Color.blue;
        break;
    }

    this.GetComponent<SpriteRenderer>().color = color;
  }
  
  void OnMouseEnter()
  {
    GameObject highlight = this.GetComponentInParent<MapManager>().GetHighlight();
    highlight.transform.position = new Vector3(transform.position.x, transform.position.y, 0.01f);
    highlight.SetActive(true);
  }

  private void OnMouseExit()
  {
    GameObject highlight = this.GetComponentInParent<MapManager>().GetHighlight();
    highlight.SetActive(false);
  }
}