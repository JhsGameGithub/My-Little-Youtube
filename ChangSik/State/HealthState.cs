using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthState : PlayerState
{
    public float fatigue_value;

    public override void StateEnter()
    {
        base.StateEnter();

        // 시간 재생
        Clock.Instance.TimeNormalizer();
    }

    public override void StateUpdate()
    {
        check_time++;
        if (fatigue_time < check_time)
        {
            check_time = 0.0f;
             DatabaseManager.Player.status.HP = -(int)fatigue_value;
             DatabaseManager.Player.status.Health += DatabaseManager.SearchData("운동기구", DatabaseManager.Instance.my_furniture_list).skill[0].ability_value;
            SpawnEffect();
        }
    }
    public void SpawnEffect()
    {

        GameObject effect = Instantiate(Resources.Load("Prefabs/Effect/Plus"), gameObject.transform.localPosition, Quaternion.identity) as GameObject;
        effect.transform.SetParent(GameObject.Find("InGameCanvas").transform.Find("HealthPlayerPage").transform, false);
        effect.GetComponent<SpawnEffect>().SetAllow(gameObject.transform.Find("Character").gameObject);
        //   effect.GetComponent<SpriteRenderer>().sortingOrder = 30;
        //   AudioManager.Sound.OverridePlay("SE/footstep", E_SOUND.SE);
    }
    public override void StateExit()
    {
        base.StateExit();
    }
}
