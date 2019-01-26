using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBall : MonoBehaviour {
	private Rigidbody ballRigidbody;

    private GameObject teleportOut;

    public void Initialize() {
        var teleports = GameObject.FindGameObjectsWithTag("BallTeleportTargetOut");
        if (teleports.Length > 0) {
            teleportOut = teleports[0];
        }        
    }

	void Start () {
        ballRigidbody = GetComponent<Rigidbody>();

        Initialize();
	}

    void OnTriggerEnter(Collider other) {
        DebugManager.Info("TeleportBall.OnTriggerEnter(): " + other.tag);

        if (teleportOut && other.CompareTag("BallTeleportTarget")) {
            gameObject.SetActive(false);
            gameObject.transform.position = teleportOut.transform.position;
            ballRigidbody.velocity = new Vector3(0, ballRigidbody.velocity.y, 0);
            gameObject.SetActive(true);            
        }
    }
}
