using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartButton : MonoBehaviour {
     
         public void RestartGame() {
             SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
			 Time.timeScale = 1f;
         }
     
}