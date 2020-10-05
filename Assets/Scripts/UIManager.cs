using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public bool isPaused;
    public bool isWon;

    [Header("UI References")]
    public GameObject pauseUI;
    public GameObject winUI;

    #region Singleton
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        isWon = false;
        pauseUI = transform.Find("Pause UI").gameObject;
        winUI = transform.Find("Win UI").gameObject;
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !isWon)
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }

        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }

    public void OnWin()
    {
        winUI.SetActive(true);
        isWon = true;
        Time.timeScale = 0f;
    }

}
