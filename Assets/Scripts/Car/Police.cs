using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Car
{
    StateMachine stateMachine;

    private bool patrolling;

    protected override void Start()
    {
        base.Start();

        MoveToRandom();

        stateMachine = new StateMachine(new PoliceIdleState(this));
    }

    protected void Update()
    {
        Debug.Log(direction);

        stateMachine.Execute();
    }

    public bool Chase(Sedan target)
    {
        MoveTo(target.gameObject);
        return target.Captured(gameObject);
    }

    public bool GetPatrolling()
    {
        return patrolling;
    }

    public void Patrolling(bool value)
    {
        patrolling = value;
    }

    public bool FindClosestTarget()
    {
        GameObject targetSedan = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject sedan in fieldOfView.GetNearObstacles())
        {
            if(sedan.tag == sedanTag)
            {
                float distance = Vector2.Distance(sedan.transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetSedan = sedan;
                }
            }
        }
        target = targetSedan;

        return target != null ? true : false;
    }
}
