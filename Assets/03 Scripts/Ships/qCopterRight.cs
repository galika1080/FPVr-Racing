using UnityEngine;
using System.Collections;
using Valve.VR;

public class qCopterRight : MonoBehaviour {
	
	public Rigidbody RF;
	public Rigidbody LF;
	public Rigidbody RB;
	public Rigidbody LB;
	public Rigidbody QC;
	
	public int lvl = 0;
	
	public float throttleMult = 1.0f;
	public float yawMult = 1.0f;
	
	Vector2 touchpad;
	Vector2 trigger;
	
	SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;
	
	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
		device = SteamVR_Controller.Input((int)controller.index);
		
		trigger = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
			
		Debug.Log(trigger);
			
		RF.AddRelativeForce(0, trigger.x*throttleMult, 0);
		LF.AddRelativeForce(0, trigger.x*throttleMult, 0);
		RB.AddRelativeForce(0, trigger.x*throttleMult, 0);
		LB.AddRelativeForce(0, trigger.x*throttleMult, 0);
		
		if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad) && device.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
			
            //Read the touchpad values
            touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
			
			QC.AddRelativeTorque(0, touchpad.x*yawMult, 0);
			
		}
		
		//if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
			//Application.LoadLevel(lvl);
		//}
	}
}
