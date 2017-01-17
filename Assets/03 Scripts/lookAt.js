var target : Transform;

var leadTime = 0.0;
var aimError = 0.0;

function Update () {
var error : Vector3 = Vector3((Random.value - 0.5)*aimError, (Random.value - 0.5)*aimError, (Random.value - 0.5)*aimError);
var rb : Rigidbody = target.GetComponent.<Rigidbody>();
var predicted : Vector3 = target.position + rb.velocity * leadTime;
transform.LookAt(predicted + error);
}