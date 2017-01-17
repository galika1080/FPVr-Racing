var com: Vector3;
var rb: Rigidbody;

function Start() {
	rb = GetComponent.<Rigidbody>();
	print(rb.centerOfMass);
	rb.centerOfMass = com;
}