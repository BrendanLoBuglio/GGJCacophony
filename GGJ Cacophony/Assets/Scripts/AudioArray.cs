using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioArray : MonoBehaviour
{

	public AudioClip[] list;

	// Use this for initialization
	void Start ()
	{
		list = new AudioClip[] {
			
			(AudioClip)Resources.Load ("I Wanna CROMCH"),

			(AudioClip)Resources.Load ("Morse Tone"),

			(AudioClip)Resources.Load ("Inhale 2"),

			(AudioClip)Resources.Load ("Hamburger Shuffle"),

			(AudioClip)Resources.Load ("I Ate A Styrofoam Cup For This "),

			(AudioClip)Resources.Load ("Wind"),

			(AudioClip)Resources.Load ("Rain7")
		
		};

	}
	void Update (){
		if (Input.GetKeyDown (KeyCode.A)) {
			AudioSource maudio = GetComponent<AudioSource> ();
			maudio.PlayOneShot (list[0]);
		
		}
	}
	

}
