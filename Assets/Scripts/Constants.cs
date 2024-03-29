using UnityEngine;

// possible options for tile colors
public enum MapColor
{
  BLACK = 0,
  WHITE = 1,
  GRAY = 2,
  RED = 3,
  BLUE = 4
}

// game states
public enum GameState
{
  READY,
  PLAYING_BASIC,
  PLAYING_ACTION,
  PAUSE,
  GAME_OVER
}

// button options on action menu
public enum ActionButton
{
  ACTION1 = 0,
  ACTION2 = 1,
  ACTION3 = 2,
  ACTION4 = 3,
  UP = 4,
  DOWN = 5,
  BACK = 6,
  CANCEL = 7
}

// callback type
// called when entering a state, exiting a state, or when a state changes
public enum CallbackType
{
  STATE_ENTER,
  STATE_EXIT,
  STATE_CHANGE
}

// location (both x and y values)
public struct Location
{
  public Location(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  public int x;
  public int y;
}

public static class Constants
{
  public static int actionMenuNumButtons = 4;

  // tile and tile highlight colors
  public static Color lightRed = new Color(0.9607844f, 0.4117647f, 0.4117647f, 1.0f);
  public static Color red = new Color(1.0f, 0.0f, 0.0f, 1.0f);
  public static Color lightBlue = new Color(0.3960785f, 0.627451f, 0.9215687f, 1.0f);
  public static Color blue = new Color(0.0f, 0.3058824f, 1.0f, 1.0f);

  // tile object sizes
  public static float tileLength = 1.0f;
  public static float tileBuffer = 0.25f;

  // main menu background options
  public static int mainMenuMapSize = 100;
  public static float mainMenuBgSpeed = 5.0f;

  // game camera movement speed
  public static float cameraSpeed = 10f;
  // game camera rotation speed
  public static float mapRotationSpeed = 10f;

  // callback delegates
  public delegate void StateChangeCallback();
  public delegate void OptionActionCallback();
  public delegate void OptionTileCallback(int x, int y);

  // calculate tile location based on x and y coordinates
  // z location is just a pass-through
  public static Vector3 CalculateLocation(int xCell, int yCell, float zLoc)
  {
    return new Vector3((tileLength / 2) + (tileLength + tileBuffer) * xCell,
        (tileLength / 2) + (tileLength + tileBuffer) * yCell, zLoc);
  }

  // get tile highlight color based on tile color enum
  public static Color EnumToHighlightColor(MapColor colorIn)
  {
    Color color = Color.gray;

    switch (colorIn)
    {
      case MapColor.RED:
        color = lightRed;
        break;
      case MapColor.BLUE:
        color = lightBlue;
        break;
      default:
        break;
    }

    return color;
  }

  // get tile color based on tile color enum
  public static Color EnumToCellColor(MapColor colorIn)
  {
    Color color = Color.white;

    switch (colorIn)
    {
      case MapColor.BLACK:
        color = Color.black;
        break;
      case MapColor.WHITE:
        color = Color.white;
        break;
      case MapColor.GRAY:
        color = Color.gray;
        break;
      case MapColor.RED:
        color = Color.red;
        break;
      case MapColor.BLUE:
        color = Color.blue;
        break;
    }

    return color;
  }
}
