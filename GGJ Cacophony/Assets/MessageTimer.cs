using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTimer : MonoBehaviour {

    public float degredationTime = 60;

    public static bool onMorse;

    private float timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > degredationTime)
        {
            onMorse = true;
            TextLog.AddWhiteSpace();
            TextLog.AddTextLineToTextLog("Signal has degraded, can now only send and recieve morse");
            Destroy(gameObject);
        }
	}
}
