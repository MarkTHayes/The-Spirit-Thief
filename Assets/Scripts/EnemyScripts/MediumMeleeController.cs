using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumMeleeController : MonoBehaviour {

    public bool canBeAssassinated;
    public int health;

    public bool canTakeDamage;
    public bool takeDamage;

    public float damageCooldown;

    public bool canMove;
    public int facing;
    public bool alert;
    public float speed;

    public TriggerZonesController personalTrigger;

    public float enemyScale;

    private Rigidbody2D myRB;

    public Vector3 enemyMovementVector;

    public bool canAttack;
    public bool isAttacking;

    

	// Use this for initialization
	void Start () {

        canAttack = true;
        canMove = true;
        canTakeDamage = true;
        facing = 1;
       // speed = 3f;
        enemyScale = transform.localScale.x;
        myRB = GetComponent<Rigidbody2D>();
        
		
	}
	
	// Update is called once per frame
	void Update () {

        CheckForAttack();


        if (facing == 1 && (transform.position.x > LevelManager.instance.playerGO.transform.position.x))
        {
            canBeAssassinated = true;
           
        }
        else if (facing == -1 && (transform.position.x < LevelManager.instance.playerGO.transform.position.x))
        {
            canBeAssassinated = true;
        }
        else
        {
            canBeAssassinated = false;
        }



        if(personalTrigger.alertEnemy)
        {
            if (facing == 1 && transform.position.x < LevelManager.instance.playerGO.transform.position.x)
            {
                alert = true;
            }
            else if(facing == -1 && transform.position.x > LevelManager.instance.playerGO.transform.position.x)
            {
                alert = true;
            }
        }
        else
        {
            alert = false;
        }




        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //if (Time.time > damageCooldown)
        //{
        //    canTakeDamage = true;

        //}

        if (canTakeDamage && LevelManager.instance.wAttack && LevelManager.instance.leftMouse)
        {
            if (takeDamage)
            {
                canTakeDamage = false;
                Debug.Log("take damage");
                health = health - 3;
                StartCoroutine("DamageCooldown");
                StartCoroutine("PushEnemy", "Warrior");
            }
        }
        else if (canTakeDamage && LevelManager.instance.tAttack && LevelManager.instance.leftMouse)
        {
            if (takeDamage)
            {
                canTakeDamage = false;
                Debug.Log("take damage");
                health = health - 1;
                StartCoroutine("DamageCooldown");
                StartCoroutine("PushEnemy", "Thief");
            }
        }

       

        if (alert)
        {
            if (transform.position.x > LevelManager.instance.playerGO.transform.position.x && facing != -1)
            {
                facing = -1;               
            }
            else if (transform.position.x < LevelManager.instance.playerGO.transform.position.x && facing != 1)
            {
                facing = 1;
            }


        }

        if (facing == 1 && enemyScale < 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -newScale.x;
            transform.localScale = newScale;
            enemyScale = transform.localScale.x;
        }
        else if (facing == -1 && enemyScale > 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -newScale.x;
            transform.localScale = newScale;
            enemyScale = transform.localScale.x;
        }

        
		
	}

    private void FixedUpdate()
    {
        enemyMovementVector = new Vector3(speed * facing, myRB.velocity.y, 0);
        if (canMove)
        {
            myRB.velocity = enemyMovementVector;
        }


    }

    public void CheckForAttack()
    {
        if ((LevelManager.instance.playerGO.transform.position.x - transform.position.x) < 3 && (LevelManager.instance.playerGO.transform.position.x - transform.position.x) > -3)
        {
            if (alert)
            {
                if (canAttack)
                {
                    StartCoroutine("Attack");
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerArrow" && canTakeDamage)
        {
            health -= 1;
            StartCoroutine("DamageCooldown");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "W_AttackBox" )
        {
            takeDamage = true;
            
        }
        
        
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "W_AttackBox")
        {
            takeDamage = false;

        }

        if (col.gameObject.tag == "TriggerZone" && !alert)
        {
           // Debug.Log("Left zone");
            facing = -facing;
        }




    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
        
    }


    IEnumerator PushEnemy(string pushType)
    {
        canMove = false;
        myRB.velocity = new Vector2(0f, 0f);
        int direction = 0;
        if (LevelManager.instance.playerGO.transform.position.x > transform.position.x)
        {
            direction = -1;
        }
        else if (LevelManager.instance.playerGO.transform.position.x < transform.position.x)
        {
            direction = 1;
        }

        if (pushType == "none")
        {

        }


        if (pushType == "Warrior")
        {
            Vector2 pushForce = new Vector2(10 * direction,5);
            myRB.AddForce(pushForce, ForceMode2D.Impulse); 
        }
        else if (pushType == "Thief")
        {
            Vector2 pushForce = new Vector2(5 * direction,2.5f);
            myRB.AddForce(pushForce, ForceMode2D.Impulse); 
        }

        yield return new WaitForSeconds(0.5f);
        canMove = true;


       
    }


    IEnumerator Attack()
    {
        canAttack = false;
        canMove = false;
        isAttacking = true;
        Vector3 v = new Vector3(0, myRB.velocity.y, 0);
        myRB.velocity = v;
        yield return new WaitForSeconds(0.5f);
        Vector3 pushForce = new Vector3(10 * facing, 0, 0);
        myRB.AddForce(pushForce, ForceMode2D.Impulse);
        StartCoroutine("StopAttacking");
        yield return new WaitForSeconds(2);
        canMove = true;
        StartCoroutine("AttackCooldown");




    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(4);
        canAttack = true;
    }

    IEnumerator StopAttacking()
    {
        yield return new WaitForSeconds(0.6f);
        isAttacking = false;
    }


}
