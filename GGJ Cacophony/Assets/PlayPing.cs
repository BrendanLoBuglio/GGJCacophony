using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPing : MonoBehaviour {
    AudioSource source;
    public static PlayPing instance;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        instance = this;
    }
	
    public static void Play()
    {
        instance.source.Play();
    }

}
