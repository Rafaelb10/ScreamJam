using UnityEngine;
using UnityEngine.AI;

public class AI_FootstepAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstepClips;

    [Header("Timing")]
    [SerializeField] private float stepInterval = 0.6f;

    private float _stepTimer;

    private void Update()
    {
        // Only play when moving
        if (agent.velocity.magnitude > 0.2f && agent.remainingDistance > agent.stoppingDistance)
        {
            _stepTimer += Time.deltaTime;
            if (_stepTimer >= stepInterval)
            {
                PlayFootstep();
                _stepTimer = 0f;
            }
        }
    }

    private void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(clip);
    }
}
