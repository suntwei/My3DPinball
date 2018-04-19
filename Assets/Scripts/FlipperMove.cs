using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperMove : MonoBehaviour {

	public bool isRight = true;
	public int moveAngle = 40;
	public int moveSpeed = 40;
	public int dotNum = 18;

	private Transform flipTs;
	private Quaternion oldRotation;
	private Quaternion newRotation;
	private string moveState;
	private Transform[] dotTF;
	private bool isMove = false;
	private Vector3[] preDotPos;

	void Start () {

		flipTs = transform.Find ("Flipper").transform;
		oldRotation = transform.localRotation;
		newRotation = Quaternion.Euler (oldRotation.eulerAngles+Vector3.up*moveAngle);
		moveState = "";

		dotPos = new Vector3[dotNum];
		for (int i = 0; i < dotNum; i++) {
			dotTF [i] = transform.Find ("dot" + (i + 1).ToString ());
		}

	}


	void Update () {

		if ((Input.GetKeyDown (KeyCode.RightArrow)&&isRight)||(Input.GetKeyDown (KeyCode.LeftArrow)&&!isRight)) 
		{
			moveState = "upAni";
		}

		if (moveState == "upAni") 
		{
			//flipper上升状态
			transform.localRotation = Quaternion.RotateTowards(transform.localRotation,newRotation,moveSpeed);
			if (transform.localRotation != newRotation) {
				RayFlip ();
			}
		}

		if ((Input.GetKeyUp (KeyCode.RightArrow)&& isRight)||(Input.GetKeyUp(KeyCode.LeftArrow)&&!isRight))
		{
			moveState = "downAni";
		}

		if (moveState == "downAni") 
		{
			//flipper下降状态
			transform.localRotation = Quaternion.RotateTowards(transform.localRotation,oldRotation,moveSpeed);
		}
		for (int i = 0; i < dotNum; i++) {
			preDotPos [i] = dotTF [i].position;
		}
	}
	void RayFlip()
	{
		for (int i = 0; i < dotNum; i++) {
			RaycastHit[] hits;
			hits = Physics.RaycastAll (preDotPos [i], dotTF [i].position-preDotPos[i], Vector3.Distance (preDotPos [i], dotTF [i].position));
		}
	}
}