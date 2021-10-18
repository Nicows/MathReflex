using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 25f;

    [SerializeField] private Transform level_Start;
    [SerializeField] private Transform level;
    [SerializeField] private Transform player;
    [SerializeField] private Transform EndLevel;

    private Vector3 lastEndPosition;
    public static bool isALevelInfinite = false;
    private void Awake() {
        if(PlayerPrefs.GetInt("Level", 0) == 0) isALevelInfinite = true;
        else isALevelInfinite = false;
    }
    private void Start()
    {
        
        lastEndPosition = level_Start.Find("EndPosition").position;

        if (isALevelInfinite == false)
        {
            for (int i = 0; i < 10; i++)
            {
                SpawnLevelPart();
                if(i == 9) SpawnEndLevel(lastEndPosition);
                ColorManager.RefreshColor();
            }
        }
        else
        {
            SpawnLevelPart();
            ColorManager.RefreshColor();
        }

    }

    private void Update()
    {
        if (isALevelInfinite)
        {
            if (Vector3.Distance(player.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            {
                SpawnLevelPart();
                ColorManager.RefreshColor();
            }
        }
    }

    public void SpawnLevelPart()
    {
        Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    private Transform SpawnLevelPart(Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(level, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
    private void SpawnEndLevel(Vector3 spawnPosition){
        Transform levelPartTransform = Instantiate(EndLevel, spawnPosition, Quaternion.identity);
        var main = levelPartTransform.GetComponentInChildren<ParticleSystem>().main;
        main.startColor = ColorManager.GetColor(PlayerPrefs.GetString("Difficulty","Easy"));
    }
}
