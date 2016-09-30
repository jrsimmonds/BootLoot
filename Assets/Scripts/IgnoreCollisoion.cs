using UnityEngine;
using System.Collections;

public class IgnoreCollisoion : MonoBehaviour {

	[SerializeField]
	private Collider2D other;
	[SerializeField]
	private Collider2D playerCol;

	void OnEnable() {
		playerCol = GameObject.Find ("Player").GetComponent<BoxCollider2D> ();
	}
	// Use this for initialization
	private void Awake () {
		Physics2D.IgnoreCollision(playerCol, other, true);
	}
}
