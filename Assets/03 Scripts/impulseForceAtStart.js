var force : Vector3;
var delay = 0.0;
var relative = false;
var killVelocity = false;

function Start () {
yield WaitForSeconds(delay);

if(killVelocity)
	GetComponent.<Rigidbody>().velocity = Vector3(0,0,0);

if(relative)
	GetComponent.<Rigidbody>().AddRelativeForce(force);
else
	GetComponent.<Rigidbody>().AddForce(force);
}