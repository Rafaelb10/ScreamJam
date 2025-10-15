using UnityEngine;

public class IdleState : AI_State
{
    private float _timeOfIdle;
    private float _idleDuration = 2f;
    public IdleState(AI_EnemyController enemyController, AI_StateMachine stateMachine) : base(enemyController, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }
    public override void Run()
    {
        Debug.Log("Running Idle State");
        _timeOfIdle += Time.deltaTime;
        Debug.Log("Time in Idle: " + _timeOfIdle);
        if (_timeOfIdle >= _idleDuration)
        {
            _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Patrol));
        }
    }
    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}
