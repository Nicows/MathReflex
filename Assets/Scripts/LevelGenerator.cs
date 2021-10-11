using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 13f;

    [SerializeField] private Transform level_Start;
    [SerializeField] private Transform level;
    [SerializeField] private Transform player;

    private Vector3 lastEndPosition;

    private void Awake() {
        lastEndPosition = level_Start.Find("EndPosition").position;

        SpawnLevelPart();
    }

    private void Update() {
        if(Vector3.Distance(player.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART){
            SpawnLevelPart();
        }
        
    }

    public void SpawnLevelPart(){
       Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition);
       lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    private Transform SpawnLevelPart(Vector3 spawnPosition){
        Transform levelPartTransform = Instantiate(level, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
