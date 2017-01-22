using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardSoundPlayer : MonoBehaviour {

    public float playerTimePerKeyStroke = .1f;
    AudioSource source;
    float timer;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.Pause();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            StopAllCoroutines();
            StartCoroutine(PlayKeySound());
        }
        Debug.Log(timer);
	}

    Coroutine keySoundRoutine;
    IEnumerator PlayKeySound()
    {
        source.UnPause();
        
        yield return new WaitForSeconds(playerTimePerKeyStroke);
        
        keySoundRoutine = null;
        source.Pause();
    }
}
