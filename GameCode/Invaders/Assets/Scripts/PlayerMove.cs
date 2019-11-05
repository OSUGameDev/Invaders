using UnityEngine;

public class PlayerMove : MonoBehaviour {

	[SerializeField]
	private float speed;

	[SerializeField]
	private GameObject explosion;

	private bool canMoveRight;

	private bool canMoveLeft;

	private float startY;

	// Use this for initialization
	void Awake () {
		canMoveRight = true; //the player can move both right and left when the game starts.
		canMoveLeft = true;
		startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.RightArrow) && canMoveRight) //if the right arrow key is being pressed and it can move right.
		{
			float x = GetComponent<Transform>().position.x; //get the current x, y, and z positions of the player
			float y = GetComponent<Transform>().position.y;
			float z = GetComponent<Transform>().position.z;

			GetComponent<Transform>().position = new Vector3(x+(speed*Time.deltaTime),y,z); //move the player slightly along the x axis.
		}
		else if(Input.GetKey(KeyCode.LeftArrow) && canMoveLeft)
		{
			float x = GetComponent<Transform>().position.x;
			float y = GetComponent<Transform>().position.y;
			float z = GetComponent<Transform>().position.z;

			GetComponent<Transform>().position = new Vector3(x-(speed*Time.deltaTime),y,z); //move the player slightly along the x axis, in the other direction.
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "EnemyLaser" || other.tag == "Enemy")
		{
			SceneController.gameOver = true; //this line is only needed if you are keeping score.
			Instantiate(explosion, this.transform.position, this.transform.rotation);
			Destroy(this.gameObject); //Destroys this object.
		}
		if(other.name == "RightWall")	//if the player touches the right or left walls (invisible objects), it can't move in that direction.
		{
			canMoveRight = false;
		}
		else if(other.name == "LeftWall")
		{
			canMoveLeft = false;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.name == "RightWall")	//if the player stops touching the right or left walls (invisible objects), it can move in that direction again.
		{
			canMoveRight = true;
		}
		else if(other.name == "LeftWall")
		{
			canMoveLeft = true;
		}
	}
}
