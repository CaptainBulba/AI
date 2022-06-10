using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public LayerMask obstacleMask;

    public bool seeSomething;

    private Car carScript;

    public List<GameObject> detectedObjects;

    private string policeTag = "Police";
    private string sedanTag = "Sedan";
    private string obstacleTag = "Obstacle";
    private string wallTag = "Wall";

    void Update()
    {
        FieldOfViewCheck();
    }

    public void FieldOfViewCheck()
    {
        detectedObjects.Clear();

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius);

        if (rangeChecks.Length != 0)
        {
            foreach (Collider visibleObject in rangeChecks)
            {
                if(containVisibleTag(visibleObject.gameObject))
                {
                    Transform target = visibleObject.transform;
                    Vector3 directionToTarget = (target.position - transform.position).normalized;

                    if(visibleObject.gameObject.tag == obstacleTag || visibleObject.gameObject.tag == wallTag)
                        AddObject(visibleObject.gameObject); 

                    if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                    {
                        float distanceToTarget = Vector3.Distance(transform.position, target.position);

                        if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                            AddObject(visibleObject.gameObject);
                    }
                }
            }
        }
    }

    private void AddObject(GameObject visibleObject)
    {
        if (!detectedObjects.Contains(visibleObject))
            detectedObjects.Add(visibleObject);
    }

    private bool containVisibleTag(GameObject visibleObject)
    {
       return visibleObject.gameObject != gameObject && (visibleObject.tag == policeTag || visibleObject.tag == sedanTag 
            || visibleObject.tag == obstacleTag || visibleObject.tag == wallTag);
    }

    public List<GameObject> GetNearObstacles()
    { 
        detectedObjects.Sort(SortByDistance);
        return detectedObjects;
    }

    private int SortByDistance(GameObject a, GameObject b)
    {
        float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }
}