var time = 0.0;

function Start () {
yield WaitForSeconds(time);
Destroy(gameObject);
}