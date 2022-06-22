using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPool : MonoBehaviour
{
    [SerializeField]
    private Queue<GameObject> slot_pool;

    public void Push(GameObject slot)
    {
        slot.SetActive(false);
        slot.transform.SetParent(transform);
        slot_pool.Enqueue(slot);
    }

    public GameObject Pop()
    {
        return slot_pool.Dequeue();
    }

    public int Count
    {
        get { return slot_pool.Count; }
    }

    public void Init()
    {
        slot_pool = new Queue<GameObject>();
    }
}
