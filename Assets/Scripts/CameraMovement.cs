using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  public GameObject game;
  public float cameraSpeed = 10;

  private float sideLength;

  // Start is called before the first frame update
  void Start()
  {
    float side = MainMenuManager.mapLength;
    float tileLength = game.transform.Find("Map").GetComponent<MapManager>().tileLength;
    float tileBuffer = game.transform.Find("Map").GetComponent<MapManager>().tileBuffer;

    sideLength = ((float)side * tileLength) + ((float)(side - 1) * tileBuffer);

    Quaternion myRotation = Quaternion.identity;
    myRotation.eulerAngles = new Vector3(-37.71f, 50.641f, -63.358f);
    transform.rotation = myRotation;
  }

  // Update is called once per frame
  void Update()
  {
    if (game.GetComponent<PlayManager>().GetState() != GameState.PAUSE &&
      game.GetComponent<PlayManager>().GetState() != GameState.GAME_OVER)
    {
      if (Input.GetKey(KeyCode.D))
      {
        transform.Translate(new Vector3(cameraSpeed * Time.deltaTime, -cameraSpeed * Time.deltaTime, 0), Space.World);
      }
      if (Input.GetKey(KeyCode.A))
      {
        transform.Translate(new Vector3(-cameraSpeed * Time.deltaTime, cameraSpeed * Time.deltaTime, 0), Space.World);
      }
      if (Input.GetKey(KeyCode.S))
      {
        transform.Translate(new Vector3(-cameraSpeed * Time.deltaTime, -cameraSpeed * Time.deltaTime, 0), Space.World);
      }
      if (Input.GetKey(KeyCode.W))
      {
        transform.Translate(new Vector3(cameraSpeed * Time.deltaTime, cameraSpeed * Time.deltaTime, 0), Space.World);
      }
    }

    if (transform.position.x < -10)
    {
      transform.position = new Vector3(-10, transform.position.y, transform.position.z);
    }
    if(transform.position.y < -10)
    {
      transform.position = new Vector3(transform.position.x, -10, transform.position.z);
    }
    if(transform.position.x > (sideLength - 15))
    {
      transform.position = new Vector3((sideLength - 15), transform.position.y, transform.position.z);
    }
    if (transform.position.y > (sideLength - 15))
    {
      transform.position = new Vector3(transform.position.x, (sideLength - 15), transform.position.z);
    }
  }
}