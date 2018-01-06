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
	private TextMeshProUGUI timer = null;
	[SerializeField]
	private GameObject killPopUpPrefab = null;

	private float timeSurvived = 0f;
	private float helicopterTime = 0f;

	private int killCount = 0;

	// Use this for initialization
	private void Start () {
		player = GameObject.FindObjectOfType<Player> ();
		bloodOverlay = GetComponentInChildren<AlphaLerp> ();

		player.playerHitObservers += OnPlayerHit;
		ZombieLogic.OnZombieKilledObservers += OnZombieKilled;

		killCounter.text = "";
	}

	private void Update() {
		timeSurvived += Time.deltaTime;
		timer.text = ParseTime ();
	}

	private void OnPlayerHit (){
		bloodOverlay.Trigger ();
		// TODO Health Bar (?)
	}

	private void OnZombieKilled(Transform zombiePosition){
		Vector3 position = Camera.main.WorldToScreenPoint(zombiePosition.position);

		GameObject popUp = Object.Instantiate(killPopUpPrefab) as GameObject;
		popUp.transform.SetParent (gameObject.transform, true);
		popUp.transform.position = position;
		killCount++;
		killCounter.text = (killCount.ToString());
	}

	private string ParseTime(){
		float minutes = Mathf.Floor (timeSurvived / 60);
		if(minutes > 0){
			float seconds = Mathf.Floor(timeSurvived - (60 * minutes));
			string secondsString = seconds.ToString ();
			string minutesString = minutes.ToString();

			if(seconds < 10){
				secondsString = string.Concat ("0" + seconds).ToString();
			}
			if(minutes < 10){
				minutesString = string.Concat ("0" + minutes.ToString ());
			}
				
			return string.Format ("{0}:{1}", minutesString, secondsString);
		} else {
			float seconds = Mathf.Floor (timeSurvived);
			string secondsString = seconds.ToString ();
			string minutesString = "00";

			if(seconds < 10){
				secondsString = string.Concat ("0" + seconds).ToString();
			}

			return string.Format ("{0}:{1}", minutesString, secondsString);
		}
	}
}
