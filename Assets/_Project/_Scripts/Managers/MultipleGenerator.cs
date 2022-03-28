// using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MultipleGenerator : MonoBehaviour
{
    [Header("Text Multiplication")]
    [SerializeField] private TMP_Text _textMultiplication;

    [Header("Buttons Result")]
    [SerializeField] private GameObject _groupButtonsResult;
    [SerializeField] private Button _buttonResult1;
    [SerializeField] private Button _buttonResult2;
    [SerializeField] private Button _buttonResult3;
    [SerializeField] private Image[] _shadowsButtonsResult;

    [Header("Numbers and results")]
    private int _firstNumber;
    private int _secondNumber;
    private int _result;
    private int _falseResult1;
    private int _falseResult2;

    [Header("Multiplication tables")]
    private int _multiplicationTableOf; //0 for infinite
    private int _secondNumberForMultiplicationTable = 1;

    public static event System.Action OnTriggerOpenDoor;
    public static event System.Action OnAddScore;
    public static event System.Action OnEnableButtonPause;
    public static event System.Action OnDisableButtonPause;

    private void Start(){
        GetCurrentMultiplicationTable();
        SetShadowsColor();
    }

    private void SetShadowsColor()
    {
        for (int i = 0; i < _shadowsButtonsResult.Length; i++)
        {
           _shadowsButtonsResult[i].color = ColorManager.Instance.GetDifficultyColor();
        }
    }

    private void OnEnable(){
        TriggerSlowMotion.OnTriggerSlowMotion += GenerateNumbers;
        TimeManager.OnEnableButtonResult += EnableButtonsResult;
    }
    private void OnDisable() {
        TriggerSlowMotion.OnTriggerSlowMotion -= GenerateNumbers;
        TimeManager.OnEnableButtonResult -= EnableButtonsResult;
    }

    private void GetCurrentMultiplicationTable() => _multiplicationTableOf = PlayerPrefs.GetInt("Level", 0); //0 for infinite
    
    public void GenerateNumbers()
    {
        if (LevelGenerator.IsALevelInfinite)
        {
            Random.InitState(Random.Range(1, 10000));
            _firstNumber = Random.Range(1, 10);
            _secondNumber = Random.Range(1, 10);
        }
        else
        {
            _firstNumber = _multiplicationTableOf;
            _secondNumber = _secondNumberForMultiplicationTable;
        }
        _result = _firstNumber * _secondNumber;
        _falseResult1 = _result - Random.Range(1, 5);
        _falseResult2 = _result + Random.Range(1, 5);
        _falseResult1 = (_falseResult1 < 0) ? 0 : _falseResult1;

        DisplayResult();

        if (!LevelGenerator.IsALevelInfinite)
            _secondNumberForMultiplicationTable++;
        
    }
    public void DisplayResult()
    {
        OnDisableButtonPause?.Invoke();
        EnableButtonsResult(true);
        EnableTextMultiplication(true);

        List<int> allResults = new List<int>() { _falseResult1, _result, _falseResult2 };
        allResults = ShuffleList(allResults);

        _textMultiplication.SetText(_firstNumber + " x " + _secondNumber);
        _buttonResult1.GetComponentInChildren<TMP_Text>().SetText(allResults[0].ToString());
        _buttonResult2.GetComponentInChildren<TMP_Text>().SetText(allResults[1].ToString());
        _buttonResult3.GetComponentInChildren<TMP_Text>().SetText(allResults[2].ToString());
    }
    public void SelectResult(Button button)
    {
        EnableButtonsResult(false);
        EnableTextMultiplication(false);
        OnEnableButtonPause?.Invoke();
        CameraShake.Instance.SetShakeCameraIntensity(5f, 0.1f);
        TimeManager.Instance.StopSlowmotion();

        var resultChosen = int.Parse(button.GetComponentInChildren<TMP_Text>().text);
        CheckResult(resultChosen);
    }
    private void CheckResult(int resultChosen)
    {
        if (resultChosen == _result)
        {
            OnTriggerOpenDoor?.Invoke();
            if (LevelGenerator.IsALevelInfinite)
                OnAddScore?.Invoke();
        }
    }
    
    private List<int> ShuffleList(List<int> myList)
    {
        Random.InitState(Random.Range(1, 10000));
        for (int i = 0; i < myList.Count; i++)
        {
            var temp = myList[i];
            var randomIndex = Random.Range(i, myList.Count);
            myList[i] = myList[randomIndex];
            myList[randomIndex] = temp;
        }
        return myList;
    }
    private void EnableTextMultiplication(bool enable)
    {
        _textMultiplication.gameObject.SetActive(enable);
    }

    public void EnableButtonsResult(bool enable)
    {
        _groupButtonsResult.SetActive(enable);
        foreach (Image shadowButton in _shadowsButtonsResult)
        {
            shadowButton.enabled = enable;
        }
    }
    

}
