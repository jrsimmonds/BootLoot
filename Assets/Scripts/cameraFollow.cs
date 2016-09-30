using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	[SerializeField]
	private float xMax;
	[SerializeField]
	private float yMax;
	[SerializeField]
	private float xMin;
	[SerializeField]
	private float yMin;

	public Transform target;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(Mathf.Clamp(target.position.x,xMin,xMax), Mathf.Clamp(target.position.y,yMin, yMax), transform.position.z);
	}
}
