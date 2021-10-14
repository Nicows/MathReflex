using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 10f;
    public bool slowmotionEnable = false;
    public float slowmotionTime;

    public static TimeManager instance;
    public MultipleGenerator multipleGenerator;

    public TMP_Text textCountDown;
    public Slider sliderCountDown;
    public bool countdownEnable = false;
    public int startCountDownAt = 10;
    public int countDownTime;
    private int nextUpdate = 1;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        string currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");

        switch (currentDifficulty)
        {
            case "Facile":
            case "Easy":
                startCountDownAt = 10;
                slowdownLength = 10;
                break;

            case "Normal":
                startCountDownAt = 6;
                slowdownLength = 6;
                break;

            case "Difficile":
            case "Hard":
                startCountDownAt = 3;
                slowdownLength = 3;
                break;

            default: break;
        }
    }
    private void Start()
    {
        sliderCountDown.maxValue = startCountDownAt;
        sliderCountDown.value = startCountDownAt;
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
            sliderCountDown.value = ((float)countDownTime);
            if (Time.unscaledTime >= nextUpdate)
            {
                // Change the next update (current second+1)
                nextUpdate = Mathf.FloorToInt(Time.unscaledTime) + 1;
                CountDown();
            }
        }

    }

    public void StartSlowmotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        slowmotionTime = Mathf.FloorToInt(Time.unscaledTime) + slowdownLength;
        slowmotionEnable = true;
    }
    public void StartSlowmotion(float slowdownFactor, float slowmotionLength = 10f)
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        slowmotionTime = Mathf.FloorToInt(Time.unscaledTime) + slowmotionLength;
        slowmotionEnable = true;
    }
    public void StopSlowmotion()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.deltaTime;
        slowmotionEnable = false;
    }
    public void CountDown()
    {
        if (countDownTime > 0)
        {
            countDownTime -= 1;
            sliderCountDown.value = countDownTime;
            textCountDown.SetText(countDownTime.ToString());
            SliderRed();
        }
    }
    public void StartCountdown()
    {
        countdownEnable = true;
        countDownTime = startCountDownAt;
        textCountDown.SetText(countDownTime.ToString());
    }
    public void StopCountdown()
    {
        countdownEnable = false;
        textCountDown.SetText(startCountDownAt.ToString());
        sliderCountDown.value = startCountDownAt;
        SliderGreen();
    }
    public void SliderGreen()
    {
        sliderCountDown.GetComponentInChildren<Image>().color = Color.green;
    }
    public void SliderRed()
    {
        sliderCountDown.GetComponentInChildren<Image>().color = Color.red;
    }
}
