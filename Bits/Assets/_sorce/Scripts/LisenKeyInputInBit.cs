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
    private float _nextActionTime;
    private bool _isListening;
    private bool _canPressKey;
    [SerializeField]
    private PostProcessVolume PostProcessVolume;
    private Vignette Vignette;
    [SerializeField]
    private float Speed = 8f; 

    [SerializeField]
    private PlayerS _player;

    void Start()
    {
        Time.timeScale = 1;
        PostProcessVolume.profile.TryGetSettings(out Vignette);
        _timeForBit = 60f / _musicBMP; 
        StartListening();
        _canPressKey = false; 
        Vignette.intensity.value = 0.0f;
    }

    void StartListening()
    {
        _isListening = true;
        _nextActionTime = Time.time + _timeForBit; 
        Vignette.intensity.value = 0.0f; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _player._spriteR.flipX = !_player._spriteR.flipX;
            _player.PlayerAtackPointRotation.transform.localScale = new Vector3
            (
                -_player.PlayerAtackPoint.transform.localScale.x,
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
            if (Time.time >= _nextActionTime -_preReactionTime)
            {
                Vignette.intensity.value = Mathf.Lerp(Vignette.intensity.value, 0.5f, Time.deltaTime * Speed); 
                _canPressKey = true;
                if (Time.time >= _nextActionTime + _reactionTime)
                {
                    TimeOut?.Invoke();
                    StartListening();
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
                            StartListening();
                            break;
                        }
                    }
                }
            }
        }
    }
}