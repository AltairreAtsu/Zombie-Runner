using UnityEngine;
using TMPro;

public class EndUI : MonoBehaviour {

	[SerializeField]
	private TextMeshProUGUI timeSurvivedDisplay = null;
	[SerializeField]
	private TextMeshProUGUI killCountDisplay = null;

	void Start () {
		timeSurvivedDisplay.text = UIManager.ParseTime();
		killCountDisplay.text = UIManager.killCount.ToString();
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
