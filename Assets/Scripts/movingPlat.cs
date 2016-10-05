using UnityEngine;
using System.Collections;

public class movingPlat : MonoBehaviour {

	private Vector3 posA;
	private Vector3 posB;
	private Vector3 nextPos;

	[SerializeField]
	private Transform childTrans;

	[SerializeField]
	private float speed;

	[SerializeField]
	private Transform transformB;

	// Use this for initialization
	void Start () {
		posA = childTrans.localPosition;
		posB = transformB.localPosition;
		nextPos = posB;
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	private void Move() {
		childTrans.localPosition = Vector3.MoveTowards (childTrans.localPosition, nextPos, speed * Time.deltaTime);

		if (Vector3.Distance (childTrans.localPosition, nextPos) <= 0.1) {
			ChangeDest ();
		}
	}

	private void ChangeDest() {
		nextPos = nextPos != posA ? posA : posB;
	}
}
