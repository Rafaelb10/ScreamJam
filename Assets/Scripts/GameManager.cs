using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool isGameOver = false;
    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private AudioSource jumpScareSound;
    [SerializeField] private GameObject jester, audiosources;

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

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
        Time.timeScale = 1;
        DeathScreen.SetActive(false);
        jester.SetActive(true);
        audiosources.SetActive(true);
    }
    
    //É triggered quando o enemy dá collide. Ativa jumpscare e troca de cena
    public IEnumerator GameEnded()
    {
        isGameOver = true;
        Debug.Log("Game Over");
        jester.SetActive(false);
        audiosources.SetActive(false);
        //Jumpscare
        GameObject.FindObjectOfType<JumpscareScript>().JumpscareSequence();
        jumpScareSound.Play();
        yield return new WaitForSeconds(1.2f);
        GameObject.FindObjectOfType<JumpscareScript>().gameObject.SetActive(false);
        jumpScareSound.Stop();
        DeathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        //Trocar de cena para Game Over
        
        yield return null;
    }
}
