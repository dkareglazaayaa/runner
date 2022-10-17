using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] availableCoins;
    private List<GameObject> currentCoins;

    public GameObject[] availableObstacles;
    private List<GameObject> currentObstacles;

    private int coinsNum;
    private int obstsNum;

    private float[] lines;

    Transform plane;
    private Vector3 roadSize;
    private Vector3 roadScale;
    private Vector3 roadPos;

    private GameObject hero;

    public int minCoinsNum, maxCoinsNum;
    public int minObstsNum, maxObstsNum;

    public bool isFisrtRoad = false;
    void Start()
    {
        currentCoins = new List<GameObject>();
        currentObstacles = new List<GameObject>();
        hero = GameObject.Find("Hero");
        var script = hero.GetComponent(typeof(PlayerMove)) as PlayerMove;
        lines = new float[3];     
        float offset = script.offset;
        lines[1]=script.startPos.x;
        lines[0] = lines[1] - offset;
        lines[2] = lines[1] + offset;
        Transform plane = transform.Find("Plane");
        roadSize = plane.transform.GetComponent<BoxCollider>().size;
        roadScale = plane.localScale;
        roadPos = transform.position;

        coinsNum = Random.Range(minCoinsNum, maxCoinsNum);
        obstsNum = Random.Range(minObstsNum, maxCoinsNum);
        for (int i = 0; i < coinsNum; i++)
        {
            CoinsGenrtr();
        }
        for(int i = 0; i < obstsNum; i++)
        {
            ObstaclesGenrtr();
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        bool isDie = hero.GetComponent<PlayerInteraction>().isDie;
        if (isDie) return;
    }
    public void CoinsGenrtr()
    {
        bool isDie = hero.GetComponent<PlayerInteraction>().isDie;
        if (isDie) return;
        int index = Random.Range(0,3);
        float x = lines[index] * Time.fixedDeltaTime;
        float y = roadPos.y + 1;
        float z = Random.Range(roadPos.z - roadSize.z * roadScale.z / 2, roadPos.z + roadSize.z* roadScale.z / 2);
        if (isFisrtRoad)
        {
            z = Random.Range(roadPos.z - roadSize.z * roadScale.z / 2 + 5, roadPos.z + roadSize.z * roadScale.z / 2);
        }
        index = Random.Range(0, availableCoins.Length);

        GameObject coin=Instantiate(availableCoins[index], new Vector3(x, y, z), new Quaternion(0, 0, 0, 0));
        currentCoins.Add(coin);
    }
    public void ObstaclesGenrtr()
    {
        int index = Random.Range(0, 3);
        float x = lines[index] * Time.fixedDeltaTime;  
        float z = Random.Range(roadPos.z - roadSize.z * roadScale.z / 2, roadPos.z + roadSize.z * roadScale.z / 2);
        if (isFisrtRoad)
        {
            Debug.Log("First");
            z= Random.Range(roadPos.z - roadSize.z * roadScale.z / 2 + 50, roadPos.z + roadSize.z * roadScale.z / 2);
        }
   
        index = Random.Range(0, availableObstacles.Length); 
        float y = availableObstacles[index].transform.position.y;
        Quaternion rot= availableObstacles[index].transform.rotation;
        GameObject obs = Instantiate(availableObstacles[index], new Vector3(x, y, z), rot);
        currentObstacles.Add(obs);
    }
    void OnDestroy()
    {    
        while (currentCoins.Count!=0)
        {
            Destroy(currentCoins[0]);
            currentCoins.RemoveAt(0);
        }
        while (currentCoins.Count != 0)
        {
            Destroy(currentObstacles[0]);
            currentObstacles.RemoveAt(0);
        }

    }
}
