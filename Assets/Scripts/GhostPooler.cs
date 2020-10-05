using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPooler : MonoBehaviour
{

    public GameObject ghostPrefab;
    public GameObject playerObject;

    private Queue<GameObject> ghostQueue;
    public int maxGhostCount;

    #region Singleton
    public static GhostPooler Instance;
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
        ghostQueue = new Queue<GameObject>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }
    public void RespawnGhosts()
    {
        foreach (GameObject ghost in ghostQueue)
        {
            GhostController gController = ghost.GetComponent<GhostController>();
            gController.ResetLooping(null);
        }
    }

    public void SpawnNewGhost()
    {
        GameObject ghost = Instantiate(ghostPrefab, transform);
        ghostQueue.Enqueue(ghost);
        if (ghostQueue.Count > maxGhostCount)
        {
            GameObject deadGhost = ghostQueue.Dequeue();
            Destroy(deadGhost);
        }


        PlayerTracker tracker = playerObject.GetComponent<PlayerTracker>();
        tracker.StopTracking();
        GhostController gController = ghost.GetComponent<GhostController>();
        gController.ResetLooping(tracker.playerPositions);

    }
}
