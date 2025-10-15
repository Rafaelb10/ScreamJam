using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string _levelGameName;

    [Header("UI Panels")]
    [SerializeField] private GameObject _creditsPanel;

    private bool _creditsOpen = false;

    private void Awake()
    {
        if (_creditsPanel != null)
            _creditsPanel.SetActive(false);
    }
    public void NewGame()
    {
        SceneManager.LoadScene(_levelGameName);
    }

    public void Credits()
    {
        CloseAllUIs();

        if (_creditsPanel != null)
        {
            _creditsPanel.SetActive(true);
            _creditsOpen = true;
        }  
        else
            {
                Debug.LogWarning("Credits panel is not assigned in UIManager.");
            }
        
    }

    private void CloseAllUIs()
    {
        if (_creditsPanel != null)
            _creditsPanel.SetActive(false);

        _creditsOpen = false;
    }
    public void Surprise()
    {

    }
    public void QuitGame()
    {
      Application.Quit();
    }
    private void CloseUIs()
    {
        if (_creditsPanel != null)
            _creditsPanel.SetActive(false);

        _creditsOpen = false;
    }

    private void Update()
    {
        // Este método corre a cada frame, por isso é aqui que se deve verificar input
        if (_creditsOpen)
        {
            // Se o jogador clicar com o rato ou carregar em Escape
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
            {
                CloseAllUIs();
            }
        }
    }
}

