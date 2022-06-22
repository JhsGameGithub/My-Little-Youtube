using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private PlayerState current;
    private PlayerState previous;

    private GlobalState global;

    private float speed = 0.0f;

    public void StateUpdate()
    {
        if (global == null)
            global = new GlobalState();

        global.StateUpdate();

        if (current != null)
            current.StateUpdate();
    }

    public void ChangeState(string name)
    {
        var page = GameObject.Find("InGameCanvas").transform.Find(name).GetComponent<PlayerState>();

        if (page != null)
        {
            if (current != null)
            {
                previous = current;
                current.StateExit();
            }


            current = page;
            current.SetAnimSpeed(speed);
            current.StateEnter();
        }
    }

    public PlayerState GetCurrentState()
    {
        return current;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;

        if (current != null)
            current.SetAnimSpeed(speed);
    }

    //남관
    public float GetSpeed()
    {
        return speed;
    }
}
