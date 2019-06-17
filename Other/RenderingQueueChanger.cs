using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderingQueueChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var material = GetComponent<Renderer>().material;
        material.renderQueue = -1;
	}
	
}
