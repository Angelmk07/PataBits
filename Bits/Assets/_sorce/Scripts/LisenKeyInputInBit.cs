using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class LisenKeyInputInBit : MonoBehaviour
{
    public static Action<KeyCode> Pressed;
    public static Action TimeOut;

    [SerializeField] private float _musicBMP = 90f;
    [SerializeField] private float _reactionTime = 0.2f;
    [SerializeField] private float _preReactionTime = 0.2f;

    private float _timeForBit;
    private float _startTime;   // Абсолютное время старта для расчета следующего такта
    private int _beatCount = 0; // Количество прошедших битов

    private bool _isListening;
    private bool _canPressKey;

    [SerializeField]
    private PostProcessVolume PostProcessVolume;
    private Vignette Vignette;

    [SerializeField]
    private float Speed = 8f;

    [SerializeField]
    private PlayerS _player;
    private int directionIndex = 1;

    void Start()
    {
        Time.timeScale = 1;
        PostProcessVolume.profile.TryGetSettings(out Vignette);
        _timeForBit = 60f / _musicBMP; 

        _startTime = Time.time; 
        _isListening = true;
        _canPressKey = false;

        Vignette.intensity.value = 0.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _player._spriteR.flipX = !_player._spriteR.flipX;
            _player.PlayerAtackPointRotation.transform.localScale = new Vector3
            (
                directionIndex *= -1,
                _player.PlayerAtackPoint.transform.localScale.y,
                _player.PlayerAtackPoint.transform.localScale.z
            );
        }

        if (Input.GetKeyDown(KeyCode.R) && _player.dead)
        {
            Time.timeScale = 1;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        if (_isListening && !_player.isHide)
        {
            float currentTime = Time.time;
            float nextBeatTime = _startTime + _timeForBit * _beatCount;

            if (currentTime >= nextBeatTime - 2 * _preReactionTime)
            {
                _canPressKey = true;
            }

            if (currentTime >= nextBeatTime - _preReactionTime)
            {
                Vignette.intensity.value = 0.5f;

                if (currentTime >= nextBeatTime + _reactionTime)
                {
                    TimeOut?.Invoke();
                    _beatCount++; 
                    _canPressKey = false;
                    Vignette.intensity.value = 0.0f;
                }
            }

            if (_canPressKey)
            {
                Vignette.intensity.value = Mathf.Lerp(Vignette.intensity.value, 0.5f, Time.deltaTime * Speed);
                if (!string.IsNullOrEmpty(Input.inputString))
                {
                    foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(keyCode))
                        {
                            Pressed?.Invoke(keyCode);
                            _canPressKey = false;
                            _beatCount++; 
                            break;
                        }
                    }
                }
            }
        }
    }
}