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
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in doors)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if(currentDistance < closestDistance){
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }
    public void OpenTheDoor()
    {
        // closestDoor.GetComponentInChildren<ParticleSystem>().Play();
        closestDoor.GetComponent<Animator>().Play("PorteOpen");
        // closestDoor.transform.position = Vector2.MoveTowards(closestDoor.transform.position, new Vector2(closestDoor.transform.position.x,-5f),5f);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Player")
        {
            CameraShake.Instance.ShakeCamera(3f, 0.2f);
        }
    }
}
