using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts;

public class InitializeTiles : MonoBehaviour
{
  public GameObject tile;
  public int side = 100;
  public float sideLength = 1.0f;
  public float buffer = 0.25f;

  private Map map;

  // Start is called before the first frame update
  void Start()
  {
    map = new Map(side, side);

    for(int i = 0; i < side; i++)
    {
      for(int j = 0; j < side; j++)
      {
        GameObject obj = Instantiate(tile, new Vector3((sideLength + buffer) * i, (sideLength + buffer) * j, 0), Quaternion.identity);
        map.GetCell(i, j).SetObject(ref obj);        
      }
    }

    for (int i = 0; i < side; i++)
    {
      for (int j = 0; j < side; j++)
      {
        map.GetCell(i, j).SetCellStatus(CellType.HIDDEN);
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
        
  } 
}