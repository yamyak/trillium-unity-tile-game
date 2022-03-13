using System.Collections.Generic;

// state manager singleton
public sealed class StateManager
{
  // singleton object
  private static StateManager instance;

  // mutex object
  private static object locker;

  // current and previous game state
  private static GameState state;
  private static GameState lastState;

  // current player number
  private static int currentPlayer;

  // maps of state enter, state exit, and state change callbacks
  private static Dictionary<GameState, List<Constants.StateChangeCallback>> stateEnterCallbacks;
  private static Dictionary<GameState, List<Constants.StateChangeCallback>> stateExitCallbacks;
  private static List<Constants.StateChangeCallback> stateChangeCallbacks;

  // private constructor for singleton
  private StateManager()
  {
    // initialize the game state
    currentPlayer = 1;
    state = GameState.READY;
    lastState = GameState.READY;
    locker = new object();

    // initialize the state enter callback map
    stateEnterCallbacks = new Dictionary<GameState, List<Constants.StateChangeCallback>>();
    stateEnterCallbacks[GameState.READY] = new List<Constants.StateChangeCallback>();
    stateEnterCallbacks[GameState.PLAYING_BASIC] = new List<Constants.StateChangeCallback>();
    stateEnterCallbacks[GameState.PLAYING_ACTION] = new List<Constants.StateChangeCallback>();
    stateEnterCallbacks[GameState.PAUSE] = new List<Constants.StateChangeCallback>();
    stateEnterCallbacks[GameState.GAME_OVER] = new List<Constants.StateChangeCallback>();

    // initialize the state exit callback map
    stateExitCallbacks = new Dictionary<GameState, List<Constants.StateChangeCallback>>();
    stateExitCallbacks[GameState.READY] = new List<Constants.StateChangeCallback>();
    stateExitCallbacks[GameState.PLAYING_BASIC] = new List<Constants.StateChangeCallback>();
    stateExitCallbacks[GameState.PLAYING_ACTION] = new List<Constants.StateChangeCallback>();
    stateExitCallbacks[GameState.PAUSE] = new List<Constants.StateChangeCallback>();
    stateExitCallbacks[GameState.GAME_OVER] = new List<Constants.StateChangeCallback>();

    // initialize the state change callback map
    stateChangeCallbacks = new List<Constants.StateChangeCallback>();
  }

  // singleton accessor function
  public static StateManager GetInstance()
  {
    if(instance == null)
    {
      instance = new StateManager();
    }

    return instance;
  }

  // add callback to appropriate map
  public void AddCallback(CallbackType type, GameState state, Constants.StateChangeCallback callback)
  {
    // lock the mutex
    lock (locker)
    {
      // select the correct map
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

      // add callback to map
      callbackList.Add(callback);
    }
  }

  // exercise all callbacks
  private void ExerciseCallbacks(GameState nowState, GameState pastState)
  {
    // state exit callbacks first based on past state
    List<Constants.StateChangeCallback> exitCallbacks = stateExitCallbacks[pastState];
    foreach(Constants.StateChangeCallback callback in exitCallbacks)
    {
      callback();
    }

    // state change callbacks second
    foreach (Constants.StateChangeCallback callback in stateChangeCallbacks)
    {
      callback();
    }

    // state enter callbacks last based on current state
    List<Constants.StateChangeCallback> enterCallbacks = stateEnterCallbacks[nowState];
    foreach (Constants.StateChangeCallback callback in enterCallbacks)
    {
      callback();
    }
  }

  // set the current game state
  public void SetState(GameState stateIn)
  {
    lock (locker)
    {
      lastState = state;
      state = stateIn;
    }

    // exercise callbacks
    ExerciseCallbacks(state, lastState);
  }

  // get current game state
  public GameState GetState()
  {
    lock (locker)
    {
      return state;
    }
  }

  // revert back to last state
  public void RevertToLastState()
  {
    GameState pastState = state;
    lock (locker)
    {
      state = lastState;
    }

    // exercise callbacks
    ExerciseCallbacks(state, pastState);
  }

  // switch current player
  public void SwitchToNextPlayer()
  {
    currentPlayer = currentPlayer > 1 ? 1 : currentPlayer + 1;
  }

  // get current player
  public int GetCurrentPlayer()
  {
    return currentPlayer;
  }
}