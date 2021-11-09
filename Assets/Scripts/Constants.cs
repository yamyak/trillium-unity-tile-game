using UnityEngine;

public enum MapColor
{
  BLACK = 0,
  WHITE = 1,
  GRAY = 2,
  RED = 3,
  BLUE = 4
}

public enum GameState
{
  READY,
  PLAYING_BASIC,
  PLAYING_ACTION,
  PAUSE,
  GAME_OVER
}

public enum CallbackType
{
  STATE_ENTER,
  STATE_EXIT,
  STATE_CHANGE
}

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
  //public float cameraSpeed = 10;

  public static Color lightRed = new Color(0.9607844f, 0.4117647f, 0.4117647f, 1.0f);
  public static Color red = new Color(1.0f, 0.0f, 0.0f, 1.0f);
  public static Color lightBlue = new Color(0.3960785f, 0.627451f, 0.9215687f, 1.0f);
  public static Color blue = new Color(0.0f, 0.3058824f, 1.0f, 1.0f);

  public static float tileLength = 1.0f;
  public static float tileBuffer = 0.25f;

  public static int mainMenuMapSize = 100;
  public static float mainMenuBgSpeed = 5.0f;

  public static float cameraSpeed = 10f;
  public static float mapRotationSpeed = 10f;

  public delegate void StateChangeCallback();

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
