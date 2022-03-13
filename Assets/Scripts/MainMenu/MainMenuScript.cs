using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// attached to main menu camera
public class MainMenuScript : MonoBehaviour
{
  // main menu item
  public GameObject main;
  // create new game menu item
  public GameObject create;

  // player 1 type selection (human or algorithm) dropdown
  public Dropdown player1TypeDD;
  // player 1 color selection dropdown
  public Dropdown player1ColorDD;
  // player 2 type selection (human or algorithm) dropdown
  public Dropdown player2TypeDD;
  // player 2 color selection dropdown
  public Dropdown player2ColorDD;
  // map size input field
  public InputField mapSizeInput;

  // tile item
  // uses for main menu background animation
  public GameObject tile;

  // midpoint the background animation rotates around
  private float midPoint;

  // player type selection dropdown options
  public string[] playerType = { "Human", "Computer" };

  // base game map lengths
  public int mapLength = 100;

  // called when "Start Game" button is selected
  public void LoadLevel(string levelName)
  {
    // TODO: need to valdiate input
    mapLength = Int32.Parse(mapSizeInput.text);

    // switches to the next scene
    SceneManager.LoadScene(levelName);
  }

  // called when "Quit Game" button is selected
  public void QuitGame()
  {
    // closes the game
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #else
      Application.Quit();
    #endif
  }

  // called when "Create New Game" or "Back" button is selected
  public void GoToMenu(string menuName)
  {
    if(menuName.Equals("Main"))
    {
      // switch from create menu to main menu
      create.SetActive(false);
      main.SetActive(true);
    }
    else if(menuName.Equals("Create"))
    {
      // switch from main menu to create menu
      main.SetActive(false);
      create.SetActive(true);
    }
  }

  // called when player color dropdown selection is changed
  public void ChangeDropdownColor(string text, Dropdown drop)
  {
    // get dropdown color object
    ColorBlock block = drop.colors;

    // change color based on text of option in dropdown selected
    if (text.Equals("Red"))
    {
      // change dropdown color to red
      block.normalColor = Constants.lightRed;
      block.selectedColor = Constants.lightRed;
      block.highlightedColor = Constants.red;
    }
    else if (text.Equals("Blue"))
    {
      // change dropdown color to blue
      block.normalColor = Constants.lightBlue;
      block.selectedColor = Constants.lightBlue;
      block.highlightedColor = Constants.blue;
    }

    // update dropdown color object
    drop.colors = block;
  }

  // Start is called before the first frame update
  void Start()
  {
    // activate main menu
    main.SetActive(true);
    create.SetActive(false);
    
    // add callback function called when player 1 color dropdown selection changed
    player1ColorDD.onValueChanged.AddListener(delegate {
      // change player 1 type dropdown color
      ChangeDropdownColor(player1ColorDD.captionText.text, player1TypeDD);
      // change player 1 color dropdown color
      ChangeDropdownColor(player1ColorDD.captionText.text, player1ColorDD);
    });

    // add callback function called when player 2 color dropdown selection changed
    player2ColorDD.onValueChanged.AddListener(delegate {
      // change player 2 type dropdown color
      ChangeDropdownColor(player2ColorDD.captionText.text, player2TypeDD);
      // change player 2 color dropdown color
      ChangeDropdownColor(player2ColorDD.captionText.text, player2ColorDD);
    });

    // add callback function called when player 1 type dropdown selection changed
    player1TypeDD.onValueChanged.AddListener(delegate
    {
      // update the player 1 type flag
      playerType[0] = player1TypeDD.captionText.text;
    });

    // add callback function called when player 2 type dropdown selection changed
    player2TypeDD.onValueChanged.AddListener(delegate
    {
      // update the player 2 type flag
      playerType[1] = player2TypeDD.captionText.text;
    });

    // create background animation tiles in 2d matrix formation
    for (int i = 0; i < Constants.mainMenuMapSize; i++)
    {
      for (int j = 0; j < Constants.mainMenuMapSize; j++)
      {
        // create tile object and place in correct location in 2d matrix
        Instantiate(tile, new Vector3((Constants.tileLength / 2) + (Constants.tileLength + Constants.tileBuffer) * i, 
          (Constants.tileLength / 2) + (Constants.tileLength + Constants.tileBuffer) * j, 0), Quaternion.identity);
      }
    }

    // calculate midpoint of the 2d matrix
    midPoint = (((float)Constants.mainMenuMapSize * Constants.tileLength) + ((float)(Constants.mainMenuMapSize - 1) * Constants.tileBuffer)) / 2;
    // calculate radius of camera track around 2d matrix
    float hypotenuse = Mathf.Sqrt(Mathf.Pow(midPoint, 2) * 2) * 1.3f;
    // place camera in starting location
    transform.position = new Vector3(midPoint + hypotenuse, midPoint, transform.position.z);

    // update camera angle to look down on 2d matrix
    Quaternion myRotation = Quaternion.identity;
    myRotation.eulerAngles = new Vector3(0, -80, 90);
    transform.rotation = myRotation;
  }

  // Update is called once per frame
  void Update()
  {
    // rotate the camera around the 2d matrix
    transform.RotateAround(new Vector3(midPoint, midPoint, 0), Vector3.back, Constants.mainMenuBgSpeed * Time.deltaTime);
  }

  // Called when scene switched
  void OnDisable()
  {
    // save all user preferences to be loaded next scene
    PlayerPrefs.SetInt("size", mapLength);
    PlayerPrefs.SetString("player1", playerType[0]);
    PlayerPrefs.SetString("player2", playerType[1]);
    PlayerPrefs.SetString("color1", player1ColorDD.captionText.text);
    PlayerPrefs.SetString("color2", player2ColorDD.captionText.text);
  }
}