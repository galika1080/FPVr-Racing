var target : Transform;
var range = 10.0;
var radius = 10.0;
var power = 10.0;
var explosionPrefab : GameObject;
var destroyOnExplode = true;

function Update () {
if(Vector3.Distance(target.position, transform.position) < range)
	StartCoroutine("explode");
}

function explode () {
var clone : GameObject;
clone = Instantiate(explosionPrefab, transform.position, transform.rotation);
var explosionPos : Vector3 = transform.position;
var colliders : Collider[] = Physics.OverlapSphere(explosionPos, radius);
for (var hit: Collider in colliders) {
	var rb : Rigidbody = hit.GetComponent.<Rigidbody>();
	if (rb != null) {
		rb.AddExplosionForce(power, explosionPos, radius, 3.0);
		print("BOOM");
		}
	}
if(destroyOnExplode)
	Destroy(gameObject);
}