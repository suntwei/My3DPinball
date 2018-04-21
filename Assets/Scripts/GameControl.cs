using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

	// Use this for initialization
	private Rigidbody ball;
	void Start () {
		//初始化重力场
		Physics.gravity = new Vector3(0,-5,-30);
		ball = GameObject.FindWithTag ("ball").GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.RightControl)) 
		{
			ball.velocity = ball.velocity + new Vector3 (20, 0, 30);
		}else if(Input.GetKeyDown (KeyCode.LeftControl))
		{
			ball.velocity = ball.velocity + new Vector3 (-20, 0, 30);
		}else if (Input.GetKeyUp (KeyCode.Space)) {
			ball.velocity = ball.velocity + new Vector3 (0, 0, -300);
		}
	}
}
