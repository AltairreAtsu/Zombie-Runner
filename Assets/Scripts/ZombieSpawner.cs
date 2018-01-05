using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

	[SerializeField][Tooltip ("Prefab to use when spawning zombies.")]
	private GameObject zombiePrefab;
	private GameObject zombieParent;

	[SerializeField][Tooltip ("Hard cap of zombies that can be spawned at once.")]
	private int zombieHardCap = 15;
	private int zombieCount;

	[SerializeField][Tooltip ("Hard Cap of Time that can pass without spawn.")]
	private float timeBetweenSpawnsMax = 15f;
	[SerializeField][Tooltip ("Soft Cap of minimum time before a zombie can spawn.")]
	private float timeBetweenSpawnsMin = 5f;
	private float timeSinceLastSpawn;

	[SerializeField][Range (0f, 1f)][Tooltip ("Base chance a zombie will spawn durring spawn tick.")]
	private float spawnChance = 0.05f;

	private void Start() {
		zombieParent = GameObject.Find ("Zombies");

		if(zombieParent == null){
			zombieParent = new GameObject ("Zombies");
		}

		ZombieLogic.OnZombieKilledObservers += OnZombieKilled;
	}

	// Update is called once per frame
	void Update () {
		timeSinceLastSpawn += Time.deltaTime;

		if(CanSpawnZombie()){
			SpawnZombie ();
		}
	}

	private bool CanSpawnZombie(){
		if (timeSinceLastSpawn > timeBetweenSpawnsMin)
		if (Random.value <= spawnChance || timeSinceLastSpawn > timeBetweenSpawnsMax) {
			if (zombieCount < zombieHardCap) {
				return true;
			}
		}

		return false;
	}

	public void SpawnZombie(){
		int childIndex = Random.Range (0, transform.childCount);
		Transform spawnPos = transform.GetChild ( childIndex );

		GameObject zombie = Object.Instantiate (zombiePrefab, spawnPos) as GameObject;
		zombie.transform.SetParent (zombieParent.transform, true);
		zombieCount++;
		timeSinceLastSpawn = 0f;
	}

	private void OnZombieKilled(Transform zombiePosition){
		zombieCount--;
	}
}
