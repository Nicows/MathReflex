using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_GENERATE_LEVEL_PART = 25f;

    public Transform player;
    public Transform levelStart;
    public Transform levelPart;
    public Transform levelEnd;
    private Vector3 lastEndPosition;
    public static bool isALevelInfinite = false;

    private void Awake()
    {
        CheckIfLevelInfinite();
    }
    private void CheckIfLevelInfinite()
    {
        if (PlayerPrefs.GetInt("Level", 0) == 0) isALevelInfinite = true;
        else isALevelInfinite = false;
    }
    private void Start()
    {
        GenerateStartLevel();
        ColorManager.Instance.RefreshColorDifficultyInAllComponents();
    }
    private void GenerateStartLevel()
    {
        lastEndPosition = levelStart.Find("EndPosition").position;
        if (isALevelInfinite) GenerateLevelPart();
        else
        {
            for (int i = 0; i < 10; i++)
            {
                GenerateLevelPart();
                if (i == 9) GenerateEndLevel(lastEndPosition);
            }
        }
    }
    private void Update()
    {
        CheckDistanceToGenerateLevelPart();
    }
    private void CheckDistanceToGenerateLevelPart(){
        if (isALevelInfinite)
        {
            if (Vector3.Distance(player.position, lastEndPosition) < PLAYER_DISTANCE_GENERATE_LEVEL_PART)
            {
                GenerateLevelPart();
                ColorManager.Instance.RefreshColorDifficultyInAllComponents();
            }
        }
    }
    public void GenerateLevelPart()
    {

        Transform lastLevelPartTransform  = Instantiate(levelPart, lastEndPosition, Quaternion.identity);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    private void GenerateEndLevel(Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelEnd, spawnPosition, Quaternion.identity);
        ParticleSystem.MainModule main = levelPartTransform.GetComponentInChildren<ParticleSystem>().main;
        main.startColor = ColorManager.Instance.GetDifficultyColor();
    }
}
