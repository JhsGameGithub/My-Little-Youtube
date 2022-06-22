using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditedVideoCalculator
{
    public int subscriber;
    public float popularity;
    public int seed;

    public EditedVideoInfo info;

    //TimeUpdate �ѹ��� �ö󰡴� ��ġ _ ����
    private float once_views;
    private float once_goods;
    private float once_subscriber;
    private float once_money;

    //���������� �ö󰡴� ��ġ _ ����
    private float temp_views;
    private float temp_goods;
    private float temp_subscriber;
    private float temp_money;

    public EditedVideoCalculator(EditedVideoInfo info,int subscriber,float popularity,int seed)
    {
        this.info = info;
        this.subscriber = subscriber;
        this.popularity = popularity;
        this.seed = seed;
        InitCaculator();
    }


    //���� �ʱ�ȭ
    public void InitCaculator()
    {
        float all_views = (subscriber / 2 + seed) * info.funny * popularity;
        float all_goods = all_views / 20;
        float all_subscriber = all_goods / 3;
        float all_money = all_views * 1.6f;

        once_views = all_views / 43200;
        once_goods = all_goods / 43200;
        once_subscriber = all_subscriber / 43200;
        once_money = all_money / 43200;

        temp_views = once_views * info.repeat;
        temp_goods = once_goods * info.repeat;
        temp_subscriber = once_subscriber * info.repeat;
        temp_money = once_money * info.repeat;

        temp_views -= (int)temp_views;
        temp_goods -= (int)temp_goods;
        temp_subscriber -= (int)temp_subscriber;
        temp_money -= (int)temp_money;
    }


    //������ �ö� ��ġ _ ����
    //�÷��� �ʹݿ� ��ġ�� �ʹ� �۱� ������
    //��Ƽ� �������ִ� ����
    public int GetOnceGoods()
    {
        temp_goods += once_goods;
        if(temp_goods > 1.0f)
        {
            int temp = (int)temp_goods;
            temp_goods -= temp;
            return temp;
        }
        return 0;
    }
    public int GetOnceSubscriber()
    {
        temp_subscriber += once_subscriber;
        if (temp_subscriber > 1.0f)
        {
            int temp = (int)temp_subscriber;
            temp_subscriber -= temp;
            return temp;
        }
        return 0;
    }
    public int GetOnceViews()
    {
        temp_views += once_views;
        if (temp_views > 1.0f)
        {
            int temp = (int)temp_views;
            temp_views -= temp;
            return temp;
        }
        return 0;
    }
    public int GetOnceMoney()
    {
        temp_money += once_money;
        if (temp_money > 1.0f)
        {
            int temp = (int)temp_money;
            temp_money -= temp;
            return temp;
        }
        return 0;
    }
}
