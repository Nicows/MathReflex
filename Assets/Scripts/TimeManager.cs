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
    public float countDownTime;
    private float nextUpdate = 1;
    private float frequenceUpdate = 1f;

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

            default:
                startCountDownAt = 10;
                slowdownLength = 10;
                break;
        }
    }
    private void Start()
    {
        sliderCountDown.maxValue = startCountDownAt;
        sliderCountDown.GetComponentInChildren<Image>().color = ColorManager.colorDifficulty;
        sliderCountDown.gameObject.SetActive(false);
        nextUpdate = frequenceUpdate;
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
                nextUpdate = Mathf.FloorToInt(Time.unscaledTime) + frequenceUpdate;
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
            countDownTime -= frequenceUpdate;
            sliderCountDown.value = countDownTime;
            textCountDown.SetText(countDownTime.ToString());
        }
    }
    public void StartCountdown()
    {
        countdownEnable = true;
        countDownTime = startCountDownAt;
        textCountDown.gameObject.SetActive(true);
        textCountDown.SetText(countDownTime.ToString());
        sliderCountDown.gameObject.SetActive(true);
    }
    public void StopCountdown()
    {
        countdownEnable = false;
        textCountDown.SetText(startCountDownAt.ToString());
        sliderCountDown.value = startCountDownAt;
        textCountDown.gameObject.SetActive(false);
       sliderCountDown.gameObject.SetActive(false);
    }
}
