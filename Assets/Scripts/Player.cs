using UnityEngine;
using System.Threading;
using System.Collections.Generic;

public class Player
{
  private GameObject map;

  private List<GameObject> pieces;

  private MapColor color;

  private Thread handler;
  private BaseAlgorithm algorithm;

  public Player(GameObject map, MapColor color, string algo, int x, int y)
  {
    this.map = map;
    pieces = new List<GameObject>();

    this.color = color;

    if(algo == "Human")
    {
      algorithm = new HumanAlgorithm();
    }
    else if(algo == "Computer")
    {
      algorithm = new ComputerAlgorithm();
    }

    GameObject piece = map.GetComponent<MapScript>().AddPieceToMap("Base", x, y, this.color);
    if(piece != null)
    {
      AddPiece(piece);
    }

    StateManager.GetInstance().AddCallback(CallbackType.STATE_EXIT, GameState.PLAYING_BASIC, OnExitPlayingBasic);
  }

  public void ProcessMove()
  {
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

  public void AddPiece(GameObject piece)
  {
    pieces.Add(piece);
  }

  public void OnExitPlayingBasic()
  {
    foreach (GameObject obj in pieces)
    {
      obj.GetComponent<PieceScript>().SetActive(false);
    }
  }
}
