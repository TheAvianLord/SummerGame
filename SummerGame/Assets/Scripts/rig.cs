using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rig : MonoBehaviour {

	GameObject pObj;

	// Use this for initialization
	void Start () {
		pObj = GameObject.Find("Player");
	}

	//Vector2 pPos = new Vector3(pObj.transform.position.x, pObj.transform.position.y, 0.0F);
	//Vector2 camPos = transform.position;
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(pObj.transform.position.x, pObj.transform.position.y, this.transform.position.z);
		this.transform.localEulerAngles = pObj.transform.rotation.eulerAngles;
	}
}


//Come up with a method that will move the camera ()