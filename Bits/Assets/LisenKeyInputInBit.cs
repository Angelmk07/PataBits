using System;
using UnityEngine;

public class LisenKeyInputInBit : MonoBehaviour
{
    public static Action<KeyCode> Pressed;
    public static Action TimeOut;

    [SerializeField] private float _musicBMP = 90f; 
    [SerializeField] private float _reactionTime = 0.2f; 

    private float _timeForBit;  
    private float _nextActionTime; 
    private bool _isListening; 

    void Start()
    {
        _timeForBit = 60f / _musicBMP;
        StartListening();
    }

    void StartListening()
    {
        _isListening = true;
        _nextActionTime = Time.time + _timeForBit;  
    }

    void Update()
    {
        if (_isListening)
        {
            if (Time.time >= _nextActionTime + _reactionTime)
            {
                TimeOut?.Invoke();
                StartListening();   
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(keyCode))
                        {
                            Pressed?.Invoke(keyCode); 
                            StartListening();     
                            break;
                        }
                    }
                }
            }
        }
    }
}