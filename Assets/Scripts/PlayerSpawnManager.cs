using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour {

    public GameObject player;
    public Transform[] spawnPoints;
    private int randIndex;
    // Use this for initialization
    void Awake()
    {
        randIndex = Random.Range(0, 3);
        player.transform.position = spawnPoints[randIndex].position;
        player.transform.forward = spawnPoints[randIndex].forward;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
