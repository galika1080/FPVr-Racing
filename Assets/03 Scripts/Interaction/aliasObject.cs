using UnityEngine;
using System.Collections;

public class aliasObject : MonoBehaviour {
	
	private MeshRenderer rend;
	public bool inPlace = false;
	
	public Collider objectCol;
	public Collider targetCol;
	public GameObject realObj;
	public bool clone = false;
	public string targetName;
	
	void Start () {
		rend = (MeshRenderer)gameObject.GetComponent<MeshRenderer>();
	}
	
	IEnumerator GetPickedUp (Transform host) {
		if(!clone && !inPlace) yield break;
		
		GameObject real = (GameObject)Instantiate(realObj, transform.position, transform.rotation);
		real.GetComponent<interactiveObject>().GetPickedUp(host);
		
		if(!clone) {
			rend.enabled = false;
			objectCol.enabled = false;
			targetCol.enabled = true;
			inPlace = false;
		}
		yield return new WaitForSeconds(0.5F);
		real.name = targetName;
	}
	
	void OnCollisionEnter(Collision col) {
		if(inPlace) return;
		if(col.gameObject.name == targetName) {
			GetPlaced(col.gameObject);
		}
	}
	
	void GetPlaced (GameObject g) {
		if(!(g.GetComponent<magazine>() == null || gameObject.GetComponent<magazineAlias>() == null)) {
			gameObject.GetComponent<magazineAlias>().setAmmo(g.GetComponent<magazine>().getAmmo());
		}
		Destroy(g);
		rend.enabled = true;
		objectCol.enabled = true;
		targetCol.enabled = false;
		inPlace = true;
	}
}