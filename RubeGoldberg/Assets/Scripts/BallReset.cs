using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using Valve.VR;

public class BallReset : MonoBehaviour {
	private Vector3 originalPosition;

	private Renderer ballRenderer;

	private RubeGoldbergGameManager gameManager;

	private Rigidbody ballRigidBody;

	private bool isValidarea;

	private string tagNameGround = "Ground";

	private string tagNameGoalTarget = "GoalTarget";

	private string tagNameCollectable = "Collectable";

	private string tagNameStructure = "Structure";

	private string tagNamePlatform = "Platform";

	private AudioSource audioSource;

	private TeleportBall teleportBall;


	[Tooltip("The game manager")]
	public GameObject GameManagerContainer;

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

	public AudioClip ballHitGroundSound;

	public AudioClip ballHitStructureSound;

	public AudioClip ballHitGoalTargetSound;

	public AudioClip ballHitUnknownSound;

	void Start () {
		originalPosition = transform.position;
		
		ballRenderer = gameObject.GetComponent<Renderer>();

	    gameManager = GameManagerContainer.GetComponent<RubeGoldbergGameManager>();

		ballRigidBody = GetComponent<Rigidbody>();

		audioSource = GetComponent<AudioSource>();

		teleportBall = GetComponent<TeleportBall>();
	}

	void OnCollisionEnter (Collision other)
    {
		DebugManager.Info("Collision detected: " + other.gameObject.tag);

        if(other.gameObject.CompareTag(tagNameGround))
        {
			if (gameManager.isNewLevelLoading()) {
				return;
			}

			audioSource.PlayOneShot(ballHitGroundSound);

			teleportBall.Initialize();
			
			ResetBallPosition();

			gameManager.ResetCollectableItems();

			return;
        }

		if(other.gameObject.CompareTag(tagNameGoalTarget))
        {
			gameManager.GoaltargetReached();

			audioSource.PlayOneShot(ballHitGoalTargetSound);

			ResetBallPosition();
			return;
        }

		if(other.gameObject.CompareTag(tagNameStructure) )
        {
			audioSource.PlayOneShot(ballHitStructureSound);
			return;
        }

		if(other.gameObject.CompareTag(tagNamePlatform) )
        {
			return;
        }

		audioSource.PlayOneShot(ballHitUnknownSound);
    }

	void OnTriggerEnter(Collider other) {
		DebugManager.Info("Trigger detected: " + other.gameObject.tag);

		if(other.gameObject.CompareTag(tagNameCollectable))
        {
			gameManager.IncrementCollectableItemsCount();
			other.gameObject.SetActive(false);
			return;
        }
	}

	private void ResetBallPosition() {
		transform.position = originalPosition;

		if (ballRigidBody) {
			ballRigidBody.velocity = Vector3.zero;
			ballRigidBody.angularVelocity = Vector3.zero; 
		}
	}

	void Update() {
		if (Player && ValidTeleportPoint && BallActiveMaterial && BallInactiveMaterial) {
			int newLayer;
			Material newMaterial;

			var distance = Vector3.Distance(Player.transform.position, ValidTeleportPoint.transform.position);

			if (!((distance >= 0) && (distance <= 2))) {
				newLayer = InvalidPlayAreaLayer;
				newMaterial = BallInactiveMaterial;
			} else {
				newLayer = PlayAreaLayer;
				newMaterial = BallActiveMaterial;
			}

			if (newLayer != gameObject.layer) {
				gameObject.layer = newLayer;
				ballRenderer.material = newMaterial;

				DebugManager.Info("Setting ball layer - " + newLayer + " - " + LayerMask.LayerToName(newLayer));
			}
		}
	}
}
