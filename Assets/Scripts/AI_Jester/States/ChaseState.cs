using UnityEngine;

public class ChaseState : AI_State
{
    private float _maxDistanceToPlayer = 8f;

    public ChaseState(AI_EnemyController enemyController, AI_StateMachine stateMachine) : base(enemyController, stateMachine)
    {
        _enemyController = enemyController;
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        Debug.Log("Entering Chase State");
        _enemyController.GetAnimationHandler().ChasingAnimationOn();
        _enemyController.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = _enemyController.GetChaseSpeed();
    }

    public override void Run()
    {
        if (_enemyController == null || _enemyController.GetPlayer() == null) return;

        _enemyController.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(_enemyController.GetPlayer().position);

        Vector3 dirToPlayer = (_enemyController.GetPlayer().position - _enemyController.transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(_enemyController.transform.position, _enemyController.GetPlayer().position);

        if (Physics.Raycast(_enemyController.transform.position, dirToPlayer, out RaycastHit hit))
        {
            if(hit.transform != null && hit.collider.gameObject == _enemyController.GetPlayer().gameObject)
            {
                Debug.Log("Player in sight during chase");
                //efeito de pressão visual ?

                if (distanceToPlayer <= _maxDistanceToPlayer)
                {
                    float randomState = Random.Range(0f, 1f);
                    Debug.Log("Random State Value: " + randomState);

                    if (randomState <= 0.3f)
                    {
                        _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Flee));
                    }
                    else if(randomState <= 0.7f && randomState > 0.3f)
                    {
                        _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Attack));
                    }
                    else if(randomState > 0.7f)
                    {
                        _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Stalk));
                    }
                }                
                return;
            }
            else
            {
                _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Search));
            }
        }

        if (Vector3.Distance(_enemyController.transform.position, _enemyController.GetPlayer().position) <= _enemyController.GetViewDistance())
        {
            _stateMachine.ChangeState(_enemyController .GetState(AI_EnemyController.StateType.Search));
        }
    }
    public override void Exit()
    {
        _enemyController.GetAnimationHandler().ChasingAnimationOff();
    }
}
