using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {
    //This class makes monsters move and is also responsible for monster visuals during the lightning strike
	private float moveSpeed;

	private Vector3 startPosition;
	private SpriteRenderer spriteRend;
	private Transform realMonster;
	private SpriteRenderer realSpriteRend;
	private float speedVariable;
	private BoxCollider colliderMonster;
	// [SerializeField]
	// GameObject speedManagerObject;
	GameObject speedManagerObject;
	private SpeedController managerScriptComponent;
	[SerializeField]
	bool isLightning;
	private bool lightningStrikeVariable;
	private float chargeVariable;

	// Use this for initialization
	void Start () {
		speedManagerObject = GameObject.FindGameObjectWithTag("Manager");
		managerScriptComponent = speedManagerObject.GetComponent<SpeedController>();
		speedVariable = speedManagerObject.GetComponent<SpeedController>().speed;
		spriteRend = GetComponent<SpriteRenderer>();
		startPosition = transform.position;
		colliderMonster = GetComponent<BoxCollider>();
		if(!isLightning)
		{
			realMonster = gameObject.transform.Find("bushreal");
			realSpriteRend = realMonster.GetComponent<SpriteRenderer>();
			lightningStrikeVariable = speedManagerObject.GetComponent<SpeedController>().lightningStrike;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		speedVariable = speedManagerObject.GetComponent<SpeedController>().speed;
		moveSpeed = speedVariable;
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,transform.position.y,-20), moveSpeed * Time.smoothDeltaTime);
		if (Vector3.Distance(transform.position, startPosition) > 70)
		{
			Destroy(gameObject);
		}
		if (transform.position.z < 1)
		{
			spriteRend.sortingOrder = 3;
		}

	}
	void OnCollisionEnter(Collision colliderLocal)
    {
		if(colliderLocal.gameObject.tag == "Player")
		{
			if(isLightning)
			{
				Destroy(gameObject);
				speedManagerObject.GetComponent<SpeedController>().chargeMeterVariable += 20;
			}
			else
			{
				Debug.Log(Time.timeScale);  
				managerScriptComponent.dieMenu = true;
				managerScriptComponent.PauseMenuSwapper(true);
				Time.timeScale = 0.0f;  
				
			}
		}
       
    }
}
