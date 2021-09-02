using UnityEngine;

namespace Assets.Scripts
{
  enum CellType
  {
    EMPTY = 0,
    VOID = 1,
    HIDDEN = 2,
    OCCUPIED = 3
  }

  class Cell
  {
    private GameObject obj;

    public Cell()
    {
    }

    public void SetObject(ref GameObject obj)
    {
      this.obj = obj;
      this.obj.GetComponent<SpriteRenderer>().color = Color.white;
    }

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

      this.obj.GetComponent<SpriteRenderer>().color = color;
    }
  }
}
