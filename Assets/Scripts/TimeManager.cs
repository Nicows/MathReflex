using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 10f;
    public bool slowmotionEnable = false;
    public float slowmotionTime;

    public static TimeManager instance;
    public MultipleGenerator multipleGenerator;

    public TMP_Text textCountDown;
    public bool countdownEnable = false;
    public int startCountDownAt = 10;
    public int countDownTime;
    private int nextUpdate = 1;

    private void Awake() {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
    }
    private void Start() {
        
    }
    private void Update()
    {
        if(slowmotionEnable){
            if (Time.unscaledTime >= slowmotionTime)
            {
                StopSlowmotion();
            }
        }
        if(countdownEnable){
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
    public void StopSlowmotion(){
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.deltaTime;
        slowmotionEnable = false;
    }
    public void CountDown(){
        
        if (countDownTime > 0)
        {
            countDownTime -= 1;
            textCountDown.SetText(countDownTime.ToString());
        }      
    }
    public void StartCountdown(){
        countdownEnable = true;
        countDownTime = startCountDownAt;
        textCountDown.SetText(countDownTime.ToString());
    }
    public void StopCountdown(){
        countdownEnable = false;
        countDownTime = startCountDownAt;
        textCountDown.SetText(countDownTime.ToString());
    }
}
