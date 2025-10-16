using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SearchState : AI_State
{
    private float _searchTimer;
    private float _searchDuration = 3f; 
    private float _stoppingDistance = 2f;
    private float _distanceEnemyToPlayer;

    public SearchState(AI_EnemyController enemyController, AI_StateMachine stateMachine) : base(enemyController, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Search State");
        _enemyController.GetAnimationHandler().SearchingAnimationOn();
        _searchTimer = 0f;

        _enemyController.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(_enemyController.LastestPlayerPos);
        //colocar aqui efeito de pressão visual ?
    }

    public override void Run()
    {
        _distanceEnemyToPlayer = Vector3.Distance(_enemyController.transform.position, _enemyController.GetLastestPlayerPos());

        if (_enemyController.GetAIVision().CanSeePlayer)
        {
            _enemyController.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
            _enemyController.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            Debug.Log("Player spotted during search, switching to Chase State");
            _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Chase));
            return;
        }

        if (_distanceEnemyToPlayer < _stoppingDistance)
        {
            _searchTimer += Time.deltaTime;
            Debug.Log("Searching... " + _searchTimer.ToString("F2") + "s");
            if (_searchTimer >= _searchDuration)
            {
                _enemyController.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
                _enemyController.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                Debug.Log("Search complete, going back to Patrol");
                _stateMachine.ChangeState(_enemyController.GetState(AI_EnemyController.StateType.Patrol));
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Search State");
        _enemyController.GetAnimationHandler().SearchingAnimationOff();
    }

    IEnumerator Search()
    {
        _enemyController.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(_enemyController.GetLastestPlayerPos());

        yield return new WaitForSeconds(3);

        _enemyController.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        _enemyController.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
    }
}
