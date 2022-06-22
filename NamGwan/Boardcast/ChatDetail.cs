using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChatDetail : MonoBehaviour
{
    // Start is called before the first frame update
    public void Setting(string set)
    {
        GetComponent<Text>().text = set;
        GetComponent<ContentSizeFitter>().SetLayoutVertical();
        Vector2 parent = gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta;
        parent.y += gameObject.GetComponent<RectTransform>().sizeDelta.y;
        gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta = parent;
    }
}
