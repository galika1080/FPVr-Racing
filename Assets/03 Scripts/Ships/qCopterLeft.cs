using UnityEngine;
using System.Collections;
using Valve.VR;

public class qCopterLeft : MonoBehaviour {
	
	public Rigidbody RF;
	public Rigidbody LF;
	public Rigidbody RB;
	public Rigidbody LB;
	
	public int lvl = 0;
	
	public float pitchMult = 1.0f;
	public float rollMult = 1.0f;
	
	Vector2 touchpad;
	
	SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;
	
	// Use this for initialization
	void Start () {
		Valve.VR.OpenVR.System.ResetSeatedZeroPose();
		controller = gameObject.GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
		device = SteamVR_Controller.Input((int)controller.index);
		
		if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad) && device.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
			
            //Read the touchpad values
            touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
			
			RF.AddRelativeForce(0, -touchpad.y*pitchMult - touchpad.x*rollMult, 0);
			LF.AddRelativeForce(0, -touchpad.y*pitchMult + touchpad.x*rollMult, 0);
			
			RB.AddRelativeForce(0, touchpad.y*pitchMult - touchpad.x*rollMult, 0);
			LB.AddRelativeForce(0, touchpad.y*pitchMult + touchpad.x*rollMult, 0);
			
		}
		
		if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
			Application.LoadLevel(lvl); 
		}
	
	}
}
