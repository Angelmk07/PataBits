using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnKeyPress : MonoBehaviour
{
    [SerializeField]
    private AudioClip soundQ;

    [SerializeField]
    private AudioClip soundR;

    [SerializeField]
    private AudioClip soundW;

    [SerializeField]
    private AudioClip soundA;

    [SerializeField]
    private AudioClip soundS;

    [SerializeField]
    private AudioClip soundD;

    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && soundQ != null)
        {
            audioSource.PlayOneShot(soundQ);
        }
        if (Input.GetKeyDown(KeyCode.R) && soundR != null)
        {
            audioSource.PlayOneShot(soundR);
        }
        if (Input.GetKeyDown(KeyCode.W) && soundW != null)
        {
            audioSource.PlayOneShot(soundW);
        }
        if (Input.GetKeyDown(KeyCode.A) && soundA != null)
        {
            audioSource.PlayOneShot(soundA);
        }
        if (Input.GetKeyDown(KeyCode.S) && soundS != null)
        {
            audioSource.PlayOneShot(soundS);
        }
        if (Input.GetKeyDown(KeyCode.D) && soundD != null)
        {
            audioSource.PlayOneShot(soundD);
        }
    }
}