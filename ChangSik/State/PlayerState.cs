using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour, IState
{
    public float fatigue_time;

    public Animator player_animator;

    protected UIView ui_view;

    protected float check_time;

    // Start is called before the first frame update
    void Start()
    {
        check_time = 0.0f;
        ui_view = GetComponent<UIView>();
    }

    public virtual void StateEnter()
    {
        check_time = 0.0f;
        ui_view.Show();
    }

    public virtual void StateUpdate()
    {

    }

    public virtual void StateExit()
    {
        ui_view.Hide();
    }

    public void SetAnimSpeed(float _speed)
    {
        if (player_animator == null)
            return;

        player_animator.speed = _speed;
    }
}
