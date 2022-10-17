using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadsGenerator : MonoBehaviour
{
    public GameObject[] availabeRoads;
    private List<GameObject> currentRoads;

    Transform plane;
    private float planeSize;

    private GameObject hero;

    void Start()
    {
        currentRoads = new List<GameObject>();    
        hero = GameObject.Find("Hero");

        CreateFirstRoad();
      
    }
    // Update is called once per frame
    void Update()
    {
        bool isDie = hero.GetComponent<PlayerInteraction>().isDie;
        if (isDie) return;
        float heroPos = hero.transform.position.z;
        float lastRoad = currentRoads[currentRoads.Count - 1].transform.position.z;
               
        if (heroPos > lastRoad - planeSize*plane.transform.localScale.z)
        {
            RemovePrevRoad();
            CreateNextRoad();
        }      
    }
    void CreateNextRoad()
    {    

        Transform curRoad = currentRoads[currentRoads.Count - 1].transform;
        plane = curRoad.Find("Plane");
        planeSize = plane.GetComponent<BoxCollider>().size.z;
        Vector3 pos = curRoad.position;
        pos.z += planeSize * plane.transform.localScale.z;
        Quaternion rot = curRoad.rotation;
       
        int index = Random.Range(0, availabeRoads.Length);
        GameObject road = Instantiate(availabeRoads[index], pos, rot);
        currentRoads.Add(road);
    }
    public void RemovePrevRoad()
    {
        if (currentRoads.Count > 2) { 
            Destroy(currentRoads[0]);
            currentRoads.RemoveAt(0);
        }
 
    }
    public void CreateFirstRoad()
    {
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.Euler(0f, 0f, 0f);
        int index = Random.Range(0, availabeRoads.Length);

        GameObject road = Instantiate(availabeRoads[index], pos, rot);
        road.GetComponent<Generator>().isFisrtRoad = true;
        currentRoads.Add(road);

        Transform curRoad = currentRoads[currentRoads.Count - 1].transform;
        plane = curRoad.Find("Plane");
        planeSize = plane.GetComponent<BoxCollider>().size.z;
       

    }
}
