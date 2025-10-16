using UnityEngine;

public class JumpscareScript : MonoBehaviour
{
    private Animator _jesterController;
    [SerializeField] private GameObject _jumpscareUI;
    
    private void Awake()
    {
        _jesterController = GameObject.FindObjectOfType<Animator>().GetComponent<Animator>();
        
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpscareSequence();
        }
    }

    public void JumpscareSequence()
    {
        _jesterController.SetBool("isAttacking", true);
        _jumpscareUI.SetActive(true);
    }
}
