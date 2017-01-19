using UnityEngine;
using System.Collections;
using Valve.VR;

[RequireComponent (typeof (Rigidbody))]

public class interactiveObject : MonoBehaviour {
	
	public bool snapToPose = false;
	
	public Vector3 posedPosition = new Vector3(0,0,0);
	public Vector3 posedRotation = new Vector3(0,0,0);
	
	void GetPickedUp (Transform host) {
		transform.parent = null;
		transform.parent = host;
		GetComponent<Rigidbody>().isKinematic = true;
		if(snapToPose) {
			transform.localPosition = posedPosition;
			transform.localEulerAngles = posedRotation;
		}
	}
	
	void GetReleased () {
		transform.parent = null;
		GetComponent<Rigidbody>().isKinematic = false;
	}
}