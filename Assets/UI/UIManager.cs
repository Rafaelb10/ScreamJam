using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string _levelGameName;

    [Header("UI Panels")]
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _deathPanel;

    [Header ("Music/Sound")]
    [SerializeField] private AudioSource _soundMachine;
    private bool _creditsOpen = false;
    private bool _paused = false;
    private bool _isDead = false;

    private void Awake()
    {
        if (_creditsPanel != null)
            _creditsPanel.SetActive(false);

        if (_pausePanel != null)
            _pausePanel.SetActive(false);

        if (_deathPanel != null)
            _deathPanel.SetActive(false);

        Time.timeScale = 1.0f;
    }
    #region MainMenu
    public void NewGame()
    {
        SceneManager.LoadScene(_levelGameName);
    }
    public void Surprise()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
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
    #endregion


    private void CloseAllUIs()
    {
        if (_creditsPanel != null)
            _creditsPanel.SetActive(false);

        if (_pausePanel != null)
            _pausePanel.SetActive(false);

        if (_deathPanel != null)
            _deathPanel.SetActive(false);

        _creditsOpen = false;
        _paused = false;

        Time.timeScale = 1.0f;
    }

    #region PauseMenu

    public void Paused()
    {
        if (_pausePanel == null) return;

        _pausePanel.SetActive(true);
        Time.timeScale = 0f;
        _paused = true;
        _soundMachine.Pause();
    }
    public void Resume()
    {
        if (_pausePanel == null) return;
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
        _paused = false;
        _soundMachine.UnPause();
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region DeathMenu

    
    public void ShowDeathUI()
    {
        if (_deathPanel == null)
        {
            Debug.LogWarning("Death panel is not assigned in UIManager.");
            return;
        }

        CloseAllUIs(); // Fecha outras UIs (só por segurança)
        _deathPanel.SetActive(true);
        _isDead = true;

        Time.timeScale = 0f; // pausa o jogo ao morrer
    }

    #endregion
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
        if (!_creditsOpen && !_isDead && Input.GetKeyDown(KeyCode.Escape))
        {
            if (_paused)
                Resume();
            else
                Paused();
        }
    }
}