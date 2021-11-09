using UnityEngine;
using System.Collections.Generic;

public sealed class StateManager
{
  private static StateManager instance;

  private static object locker;

  private static GameState state;
  private static GameState lastState;

  private static GameObject currentPiece;
  private static int currentPlayer;

  private static Dictionary<GameState, List<Constants.StateChangeCallback>> stateEnterCallbacks;
  private static Dictionary<GameState, List<Constants.StateChangeCallback>> stateExitCallbacks;
  private static List<Constants.StateChangeCallback> stateChangeCallbacks;

  private StateManager()
  {
    currentPlayer = 1;
    state = GameState.READY;
    lastState = GameState.READY;
    locker = new object();

    stateEnterCallbacks = new Dictionary<GameState, List<Constants.StateChangeCallback>>();
    stateEnterCallbacks[GameState.READY] = new List<Constants.StateChangeCallback>();
    stateEnterCallbacks[GameState.PLAYING_BASIC] = new List<Constants.StateChangeCallback>();
    stateEnterCallbacks[GameState.PLAYING_ACTION] = new List<Constants.StateChangeCallback>();
    stateEnterCallbacks[GameState.PAUSE] = new List<Constants.StateChangeCallback>();
    stateEnterCallbacks[GameState.GAME_OVER] = new List<Constants.StateChangeCallback>();

    stateExitCallbacks = new Dictionary<GameState, List<Constants.StateChangeCallback>>();
    stateExitCallbacks[GameState.READY] = new List<Constants.StateChangeCallback>();
    stateExitCallbacks[GameState.PLAYING_BASIC] = new List<Constants.StateChangeCallback>();
    stateExitCallbacks[GameState.PLAYING_ACTION] = new List<Constants.StateChangeCallback>();
    stateExitCallbacks[GameState.PAUSE] = new List<Constants.StateChangeCallback>();
    stateExitCallbacks[GameState.GAME_OVER] = new List<Constants.StateChangeCallback>();

    stateChangeCallbacks = new List<Constants.StateChangeCallback>();
  }

  public static StateManager GetInstance()
  {
    if(instance == null)
    {
      instance = new StateManager();
    }

    return instance;
  }

  public void AddCallback(CallbackType type, GameState state, Constants.StateChangeCallback callback)
  {
    lock (locker)
    {
      List<Constants.StateChangeCallback> callbackList = new List<Constants.StateChangeCallback>(); ;
      switch(type)
      {
        case CallbackType.STATE_ENTER:
          callbackList = stateEnterCallbacks[state];
          break;
        case CallbackType.STATE_EXIT:
          callbackList = stateExitCallbacks[state];
          break;
        case CallbackType.STATE_CHANGE:
          callbackList = stateChangeCallbacks;
          break;
      }

      callbackList.Add(callback);
    }
  }

  private void ExerciseCallbacks(GameState nowState, GameState pastState)
  {
    List<Constants.StateChangeCallback> exitCallbacks = stateExitCallbacks[pastState];
    foreach(Constants.StateChangeCallback callback in exitCallbacks)
    {
      callback();
    }

    foreach (Constants.StateChangeCallback callback in stateChangeCallbacks)
    {
      callback();
    }

    List<Constants.StateChangeCallback> enterCallbacks = stateEnterCallbacks[nowState];
    foreach (Constants.StateChangeCallback callback in enterCallbacks)
    {
      callback();
    }
  }

  public void SetState(GameState stateIn)
  {
    lock (locker)
    {
      lastState = state;
      state = stateIn;
    }

    ExerciseCallbacks(state, lastState);
  }

  public GameState GetState()
  {
    lock (locker)
    {
      return state;
    }
  }

  public void RevertToLastState()
  {
    GameState pastState = state;
    lock (locker)
    {
      state = lastState;
    }

    ExerciseCallbacks(state, pastState);
  }

  public void SetCurrentPiece(GameObject piece)
  {
    currentPiece = piece;
  }

  public GameObject GetCurrentPiece()
  {
    return currentPiece;
  }

  public void SwitchToNextPlayer()
  {
    currentPlayer = currentPlayer > 1 ? 1 : currentPlayer + 1;
  }

  public int GetCurrentPlayer()
  {
    return currentPlayer;
  }
}