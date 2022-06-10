using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sedan : Car
{
    private bool captured;
    private GameObject capturer;

    StateMachine stateMachine;

    protected override void Start()
    {
        base.Start();

        stateMachine = new StateMachine(new SedanIdleState(this));
    }

    protected void Update()
    {
        stateMachine.Execute();
    }

    public bool Captured(GameObject police)
    {
        float distance = Vector2.Distance(transform.position, police.transform.position);
        if (distance <= catchDistance)
        {
            capturer = police;
            return captured = true;
        }  
        else
            return false;
    }

    public bool Captured()
    {
        return captured;
    }

    public void HideCar()
    {
        capturer.GetComponent<Police>().SetTarget(null);
        capturer = null;
        gameObject.SetActive(false);
    }

    public bool IsJailed()
    {
        return Vector2.Distance(transform.position, new Vector3(prison.transform.position.x, 0f, prison.transform.position.z)) <= jailedDistance;
    }
}
