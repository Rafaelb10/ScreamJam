using UnityEngine;
using UnityEngine.AI;

public class FleeState : AI_State
{
    public FleeState(AI_EnemyController enemyController, AI_StateMachine stateMachine) : base(enemyController, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Entering Flee State");
        _enemyController.GetAnimationHandler().FleeAnimationOn();
    }
    public override void Run()
    {
        if (_enemyController == null || _enemyController.GetPlayer() == null) return;
        
        NavMeshAgent _agent = _enemyController.GetComponent<NavMeshAgent>();
        
        _agent.speed = _enemyController.GetRunSpeed();
        _agent.SetDestination(_enemyController.GetPatrolPoints()[1].gameObject.transform.position);

        float distanceToPoint = Vector3.Distance(_enemyController.transform.position, _enemyController.GetPatrolPoints()[1].gameObject.transform.position);
        if (distanceToPoint < 1f)
        {
            _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Patrol));
        }

        // _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Chase));
    }
    public override void Exit() 
    {
        _enemyController.GetAnimationHandler().FleeAnimationOff();
    }
}
