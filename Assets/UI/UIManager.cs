using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
	[SerializeField][Tooltip ("Store the refrence to the Kill Counter UI Element in here.")]
	private TextMeshProUGUI killCounter = null;
	[SerializeField][Tooltip ("Store the Refrence to the Timer UI Element in here.")]
	private TextMeshProUGUI timer = null;
    [SerializeField][Tooltip ("Store the Refrence to the Ammo UI here.")]
    private TextMeshProUGUI ammoDisplay = null;
	[SerializeField][Tooltip ("Store the Refrence to the Slider UI Element in here.")]
	private Slider helicopterSlider = null;
    [SerializeField][Tooltip ("Store the Refrence to the Health UI image here.")]
    private Image healthImage = null;
    [Space]
	[SerializeField][Tooltip ("The Kill UI Pop Up prefab to instantiate when a zombie is killed.")]
	private GameObject killPopUpPrefab = null;

	private Player player;
    private FireGun gun;
	private Helicopter helicopter;
	private AlphaLerp bloodOverlay;

	public static float timeSurvived = 0f;
	public static int killCount = 0;

	// Use this for initialization
	private void Start () {
		player = GameObject.FindObjectOfType<Player> ();
        gun = player.transform.GetComponentInChildren<FireGun>();
		helicopter = GameObject.FindObjectOfType<Helicopter>();
		bloodOverlay = GetComponentInChildren<AlphaLerp> ();

		player.playerHitObservers += OnPlayerHit;
		ZombieLogic.OnZombieKilledObservers += OnZombieKilled;

		killCounter.text = "";

		killCount = 0;
		timeSurvived = 0;
	}

	private void Update() {
		timeSurvived += Time.deltaTime;
		timer.text = ParseTime ();
        ammoDisplay.text = gun.getAmmo().ToString();
        healthImage.fillAmount = player.getHealthPercentage();

		helicopterSlider.value = Mathf.Lerp (1, 0, helicopter.passedTime / helicopter.GetArrivalTime());
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

	private void OnDestroy(){
		player.playerHitObservers -= OnPlayerHit;
		ZombieLogic.OnZombieKilledObservers -= OnZombieKilled;
	}

	public static string ParseTime(){
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
