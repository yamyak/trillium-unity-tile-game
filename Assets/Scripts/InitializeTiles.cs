using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts;

public class InitializeTiles : MonoBehaviour
{
  public GameObject parent;
  public GameObject tile;
  public int side = 100;
  public float tileLength = 1.0f;
  public float tileBuffer = 0.25f;

  private Map map;

  public float speed = 10f;
  private bool activeInput = true;
  private float completedDegrees, direction;

  // Start is called before the first frame update
  void Start()
  {
    float midPoint = (((float)side * tileLength) + ((float)(side - 1) * tileBuffer)) / 2;
    parent.transform.position = new Vector3(midPoint, midPoint, 0);

    map = new Map(side, side);

    for(int i = 0; i < side; i++)
    {
      for(int j = 0; j < side; j++)
      {
        GameObject obj = Instantiate(tile, new Vector3((tileLength / 2) + (tileLength + tileBuffer) * i, (tileLength / 2) + (tileLength + tileBuffer) * j, 0), Quaternion.identity);
        obj.transform.parent = parent.transform;
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
    if (activeInput)
    {
      if (Input.GetKeyUp(KeyCode.E))
      {
        completedDegrees = Time.deltaTime * speed;
        direction = -1;
        activeInput = false;

        parent.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * speed * direction)), Space.World);
      }
      if (Input.GetKeyUp(KeyCode.Q))
      {
        completedDegrees = Time.deltaTime * speed;
        direction = 1;
        activeInput = false;

        parent.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * speed * direction)), Space.World);
      }
    }
    else
    {
      if(completedDegrees < 90)
      {
        completedDegrees += (Time.deltaTime * speed);
        parent.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * speed * direction)), Space.World);
      }
      else
      {
        activeInput = true;
      }
    }
  } 
}
