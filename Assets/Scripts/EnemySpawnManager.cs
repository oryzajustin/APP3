using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {

    public Transform[] enemySpawnPoints;
    public GameObject[] enemies;
    //private int randIndex;
	// Use this for initialization
	void Awake () {
        //for (int i = 0; i < 3; i++){
            //randIndex = Random.Range(0, 2);
        enemies[0].transform.position = enemySpawnPoints[0].position;
        enemies[1].transform.position = enemySpawnPoints[1].position;
        enemies[2].transform.position = enemySpawnPoints[2].position;
        //}
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
