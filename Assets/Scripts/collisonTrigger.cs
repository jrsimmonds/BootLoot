using UnityEngine;
using System.Collections;

public class collisonTrigger : MonoBehaviour {
		
		[SerializeField]
		private BoxCollider2D platformCollider;
		
		[SerializeField]
		private BoxCollider2D platformTrigger1;
		[SerializeField]
		private BoxCollider2D platformTrigger2;
		[SerializeField]
		private BoxCollider2D platformTrigger3;

		[SerializeField]
		private Collider2D playerCollider;

		void Update() {
		
		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			StartCoroutine(dropDown());
		}
	}
		
		void OnTriggerEnter2D(Collider2D other){
			if (other.tag == "Player" || other.tag == "Enemy") 
			{
				Physics2D.IgnoreCollision(platformCollider, other, true);
			}
		}
		
		void OnTriggerExit2D(Collider2D other){
			//If the player stop colliding
			if (other.tag == "Player" || other.tag == "Enemy")
			{
				//Stop the collision from ignoring the player
				Physics2D.IgnoreCollision(platformCollider, other , false);
			}
		}
		
	IEnumerator dropDown() {
		yield return new WaitForSeconds (0);
		Physics2D.IgnoreCollision(platformCollider, playerCollider , true);
		yield return new WaitForSeconds (1.5f);
		Physics2D.IgnoreCollision(platformCollider, playerCollider , false);
	}
	}
