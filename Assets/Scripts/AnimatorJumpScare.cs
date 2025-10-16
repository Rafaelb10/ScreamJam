using UnityEngine;

public class AnimatorJumpScare : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(GameManager.Instance.IsGameOver)
        {
            PlayJumpScare();
        }
    }
    public void PlayJumpScare()
    {
        _animator.SetTrigger("JumpScare");
    }
}
