using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private int pointIndex;
    public Transform moveTrail;
    private List<Transform> movePoints;
    public float maxSpeed;
    public float distanceThreshold;

    public Vector3 originPosition;
    
    void Start()
    {
        movePoints = new List<Transform>();
        originPosition = transform.position;
        foreach(Transform child in moveTrail)
        {
            movePoints.Add(child);
        }
        pointIndex = 0;
    }

    void Update()
    {
        Vector3 nextPoint = movePoints[pointIndex].position;

        float moveSpeed = maxSpeed;
        //float moveSpeed = CalculateMoveSpeed();

        Vector3 moveDirection = Vector3.Normalize(nextPoint - transform.position);

        transform.Translate(moveSpeed * moveDirection * Time.deltaTime);

        float ownDistance = Vector3.Distance(nextPoint, transform.position);
        if (ownDistance < distanceThreshold)
        {
            //Debug.Log("Next Point!");
            pointIndex = ++pointIndex % movePoints.Count;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Collide");
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }

    public void ResetPlatform()
    {
        transform.position = originPosition;
        pointIndex = 0;
    }
    //float CalculateMoveSpeed()
    //{
    //    Vector3 nextPoint = movePoints[pointIndex].position;
    //    Vector3 prevPoint;
    //    if (pointIndex == 0)
    //    {
    //        prevPoint = movePoints[movePoints.Count - 1].position;
    //    }
    //    else
    //    {
    //        prevPoint = movePoints[pointIndex - 1].position;
    //    }
    //    Vector3 halfPoint = (nextPoint - prevPoint) / 2f;

    //    float halfDistance = Vector3.Distance(nextPoint, halfPoint);
    //    float ownDistance = Vector3.Distance(nextPoint, transform.position);

    //    float speedIncrement = (maxSpeed / 2) * Mathf.Min(ownDistance / halfDistance, 1);

    //    return maxSpeed / 2 + speedIncrement;
    //}
}
