using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {

    public Rigidbody2D playerRB;

    public bool canMove;
    public bool isMoving;
    public int moveRight;
    public Vector2 moveForce;
    public Vector2 jumpForce;

    public bool grounded;

    public Animator playerAnimState;

    public bool facingRight;

    //Warrior Variables
    public bool shielded;
    public float shieldedCooldownTimer;


	// Use this for initialization
	void Start () {

        LevelManager.instance.canMove = true;
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerAnimState = GetComponent<Animator>();
        facingRight = true;
        moveRight = 1;
        canMove = true;
        LevelManager.instance.canDash = true;
	}
	


	// Update is called once per frame
	void Update () {

        moveForce = new Vector2(LevelManager.instance.playerSpeed * moveRight, playerRB.velocity.y);
        jumpForce = new Vector2(playerRB.velocity.x, LevelManager.instance.playerJumpForce);


        if (Input.GetAxis("Horizontal") > 0 && LevelManager.instance.canMove)
        {
            isMoving = true;
            moveRight = 1;
        }
        else if(Input.GetAxis("Horizontal") < 0 && LevelManager.instance.canMove)
        {
            isMoving = true;
            moveRight = -1;
        }
        else
        {
            isMoving = false;
        }

        FlipCharacter();

        


        if (Input.GetKeyDown(KeyCode.Alpha1) && playerAnimState.GetInteger("PlayerState") != 1)
        {
            LevelManager.instance.playerState = 1;
            playerAnimState.SetInteger("PlayerState", 1);
            LevelManager.instance.playerSoulCount -= 5;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && playerAnimState.GetInteger("PlayerState") != 2)
        {
            LevelManager.instance.playerState = 2;
            playerAnimState.SetInteger("PlayerState", 2);
            LevelManager.instance.playerSoulCount -= 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && playerAnimState.GetInteger("PlayerState") != 3)
        {
            LevelManager.instance.playerState = 3;
            playerAnimState.SetInteger("PlayerState", 3);
            LevelManager.instance.playerSoulCount -= 5;
        }




        if (LevelManager.instance.playerState == 1)
        {
            if (Input.GetKeyDown(KeyCode.Q) && LevelManager.instance.canShield)
            {
                canMove = false;
                shielded = true;
                LevelManager.instance.shieldedTime = Time.time + 3;
                LevelManager.instance.canShield = false;
                shieldedCooldownTimer = Time.time + 8;
                Debug.Log("Shielded");
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                shielded = false;
                canMove = true;
            }

            if (Time.time > shieldedCooldownTimer)
            {
                LevelManager.instance.ShieldedCooldown();
            }
            if (Time.time > LevelManager.instance.shieldedTime)
            {
                shielded = false;
                canMove = true;
            }


            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                LevelManager.instance.WarriorAttack();
            }





        }


        if (LevelManager.instance.playerState == 2)
        {

            if (Input.GetKeyDown(KeyCode.Q) && LevelManager.instance.canDash)
            {
                LevelManager.instance.RangerDash();
                


            }



        }


        if (LevelManager.instance.playerState == 3)
        {



        }









    }


    private void FixedUpdate()
    {
       // groundCheck = Physics2D.OverlapCircle(groundCheck.transform.position, 1f);
        if (isMoving && moveRight > 0 && canMove)
        {
            playerRB.velocity = moveForce;
            Debug.Log("move right");
        }
        else if (isMoving && moveRight < 0 && canMove)
        {
            playerRB.velocity = moveForce;
            Debug.Log("move right");
        }
        else
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x * 0.9f, playerRB.velocity.y);
        }

        if (grounded && Input.GetButtonDown("Jump") && canMove)
        {
            playerRB.AddForce(jumpForce, ForceMode2D.Impulse);
        }



    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }

        if (col.gameObject.tag ==  "Enemy" && !shielded)
        {
            LevelManager.instance.playerHealth -= 1;
        }


    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void FlipCharacter()
    {
        if (moveRight == 1 && !facingRight)
        {
            Vector3 v = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            transform.localScale = v;
            facingRight = true;
        }
        if (moveRight == -1 && facingRight)
        {
            Vector3 v = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            transform.localScale = v;
            facingRight = false;
        }



    }





}
