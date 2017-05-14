using UnityEngine;
using System.Collections;

public class interactiveObject_pivoting : MonoBehaviour {
	
	Transform hostobj;
	public float maxDistance = 1.0f;
	public Vector3 hostOffset = new Vector3(0,0,0);
	
	void Update () {
		if(hostobj != null) {
			transform.LookAt(hostobj.position + hostOffset);
			if(Vector3.Distance(transform.position, hostobj.position) > maxDistance) {
				hostobj = null;
			}
		}
	}
	
	void GetPickedUp (Transform host) {
		hostobj = host;
	}
	
	void GetReleased () {
		hostobj = null;
	}
	
	public Transform getHost () {
		return hostobj;
	}
}
