using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InfoPrefab<T> : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] public T info;
}

public class InfoArrPrefab<T> : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] public T[] info;
}