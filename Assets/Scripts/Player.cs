using UnityEngine;
using System.Threading;
using System.Collections.Generic;

public class Player
{
  List<GameObject> pieces;
  private Color highlight;

  Thread handler;
  BaseAlgorithm algorithm;

  public Player(string color, string algo)
  {
    pieces = new List<GameObject>();

    if(color == "Red")
    {
      highlight = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    }
    else if(color == "Blue")
    {
      highlight = new Color(0.0f, 0.3058824f, 1.0f, 1.0f);
    }

    if(algo == "Human")
    {
      algorithm = new HumanAlgorithm();
    }
    else if(algo == "Computer")
    {
      algorithm = new ComputerAlgorithm();
    }
  }

  public void ProcessMove()
  {
    handler = new Thread(new ThreadStart(algorithm.GetNextMove));
    handler.Start();
  }

  public void AddPiece(GameObject piece)
  {
    pieces.Add(piece);
  }
}
