using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class DoorCredits : MonoBehaviour
{
    [SerializeField] private string _levelGameName;

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            SceneManager.LoadScene(_levelGameName);
           
        }
    }

  
}
