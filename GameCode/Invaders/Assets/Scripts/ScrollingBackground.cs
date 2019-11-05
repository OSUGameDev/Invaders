using UnityEngine;

/// <summary>
/// This is handling the moving background.
/// </summary>
public class ScrollingBackground : MonoBehaviour 
{
	[SerializeField]
	private float offsetX, offsetY; //How much we want to move the background each Update() call.

	private Vector2 offset; //The vector format of offsetX and offsetY.

	private Material material; //The star background

	// Happens before start
	void Awake()
	{
		material = GetComponent<Renderer>().material; //sets the material variable to the starry background.
	}

	// Use this for initialization
	void Start () 
	{
		offset = new Vector2(offsetX, offsetY); //makes a 2d Vector with both offsets.
	}
	
	// Update is called once per frame
	void Update () 
	{
		material.mainTextureOffset += offset*Time.deltaTime; //changes the material offset by the desired amount each frame.
	}
}
