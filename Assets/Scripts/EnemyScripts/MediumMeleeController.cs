using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumMeleeController : MonoBehaviour {


    public int health;

    public bool canTakeDamage;

    public float damageCooldown;

    public int facing;
    public bool alert;
    public float speed;

    public TriggerZonesController personalTrigger;

    

	// Use this for initialization
	void Start () {


        facing = 1;
        speed = 3f;
        
		
	}
	
	// Update is called once per frame
	void Update () {


        if(personalTrigger.alertEnemy)
        {
            alert = true;
        }
        else
        {
            alert = false;
        }

        


        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (Time.time > damageCooldown)
        {
            canTakeDamage = true;
            
        }

        if (canTakeDamage && Input.GetKeyDown(KeyCode.Mouse0))
        {
            health = health - 1;
        }

        transform.Translate(speed * facing * Time.deltaTime, 0, 0);

        if (alert)
        {
            if (transform.position.x > LevelManager.instance.playerGO.transform.position.x)
            {
                facing = -1;               
            }
            else if (transform.position.x < LevelManager.instance.playerGO.transform.position.x)
            {
                facing = 1;
            }


        }

        
		
	}


    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "W_AttackBox" )
        {

            canTakeDamage = true;
            damageCooldown = Time.time + 1f;
            canTakeDamage = false;
            
        }

        
        
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "W_AttackBox")
        {

            canTakeDamage = true;

        }

        if (col.gameObject.tag == "TriggerZone" && !alert)
        {
            facing = -facing;
        }




    }


}
