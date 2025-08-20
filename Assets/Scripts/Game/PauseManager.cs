using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Health _playerHealth;

    private bool _paused;
    public bool Paused => _paused;
    private bool _ignorePauseInput;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PauseGame()
    {
        _paused = true;
        Time.timeScale = 0f;
        ScreenClickManager.Instance.MouseIsOverScreen = false;
        _pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        _paused = false;
        Time.timeScale = 1f;
        ScreenClickManager.Instance.MouseIsOverScreen = true;
        _pauseMenu.SetActive(false);
    }

    public void OnEsc()
    {
        if (_ignorePauseInput)
        {
            return;
        }

        if (_paused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void OnExitPressed()
    {
        _ignorePauseInput = true;
        ResumeGame();
        _playerHealth.Kill();
    }
}
