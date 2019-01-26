using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RubeGoldbergGameManager : MonoBehaviour {
	private bool isLoading;

	[Tooltip("Next level scene name")]
	public string NextLevel = "Level 1";

	[Tooltip("UI to show when player wins")]
	public GameObject PlayerWinUI;

	[Tooltip("Number of targets to be collected for passing the level")]
	public List<GameObject> CollectableItems;

	
	[Tooltip("Ball")]
	public GameObject Ball;

	private AudioSource audioSource;

	public AudioClip collectableItemSound;

	private int collectableItemsCount;

	public bool isNewLevelLoading() {
		return isLoading;
	}

	void Start() {
		HideWInUI();

		audioSource = GetComponent<AudioSource>();

		ResetCollectableItems();
	}

	public void LoadNextLevel() {
		Object.Destroy(Ball);

		StartCoroutine(LoadSceneAsync());
	}

	public void ShowWinUI() {
		if (PlayerWinUI) {
			PlayerWinUI.SetActive(true);
		}
	}

	public void HideWInUI() {
		if (PlayerWinUI) {
			PlayerWinUI.SetActive(false);
		}
	}

	IEnumerator LoadSceneAsync() {
		if (!string.IsNullOrEmpty(NextLevel)) {
			DebugManager.Info("GameManager - LoadSceneAsync() - Loading level - " + NextLevel);

			yield return new WaitForSeconds(3);
			
			SteamVR_LoadLevel.Begin(NextLevel);
		}
	}

	public void ResetCollectableItems() {
		if (isLoading) {
			return;
		}

        foreach(var item in CollectableItems) {
			item.SetActive(true);
		}

		collectableItemsCount = 0;
    }

	public void GoaltargetReached() {
		DebugManager.Info("GameManager - GoaltargetReached() - Collectable items: " + collectableItemsCount);

		if (collectableItemsCount >= CollectableItems.Count) {
			isLoading = true;

			ShowWinUI();

			LoadNextLevel();
		} else {
			ResetCollectableItems();
		}
	}

	public void IncrementCollectableItemsCount() {
		DebugManager.Info("GameManager - IncrementCollectableItemsCount() - Collectable items: " + collectableItemsCount);

		collectableItemsCount++;

		audioSource.PlayOneShot(collectableItemSound);
	}
}
