using UnityEngine;
using System.Collections;

public class playerResets : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void KickReset() {
		GameObject.Find ("controllerCollider").GetComponent<Player> ().kicking = false;
	}
}
