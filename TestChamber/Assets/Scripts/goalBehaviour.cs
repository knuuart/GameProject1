using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goalBehaviour : MonoBehaviour {

    [Header("The number of the scene we transition to")]
    public int nextLevelInt;
    [Header("How we're loading the next scene")]
    public LoadSceneMode loadMode;

	void OnTriggerEnter(Collider coll) {
        if (coll.tag == "Player") {
			StartCoroutine (LoadNextScene ());
        }
    }
	IEnumerator LoadNextScene(){
		// FADE HERE
		yield return new WaitForSeconds(1f);
		Victory ();
	}

    void Victory() {
        SceneManager.LoadScene(nextLevelInt, loadMode);
    }
}
