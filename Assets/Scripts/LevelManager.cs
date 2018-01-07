using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(int index){
		SceneManager.LoadScene(index);
	}

	public void Quit(){
		Application.Quit ();
	}
}
