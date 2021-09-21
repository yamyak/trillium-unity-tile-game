using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
  public GameObject main;
  public GameObject create;

  public Dropdown player1Type;
  public Dropdown player1Color;
  public Dropdown player2Type;
  public Dropdown player2Color;

  private Color lightRed;
  private Color red;
  private Color lightBlue;
  private Color blue;

  public GameObject tile;
  public int side = 100;
  public float tileLength = 1.0f;
  public float tileBuffer = 0.25f;

  private float midPoint;

  public float speed = 5.0f;

  public void LoadLevel(string levelName)
  {
    SceneManager.LoadScene(levelName);
  }

  public void QuitGame()
  {
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #else
      Application.Quit();
    #endif
  }

  public void GoToMenu(string menuName)
  {
    if(menuName.Equals("Main"))
    {
      create.SetActive(false);
      main.SetActive(true);
    }
    else if(menuName.Equals("Create"))
    {
      main.SetActive(false);
      create.SetActive(true);
    }
  }

  public void ChangeDropdownColor(string text, Dropdown drop)
  {
    ColorBlock block = drop.colors;

    if (text.Equals("Red"))
    {
      block.normalColor = lightRed;
      block.selectedColor = lightRed;
      block.highlightedColor = red;

      drop.colors = block;
    }
    else if (text.Equals("Blue"))
    {
      block.normalColor = lightBlue;
      block.selectedColor = lightBlue;
      block.highlightedColor = blue;

      drop.colors = block;
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    main.SetActive(true);
    create.SetActive(false);

    lightRed = new Color(0.9607844f, 0.4117647f, 0.4117647f, 1.0f);
    red = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    lightBlue = new Color(0.3960785f, 0.627451f, 0.9215687f, 1.0f);
    blue = new Color(0.0f, 0.3058824f, 1.0f, 1.0f);
    
    player1Color.onValueChanged.AddListener(delegate {
      ChangeDropdownColor(player1Color.captionText.text, player1Type);
      ChangeDropdownColor(player1Color.captionText.text, player1Color);
    });

    player2Color.onValueChanged.AddListener(delegate {
      ChangeDropdownColor(player2Color.captionText.text, player2Type);
      ChangeDropdownColor(player2Color.captionText.text, player2Color);
    });

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
