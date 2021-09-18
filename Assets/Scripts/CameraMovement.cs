using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
  public float cameraSpeed = 10;

  private float sideLength;

  // Start is called before the first frame update
  void Start()
  {
    InitializeTiles script = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<InitializeTiles>();

    float side = script.side;
    float tileLength = script.tileLength;
    float tileBuffer = script.tileBuffer;

    sideLength = ((float)side * tileLength) + ((float)(side - 1) * tileBuffer);

    Quaternion myRotation = Quaternion.identity;
    myRotation.eulerAngles = new Vector3(-37.71f, 50.641f, -63.358f);
    transform.rotation = myRotation;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.RightArrow))
    {
      transform.Translate(new Vector3(cameraSpeed * Time.deltaTime, -cameraSpeed * Time.deltaTime, 0), Space.World);
    }
    if (Input.GetKey(KeyCode.LeftArrow))
    {
      transform.Translate(new Vector3(-cameraSpeed * Time.deltaTime, cameraSpeed * Time.deltaTime, 0), Space.World);
    }
    if (Input.GetKey(KeyCode.DownArrow))
    {
      transform.Translate(new Vector3(-cameraSpeed * Time.deltaTime, -cameraSpeed * Time.deltaTime, 0), Space.World);
    }
    if (Input.GetKey(KeyCode.UpArrow))
    {
      transform.Translate(new Vector3(cameraSpeed * Time.deltaTime, cameraSpeed * Time.deltaTime, 0), Space.World);
    }

    if(transform.position.x < -10)
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
