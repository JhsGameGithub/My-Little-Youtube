using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    private Text player_name;

    private void Start()
    {
        player_name = GetComponent<Text>();

        StartCoroutine(BindName());
    }

    IEnumerator BindName()
    {
        while (!DatabaseManager.Instance.isLoad)
        {
            yield return null;

        }

        player_name.text = DatabaseManager.Player.status.Name;
    }
}
