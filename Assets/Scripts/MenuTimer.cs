using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MenuTimer : MonoBehaviour
{
    [SerializeField] private string  _menuName;

    private void Start()
    {
        StartCoroutine(BackToMainMenu());
    }
    IEnumerator BackToMainMenu()
    {

        yield return new WaitForSeconds(13f);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(_menuName);
        
    }
}
