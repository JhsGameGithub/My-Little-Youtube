using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeData : MonoBehaviour
{
	public InputField mainInputField;

	public void Start()
	{
		mainInputField=GetComponent<InputField>();
		mainInputField.onValueChanged.AddListener(ValueChange);
	}
	
	public void ValueChange(string text)
	{
		BoardcastManager.Instance.title = text;
	}
}
