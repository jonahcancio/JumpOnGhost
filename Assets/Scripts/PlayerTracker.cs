using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerTracker : MonoBehaviour
{
    public bool isTracking;
    public List<Vector3> playerPositions;

    public float trackInterval;
    private float timeLastTracked;
    public float maxTrackTime;
    private float timeTrackStarted;


    public void ResetTracking()
    {
        playerPositions = new List<Vector3>();
        isTracking = true;
        timeLastTracked = 0;
        timeTrackStarted = Time.time;
    }
    
    void Update()
    {
        if (isTracking)
        {
            if (Time.time - timeLastTracked > trackInterval)
            {
                timeLastTracked = Time.time;
                playerPositions.Add(transform.position);
            }
            if (Time.time - timeTrackStarted > maxTrackTime)
            {
                StopTracking();
            }
        }
    }

    public void StopTracking()
    {
        isTracking = false;
        // Add last known location of player to the end of the list
        playerPositions.Add(transform.position);
    }
}
