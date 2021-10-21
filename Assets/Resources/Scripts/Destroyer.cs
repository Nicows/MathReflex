using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    [SerializeField] private float lifetime = 15;

    void Start()
    {
        LifetimeOverDifficulty();
    }
    private void LifetimeOverDifficulty()
    {
        string currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        switch (currentDifficulty)
        {
            case "Facile":
            case "Easy":
                lifetime = 15;
                break;

            case "Normal":
                lifetime = 10;
                break;

            case "Difficile":
            case "Hard":
                lifetime = 6;
                break;

            default:
                lifetime = 15;
                break;
        }
        if (LevelGenerator.isALevelInfinite)
            Destroy(gameObject, lifetime);
    }
}
