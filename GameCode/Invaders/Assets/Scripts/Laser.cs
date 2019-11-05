using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField]
	private GameObject explosion;
	private float lifetime = 4, timeLeft;
	
	private bool addedToPauseState;

	void Start()
	{
		timeLeft = lifetime;
		StartCoroutine("LoseTime");
	}

    void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(this.gameObject);
	}
	
	//Timer Coroutine
	IEnumerator LoseTime()
	{
		while (true) {
			yield return new WaitForSeconds (1);
			if(!SceneController.gamePaused)
			{
				if(timeLeft > 0)
				{
					timeLeft--; 
				}else
				{
					Destroy(this.gameObject);
				}
			}
		}
	}
}
