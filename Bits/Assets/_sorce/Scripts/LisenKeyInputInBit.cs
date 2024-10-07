using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
    [SerializeField]
    private PostProcessVolume PostProcessVolume;
    private Vignette Vignette;
    [SerializeField]
    private float Speed =4;

    [SerializeField]
    private PlayerS _player;
    void Start()
    {
        PostProcessVolume.profile.TryGetSettings(out Vignette);
        _timeForBit = 60f / _musicBMP;
        StartListening();
        _canPressKey = true;
        Vignette.intensity.value = 0.0f;
    }

    void StartListening()
    {
        _isListening = true;
        _nextActionTime = Time.time + _timeForBit;
        Vignette.intensity.value = 0;
    }

    void Update()
    {
        if (_isListening&&!_player.isHide)
        {
            if (Time.time >= _nextActionTime + _reactionTime)
            {
                TimeOut?.Invoke();
                StartListening();
                _canPressKey = false;
                Vignette.intensity.value = 0.0f;
            }
            else
            {
                if (Time.time >= _nextActionTime - _reactionTime)
                {
                    _canPressKey = true;
                    Vignette.intensity.value = 0.5f;
                }

                if (_canPressKey)
                {
                    Vignette.intensity.value = 0.5f;
                    if (!string.IsNullOrEmpty(Input.inputString) )
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
                }

            }
        }
    }
}