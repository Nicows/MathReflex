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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetNumbers()
    {
        firstNumber = Random.Range(1, 10);
        secondNumber = Random.Range(1, 10);
        result = firstNumber * secondNumber;
        falseResult1 = result - Random.Range(1, 5);
        if (falseResult1 < 0)
        {
            falseResult1 = 0;
        }
        falseResult2 = result + Random.Range(1, 5);
        EnableButtons(true);
        DisplayResult();
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
        CameraShake.Instance.ShakeCamera(2f, 0.05f);
        TimeManager.instance.StopSlowmotion();
        TimeManager.instance.StopCountdown();

        if (resultChosen == result)
        {
            openDoor.OpenTheDoor();
            scoreManager.AddScore();
        }
    }
    
    public void EnableButtons(bool enable)
    {
        buttonGroup.SetActive(enable);
    }
    

}
