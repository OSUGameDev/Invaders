using UnityEngine;

public class Enemy : MonoBehaviour {
	
	[SerializeField]
	private GameObject explosion;

	void OnTriggerEnter2D(Collider2D other)
	{
		//then the enemy has been hit by the player
		if(other.tag == "Laser")
		{
			SceneController.score += 1; //this line is only needed if you are keeping score.
			Instantiate(explosion, this.transform.position, this.transform.rotation);
			Destroy(this.gameObject); //Destroys this object after 6 seconds.
		}
		//else the enemy has gone off-screen
		else if(other.tag == "Boundry") //The boundary is at the bottom of the screen.
		{
			Destroy(this.gameObject);
		}
	}
}
