using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
	private Player player;
	private AlphaLerp bloodOverlay;

	[SerializeField]
	private TextMeshProUGUI killCounter = null;
	[SerializeField]
	private GameObject killPopUpPrefab = null;

	private int killCount = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<Player> ();
		bloodOverlay = GetComponentInChildren<AlphaLerp> ();

		player.playerHitObservers += OnPlayerHit;
		ZombieLogic.OnZombieKilledObservers += OnZombieKilled;

		killCounter.text = "";
	}

	void OnPlayerHit (){
		bloodOverlay.Trigger ();
		// TODO Health Bar (?)
	}

	void OnZombieKilled(Transform zombiePosition){
		// TODO instantiate Pop Up
		Vector3 position = Camera.main.WorldToScreenPoint(zombiePosition.position);
		Debug.Log (position.x + ", " + position.y);

		GameObject popUp = Object.Instantiate(killPopUpPrefab) as GameObject;
		popUp.transform.SetParent (gameObject.transform, true);
		popUp.transform.position = position;
		killCount++;
		killCounter.text = (killCount.ToString());
	}
}
