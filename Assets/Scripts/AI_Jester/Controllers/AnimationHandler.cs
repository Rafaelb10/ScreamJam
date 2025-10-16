using UnityEngine;

public class AnimationHandler
{
    private AI_EnemyController _enemyController;
    private Animator _animator;
    public AnimationHandler(AI_EnemyController enemyController)
    {
        _enemyController = enemyController;

        _animator = enemyController.GetAnimator();
    }

    public void FleeAnimationOn()
    {
        _animator.SetBool("isFleeing", true);
    }
    public void FleeAnimationOff()
    {
        _animator.SetBool("isFleeing", false);
    }
    public void SearchingAnimationOn()
    {
        _animator.SetBool("isSearching", true);
    }
    public void SearchingAnimationOff()
    {
        _animator.SetBool("isSearching", false);
    }
    public void StalkingAnimationOn()
    {
        _animator.SetBool("isStalking", true);
    }
    public void StalkingAnimationOff()
    {
        _animator.SetBool("isStalking", false);
    }
}
