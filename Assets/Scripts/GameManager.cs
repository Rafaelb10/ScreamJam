using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    //É triggered quando o enemy dá collide. Ativa jumpscare e troca de cena
    public IEnumerator GameEnded()
    {
        Debug.Log("Game Over");
        //Jumpscare
        GameObject.FindObjectOfType<JumpscareScript>().JumpscareSequence();
        yield return new WaitForSeconds(0.5f);
        //Trocar de cena para Game Over
        
        yield return null;
    }
}
