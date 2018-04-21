using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Vector3 prevPos;
	public Vector3 oldPos;
	private Rigidbody rig;
	void Start () {
		rig = GetComponent<Rigidbody> ();
		prevPos = transform.position;
		oldPos = transform.position;
	}

	void FixedUpdate() {
		FixPos();
	}
	void OnCollisionEnter(Collision collision)
	{
		print ("wells==>>collision.name = "+collision.transform.name);
		if (collision.transform.CompareTag("dead")) {
			//dead
			BlinkTo(oldPos);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		print ("wells==>>other.name = "+other.name);
		print ("wells==>>other.tag = "+other.tag);
		if (other.transform.CompareTag("dead")) {
			//dead
			BlinkTo(oldPos);
		}
	}

	public void FixPos() {
//		Ray ray = new Ray(prevPos, transform.position - prevPos);
//		RaycastHit hit;
//		if (Physics.Raycast(ray, out hit, Vector3.Distance(prevPos, transform.position))) {
//			if (!hit.transform.CompareTag("Ball") && !hit.collider.isTrigger) {
//				Vector3 _fix = (prevPos - hit.point).normalized * 0.6f;
//				transform.position = hit.point + _fix;
//				rig.velocity *= -0.8f;
//			}
//		}
	}


	public void BlinkTo(Vector3 _pos) {
		prevPos = transform.position = _pos;
	}
}
