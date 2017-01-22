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
			
			(AudioClip)Resources.Load ("Audio/I Wanna CROMCH"),

			(AudioClip)Resources.Load ("Audio/Morse Tone"),

			(AudioClip)Resources.Load ("Audio/Inhale 2"),

			(AudioClip)Resources.Load ("Audio/Hamburger Shuffle"),

			(AudioClip)Resources.Load ("Audio/I Ate A Styrofoam Cup For This "),

			(AudioClip)Resources.Load ("Audio/Wind")





		};
	}
	

}
