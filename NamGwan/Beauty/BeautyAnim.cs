using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeautyAnim : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Action()
    {
        animator.SetFloat("Speed", Clock.Instance.state_machine.GetSpeed());
    }
    private void LateUpdate()
    {
        Action();
    }
}