using UnityEngine;

public class AI_Vision
{
    Transform _enemy;
    Transform _player;
    float _angleFov;
    float _viewDistance;

    private bool _canSeePlayer;
    public AI_Vision(Transform enemy, Transform player, float angleFov, float viewDistance)
    {
        _enemy = enemy;
        _player = player;
        _angleFov = angleFov;
        _viewDistance = viewDistance;
    }

    public bool CanSeePlayer { get => _canSeePlayer; set => _canSeePlayer = value; }

    public void UpdateVision()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(_enemy.position, _enemy.forward);

        if (_enemy == null || _player == null || _angleFov == 0 || _viewDistance == 0) return; 

        Vector3 dirToPlayer = (_player.position - _enemy.position).normalized;
        
        if (Vector3.Distance(_enemy.position,_player.position) < _viewDistance)
        {
            float angleToPlayer = Vector3.Angle(_enemy.forward, dirToPlayer);
            if(angleToPlayer < _angleFov / 2)
            {
                
                if (Physics.Raycast(_enemy.position, dirToPlayer,out RaycastHit hit , _viewDistance))
                {
                    if (hit.transform == _player)
                    {
                        _canSeePlayer = true;
                        return;
                    }
                }
            }
        }
        _canSeePlayer = false;
    }
}
