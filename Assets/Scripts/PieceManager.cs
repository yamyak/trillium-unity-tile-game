using UnityEngine;

public class PieceScript : MonoBehaviour
{
  private float tileLength;
  private float tileBuffer;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    
  }

  public void Initialize(float tileLength, float tileBuffer)
  {
    this.tileLength = tileLength;
    this.tileBuffer = tileBuffer;
  }

  public void SetLocation(int x, int y)
  {
    float xLocation = (tileLength / 2) + (x * (tileLength + tileBuffer));
    float yLocation = (tileLength / 2) + (y * (tileLength + tileBuffer));

    transform.position = new Vector3(xLocation, yLocation, transform.position.z);
  }
}
