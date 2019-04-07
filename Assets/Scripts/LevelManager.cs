using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {


    public static LevelManager instance;


    public LayerMask enemyLayer;

    //Input variables
    public bool leftMouse;

    //Player Variables
    public GameObject playerGO;
    public PlayerHandler playerHandler;
    public int playerHealth;
    public int playerSpeed;
    public float playerJumpForce;
    public Rigidbody2D playerRB;
    public Vector3 playerVelocity;

    public int collectibleCount;
    
    public bool canMove;
    public int moveRight;
    public float Score;
    public Text scoreInc;

    public int playerState;

    public int playerSoulCount = 30;

    public bool canAttack;
    

    //Arrow Variables
    public float arrowForce;
    public GameObject arrow;


    //Warrior Variable
    public bool canShield;
    public float shieldedTime;
    public GameObject wAttackBox;
    public Transform wAttackPoint;
    public bool wAttack;
    public float wAttackTimer;
    public float wAttackCooldown;


    //Ranger Variables
    public bool canDash;
    public bool isDashing;
    public float dashForce;
    public float dashTimer;
    public float dashCooldown;
    public bool resetVelocity;
    public float resetVelocityTime;
    public GameObject rangerAttackPoint;
    public Transform rangerRotatePoint;
    public float rotAngle;
    public bool canFireArrow;
    public float rAttackCooldown;
    


    //Thief Variables
    public int assassinateDamageValue;
    public bool canBlink;
    public bool isBlinking;
    public float blinkCooldown;
    public float blinkDuration;
    public bool tAttack;
    public float tAttackTimer;
    public float tAttackCooldown;
    


    //Checkpoint Variable
    public GameObject checkpoint;
    public bool canInteract;

    //Tutorial Text Objects
    private GameObject thiefSign1;
    private GameObject rangerSign1;
    


    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {

        Cursor.lockState = CursorLockMode.Confined;

        canFireArrow = true;
        canShield = true;
        canAttack = true;
        moveRight = 1;
        dashCooldown = 3f;

        //begins the coroutine that moves the reticle on the screen, the coroutine is an endless loop so this only needs to be called once
        StartCoroutine("RangerRotateAttackPoint");

        //assigns the gameObject to the variable
        thiefSign1 = GameObject.Find("Sign_Thief");
        thiefSign1.SetActive(false);

        rangerSign1 = GameObject.Find("Sign_Ranger");
        rangerSign1.SetActive(false);


    }
	
	// Update is called once per frame
	void Update () {

        //checks if the number of collectibles is greater than or equal to the number required to open the door.
        if (LevelManager.instance.collectibleCount >= 3)
        {
            Debug.Log("Open Door");
        }


        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("left mouse pressed");
        }


        //if (Input.GetMouseButtonDown(0)
        //    {

        //}

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("LeftShift pressed");
        }


        //Stops the soul counter from going below 0
		if (playerSoulCount < 0)
        {
            playerSoulCount = 0;
        }


        //Calls the input manager every frame to de-clutter the update function
        InputManager();

        


        //Ranger

        //Restores gravity and velocity after the dash has finished
        //if(Time.time > resetVelocityTime && resetVelocity)
        //{
        //    resetVelocity = false;
        //    ResetVelocity();
        //    canMove = true;

        //}

       // RangerRotateAttackPoint();

        //if (Time.time > dashCooldown)
        //{
        //    canDash = true;
        //}




       // scoreInc.text = "score = " + Score;


    }
    
 
     


    public void activateThiefSign ()
    {

        thiefSign1.SetActive(true);

    }

    public void activateRangerSign()
    {
        rangerSign1.SetActive(true);
    }


    public void InputManager()
    {

        //sets a variable to check whether or not the left shift key is down.
        //this is to prevent the need for getkey to be checked constantly

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            leftMouse = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            leftMouse = false;
        }
            

    }

    public void ShieldedCooldown()
    {

        canShield = true;

    }

    public void WarriorAttack()
    {
        if (canAttack)
        {

            //Debug.Log(rangerRotatePoint.transform.eulerAngles.z);
            canAttack = false;            
            StartCoroutine("WarriorAttackCooldown");
            wAttack = true;
            StartCoroutine("WarriorAttackingTime");

        }


    }


    public void WarriorShieldPush()
    {
        Debug.Log("boosh");

        //creates an array to store the returned gamebjects
        Collider2D[] enemiesInRadius;
        //stores the colliders of objects on the enemyLayer collission layer in the array within 7f unitys
        enemiesInRadius = Physics2D.OverlapCircleAll(playerGO.transform.position, 7f, enemyLayer);

        //loops through each collider in the array
        foreach(Collider2D i in enemiesInRadius)
        {
            if (i.gameObject.tag == "Enemy")
            {
                //creates a vector that is a line from the player directly to the enemy
                Vector2 v = new Vector2(i.gameObject.transform.position.x - playerGO.transform.position.x, i.gameObject.transform.position.y - playerGO.transform.position.y);
                Debug.Log(i.gameObject.name);
                //increases the y position so the enemy is pushed up more
                v.y = 2;
                //goes through the the array, calling the push enemy coroutine on the enemy that is being pushed
                //this is to disable the canMove variable and then re-set it 
                i.gameObject.GetComponent<MediumMeleeController>().StartCoroutine("PushEnemy", "none");
                //adds the force to the object in the array
                //normalised stops the vector's magnitude being different, doing this means that they are all pushed with the same force
                i.gameObject.GetComponent<Rigidbody2D>().AddForce(v.normalized * 300);
            }
        }

    }


    public void RangerDash()
    {
        //stops the player from doing multiple dashes or moving while the dash is in progress
        isDashing = true;
        canDash = false;
        canMove = false;
        playerHandler.StopCoroutine("MovePlayer");

        //Stops the player from going through colliders by checking collisions more often
        playerRB.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        //Starts the timer to return the velocity of the player to what it was before the dash had initiated
        //resetVelocityTime = Time.time + 0.15f;
        StartCoroutine("ResetVelocity");
        resetVelocity = true;

        //stores the player's velocity before the dash is applied
        playerVelocity = playerRB.velocity;

        //sets the players velocity to 0,0,0 so that the dash is always the exact same
        playerRB.velocity = new Vector3(0, 0, 0);

        //disables gravity so the player won't be pulled down while dashing
        playerRB.gravityScale = 0;
        Debug.Log(Input.GetAxisRaw("Horizontal"));

        //creates the vector3 with the dashForce varianle multiplied by the direction the character was last moving
        Vector3 v = new Vector3(dashForce * moveRight, 0, 0);

        //adds the force for the dash
        playerRB.AddForce(v, ForceMode2D.Impulse);

        //enables gravity again and at this point the timer to reset the velocity of the character should be complete
        playerRB.gravityScale = 1;

        //starts the cooldown timer to allow the character to dash again
        //dashCooldown = Time.time + 3;
        StartCoroutine("DashCooldown");
        
         
        
    }


    IEnumerator FireArrow()
    {
        //if you are allowed to shoot an arrow
        if(canFireArrow)
        {
            //prevents you from being able to shoot another arrow
            canFireArrow = false;
            //creates the arrow at the attack point of the ranger, but the rotaton is that of the rotation point
            Instantiate(arrow, rangerAttackPoint.transform.position, rangerRotatePoint.rotation);
            //starts the coroutine that resets the canFireArrow variable
            StartCoroutine("RangerAttackCooldown");
        }
           
           yield return null;
        

    }

    IEnumerator RangerAttackCooldown()
    {
        yield return new WaitForSeconds(tAttackCooldown);
        canFireArrow = true;


    }

    //public void ResetVelocity()
    //{
    //    playerRB.velocity = playerVelocity;
    //}


    IEnumerator RangerRotateAttackPoint()
    {


        for(int i = 1; i > 0; i++)
        {
            //Sets the mouse position to the world space
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            //The issue with the following attempt is it is also rotating around the Y value and can move closer to the the RangerRotatePoint GameObject
            //I would like to keep the distance between these two objects the same which is the reason for the other attempts below.

            //Vector3 v = new Vector3(rangerRotatePoint.localPosition.x, rangerRotatePoint.localPosition.y, rangerRotatePoint.localPosition.z);
            //rangerAttackPoint.transform.RotateAround(v, Vector3.forward, rotAngle * Time.deltaTime);
            //rangerAttackPoint.transform.position = Vector2.MoveTowards(rangerRotatePoint.transform.position, mousePos, 1f);
            //Vector3 targetDir = mousePos - rangerRotatePoint.transform.position;
            //rangerAttackPoint.transform.rotation = Quaternion.LookRotation(targetDir, Vector3.forward);
            //Debug.Log(mousePos);


            //The following attempts all require the RangerAttackPoint game object to be the child of the RangerRotatePoint Gameobject.
            //They are all attempting to change the z rotation of the RangerRotatePoint to be facing the mouse position.


            Vector3 difference = mousePos * moveRight - rangerRotatePoint.transform.position * moveRight;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            rotationZ = Mathf.Clamp(rotationZ, -80, 80);
            rangerRotatePoint.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);




            //THIS NEEDS TO BE MOVED
            //Changes the player direction based on the mouse position relative to the player
            if (mousePos.x > rangerRotatePoint.transform.position.x)
            {
                if (moveRight == -1)
                {
                    moveRight = 1;
                }
            }
            else if (mousePos.x < rangerRotatePoint.transform.position.x)
            {
                if (moveRight == 1)
                {
                    moveRight = -1;
                }
            }








            //Vector3 dir = mousePos - rangerRotatePoint.transform.position;
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            //Vector2 newDir =  Vector3.RotateTowards(transform.forward, mousePos - rangerRotatePoint.transform.position, 10f, 0.0f);
            //rangerRotatePoint.transform.rotation = Quaternion.LookRotation(newDir);


            //Vector3 targetDir = rangerRotatePoint.position - mousePos;
            //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1f, 0.0f);
            //rangerRotatePoint.transform.rotation = Quaternion.LookRotation(newDir);  
            yield return new WaitForSeconds(0.1f);
        }

        

    }



    public void ThiefBlink()
    {
        //checks whether the character is blinking, and whether or not they are not currently blinking
        if (canBlink && !isBlinking)
        {
            //stops you from blinking while blinking
            canBlink = false;
            //sets you to blinking
            isBlinking = true;
            //lets the player ignore collision with the layer that the thief objects are located on
            Physics2D.IgnoreLayerCollision(13, 12, true);
            // starts the timer to reset the ability to blink
            StartCoroutine("BlinkCooldown");
            //starts the coroutine to limit the duration of which you can be blinking
            StartCoroutine("BlinkDuration");
        }
    }

    public void ThiefAttack()
    {
        if (canAttack)
        {

            canAttack = false;
            StartCoroutine("ThiefAttackCooldown");
            tAttack = true;
            StartCoroutine("ThiefAttackingTime");

        }
    }

    //Warrior Timers
    IEnumerator WarriorAttackCooldown()
    {
        yield return new WaitForSeconds(wAttackCooldown);
        canAttack = true;        
    }

    IEnumerator WarriorAttackingTime()
    {
        Debug.Log("isAttacking");
        yield return new WaitForSeconds(wAttackTimer);
        Debug.Log("stopsAttacking");
        wAttack = false;
    }

   IEnumerator WarriorShieldCooldown(int time)
    {
        WarriorShieldPush();
        
        yield return new WaitForSeconds(time);
        if (!canShield)
        {
            canShield = true;
        }
    }


    public void WarriorShieldedDuration()
    {
        if (playerHandler.shielded)
        {
            playerHandler.shielded = false;
            StartCoroutine("WarriorShieldCooldown", 8);
        }
    }

    //Ranger Timers
    IEnumerator ResetVelocity()
    {
        yield return new WaitForSeconds(resetVelocityTime);
        if (resetVelocity)
        {
            //stops reset velocity from being reset multiple times
            resetVelocity = false;
            //lets the player control the movement of the character again
            canMove = true;
            //sets the velocity
            playerRB.velocity = playerVelocity;
            isDashing = false;
            playerRB.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            //playerHandler.StartCoroutine("MovePlayer");
        }

    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }

    //Thief Timers
    IEnumerator BlinkCooldown()
    {
        yield return new WaitForSeconds(blinkCooldown);
        canBlink = true;        
    }

    IEnumerator BlinkDuration()
    {
        yield return new WaitForSeconds(blinkDuration);
        isBlinking = false;
        Physics2D.IgnoreLayerCollision(13, 12, false);
    }

    IEnumerator ThiefAttackCooldown()
    {
        yield return new WaitForSeconds(tAttackCooldown);
        canAttack = true;
    }

    IEnumerator ThiefAttackingTime()
    {
        Debug.Log("ThiefIsAttacking");
        yield return new WaitForSeconds(tAttackTimer);
        Debug.Log("ThiefStopsAttacking");
        tAttack = false;
    }





}
