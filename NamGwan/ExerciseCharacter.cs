using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class ExerciseCharacter : MonoBehaviour
{
    public GameObject player;
    public Vector3 initPos;
    public float spawnWait;
    public const float MaxWait = 1f;
    private void Start()
    {
        player = this.gameObject;
        initPos = player.transform.position;
        initPos.y = -1.5f;
        spawnWait = MaxWait;
    }

    private void LateUpdate()
    {
        MoveCharacter();
    }
    void MoveCharacter()
    {
        if(player.transform.position.x >=4 )
        {
            player.transform.position = initPos;
        }
        player.transform.position += (Vector3.right * Clock.Instance.state_machine.GetSpeed() * Time.deltaTime);
       
        if(spawnWait<= 0)
        {
            SpawnDust();
        }
        else
        {
            spawnWait -= Time.deltaTime * Clock.Instance.state_machine.GetSpeed();
        }
    }

    void SpawnDust()
    {
        GameObject dust = Instantiate(Resources.Load("Prefabs/Exercise/dustObject"), gameObject.transform.localPosition, Quaternion.identity) as GameObject;
        dust.transform.SetParent(GameObject.Find("InGameCanvas").transform.Find("HealthPlayerPage").transform, false);
        dust.GetComponent<SpriteRenderer>().sortingOrder = 30;
        AudioManager.Sound.OverridePlay("SE/footstep", E_SOUND.SE);
        spawnWait = MaxWait;
    }
}
