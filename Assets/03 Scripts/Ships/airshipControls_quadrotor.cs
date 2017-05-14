using UnityEngine;
using System.Collections;
using Valve.VR;

public class airshipControls_quadrotor : MonoBehaviour {
	
	public float maxDistance = 1.0f;
	Transform hostobj;
	
	public Rigidbody RF;
	public Rigidbody LF;
	public Rigidbody RB;
	public Rigidbody LB;
	public Rigidbody QC;
	
	public int lvl = 0;
	
	public float pitchMult = 1.0f;
	public float rollMult = 1.0f;
	
	public float throttleMult = 1.0f;
	public float yawMult = 1.0f;
	
	Vector2 touchpad;
	Vector2 trigger;
	//
	SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;
	
	public bool leftController = true;
	
	// Use this for initialization
	void Start () {
		Valve.VR.OpenVR.System.ResetSeatedZeroPose();
	}
	
	void left () {
		if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad) && device.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
				
			//Read the touchpad values
			touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
			
			RF.AddRelativeForce(0, -touchpad.y*pitchMult - touchpad.x*rollMult, 0);
			LF.AddRelativeForce(0, -touchpad.y*pitchMult + touchpad.x*rollMult, 0);
			
			RB.AddRelativeForce(0, touchpad.y*pitchMult - touchpad.x*rollMult, 0);
			LB.AddRelativeForce(0, touchpad.y*pitchMult + touchpad.x*rollMult, 0);	
		}
			
		//if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
		//	Application.LoadLevel(lvl); 
		//}
	}
	
	void right () {	
		RF.AddRelativeForce(0, trigger.x*throttleMult, 0);
		LF.AddRelativeForce(0, trigger.x*throttleMult, 0);
		RB.AddRelativeForce(0, trigger.x*throttleMult, 0);
		LB.AddRelativeForce(0, trigger.x*throttleMult, 0);
		
		if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad) && device.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
			
            //Read the touchpad values
            touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
			
			QC.AddRelativeTorque(0, touchpad.x*yawMult, 0);
			
		}
	}
	
	void FixedUpdate () {
		if(hostobj != null) {
			QC.centerOfMass = new Vector3(0, 0, 0);
			device = SteamVR_Controller.Input((int)controller.index);
			trigger = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
			
			//Debug.Log("A controller is connected...");
			if(leftController)
				left();
			else
				right();
			
			if(Vector3.Distance(transform.position, hostobj.position) > maxDistance) {
				hostobj = null;
				//Debug.Log("Disconnected from controller");
			}
		} else {
			QC.centerOfMass = new Vector3(0, -10, 0);
		}
	}
	
	void GetPickedUp (Transform host) {
		//Debug.Log("A controller connected!");
		hostobj = host;
		controller = host.gameObject.GetComponent<SteamVR_TrackedObject>();
	}
	
	void GetReleased () {
		//hostobj = null;
		//controller = null;
	}
}
