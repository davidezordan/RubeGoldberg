using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour {

	public GameObject Ball;

	public float VelocityDelta = 0.135f;

	public float MaximumDistance = 6f;

	private Rigidbody ballRigidbody;

	// Use this for initialization
	void Start () {
		if (Ball) {
			ballRigidbody = Ball.GetComponent<Rigidbody>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (ballRigidbody) {
//			var ballDistance = Vector3.Distance(gameObject.transform.position, Ball.transform.position);
			//DebugManager.Info("Distance: " + ballDistance);

			var pDir = Ball.transform.position - transform.position;
			DebugManager.Info("Fan rotation: " + pDir);

			pDir = (1f/pDir.magnitude) * pDir; //This Vector3 has now the Length 1
			ballRigidbody.velocity = pDir; 

			// if (ballDistance < MaximumDistance) {
			// 	float delta = VelocityDelta;
			// 	ballRigidbody.velocity = new Vector3(ballRigidbody.velocity.x + delta, ballRigidbody.velocity.y, ballRigidbody.velocity.z + delta);
			// 	ballRigidbody.angularVelocity = new Vector3(ballRigidbody.angularVelocity.x + delta, ballRigidbody.angularVelocity.y, ballRigidbody.angularVelocity.z + delta); 
			// }
		}
	}
}
