using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    
    public float lifetime;

    void Start()
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

            default: break;
        }

        if(LevelGenerator.isALevelInfinite)
            Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
