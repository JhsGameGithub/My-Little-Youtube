using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CatItem : Item
{
    public CatState state;
    public bool rightMove;
    public Animator animator;
    public bool canSound; 
    public enum CatState
    {
        SLEEP, //잠자기
        WANDER, //배회하기 
        SIZE,
    }
    public void OnEnable()
    {     
    }
    public void Start()
    {
        transform.parent.GetComponent<CharacterItem>().AddItem(this);
    }
    public override void ChangeSetting()//화면이 바뀌면 다른 동작을 할수도있음 
    {
        if(animator==null) 
            animator = GetComponent<Animator>();
        state = (CatState)Random.Range((int)CatState.SLEEP, (int)CatState.SIZE);
        canSound = true;
        switch (state)
        {
            case CatState.SLEEP:
                if (place == Place.RESTPLACE)
                {
                    gameObject.transform.localPosition = new Vector2(460f, -90f);
                }
                else
                {
                    gameObject.transform.localPosition = new Vector2(-130f, -320f);
                }
                animator.SetBool("Sleep", true);
                GetComponent<SpriteRenderer>().flipX = false;
                break;
            case CatState.WANDER:
                if (place == Place.RESTPLACE)
                {
                    gameObject.transform.localPosition = new Vector2(440, -500f);
                }
                else
                {
                    gameObject.transform.localPosition = new Vector2(440, -550f);
                }
                animator.SetBool("Sleep", false);
                MoveChange();
                break;
            default:
                break;
        }
    }
    public void MoveChange()
    {
        if(rightMove==false)
        {
            if (transform.localPosition.x < -440)
            {
                rightMove = true;  
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            if(transform.localPosition.x> 440)
            {
                rightMove = false;
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
    public override void UpdateItem() //아이템 동작
    {
       
    }
    public void AnimationAction()
    {
      //  if()
        switch (state)
        {
            case CatState.SLEEP:
                //자고있을떄 
                break;
            case CatState.WANDER:
                //돌아다닐떄
                float speed = rightMove == false ? -1f : 1f;

                animator.SetFloat("Speed", Clock.Instance.state_machine.GetSpeed());
                gameObject.transform.Translate(Clock.Instance.state_machine.GetSpeed() *
                     speed
                    * Time.deltaTime, 0, 0);
                MoveChange();


                break;
            default:
                break;
        }
       

    }
    public void Update()
    {
        AnimationAction();
        ClickCat();
    }
    public void ClickCat()
    {
        if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0) )&& state == CatState.WANDER && canSound)
        {

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if (hit.collider != null)
            {
                int soundNumber = Random.Range(1, 4);
                AudioManager.Sound.Play("SE/catSound"+ soundNumber.ToString(), E_SOUND.SE);
                StartCoroutine(CoolTime());
            }
        }
       
    }
    IEnumerator CoolTime()
    {
        canSound = false;
        yield return new WaitForSeconds(3.0f);
        canSound = true;
    }
}