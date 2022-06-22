using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeautyState : PlayerState
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
            DatabaseManager.Player.status.Appearance += 0.1f;
            SpawnEffect();
        }
    }
    public void SpawnEffect()
    {
      
        GameObject effect = Instantiate(Resources.Load("Prefabs/Effect/Plus"), gameObject.transform.localPosition, Quaternion.identity) as GameObject;
        effect.transform.SetParent(GameObject.Find("InGameCanvas").transform.Find("BeautyPlayerPage").transform, false);
        effect.GetComponent<SpawnEffect>().SetAllow(gameObject.transform.Find("Character").Find("Anim").gameObject);
     //   effect.GetComponent<SpriteRenderer>().sortingOrder = 30;
     //   AudioManager.Sound.OverridePlay("SE/footstep", E_SOUND.SE);
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}
