using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class BloomController : MonoBehaviour {

    public float bloomJitter;
    public float bloomMovementSpeed;
    public float morseIncreasedBloom;

    float currentBloom;
    Bloom bloom;
    AudioSource morseSource;

	// Use this for initialization
	void Start () {
        bloom = Camera.main.GetComponent<Bloom>();
        currentBloom = bloom.bloomIntensity;
        morseSource = MorseAudioController.instance.mAudioSource;
    }
	
	// Update is called once per frame
	void Update () {
        bloom.bloomIntensity = currentBloom + Random.Range(-bloomJitter, bloomJitter);
        
        if (MorseAudioController.morsePlaying)
        {
            bloom.bloomIntensity += morseIncreasedBloom;
        }
        
	}
}
