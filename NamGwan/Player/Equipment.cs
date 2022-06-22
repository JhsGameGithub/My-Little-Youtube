using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType //  장비 종류 
{
    CHAIR, //의자 
    COMPUTER, //컴퓨터 
    DESK, //책상 
    EXERCISE, //운동기구 
    BED, // 침대 
}
[System.Serializable]
public class Equipment //가구 클래스 
{
    public EquipmentType type;
    public int level;
    public Equipment(EquipmentType getType, int getLevel=1)
    {
        type = getType;
        level = getLevel;
    }
}
public class Chair : Equipment
{
    public Chair() : base(EquipmentType.CHAIR)
    {
      
    }
}
public class Computer : Equipment
{
    public Computer() : base(EquipmentType.COMPUTER)
    {

    }
}
public class Desk : Equipment
{
    public Desk() : base(EquipmentType.DESK)
    {

    }
}
public class Exercise : Equipment
{
    public Exercise() : base(EquipmentType.EXERCISE)
    {

    }
}
public class Bed : Equipment
{
    public Bed() : base(EquipmentType.BED)
    {

    }
}