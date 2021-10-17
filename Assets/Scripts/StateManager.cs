public sealed class StateManager
{
  private static StateManager instance;
  private static GameState state;
  private static GameState lastState;
  private static object locker;

  private StateManager()
  {
    state = GameState.READY;
    lastState = GameState.READY;
    locker = new object();
  }

  public static StateManager GetInstance()
  {
    if(instance == null)
    {
      instance = new StateManager();
    }

    return instance;
  }

  public GameState SetState(GameState stateIn)
  {
    lock (locker)
    {
      lastState = state;
      state = stateIn;

      return state;
    }
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
    lock (locker)
    {
      state = lastState;
    }
  }
}