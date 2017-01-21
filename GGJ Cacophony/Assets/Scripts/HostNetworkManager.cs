using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostNetworkManager : NetworkManager{

    public static readonly string hostIP = "192.168.43.54";
    public static readonly int port = 7777;

    // Use this for initialization
    void Start () {
        networkAddress = hostIP;
        this.networkPort = port;
        StartHost();
        MessageSender.isTextPlayer = true;
	}

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
