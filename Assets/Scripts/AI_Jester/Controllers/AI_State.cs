using UnityEngine;

public abstract class AI_State
{
    protected AI_EnemyController _enemyController;
    protected AI_StateMachine _stateMachine;

    public AI_State(AI_EnemyController enemyController, AI_StateMachine stateMachine)
    {
        _enemyController = enemyController;
        _stateMachine = stateMachine;
    }
    public virtual void Enter()
    {

    }
    public virtual void Run()
    {

    }

    public virtual void Exit()
    {

    }
}
