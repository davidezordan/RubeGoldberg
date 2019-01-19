using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class BallReset : MonoBehaviour {

	[Tooltip("Next level scene name")]
	public string NextLevel = "Level 1";

	[Tooltip("Number of targets to be collected for passing the level")]
	public List<GameObject> CollectableItems;

	[Tooltip("Player object")]
	public GameObject Player;

	[Tooltip("Valid game area point")]
	public GameObject ValidTeleportPoint;

	[Tooltip("Valid game area layer")]
	public int PlayAreaLayer = 0;

	[Tooltip("Invalid game area layer")]
	public int InvalidPlayAreaLayer = 8;

	[Tooltip("Active Ball Material")]
	public Material BallActiveMaterial;

	[Tooltip("Inactive Ball Material")]
	public Material BallInactiveMaterial;

	private int currentTargetCount;

	private Vector3 originalPosition;

	private Renderer ballRenderer;

	private Color defaultBallColor;

	void Start () {
		SteamVR.Initialize();

		originalPosition = transform.position;
		currentTargetCount = 0;

		ballRenderer = gameObject.GetComponent<Renderer>();
		defaultBallColor = ballRenderer.material.color;
	}

	void OnCollisionEnter (Collision col)
    {
		DebugManager.Info("Collision detected: " + col.gameObject.tag);

        if(col.gameObject.tag.ToLower().Equals("ground"))
        {
            transform.position = originalPosition;

			var rigidbody = GetComponent<Rigidbody>();
			if (rigidbody) {
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero; 
			}

			ResetCollectableItems();

			return;
        }
    }

	void OnTriggerEnter(Collider col) {
		DebugManager.Info("Trigger detected: " + col.gameObject.tag);

        if(col.gameObject.tag.ToLower().Equals("ground"))
        {
            transform.position = originalPosition;

			var rigidbody = GetComponent<Rigidbody>();
			if (rigidbody) {
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero; 
			}

			ResetCollectableItems();

			return;
        }

		if(col.gameObject.tag.ToLower().Equals("goaltarget"))
        {
			currentTargetCount++;
			col.gameObject.SetActive(false);

			if (currentTargetCount == CollectableItems.Count) {

				SteamVR_LoadLevel.Begin(NextLevel);
			}
        }	
	}

    private void ResetCollectableItems()
    {
        foreach(var item in CollectableItems) {
			item.SetActive(true);
		}

		currentTargetCount = 0;
    }

	void Update() {
		if (Player && ValidTeleportPoint && BallActiveMaterial && BallInactiveMaterial) {
			Color newColor = defaultBallColor;
			int newLayer = PlayAreaLayer;
			Material newMaterial = BallActiveMaterial;

			if (Player.transform.position != ValidTeleportPoint.transform.position) {
				newColor = Color.red;
				newLayer = InvalidPlayAreaLayer;
				newMaterial = BallInactiveMaterial;

				DebugManager.Info("Not a valid playarea");
			}

			// if (newColor != ballRenderer.material.color) {
			// 	ballRenderer.material.color = newColor;
			// 	newMaterial = BallActiveMaterial;

			// 	DebugManager.Info("Setting ball color - " + newColor);
			// }

			if (newMaterial != ballRenderer.material) {
				ballRenderer.material = newMaterial;

				DebugManager.Info("Setting ball material - " + newColor);
			}

			if (newLayer != gameObject.layer) {
				gameObject.layer = newLayer;

				DebugManager.Info("Setting ball layer - " + newLayer + " - " + LayerMask.LayerToName(newLayer));
			}
		}
	}
}
