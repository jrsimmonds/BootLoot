using UnityEngine;
using System.Collections;

public class playerResets : MonoBehaviour {

	[SerializeField]
	private GameObject kickCollider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void KickReset() {
		GameObject.Find ("controllerCollider").GetComponent<Player> ().kicking = false;
	}

	public void kickOn() {
		kickCollider.SetActive (true);
	}

	public void kickOff() {
		kickCollider.SetActive (false);
	}
}
