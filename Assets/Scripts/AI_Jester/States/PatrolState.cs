using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : AI_State
{
    private NavMeshAgent _navMeshAgent;
    private Transform[] _patrolPoints;
    private int _currentPatrolIndex = 0;
    private int _nextPatrolIndex;
    public PatrolState(AI_EnemyController enemyController, AI_StateMachine stateMachine) : base(enemyController, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
        _navMeshAgent = _enemyController.GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _enemyController.GetWalkSpeed();
        _patrolPoints = _enemyController.GetPatrolPoints();
    }
    public override void Run()
    {
        if (_navMeshAgent == null || _patrolPoints == null) return;

        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f)
        {
            _nextPatrolIndex = Random.Range(0, _patrolPoints.Length);
            _currentPatrolIndex = (_currentPatrolIndex + _nextPatrolIndex) % _patrolPoints.Length;
            _navMeshAgent.SetDestination(_patrolPoints[_currentPatrolIndex].position);
        }

        if (_enemyController.GetAIVision().CanSeePlayer)
        {
            _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Chase));
            return;
        }

        //criar aqui um random para sons e efeitos visuais, random de escolha e de tempo
    }
    public override void Exit()
    {
        base.Exit();
    }
}
