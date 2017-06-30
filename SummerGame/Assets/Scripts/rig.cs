using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rig : MonoBehaviour {

	GameObject pObj;
	Rigidbody2D thisBody;
	Rigidbody2D pBody;

	// Use this for initialization
	void Start () {
		pObj = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

		pBody = pObj.GetComponent<Rigidbody2D>();
		thisBody = this.GetComponent<Rigidbody2D>();
		Vector2 posDiff = pObj.transform.position - this.transform.position;
		thisBody.velocity = posDiff;
		//this.transform.localEulerAngles = pObj.transform.rotation.eulerAngles;
	}
}