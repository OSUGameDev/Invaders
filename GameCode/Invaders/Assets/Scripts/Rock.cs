using UnityEngine;

public class Rock : MonoBehaviour {

	[SerializeField]
	private GameObject explosion;

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Laser")
		{
			Instantiate(explosion, this.transform.position, this.transform.rotation);
			Destroy(this.gameObject); //Destroys this object after 6 seconds.
		}
		else if(other.tag == "Boundry") //The boundary is at the bottom of the screen.
		{
			Destroy(this.gameObject);
		}
	}
}
