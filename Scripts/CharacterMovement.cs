using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    //This class contains logic for player movement on the lines
	[SerializeField]
	float secondsToMove;

	[SerializeField]
	KeyCode moveLeft;
	
	[SerializeField]
	KeyCode moveRight;
	private float velocity = 0;
	[SerializeField]
	float movementSpeed;
	private int lineNumber = 2;
	private float[] lineCoordinatesX = new float[5] {-8.5f, -3.5f, 1.5f, 6.5f, 11.5f};
	private Rigidbody2D rb; 

	private bool isMovingLeft = false;
	private bool isMovingRight = false;

	private bool changedLineLeft = false;
	private bool changedLineRight = false;

	private bool goingLeft;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(moveRight) || isMovingRight == true)
		{

			if (changedLineRight)
			{
				
				if (transform.position.x >= lineCoordinatesX[lineNumber])
				{
					rb.velocity = new Vector3 (0,0,0);
					changedLineRight = false;
					isMovingRight = false;
				}
			}
			else if (lineNumber < 4 && isMovingLeft == false)
			{
				lineNumber += 1;
				rb.velocity = new Vector3 (movementSpeed,0,0);
				changedLineRight = true;
				isMovingRight = true;
			}
			
		}
		if (Input.GetKeyDown(moveLeft) || isMovingLeft == true)
		{
			if (changedLineLeft)
			{
				
				if (transform.position.x <= lineCoordinatesX[lineNumber])
				{
					rb.velocity = new Vector3 (0,0,0);
					changedLineLeft = false;
					isMovingLeft = false;
				}
			}
			else if (lineNumber > 0 && isMovingRight == false)
			{
				lineNumber -= 1;
				rb.velocity = new Vector3 (-movementSpeed,0,0);
				changedLineLeft = true;
				isMovingLeft = true;
			}
			
		}
	}

    //Obsolete code

	// IEnumerator StopMevement()
	// {
	// 	if (goingLeft)
	// 	{
	// 		while (transform.position.x > lineCoordinatesX[lineNumber])
	// 		{
	// 			yield return null;
	// 		}
	// 	}
	// 	else
	// 	{
	// 		while (transform.position.x < lineCoordinatesX[lineNumber])
	// 		{
	// 			yield return null;
	// 		}
	// 	}
		
	// 	//yield return new WaitForSeconds (secondsToMove);
	// 	velocity = 0;
	// }
}
