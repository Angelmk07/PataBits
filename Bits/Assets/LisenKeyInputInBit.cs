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
    private bool _canPressKey;

    void Start()
    {
        _timeForBit = 60f / _musicBMP;
        StartListening();
        _canPressKey = true;
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

                if (!string.IsNullOrEmpty(Input.inputString) && _canPressKey)
                {
                    foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(keyCode))
                        {
                            Pressed?.Invoke(keyCode);
                            _canPressKey = false;  
                            StartListening();   
                            break;
                        }
                    }
                }
                if (Time.time >= _nextActionTime-_reactionTime)
                {
                    _canPressKey = true;
                }
            }
        }
    }
}