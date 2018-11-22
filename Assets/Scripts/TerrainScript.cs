using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (Transform t in transform)
        {
            t.gameObject.tag = "Terrain";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
