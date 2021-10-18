using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    public GameObject[] doors;
    public GameObject closestDoor;

    public static OpenDoor instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    void Start()
    {
        closestDoor = null;
    }
    private void Update() {
        closestDoor = getClosestDoor().gameObject;
    }
    
    public Transform getClosestDoor(){
        
        doors = GameObject.FindGameObjectsWithTag("Porte");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in doors)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(player.transform.position, go.transform.position);
            if(currentDistance < closestDistance){
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }
    public void OpenTheDoor()
    {
        var particuleMain = closestDoor.GetComponentInChildren<ParticleSystem>().main;
        particuleMain.startColor = ColorManager.GetColor(PlayerPrefs.GetString("Difficulty","Easy"));
        closestDoor.GetComponentInChildren<ParticleSystem>().Play();
        closestDoor.GetComponent<Animator>().Play("PorteOpen");
    }
}
