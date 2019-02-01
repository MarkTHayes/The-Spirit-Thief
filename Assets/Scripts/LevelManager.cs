using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


    public static LevelManager instance;

    //Player Variables
    public GameObject playerGO;
    public PlayerHandler playerHandler;
    public int playerHealth;
    public int playerSpeed;
    public float playerJumpForce;
    public Rigidbody2D playerRB;
    public Vector3 playerVelocity;
    public bool resetVelocity;
    public float resetVelocityTime;
    public bool canMove;

    public int playerState;

    public int playerSoulCount = 30;

    public bool canAttack;
    public float attackCooldown;

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


    //Ranger Variables
    public bool canDash;
    public float dashForce;
    public float dashTimer;
    public GameObject rangerAttackPoint;
    public Transform rangerRotatePoint;
    public float rotAngle;


    //Checkpoint Variable
    public GameObject checkpoint;
    public bool canInteract;
    


    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {

        canShield = true;
        canAttack = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerSoulCount < 0)
        {
            playerSoulCount = 0;
        }

        if (attackCooldown < Time.time)
        {
            canAttack = true;
        }
       
        //Warrior
        if (wAttackTimer < Time.time)
        {
            wAttack = false;
        }

        if(Time.time > resetVelocityTime && resetVelocity)
        {
            resetVelocity = false;
            ResetVelocity();
            canMove = true;

        }

        rangerRotateAttackPoint();




	}


    public void ShieldedCooldown()
    {

        canShield = true;

    }

    public void WarriorAttack()
    {
        if (canAttack)
        {
            canAttack = false;
            attackCooldown = Time.time + 2;
            wAttack = true;
            wAttackTimer = Time.time + 0.5f;

        }


    }

    public void RangerDash()
    {
        canMove = false;
        resetVelocityTime = Time.time + 0.15f;
        resetVelocity = true;
        playerVelocity = playerRB.velocity;
        playerRB.velocity = new Vector3(0, 0, 0);
        playerRB.gravityScale = 0;
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        Vector3 v = new Vector3(dashForce * playerHandler.moveRight, 0, 0);
        playerRB.AddForce(v, ForceMode2D.Impulse);
        playerRB.gravityScale = 1;        
         
        
    }

    public void ResetVelocity()
    {
        playerRB.velocity = playerVelocity;
    }


    public void rangerRotateAttackPoint()
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


        Vector3 difference = mousePos - rangerRotatePoint.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rangerRotatePoint.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //Vector3 dir = mousePos - rangerRotatePoint.transform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //Vector2 newDir =  Vector3.RotateTowards(transform.forward, mousePos - rangerRotatePoint.transform.position, 10f, 0.0f);
        //rangerRotatePoint.transform.rotation = Quaternion.LookRotation(newDir);


        //Vector3 targetDir = rangerRotatePoint.position - mousePos;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1f, 0.0f);
        //rangerRotatePoint.transform.rotation = Quaternion.LookRotation(newDir);        

    }




}
