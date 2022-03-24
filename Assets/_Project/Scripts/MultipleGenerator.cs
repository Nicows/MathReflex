using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MultipleGenerator : MonoBehaviour
{
    [Header("Scripts")]
    public ScoreManager scoreManager;
    public OpenDoor openDoor;

    [Header("Button Pause")]
    public GameObject buttonPause;

    [Header("Text Multiplication")]
    public TMP_Text textMultiplication;

    [Header("Buttons Result")]
    public GameObject groupButtonsResult;
    public Button buttonResult1;
    public Button buttonResult2;
    public Button buttonResult3;
    public Image[] shadowsButtonsResult;

    [Header("Numbers and results")]
    private int _firstNumber;
    private int _secondNumber;
    private int _result;
    private int _falseResult1;
    private int _falseResult2;

    [Header("Multiplication tables")]
    private int _multiplicationTableOf; //0 for infinite
    private int _secondNumberForMultiplicationTable = 1;

    private void Start()
    {
        GetCurrentMultiplicationTable();
    }

    private void GetCurrentMultiplicationTable()
    {
        _multiplicationTableOf = PlayerPrefs.GetInt("Level", 0); //0 for infinite
    }
    public void GenerateNumbers()
    {
        if (LevelGenerator.isALevelInfinite)
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

        if (!LevelGenerator.isALevelInfinite)
            _secondNumberForMultiplicationTable++;
        
    }
    public void DisplayResult()
    {
        EnablePause(false);
        EnableButtonsResult(true);
        EnableTextMultiplication(true);

        List<int> allResults = new List<int>() { _falseResult1, _result, _falseResult2 };
        allResults = ShuffleList(allResults);

        textMultiplication.SetText(_firstNumber + " x " + _secondNumber);
        buttonResult1.GetComponentInChildren<TMP_Text>().SetText(allResults[0].ToString());
        buttonResult2.GetComponentInChildren<TMP_Text>().SetText(allResults[1].ToString());
        buttonResult3.GetComponentInChildren<TMP_Text>().SetText(allResults[2].ToString());
    }
    public void SelectResult(Button button)
    {
        EnableButtonsResult(false);
        EnableTextMultiplication(false);
        EnablePause(true);
        CameraShake.Instance.SetShakeCameraIntensity(5f, 0.1f);
        TimeManager.Instance.StopSlowmotion();

        int resultChosen = int.Parse(button.GetComponentInChildren<TMP_Text>().text);
        CheckResult(resultChosen);
    }
    private void CheckResult(int resultChosen)
    {
        if (resultChosen == _result)
        {
            openDoor.OpenTheDoor();
            if (LevelGenerator.isALevelInfinite)
                scoreManager.AddScore();
        }
    }
    private List<int> ShuffleList(List<int> myList)
    {
        Random.InitState(Random.Range(1, 10000));
        for (int i = 0; i < myList.Count; i++)
        {
            int temp = myList[i];
            int randomIndex = Random.Range(i, myList.Count);
            myList[i] = myList[randomIndex];
            myList[randomIndex] = temp;
        }
        return myList;
    }
    private void EnableTextMultiplication(bool enable)
    {
        textMultiplication.gameObject.SetActive(enable);
    }
    public void EnableButtonsResult(bool enable)
    {
        buttonResult1.gameObject.SetActive(enable);
        buttonResult2.gameObject.SetActive(enable);
        buttonResult3.gameObject.SetActive(enable);

        for (int i = 0; i < shadowsButtonsResult.Length; i++)
        {
            shadowsButtonsResult[i].enabled = enable;
        }
    }
    private void EnablePause(bool enable)
    {
        buttonPause.gameObject.SetActive(enable);
    }

}
