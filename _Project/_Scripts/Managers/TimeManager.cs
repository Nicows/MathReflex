using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    [Header("Components")]
    [SerializeField] private Slider _sliderCountDown;

    public static event EnableButtonResult OnEnableButtonResult;
    public delegate void EnableButtonResult(bool boolean);

    [Header("Slowdown")]
    private float _slowdownFactor = .05f;
    private float _slowdownLength = 10f;
    private bool _slowmotionEnable = false;
    private float _slowmotionTime;
    private float _slowmotionRegular = .02f;

    [Header("Countdown")]
    private float _startCountDownAt = 10f;
    private bool _countdownEnable = false;
    private float _countDownTime;

    public float _slowmotionLength { get; private set; }

    private void Start()
    {
        GetStatsFromDifficulty();
        SliderStart();
    }
    private void OnEnable()
    {
        TriggerSlowMotion.OnTriggerSlowMotion += StartSlowmotion;
        MultipleGenerator.OnResultSelected += StopSlowmotion;
    }
    private void OnDisable()
    {
        TriggerSlowMotion.OnTriggerSlowMotion -= StartSlowmotion;
        MultipleGenerator.OnResultSelected -= StopSlowmotion;
    }
    private void GetStatsFromDifficulty()
    {
        var currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        switch (currentDifficulty)
        {
            case "Easy":
                _startCountDownAt = 10;
                _slowdownLength = 10;
                _slowdownFactor = .02f;
                break;
            case "Normal":
                _startCountDownAt = 5;
                _slowdownLength = 5;
                _slowdownFactor = .025f;
                break;
            case "Hard":
                _startCountDownAt = 2;
                _slowdownLength = 2;
                _slowdownFactor = .045f;
                break;
        }
    }
    private void SliderStart()
    {
        _sliderCountDown.maxValue = _startCountDownAt;
        _sliderCountDown.GetComponentInChildren<Image>().color = ColorManager.Instance.GetDifficultyColor();
        _sliderCountDown.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (_slowmotionEnable is false)
            return;
        SlowmotionEnable();
        CountdownEnable();
    }
    private void SlowmotionEnable()
    {
        if (_slowmotionEnable)
        {
            if (Time.unscaledTime >= _slowmotionTime)
            {
                StopSlowmotion();
            }
        }
    }
    private void CountdownEnable()
    {
        if (_countdownEnable)
        {
            if (_countDownTime > 0)
            {
                _countDownTime -= 1f * Time.unscaledDeltaTime;
                _sliderCountDown.value = _countDownTime;
            }
        }
    }
    public void StartSlowmotion()
    {
        Time.timeScale = _slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * _slowmotionRegular;
        _slowmotionTime = Time.unscaledTime + _slowdownLength;
        _slowmotionEnable = true;
        StartCountdown();
    }
    public void StopSlowmotion()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.deltaTime;
        _slowmotionEnable = false;
        _slowmotionTime = 0;
        StopCountdown();
        OnEnableButtonResult?.Invoke(false);
    }
    private void StartCountdown()
    {
        _countDownTime = _startCountDownAt;
        _countdownEnable = true;
        _sliderCountDown.gameObject.SetActive(true);
    }
    private void StopCountdown()
    {
        _countdownEnable = false;
        _countDownTime = 0;
        _sliderCountDown.gameObject.SetActive(false);
    }
    public void StartEndSlowmotion(float slowdownFactor, float slowmotionLength)
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * _slowmotionRegular;
        _slowmotionTime = Mathf.FloorToInt(Time.unscaledTime) + slowmotionLength;
        _slowmotionEnable = true;
    }
}
