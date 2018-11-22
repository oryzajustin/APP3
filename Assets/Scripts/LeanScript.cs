using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanScript : MonoBehaviour {

    public Transform _Pivot;

    private float leanSpeed = 100f;
    private float maxAngle = 20f;

    float curAngle = 0f;

    void Awake()
    {
        if (_Pivot == null && transform.parent != null) _Pivot = transform.parent;
    }

    // Update is called once per frame
    void Update () {
		 // lean left
        if (Input.GetKey(KeyCode.Q))
        {
            curAngle = Mathf.MoveTowardsAngle(curAngle, maxAngle, leanSpeed * Time.deltaTime);
        }
        // lean right
        else if (Input.GetKey(KeyCode.E))
        {
            curAngle = Mathf.MoveTowardsAngle(curAngle, -maxAngle,leanSpeed * Time.deltaTime);
        }
        // reset lean
        else
        {
            curAngle = Mathf.MoveTowardsAngle(curAngle, 0f, leanSpeed * Time.deltaTime);
        }

        _Pivot.transform.localRotation = Quaternion.AngleAxis(curAngle, Vector3.forward);
	}
}
