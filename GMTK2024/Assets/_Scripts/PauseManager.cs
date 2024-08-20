using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuCanvas;
    private MenuInpuActions _menuActions;
    private bool _isPaused;

    private void OnEnable() 
    {
        _isPaused = false;
        _menuActions = new MenuInpuActions();
        _pauseMenuCanvas.SetActive(false);

        _menuActions.Pause.Pause.performed += PauseGame;
        _menuActions.Enable();
    }

    private void OnDisable() 
    {
        _menuActions.Pause.Pause.performed -= PauseGame;


        _menuActions.Disable();
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        
        Time.timeScale = !_isPaused ? 0.0f : 1.0f;
        _pauseMenuCanvas.SetActive(!_isPaused ? true : false);
        _isPaused = !_isPaused;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        _isPaused = false;
        _pauseMenuCanvas.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
