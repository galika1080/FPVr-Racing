﻿using UnityEngine;
using System.Collections;
using Valve.VR;

[RequireComponent (typeof (Rigidbody))]

public class interactiveObject : MonoBehaviour {
	
	public bool snapToPose = false;
	
	private bool up = false;
	
	public Vector3 posedPosition = new Vector3(0,0,0);
	public Vector3 posedRotation = new Vector3(0,0,0);
	
	public void GetPickedUp (Transform host) {
		transform.parent = null;
		transform.parent = host;
		//GetComponent<Rigidbody>().isKinematic = true;
		if(snapToPose) {
			transform.localPosition = posedPosition;
			transform.localEulerAngles = posedRotation;
		}
		GetComponent<Rigidbody>().useGravity = false;
		up = true;
	}
	
	void FixedUpdate () {
		if(up) {
			GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
		}
	}
	
	void GetReleased () {
		transform.parent = null;
		GetComponent<Rigidbody>().useGravity = true;
		//GetComponent<Rigidbody>().isKinematic = false;
		up = false;
	}
}