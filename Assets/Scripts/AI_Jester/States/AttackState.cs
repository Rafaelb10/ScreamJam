using UnityEngine;

public class AttackState : AI_State
{
    float _timeToAttack;
    float _maxTimeToAttack = 3f;
    public AttackState(AI_EnemyController enemyController, AI_StateMachine stateMachine) : base(enemyController, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Attack State");
        _timeToAttack = 0f;
    }
    public override void Run()
    {
        if (_enemyController == null || _enemyController.GetPlayer() == null) return;

        _enemyController.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(_enemyController.GetPlayer().position);

        _timeToAttack += Time.deltaTime;

        if(_timeToAttack >= _maxTimeToAttack)
        {
            _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Chase));
        }

    }
    public override void Exit()
    {

    }

}
