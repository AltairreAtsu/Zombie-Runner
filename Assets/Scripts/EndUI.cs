﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndUI : MonoBehaviour {

	[SerializeField]
	private TextMeshProUGUI timeSurvivedDisplay;
	[SerializeField]
	private TextMeshProUGUI killCountDisplay;

	// Use this for initialization
	void Start () {
		timeSurvivedDisplay.text = UIManager.ParseTime();
		killCountDisplay.text = UIManager.killCount.ToString();
	}
}
