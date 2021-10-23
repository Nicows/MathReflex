using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [Header ("Move Spot")]
    public Transform moveSpot;
    private const float MIN_X = -10.6f;
    private const float MAX_X = 10.6f;
    private const float MIN_Y = -4.9f;
    private const float MAX_Y = 4.9f;

    [Header ("Movements")]
    private const float SPEED = 2f;
    private const float WAIT_TIME  = 3f;
    private float waiting = WAIT_TIME;
    private bool stopMoving = false;

    private void Start()
    {
        SelectRandomMoveSpot();
    }

    private void Update()
    {
        CheckMoving();
    }
    private void CheckMoving(){
        if (stopMoving == false)
        {
            MoveToMoveSpot();
            if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
            {
                if (waiting <= 0)
                {
                    SelectRandomMoveSpot();
                    waiting = WAIT_TIME;
                }
                else
                {
                    waiting -= Time.deltaTime;
                }
            }
        }
    }
    private void MoveToMoveSpot(){
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, SPEED * Time.deltaTime);
    }

    private void SelectRandomMoveSpot(){
        moveSpot.position = new Vector2(Random.Range(MIN_X, MAX_X), Random.Range(MIN_Y, MAX_Y));
    }

}