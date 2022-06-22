using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidItem : Item
{
    public Animator animator;

    public const int SkillTime = 30; // 30분마다 발동 
    public const int Per = 30; // 30퍼 확률로 발동 
    public int coolTime = 0; //시간 
    public void Start()
    {
        animator = GetComponent<Animator>(); 
        transform.parent.GetComponent<CharacterItem>().AddItem(this);
    }
    public override void ChangeSetting()
    {
        coolTime = 0;
    }

    public override void UpdateItem()
    {
        HealSkill();
    }
    public void HealSkill()
    {
        coolTime++;
        if (coolTime >= 30)
        {
            coolTime = 0;

            if(Per>= Random.Range(0,101))
            {
                DatabaseManager.Player.status.HP = 5;
            }
        }
    }

    public void Update()
    {
        AnimationAction();
    }
    public void AnimationAction()
    {
        animator.SetFloat("Speed", Clock.Instance.state_machine.GetSpeed());
    }
}
