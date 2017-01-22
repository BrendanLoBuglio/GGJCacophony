using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MessageSender : NetworkBehaviour {

    public static bool isTextPlayer;
    public static bool isWikiPlayer;


    static MessageSender _instance;
    public static MessageSender instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MessageSender>();
            }
            return _instance;
        }
    }

    MorseAudioController morsePlayer;

    void Start()
    {
        morsePlayer = GetComponent<MorseAudioController>();
    }

    public override void OnStartLocalPlayer()
    {
        _instance = this;
    }

    [Command]
    public void CmdSendMessageToTextPlayer(string message)
    {
        if (isTextPlayer)
        {
            morsePlayer.EnqueueMorseString(message);
        }
    }

    [ClientRpc]
    public void RpcSendMessageToWikiPlayer(string message)
    {
        if (isWikiPlayer)
        {
            morsePlayer.EnqueueMorseString(message);
        }
    }

    public void PlayMorse(string message)
    {

    }

    public void SendTextMessage(string message)
    {
        if (isWikiPlayer)
        {
            CmdSendMessageToTextPlayer(message);
        }
        else
        {
            RpcSendMessageToWikiPlayer(message);
        }
    }
}
