using UnityEngine;
using System.Collections;

public class objectInteraction : MonoBehaviour {

	SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;
	
	public float radius = 0.1f;
	
	GameObject heldObject;
	
	// Use this for initialization
	void Start () {
		Valve.VR.OpenVR.System.ResetSeatedZeroPose();
		controller = gameObject.GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
		device = SteamVR_Controller.Input((int)controller.index);
		if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
			if(heldObject == null) {
				Collider[] inRange = Physics.OverlapSphere(transform.position, radius);
				int i = 0;
				while (i < inRange.Length) {
					//if(inRange[i].GetComponent<Rigidbody>() != null) {
						inRange[i].BroadcastMessage("GetPickedUp", transform);
						heldObject = inRange[i].gameObject;
					//}
					i++;
				}
			}
		}
		if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
			if(heldObject != null) {
				heldObject.BroadcastMessage("GetReleased");
				heldObject = null;
			}
		}
	}
}
