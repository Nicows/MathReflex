using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Header("Doors")]
    private GameObject[] _allDoors;
    private GameObject _closestDoor = null;
    private Transform playerTransform;

    private void Start() => playerTransform = Helpers.PlayerTransform;
    private void Update() => GetClosestDoor();
    private void OnEnable() => MultipleGenerator.OnTriggerOpenDoor += OpenTheDoor;
    private void OnDisable() => MultipleGenerator.OnTriggerOpenDoor -= OpenTheDoor;

    public void GetClosestDoor()
    {
        _allDoors = GameObject.FindGameObjectsWithTag("Porte");
        float _closestDistance = Mathf.Infinity;
        // Vector3 _position = transform.position;
        foreach (GameObject _door in _allDoors)
        {
            Vector3 _direction = _door.transform.position - playerTransform.position;
            float _distance = _direction.sqrMagnitude;
            if (_distance < _closestDistance)
            {
                _closestDistance = _distance;
                _closestDoor = _door;
            }
        }
    }
    public void OpenTheDoor()
    {
        if (_closestDoor != null)
        {
            _closestDoor.GetComponent<Animator>().SetTrigger("Open");
            _closestDoor.GetComponent<AudioSource>().pitch = Random.Range(0.7f, 0.9f);
            _closestDoor.GetComponent<AudioSource>().Play();
            PlayParticulesDoor();
        }
    }
    private void PlayParticulesDoor()
    {
        ParticleSystem.MainModule particuleMain = _closestDoor.GetComponentInChildren<ParticleSystem>().main;
        particuleMain.startColor = ColorManager.Instance.GetDifficultyColor();
        _closestDoor.GetComponentInChildren<ParticleSystem>().Play();
    }
}
