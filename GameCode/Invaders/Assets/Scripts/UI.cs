using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Only needed if you wish to have a score or gameover message.
/// </summary>
public class UI : MonoBehaviour {

	[SerializeField]
	private GameObject gameover_panel, paused_panel, prompt_panel; //The parent objects of the three different panels
	
	[SerializeField]
    private GameObject[] outlines_Gameover, outlines_Paused, outlines_Prompt; //The button outlines for the three panels

	[SerializeField]
	private GameObject player; //the Player Prefab

	[SerializeField]
	private GameObject startLocation; //The location where the player should be spawned into the scene.

	[SerializeField]
    private Text _ScoreText; //the score in the top left corner

	[SerializeField]
	private Text finalScore; //the score displayed when the game ends.

	private int gameoverIndex, pausedIndex, promptIndex; //Stores which button is currently selected.

	private GameObject activePlayer; //The player object that is currently in the scene.

	// Use this for initialization
	void Start () {
		//Initialize which buttons should be active.
		gameoverIndex = 0; 
		pausedIndex = 0;
		promptIndex = 1;
		outlines_Gameover[1].SetActive(false);
		outlines_Paused[1].SetActive(false);
		outlines_Paused[2].SetActive(false);
		outlines_Prompt[0].SetActive(false);

		Replay(); //sets up the game state

		_ScoreText.text = ""+SceneController.score;

		Time.timeScale = 1; //Just making sure that the timeScale is right
	}
	
	void Update () {

		if(SceneController.gameOver)//If the game is over, display the Gameover UI panel and receive input.
		{
			finalScore.text = "Final Score: " + _ScoreText.text;
			_ScoreText.enabled = false;
			gameover_panel.SetActive(true);

			if ((Input.GetKeyDown (KeyCode.E) || Input.GetKeyDown (KeyCode.Space))) //If user presses a 'select' key.
			{
				Select_Gameover (); //click on the currently selected button.
			}
			gameoverIndex = UpdateSelection (gameoverIndex, outlines_Gameover);
				
		}
		else //otherwise, if the game is not over, update the scoreboard to display the current score.
		{	
			_ScoreText.text = ""+SceneController.score;//TODO
		}

		if(Input.GetKeyDown(KeyCode.Escape) && !SceneController.gameOver) //If the Escape key is pressed while not Gameover, either pause or unpause the game and show or hide the UI.
		{
			if(Time.timeScale == 1)//NOTE: Time.timeScale is the current rate time moves in your game (e.g., 0=paused, .5=half speed, 1=normal speed, etc.)
			{
				Time.timeScale = 0;
				SceneController.gamePaused = true;
				paused_panel.SetActive(true);
			}
			else
			{
				Time.timeScale = 1;
				SceneController.gamePaused = false;
				pausedIndex = 0;
				outlines_Paused[0].SetActive(true);
				outlines_Paused[1].SetActive(false);
				outlines_Paused[2].SetActive(false);
				paused_panel.SetActive(false);
				promptIndex = 1;
				outlines_Prompt[0].SetActive(false);
				outlines_Prompt[1].SetActive(true);
				prompt_panel.SetActive(false);
			}
		}

		if(SceneController.gamePaused && paused_panel.activeSelf == true)
		{
			if ((Input.GetKeyDown (KeyCode.E) || Input.GetKeyDown (KeyCode.Space))) //If user presses a 'select' key.
			{
				Select_Paused (); //click on the currently selected button.
			}
			pausedIndex = UpdateSelection (pausedIndex, outlines_Paused);
		}
		else if (SceneController.gamePaused)
		{
			if ((Input.GetKeyDown (KeyCode.E) || Input.GetKeyDown (KeyCode.Space))) //If user presses a 'select' key.
			{
				Select_Prompt (); //click on the currently selected button.
			}
			promptIndex = UpdateSelection (promptIndex, outlines_Prompt);
		}
	}
		
    private int UpdateSelection (int index, GameObject[] outlines)
    {
        if (Input.GetKeyDown (KeyCode.RightArrow)) //if the right arrow is pressed, change the highlighted button to the next one, if possible.
        {
            if (index < outlines.Length - 1)
            {
                outlines[index].SetActive (false);
                outlines[++index].SetActive (true);
            }
        }
        else if (Input.GetKeyDown (KeyCode.LeftArrow)) //if the left arrow is pressed, change the highlighted button to the previous one, if possible.
        {
            if (index > 0)
            {
                outlines[index].SetActive (false);
                outlines[--index].SetActive (true);
            }
        }
        return index; //return the modified index.
    }

    private void Select_Gameover ()
    {
        //Do things based on which button is selected.
        switch (gameoverIndex)
        {
            case 0:
				Replay();
                break;
            case 1:
				Application.Quit();
				Debug.Log("Quit");
                break;
        }
    }

    private void Select_Paused ()
    {
        //Do things based on which button is selected.
        switch (pausedIndex)
        {
            case 0:
				SceneController.gamePaused = false;
				paused_panel.SetActive(false);
				Time.timeScale = 1;
                break;
            case 1:
				pausedIndex = 0;
				outlines_Paused[0].SetActive(true);
				outlines_Paused[1].SetActive(false);
				paused_panel.SetActive(false);
				prompt_panel.SetActive(true);
                break;
            case 2:
				Application.Quit();
				Debug.Log("Quit");
                break;
        }
    }
	
    private void Select_Prompt ()
    {
		outlines_Prompt[0].SetActive(false);
		outlines_Prompt[1].SetActive(true);
		prompt_panel.SetActive(false);
        //Do things based on which button is selected.
        switch (promptIndex)
        {
            case 0:
				Time.timeScale = 1;
				promptIndex = 1;
				SceneController.gamePaused = false;
				Destroy(activePlayer);
				Replay();
                break;
            case 1:
				promptIndex = 1;
				paused_panel.SetActive(true);
                break;
        }
    }
	
	/// <summary>
	/// runs when the replay button is pressed.false Destroys all the current enemies and enemy lasers,
	/// and resets the score to zero.
	/// </summary>
	public void Replay()
	{
		//respawns the player
		activePlayer = GameObject.Instantiate(player, startLocation.transform.position, startLocation.transform.rotation);

		SceneController.score = 0;

		//finds all the enemies and enemy lasers, then systematically destroys them.
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
     	GameObject.Destroy(go);

		foreach(GameObject go in GameObject.FindGameObjectsWithTag("EnemyLaser"))
     	GameObject.Destroy(go);
		 
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Laser"))
     	GameObject.Destroy(go);

		SceneController.gameOver = false;
		prompt_panel.SetActive(false);
		paused_panel.SetActive(false);
		gameover_panel.SetActive(false);
		_ScoreText.enabled = true;
	}
}
