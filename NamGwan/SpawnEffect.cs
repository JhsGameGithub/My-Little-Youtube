using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    Animator animator;
    public GameObject allow = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        AudioManager.Sound.OverridePlay("SE/GetCoin", E_SOUND.SE);
    }
    public void SetAllow(GameObject obj)
    {
        allow = obj;
    }
    public void AllowPos()
    {
        if (allow != null)
        {
            Vector3 pos = allow.transform.position;
            pos.y += 1f;
            gameObject.transform.position = pos;
        }
    }
    public void Action()
    {
        animator.SetFloat("Speed", Clock.Instance.state_machine.GetSpeed()*2);
    }
    public void DustEnd()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.PlusEffect") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Destroy(this.gameObject);
        }
    }
    public void LateUpdate()
    {
        Action();
        AllowPos();
        DustEnd();
    }
}
