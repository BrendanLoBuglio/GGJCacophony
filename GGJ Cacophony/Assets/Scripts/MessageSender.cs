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

    private AudioSource source;

    public override void OnStartLocalPlayer()
    {
        _instance = this;
        source = GetComponents<AudioSource>()[1];
    }

    [Command]
    public void CmdSendMessageToTextPlayer(string message)
    {
        if (isTextPlayer)
        {
            MorseAudioController.instance.EnqueueMorseString(message);
            PlayMessagePing();
        }
    }

    [ClientRpc]
    public void RpcSendMessageToWikiPlayer(string message)
    {
        if (isWikiPlayer)
        {
            MorseAudioController.instance.EnqueueMorseString(message);
            PlayMessagePing();
        }
    }

    void PlayMessagePing()
    {
        PlayPing.Play();
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
