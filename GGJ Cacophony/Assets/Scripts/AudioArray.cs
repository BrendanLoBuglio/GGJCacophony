using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioArray : MonoBehaviour
{
	private AudioSource maudio;
	public AudioClip[] list;

	// Use this for initialization
	void Start ()
	{
		maudio = GetComponent<AudioSource> ();
			
		list = new AudioClip[] {
			
			(AudioClip)Resources.Load ("I Wanna CROMCH"),

			(AudioClip)Resources.Load ("Morse Tone"),

			(AudioClip)Resources.Load ("Inhale 2"),

			(AudioClip)Resources.Load ("Hamburger Shuffle"),

			(AudioClip)Resources.Load ("I Ate A Styrofoam Cup For This "),

			(AudioClip)Resources.Load ("Wind"),

			(AudioClip)Resources.Load ("Rain7"),

			(AudioClip)Resources.Load ("Wet Meat Noises"),

			(AudioClip)Resources.Load ("Crow cawing sound effect")
		
		};

	}
//	void Update (){
//		if (Input.GetKeyDown (KeyCode.Q)) {
//			AudioSource maudio = GetComponent<AudioSource> ();
//			maudio.PlayOneShot (list[0]);
//
//		}
//		if (Input.GetKeyDown (KeyCode.W)) {
//			AudioSource maudio = GetComponent<AudioSource> ();
//			maudio.PlayOneShot (list[1]);
//		
//
//		}
//		if (Input.GetKeyDown (KeyCode.E)) {
//			AudioSource maudio = GetComponent<AudioSource> ();
//			maudio.PlayOneShot (list[2]);
//
//		}
//		if (Input.GetKeyDown (KeyCode.R)) {
//			AudioSource maudio = GetComponent<AudioSource> ();
//			maudio.PlayOneShot (list[3]);
//
//		}
//
//		if (Input.GetKeyDown (KeyCode.T)) {
//			AudioSource maudio = GetComponent<AudioSource> ();
//			maudio.PlayOneShot (list[4]);
//
//		}
//
//		if (Input.GetKeyDown (KeyCode.Y)) {
//			AudioSource maudio = GetComponent<AudioSource> ();
//			maudio.PlayOneShot (list[5]);
//
//		}
//
//		if (Input.GetKeyDown (KeyCode.U)) {
//			AudioSource maudio = GetComponent<AudioSource> ();
//			maudio.PlayOneShot (list[6]);
//
//		}
//
//	}

	public void PlayCromch(){
		maudio.PlayOneShot (list[0]);

	}

	public void PlayMorseTone(){
		maudio.PlayOneShot (list [1]);
	}

	public void PlayInhale(){
		maudio.PlayOneShot (list [2]);
	}

	public void PlayTrashRifling(){
		maudio.PlayOneShot (list [3]);
	}

	public void PlayStyrofoamCrunch(){
		maudio.PlayOneShot (list [4]);
	}

	public void PlaySpookyBG(){
		maudio.PlayOneShot (list [5]);
	}

	public void PlayRain(){
		maudio.PlayOneShot (list [6]);
	}	

		public void PlaySkinningCorpse(){
		maudio.PlayOneShot (list [7]);
	}

	public void PlayCrowCaw(){
		maudio.PlayOneShot (list [8]);
	}


}
