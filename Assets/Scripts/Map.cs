using UnityEngine;

namespace Assets.Scripts
{
  class Map
  {
    private Cell[,] grid;

    public Map(int x, int y)
    {
      grid = new Cell[x, y];

      for(int i = 0; i < x; i++)
      {
        for(int j = 0; j < y; j++)
        {
          grid[i, j] = new Cell();
        }
      }
    }

    public ref Cell GetCell(int x, int y)
    {
      return ref grid[x, y];
    }
  }
}
