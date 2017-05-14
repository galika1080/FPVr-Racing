using UnityEngine;
using System.Collections;

public class turnquadcopter : MonoBehaviour {

	public int turnspeed = 10;
	
	void Start () {
		Valve.VR.OpenVR.System.ResetSeatedZeroPose();
	}
	
	void Update () {
		if(Input.GetKey("left")) {
			GetComponent<Rigidbody>().AddRelativeTorque(0, -turnspeed, 0);
		} else if(Input.GetKey("right")) {
			GetComponent<Rigidbody>().AddRelativeTorque(0, turnspeed, 0);
		}
	}
}