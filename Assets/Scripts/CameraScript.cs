using UnityEngine;

// camera script
public class CameraScript : MonoBehaviour
{
  // map side length
  private float sideLength;
  // flag for whether game is paused
  private bool active;

  // Start is called before the first frame update
  void Start()
  {
    // initialize game as not paused
    active = true;

    // retrieve the selected map length from main menu scene
    float side = MainMenuScript.mapLength;
    // calculate length of side of map
    sideLength = ((float)side * Constants.tileLength) + ((float)(side - 1) * Constants.tileBuffer);

    // calculate and set camera angle
    Quaternion myRotation = Quaternion.identity;
    myRotation.eulerAngles = new Vector3(-37.71f, 50.641f, -63.358f);
    transform.rotation = myRotation;

    // register callbacks
    StateManager.GetInstance().AddCallback(CallbackType.STATE_ENTER, GameState.PAUSE, OnEnterPauseState);
    StateManager.GetInstance().AddCallback(CallbackType.STATE_EXIT, GameState.PAUSE, OnExitPauseState);
  }

  // pause state enter callback
  public void OnEnterPauseState()
  {
    // deactivate camera movement
    active = false;
  }

  // pause state exit callback
  public void OnExitPauseState()
  {
    // activate camera movement
    active = true;
  }

  // Update is called once per frame
  void Update()
  {
    // if game not pause
    if (active)
    {
      // based on WASD keys, move camera around
      if (Input.GetKey(KeyCode.D))
      {
        transform.Translate(new Vector3(Constants.cameraSpeed * Time.deltaTime, -Constants.cameraSpeed * Time.deltaTime, 0), Space.World);
      }
      if (Input.GetKey(KeyCode.A))
      {
        transform.Translate(new Vector3(-Constants.cameraSpeed * Time.deltaTime, Constants.cameraSpeed * Time.deltaTime, 0), Space.World);
      }
      if (Input.GetKey(KeyCode.S))
      {
        transform.Translate(new Vector3(-Constants.cameraSpeed * Time.deltaTime, -Constants.cameraSpeed * Time.deltaTime, 0), Space.World);
      }
      if (Input.GetKey(KeyCode.W))
      {
        transform.Translate(new Vector3(Constants.cameraSpeed * Time.deltaTime, Constants.cameraSpeed * Time.deltaTime, 0), Space.World);
      }
    }

    // limit camera position to certain area around map
    // if camera goes outside bounds, cap it to the bounds
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