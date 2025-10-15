public class AI_StateMachine
{
    private AI_State _currentState;

    public void Initialize(AI_State startState)
    {
        _currentState = startState;
        _currentState.Enter();
    }

    public void ChangeState(AI_State newState)
    {
        if (_currentState == newState) return;

        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void RunState()
    {
        _currentState?.Run();
    }
}
