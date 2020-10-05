using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeKeeper : MonoBehaviour
{
    
    
    [Header("Time Variables")]
    public float maxTime;
    public Text timeText;
    [SerializeField]
    private bool isTiming;

    private float timeLeft;
    private float lastWholeTime;


    //public GameObject playerObject;
    //public Transform playerSpawn;

    private bool isPaused;
    public GameObject pauseUI;

    [Header("Time Events")]
    public UnityEvent TimeEndEvent;
    public UnityEvent TimeStartEvent;

    #region Singleton
    public static TimeKeeper Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        TimeEndEvent.AddListener(LoopLevel);
        ResetTime();
    }

    public void ResetTime()
    {
        isTiming = true;
        timeLeft = maxTime;
        lastWholeTime = Mathf.Ceil(timeLeft);
        timeText.text = lastWholeTime + "s";

        TimeStartEvent.Invoke();
    }

    public void EndTime()
    {
        isTiming = false;
        TimeEndEvent.Invoke();
    }

    public void OnLoop(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (!UIManager.Instance.isWon)
            {
                if (UIManager.Instance.isPaused)
                {
                    UIManager.Instance.UnpauseGame();
                }
                    LoopLevel();                
            } else if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            {        
                LevelController.Instance.NextLevel();
            } else
            {
                Debug.Log("Last Level Reached");
            }
            
        }
    }

    void Update()
    {
        if (isTiming)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < lastWholeTime - 1)
            {
                lastWholeTime = Mathf.Ceil(timeLeft);
                timeText.text = lastWholeTime + "s";
            }
            if (timeLeft < 0)
            {
                EndTime();          
            }
        }

    }

    public void LoopLevel()
    {
        GhostPooler.Instance.SpawnNewGhost();
        LevelController.Instance.RespawnPlayer();
        LevelController.Instance.ResetUIs();
        GhostPooler.Instance.RespawnGhosts();

        isTiming = true;
        ResetTime();
    }

    //public void RespawnPlayer()
    //{
    //    playerObject.transform.position = playerSpawn.position;
    //    PlayerTracker tracker = playerObject.GetComponent<PlayerTracker>();
    //    tracker.ResetTracking();
    //}

}
