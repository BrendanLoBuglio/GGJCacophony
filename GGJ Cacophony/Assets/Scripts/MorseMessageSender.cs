using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorseMessageSender : MonoBehaviour
{
    [SerializeField] private Button sendButton;
    [SerializeField] private InputField textField;
    [SerializeField] private MorseAudioController morseController;


    private void Start()
    {
        sendButton.onClick.AddListener(PushMorseString);
    }

    public void PushMorseString()
    {
        morseController.PlayMorseString(textField.text);
    }
}