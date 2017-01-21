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

    void Start()
    {
        
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
            Debug.Log(message);
        }
    }

    [ClientRpc]
    public void RpcSendMessageToWikiPlayer(string message)
    {
        if (isWikiPlayer)
        {
            Debug.Log(message);

        }
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
