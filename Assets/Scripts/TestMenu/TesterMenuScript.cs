using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TesterMenuScript : MonoBehaviour
{
  // tester menu item
  public GameObject menu;

  // map size input field
  public InputField mapSizeInput;
  // tile configuration file dropdown
  public Dropdown tileDropdown;
  // piece configuration file dropdown
  public Dropdown pieceDropdown;

  // path to game config area
  private String path = "C:\\Users\\" + Environment.UserName + "\\.trillium";

  // base game map lengths
  public int mapLength = 100;

  // Start is called before the first frame update
  void Start()
  {
    // TODO: check if config area exists, create if it doesn't

    // find all tile config files and load into list
    List<String> options = new List<String>();
    var files = Directory.EnumerateFiles(path + "\\Tiles");
    foreach(var file in files)
    {
      options.Add(Path.GetFileName(file));
    }
    // populate dropdown with config file names
    tileDropdown.ClearOptions();
    tileDropdown.AddOptions(options);
    options.Clear();

    // find all piece config files and load into list
    files = Directory.EnumerateFiles(path + "\\Pieces");
    foreach (var file in files)
    {
      options.Add(Path.GetFileName(file));
    }
    // populate dropdown with config file names
    pieceDropdown.ClearOptions();
    pieceDropdown.AddOptions(options);
    options.Clear();
  }
  public void LoadLevel(string levelName)
  {
    // TODO: need to valdiate input
    mapLength = Int32.Parse(mapSizeInput.text);

    TileConfigParser tileParser = new TileConfigParser(path + "\\Tiles\\" + tileDropdown.captionText.text);

    PieceConfigParser pieceParser = new PieceConfigParser(path + "\\Pieces\\" + pieceDropdown.captionText.text);

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

  // Called when scene switched
  void OnDisable()
  {
    // save all user preferences to be loaded next scene
    PlayerPrefs.SetInt("size", mapLength);
    PlayerPrefs.SetString("player1", "Human");
    PlayerPrefs.SetString("player2", "Human");
    PlayerPrefs.SetString("color1", "Red");
    PlayerPrefs.SetString("color2", "Red");
  }
}
