using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public bool isLooping;

    public List<Vector3> ghostPositions;

    public float trackInterval;
    private float timeLastTracked;

    public int posIndex;

    private void Start()
    {
        
    }


    public void ResetLooping(List<Vector3> positions)
    {
        if (positions != null)
        {
            ghostPositions = positions;
        }        
        isLooping = true;
        timeLastTracked = Time.time;
        posIndex = 0;
    }

    public void StopLooping()
    {
        isLooping = false;
    }

    void Update()
    {
        if (isLooping)
        {
            float lerpValue = (Time.time - timeLastTracked) / trackInterval;

            if (lerpValue > 1)
            {
                timeLastTracked = Time.time;
                posIndex++;
                lerpValue = 0;
            }

            if (posIndex < ghostPositions.Count - 1)
            {
                transform.position = Vector3.Lerp(ghostPositions[posIndex], ghostPositions[posIndex + 1], lerpValue);
                          
            } else
            {
                transform.position = ghostPositions[posIndex];
                StopLooping();
            }            
        }                 
    }
}
