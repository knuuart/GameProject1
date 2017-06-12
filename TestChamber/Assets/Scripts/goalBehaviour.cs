using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class goalBehaviour : MonoBehaviour {

    [Header("The number of the scene we transition to")]
    public int nextLevelInt, thisLevelInt;
    [Header("How we're loading the next scene")]
    public LoadSceneMode loadMode;
	public float waitTime;
	public GameObject fadeScreen;
	public UnityEvent message;

	void OnTriggerEnter(Collider coll) {
        if (coll.tag == "Player") {
			fadeScreen.SetActive (true);
//			fadeScreen.GetComponent<Renderer> ().material.color = new Color (0, 0, 0, 0);
			StartCoroutine (LoadNextScene ());
			message.Invoke ();
        }
    }
	IEnumerator LoadNextScene(){
		

		yield return new WaitForSeconds(waitTime);
		Victory ();
	}

    void Victory() {
        SceneManager.LoadScene(nextLevelInt, loadMode);
    }
	void Update(){
		if (Input.GetKeyDown (KeyCode.F5)) {
			SceneManager.LoadScene (thisLevelInt);
		}
	}
}
