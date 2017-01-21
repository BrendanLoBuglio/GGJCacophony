using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EntryField : MonoBehaviour{

    InputField inputField;
    private string prevTextState = "";

	// Use this for initialization
	void Start () {
        inputField = GetComponent<InputField>();
	}

    public void SendTextMessage()
    {
        MessageSender instance = MessageSender.instance;
        if (instance != null)
        {
            instance.SendTextMessage(inputField.text);
        }
        inputField.ActivateInputField();
        inputField.text = "";

    }

    public void reset()
    {
        inputField.ActivateInputField();
        inputField.text = "";
    }

    public void RemoveCommands()
    {
        inputField.text =  inputField.text.Replace("[", "");
        inputField.text = inputField.text.Replace("]", "");
        inputField.text = inputField.text.Replace(@"\", "");
        inputField.text = inputField.text.Replace("-", "");
        inputField.text = inputField.text.Replace("+", "");
    }

}
