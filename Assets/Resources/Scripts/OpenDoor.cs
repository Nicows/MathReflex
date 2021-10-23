using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Header ("Doors")]
    private GameObject[] allDoors;
    private GameObject closestDoor = null;

    private void Update() {
        getClosestDoor();
    }
    
    public void getClosestDoor(){
        
        allDoors = GameObject.FindGameObjectsWithTag("Porte");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float closestDistance = Mathf.Infinity;
        GameObject currentClosestDoor = null;

        foreach (GameObject door in allDoors)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(player.transform.position, door.transform.position);
            if(currentDistance < closestDistance){
                closestDistance = currentDistance;
                currentClosestDoor = door;
            }
        }
        closestDoor = currentClosestDoor;
    }
    public void OpenTheDoor()
    {
        closestDoor.GetComponent<Animator>().Play("PorteOpen");
        closestDoor.GetComponent<AudioSource>().Play();
        PlayParticulesDoor();
    }
    private void PlayParticulesDoor(){
        ParticleSystem.MainModule particuleMain = closestDoor.GetComponentInChildren<ParticleSystem>().main;
        particuleMain.startColor = ColorManager.colorDifficulty;
        closestDoor.GetComponentInChildren<ParticleSystem>().Play();
    }
}
