using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperMove : MonoBehaviour {

	public bool isRight = true;
	public int moveAngle = 40;
	public int moveSpeed = 40;
	public int dotNum = 18;
	public float flipSpeed = 40;
	public float maxFlipSpeed = 60;  
	public float blinkRant = 0.03f;//球碰撞到flip后，跳跃一段距离，防止与flip再次发生碰撞，导致不正常轨迹，是速度与距离的比率。

	private Transform flipTs;
	private Quaternion prevRotation;
	private Quaternion oldRotation;
	private Quaternion newRotation;
	private string moveState;
	private Transform[] dotTF;
	private Vector3[] preDotPos;
	private Vector3 addSpeedPos;

	void Start () {

		flipTs = transform.Find ("Flipper").transform;
		oldRotation = prevRotation =  transform.localRotation;
		newRotation = Quaternion.Euler (oldRotation.eulerAngles+Vector3.up*moveAngle);
		moveState = "";

		dotTF = new Transform[dotNum];
		for (int i = 0; i < dotNum; i++) {
			dotTF [i] = transform.Find ("dot" + (i + 1).ToString ());
		}

		addSpeedPos = transform.Find ("shootDot").position;
		preDotPos = new Vector3[dotNum];
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
			if (transform.localRotation != prevRotation) {
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

		prevRotation = transform.localRotation;
		for (int i = 0; i < dotNum; i++) {
			preDotPos [i] = dotTF [i].position;
		}
	}
	void RayFlip()
	{
		for (int i = 0; i < dotNum; i++) {
			RaycastHit[] hits;
			hits = Physics.RaycastAll (preDotPos [i], dotTF [i].position-preDotPos[i], Vector3.Distance (preDotPos [i], dotTF [i].position));
			for (int j = 0; j < hits.Length; j++) {
				if (hits [j].transform.CompareTag("ball")) {
					//反弹球
					Rigidbody hitsRig = hits[j].transform.GetComponent<Rigidbody>();
					hitsRig.velocity = Vector3.Normalize (hits[j].transform.position - addSpeedPos);
					hitsRig.velocity *=flipSpeed*Vector3.Distance(hits[j].transform.position,transform.position);
					hitsRig.velocity -= hitsRig.velocity.y * Vector3.up;
					hitsRig.velocity = Vector3.ClampMagnitude (hitsRig.velocity,maxFlipSpeed);
					hits [j].transform.GetComponent<Ball> ().BlinkTo (hits[j].transform.position+hitsRig.velocity * blinkRant);;
				}
			}
		}
	}
}