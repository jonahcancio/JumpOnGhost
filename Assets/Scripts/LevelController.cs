using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    private GameObject playerObject;
    private GameObject levelMap;
    private Transform playerSpawn;

    public Transform UIPool;
    private GameObject pauseUI;
    private GameObject winUI;
    private GameObject timeUI;

    #region Singleton
    public static LevelController Instance;
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

    void Start()
    {
        Time.timeScale = 1f;

        playerObject = GameObject.FindGameObjectWithTag("Player");
        levelMap = GameObject.FindGameObjectWithTag("Map");
        playerSpawn = levelMap.transform.Find("PlayerSpawn");

        pauseUI = UIPool.Find("Pause UI").gameObject;
        winUI = UIPool.Find("Win UI").gameObject;
        timeUI = UIPool.Find("Time UI").gameObject;

        RespawnPlayer();
        ResetUIs();
    }

    public void RespawnPlayer()
    {
        playerObject.transform.position = playerSpawn.position;
        PlayerTracker tracker = playerObject.GetComponent<PlayerTracker>();
        tracker.ResetTracking();
    }

    public void ResetUIs()
    {
        pauseUI.SetActive(false);
        winUI.SetActive(false);
        timeUI.SetActive(true);
    }

    public void NextLevel()
    {
        Debug.Log("Loading next level");
        Instance = null;
        GhostPooler.Instance = null;
        TimeKeeper.Instance = null;

        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else
        {
            Debug.Log("LAST LEVEL REACHED");
        }
        
    }
}
