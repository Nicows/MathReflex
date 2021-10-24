using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; private set; }

    [Header("Components")]
    public GameObject buttonPause;
    public Slider sliderCountDown;
    public ReturnTo returnTo;
    public MultipleGenerator multipleGenerator;

    [Header("Slowdown")]
    [SerializeField] private float slowdownFactor = .05f;
    private float slowdownLength = 10f;
    [SerializeField] private bool slowmotionEnable = false;
    [SerializeField] private float slowmotionTime;
    private float slowmotionRegular = .02f;

    [Header("Countdown")]
    private float startCountDownAt = 10f;
    private bool countdownEnable = false;
    private float countDownTime;


    private void Awake()
    {
        CheckInstance();
    }
    private void CheckInstance()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
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
                startCountDownAt = 10;
                slowdownLength = 10;
                slowdownFactor = .02f;
                break;
            case "Normal":
                startCountDownAt = 5;
                slowdownLength = 5;
                slowdownFactor = .025f;
                break;
            case "Hard":
                startCountDownAt = 2;
                slowdownLength = 2;
                slowdownFactor = .045f;
                break;
            default: break;
        }
    }
    private void SliderStart()
    {
        sliderCountDown.maxValue = startCountDownAt;
        sliderCountDown.GetComponentInChildren<Image>().color = ColorManager.colorDifficulty;
        sliderCountDown.gameObject.SetActive(false);
    }
    private void Update()
    {
        SlowmotionEnable();
        CountdownEnable();
    }
    private void SlowmotionEnable()
    {
        if (slowmotionEnable)
        {
            if (Time.unscaledTime >= slowmotionTime)
            {
                StopSlowmotion();
                if (PlayerBehaviour.isDead)
                {
                    returnTo.ReturnToMenu();
                }
            }
        }
    }
    private void CountdownEnable()
    {
        if (countdownEnable)
        {
            if (countDownTime > 0)
            {
                countDownTime -= 1f * Time.unscaledDeltaTime;
                sliderCountDown.value = countDownTime;
            }
        }
    }
    public void StartSlowmotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * slowmotionRegular;
        slowmotionTime = Time.unscaledTime + slowdownLength;
        slowmotionEnable = true;
        StartCountdown();
    }
    public void StopSlowmotion()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.deltaTime;
        slowmotionEnable = false;
        slowmotionTime = 0;
        StopCountdown();
        buttonPause.gameObject.SetActive(true);
        multipleGenerator.EnableButtonsResult(false);
    }
    private void StartCountdown()
    {
        countDownTime = startCountDownAt;
        countdownEnable = true;
        sliderCountDown.gameObject.SetActive(true);
    }
    private void StopCountdown()
    {
        countdownEnable = false;
        countDownTime = 0;
        sliderCountDown.gameObject.SetActive(false);
    }
    public void StartEndSlowmotion(float slowdownFactor, float slowmotionLength)
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * slowmotionRegular;
        slowmotionTime = Mathf.FloorToInt(Time.unscaledTime) + slowmotionLength;
        slowmotionEnable = true;
    }
}
