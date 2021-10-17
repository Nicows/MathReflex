using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MultipleGenerator : MonoBehaviour
{

    public int firstNumber;
    public int secondNumber;
    public int result;
    public int falseResult1;
    public int falseResult2;
    public TMP_Text textCalcul;
    public Button buttonReponse1;
    public Button buttonReponse2;
    public Button buttonReponse3;
    public GameObject buttonGroup;

    public ScoreManager scoreManager;
    public OpenDoor openDoor;

    public static int multiplicationTableOf; //0 for infinite
    public int numberForMultiplicationTable = 1;

    private void Awake() {
        multiplicationTableOf = PlayerPrefs.GetInt("Level", 0); //0 for infinite
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetNumbers()
    {   
        if(multiplicationTableOf == 0){
            firstNumber = Random.Range(1, 10);
            secondNumber = Random.Range(1, 10);
        }else{
            firstNumber = multiplicationTableOf;
            secondNumber = numberForMultiplicationTable;  
        }
        result = firstNumber * secondNumber;
        falseResult1 = result - Random.Range(1, 5);
        if (falseResult1 < 0)
        {
            falseResult1 = 0;
        }
        falseResult2 = result + Random.Range(1, 5);
        EnableButtons(true);
        DisplayResult();
        
        if(multiplicationTableOf != 0){
            numberForMultiplicationTable++;
        }
        
    }
    
    public void DisplayResult()
    {
        List<int> myList = new List<int>() { falseResult1, result, falseResult2 };

        //range la liste aléatoirement
        for (int i = 0; i < myList.Count; i++)
        {
            int temp = myList[i];
            int randomIndex = Random.Range(i, myList.Count);
            myList[i] = myList[randomIndex];
            myList[randomIndex] = temp;
        }

        //affiche le calcul et les résulats
        textCalcul.SetText(firstNumber + "x" + secondNumber + " = " + result);
        buttonReponse1.GetComponentInChildren<TMP_Text>().SetText(myList[0].ToString());
        buttonReponse2.GetComponentInChildren<TMP_Text>().SetText(myList[1].ToString());
        buttonReponse3.GetComponentInChildren<TMP_Text>().SetText(myList[2].ToString());
    }

    public void CheckResult(Button button)
    {
        EnableButtons(false);

        int resultChosen = int.Parse(button.GetComponentInChildren<TMP_Text>().text);
        CameraShake.Instance.ShakeCamera(5f, 0.1f);
        TimeManager.instance.StopSlowmotion();
        TimeManager.instance.StopCountdown();

        if (resultChosen == result)
        {
            openDoor.OpenTheDoor();
            if(LevelGenerator.isALevelInfinite)
                scoreManager.AddScore();
        }
    }
    
    public void EnableButtons(bool enable)
    {
        buttonGroup.SetActive(enable);
    }
    

}
