using System.Collections.Generic;
using UnityEngine;

public class AI_EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Transform _player;

    private Collider _collider;
    private Animator _animator;

    private AI_StateMachine _stateMachine;
    private AI_Vision _aiVision;
    private AnimationHandler _animationHandler;

    private float _angleFov = 90f;
    private float _viewDistance = 20f;
    private float _changeStateDistance = 10f;

    private float _stalkSpeed = 1;
    private float _walkSpeed = 4;
    private float _chaseSpeed = 6;
    private float _runSpeed = 8;

    private Vector3 _lastestPlayerPos;
    
    private Dictionary<StateType, AI_State> _states = new Dictionary<StateType,AI_State>();

    public Vector3 LastestPlayerPos { get => _lastestPlayerPos; set => _lastestPlayerPos = value; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _stateMachine = new AI_StateMachine();
        _aiVision = new AI_Vision(transform, _player, _angleFov, _viewDistance);
        _animationHandler = new AnimationHandler(this);


        _states[StateType.Idle] = new IdleState(this, _stateMachine);
        _states[StateType.Patrol] = new PatrolState(this, _stateMachine);
        _states[StateType.Chase] = new ChaseState(this, _stateMachine);
        _states[StateType.Search] = new SearchState(this, _stateMachine);
        _states[StateType.Attack] = new AttackState(this, _stateMachine);
        _states[StateType.Flee] = new FleeState(this, _stateMachine);
        _states[StateType.Stalk] = new StalkState(this, _stateMachine);

        _stateMachine.Initialize(_states[StateType.Idle]);
    }

    private void Update()
    {
        _stateMachine.RunState();
        _aiVision.UpdateVision();

        if(_aiVision.CanSeePlayer)
        {
            _lastestPlayerPos = _player.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.GameEnded());
        }
    }
    public AI_State GetState(StateType stateType) => _states[stateType];
    public Transform[] GetPatrolPoints() => _patrolPoints;
    public Transform GetPlayer() => _player;
    public Vector3 GetLastestPlayerPos() => _lastestPlayerPos;
    public float GetViewDistance() => _viewDistance;
    public float GetChangeStateDistance() => _changeStateDistance;
    public AI_Vision GetAIVision() => _aiVision;
    public AnimationHandler GetAnimationHandler() => _animationHandler;
    public float GetWalkSpeed() => _walkSpeed;
    public float GetRunSpeed() => _runSpeed;
    public float GetStalkSpeed() => _stalkSpeed;
    public Animator GetAnimator() => _animator;
    public enum StateType
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Search,
        Flee,
        Stalk
    }
}
