using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GunEvent : SnailEvent 
{
    public GameObject gun;
    public GameObject bullet;
    public List<GameObject> snail;
    public bool shoot; 
    private void Start()
    {
        gun = Instantiate(Resources.Load("Prefabs/Snail/SnailGun"), new Vector3(0, 0, 0), Quaternion.Euler(0,180,0) )as GameObject;
        bullet = Instantiate(Resources.Load("Prefabs/Snail/SnailBullet"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        NewGame();
    }
    public override void NewGame()
    {
        base.NewGame();
        GetSnail();
    }
    public void GetSnail()
    {
        snail = GetComponent<SnailGame>().snailObject;
    }
    public override void Initalize()
    {
        shoot = false;
        bullet.SetActive(false);
        gun.SetActive(false);
    }

    public override void ActionEvent()
    {
        if(shoot ==false) //아직 이벤트 발생하지 않음 
        {
            if(Condition() == true) //조건 통과 
            {

                shoot = true; 
            }
        }
    }
    public override bool Condition()
    {
        for(int i=0; i< snail.Count;i++)
        {
            if(snail[i].transform.localPosition.x>250f)
            {
                Reload(line[i].transform,snail[i]);
              
                
                return true;
            }
        }
       
        return false;
    }
    public void Reload(Transform parent, GameObject target)
    {
        AudioManager.Sound.Play("SE/gunReady", E_SOUND.SE);
        bullet.transform.SetParent(parent);
        gun.transform.SetParent(parent);
        gun.SetActive(true);
        gun.transform.localScale = new Vector3(1f, 1f, 1f);
        gun.transform.localPosition = new Vector2(600, 0);
        gun.transform.DOLocalMoveX(360, 1f).OnComplete(() => { Shoot(target); });

    }
    public void Shoot(GameObject target)
    {
        AudioManager.Sound.Play("SE/gunFire", E_SOUND.SE);
        target.GetComponent<Snail>().DieSnail();

        bullet.SetActive(true);
        bullet.transform.localScale = new Vector3(1f, 1f, 1f);
        bullet.transform.localPosition = new Vector2(200, 20);
        bullet.transform.DOLocalMoveX(-350, 1f).SetEase(Ease.OutQuart);
    }
}


