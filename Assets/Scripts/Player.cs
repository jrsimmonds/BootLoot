using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField]
	private Vector3 spawnPoint;

	[SerializeField]
	private GameObject mPlat1;

    public Rigidbody2D MyRigidbody { get; set; }

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private bool moving = false;

    public Animator MyAnimator;

    public bool facingRight = false;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private bool kick;
	public bool kicking;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private bool AirControl;

	[SerializeField]
	public bool hurtControlsOff;

    // Use this for initialization
    void Start () {
        MyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal != 0)
        {
            moving = true;
        }else
        {
            moving = false;
        }
        isGrounded = IsGrounded();
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleLayers();
        Reset();
    }

    void HandleMovement(float horizontal)
    {
		if (!hurtControlsOff) {

			if (MyRigidbody.velocity.y < 0) {
				MyAnimator.SetBool ("Land", true);
			}

			if (isGrounded) {
				MyAnimator.SetBool ("Land", false);
			}

			if (!kicking && (isGrounded || AirControl)) {
				MyRigidbody.velocity = new Vector2 (horizontal * movementSpeed, MyRigidbody.velocity.y);
			}

			if (kicking && isGrounded) {
				MyRigidbody.velocity = new Vector2 (0, 0);
			}

			//JUMP CODE
			if (!kicking && moving && isGrounded && jump) {
				if (isGrounded && jump) {
					isGrounded = false;
					if (facingRight) {
						MyRigidbody.AddForce (new Vector2 (100, jumpForce));
						MyAnimator.SetTrigger ("Jump");
					} else if (!facingRight) {
						MyRigidbody.AddForce (new Vector2 (-100, jumpForce));
						MyAnimator.SetTrigger ("Jump");
					}
				}
			} else if (!kicking && !moving && isGrounded && jump) {
				MyRigidbody.AddForce (new Vector2 (0, jumpForce));
				MyAnimator.SetTrigger ("Jump");
			}
			/////JUMP CODE FINISHED

			//KICK CODE
			if (kick) {
				MyAnimator.SetTrigger ("Kick");
			}
			/////KICK CODE FINISHED

			MyAnimator.SetFloat ("Speed", Mathf.Abs (horizontal));
		}
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            kick = true;
			kicking = true;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            MyAnimator.SetTrigger("Hurt");
            GameObject.Find("Globals").GetComponent<globals>().health = GameObject.Find("Globals").GetComponent<globals>().health - 1;
        }


    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }

    public virtual void ChangeDirection()
    {
        //Changes the facingRight bool to its negative value
        facingRight = !facingRight;

        //Flips the character by changing the scale
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    private bool IsGrounded()
    {
        if(MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        MyAnimator.ResetTrigger("Jump");
                        MyAnimator.SetBool("Land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!isGrounded)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    void Reset()
    {
        jump = false;
        kick = false;
    }

    private void kicker()
    {
        MyRigidbody.AddForce(new Vector2(0, 400));
    }

	public void spikerHurt(){
		if (facingRight) {
			MyRigidbody.AddForce(new Vector2(-300,100));
			MyAnimator.SetTrigger("Hurt");
			StartCoroutine(playerHurt());
			GameObject.Find("Globals").GetComponent<globals>().health = GameObject.Find("Globals").GetComponent<globals>().health - 1;
		}
		if (!facingRight) {
			MyRigidbody.AddForce(new Vector2(300,100));
			MyAnimator.SetTrigger("Hurt");
			StartCoroutine(playerHurt());
			GameObject.Find("Globals").GetComponent<globals>().health = GameObject.Find("Globals").GetComponent<globals>().health - 1;
		}
	}

	public IEnumerator playerHurt() {
		hurtControlsOff = true;
		GameObject.Find ("Globals").GetComponent<globals> ().playerHurtCooldown = true;
		yield return new WaitForSeconds (0.50f);
		GameObject.Find ("Player").GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.25f);
		hurtControlsOff = false;
		GameObject.Find ("Player").GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.25f);
		GameObject.Find ("Player").GetComponent<SpriteRenderer> ().enabled =false;
		yield return new WaitForSeconds (0.25f);
		GameObject.Find ("Player").GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.25f);
		GameObject.Find ("Player").GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.25f);
		GameObject.Find ("Player").GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.25f);
		GameObject.Find ("Player").GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.25f);
		GameObject.Find ("Player").GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.25f);
		GameObject.Find ("Globals").GetComponent<globals> ().playerHurtCooldown = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		//if (other.tag == "movingPlat1") {
		//	gameObject.transform.parent = mPlat1.transform;
		//}

		if (other.tag == "fallDeath") {
			gameObject.transform.position = spawnPoint;
		}

		if (other.tag == "feet") {
			MyRigidbody.AddForce (new Vector2 (0,85000));
			Debug.Log ("should be jumping high");
		}
	}

	//void OnTriggerExit2D(Collider2D other) {
	//	if (other.tag == "movingPlat1") {
	//		transform.parent = null;
	//	}
	//}
}
