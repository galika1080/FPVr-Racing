using UnityEngine;
using System.Collections;

public class quadcopterMotorSimple : MonoBehaviour {

	public string upKey = "w";
	public string downKey = "s";
	
	public int regular = 10;
	public int lower = 7;
	public int higher = 13;

	void FixedUpdate () {
		if(Input.GetKey(upKey)) {
			GetComponent<Rigidbody>().AddRelativeForce(0, higher, 0);
		} else if(Input.GetKey(downKey)) {
			GetComponent<Rigidbody>().AddRelativeForce(0, lower, 0);
		} else { GetComponent<Rigidbody>().AddRelativeForce(0, regular, 0); }
	}
}
