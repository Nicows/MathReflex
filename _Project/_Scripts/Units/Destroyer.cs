using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 35f;

    private void Update() => DestroyAfterDistance();

    private void DestroyAfterDistance()
    {
        if(!LevelGenerator.IsALevelInfinite) return;
        if (Vector3.Distance(transform.position, Helpers.PlayerTransform.position) > _maxDistance)
            Destroy(gameObject);
    }
}
