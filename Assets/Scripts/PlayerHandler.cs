using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {

    public Rigidbody2D playerRB;

    public GameObject respawnPoint;

    public GameObject groundCheckOBJ;
    public LayerMask groundLayerMask;



    public GameObject thiefParticle;
    public GameObject warriorParticle;
    public GameObject rangerParticle;

   // public bool LevelManager.instance.canMove;
    public bool isMoving;
   // public int moveRight;
    public Vector2 moveForce;
    public Vector2 jumpForce;
    //private Vector2 pushForce;
    public float pushStrength;

    public bool grounded;

    public Animator playerAnimState;

    public bool facingRight;

    //Warrior Variables
    public bool hasThiefCollectible;
    public bool shielded;
    public float shieldedCooldownTimer;


    //Ranger Variables
    public bool hasRangerCollectible;
    public Animator rangerAnimator;


	// Use this for initialization
	void Start () {

        LevelManager.instance.canMove = true;
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerAnimState = GetComponent<Animator>();
        facingRight = true;
        //pushForce = new Vector2(pushStrength * 2, pushStrength);
        
        LevelManager.instance.canMove = true;
        LevelManager.instance.canDash = true;

        
    }
	


	// Update is called once per velocity
	void Update () {


       // Debug.Log(playerRB.velocity.y);


        updateAnimator();

        moveForce = new Vector2(LevelManager.instance.playerSpeed * Input.GetAxis("Horizontal"), playerRB.velocity.y);
        jumpForce = new Vector2(0, LevelManager.instance.playerJumpForce);

        if (!LevelManager.instance.isDashing)
        {
            StartCoroutine("MovePlayer");
        }

        //if (grounded && Input.GetButtonDown("Jump") && LevelManager.instance.canMove)
        //{
        //    Debug.Log("jump");
        //    playerRB.AddForce(jumpForce, ForceMode2D.Impulse);
        //    Debug.Log(playerRB.velocity.y);

        //}

        if (Input.GetKeyDown(KeyCode.Space) && LevelManager.instance.canMove && grounded)
        {
            Debug.Log("Jump");
            Vector2 v = new Vector2(0, 500);
            playerRB.AddForce(v);
        }




        FlipCharacter();

        

        // this is the code to swtich character states
        //It prevents you from switching to the current character state
        if (Input.GetKeyDown(KeyCode.Alpha1) && playerAnimState.GetInteger("PlayerState") != 1)
        {
            LevelManager.instance.playerState = 1;
            playerAnimState.SetInteger("PlayerState", 1);
            LevelManager.instance.playerSoulCount -= 5;
            Instantiate(warriorParticle, transform.position, Quaternion.identity);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && playerAnimState.GetInteger("PlayerState") != 2  && hasRangerCollectible)
        {
            LevelManager.instance.playerState = 2;
            playerAnimState.SetInteger("PlayerState", 2);
            LevelManager.instance.playerSoulCount -= 5;
            Instantiate(rangerParticle, transform.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && playerAnimState.GetInteger("PlayerState") != 3 && hasThiefCollectible)
        {
            LevelManager.instance.playerState = 3;
            playerAnimState.SetInteger("PlayerState", 3);
            LevelManager.instance.playerSoulCount -= 5;
            Instantiate(thiefParticle, transform.position, Quaternion.identity);
        }



        //if the player state is 1 (warrior state) then these are the abilities available to the player
        if (LevelManager.instance.playerState == 1)
        {
            if (Input.GetKeyDown(KeyCode.Q) && LevelManager.instance.canShield && !shielded)
            {
                LevelManager.instance.canShield = false;
                LevelManager.instance.canMove = false;
                shielded = true;
                LevelManager.instance.Invoke("WarriorShieldedDuration", 3f);
                //LevelManager.instance.shieldedTime = Time.time + 3;
                //LevelManager.instance.canShield = false;
                //shieldedCooldownTimer = Time.time + 8;
               // Debug.Log("Shielded");
            }
            else if (Input.GetKeyDown(KeyCode.Q) && shielded)
            {
                shielded = false;
                LevelManager.instance.canMove = true;
                LevelManager.instance.StartCoroutine("WarriorShieldCooldown", 5);
            }

            //if (Time.time > shieldedCooldownTimer)
            //{
            //    LevelManager.instance.ShieldedCooldown();
            //}
            //if (Time.time > LevelManager.instance.shieldedTime)
            //{
            //    shielded = false;
            //   // LevelManager.instance.canMove = true;
            //}


            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                LevelManager.instance.WarriorAttack();
            }





        }

        // The ranger's abilities
        if (LevelManager.instance.playerState == 2)
        {
            if (Input.GetKeyDown(KeyCode.Q) && LevelManager.instance.canDash)
            {
                LevelManager.instance.RangerDash();

            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                LevelManager.instance.StartCoroutine("FireArrow");
            }

            //if (Input.GetKeyDown(KeyCode.Q) && LevelManager.instance.canDash)
            //{
            //    LevelManager.instance.RangerDash();                
                
            //}


            



        }



        //the Thiefs abilities
        if (LevelManager.instance.playerState == 3)
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                LevelManager.instance.ThiefBlink();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                LevelManager.instance.ThiefAttack();
            }




        }


        //moves the player
        if (isMoving && LevelManager.instance.canMove)
        {
            playerRB.velocity = moveForce;
        }









    }


    private void FixedUpdate()
    {
        //checks if the player is grounded by using overlap circle to detect objects on the ground layer within a radius of the groundcheck object
        grounded = Physics2D.OverlapCircle(groundCheckOBJ.transform.position, 0.25f, groundLayerMask);

        // groundCheck = Physics2D.OverlapCircle(groundCheck.transform.position, 1f);


        //    else if (LevelManager.instance.LevelManager.instance.canMove && !isMoving)
        //{
        //    playerRB.velocity = new Vector2(playerRB.velocity.x * 0.9f, playerRB.velocity.y);
        //}






    }


    public void updateAnimator()
    {

        if (LevelManager.instance.isDashing)
        {
            rangerAnimator.SetBool("isDashing", true);
            rangerAnimator.SetBool("isIdle", false);
            rangerAnimator.SetBool("isIdle", false);
        }
        else if (LevelManager.instance.isDashing && isMoving)
        {
            rangerAnimator.SetBool("isDashing", true);
            rangerAnimator.SetBool("isIdle", false);
            rangerAnimator.SetBool("isIdle", false);
        }
        else if (!LevelManager.instance.isDashing)
        {
            rangerAnimator.SetBool("isDashing", false);
        }

        if (LevelManager.instance.canMove && isMoving)
        {
            rangerAnimator.SetBool("isMoving", true);
            rangerAnimator.SetBool("isIdle", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rangerAnimator.SetTrigger("isAttacking");
        }

        

        if (!LevelManager.instance.isDashing && !isMoving)
        {
            rangerAnimator.SetBool("isIdle", true);
            rangerAnimator.SetBool("isMoving", false);
        }


    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.tag == "Ground" )
        //{
        //    grounded = true;
        //}


        if (col.gameObject.tag ==  "Enemy" && !shielded)
        {
            if(col.gameObject.GetComponent<MediumMeleeController>().isAttacking)
            {
                LevelManager.instance.playerHealth -= 1;
                if (col.transform.position.x >= transform.position.x)
                {
                    StartCoroutine("pushLeft");
                }
                else if (col.transform.position.x <= transform.position.x)
                {
                    StartCoroutine("pushRight");
                }
            }
           
        }


    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "ResetZone")
        {
            playerRB.velocity = new Vector2(0, 0);
            transform.position = respawnPoint.transform.position;
        }

        if(col.gameObject.name == "ThiefCollectible")
        {
            hasThiefCollectible = true;
            col.gameObject.SetActive(false);
            LevelManager.instance.activateThiefSign();
        }

        if (col.gameObject.name == "RangerCollectible")
        {
            hasRangerCollectible = true;
            col.gameObject.SetActive(false);
            LevelManager.instance.activateRangerSign();
        }

        if (col.gameObject.tag == "Coin")
        {
            col.gameObject.SetActive(false);
            LevelManager.instance.collectibleCount += 1;
        }

    }


    private void FlipCharacter()
    {

        if (LevelManager.instance.moveRight == 1 && !facingRight)
        {
            Vector3 v = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            transform.localScale = v;
            facingRight = true;
        }
        if (LevelManager.instance.moveRight == -1 && facingRight)
        {
            Vector3 v = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            transform.localScale = v;
            facingRight = false;
        }



    }


    IEnumerator MovePlayer()
    {

        if (Input.GetAxis("Horizontal") > 0 && LevelManager.instance.canMove)
        {
            isMoving = true;
            //LevelManager.instance.moveRight = 1;
        }
        else if (Input.GetAxis("Horizontal") < 0 && LevelManager.instance.canMove)
        {
            isMoving = true;
            //LevelManager.instance.moveRight = -1;
        }
        else
        {
            isMoving = false;
        }
        yield return null;
    }


    //IEnumerator PushPlayer (int Direction)
    //{
    //    //Debug.Log(Direction);
    //    LevelManager.instance.canMove = false;
    //    playerRB.velocity = new Vector2(0, 0);
    //    pushForce.x *= Direction;
    //    //Debug.Log(pushForce);
    //    playerRB.AddForce(pushForce, ForceMode2D.Impulse);
    //    yield return new WaitForSeconds(1f);
    //    LevelManager.instance.canMove = true; 
    //}

    IEnumerator pushRight()
    {        

        //stops the player from moving using the arrow keys
        LevelManager.instance.canMove = false;
        //sets
        playerRB.velocity = new Vector2(0, 0);
        Vector2 v = new Vector2(10f, 10f);
        playerRB.AddForce(v, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        LevelManager.instance.canMove = true;

    }

    IEnumerator pushLeft()
    {

        LevelManager.instance.canMove = false;
        playerRB.velocity = new Vector2(0, 0);
        Vector2 v = new Vector2(-10f, 10f);
        playerRB.AddForce(v, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        LevelManager.instance.canMove = true;

    }




}
