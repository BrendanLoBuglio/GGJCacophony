using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInAudio : MonoBehaviour


{
	public AudioSource thesound;


	// Use this for initialization
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKey (KeyCode.Q)) {
			AudioSource audio = GetComponent<AudioSource> ();
			audio.Play ();
		}
	}
}