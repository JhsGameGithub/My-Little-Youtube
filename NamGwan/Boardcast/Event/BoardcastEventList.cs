using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardcastEventList 
{
    public List<string> positiveList;
    public List<string> negativeList;
    public List<string> nothingList;
    public List<string> recordingList;
 
    public BoardcastEventList()
    {
        positiveList = new List<string>();
        negativeList = new List<string>();
        nothingList = new List<string>();
        recordingList = new List<string>();

    }
    public void ClearEvent()
    {
        positiveList.Clear();
        negativeList.Clear();
        nothingList.Clear();
        recordingList.Clear();
    }
    public void AddEvent(string path, BoardcastEventEnumType type)
    {
        if (type == BoardcastEventEnumType.POSITIVE)
        {
            positiveList.Add(path);
        }
        else if (type == BoardcastEventEnumType.NEGATIVE)
        {
            negativeList.Add(path);
        }
        else if (type == BoardcastEventEnumType.NOTHING)
        {
            nothingList.Add(path);
        }
        else if (type == BoardcastEventEnumType.RECORDING)
        {
            recordingList.Add(path);
        }

    }
    public void ActionEvent(BoardcastEventEnumType type) 
    {

       if(type ==BoardcastEventEnumType.POSITIVE)//긍정적인 이벤트 
        {
            if(positiveList.Count==0) //더이상 이벤트가 없을떄 
            {
                ActionEvent(BoardcastEventEnumType.NOTHING);
                return;
            }
            string temp = positiveList[Random.Range(0, positiveList.Count)];
            positiveList.Remove(temp);

           BoardcastManager.Instance.eventListener.Spawn(temp);
           AudioManager.Sound.Play("SE/Coin_-_Sound_Effect_SFX", E_SOUND.SE);
        }
        else if(type == BoardcastEventEnumType.NEGATIVE) //부정적인 이벤트 
        {
            if (negativeList.Count == 0) //더이상 이벤트가 없을떄 
            {
                ActionEvent(BoardcastEventEnumType.NOTHING);
                return;
            }
            string temp = negativeList[Random.Range(0, negativeList.Count)];
            negativeList.Remove(temp);

            BoardcastManager.Instance.eventListener.Spawn(temp);
            AudioManager.Sound.Play("SE/button-12", E_SOUND.SE);
        }
        else if (type == BoardcastEventEnumType.NOTHING) //아무일도 일어나지 않는 이벤트 
        {
            BoardcastManager.Instance.eventListener.Spawn(nothingList[Random.Range(0, nothingList.Count)]);
            AudioManager.Sound.Play("SE/button-13", E_SOUND.SE);
        }
       else if(type == BoardcastEventEnumType.RECORDING)
        {
            BoardcastManager.Instance.eventListener.Spawn(recordingList[Random.Range(0, recordingList.Count)]);
            AudioManager.Sound.Play("SE/RecordSound", E_SOUND.SE);
        }
    }

  
}
