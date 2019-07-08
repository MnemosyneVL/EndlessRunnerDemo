using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour {
    //This class is responsilbe for making game harder depending on playtime and panic level
	public float initialSpeed;
	public float speed;
	public bool lightningStrike;
	[SerializeField]
	float normalSpeedMultiplier;
	[SerializeField]
	GameObject normalLight;
	[SerializeField]
	GameObject lightningLight;
	[SerializeField]
	Canvas vail;
	private float vailTransparancy;
	public GameObject[] realMonsters;
	public GameObject[] currentMonsters;

	public Slider panicMeter;
	[SerializeField]
	KeyCode chargeButton;
	[SerializeField]
	float normalPanicMultiplier;
	private float panicMeterVariable = 0f;
	private float panicMeterValue;
	public Slider chargeMeter;	
	public float chargeMeterVariable;
	private float chargeMeterValue;

	[SerializeField]
	Text scoreText;
	private float scoreDeci = 0f;
	private int scoreInt = 0;
	[SerializeField]
	Canvas pauseMenu;
	[SerializeField]
	Canvas resumeButtonMenu;
	public bool dieMenu;
	//private bool swapMenu = false;

	// Use this for initialization
	void Start () {
		panicMeter.value = 0f;
		chargeMeter.value = 0f;
		pauseMenu.enabled = false;
		resumeButtonMenu.enabled = false;
		vail.enabled = false;
		dieMenu = false;
		PauseMenuSwapper(true);
	}
	
	// Update is called once per frame
	void Update () {
		currentMonsters = GameObject.FindGameObjectsWithTag("CurrentMonster");
		realMonsters = GameObject.FindGameObjectsWithTag("RealMonster");
		if (panicMeterVariable < 100)
		{
			panicMeterVariable += 2 * Time.deltaTime;
			panicMeter.value = panicMeterVariable/100f;
		}
		if (chargeMeterVariable <= 100)
		{
			chargeMeter.value = chargeMeterVariable/100f;
		}
		normalSpeedMultiplier += Time.deltaTime;
		speed = initialSpeed + normalSpeedMultiplier/25f + panicMeterVariable/25f;
		scoreDeci += Time.deltaTime * speed;
		scoreInt = Mathf.RoundToInt(scoreDeci);
		scoreText.text = "" + scoreInt;
		
		
		if(Input.GetKeyDown(chargeButton) && chargeMeterVariable >= 100f)
		{
			Debug.Log("CHARGE!");
			chargeMeterVariable = 0f;
			chargeMeter.value = chargeMeterVariable/100f;
			if( panicMeterVariable > 40f)
			{
				panicMeterVariable -= 40f;
			}
			else
			{
				panicMeterVariable = 0f;
			}
			StartCoroutine(LigtningWait(0f, 1, 0, 0));
			StartCoroutine(LigtningWait(0.2f, 0, 0, 0));
			StartCoroutine(LigtningWait(0.3f,1, 1, 1));
			StartCoroutine(LigtningWait(0.4f,0, 1, 1));
			StartCoroutine(LigtningWait(1.1f, 1, 0, 0));
			StartCoroutine(LigtningWait(1.2f, 0, 0, 0));
			
		}
	
		if(Input.GetKeyDown("escape") && !dieMenu)
		{
			PauseMenuSwapper(true);
		} 
		
	}
	IEnumerator LigtningWait(float timeToWait, int vailPhase, int lightPhase, int monsterPhase)
	{
		yield return new WaitForSeconds (timeToWait);
		if (vailPhase == 1)
		{
			vail.enabled = true;
		}
		else if (vailPhase == 0)
		{
			vail.enabled = false;
		}
		if (lightPhase == 1)
		{
			lightningLight.GetComponent<Light>().enabled = true;
		}
		else if (lightPhase == 0)
		{
			lightningLight.GetComponent<Light>().enabled = false;
		}
		if (monsterPhase == 1)
		{
			foreach (GameObject realMonster in realMonsters)
			{
				realMonster.GetComponent<SpriteRenderer>().enabled = true;
			}
			foreach (GameObject currentMonster in currentMonsters)
			{
				currentMonster.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		else if (monsterPhase == 0)
		{
			foreach (GameObject realMonster in realMonsters)
			{
				realMonster.GetComponent<SpriteRenderer>().enabled = false;
			}
			foreach (GameObject currentMonster in currentMonsters)
			{
				currentMonster.GetComponent<SpriteRenderer>().enabled = true;
			}
		}


		Debug.Log("wait");
	}

	public void PauseMenuSwapper(bool toggleMenu)
	{
		if(toggleMenu)     
   		{    
			if (Time.timeScale == 1.0f)    
			{
				if(!dieMenu)
				{
					resumeButtonMenu.enabled = true;
					pauseMenu.enabled = true;
					Debug.Log(Time.timeScale);  
					Time.timeScale = 0.0f;  
					toggleMenu = false;
				}
				else
				{
					pauseMenu.enabled = true;
					Debug.Log(Time.timeScale);  
					Time.timeScale = 0.0f;  
					toggleMenu = false;
				}
				
			}   
						
			else
			{
				if(!dieMenu)
				
				{
					resumeButtonMenu.enabled = false;
					pauseMenu.enabled = false;
					Debug.Log(Time.timeScale);  
					Time.timeScale = 1.0f;
					toggleMenu = false; 
				}
				else
				{
					toggleMenu = false;
					Debug.Log("Start new game!");
				}
				
			}
			               
    	}
	}
	
}
