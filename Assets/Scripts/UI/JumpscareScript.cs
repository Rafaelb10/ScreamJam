using UnityEngine;

public class JumpscareScript : MonoBehaviour
{
    private Animator _jesterController;
    [SerializeField] private GameObject _jumpscareUI;
    
    private void Awake()
    {
        _jesterController = GameObject.FindObjectOfType<Animator>().GetComponent<Animator>();
        
    }

    public void JumpscareSequence()
    {
        _jesterController.SetTrigger("JumpScare");
        _jumpscareUI.SetActive(true);
    }
}
