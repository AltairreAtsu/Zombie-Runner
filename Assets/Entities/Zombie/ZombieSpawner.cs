using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {
	[SerializeField][Tooltip ("Prefab to use when spawning zombies.")]
	private GameObject zombiePrefab = null;
	[SerializeField][Tooltip ("Hard cap of zombies that can be spawned at once.")]
	private int zombieHardCap = 15;
	[SerializeField][Tooltip ("Hard Cap of Time that can pass without spawn.")]
	private float timeBetweenSpawnsMax = 15f;
	[SerializeField][Tooltip ("Soft Cap of minimum time before a zombie can spawn.")]
	private float timeBetweenSpawnsMin = 5f;
	[SerializeField][Range (0f, 1f)][Tooltip ("Base chance a zombie will spawn durring spawn tick.")]
	private float spawnChance = 0.05f;
	[SerializeField][Tooltip ("How far a spawn point must be from the player to spawn a zombie.")]
	private float spawnDistance = 20f;

	private GameObject zombieParent;
	private Player player;
	private float timeSinceLastSpawn;
	private int zombieCount;

	private void Start() {
		zombieParent = GameObject.Find ("Zombies");
		player = GameObject.FindObjectOfType<Player> ();

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
		Transform spawnPos = RandomSpawnPoint ();

		GameObject zombie = Object.Instantiate (zombiePrefab, spawnPos) as GameObject;
		zombie.transform.SetParent (zombieParent.transform, true);
		zombieCount++;
		timeSinceLastSpawn = 0f;
	}

	private Transform RandomSpawnPoint(){
		float differentialFloat = 0f;
		Transform spawnPos = null;

		while ((differentialFloat == 0f) && (differentialFloat < spawnDistance)){
			int childIndex = Random.Range (0, transform.childCount);
			spawnPos = transform.GetChild ( childIndex );
			differentialFloat = Mathf.Abs(player.transform.position.magnitude - spawnPos.position.magnitude);
		}

		return spawnPos;
	}

	private void OnZombieKilled(Transform zombiePosition){
		zombieCount--;
	}
}
