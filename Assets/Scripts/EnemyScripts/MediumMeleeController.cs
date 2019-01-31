using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumMeleeController : MonoBehaviour {


    public int health;

    public bool canTakeDamage;

    public float damageCooldown;

	// Use this for initialization
	void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {

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
    }


}
