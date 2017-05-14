var pitch = 0.0;
var gliding = 0.0;
var glideMult = 2.5;
var speed = 0.0;

function Start () {
	GetComponent.<Rigidbody>().isKinematic = true;
	Valve.VR.OpenVR.System.ResetSeatedZeroPose();
	yield WaitForSeconds(5);
	Valve.VR.OpenVR.System.ResetSeatedZeroPose();
	GetComponent.<Rigidbody>().isKinematic = false;
}

function FixedUpdate () {
var locVel = transform.InverseTransformDirection(GetComponent.<Rigidbody>().velocity);
speed = locVel.z;
pitch = transform.localEulerAngles.x + 10;

if(pitch < 30) {
	gliding = Mathf.Clamp(pitch/30, 0.0, 1.0);
	//gliding = 0;
	} else if(pitch > 90) {
	gliding = 0;
	} else {
	gliding = Mathf.Clamp(Mathf.Abs((90 - (pitch - 30))/90), 0.0, 1.0);
	}


print(locVel.magnitude);
locVel.y = locVel.y * Mathf.Clamp(10.0/(locVel.magnitude+0.01), 0.0, 1.0);
GetComponent.<Rigidbody>().AddRelativeForce(-locVel.x*0.85, 0, gliding * glideMult);


transform.localEulerAngles.z = 0;
if(transform.localEulerAngles.x < -15)
	transform.localEulerAngles.x = -15;

		// input stuff

if(Input.GetKey('w'))
	GetComponent.<Rigidbody>().AddRelativeTorque(7, 0, 0);
if(Input.GetKey('s'))
	GetComponent.<Rigidbody>().AddRelativeTorque(-7, 0, 0);
if(Input.GetKey('a')) {
	GetComponent.<Rigidbody>().AddRelativeTorque(0, -40, 0);
	} else if(Input.GetKey('d')) {
	GetComponent.<Rigidbody>().AddRelativeTorque(0, 40, 0);
	}
//print(pitch);
GetComponent.<Rigidbody>().velocity = transform.TransformDirection(locVel);
}


var hand1 : Transform;
var hand2 : Transform;
var head : Transform;
var processedInput : Vector3;
var rollMultiplier = 1.0;
var yawMultiplier = 1.0;
var pitchMultiplier = 1.0;

function Update () {
if (Input.GetMouseButtonDown(0)){
    Valve.VR.OpenVR.System.ResetSeatedZeroPose();
	}
processedInput = Vector3(0.15*(hand1.localPosition.z + hand2.localPosition.z) + 0.7*head.localPosition.z, 0, 0.15*(hand1.localPosition.x + hand2.localPosition.x) + 0.7*head.localPosition.x);
transform.Rotate(0, processedInput.z * yawMultiplier * Time.deltaTime, 0);
transform.localEulerAngles = Vector3(pitchMultiplier * processedInput.x, transform.localEulerAngles.y, rollMultiplier * -processedInput.z);
}