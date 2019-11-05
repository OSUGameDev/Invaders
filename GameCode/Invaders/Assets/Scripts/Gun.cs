using UnityEngine;

public class Gun : MonoBehaviour {

	[SerializeField]
	private float speed; // the rate of fire.

	[SerializeField]
	private float thrust; //the speed of the lasers.

	[SerializeField]
	private GameObject prefab; //the laser prefab that gets fired.

	[SerializeField]
	private AudioSource blip; //sound when the gun fires.

	private float timeTilNextFire; //the time still remaining before the gun fire again.


	// Use this for initialization
	void Start () {
		timeTilNextFire = speed; //initialize the time until the next laser is spawned.
	}
	
	// Update is called once per frame
	void Update () {
		timeTilNextFire -= Time.deltaTime; 	//this is a basic countdown timer that will make the timeTilNextFIre variable
											// countdown in seconds.

		if(timeTilNextFire<=0) //when the countdown timer gets to zero, spawn lasers at the gun locations.
		{
			blip.Play(); //play a sound.

            GameObject laser = Instantiate(prefab, transform.position, transform.rotation); //spawn a laser where the gun is.
			laser.GetComponent<Rigidbody2D>().AddForce(transform.up*thrust); //apply force to the laser.

			timeTilNextFire = speed; //reset the countdown timer.
		}
	}
}
