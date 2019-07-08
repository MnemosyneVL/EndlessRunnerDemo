using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour {
    //This class has logic to move the ground towards the screen and create an illusion of player running
	private float scrollSpeed;
	public float tileSizeY;
	private Vector3 startPosition;
	private float speedVariable;
	GameObject speedManager;
	// Use this for initialization
	void Start () {
		speedManager = GameObject.FindGameObjectWithTag("Manager");
		speedVariable = speedManager.GetComponent<SpeedController>().speed;
		startPosition = transform.position;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		speedVariable = speedManager.GetComponent<SpeedController>().speed;
		scrollSpeed += -speedVariable * Time.smoothDeltaTime;
		float newPosition = Mathf.Repeat(scrollSpeed , tileSizeY);
        transform.position = startPosition + Vector3.forward * newPosition;
		
	}
}
