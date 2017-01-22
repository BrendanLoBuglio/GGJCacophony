﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MorsePlaybackState { stopped, playingDot, playingDash, elementBreak, letterBreak, wordBreak, messageBreak}

[RequireComponent(typeof(AudioSource))]
public class MorseAudioController : MonoBehaviour
{
    [SerializeField] private float dotLength = 0.09f;

    private AudioSource mAudioSource;
    private string currentSentence;

    private string[][] currentMorseMessage;
    private Queue<string[][]> upcomingMorseMessages;

    private int wordIndex = 0;
    private int letterIndex = 0;
    private int charIndex = -1;
    private float timer = 0f;
    private bool needElementSeparator = false;
    private bool playingCharacter = false;
    private MorsePlaybackState playbackState = MorsePlaybackState.stopped;


	void Start ()
    {
        mAudioSource = GetComponent<AudioSource>();
        upcomingMorseMessages = new Queue<string[][]>();
	}


    public void EnqueueMorseString(string morseStringIn)
    {
        upcomingMorseMessages.Enqueue(MorseUtility.GetMorseWordLetters(MorseUtility.GenerateMorseSentence(morseStringIn)));
    }

    private void playNextMorseString()
    {
        if(upcomingMorseMessages.Count >= 1) {
            currentMorseMessage = upcomingMorseMessages.Dequeue();
            resetMorseSequence();
        }
    }

    public void resetMorseSequence()
    {
        timer = 0f;
        wordIndex = 0;
        letterIndex = 0;
        charIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            EnqueueMorseString("This is the test string");
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            playNextMorseString();
        }

        //I hate this:
        if(currentMorseMessage != null) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                if (needElementSeparator) {
                    Debug.Log("Playing element break at Time " + Time.time);
                    timer = dotLength * 1f;
                    mAudioSource.Stop();
                    needElementSeparator = false;
                }
                else {
                    if (playingCharacter) {
                        charIndex++;
                    }
                    if (charIndex < currentMorseMessage[wordIndex][letterIndex].Length) {
                        switch (currentMorseMessage[wordIndex][letterIndex][charIndex]) {
                            case '.':
                                //Play Dot:
                                Debug.Log("Playing dot at Time " + Time.time);
                                timer = dotLength * 1f;
                                mAudioSource.Play();
                                playingCharacter = true;
                                if(charIndex < currentMorseMessage[wordIndex][letterIndex].Length - 1) {
                                    needElementSeparator = true;
                                }
                                break;
                            case '-':
                                //Play Dash:
                                Debug.Log("Playing Dash at Time " + Time.time);
                                timer = dotLength * 3f;
                                mAudioSource.Play();
                                playingCharacter = true;
                                if (charIndex < currentMorseMessage[wordIndex][letterIndex].Length - 1) {
                                    needElementSeparator = true;
                                }
                                break;
                        }
                    }
                    else {
                        mAudioSource.Stop();
                        charIndex = 0;
                        letterIndex++;
                        timer = dotLength * MorseUtility.spaceBetweenLetters;
                        if (letterIndex >= currentMorseMessage[wordIndex].Length) {
                            letterIndex = 0;
                            wordIndex++;
                            timer = dotLength * MorseUtility.spaceBetweenWords;
                            if (wordIndex >= currentMorseMessage.Length) {
                                wordIndex = 0;
                                timer = dotLength * MorseUtility.spaceBetweenMessages;
                                playingCharacter = false;
                                Debug.Log("Playing message break at Time " + Time.time);
                            }
                            else {
                                playingCharacter = false;
                                Debug.Log("Playing word break at Time " + Time.time);
                            }
                        }
                        else {
                            playingCharacter = false;
                            Debug.Log("Playing Letter break at Time " + Time.time);
                        }
                    }

                    Debug.Log("The current letter is \"" + currentMorseMessage[wordIndex][letterIndex] + "\"");
                }
            }
        }
    }

    //private IEnumerator playMorseSequence(string morseStringIn)
    //{
    //    Debug.Log("Interpretting Morse " + morseStringIn);
    //
    //    for (int i = 0; i < morseStringIn.Length; i++) {
    //        char morseChar = morseStringIn[i];
    //        switch (morseChar) {
    //            case '.':
    //                mAudioSource.Play();
    //                yield return new WaitForSeconds(dotLength);
    //                mAudioSource.Stop();
    //                yield return new WaitForSeconds(dotLength);
    //                break;
    //            case '-':
    //                mAudioSource.Play();
    //                yield return new WaitForSeconds(dotLength * 3);
    //                mAudioSource.Stop();
    //                yield return new WaitForSeconds(dotLength);
    //                break;
    //            case ' ':
    //                mAudioSource.Stop();
    //                yield return new WaitForSeconds(dotLength);
    //                break;
    //        }
    //    }
    //}
}
