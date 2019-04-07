using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorWallController : MonoBehaviour {


    public bool takeDamage;
    public bool canTakeDamage;
    public int health;

	// Use this for initialization
	void Start () {


        health = 2;
        canTakeDamage = true;
		
	}

    // Update is called once per frame
    void Update()
    {
        if (canTakeDamage && LevelManager.instance.wAttack && LevelManager.instance.leftMouse)
        {
            if (takeDamage)
            {
                canTakeDamage = false;                
                health = health - 1;
                StartCoroutine("DamageCooldown");
                
            }
        }


        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {        
        if (col.gameObject.tag == "W_AttackBox")
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
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1);
        canTakeDamage = true;

    }


}
