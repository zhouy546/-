using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TochTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Touch myTouch = Input.GetTouch(0);

        Touch[] myTouches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++)
        {
            Debug.Log(myTouches[i].position);
        }
    }
}
