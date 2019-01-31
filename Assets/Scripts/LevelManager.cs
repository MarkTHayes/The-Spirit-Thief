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

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.WorldToScreenPoint(mousePos);
        mousePos.z = 0;
        Vector3 v = new Vector3(rangerRotatePoint.localPosition.x, rangerRotatePoint.localPosition.y, rangerRotatePoint.localPosition.z);
        rangerAttackPoint.transform.RotateAround(v, Vector3.forward, rotAngle * Time.deltaTime);
        rangerAttackPoint.transform.position = Vector2.MoveTowards(rangerRotatePoint.transform.position, mousePos, 1f);
        Debug.Log(mousePos);
        

    }
    



}
