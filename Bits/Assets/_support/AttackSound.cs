using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audio;

    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void Hit()
    {
        _audioSource.PlayOneShot(_audio);
    }

}
