using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField]
	private GameObject[] enemies; // a list of types of enemies the spawner can spawn (e.g., astroid, alian1, alian2, etc.).

	[SerializeField]
	private int spawnRateMax;

	[SerializeField]
	private int spawnRateMin;

	[SerializeField]
	private float speed; //the speed at which the enemies move after getting spawned.

	 private float timeTilNextSpawn;

	// Use this for initialization
	void Start () {
		resetTimeTilNextSpawn();
	}
	
	// Update is called once per frame
	void Update () {
		timeTilNextSpawn -= Time.deltaTime; //this is a basic countdown timer that will make the timeTilNextSpawn variable
											// countdown in seconds.

		if(timeTilNextSpawn <= 0) //when the countdown timer gets to zero, spawn a new enemy.
		{

			System.Random rnd = new System.Random(); //creats a random number generator
			int index = rnd.Next(0,enemies.Length); //get a random integer between zero and the number of different types of enemies.

			//creates the enemy at this objects position and rotation, then applies force to the enemies rigidbody downwards.
			Instantiate(enemies[index], this.transform.position, this.transform.rotation).GetComponent<Rigidbody2D>().AddForce((-this.transform.up)* speed);

			resetTimeTilNextSpawn(); //resets the clock to a new random amount of time between the max and min.
		}
	}

	/// <summary>
	/// resets the time until next spawn to a random number between spawnRateMin and spawnRateMax.
	/// </summary>
	void resetTimeTilNextSpawn(){
		timeTilNextSpawn = Random.value * spawnRateMax + spawnRateMin;
	}
}
