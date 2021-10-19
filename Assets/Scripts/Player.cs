using UnityEngine;
using System.Threading;
using System.Collections.Generic;

public class Player
{
  private GameObject map;

  List<GameObject> pieces;

  private MapColor color;

  Thread handler;
  BaseAlgorithm algorithm;

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

    GameObject piece = map.GetComponent<MapScript>().AddPieceToMap("Base", x, y, this.color, false);
    if(piece != null)
    {
      AddPiece(piece);
    }
  }

  public void ProcessMove()
  {
    foreach(GameObject obj in pieces)
    {
      obj.GetComponent<PieceScript>().SetActive(true);
    }

    handler = new Thread(new ThreadStart(algorithm.GetNextMove));
    handler.Start();
  }

  public void AddPiece(GameObject piece)
  {
    pieces.Add(piece);
  }
}
