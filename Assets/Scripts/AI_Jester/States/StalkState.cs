using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StalkState : AI_State
{
    private bool _canWalkToPlayer;
    private NavMeshAgent _navMeshAgent;
    public StalkState(AI_EnemyController enemyController, AI_StateMachine stateMachine) : base(enemyController, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Stalk State");
        _canWalkToPlayer = true;
        _navMeshAgent = _enemyController.GetComponent<NavMeshAgent>();
        _enemyController.GetAnimationHandler().StalkingAnimationOn();
    }
    public override void Run()
    {
        if(_enemyController == null || _enemyController.GetPlayer() == null) return;

        if (_canWalkToPlayer)
        {
            _navMeshAgent.speed = _enemyController.GetStalkSpeed();
            _navMeshAgent.SetDestination(_enemyController.GetPlayer().position);
            
        }

        _enemyController.StartCoroutine(OptionDelay(2.5f));
        if (_canWalkToPlayer) return;

        if(_canWalkToPlayer == false)
        {
            float randomOption = Random.Range(0f, 1f);

            if(randomOption <= 0.5f)
            {
                float randomTime = Random.Range(1.5f, 3f);
                _enemyController.StartCoroutine(LookPlayerSystem(randomTime));
            }
            else if (randomOption > 0.5f)
            {
                _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Chase));
            }
        }
    }
    public override void Exit()
    {
        _navMeshAgent.speed = _enemyController.GetWalkSpeed();
        _enemyController.GetAnimationHandler().StalkingAnimationOff();
    }
    private void LookAtPlayer()
    {
        Vector3 direction = (_enemyController.GetPlayer().position - _enemyController.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        _enemyController.transform.rotation = Quaternion.Slerp(_enemyController.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    IEnumerator LookPlayerSystem(float randomTime)
    {
        LookAtPlayer();
        yield return new WaitForSeconds(randomTime);

        float randomOption = Random.Range(0f, 1f);

        if(randomOption <= 0.5f)
        {
            _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Flee));
        }
        else
        {
            _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Attack));
        }

    }

    IEnumerator OptionDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        _canWalkToPlayer = false;
    }
}
