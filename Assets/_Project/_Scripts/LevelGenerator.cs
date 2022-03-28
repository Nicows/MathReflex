using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_GENERATE_LEVEL_PART = 20f;

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _levelStart;
    [SerializeField] private Transform _levelPart;
    [SerializeField] private Transform _levelEnd;
    private Vector3 _lastEndPosition;
    public static bool IsALevelInfinite = false;

    private void Awake()
    {
        CheckIfLevelInfinite();
    }

    private void CheckIfLevelInfinite()
    {
        if (PlayerPrefs.GetInt("Level", 0) == 0) IsALevelInfinite = true;
        else IsALevelInfinite = false;
    }
    private void Start()
    {
        GenerateStartLevel();
        ColorManager.Instance.RefreshColorDifficultyInLevelComponents();
    }
    private void GenerateStartLevel()
    {
        _lastEndPosition = _levelStart.Find("EndPosition").position;
        if (IsALevelInfinite) GenerateLevelPart();
        else
        {
            for (int i = 0; i < 10; i++)
            {
                GenerateLevelPart();
                if (i == 9) GenerateEndLevel(_lastEndPosition);
            }
        }
    }
    private void Update()
    {
        CheckDistanceToGenerateLevelPart();
    }
    private void CheckDistanceToGenerateLevelPart()
    {
        if (!IsALevelInfinite) return;
        var distance = Vector3.Distance(_player.position, _lastEndPosition);
        if (distance < PLAYER_DISTANCE_GENERATE_LEVEL_PART)
        {
            GenerateLevelPart();
            ColorManager.Instance.RefreshColorDifficultyInLevelComponents();
        }

    }
    public void GenerateLevelPart()
    {
        var lastLevelPartTransform = Instantiate(_levelPart, _lastEndPosition, Quaternion.identity);
        _lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }
    private void GenerateEndLevel(Vector3 spawnPosition)
    {
        var levelPartTransform = Instantiate(_levelEnd, spawnPosition, Quaternion.identity);
        ParticleSystem.MainModule main = levelPartTransform.GetComponentInChildren<ParticleSystem>().main;
        main.startColor = ColorManager.Instance.GetDifficultyColor();
    }
}
