using UnityEngine;

public class MainMenuBGCycle : MonoBehaviour
{
  public GameObject tile;
  public int side = 100;
  public float tileLength = 1.0f;
  public float tileBuffer = 0.25f;

  private float midPoint;

  public float speed = 5.0f;

  // Start is called before the first frame update
  void Start()
  {
    Time.timeScale = 1;

    for (int i = 0; i < side; i++)
    {
      for (int j = 0; j < side; j++)
      {
        Instantiate(tile, new Vector3((tileLength / 2) + (tileLength + tileBuffer) * i, (tileLength / 2) + (tileLength + tileBuffer) * j, 0), Quaternion.identity);
      }
    }

    midPoint = (((float)side * tileLength) + ((float)(side - 1) * tileBuffer)) / 2;
    float hypotenuse = Mathf.Sqrt(Mathf.Pow(midPoint, 2) * 2) * 1.3f;
    transform.position = new Vector3(midPoint + hypotenuse, midPoint, transform.position.z);

    Quaternion myRotation = Quaternion.identity;
    myRotation.eulerAngles = new Vector3(0, -80, 90);
    transform.rotation = myRotation;
  }

  // Update is called once per frame
  void Update()
  {
    transform.RotateAround(new Vector3(midPoint, midPoint, 0), Vector3.back, speed * Time.deltaTime);
  }
}
