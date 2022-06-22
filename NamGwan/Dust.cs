using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
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
    public void DustEnd()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.dust")&& animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Destroy(this.gameObject);
        }
    }
    public void LateUpdate()
    {
        Action();
        DustEnd();
    }
}
