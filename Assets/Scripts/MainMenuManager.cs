using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
  public GameObject main;
  public GameObject create;

  public Dropdown player1TypeDD;
  public Dropdown player1ColorDD;
  public Dropdown player2TypeDD;
  public Dropdown player2ColorDD;
  public Dropdown mapSizeDD;

  public GameObject tile;

  private float midPoint;

  public static string[] playerType = { "Human", "Computer" };
  public static MapColor[] playerColor = { MapColor.RED, MapColor.BLUE };

  public static int mapLength = 100;

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
      block.normalColor = Constants.lightRed;
      block.selectedColor = Constants.lightRed;
      block.highlightedColor = Constants.red;

      drop.colors = block;
    }
    else if (text.Equals("Blue"))
    {
      block.normalColor = Constants.lightBlue;
      block.selectedColor = Constants.lightBlue;
      block.highlightedColor = Constants.blue;

      drop.colors = block;
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    main.SetActive(true);
    create.SetActive(false);
    
    player1ColorDD.onValueChanged.AddListener(delegate {
      playerColor[0] = (MapColor)Enum.Parse(typeof(MapColor), player1ColorDD.captionText.text, true);

      ChangeDropdownColor(player1ColorDD.captionText.text, player1TypeDD);
      ChangeDropdownColor(player1ColorDD.captionText.text, player1ColorDD);
    });

    player2ColorDD.onValueChanged.AddListener(delegate {
      playerColor[1] = (MapColor)Enum.Parse(typeof(MapColor), player2ColorDD.captionText.text, true);

      ChangeDropdownColor(player2ColorDD.captionText.text, player2TypeDD);
      ChangeDropdownColor(player2ColorDD.captionText.text, player2ColorDD);
    });

    player1TypeDD.onValueChanged.AddListener(delegate
    {
      playerType[0] = player1TypeDD.captionText.text;
    });

    player2TypeDD.onValueChanged.AddListener(delegate
    {
      playerType[1] = player2TypeDD.captionText.text;
    });

    mapSizeDD.onValueChanged.AddListener(delegate
    {
      mapLength = (mapSizeDD.value + 1) * 100;
    });

    Time.timeScale = 1;

    for (int i = 0; i < Constants.mainMenuMapSize; i++)
    {
      for (int j = 0; j < Constants.mainMenuMapSize; j++)
      {
        Instantiate(tile, new Vector3((Constants.tileLength / 2) + (Constants.tileLength + Constants.tileBuffer) * i, 
          (Constants.tileLength / 2) + (Constants.tileLength + Constants.tileBuffer) * j, 0), Quaternion.identity);
      }
    }

    midPoint = (((float)Constants.mainMenuMapSize * Constants.tileLength) + ((float)(Constants.mainMenuMapSize - 1) * Constants.tileBuffer)) / 2;
    float hypotenuse = Mathf.Sqrt(Mathf.Pow(midPoint, 2) * 2) * 1.3f;
    transform.position = new Vector3(midPoint + hypotenuse, midPoint, transform.position.z);

    Quaternion myRotation = Quaternion.identity;
    myRotation.eulerAngles = new Vector3(0, -80, 90);
    transform.rotation = myRotation;
  }

  // Update is called once per frame
  void Update()
  {
    transform.RotateAround(new Vector3(midPoint, midPoint, 0), Vector3.back, Constants.mainMenuBgSpeed * Time.deltaTime);
  }
}