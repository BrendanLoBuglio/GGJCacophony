using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientNetworkManager : NetworkManager{

	// Use this for initialization
	void Start () {
        networkAddress = HostNetworkManager.hostIP;
        networkPort = HostNetworkManager.port;
        StartClient();
        MessageSender.isWikiPlayer = true;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
