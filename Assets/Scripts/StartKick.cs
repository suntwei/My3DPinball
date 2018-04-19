using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartKick : MonoBehaviour {

	// Use this for initialization
	private  ArrayList rigBallList = new ArrayList();
	private Transform kicker;

	private float nowPower = 10;
	public float minPower = 10;
	public float maxPower = 120;
	public float perSecPower = 60;

	private Vector3 kickScale;
	void Start () {
		kicker = transform.Find ("Kicker");
		kickScale = kicker.localScale;
		ResetKick ();
	}
	void ResetKick()
	{
		nowPower = minPower;
		kicker.localScale = kickScale;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Space)) 
		{
			nowPower = Mathf.Min(maxPower,nowPower+Time.deltaTime*perSecPower);
			kicker.localScale = kickScale+new Vector3 (0,0,-(nowPower-minPower)/(maxPower-minPower)*0.8f);
		}
		if (Input.GetKeyUp (KeyCode.DownArrow) || Input.GetKeyUp (KeyCode.Space)) 
		{
			//喷射球
			Debug.Log("key up"+rigBallList.Count);
			foreach(Rigidbody rig in rigBallList)
			{
				print (rig.name);
				rig.velocity = new Vector3 (0,0, nowPower);
			}

			ResetKick ();
		}
	}
	void OnTriggerEnter(Collider _c) {
		print ("OnTriggerEnter" + _c.name);
		rigBallList.Add(_c.gameObject.GetComponent<Rigidbody>());
	}

	void OnTriggerExit(Collider _c) {
		print ("OnTriggerExit" + _c.name);
		rigBallList.Remove(_c.gameObject.GetComponent<Rigidbody>());
	}

	void KickBall()
	{
		
	}
}
