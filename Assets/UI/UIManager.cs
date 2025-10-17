using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string _levelGameName, _MenuName;

    [Header("UI Panels")]
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _deathPanel;
    [SerializeField] private GameObject _aim;

    [Header ("Music/Sound")]
    [SerializeField] private AudioSource _soundMachine;
    private bool _creditsOpen = false;
    private bool _paused = false;
    private bool _isDead = false;

    [SerializeField] private SupriseScript _script;
    public string MenuName { get => _MenuName; set => _MenuName = value; }

    private void Awake()
    {
        if (_creditsPanel != null)
            _creditsPanel.SetActive(false);

        if (_pausePanel != null)
            _pausePanel.SetActive(false);

        if (_deathPanel != null)
            _deathPanel.SetActive(false);

        if (_aim != null)
            _aim.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1.0f;
    }
    #region MainMenu
    public void NewGame()
    {
        if (_soundMachine != null)
        {
            _soundMachine.UnPause();     // Caso tenha sido pausado
            _soundMachine.volume = 1f;   // Garante volume cheio
            if (!_soundMachine.isPlaying)
                _soundMachine.Play();    // Recomeça se necessário
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(_levelGameName);
    }

    public void Surprise()
    {
        _script.RandomSuprise();
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.LogWarning("Quit");
    }
    public void ReturnMainMenu()
    {
        if (_soundMachine != null)
        {
            _soundMachine.UnPause();
            //_soundMachine.volume = 1f;
            _soundMachine.Play();
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(_MenuName);
    }

    public void Retry()
    {
        // Despausa o som antes de trocar de cena
        if (_soundMachine != null)
        {
            _soundMachine.UnPause();
            //_soundMachine.volume = 1f;
            _soundMachine.Play();
        }

        // Desbloqueia cursor se necessário
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Reinicia a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

        if(_aim != null)
            _aim.SetActive(false);

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
        Cursor.lockState = CursorLockMode.None;
        _aim.SetActive(false);
    }
    public void Resume()
    {
        if (_pausePanel == null) return;
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
        _paused = false;
        _soundMachine.UnPause();
        Cursor.lockState = CursorLockMode.Locked;
        _aim.SetActive(true);
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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