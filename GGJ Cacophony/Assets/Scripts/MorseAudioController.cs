using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MorseAudioController : MonoBehaviour
{
    [SerializeField] private float dotLength = 0.09f;

    private AudioSource mAudioSource;
    private string currentSentence;
    private int charIndex = 0;

    private Coroutine morseCoroutine;

	void Start ()
    {
        mAudioSource = GetComponent<AudioSource>();
	}

    public void PlayMorseString(string morseStringIn)
    {
        if(morseCoroutine != null) {
            this.StopCoroutine(morseCoroutine);
        }

        morseCoroutine = this.StartCoroutine(playMorseSequence(morseStringIn));
    }


    private IEnumerator playMorseSequence(string morseStringIn)
    {
        Debug.Log("Interpretting Morse " + morseStringIn);

        for (int i = 0; i < morseStringIn.Length; i++) {
            char morseChar = morseStringIn[i];
            switch (morseChar) {
                case '.':
                    mAudioSource.Play();
                    yield return new WaitForSeconds(dotLength);
                    mAudioSource.Stop();
                    yield return new WaitForSeconds(dotLength);
                    break;
                case '-':
                    mAudioSource.Play();
                    yield return new WaitForSeconds(dotLength * 3);
                    mAudioSource.Stop();
                    yield return new WaitForSeconds(dotLength);
                    break;
                case ' ':
                    mAudioSource.Stop();
                    yield return new WaitForSeconds(dotLength);
                    break;
            }
        }
    }
}
