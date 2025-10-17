using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupriseScript : MonoBehaviour
{
    [SerializeField] private AudioSource jumpScareSound;
    [SerializeField] private GameObject jumpScarePrefab;

    private bool canShowSuprise = true;
    public void RandomSuprise()
    {
        int supriseNum;
        supriseNum = Random.Range(0, 0);
        
        if(supriseNum == 0 && canShowSuprise)
        {
            jumpScarePrefab.SetActive(true);
            StartCoroutine(ShowJumpScare());
        }
       /* if(supriseNum == 1)
        {

        }
        if(supriseNum == 2)
        {

        }
        if(supriseNum == 3)
        {

        }*/
    }
    public IEnumerator ShowJumpScare()
    {
        canShowSuprise = false;
        //Jumpscare
        GameObject.FindObjectOfType<JumpscareScript>().JumpscareSequence();
        jumpScareSound.Play();
        yield return new WaitForSeconds(1.2f);
        GameObject.FindObjectOfType<JumpscareScript>().gameObject.SetActive(false);
        jumpScareSound.Stop();
        //Trocar de cena para Game Over
        yield return new WaitForSeconds(2f);
        canShowSuprise = true;
        yield return null;
    }
}
