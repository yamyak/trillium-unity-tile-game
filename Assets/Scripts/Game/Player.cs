using UnityEngine;
using System.Threading;
using System.Collections.Generic;

// player objects
public class Player
{
  // list of player pieces
  private List<GameObject> pieces;

  // player color
  private MapColor color;

  // player algorithm thread and algorithm
  private Thread handler;
  private BaseAlgorithm algorithm;

  // constructor
  public Player(GameObject map, MapColor color, string algo, int x, int y)
  {
    // initialize piece list
    pieces = new List<GameObject>();

    // set color
    this.color = color;

    // set algorithm object
    if(algo == "Human")
    {
      algorithm = new HumanAlgorithm();
    }
    else if(algo == "Computer")
    {
      algorithm = new ComputerAlgorithm();
    }

    // create base piece on map and add to piece list
    GameObject piece = map.GetComponent<MapScript>().AddPieceToMap("Base", x, y, this.color);
    if(piece != null)
    {
      AddPiece(piece);
    }

    // add callback function for when leaving basic play state
    StateManager.GetInstance().AddCallback(CallbackType.STATE_EXIT, GameState.PLAYING_BASIC, OnExitPlayingBasic);
  }

  // called for current player when entering basic play state
  public void ProcessMove()
  {
    // activate all player pieces to make them selectable
    foreach(GameObject obj in pieces)
    {
      obj.GetComponent<PieceScript>().SetActive(true);
    }

    //handler = new Thread(new ThreadStart(RunAlgorithm));
    //handler.Start();
  }

  private void RunAlgorithm()
  {
    algorithm.GetNextMove();

    StateManager.GetInstance().SetState(GameState.READY);
  }

  // add piece to player piece list
  public void AddPiece(GameObject piece)
  {
    pieces.Add(piece);
  }

  // callback function for when leaving basic play state
  public void OnExitPlayingBasic()
  {
    // pieces are only active and selectable in basic play state
    // deactivate all player pieces when leaving basic play state
    foreach (GameObject obj in pieces)
    {
      obj.GetComponent<PieceScript>().SetActive(false);
    }
  }
}
