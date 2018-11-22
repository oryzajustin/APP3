using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler : MonoBehaviour {
    public GameObject[] portals;
    private int randIndex;
	// Use this for initialization
	void Start () {
        randIndex = Random.Range(0, 3);
        portals[randIndex].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
