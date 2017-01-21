using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EntryField : MonoBehaviour{

    InputField inputField;

	// Use this for initialization
	void Start () {
        inputField = GetComponent<InputField>();
	}

    public void SendTextMessage()
    {
        MessageSender.instance.SendTextMessage(inputField.text);
        inputField.text = "";
        inputField.ActivateInputField();
    }

}
