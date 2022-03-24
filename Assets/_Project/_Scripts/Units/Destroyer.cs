using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float _lifetime = 15;

    void Start()
    {
        LifetimeOverDifficulty();
    }
    private void LifetimeOverDifficulty()
    {
        string currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        switch (currentDifficulty)
        {
            case "Easy":
                _lifetime = 15;
                break;
            case "Normal":
                _lifetime = 10;
                break;
            case "Hard":
                _lifetime = 6;
                break;
            default:
                _lifetime = 15;
                break;
        }
        if (LevelGenerator.isALevelInfinite)
            Destroy(gameObject, _lifetime);
    }
}
