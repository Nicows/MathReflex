using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 10f;
    public float slowmotionTime;
    public bool slowmotionEnable = false;

    public static TimeManager instance { get; private set; }

    public Slider sliderCountDown;
    public float startCountDownAt = 10f;
    public float countDownTime;
    public bool countdownEnable = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);

    }
    private void Start()
    {
        string currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");

        switch (currentDifficulty)
        {
            case "Facile":
            case "Easy":
                startCountDownAt = 10;
                slowdownLength = 10;
                break;

            case "Normal":
                startCountDownAt = 5;
                slowdownLength = 5;
                break;

            case "Difficile":
            case "Hard":
                startCountDownAt = 2;
                slowdownLength = 2;
                break;

            default:
                startCountDownAt = 10;
                slowdownLength = 10;
                break;
        }

        sliderCountDown.maxValue = startCountDownAt;
        sliderCountDown.GetComponentInChildren<Image>().color = ColorManager.colorDifficulty;
        sliderCountDown.gameObject.SetActive(false);
        // nextUpdate = 1f;
    }
    private void Update()
    {

        if (slowmotionEnable)
        {
            if (Time.unscaledTime >= slowmotionTime)
            {
                StopSlowmotion();
                if (PlayerBehaviour.isDead)
                {
                    GameOver.ReturnToMenu();
                }
            }
        }
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
        Time.fixedDeltaTime = Time.timeScale * .02f;
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
    }
    public void CountDown()
    {
        // if (countdownEnable)
        // {
        //     sliderCountDown.value = countDownTime;
        //     if (Time.unscaledTime >= nextUpdate)
        //     {
        //         // Change the next update (current second+1)
        //         nextUpdate = Mathf.FloorToInt(Time.unscaledTime) + 1f;
        //         if (countDownTime > 0)
        //         {
        //             countDownTime -= 1f;
        //             textCountDown.SetText(countDownTime.ToString());
        //         }
        //     }
        // }
    }
    public void StartCountdown()
    {
        countdownEnable = true;
        countDownTime = startCountDownAt;

        sliderCountDown.gameObject.SetActive(true);
    }
    public void StopCountdown()
    {
        countdownEnable = false;
        sliderCountDown.gameObject.SetActive(false);
    }
    public void StartEndSlowmotion(float slowdownFactor, float slowmotionLength = 10f)
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        slowmotionTime = Mathf.FloorToInt(Time.unscaledTime) + slowmotionLength;
        slowmotionEnable = true;
    }
}
