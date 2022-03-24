using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class TimeManager : Singleton<TimeManager>
{
    [Header("Components")]
    public GameObject buttonPause;
    public Slider sliderCountDown;
    public ReturnTo returnTo;
    public MultipleGenerator multipleGenerator;

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

    private void Start()
    {
        GetStatsFromDifficulty();
        SliderStart();
    }
    private void GetStatsFromDifficulty()
    {
        string currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
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
            default: break;
        }
    }
    private void SliderStart()
    {
        sliderCountDown.maxValue = _startCountDownAt;
        sliderCountDown.GetComponentInChildren<Image>().color = ColorManager.Instance.GetDifficultyColor();
        sliderCountDown.gameObject.SetActive(false);
    }
    private void Update()
    {
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
                if (PlayerBehaviour.isDead)
                {
                    returnTo.Return("Menu");
                }
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
                sliderCountDown.value = _countDownTime;
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
        buttonPause.gameObject.SetActive(true);
        multipleGenerator.EnableButtonsResult(false);
    }
    private void StartCountdown()
    {
        _countDownTime = _startCountDownAt;
        _countdownEnable = true;
        sliderCountDown.gameObject.SetActive(true);
    }
    private void StopCountdown()
    {
        _countdownEnable = false;
        _countDownTime = 0;
        sliderCountDown.gameObject.SetActive(false);
    }
    public void StartEndSlowmotion(float slowdownFactor, float slowmotionLength)
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * _slowmotionRegular;
        _slowmotionTime = Mathf.FloorToInt(Time.unscaledTime) + slowmotionLength;
        _slowmotionEnable = true;
    }
}
